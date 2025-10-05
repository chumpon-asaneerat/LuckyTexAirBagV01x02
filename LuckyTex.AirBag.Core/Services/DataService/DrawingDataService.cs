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

    #region Warping Data Service

    /// <summary>
    /// The data service for User and Beaming.
    /// </summary>
    public class DrawingDataService : BaseDataService
    {
        #region Singelton

        private static DrawingDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static DrawingDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(DrawingDataService))
                    {
                        _instance = new DrawingDataService();
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
        private DrawingDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~DrawingDataService()
        {
        }

        #endregion

        #region Public Methods

        #region เพิ่มใหม่ ITM_GETITEMPREPARELIST ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ITM_GETITEMPREPARELIST> ITM_GETITEMPREPARELIST()
        {
            List<ITM_GETITEMPREPARELIST> results = null;

            if (!HasConnection())
                return results;

            ITM_GETITEMPREPARELISTParameter dbPara = new ITM_GETITEMPREPARELISTParameter();

            List<ITM_GETITEMPREPARELISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.ITM_GETITEMPREPARELIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<ITM_GETITEMPREPARELIST>();
                    foreach (ITM_GETITEMPREPARELISTResult dbResult in dbResults)
                    {
                        ITM_GETITEMPREPARELIST inst = new ITM_GETITEMPREPARELIST();

                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.ITM_YARN = dbResult.ITM_YARN;

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

        #region เพิ่มใหม่ DRAW_GETSPECBYCHOPNO ใช้ในการ Load Spec

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DRAW_GETSPECBYCHOPNO> ITM_GETITEMPREPARELIST(string P_ITMPREPARE)
        {
            List<DRAW_GETSPECBYCHOPNO> results = null;

            if (!HasConnection())
                return results;

            DRAW_GETSPECBYCHOPNOParameter dbPara = new DRAW_GETSPECBYCHOPNOParameter();
            dbPara.P_ITMPREPARE = P_ITMPREPARE;

            List<DRAW_GETSPECBYCHOPNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.DRAW_GETSPECBYCHOPNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<DRAW_GETSPECBYCHOPNO>();
                    foreach (DRAW_GETSPECBYCHOPNOResult dbResult in dbResults)
                    {
                        DRAW_GETSPECBYCHOPNO inst = new DRAW_GETSPECBYCHOPNO();

                        inst.CHOPNO = dbResult.CHOPNO;
                        inst.NOYARN = dbResult.NOYARN;
                        inst.REEDTYPE = dbResult.REEDTYPE;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.NODENT = dbResult.NODENT;
                        inst.PITCH = dbResult.PITCH;
                        inst.AIRSPACE = dbResult.AIRSPACE;

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

        #region เพิ่มใหม่ CheckBeamLot_ITM_Prepare ใช้ในการ Load BEAM

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DRAW_GETBEAMLOTDETAIL CheckBeamLot_ITM_Prepare(string BEAMLOT, string ITM_PREPARE)
        {
            DRAW_GETBEAMLOTDETAIL results = null;

            if (!HasConnection())
                return results;


            DRAW_GETBEAMLOTDETAILParameter dbPara = new DRAW_GETBEAMLOTDETAILParameter();
            dbPara.P_BEAMLOT = BEAMLOT;

            List<DRAW_GETBEAMLOTDETAILResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.DRAW_GETBEAMLOTDETAIL(dbPara);
                if (null != dbResults)
                {
                    if (dbResults.Count > 0)
                    {
                        results = new DRAW_GETBEAMLOTDETAIL();

                        foreach (DRAW_GETBEAMLOTDETAILResult dbResult in dbResults)
                        {
                            DRAW_GETBEAMLOTDETAIL inst = new DRAW_GETBEAMLOTDETAIL();

                            if (BEAMLOT == dbResult.BEAMLOT)
                            {
                                if (ITM_PREPARE == dbResult.ITM_PREPARE)
                                {
                                    results.BEAMNO = dbResult.BEAMNO;
                                    results.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                                    results.TOTALYARN = dbResult.TOTALYARN;

                                    results.StrMsg = string.Empty;

                                    break;
                                }
                                else
                                {
                                    results.StrMsg = "Beam Lot is not map for this Item Prepare, Please Try Again";
                                }
                            }
                            else
                            {
                                results.StrMsg = "Beam Lot have no data";
                            }
                        }
                    }
                    else
                    {
                        results.StrMsg = "Beam Lot have no data";
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

        #region เพิ่มใหม่ DRAW_GETDRAWINGLISTBYITEM ใช้ในการ Load Draw
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<DRAW_GETDRAWINGLISTBYITEM> DRAW_GETDRAWINGLISTBYITEM(string P_ITMPREPARE)
        {
            List<DRAW_GETDRAWINGLISTBYITEM> results = null;

            if (!HasConnection())
                return results;

            DRAW_GETDRAWINGLISTBYITEMParameter dbPara = new DRAW_GETDRAWINGLISTBYITEMParameter();
            dbPara.P_ITMPREPARE = P_ITMPREPARE;

            List<DRAW_GETDRAWINGLISTBYITEMResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.DRAW_GETDRAWINGLISTBYITEM(dbPara);
                if (null != dbResults)
                {
                    results = new List<DRAW_GETDRAWINGLISTBYITEM>();
                    foreach (DRAW_GETDRAWINGLISTBYITEMResult dbResult in dbResults)
                    {
                        DRAW_GETDRAWINGLISTBYITEM inst = new DRAW_GETDRAWINGLISTBYITEM();

                        inst.BEAMNO = dbResult.BEAMNO;

                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.DRAWINGTYPE = dbResult.DRAWINGTYPE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDATE = dbResult.ENDATE;
                        inst.REEDNO = dbResult.REEDNO;
                        inst.HEALDCOLOR = dbResult.HEALDCOLOR;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.USEFLAG = dbResult.USEFLAG;
                        inst.HEALDNO = dbResult.HEALDNO;
                        inst.REEDTYPE = dbResult.REEDTYPE;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

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

        // เพิ่มใหม่ 21/01/17
        #region เพิ่มใหม่ DRAW_DAILYREPORT ใช้ในการ Load Draw
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<DRAW_DAILYREPORT> DRAW_DAILYREPORT(string P_DATE)
        {
            List<DRAW_DAILYREPORT> results = null;

            if (!HasConnection())
                return results;

            DRAW_DAILYREPORTParameter dbPara = new DRAW_DAILYREPORTParameter();
            dbPara.P_DATE = P_DATE;

            List<DRAW_DAILYREPORTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.DRAW_DAILYREPORT(dbPara);
                if (null != dbResults)
                {
                    results = new List<DRAW_DAILYREPORT>();

                    int i = 0;
                    foreach (DRAW_DAILYREPORTResult dbResult in dbResults)
                    {
                        DRAW_DAILYREPORT inst = new DRAW_DAILYREPORT();

                        inst.No = (i + 1);
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.DRAWINGTYPE = dbResult.DRAWINGTYPE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDATE = dbResult.ENDATE;
                        inst.REEDNO = dbResult.REEDNO;
                        inst.HEALDCOLOR = dbResult.HEALDCOLOR;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.USEFLAG = dbResult.USEFLAG;
                        inst.HEALDNO = dbResult.HEALDNO;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;
                        inst.TOTALYARN = dbResult.TOTALYARN;
                        inst.BEAMNO = dbResult.BEAMNO;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.BEAMERNO = dbResult.BEAMERNO;

                        results.Add(inst);
                        i++;
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

        // เพิ่มใหม่ 21/01/17
        #region เพิ่มใหม่ DRAW_TRANSFERSLIP ใช้ในการ Load Draw
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<DRAW_TRANSFERSLIP> DRAW_TRANSFERSLIP(string P_BEAMERROLL)
        {
            List<DRAW_TRANSFERSLIP> results = null;

            if (!HasConnection())
                return results;

            DRAW_TRANSFERSLIPParameter dbPara = new DRAW_TRANSFERSLIPParameter();
            dbPara.P_BEAMERROLL = P_BEAMERROLL;

            List<DRAW_TRANSFERSLIPResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.DRAW_TRANSFERSLIP(dbPara);
                if (null != dbResults)
                {
                    results = new List<DRAW_TRANSFERSLIP>();

                    foreach (DRAW_TRANSFERSLIPResult dbResult in dbResults)
                    {
                        DRAW_TRANSFERSLIP inst = new DRAW_TRANSFERSLIP();

                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.DRAWINGTYPE = dbResult.DRAWINGTYPE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDATE = dbResult.ENDATE;
                        inst.REEDNO = dbResult.REEDNO;
                        inst.HEALDCOLOR = dbResult.HEALDCOLOR;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.USEFLAG = dbResult.USEFLAG;
                        inst.HEALDNO = dbResult.HEALDNO;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;
                        inst.TOTALYARN = dbResult.TOTALYARN;
                        inst.BEAMNO = dbResult.BEAMNO;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.BEAMERNO = dbResult.BEAMERNO;

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

        #region DRAW_INSERTDRAWING

        public string DRAW_INSERTDRAWING(string P_BEAMLOT, string P_ITMPREPARE, string P_PRODUCTID, string P_DRAWINGTYPE
            , string P_REEDNO, string P_HEALDCOLOR, decimal? P_HEALDNO, string P_OPERATOR, string P_GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return result;

            if (!HasConnection())
                return result;

            DRAW_INSERTDRAWINGParameter dbPara = new DRAW_INSERTDRAWINGParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_PRODUCTID = P_PRODUCTID;
            dbPara.P_DRAWINGTYPE = P_DRAWINGTYPE;
            dbPara.P_REEDNO = P_REEDNO;
            dbPara.P_HEALDCOLOR = P_HEALDCOLOR;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_HEALDNO = P_HEALDNO;
            dbPara.P_GROUP = P_GROUP;

            DRAW_INSERTDRAWINGResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.DRAW_INSERTDRAWING(dbPara);

                result = dbResult.R_RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region DRAW_UPDATEDRAWING

        public string DRAW_UPDATEDRAWING(string P_BEAMLOT, string P_DRAWINGTYPE
            , string P_REEDNO, string P_HEALDCOLOR, decimal? P_HEALDNO, string P_OPERATOR, string P_FLAG, string P_GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return result;

            if (!HasConnection())
                return result;

            DRAW_UPDATEDRAWINGParameter dbPara = new DRAW_UPDATEDRAWINGParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;

            dbPara.P_DRAWINGTYPE = P_DRAWINGTYPE;
            dbPara.P_REEDNO = P_REEDNO;
            dbPara.P_HEALDCOLOR = P_HEALDCOLOR;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_HEALDNO = P_HEALDNO;
            dbPara.P_FLAG = P_FLAG;
            dbPara.P_GROUP = P_GROUP;

            DRAW_UPDATEDRAWINGResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.DRAW_UPDATEDRAWING(dbPara);

                result = dbResult.R_RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region BEAM_UPDATEBEAMDETAIL

        public bool BEAM_UPDATEBEAMDETAIL(string P_BEAMLOT, string P_FLAG)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return result;

            if (!HasConnection())
                return result;

            BEAM_UPDATEBEAMDETAILParameter dbPara = new BEAM_UPDATEBEAMDETAILParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_FLAG = P_FLAG;

            BEAM_UPDATEBEAMDETAILResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_UPDATEBEAMDETAIL(dbPara);

                result = (null != dbResult);
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }

        #endregion

        #endregion
    }

    #endregion
}








