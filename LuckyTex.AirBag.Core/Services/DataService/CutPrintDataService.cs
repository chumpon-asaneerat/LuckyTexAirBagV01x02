#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using NLib;
using NLib.Data;
using NLib.Logs;
using NLib.Xml;

using LuckyTex.Configs;
using LuckyTex.Domains;
using LuckyTex.Models;

using System.Data;
using System.Data.OracleClient;

#endregion

namespace LuckyTex.Services
{
    #region CutPrint Data Service

    /// <summary>
    /// The data service for CutPrint process.
    /// </summary>
    public class CutPrintDataService : BaseDataService
    {
        #region Singelton

        private static CutPrintDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static CutPrintDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(CutPrintDataService))
                    {
                        _instance = new CutPrintDataService();
                    }

                }
                return _instance;
            }
        }

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private CutPrintDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~CutPrintDataService()
        {
        }

        #endregion

        #region Public Methods

        #region GetMachines

        /// <summary>
        /// Gets all Finishing Machines.
        /// </summary>
        /// <returns>Returns inspection machine.</returns>
        public List<CutPrintMCItem> GetMachines()
        {
            List<CutPrintMCItem> results = new List<CutPrintMCItem>();

            // Inspection Process ID = 8
            List<GETMACHINELISTBYPROCESSIDResult> dbResults = this.GetMachines(7);
            if (null == dbResults || dbResults.Count <= 0)
                return results;

            int mcNo = 1;
            CutPrintMCItem inst = null;
            foreach (GETMACHINELISTBYPROCESSIDResult dbResult in dbResults)
            {
                inst = new CutPrintMCItem();

                inst.MCNo = mcNo;
                inst.MCId = dbResult.MACHINEID;
                inst.DisplayName = dbResult.MCNAME;

                results.Add(inst);

                ++mcNo;
            }

            inst = new CutPrintMCItem();
            inst.MCNo = mcNo;
            inst.MCId = "C";
            inst.DisplayName = "Cut&Print List";
            results.Add(inst);
            ++mcNo;

            return results;
        }

        public List<CutPrintMCItem> GetCutPrintMCItem()
        {
            List<CutPrintMCItem> results = new List<CutPrintMCItem>();

            // Inspection Process ID = 8
            List<GETMACHINELISTBYPROCESSIDResult> dbResults = this.GetMachines(7);
            if (null == dbResults || dbResults.Count <= 0)
                return results;

            int mcNo = 1;
            CutPrintMCItem inst = null;
            foreach (GETMACHINELISTBYPROCESSIDResult dbResult in dbResults)
            {
                inst = new CutPrintMCItem();

                inst.MCNo = mcNo;
                inst.MCId = dbResult.MACHINEID;
                inst.DisplayName = dbResult.MCNAME;

                results.Add(inst);

                ++mcNo;
            }

            return results;
        }

        #endregion

        #region Create new Session

        /// <summary>
        /// Gets Finishing Session.
        /// </summary>
        /// <param name="mcItem">The machine information.</param>
        /// <param name="loginResult">The current user login information.</param>
        /// <returns>Returns Inspection session instance.</returns>
        public CutPrintSession GetSession(CutPrintMCItem mcItem, LogInResult loginResult)
        {
            CutPrintSession result = new CutPrintSession();
            result.Init(mcItem, loginResult);
            return result;
        }

        #endregion

        #region GetSuspendInspectionProcess
        /// <summary>
        /// Get Suspend Inspection Process.
        /// </summary>
        /// <param name="machineId">The machine id.</param>
        /// <returns>Returns list of inspection data that suspend.</returns>
        public List<INS_GETMCSUSPENDDATAResult> GetSuspendInspectionProcess(string machineId)
        {
            if (!HasConnection())
                return null;

            INS_GETMCSUSPENDDATAParameter dbPara = new INS_GETMCSUSPENDDATAParameter();
            dbPara.P_INSMC = machineId;
            List<INS_GETMCSUSPENDDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.INS_GETMCSUSPENDDATA(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
                dbResults = null;
            }

            return dbResults;
        }

        #endregion

        #region เพิ่มใหม่ CUT_GETFINISHINGDATA ใช้ในการ Load Cut FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CUT_GETFINISHINGDATA> GetCUT_GETFINISHINGDATAList(string ITEMLOT)
        {
            List<CUT_GETFINISHINGDATA> results = null;

            if (!HasConnection())
                return results;

            CUT_GETFINISHINGDATAParameter dbPara = new CUT_GETFINISHINGDATAParameter();
            dbPara.P_ITEMLOT = ITEMLOT;

            List<CUT_GETFINISHINGDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.CUT_GETFINISHINGDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<CUT_GETFINISHINGDATA>();
                    foreach (CUT_GETFINISHINGDATAResult dbResult in dbResults)
                    {
                        CUT_GETFINISHINGDATA inst = new CUT_GETFINISHINGDATA();

                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.ITEMLOT = dbResult.ITEMLOT;
                        inst.BATCHNO = dbResult.BATCHNO;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;

                        //เพิ่มใหม่ 20/01/16
                        inst.FINISHINGPROCESS = dbResult.FINISHINGPROCESS;

                        //เพิ่มใหม่ 27/06/16
                        inst.SND_BARCODE = dbResult.SND_BARCODE;

                        inst.CUSTOMERID = dbResult.CUSTOMERID;

                        //เพิ่มใหม่ 04/10/17
                        inst.BEFORE_WIDTH = dbResult.BEFORE_WIDTH;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ CUT_GETCONDITIONBYITEMCODE ใช้ในการ Load CONDITIONBYITEMCODE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <returns></returns>
        public List<CUT_GETCONDITIONBYITEMCODE> CUT_GETCONDITIONBYITEMCODE(string P_ITMCODE)
        {
            List<CUT_GETCONDITIONBYITEMCODE> results = null;

            if (!HasConnection())
                return results;

            CUT_GETCONDITIONBYITEMCODEParameter dbPara = new CUT_GETCONDITIONBYITEMCODEParameter();
            dbPara.P_ITMCODE = P_ITMCODE;

            List<CUT_GETCONDITIONBYITEMCODEResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.CUT_GETCONDITIONBYITEMCODE(dbPara);
                if (null != dbResults)
                {
                    results = new List<CUT_GETCONDITIONBYITEMCODE>();
                    foreach (CUT_GETCONDITIONBYITEMCODEResult dbResult in dbResults)
                    {
                        CUT_GETCONDITIONBYITEMCODE inst = new CUT_GETCONDITIONBYITEMCODE();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WIDTHBARCODE_MIN = dbResult.WIDTHBARCODE_MIN;
                        inst.WIDTHBARCODE_MAX = dbResult.WIDTHBARCODE_MAX;
                        inst.DISTANTBARCODE_MIN = dbResult.DISTANTBARCODE_MIN;
                        inst.DISTANTBARCODE_MAX = dbResult.DISTANTBARCODE_MAX;
                        inst.DISTANTLINE_MIN = dbResult.DISTANTLINE_MIN;
                        inst.DISTANTLINE_MAX = dbResult.DISTANTLINE_MAX;
                        inst.DENSITYWARP_MIN = dbResult.DENSITYWARP_MIN;
                        inst.DENSITYWARP_MAX = dbResult.DENSITYWARP_MAX;
                        inst.DENSITYWEFT_MIN = dbResult.DENSITYWEFT_MIN;
                        inst.DENSITYWEFT_MAX = dbResult.DENSITYWEFT_MAX;
                        inst.SPEED = dbResult.SPEED;
                        inst.SPEED_MARGIN = dbResult.SPEED_MARGIN;
                        inst.AFTER_WIDTH = dbResult.AFTER_WIDTH;
                        inst.SHOWSELVAGE = dbResult.SHOWSELVAGE;

                        #region strWIDTHBARCODE

                        if (inst.WIDTHBARCODE_MIN != null && inst.WIDTHBARCODE_MAX != null)
                        {
                            inst.strWIDTHBARCODE = inst.WIDTHBARCODE_MIN.Value.ToString("#,##0.##") + " - " + inst.WIDTHBARCODE_MAX.Value.ToString("#,##0.##");
                        }
                        else
                        {
                            inst.strWIDTHBARCODE = "-";
                        }

                        #endregion

                        #region strDISTANTBARCODE

                        if (inst.DISTANTBARCODE_MIN != null && inst.DISTANTBARCODE_MAX != null)
                        {
                            inst.strDISTANTBARCODE = inst.DISTANTBARCODE_MIN.Value.ToString("#,##0.##") + " - " + inst.DISTANTBARCODE_MAX.Value.ToString("#,##0.##");
                        }
                        else
                        {
                            inst.strDISTANTBARCODE = "-";
                        }

                        #endregion

                        #region strDISTANTLINE

                        if (inst.DISTANTLINE_MIN != null && inst.DISTANTLINE_MAX != null)
                        {
                            inst.strDISTANTLINE = inst.DISTANTLINE_MIN.Value.ToString("#,##0.##") + " - " + inst.DISTANTLINE_MAX.Value.ToString("#,##0.##");
                        }
                        else
                        {
                            inst.strDISTANTLINE = "-";
                        }

                        #endregion

                        #region strDENSITYWARP

                        if (inst.DENSITYWARP_MIN != null && inst.DENSITYWARP_MAX != null)
                        {
                            inst.strDENSITYWARP = inst.DENSITYWARP_MIN.Value.ToString("#,##0.##") + " - " + inst.DENSITYWARP_MAX.Value.ToString("#,##0.##");
                        }
                        else
                        {
                            inst.strDENSITYWARP = "-";
                        }

                        #endregion

                        #region strDENSITYWEFT

                        if (inst.DENSITYWEFT_MIN != null && inst.DENSITYWEFT_MAX != null)
                        {
                            inst.strDENSITYWEFT = inst.DENSITYWEFT_MIN.Value.ToString("#,##0.##") + " - " + inst.DENSITYWEFT_MAX.Value.ToString("#,##0.##");
                        }
                        else
                        {
                            inst.strDENSITYWEFT = "-";
                        }

                        #endregion

                        #region strSPEED

                        if (inst.SPEED != null && inst.SPEED_MARGIN != null)
                        {
                            inst.strSPEED = inst.SPEED.Value.ToString("#,##0.##") + " - " + inst.SPEED_MARGIN.Value.ToString("#,##0.##");
                        }
                        else
                        {
                            inst.strSPEED = "-";
                        }

                        #endregion

                        #region strAFTER

                        if (inst.AFTER_WIDTH != null)
                        {
                            inst.strAFTER = "> " + inst.AFTER_WIDTH.Value.ToString("#,##0.##");
                        }
                        else
                        {
                            inst.strAFTER = "-";
                        }

                        #endregion

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ CUT_SERACHLIST ใช้ในการ Load CUT_SERACHLIST
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_STARTDATE"></param>
        /// <param name="P_MC"></param>
        /// <returns></returns>
        public List<CUT_SERACHLIST> CUT_SERACHLIST(string P_STARTDATE, string P_MC)
        {
            List<CUT_SERACHLIST> results = null;

            if (!HasConnection())
                return results;

            CUT_SERACHLISTParameter dbPara = new CUT_SERACHLISTParameter();
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_MC = P_MC;

            List<CUT_SERACHLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.CUT_SERACHLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<CUT_SERACHLIST>();
                    foreach (CUT_SERACHLISTResult dbResult in dbResults)
                    {
                        CUT_SERACHLIST inst = new CUT_SERACHLIST();

                        inst.ITEMLOT = dbResult.ITEMLOT;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.WIDTHBARCODE1 = dbResult.WIDTHBARCODE1;
                        inst.WIDTHBARCODE2 = dbResult.WIDTHBARCODE2;
                        inst.WIDTHBARCODE3 = dbResult.WIDTHBARCODE3;
                        inst.WIDTHBARCODE4 = dbResult.WIDTHBARCODE4;
                        inst.DISTANTBARCODE1 = dbResult.DISTANTBARCODE1;
                        inst.DISTANTBARCODE2 = dbResult.DISTANTBARCODE2;
                        inst.DISTANTBARCODE3 = dbResult.DISTANTBARCODE3;
                        inst.DISTANTBARCODE4 = dbResult.DISTANTBARCODE4;
                        inst.DISTANTLINE1 = dbResult.DISTANTLINE1;
                        inst.DISTANTLINE2 = dbResult.DISTANTLINE2;
                        inst.DISTANTLINE3 = dbResult.DISTANTLINE3;
                        inst.DENSITYWARP = dbResult.DENSITYWARP;
                        inst.DENSITYWEFT = dbResult.DENSITYWEFT;
                        inst.SPEED = dbResult.SPEED;
                        inst.BEFORE_WIDTH = dbResult.BEFORE_WIDTH;
                        inst.AFTER_WIDTH = dbResult.AFTER_WIDTH;
                        inst.BEGINROLL_LINE1 = dbResult.BEGINROLL_LINE1;
                        inst.BEGINROLL_LINE2 = dbResult.BEGINROLL_LINE2;
                        inst.BEGINROLL_LINE3 = dbResult.BEGINROLL_LINE3;
                        inst.BEGINROLL_LINE4 = dbResult.BEGINROLL_LINE4;
                        inst.ENDROLL_LINE1 = dbResult.ENDROLL_LINE1;
                        inst.ENDROLL_LINE2 = dbResult.ENDROLL_LINE2;
                        inst.ENDROLL_LINE3 = dbResult.ENDROLL_LINE3;
                        inst.ENDROLL_LINE4 = dbResult.ENDROLL_LINE4;
                        inst.OPERATORID = dbResult.OPERATORID;
                        inst.SELVAGE_LEFT = dbResult.SELVAGE_LEFT;
                        inst.SELVAGE_RIGHT = dbResult.SELVAGE_RIGHT;
                        inst.REMARK = dbResult.REMARK;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.MCNO = dbResult.MCNO;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.PARTNO = dbResult.PARTNO;
                        inst.MCNAME = dbResult.MCNAME;

                        // เพิ่มใหม่ 28/06/16
                        inst.SND_BARCODE = dbResult.SND_BARCODE;

                        inst.BEGINROLL2_LINE1 = dbResult.BEGINROLL2_LINE1;
                        inst.BEGINROLL2_LINE2 = dbResult.BEGINROLL2_LINE2;
                        inst.BEGINROLL2_LINE3 = dbResult.BEGINROLL2_LINE3;
                        inst.BEGINROLL2_LINE4 = dbResult.BEGINROLL2_LINE4;
                        inst.ENDROLL2_LINE1 = dbResult.ENDROLL2_LINE1;
                        inst.ENDROLL2_LINE2 = dbResult.ENDROLL2_LINE2;
                        inst.ENDROLL2_LINE3 = dbResult.ENDROLL2_LINE3;
                        inst.ENDROLL2_LINE4 = dbResult.ENDROLL2_LINE4;

                        //เพิ่มใหม่ 28/06/17
                        inst.TENSION = dbResult.TENSION;
                        inst.FINISHLOT = dbResult.FINISHLOT;
                        inst.FINISHLENGTH = dbResult.FINISHLENGTH;
                        inst.LENGTHPRINT = dbResult.LENGTHPRINT;

                        inst.STATUS = dbResult.STATUS;
                        inst.SUSPENDDATE = dbResult.SUSPENDDATE;
                        inst.SUSPENDBY = dbResult.SUSPENDBY;
                        inst.CLEARDATE = dbResult.CLEARDATE;
                        inst.CLEARREMARK = dbResult.CLEARREMARK;
                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.SUSPENDSTARTDATE = dbResult.SUSPENDSTARTDATE;

                        //เพิ่มใหม่ 25/08/17
                        inst.LENGTHDETAIL = dbResult.LENGTHDETAIL;
                        inst.FINISHLENGTH1 = dbResult.FINISHLENGTH1;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่ม CUT_INSERTDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ITEMLOT"></param>
        /// <param name="STARTDATE"></param>
        /// <param name="PRODUCTTYPEID"></param>
        /// <param name="OPERATORID"></param>
        /// <param name="REMARK"></param>
        /// <param name="MCNO"></param>
        /// <param name="WIDTH1"></param>
        /// <param name="WIDTH2"></param>
        /// <param name="WIDTH3"></param>
        /// <param name="WIDTH4"></param>
        /// <param name="DISTANTBAR1"></param>
        /// <param name="DISTANTBAR2"></param>
        /// <param name="DISTANTBAR3"></param>
        /// <param name="DISTANTBAR4"></param>
        /// <param name="DISTANTLINE1"></param>
        /// <param name="DISTANTLINE2"></param>
        /// <param name="DISTANTLINE3"></param>
        /// <param name="DENWARP"></param>
        /// <param name="DENWEFT"></param>
        /// <param name="SPEED"></param>
        /// <param name="WIDTHBE"></param>
        /// <param name="WIDTHAF"></param>
        /// <param name="BEGINLINE1"></param>
        /// <param name="BEGINLINE2"></param>
        /// <param name="BEGINLINE3"></param>
        /// <param name="BEGINLINE4"></param>
        /// <param name="ENDLINE1"></param>
        /// <param name="ENDLINE2"></param>
        /// <param name="ENDLINE3"></param>
        /// <param name="ENDLINE4"></param>
        /// <param name="SELVAGELEFT"></param>
        /// <param name="SELVAGERIGHT"></param>
        /// <param name="FINISHINGPROCESS"></param>
        /// <param name="SUSPENSTARTDATE"></param>
        /// <param name="P_2BEGINLINE1"></param>
        /// <param name="P_2BEGINLINE2"></param>
        /// <param name="P_2BEGINLINE3"></param>
        /// <param name="P_2BEGINLINE4"></param>
        /// <param name="P_2ENDLINE1"></param>
        /// <param name="P_2ENDLINE2"></param>
        /// <param name="P_2ENDLINE3"></param>
        /// <param name="P_2ENDLINE4"></param>
        /// <param name="P_TENSION"></param>
        /// <returns></returns>
        public bool CUT_INSERTDATA(string ITEMLOT, DateTime? STARTDATE, string PRODUCTTYPEID, string OPERATORID,
        string REMARK, string MCNO, decimal? WIDTH1, decimal? WIDTH2, decimal? WIDTH3, decimal? WIDTH4,
        decimal? DISTANTBAR1, decimal? DISTANTBAR2, decimal? DISTANTBAR3, decimal? DISTANTBAR4,
        decimal? DISTANTLINE1, decimal? DISTANTLINE2, decimal? DISTANTLINE3, decimal? DENWARP, decimal? DENWEFT,
        decimal? SPEED, decimal? WIDTHBE, decimal? WIDTHAF, 
        string BEGINLINE1, string BEGINLINE2, string BEGINLINE3, string BEGINLINE4,
        string ENDLINE1, string ENDLINE2, string ENDLINE3, string ENDLINE4, 
        string SELVAGELEFT, string SELVAGERIGHT, string FINISHINGPROCESS, DateTime? SUSPENSTARTDATE,
        string P_2BEGINLINE1, string P_2BEGINLINE2, string P_2BEGINLINE3, string P_2BEGINLINE4,
        string P_2ENDLINE1, string P_2ENDLINE2, string P_2ENDLINE3, string P_2ENDLINE4, decimal? P_TENSION, string P_LENGTHDETAIL, decimal? P_WIDTHAF_END)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(ITEMLOT))
                return false;
            if (string.IsNullOrWhiteSpace(PRODUCTTYPEID))
                return false;
            if (string.IsNullOrWhiteSpace(MCNO))
                return false;
            if (string.IsNullOrWhiteSpace(OPERATORID))
                return false;

            if (!HasConnection())
                return result;

            CUT_INSERTDATAParameter dbPara = new CUT_INSERTDATAParameter();

            dbPara.P_ITEMLOT = ITEMLOT;
            dbPara.P_STARTDATE = STARTDATE;
            dbPara.P_PRODUCTTYPEID = PRODUCTTYPEID;
            dbPara.P_OPERATORID = OPERATORID;
            dbPara.P_REMARK = REMARK;
            dbPara.P_MCNO = MCNO;
            dbPara.P_WIDTH1 = WIDTH1;
            dbPara.P_WIDTH2 = WIDTH2;
            dbPara.P_WIDTH3 = WIDTH3;
            dbPara.P_WIDTH4 = WIDTH4;
            dbPara.P_DISTANTBAR1 = DISTANTBAR1;
            dbPara.P_DISTANTBAR2 = DISTANTBAR2;
            dbPara.P_DISTANTBAR3 = DISTANTBAR3;
            dbPara.P_DISTANTBAR4 = DISTANTBAR4;
            dbPara.P_DISTANTLINE1 = DISTANTLINE1;
            dbPara.P_DISTANTLINE2 = DISTANTLINE2;
            dbPara.P_DISTANTLINE3 = DISTANTLINE3;
            dbPara.P_DENWARP = DENWARP;
            dbPara.P_DENWEFT = DENWEFT;
            dbPara.P_SPEED = SPEED;
            dbPara.P_WIDTHBE = WIDTHBE;
            dbPara.P_WIDTHAF = WIDTHAF;
            dbPara.P_BEGINLINE1 = BEGINLINE1;
            dbPara.P_BEGINLINE2 = BEGINLINE2;
            dbPara.P_BEGINLINE3 = BEGINLINE3;
            dbPara.P_BEGINLINE4 = BEGINLINE4;
            dbPara.P_ENDLINE1 = ENDLINE1;
            dbPara.P_ENDLINE2 = ENDLINE2;
            dbPara.P_ENDLINE3 = ENDLINE3;
            dbPara.P_ENDLINE4 = ENDLINE4;

            //เพิ่มใหม่ 27/06/16
            dbPara.P_2BEGINLINE1 = P_2BEGINLINE1;
            dbPara.P_2BEGINLINE2 = P_2BEGINLINE2;
            dbPara.P_2BEGINLINE3 = P_2BEGINLINE3;
            dbPara.P_2BEGINLINE4 = P_2BEGINLINE4;
            dbPara.P_2ENDLINE1 = P_2ENDLINE1;
            dbPara.P_2ENDLINE2 = P_2ENDLINE2;
            dbPara.P_2ENDLINE3 = P_2ENDLINE3;
            dbPara.P_2ENDLINE4 = P_2ENDLINE4;

            if (FINISHINGPROCESS != "Scouring")
            {
                dbPara.P_SELVAGELEFT = SELVAGELEFT;
                dbPara.P_SELVAGERIGHT = SELVAGERIGHT;
            }

            dbPara.P_SUSPENSTARTDATE = SUSPENSTARTDATE;

            //เพิ่มใหม่ 28/06/17
            dbPara.P_TENSION = P_TENSION;

            //เพิ่มใหม่ 25/08/17
            dbPara.P_LENGTHDETAIL = P_LENGTHDETAIL;

            //เพิ่มใหม่ 04/10/17
            dbPara.P_WIDTHAF_END = P_WIDTHAF_END;

            CUT_INSERTDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.CUT_INSERTDATA(dbPara);
                if (null != dbResult)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }

        #endregion

        #region เพิ่ม CUT_UPDATEDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ITEMLOT"></param>
        /// <param name="REMARK"></param>
        /// <param name="WIDTH1"></param>
        /// <param name="WIDTH2"></param>
        /// <param name="WIDTH3"></param>
        /// <param name="WIDTH4"></param>
        /// <param name="DISTANTBAR1"></param>
        /// <param name="DISTANTBAR2"></param>
        /// <param name="DISTANTBAR3"></param>
        /// <param name="DISTANTBAR4"></param>
        /// <param name="DISTANTLINE1"></param>
        /// <param name="DISTANTLINE2"></param>
        /// <param name="DISTANTLINE3"></param>
        /// <param name="DENWARP"></param>
        /// <param name="DENWEFT"></param>
        /// <param name="SPEED"></param>
        /// <param name="WIDTHBE"></param>
        /// <param name="WIDTHAF"></param>
        /// <param name="BEGINLINE1"></param>
        /// <param name="BEGINLINE2"></param>
        /// <param name="BEGINLINE3"></param>
        /// <param name="BEGINLINE4"></param>
        /// <param name="ENDLINE1"></param>
        /// <param name="ENDLINE2"></param>
        /// <param name="ENDLINE3"></param>
        /// <param name="ENDLINE4"></param>
        /// <param name="SELVAGELEFT"></param>
        /// <param name="SELVAGERIGHT"></param>
        /// <param name="FINISHINGPROCESS"></param>
        /// <param name="STATUS"></param>
        /// <param name="LENGTHPRINT"></param>
        /// <param name="CLEARBY"></param>
        /// <param name="CLEARREMARK"></param>
        /// <param name="CLEARDATE"></param>
        /// <param name="SUSPENDDATE"></param>
        /// <param name="P_2BEGINLINE1"></param>
        /// <param name="P_2BEGINLINE2"></param>
        /// <param name="P_2BEGINLINE3"></param>
        /// <param name="P_2BEGINLINE4"></param>
        /// <param name="P_2ENDLINE1"></param>
        /// <param name="P_2ENDLINE2"></param>
        /// <param name="P_2ENDLINE3"></param>
        /// <param name="P_2ENDLINE4"></param>
        /// <param name="P_TENSION"></param>
        /// <returns></returns>
        public bool CUT_UPDATEDATA(string ITEMLOT, 
        string REMARK, decimal? WIDTH1, decimal? WIDTH2, decimal? WIDTH3, decimal? WIDTH4,
        decimal? DISTANTBAR1, decimal? DISTANTBAR2, decimal? DISTANTBAR3, decimal? DISTANTBAR4,
        decimal? DISTANTLINE1, decimal? DISTANTLINE2, decimal? DISTANTLINE3, decimal? DENWARP, decimal? DENWEFT,
        decimal? SPEED, decimal? WIDTHBE, decimal? WIDTHAF, 
        string BEGINLINE1, string BEGINLINE2, string BEGINLINE3, string BEGINLINE4,
        string ENDLINE1, string ENDLINE2, string ENDLINE3, string ENDLINE4, 
        string SELVAGELEFT, string SELVAGERIGHT, string FINISHINGPROCESS,
        string STATUS, decimal? LENGTHPRINT, string CLEARBY, string CLEARREMARK, DateTime? CLEARDATE, DateTime? SUSPENDDATE,
        string P_2BEGINLINE1, string P_2BEGINLINE2, string P_2BEGINLINE3, string P_2BEGINLINE4,
        string P_2ENDLINE1, string P_2ENDLINE2, string P_2ENDLINE3, string P_2ENDLINE4, decimal? P_TENSION, string P_LENGTHDETAIL, decimal? P_WIDTHAF_END)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(ITEMLOT))
                return false;

            if (!HasConnection())
                return result;

            CUT_UPDATEDATAParameter dbPara = new CUT_UPDATEDATAParameter();

            dbPara.P_ITEMLOT = ITEMLOT;
            dbPara.P_REMARK = REMARK;
            dbPara.P_WIDTH1 = WIDTH1;
            dbPara.P_WIDTH2 = WIDTH2;
            dbPara.P_WIDTH3 = WIDTH3;
            dbPara.P_WIDTH4 = WIDTH4;
            dbPara.P_DISTANTBAR1 = DISTANTBAR1;
            dbPara.P_DISTANTBAR2 = DISTANTBAR2;
            dbPara.P_DISTANTBAR3 = DISTANTBAR3;
            dbPara.P_DISTANTBAR4 = DISTANTBAR4;
            dbPara.P_DISTANTLINE1 = DISTANTLINE1;
            dbPara.P_DISTANTLINE2 = DISTANTLINE2;
            dbPara.P_DISTANTLINE3 = DISTANTLINE3;
            dbPara.P_DENWARP = DENWARP;
            dbPara.P_DENWEFT = DENWEFT;
            dbPara.P_SPEED = SPEED;
            dbPara.P_WIDTHBE = WIDTHBE;
            dbPara.P_WIDTHAF = WIDTHAF;
            dbPara.P_BEGINLINE1 = BEGINLINE1;
            dbPara.P_BEGINLINE2 = BEGINLINE2;
            dbPara.P_BEGINLINE3 = BEGINLINE3;
            dbPara.P_BEGINLINE4 = BEGINLINE4;
            dbPara.P_ENDLINE1 = ENDLINE1;
            dbPara.P_ENDLINE2 = ENDLINE2;
            dbPara.P_ENDLINE3 = ENDLINE3;
            dbPara.P_ENDLINE4 = ENDLINE4;
            dbPara.P_SELVAGELEFT = SELVAGELEFT;
            dbPara.P_SELVAGERIGHT = SELVAGERIGHT;

            dbPara.P_STATUS = STATUS;

            dbPara.P_LENGTHPRINT = LENGTHPRINT;

            dbPara.P_CLEARBY = CLEARBY;
            dbPara.P_CLEARREMARK = CLEARREMARK;
            dbPara.P_CLEARDATE = CLEARDATE;
            dbPara.P_SUSPENDDATE = SUSPENDDATE;

            //เพิ่มใหม่ 27/06/16
            dbPara.P_2BEGINLINE1 = P_2BEGINLINE1;
            dbPara.P_2BEGINLINE2 = P_2BEGINLINE2;
            dbPara.P_2BEGINLINE3 = P_2BEGINLINE3;
            dbPara.P_2BEGINLINE4 = P_2BEGINLINE4;
            dbPara.P_2ENDLINE1 = P_2ENDLINE1;
            dbPara.P_2ENDLINE2 = P_2ENDLINE2;
            dbPara.P_2ENDLINE3 = P_2ENDLINE3;
            dbPara.P_2ENDLINE4 = P_2ENDLINE4;

            if (FINISHINGPROCESS != "Scouring")
            {
                dbPara.P_SELVAGELEFT = SELVAGELEFT;
                dbPara.P_SELVAGERIGHT = SELVAGERIGHT;
            }

            //เพิ่มใหม่ 28/06/17
            dbPara.P_TENSION = P_TENSION;

            //เพิ่มใหม่ 25/08/17
            dbPara.P_LENGTHDETAIL = P_LENGTHDETAIL;
            
            //เพิ่มใหม่ 04/10/17
            dbPara.P_WIDTHAF_END = P_WIDTHAF_END;

            CUT_UPDATEDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.CUT_UPDATEDATA(dbPara);
                if (null != dbResult)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }

        #endregion

        #region เพิ่ม CUT_UPDATEDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ITEMLOT"></param>
        /// <param name="ENDDATE"></param>
        /// <param name="REMARK"></param>
        /// <param name="WIDTH1"></param>
        /// <param name="WIDTH2"></param>
        /// <param name="WIDTH3"></param>
        /// <param name="WIDTH4"></param>
        /// <param name="DISTANTBAR1"></param>
        /// <param name="DISTANTBAR2"></param>
        /// <param name="DISTANTBAR3"></param>
        /// <param name="DISTANTBAR4"></param>
        /// <param name="DISTANTLINE1"></param>
        /// <param name="DISTANTLINE2"></param>
        /// <param name="DISTANTLINE3"></param>
        /// <param name="DENWARP"></param>
        /// <param name="DENWEFT"></param>
        /// <param name="SPEED"></param>
        /// <param name="WIDTHBE"></param>
        /// <param name="WIDTHAF"></param>
        /// <param name="BEGINLINE1"></param>
        /// <param name="BEGINLINE2"></param>
        /// <param name="BEGINLINE3"></param>
        /// <param name="BEGINLINE4"></param>
        /// <param name="ENDLINE1"></param>
        /// <param name="ENDLINE2"></param>
        /// <param name="ENDLINE3"></param>
        /// <param name="ENDLINE4"></param>
        /// <param name="SELVAGELEFT"></param>
        /// <param name="SELVAGERIGHT"></param>
        /// <param name="FINISHINGPROCESS"></param>
        /// <param name="STATUS"></param>
        /// <param name="LENGTHPRINT"></param>
        /// <param name="P_2BEGINLINE1"></param>
        /// <param name="P_2BEGINLINE2"></param>
        /// <param name="P_2BEGINLINE3"></param>
        /// <param name="P_2BEGINLINE4"></param>
        /// <param name="P_2ENDLINE1"></param>
        /// <param name="P_2ENDLINE2"></param>
        /// <param name="P_2ENDLINE3"></param>
        /// <param name="P_2ENDLINE4"></param>
        /// <param name="P_TENSION"></param>
        /// <returns></returns>
        public bool CUT_UPDATEDATA(string ITEMLOT, DateTime? ENDDATE,
        string REMARK, decimal? WIDTH1, decimal? WIDTH2, decimal? WIDTH3, decimal? WIDTH4,
        decimal? DISTANTBAR1, decimal? DISTANTBAR2, decimal? DISTANTBAR3, decimal? DISTANTBAR4,
        decimal? DISTANTLINE1, decimal? DISTANTLINE2, decimal? DISTANTLINE3, decimal? DENWARP, decimal? DENWEFT,
        decimal? SPEED, decimal? WIDTHBE, decimal? WIDTHAF, 
        string BEGINLINE1, string BEGINLINE2, string BEGINLINE3, string BEGINLINE4,
        string ENDLINE1, string ENDLINE2, string ENDLINE3, string ENDLINE4, 
        string SELVAGELEFT, string SELVAGERIGHT, string FINISHINGPROCESS, string STATUS, decimal? LENGTHPRINT,
        string P_2BEGINLINE1, string P_2BEGINLINE2, string P_2BEGINLINE3, string P_2BEGINLINE4,
        string P_2ENDLINE1, string P_2ENDLINE2, string P_2ENDLINE3, string P_2ENDLINE4, decimal? P_TENSION, string P_LENGTHDETAIL, decimal? P_WIDTHAF_END)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(ITEMLOT))
                return false;

            if (!HasConnection())
                return result;

            CUT_UPDATEDATAParameter dbPara = new CUT_UPDATEDATAParameter();

            dbPara.P_ITEMLOT = ITEMLOT;
            dbPara.P_ENDDATE = ENDDATE;
            dbPara.P_REMARK = REMARK;
            dbPara.P_WIDTH1 = WIDTH1;
            dbPara.P_WIDTH2 = WIDTH2;
            dbPara.P_WIDTH3 = WIDTH3;
            dbPara.P_WIDTH4 = WIDTH4;
            dbPara.P_DISTANTBAR1 = DISTANTBAR1;
            dbPara.P_DISTANTBAR2 = DISTANTBAR2;
            dbPara.P_DISTANTBAR3 = DISTANTBAR3;
            dbPara.P_DISTANTBAR4 = DISTANTBAR4;
            dbPara.P_DISTANTLINE1 = DISTANTLINE1;
            dbPara.P_DISTANTLINE2 = DISTANTLINE2;
            dbPara.P_DISTANTLINE3 = DISTANTLINE3;
            dbPara.P_DENWARP = DENWARP;
            dbPara.P_DENWEFT = DENWEFT;
            dbPara.P_SPEED = SPEED;
            dbPara.P_WIDTHBE = WIDTHBE;
            dbPara.P_WIDTHAF = WIDTHAF;
            dbPara.P_BEGINLINE1 = BEGINLINE1;
            dbPara.P_BEGINLINE2 = BEGINLINE2;
            dbPara.P_BEGINLINE3 = BEGINLINE3;
            dbPara.P_BEGINLINE4 = BEGINLINE4;
            dbPara.P_ENDLINE1 = ENDLINE1;
            dbPara.P_ENDLINE2 = ENDLINE2;
            dbPara.P_ENDLINE3 = ENDLINE3;
            dbPara.P_ENDLINE4 = ENDLINE4;
            dbPara.P_SELVAGELEFT = SELVAGELEFT;
            dbPara.P_SELVAGERIGHT = SELVAGERIGHT;
            dbPara.P_STATUS = STATUS;

            dbPara.P_LENGTHPRINT = LENGTHPRINT;

            //เพิ่มใหม่ 27/06/16
            dbPara.P_2BEGINLINE1 = P_2BEGINLINE1;
            dbPara.P_2BEGINLINE2 = P_2BEGINLINE2;
            dbPara.P_2BEGINLINE3 = P_2BEGINLINE3;
            dbPara.P_2BEGINLINE4 = P_2BEGINLINE4;
            dbPara.P_2ENDLINE1 = P_2ENDLINE1;
            dbPara.P_2ENDLINE2 = P_2ENDLINE2;
            dbPara.P_2ENDLINE3 = P_2ENDLINE3;
            dbPara.P_2ENDLINE4 = P_2ENDLINE4;

            if (FINISHINGPROCESS != "Scouring")
            {
                dbPara.P_SELVAGELEFT = SELVAGELEFT;
                dbPara.P_SELVAGERIGHT = SELVAGERIGHT;
            }

            //เพิ่มใหม่ 28/06/17
            dbPara.P_TENSION = P_TENSION;

            //เพิ่มใหม่ 25/08/17
            dbPara.P_LENGTHDETAIL = P_LENGTHDETAIL;

            //เพิ่มใหม่ 04/10/17
            dbPara.P_WIDTHAF_END = P_WIDTHAF_END;

            CUT_UPDATEDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.CUT_UPDATEDATA(dbPara);
                if (null != dbResult)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }

        #endregion

        #region เพิ่ม CUT_UPDATEDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ITEMLOT"></param>
        /// <param name="STATUS"></param>
        /// <param name="CLEARBY"></param>
        /// <param name="CLEARREMARK"></param>
        /// <param name="CLEARDATE"></param>
        /// <returns></returns>
        public bool CUT_UPDATEDATA(string ITEMLOT, string STATUS, string CLEARBY, string CLEARREMARK, DateTime? CLEARDATE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(ITEMLOT))
                return false;

            if (!HasConnection())
                return result;

            CUT_UPDATEDATAParameter dbPara = new CUT_UPDATEDATAParameter();

            dbPara.P_ITEMLOT = ITEMLOT;
            dbPara.P_STATUS = STATUS;
            dbPara.P_CLEARBY = CLEARBY;
            dbPara.P_CLEARREMARK = CLEARREMARK;
            dbPara.P_CLEARDATE = CLEARDATE;

            CUT_UPDATEDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.CUT_UPDATEDATA(dbPara);
                if (null != dbResult)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }

        #endregion

        #region GetAuthorizeByProcessID
        public bool GetAuthorizeByProcessID(string PROCESSID, string USER, string PASS)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(PROCESSID))
                return false;

            if (!HasConnection())
                return result;

            GETAUTHORIZEBYPROCESSIDParameter dbPara = new GETAUTHORIZEBYPROCESSIDParameter();
            dbPara.P_PROCESSID = PROCESSID;
            dbPara.P_USER = USER;
            dbPara.P_PASS = PASS;

            GETAUTHORIZEBYPROCESSIDResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.GETAUTHORIZEBYPROCESSID(dbPara);

                if (null != dbResult)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }
        #endregion

        #region Cut_GetMCSuspendData
        public List<CUT_GETMCSUSPENDDATA> Cut_GetMCSuspendData(string CUTMCNO)
        {
            List<CUT_GETMCSUSPENDDATA> results = null;

            if (string.IsNullOrWhiteSpace(CUTMCNO))
                return results;

            if (!HasConnection())
                return results;

            CUT_GETMCSUSPENDDATAParameter dbPara = new CUT_GETMCSUSPENDDATAParameter();
            dbPara.P_CUTMCNO = CUTMCNO;

            List<CUT_GETMCSUSPENDDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.CUT_GETMCSUSPENDDATA(dbPara);

                if (null != dbResults)
                {
                    results = new List<CUT_GETMCSUSPENDDATA>();
                    foreach (CUT_GETMCSUSPENDDATAResult dbResult in dbResults)
                    {
                        CUT_GETMCSUSPENDDATA inst = new CUT_GETMCSUSPENDDATA();

                        inst.ITEMLOT = dbResult.ITEMLOT;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.WIDTHBARCODE1 = dbResult.WIDTHBARCODE1;
                        inst.WIDTHBARCODE2 = dbResult.WIDTHBARCODE2;
                        inst.WIDTHBARCODE3 = dbResult.WIDTHBARCODE3;
                        inst.WIDTHBARCODE4 = dbResult.WIDTHBARCODE4;
                        inst.DISTANTBARCODE1 = dbResult.DISTANTBARCODE1;
                        inst.DISTANTBARCODE2 = dbResult.DISTANTBARCODE2;
                        inst.DISTANTBARCODE3 = dbResult.DISTANTBARCODE3;
                        inst.DISTANTBARCODE4 = dbResult.DISTANTBARCODE4;
                        inst.DISTANTLINE1 = dbResult.DISTANTLINE1;
                        inst.DISTANTLINE2 = dbResult.DISTANTLINE2;
                        inst.DISTANTLINE3 = dbResult.DISTANTLINE3;
                        inst.DENSITYWARP = dbResult.DENSITYWARP;
                        inst.DENSITYWEFT = dbResult.DENSITYWEFT;
                        inst.SPEED = dbResult.SPEED;
                        inst.BEFORE_WIDTH = dbResult.BEFORE_WIDTH;
                        inst.AFTER_WIDTH = dbResult.AFTER_WIDTH;
                        inst.BEGINROLL_LINE1 = dbResult.BEGINROLL_LINE1;
                        inst.BEGINROLL_LINE2 = dbResult.BEGINROLL_LINE2;
                        inst.BEGINROLL_LINE3 = dbResult.BEGINROLL_LINE3;
                        inst.BEGINROLL_LINE4 = dbResult.BEGINROLL_LINE4;
                        inst.ENDROLL_LINE1 = dbResult.ENDROLL_LINE1;
                        inst.ENDROLL_LINE2 = dbResult.ENDROLL_LINE2;
                        inst.ENDROLL_LINE3 = dbResult.ENDROLL_LINE3;
                        inst.ENDROLL_LINE4 = dbResult.ENDROLL_LINE4;
                        inst.OPERATORID = dbResult.OPERATORID;
                        inst.SELVAGE_LEFT = dbResult.SELVAGE_LEFT;
                        inst.SELVAGE_RIGHT = dbResult.SELVAGE_RIGHT;
                        inst.REMARK = dbResult.REMARK;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.MCNO = dbResult.MCNO;

                        inst.STATUS = dbResult.STATUS;
                        inst.SUSPENDDATE = dbResult.SUSPENDDATE;
                        inst.SUSPENDBY = dbResult.SUSPENDBY;
                        inst.CLEARDATE = dbResult.CLEARDATE;
                        inst.CLEARREMARK = dbResult.CLEARREMARK;
                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.LENGTHPRINT = dbResult.LENGTHPRINT;
                        inst.SUSPENDSTARTDATE = dbResult.SUSPENDSTARTDATE;

                        //เพิ่มใหม่ 27/06/16
                        inst.BEGINROLL2_LINE1 = dbResult.BEGINROLL2_LINE1;
                        inst.BEGINROLL2_LINE2 = dbResult.BEGINROLL2_LINE2;
                        inst.BEGINROLL2_LINE3 = dbResult.BEGINROLL2_LINE3;
                        inst.BEGINROLL2_LINE4 = dbResult.BEGINROLL2_LINE4;
                        inst.ENDROLL2_LINE1 = dbResult.ENDROLL2_LINE1;
                        inst.ENDROLL2_LINE2 = dbResult.ENDROLL2_LINE2;
                        inst.ENDROLL2_LINE3 = dbResult.ENDROLL2_LINE3;
                        inst.ENDROLL2_LINE4 = dbResult.ENDROLL2_LINE4;

                        //เพิ่มใหม่ 28/06/17
                        inst.TENSION = dbResult.TENSION;

                        //เพิ่มใหม่ 25/08/17
                        inst.LENGTHDETAIL = dbResult.LENGTHDETAIL;

                        //เพิ่มใหม่ 04/10/17
                        inst.AFTER_WIDTH_END = dbResult.AFTER_WIDTH_END;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                results = null;
            }

            return results;
        }
        #endregion

        #region Report

        // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ CUT_GETSLIP Report 
        public List<CUT_GETSLIPReport> CUT_GETSLIPReportData(string ITEMLOT)
        {
            List<CUT_GETSLIPReport> results = null;

            if (string.IsNullOrWhiteSpace(ITEMLOT))
                return results;

            if (!HasConnection())
                return results;

            CUT_GETSLIPParameter dbPara = new CUT_GETSLIPParameter();
            List<CUT_GETSLIPResult> dbResults = null;
            dbPara.P_ITEMLOT = ITEMLOT;
            try
            {
                dbResults = DatabaseManager.Instance.CUT_GETSLIP(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results = new List<CUT_GETSLIPReport>();
                    foreach (CUT_GETSLIPResult dbResult in dbResults)
                    {

                        CUT_GETSLIPReport inst = new CUT_GETSLIPReport();

                        inst.ITEMLOT = dbResult.ITEMLOT;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.WIDTHBARCODE1 = dbResult.WIDTHBARCODE1;
                        inst.WIDTHBARCODE2 = dbResult.WIDTHBARCODE2;
                        inst.WIDTHBARCODE3 = dbResult.WIDTHBARCODE3;
                        inst.WIDTHBARCODE4 = dbResult.WIDTHBARCODE4;
                        inst.DISTANTBARCODE1 = dbResult.DISTANTBARCODE1;
                        inst.DISTANTBARCODE2 = dbResult.DISTANTBARCODE2;
                        inst.DISTANTBARCODE3 = dbResult.DISTANTBARCODE3;
                        inst.DISTANTBARCODE4 = dbResult.DISTANTBARCODE4;
                        inst.DISTANTLINE1 = dbResult.DISTANTLINE1;
                        inst.DISTANTLINE2 = dbResult.DISTANTLINE2;
                        inst.DISTANTLINE3 = dbResult.DISTANTLINE3;
                        inst.DENSITYWARP = dbResult.DENSITYWARP;
                        inst.DENSITYWEFT = dbResult.DENSITYWEFT;
                        inst.SPEED = dbResult.SPEED;
                        inst.BEFORE_WIDTH = dbResult.BEFORE_WIDTH;
                        inst.AFTER_WIDTH = dbResult.AFTER_WIDTH;
                        inst.BEGINROLL_LINE1 = dbResult.BEGINROLL_LINE1;
                        inst.BEGINROLL_LINE2 = dbResult.BEGINROLL_LINE2;
                        inst.BEGINROLL_LINE3 = dbResult.BEGINROLL_LINE3;
                        inst.BEGINROLL_LINE4 = dbResult.BEGINROLL_LINE4;
                        inst.ENDROLL_LINE1 = dbResult.ENDROLL_LINE1;
                        inst.ENDROLL_LINE2 = dbResult.ENDROLL_LINE2;
                        inst.ENDROLL_LINE3 = dbResult.ENDROLL_LINE3;
                        inst.ENDROLL_LINE4 = dbResult.ENDROLL_LINE4;
                        inst.OPERATORID = dbResult.OPERATORID;
                        inst.SELVAGE_LEFT = dbResult.SELVAGE_LEFT;
                        inst.SELVAGE_RIGHT = dbResult.SELVAGE_RIGHT;
                        inst.REMARK = dbResult.REMARK;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.MCNO = dbResult.MCNO;
                        inst.FINISHLOT = dbResult.FINISHLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.ITEMLOT1 = dbResult.ITEMLOT1;
                        inst.PARTNO = dbResult.PARTNO;
                        inst.MCNAME = dbResult.MCNAME;

                        //เพิ่มใหม่ 20/01/16
                        inst.FINISHINGPROCESS = dbResult.FINISHINGPROCESS;

                        //เพิ่มใหม่ 03/05/16
                        inst.STATUS = dbResult.STATUS;
                        inst.SUSPENDDATE = dbResult.SUSPENDDATE;
                        inst.SUSPENDBY = dbResult.SUSPENDBY;
                        inst.CLEARDATE = dbResult.CLEARDATE;
                        inst.CLEARREMARK = dbResult.CLEARREMARK;
                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.LENGTHPRINT = dbResult.LENGTHPRINT;
                        inst.SUSPENDSTARTDATE = dbResult.SUSPENDSTARTDATE;

                        //เพิ่มใหม่ 06/05/16
                        inst.SHOWSELVAGE = dbResult.SHOWSELVAGE;

                        //เพิ่มใหม่ 27/06/16
                        inst.BEGINROLL2_LINE1 = dbResult.BEGINROLL2_LINE1;
                        inst.BEGINROLL2_LINE2 = dbResult.BEGINROLL2_LINE2;
                        inst.BEGINROLL2_LINE3 = dbResult.BEGINROLL2_LINE3;
                        inst.BEGINROLL2_LINE4 = dbResult.BEGINROLL2_LINE4;
                        inst.ENDROLL2_LINE1 = dbResult.ENDROLL2_LINE1;
                        inst.ENDROLL2_LINE2 = dbResult.ENDROLL2_LINE2;
                        inst.ENDROLL2_LINE3 = dbResult.ENDROLL2_LINE3;
                        inst.ENDROLL2_LINE4 = dbResult.ENDROLL2_LINE4;
                        inst.SND_BARCODE = dbResult.SND_BARCODE;

                        //เพิ่มใหม่ 10/12/16
                        inst.CUSTOMERID = dbResult.CUSTOMERID;

                        //เพิ่ม 28/06/17
                        inst.TENSION = dbResult.TENSION;

                        //เพิ่มใหม่ 13/07/17
                        if (!string.IsNullOrEmpty(dbResult.FINISHLENGTH))
                            inst.FINISHLENGTH = dbResult.FINISHLENGTH;

                        //เพิ่มใหม่ 28/09/17
                        if (!string.IsNullOrEmpty(dbResult.LENGTHDETAIL))
                            inst.LENGTHDETAIL = dbResult.LENGTHDETAIL;

                        //เพิ่มใหม่ 04/10/17
                        if (dbResult.AFTER_WIDTH_END != null)
                            inst.AFTER_WIDTH_END = dbResult.AFTER_WIDTH_END;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #endregion
    }

    #endregion
}



