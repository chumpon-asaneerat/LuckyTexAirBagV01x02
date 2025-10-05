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

    #region Weaving Data Service

    /// <summary>
    /// The data service for User and Employee.
    /// </summary>
    public class WeavingDataService : BaseDataService
    {
        #region Singelton

        private static WeavingDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static WeavingDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(WeavingDataService))
                    {
                        _instance = new WeavingDataService();
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
        private WeavingDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~WeavingDataService()
        {
        }

        #endregion

        #region Public Methods

        #region เพิ่มใหม่ WEAV_GETITEMWEAVINGLIST ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_GETITEMWEAVINGLIST> WEAV_GETITEMWEAVINGLIST()
        {
            List<WEAV_GETITEMWEAVINGLIST> results = null;

            if (!HasConnection())
                return results;

            WEAV_GETITEMWEAVINGLISTParameter dbPara = new WEAV_GETITEMWEAVINGLISTParameter();

            List<WEAV_GETITEMWEAVINGLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETITEMWEAVINGLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETITEMWEAVINGLIST>();
                    foreach (WEAV_GETITEMWEAVINGLISTResult dbResult in dbResults)
                    {
                        WEAV_GETITEMWEAVINGLIST inst = new WEAV_GETITEMWEAVINGLIST();

                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;

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

        #region เพิ่มใหม่ WEAV_GETITEMWEAVINGLIST ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_GETITEMWEAVINGLIST> WEAV_GETITEMWEAVINGLIST(string P_WEAVETYPE)
        {
            List<WEAV_GETITEMWEAVINGLIST> results = null;

            if (!HasConnection())
                return results;

            WEAV_GETITEMWEAVINGLISTParameter dbPara = new WEAV_GETITEMWEAVINGLISTParameter();
            dbPara.P_WEAVETYPE = P_WEAVETYPE;

            List<WEAV_GETITEMWEAVINGLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETITEMWEAVINGLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETITEMWEAVINGLIST>();
                    foreach (WEAV_GETITEMWEAVINGLISTResult dbResult in dbResults)
                    {
                        WEAV_GETITEMWEAVINGLIST inst = new WEAV_GETITEMWEAVINGLIST();

                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;

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

        #region เพิ่มใหม่ WEAV_GETITEMWEAVINGLISTWeavingProcess ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_GETITEMWEAVINGLIST> WEAV_GETITEMWEAVINGLIST(string ITM_WEAVING ,string type)
        {
            List<WEAV_GETITEMWEAVINGLIST> results = null;

            if (!HasConnection())
                return results;

            WEAV_GETITEMWEAVINGLISTParameter dbPara = new WEAV_GETITEMWEAVINGLISTParameter();

            List<WEAV_GETITEMWEAVINGLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETITEMWEAVINGLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETITEMWEAVINGLIST>();
                    foreach (WEAV_GETITEMWEAVINGLISTResult dbResult in dbResults)
                    {
                        
                        if (type == dbResult.WEAVE_TYPE)
                        {
                            if (ITM_WEAVING == dbResult.ITM_WEAVING)
                            {
                                WEAV_GETITEMWEAVINGLIST inst = new WEAV_GETITEMWEAVINGLIST();

                                inst.ITM_YARN = dbResult.ITM_YARN;

                                results.Add(inst);
                                break;
                            }
                        }
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

        #region เพิ่มใหม่ WEAV_GETALLITEMWEAVING ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_GETALLITEMWEAVING> WEAV_GETALLITEMWEAVING()
        {
            List<WEAV_GETALLITEMWEAVING> results = null;

            if (!HasConnection())
                return results;

            WEAV_GETALLITEMWEAVINGParameter dbPara = new WEAV_GETALLITEMWEAVINGParameter();

            List<WEAV_GETALLITEMWEAVINGResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETALLITEMWEAVING(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETALLITEMWEAVING>();
                    foreach (WEAV_GETALLITEMWEAVINGResult dbResult in dbResults)
                    {
                        WEAV_GETALLITEMWEAVING inst = new WEAV_GETALLITEMWEAVING();

                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.WIDTHWEAVING = dbResult.WIDTHWEAVING;

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

        #region เพิ่มใหม่ WEAV_DEFECTLIST ใช้ในการ Load DEFECTLIST

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_DEFECTLIST> WEAV_DEFECTLIST()
        {
            List<WEAV_DEFECTLIST> results = null;

            if (!HasConnection())
                return results;

            WEAV_DEFECTLISTParameter dbPara = new WEAV_DEFECTLISTParameter();

            List<WEAV_DEFECTLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_DEFECTLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_DEFECTLIST>();
                    foreach (WEAV_DEFECTLISTResult dbResult in dbResults)
                    {
                        WEAV_DEFECTLIST inst = new WEAV_DEFECTLIST();

                        inst.DEFECTCODE = dbResult.DEFECTCODE;
                        inst.DEFECTTYPE = dbResult.DEFECTTYPE;
                        inst.DESCRIPTION = dbResult.DESCRIPTION;
                        inst.YARN = dbResult.YARN;

                        inst.DefectCodeName = dbResult.DEFECTCODE + " " + dbResult.DESCRIPTION;

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

        #region เพิ่มใหม่ GetItemWidth ใช้ในการ Load WIDTHWEAVING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal? GetItemWidth(string ITM_WEAVING)
        {
            decimal? results = null;

            if (!HasConnection())
                return results;

            WEAV_GETITEMWEAVINGLISTParameter dbPara = new WEAV_GETITEMWEAVINGLISTParameter();
            
            List<WEAV_GETITEMWEAVINGLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETITEMWEAVINGLIST(dbPara);
                if (null != dbResults)
                {
                    foreach (WEAV_GETITEMWEAVINGLISTResult dbResult in dbResults)
                    {
                        WEAV_GETITEMWEAVINGLIST inst = new WEAV_GETITEMWEAVINGLIST();

                        if (ITM_WEAVING == dbResult.ITM_WEAVING)
                        {
                            results = dbResult.WIDTHWEAVING;
                            break;
                        }
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

        #region เพิ่มใหม่ WEAV_GETCNTCHINALOT ใช้ในการ Load WIDTHWEAVING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string WEAV_GETCNTCHINALOT(string P_LOT)
        {
            string results = string.Empty;

            if (!HasConnection())
                return results;

            WEAV_GETCNTCHINALOTParameter dbPara = new WEAV_GETCNTCHINALOTParameter();
            dbPara.P_LOT = P_LOT;

            WEAV_GETCNTCHINALOTResult dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETCNTCHINALOT(dbPara);
                if (null != dbResults)
                {
                    results = dbResults.CNT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ GetWeavingingDataList ใช้ในการ Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<GETWEAVINGINGDATA> GetWeavingingDataList(string WEAVINGLOT)
        {
            List<GETWEAVINGINGDATA> results = null;

            if (!HasConnection())
                return results;

            GETWEAVINGINGDATAParameter dbPara = new GETWEAVINGINGDATAParameter();
            dbPara.P_WEAVINGLOT = WEAVINGLOT;


            List<GETWEAVINGINGDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.GETWEAVINGINGDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<GETWEAVINGINGDATA>();
                    foreach (GETWEAVINGINGDATAResult dbResult in dbResults)
                    {
                        GETWEAVINGINGDATA inst = new GETWEAVINGINGDATA();

                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.WEAVINGDATE = dbResult.WEAVINGDATE;
                        inst.SHIFT = dbResult.SHIFT;
                        inst.REMARK = dbResult.REMARK;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.PREPAREBY = dbResult.PREPAREBY;
                        inst.WEAVINGNO = dbResult.WEAVINGNO;

                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.DOFFNO = dbResult.DOFFNO;
                        inst.DENSITY_WARP = dbResult.DENSITY_WARP;
                        inst.TENSION = dbResult.TENSION;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.SPEED = dbResult.SPEED;
                        inst.WASTE = dbResult.WASTE;
                        inst.DENSITY_WEFT = dbResult.DENSITY_WEFT;
                        inst.DELETEFLAG = dbResult.DELETEFLAG;
                        inst.DELETEBY = dbResult.DELETEBY;
                        inst.DELETEDATE = dbResult.DELETEDATE;


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

        #region เพิ่มใหม่ WEAV_GREYROLLDAILYREPORT ใช้ในการ Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_GREYROLLDAILYREPORT> WEAV_GREYROLLDAILYREPORT(string P_DATE, string P_CHINA)
        {
            List<WEAV_GREYROLLDAILYREPORT> results = null;

            if (!HasConnection())
                return results;

            WEAV_GREYROLLDAILYREPORTParameter dbPara = new WEAV_GREYROLLDAILYREPORTParameter();
            dbPara.P_DATE = P_DATE;
            dbPara.P_CHINA = P_CHINA;

            List<WEAV_GREYROLLDAILYREPORTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GREYROLLDAILYREPORT(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GREYROLLDAILYREPORT>();
                    foreach (WEAV_GREYROLLDAILYREPORTResult dbResult in dbResults)
                    {
                        WEAV_GREYROLLDAILYREPORT inst = new WEAV_GREYROLLDAILYREPORT();

                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.WEAVINGDATE = dbResult.WEAVINGDATE;
                        inst.SHIFT = dbResult.SHIFT;
                        inst.REMARK = dbResult.REMARK;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.PREPAREBY = dbResult.PREPAREBY;
                        inst.WEAVINGNO = dbResult.WEAVINGNO;

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

        #region เพิ่มใหม่ WEAV_GETMACHINEZONELIST ใช้ในการ Load Zone Total

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_GETMACHINEZONELIST> WEAV_GETMACHINEZONELIST(string zone)
        {
            List<WEAV_GETMACHINEZONELIST> results = null;

            if (!HasConnection())
                return results;

            WEAV_GETMACHINEZONELISTParameter dbPara = new WEAV_GETMACHINEZONELISTParameter();

            List<WEAV_GETMACHINEZONELISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETMACHINEZONELIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETMACHINEZONELIST>();
                    foreach (WEAV_GETMACHINEZONELISTResult dbResult in dbResults)
                    {
                        if (dbResult.ZONE == zone)
                        {
                            WEAV_GETMACHINEZONELIST inst = new WEAV_GETMACHINEZONELIST();

                            inst.TYPE = dbResult.TYPE;
                            inst.TOTAL = dbResult.TOTAL;

                            results.Add(inst);
                            break;
                        }
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

        #region เพิ่มใหม่ WEAVE_CHECKITEMPREPARE ใช้ในการ Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAVE_CHECKITEMPREPARE> WEAVE_CHECKITEMPREPARE(string P_ITMWEAVING, string P_ITMPREPARE)
        {
            List<WEAVE_CHECKITEMPREPARE> results = null;

            if (!HasConnection())
                return results;

            WEAVE_CHECKITEMPREPAREParameter dbPara = new WEAVE_CHECKITEMPREPAREParameter();
            dbPara.P_ITMWEAVING = P_ITMWEAVING;
            dbPara.P_ITMPREPARE = P_ITMPREPARE;

            List<WEAVE_CHECKITEMPREPAREResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAVE_CHECKITEMPREPARE(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAVE_CHECKITEMPREPARE>();
                    foreach (WEAVE_CHECKITEMPREPAREResult dbResult in dbResults)
                    {
                        WEAVE_CHECKITEMPREPARE inst = new WEAVE_CHECKITEMPREPARE();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.ITM_YARN = dbResult.ITM_YARN;
                        inst.ITM_WIDTH = dbResult.ITM_WIDTH;
                        inst.ITM_PROC1 = dbResult.ITM_PROC1;
                        inst.ITM_PROC2 = dbResult.ITM_PROC2;
                        inst.ITM_PROC3 = dbResult.ITM_PROC3;
                        inst.ITM_PROC4 = dbResult.ITM_PROC4;
                        inst.ITM_PROC5 = dbResult.ITM_PROC5;
                        inst.ITM_PROC6 = dbResult.ITM_PROC6;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.COREWEIGHT = dbResult.COREWEIGHT;
                        inst.FULLWEIGHT = dbResult.FULLWEIGHT;
                        inst.ITM_GROUP = dbResult.ITM_GROUP;
                        inst.YARNCODE = dbResult.YARNCODE;
                        inst.WIDTHCODE = dbResult.WIDTHCODE;
                        inst.WIDTHWEAVING = dbResult.WIDTHWEAVING;
                        inst.LABFORM = dbResult.LABFORM;
                        inst.WEAVE_TYPE = dbResult.WEAVE_TYPE;

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

        #region เพิ่มใหม่ WEAVE_GETBEAMLOTDETAIL ใช้ในการ Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAVE_GETBEAMLOTDETAIL> WEAVE_GETBEAMLOTDETAIL(string P_BEAMLOT)
        {
            List<WEAVE_GETBEAMLOTDETAIL> results = null;

            if (!HasConnection())
                return results;

            WEAVE_GETBEAMLOTDETAILParameter dbPara = new WEAVE_GETBEAMLOTDETAILParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;

            List<WEAVE_GETBEAMLOTDETAILResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAVE_GETBEAMLOTDETAIL(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAVE_GETBEAMLOTDETAIL>();
                    foreach (WEAVE_GETBEAMLOTDETAILResult dbResult in dbResults)
                    {
                        WEAVE_GETBEAMLOTDETAIL inst = new WEAVE_GETBEAMLOTDETAIL();

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
                        inst.LENGTH = dbResult.LENGTH;

                        //เพิ่ม 01/09/17
                        inst.RESULT = dbResult.RESULT;


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

        #region เพิ่มใหม่ WEAV_WEAVINGINPROCESSLIST ใช้ในการ Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_WEAVINGINPROCESSLIST> WEAV_WEAVINGINPROCESSLIST(string P_BEAMROLL, decimal? P_DOFFNO)
        {
            List<WEAV_WEAVINGINPROCESSLIST> results = null;

            if (!HasConnection())
                return results;

            WEAV_WEAVINGINPROCESSLISTParameter dbPara = new WEAV_WEAVINGINPROCESSLISTParameter();
            dbPara.P_BEAMROLL = P_BEAMROLL;
            dbPara.P_DOFFNO = P_DOFFNO;

            List<WEAV_WEAVINGINPROCESSLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_WEAVINGINPROCESSLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_WEAVINGINPROCESSLIST>();
                    foreach (WEAV_WEAVINGINPROCESSLISTResult dbResult in dbResults)
                    {
                        WEAV_WEAVINGINPROCESSLIST inst = new WEAV_WEAVINGINPROCESSLIST();

                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.MC = dbResult.MC;
                        inst.REEDNO2 = dbResult.REEDNO2;
                        inst.WEFTYARN = dbResult.WEFTYARN;
                        inst.TEMPLETYPE = dbResult.TEMPLETYPE;
                        inst.BARNO = dbResult.BARNO;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.FINISHDATE = dbResult.FINISHDATE;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.SETTINGBY = dbResult.SETTINGBY;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.WIDTH = dbResult.WIDTH;

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

        #region เพิ่มใหม่ WEAV_WEAVINGMCSTATUS ใช้ในการ Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WEAV_WEAVINGMCSTATUS WEAV_WEAVINGMCSTATUS(string P_MC)
        {
            WEAV_WEAVINGMCSTATUS results = new WEAV_WEAVINGMCSTATUS();

            if (!HasConnection())
                return results;

            WEAV_WEAVINGMCSTATUSParameter dbPara = new WEAV_WEAVINGMCSTATUSParameter();
            dbPara.P_MC = P_MC;

            List<WEAV_WEAVINGMCSTATUSResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_WEAVINGMCSTATUS(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results.BEAMLOT = dbResults[0].BEAMLOT;
                    results.MC = dbResults[0].MC;
                    results.REEDNO2 = dbResults[0].REEDNO2;
                    results.WEFTYARN = dbResults[0].WEFTYARN;
                    results.TEMPLETYPE = dbResults[0].TEMPLETYPE;
                    results.BARNO = dbResults[0].BARNO;
                    results.STARTDATE = dbResults[0].STARTDATE;
                    results.FINISHDATE = dbResults[0].FINISHDATE;
                    results.FINISHFLAG = dbResults[0].FINISHFLAG;
                    results.SETTINGBY = dbResults[0].SETTINGBY;
                    results.EDITDATE = dbResults[0].EDITDATE;
                    results.EDITBY = dbResults[0].EDITBY;
                    results.ITM_WEAVING = dbResults[0].ITM_WEAVING;
                    results.PRODUCTTYPEID = dbResults[0].PRODUCTTYPEID;
                    results.WIDTH = dbResults[0].WIDTH;
                    results.BEAMLENGTH = dbResults[0].BEAMLENGTH;
                    results.REEDNO = dbResults[0].REEDNO;
                    results.HEALDCOLOR = dbResults[0].HEALDCOLOR;

                    results.SPEED = dbResults[0].SPEED;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ WEAV_GETWEFTYARNLISTBYDOFFNO ใช้ในการ Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_GETWEFTYARNLISTBYDOFFNO> WEAV_GETWEFTYARNLISTBYDOFFNO(string P_BEAMROLL, decimal? P_DOFFNO)
        {
            List<WEAV_GETWEFTYARNLISTBYDOFFNO> results = null;

            if (!HasConnection())
                return results;

            WEAV_GETWEFTYARNLISTBYDOFFNOParameter dbPara = new WEAV_GETWEFTYARNLISTBYDOFFNOParameter();
            dbPara.P_BEAMROLL = P_BEAMROLL;
            dbPara.P_DOFFNO = P_DOFFNO;

            List<WEAV_GETWEFTYARNLISTBYDOFFNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETWEFTYARNLISTBYDOFFNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETWEFTYARNLISTBYDOFFNO>();
                    foreach (WEAV_GETWEFTYARNLISTBYDOFFNOResult dbResult in dbResults)
                    {
                        WEAV_GETWEFTYARNLISTBYDOFFNO inst = new WEAV_GETWEFTYARNLISTBYDOFFNO();

                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.DOFFNO = dbResult.DOFFNO;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.CHLOTNO = dbResult.CHLOTNO;
                        inst.ADDDATE = dbResult.ADDDATE;
                        inst.ADDBY = dbResult.ADDBY;
                        inst.USETYPE = dbResult.USETYPE;
                        inst.REMARK = dbResult.REMARK;

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

        #region WEAV_GETWEAVELISTBYBEAMROLL ใช้ในการ Load Weavinging

        public List<WEAV_GETWEAVELISTBYBEAMROLL> WEAV_GETWEAVELISTBYBEAMROLL(string P_BEAMROLL, string P_LOOM)
        {
            List<WEAV_GETWEAVELISTBYBEAMROLL> results = null;

            if (!HasConnection())
                return results;

            WEAV_GETWEAVELISTBYBEAMROLLParameter dbPara = new WEAV_GETWEAVELISTBYBEAMROLLParameter();
            dbPara.P_BEAMROLL = P_BEAMROLL;
            dbPara.P_LOOM = P_LOOM;

            List<WEAV_GETWEAVELISTBYBEAMROLLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETWEAVELISTBYBEAMROLL(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETWEAVELISTBYBEAMROLL>();
                    int i = 1; 
                    foreach (WEAV_GETWEAVELISTBYBEAMROLLResult dbResult in dbResults)
                    {
                        WEAV_GETWEAVELISTBYBEAMROLL inst = new WEAV_GETWEAVELISTBYBEAMROLL();

                        inst.RowNo = i;

                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.WEAVINGDATE = dbResult.WEAVINGDATE;
                        inst.SHIFT = dbResult.SHIFT;
                        inst.REMARK = dbResult.REMARK;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.PREPAREBY = dbResult.PREPAREBY;
                        inst.WEAVINGNO = dbResult.WEAVINGNO;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.DOFFNO = dbResult.DOFFNO;
                        
                        inst.TENSION = dbResult.TENSION;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.SPEED = dbResult.SPEED;
                        inst.WASTE = dbResult.WASTE;

                        inst.DENSITY_WARP = dbResult.DENSITY_WARP;
                        inst.DENSITY_WEFT = dbResult.DENSITY_WEFT;

                        inst.DELETEFLAG = dbResult.DELETEFLAG;
                        inst.DELETEBY = dbResult.DELETEBY;
                        inst.DELETEDATE = dbResult.DELETEDATE;

                        if(!string.IsNullOrEmpty(inst.DELETEFLAG))
                        {
                            if (inst.DELETEFLAG == "1")
                                inst.DeleteHistory = "No";
                            else if (inst.DELETEFLAG == "0")
                                inst.DeleteHistory = "Yes";
                        }

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

        #region WEAV_GETMCSTOPBYLOT ใช้ในการ Load Weavinging
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WEAVINGLOT"></param>
        /// <returns></returns>
        public List<WEAV_GETMCSTOPBYLOT> WEAV_GETMCSTOPBYLOT(string P_WEAVINGLOT)
        {
            List<WEAV_GETMCSTOPBYLOT> results = null;

            if (string.IsNullOrEmpty(P_WEAVINGLOT))
                return results;

            if (!HasConnection())
                return results;

            WEAV_GETMCSTOPBYLOTParameter dbPara = new WEAV_GETMCSTOPBYLOTParameter();
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<WEAV_GETMCSTOPBYLOTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETMCSTOPBYLOT(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETMCSTOPBYLOT>();
                    int i = 1;
                    foreach (WEAV_GETMCSTOPBYLOTResult dbResult in dbResults)
                    {
                        WEAV_GETMCSTOPBYLOT inst = new WEAV_GETMCSTOPBYLOT();

                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.DEFECTCODE = dbResult.DEFECTCODE;
                        inst.DEFECTPOSITION = dbResult.DEFECTPOSITION;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.REMARK = dbResult.REMARK;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.BEAMERROLL = dbResult.BEAMERROLL;
                        inst.DOFFNO = dbResult.DOFFNO;
                        inst.DEFECTLENGTH = dbResult.DEFECTLENGTH;
                        inst.DESCRIPTION = dbResult.DESCRIPTION;
                        inst.WEAVSTARTDATE = dbResult.WEAVSTARTDATE;
                        inst.WEAVFINISHDATE = dbResult.WEAVFINISHDATE;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.LENGTH = dbResult.LENGTH;
                        
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

        #region เพิ่มใหม่ WEAV_GETINPROCESSBYBEAMROLL ใช้ในการ Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WEAV_GETINPROCESSBYBEAMROLL WEAV_GETINPROCESSBYBEAMROLL(string P_BEAMROLL, string P_LOOM)
        {
            WEAV_GETINPROCESSBYBEAMROLL results = new WEAV_GETINPROCESSBYBEAMROLL();

            if (!HasConnection())
                return results;

            WEAV_GETINPROCESSBYBEAMROLLParameter dbPara = new WEAV_GETINPROCESSBYBEAMROLLParameter();
            dbPara.P_BEAMROLL = P_BEAMROLL;
            dbPara.P_LOOM = P_LOOM;

            List<WEAV_GETINPROCESSBYBEAMROLLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETINPROCESSBYBEAMROLL(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results.WEAVINGLOT = dbResults[0].WEAVINGLOT;
                    results.ITM_WEAVING = dbResults[0].ITM_WEAVING;
                    results.LENGTH = dbResults[0].LENGTH;
                    results.LOOMNO = dbResults[0].LOOMNO;
                    results.WEAVINGDATE = dbResults[0].WEAVINGDATE;
                    results.SHIFT = dbResults[0].SHIFT;
                    results.REMARK = dbResults[0].REMARK;
                    results.CREATEDATE = dbResults[0].CREATEDATE;
                    results.WIDTH = dbResults[0].WIDTH;
                    results.PREPAREBY = dbResults[0].PREPAREBY;
                    results.WEAVINGNO = dbResults[0].WEAVINGNO;
                    results.BEAMLOT = dbResults[0].BEAMLOT;
                    results.DOFFNO = dbResults[0].DOFFNO;

                    results.TENSION = dbResults[0].TENSION;
                    results.STARTDATE = dbResults[0].STARTDATE;
                    results.DOFFBY = dbResults[0].DOFFBY;
                    results.SPEED = dbResults[0].SPEED;
                    results.WASTE = dbResults[0].WASTE;

                    results.DENSITY_WARP = dbResults[0].DENSITY_WARP;
                    results.DENSITY_WEFT = dbResults[0].DENSITY_WEFT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region WEAVE_CHECKWEAVINGMC ใช้ในการ Load Weavinging

        public List<WEAVE_CHECKWEAVINGMC> WEAVE_CHECKWEAVINGMC(string P_LOOMNO)
        {
            List<WEAVE_CHECKWEAVINGMC> results = null;

            if (!HasConnection())
                return results;

            WEAVE_CHECKWEAVINGMCParameter dbPara = new WEAVE_CHECKWEAVINGMCParameter();

            dbPara.P_LOOMNO = P_LOOMNO;

            List<WEAVE_CHECKWEAVINGMCResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAVE_CHECKWEAVINGMC(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAVE_CHECKWEAVINGMC>();

                    foreach (WEAVE_CHECKWEAVINGMCResult dbResult in dbResults)
                    {
                        WEAVE_CHECKWEAVINGMC inst = new WEAVE_CHECKWEAVINGMC();

                        inst.MACHINEID = dbResult.MACHINEID;
                        inst.PROCESSID = dbResult.PROCESSID;
                        inst.MCNAME = dbResult.MCNAME;
                        inst.ZONE = dbResult.ZONE;
                        inst.NO = dbResult.NO;
                        inst.TYPE = dbResult.TYPE;

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

        #region WEAV_GETSAMPLINGDATA ใช้ในการ Load Weavinging

        public List<WEAV_GETSAMPLINGDATA> WEAV_GETSAMPLINGDATA(string P_BEAMROLL, string P_LOOM)
        {
            List<WEAV_GETSAMPLINGDATA> results = null;

            if (!HasConnection())
                return results;

            WEAV_GETSAMPLINGDATAParameter dbPara = new WEAV_GETSAMPLINGDATAParameter();

            dbPara.P_BEAMROLL = P_BEAMROLL;
            dbPara.P_LOOM = P_LOOM;

            List<WEAV_GETSAMPLINGDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETSAMPLINGDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETSAMPLINGDATA>();

                    foreach (WEAV_GETSAMPLINGDATAResult dbResult in dbResults)
                    {
                        WEAV_GETSAMPLINGDATA inst = new WEAV_GETSAMPLINGDATA();

                        inst.BEAMERROLL = dbResult.BEAMERROLL;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.SETTINGDATE = dbResult.SETTINGDATE;
                        inst.BARNO = dbResult.BARNO;
                        inst.SPIRAL_L = dbResult.SPIRAL_L;
                        inst.SPIRAL_R = dbResult.SPIRAL_R;
                        inst.STSAMPLING = dbResult.STSAMPLING;
                        inst.RECUTSAMPLING = dbResult.RECUTSAMPLING;
                        inst.STSAMPLINGBY = dbResult.STSAMPLINGBY;
                        inst.RECUTBY = dbResult.RECUTBY;
                        inst.STDATE = dbResult.STDATE;
                        inst.RECUTDATE = dbResult.RECUTDATE;
                        inst.REMARK = dbResult.REMARK;

                        inst.BEAMMC = dbResult.BEAMMC;
                        inst.WARPMC = dbResult.WARPMC;
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

        #region WEAV_GETMCSTOPLISTBYDOFFNO ใช้ในการ Load Weavinging

        public List<WEAV_GETMCSTOPLISTBYDOFFNO> WEAV_GETMCSTOPLISTBYDOFFNO(string P_LOOMNO, decimal? P_DOFFNO, string P_BEAMROLL, string P_WEAVELOT)
        {
            List<WEAV_GETMCSTOPLISTBYDOFFNO> results = null;

            if (!HasConnection())
                return results;

            WEAV_GETMCSTOPLISTBYDOFFNOParameter dbPara = new WEAV_GETMCSTOPLISTBYDOFFNOParameter();

            dbPara.P_LOOMNO = P_LOOMNO;
            dbPara.P_DOFFNO = P_DOFFNO;
            dbPara.P_BEAMROLL = P_BEAMROLL;
            dbPara.P_WEAVELOT = P_WEAVELOT;

            List<WEAV_GETMCSTOPLISTBYDOFFNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETMCSTOPLISTBYDOFFNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETMCSTOPLISTBYDOFFNO>();

                    foreach (WEAV_GETMCSTOPLISTBYDOFFNOResult dbResult in dbResults)
                    {
                        WEAV_GETMCSTOPLISTBYDOFFNO inst = new WEAV_GETMCSTOPLISTBYDOFFNO();

                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.DEFECTCODE = dbResult.DEFECTCODE;
                        inst.DEFECTPOSITION = dbResult.DEFECTPOSITION;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.REMARK = dbResult.REMARK;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.BEAMERROLL = dbResult.BEAMERROLL;
                        inst.DOFFNO = dbResult.DOFFNO;
                        inst.DESCRIPTION = dbResult.DESCRIPTION;
                        inst.DEFECTLENGTH = dbResult.DEFECTLENGTH;
                        inst.STOPDATE = dbResult.STOPDATE;
                        
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

        #region WEAV_SEARCHPRODUCTION ใช้ในการ Load Weavinging

        public List<WEAV_SEARCHPRODUCTION> WEAV_SEARCHPRODUCTION(string P_LOOMNO, string P_BEAMERROLL, string P_ITMWEAVING, string P_SETDATE)
        {
            List<WEAV_SEARCHPRODUCTION> results = null;

            if (!HasConnection())
                return results;

            WEAV_SEARCHPRODUCTIONParameter dbPara = new WEAV_SEARCHPRODUCTIONParameter();

            dbPara.P_LOOMNO = P_LOOMNO;
            dbPara.P_BEAMERROLL = P_BEAMERROLL;
            dbPara.P_ITMWEAVING = P_ITMWEAVING;
            dbPara.P_SETDATE = P_SETDATE;

            List<WEAV_SEARCHPRODUCTIONResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_SEARCHPRODUCTION(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_SEARCHPRODUCTION>();

                    foreach (WEAV_SEARCHPRODUCTIONResult dbResult in dbResults)
                    {
                        WEAV_SEARCHPRODUCTION inst = new WEAV_SEARCHPRODUCTION();

                        inst.MC = dbResult.MC;
                        inst.REEDNO2 = dbResult.REEDNO2;
                        inst.WEFTYARN = dbResult.WEFTYARN;
                        inst.TEMPLETYPE = dbResult.TEMPLETYPE;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.BARNO = dbResult.BARNO;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.FINISHDATE = dbResult.FINISHDATE;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.SETTINGBY = dbResult.SETTINGBY;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.BEAMLENGTH = dbResult.BEAMLENGTH;
                        inst.SPEED = dbResult.SPEED;
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

        #region เพิ่มใหม่ WEAV_SHIPMENTREPORT ใช้ในการ Load SHIPMENTR

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_SHIPMENTREPORT> WEAV_SHIPMENTREPORT(string P_BEGINDATE, string P_ENDDATE)
        {
            List<WEAV_SHIPMENTREPORT> results = null;

            if (!HasConnection())
                return results;

            WEAV_SHIPMENTREPORTParameter dbPara = new WEAV_SHIPMENTREPORTParameter();
            dbPara.P_BEGINDATE = P_BEGINDATE;
            dbPara.P_ENDDATE = P_ENDDATE;

            List<WEAV_SHIPMENTREPORTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_SHIPMENTREPORT(dbPara);
                if (null != dbResults)
                {
                    int i = 0;
                    results = new List<WEAV_SHIPMENTREPORT>();
                    foreach (WEAV_SHIPMENTREPORTResult dbResult in dbResults)
                    {
                        WEAV_SHIPMENTREPORT inst = new WEAV_SHIPMENTREPORT();

                        inst.No = i+1;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.PIECES = dbResult.PIECES;
                        inst.METERS = dbResult.METERS;
                      
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

        #region INSERTUPDATEWEAVINGDATA

        public bool INSERTUPDATEWEAVINGDATA(string P_WEAVINGLOTNEW, string P_WEAVINGLOTOLD, string P_ITEMWEAVING, decimal? P_LENGHT,
            DateTime? P_WEAVINGDATE ,string P_LOOM ,string P_SHIFT ,decimal? P_WIDTH ,
            string P_REMARK ,string P_OPERATOR )
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WEAVINGLOTNEW))
                return result;
            if (string.IsNullOrWhiteSpace(P_WEAVINGLOTOLD))
                return result;
            if (string.IsNullOrWhiteSpace(P_ITEMWEAVING))
                return result;

            if (!HasConnection())
                return result;

            INSERTUPDATEWEAVINGDATAParameter dbPara = new INSERTUPDATEWEAVINGDATAParameter();
            dbPara.P_WEAVINGLOTNEW = P_WEAVINGLOTNEW;
            dbPara.P_WEAVINGLOTOLD = P_WEAVINGLOTOLD;
            dbPara.P_ITEMWEAVING = P_ITEMWEAVING;
            dbPara.P_LENGHT = P_LENGHT;
            dbPara.P_WEAVINGDATE = P_WEAVINGDATE;
            dbPara.P_LOOM = P_LOOM;
            dbPara.P_SHIFT = P_SHIFT;
            dbPara.P_WIDTH = P_WIDTH;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_OPERATOR = P_OPERATOR;

            INSERTUPDATEWEAVINGDATAResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.INSERTUPDATEWEAVINGDATA(dbPara);

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

        #region WEAV_DELETEWEAVINGLOT

        public bool WEAV_DELETEWEAVINGLOT(string P_WEAVINGLOT)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WEAVINGLOT))
                return result;

            if (!HasConnection())
                return result;

            WEAV_DELETEWEAVINGLOTParameter dbPara = new WEAV_DELETEWEAVINGLOTParameter();
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            WEAV_DELETEWEAVINGLOTResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WEAV_DELETEWEAVINGLOT(dbPara);

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

        #region WEAVE_INSERTPROCESSSETTING

        public WEAVE_INSERTPROCESSSETTING WEAVE_INSERTPROCESSSETTING(string P_BEAMLOT,string P_MC,string P_ITMWEAVE,string P_REEDNO2,
        string P_WEFTYARN,string P_TEMPLE,
        string P_BARNO, DateTime? P_STARTDATE, string P_SETTINGBY, string P_PRODUCTTYPE, decimal? P_WIDTH, decimal? P_BEAMLENGTH, decimal? P_SPEED)
        {
            WEAVE_INSERTPROCESSSETTING results = null;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return results;
            if (string.IsNullOrWhiteSpace(P_MC))
                return results;
            if (string.IsNullOrWhiteSpace(P_ITMWEAVE))
                return results;

            if (!HasConnection())
                return results;

            WEAVE_INSERTPROCESSSETTINGParameter dbPara = new WEAVE_INSERTPROCESSSETTINGParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_MC = P_MC;
            dbPara.P_ITMWEAVE = P_ITMWEAVE;
            dbPara.P_REEDNO2 = P_REEDNO2;
            dbPara.P_WEFTYARN = P_WEFTYARN;
            dbPara.P_TEMPLE = P_TEMPLE;
            dbPara.P_BARNO = P_BARNO;
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_SETTINGBY = P_SETTINGBY;
            dbPara.P_PRODUCTTYPE = P_PRODUCTTYPE;
            // เพิ่ม ใหม่
            dbPara.P_WIDTH = P_WIDTH;
            dbPara.P_BEAMLENGTH = P_BEAMLENGTH;
            dbPara.P_SPEED = P_SPEED;

            WEAVE_INSERTPROCESSSETTINGResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.WEAVE_INSERTPROCESSSETTING(dbPara);

                results = new WEAVE_INSERTPROCESSSETTING();

                if (null != dbResults)
                {
                    results.RESULT = dbResults.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region WEAVE_UPDATEPROCESSSETTING

        public string WEAVE_UPDATEPROCESSSETTING(string P_BEAMLOT,string P_REEDNO2,string P_TEMPLE,string P_BARNO,string P_PRODUCTTYPE,
        decimal? P_WIDTH, DateTime? P_FINISHDATE, string P_FLAG, DateTime? P_EDITDATE, string P_EDITBY, decimal? P_SPEED)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return results;

            if (!HasConnection())
                return results;

            WEAVE_UPDATEPROCESSSETTINGParameter dbPara = new WEAVE_UPDATEPROCESSSETTINGParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_REEDNO2 = P_REEDNO2;
            dbPara.P_TEMPLE = P_TEMPLE;
            dbPara.P_BARNO = P_BARNO;
            dbPara.P_PRODUCTTYPE = P_PRODUCTTYPE;
            dbPara.P_WIDTH = P_WIDTH;
            dbPara.P_FINISHDATE = P_FINISHDATE;
            dbPara.P_FLAG = P_FLAG;
            dbPara.P_EDITDATE = P_EDITDATE;
            dbPara.P_EDITBY = P_EDITBY;
            dbPara.P_SPEED = P_SPEED;

            WEAVE_UPDATEPROCESSSETTINGResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.WEAVE_UPDATEPROCESSSETTING(dbPara);

                if (null != dbResults)
                {
                    results = dbResults.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region WEAVE_WEAVINGPROCESS

        public string WEAVE_WEAVINGPROCESS(string P_BEAMLOT ,decimal? P_DOFFNO ,string P_ITEMWEAVING ,
        decimal? P_LENGHT ,DateTime? P_WEAVINGDATE ,string P_LOOM ,string P_SHIFT ,
        decimal? P_DENSITYWARP, decimal? P_DENSITYWEFT, decimal? P_TENSION, decimal? P_SPEED, decimal? P_WASTE, string P_REMARK, string P_OPERATOR, DateTime? P_STARTDATE)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return results;

            if (!HasConnection())
                return results;

            WEAVE_WEAVINGPROCESSParameter dbPara = new WEAVE_WEAVINGPROCESSParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_DOFFNO = P_DOFFNO;
            dbPara.P_ITEMWEAVING = P_ITEMWEAVING;
            dbPara.P_LENGHT = P_LENGHT;
            dbPara.P_WEAVINGDATE = P_WEAVINGDATE;
            dbPara.P_LOOM = P_LOOM;
            dbPara.P_SHIFT = P_SHIFT;
            dbPara.P_DENSITYWARP = P_DENSITYWARP;
            dbPara.P_DENSITYWEFT = P_DENSITYWEFT;
            dbPara.P_TENSION = P_TENSION;
            dbPara.P_SPEED = P_SPEED;
            dbPara.P_WASTE = P_WASTE;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_STARTDATE = P_STARTDATE;

            WEAVE_WEAVINGPROCESSResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.WEAVE_WEAVINGPROCESS(dbPara);

                if (null != dbResults)
                {
                    results = dbResults.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region WEAVE_INSERTUPDATEWEFTYARN

        public string WEAVE_INSERTUPDATEWEFTYARN(string P_BEAMLOT,decimal? P_DOFFNO,string P_PALLETNO,
            string P_CHLOTNO,DateTime? P_ADDDATE,string P_ADDBY,string P_USETYPE,string P_REMARK,string P_MCNO)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return results;

            if (!HasConnection())
                return results;

            WEAVE_INSERTUPDATEWEFTYARNParameter dbPara = new WEAVE_INSERTUPDATEWEFTYARNParameter();

            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_DOFFNO = P_DOFFNO;
            dbPara.P_PALLETNO = P_PALLETNO;
            dbPara.P_CHLOTNO = P_CHLOTNO;
            dbPara.P_ADDDATE = P_ADDDATE;
            dbPara.P_ADDBY = P_ADDBY;
            dbPara.P_USETYPE = P_USETYPE;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_MCNO = P_MCNO;
       

            WEAVE_INSERTUPDATEWEFTYARNResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.WEAVE_INSERTUPDATEWEFTYARN(dbPara);

                if (null != dbResults)
                {
                    results = dbResults.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region WEAVE_DELETEWEFTYARN

        public bool WEAVE_DELETEWEFTYARN(string P_BEAMLOT, decimal? P_DOFFNO, string P_PALLETNO, string P_CHLOTNO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return result;

            if (!HasConnection())
                return result;

            WEAVE_DELETEWEFTYARNParameter dbPara = new WEAVE_DELETEWEFTYARNParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_DOFFNO = P_DOFFNO;
            dbPara.P_PALLETNO = P_PALLETNO;
            dbPara.P_CHLOTNO = P_CHLOTNO;

            WEAVE_DELETEWEFTYARNResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WEAVE_DELETEWEFTYARN(dbPara);

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

        #region DRAW_UPDATEDRAWING

        public string DRAW_UPDATEDRAWING(string P_BEAMLOT,string P_DRAWINGTYPE,string P_REEDNO,string P_HEALDCOLOR,
        decimal? P_HEALDNO,string P_OPERATOR,string P_FLAG,string P_GROUP)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return results;

            if (!HasConnection())
                return results;

            DRAW_UPDATEDRAWINGParameter dbPara = new DRAW_UPDATEDRAWINGParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_DRAWINGTYPE = P_DRAWINGTYPE;
            dbPara.P_REEDNO = P_REEDNO;
            dbPara.P_HEALDCOLOR = P_HEALDCOLOR;
            dbPara.P_HEALDNO = P_HEALDNO;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_FLAG = P_FLAG;
            dbPara.P_GROUP = P_GROUP;

            DRAW_UPDATEDRAWINGResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.DRAW_UPDATEDRAWING(dbPara);

                if (null != dbResults)
                {
                    results = dbResults.R_RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region BeamChange

        public string BeamChange(string P_BEAMLOT,DateTime? P_FINISHDATE,string P_FLAG,string P_EDITBY)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return results;

            if (!HasConnection())
                return results;

            WEAVE_UPDATEPROCESSSETTINGParameter dbPara = new WEAVE_UPDATEPROCESSSETTINGParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_FINISHDATE = P_FINISHDATE;
            dbPara.P_FLAG = P_FLAG;
            dbPara.P_EDITBY = P_EDITBY;

            WEAVE_UPDATEPROCESSSETTINGResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.WEAVE_UPDATEPROCESSSETTING(dbPara);

                if (null != dbResults)
                {
                    results = dbResults.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region WEAVE_CANCELLOOMSETUP

        public string WEAVE_CANCELLOOMSETUP(string P_BEAMLOT, string P_LOOMNO, string P_OPERATOR)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return results;

            if (!HasConnection())
                return results;

            WEAVE_CANCELLOOMSETUPParameter dbPara = new WEAVE_CANCELLOOMSETUPParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_LOOMNO = P_LOOMNO;
            dbPara.P_OPERATOR = P_OPERATOR;

            WEAVE_CANCELLOOMSETUPResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.WEAVE_CANCELLOOMSETUP(dbPara);

                if (null != dbResults)
                {
                    results = dbResults.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region WEAV_SAMPLING

        public string WEAV_SAMPLING(string P_BEAMERROLL,
        string P_LOOM, string P_ITMWEAVE, DateTime? P_SETTINGDATE,
        string P_BARNO, decimal? P_SPIRIAL_L, decimal? P_SPIRIAL_R, decimal? P_SAMPLING,
        string P_SAMPLINGBY, decimal? P_RECUT, string P_RECUTBY, DateTime? P_RECUTDATE, string P_REMARK)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMERROLL))
                return results;

            if (!HasConnection())
                return results;

            WEAV_SAMPLINGParameter dbPara = new WEAV_SAMPLINGParameter();
            dbPara.P_BEAMERROLL = P_BEAMERROLL;
            dbPara.P_LOOM = P_LOOM;
            dbPara.P_ITMWEAVE = P_ITMWEAVE;
            dbPara.P_SETTINGDATE = P_SETTINGDATE;
            dbPara.P_BARNO = P_BARNO;
            dbPara.P_SPIRIAL_L = P_SPIRIAL_L;
            dbPara.P_SPIRIAL_R = P_SPIRIAL_R;
            dbPara.P_SAMPLING = P_SAMPLING;
            dbPara.P_SAMPLINGBY = P_SAMPLINGBY;
            dbPara.P_RECUT = P_RECUT;
            dbPara.P_RECUTBY = P_RECUTBY;
            dbPara.P_RECUTDATE = P_RECUTDATE;
            dbPara.P_REMARK = P_REMARK;

            WEAV_SAMPLINGResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.WEAV_SAMPLING(dbPara);

                if (null != dbResults)
                {
                    results = dbResults.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region WEAV_INSERTMCSTOP

        public string WEAV_INSERTMCSTOP( string P_LOOMNO ,decimal? P_DOFFNO ,string P_BEAMROLL , string P_WEAVINGLOT ,
                         string P_DEFECT, decimal? P_LENGTH, decimal? P_POSITION, string P_REMARK, string P_OPERATOR, DateTime? P_DATE)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(P_DEFECT))
                return results;

            if (!HasConnection())
                return results;

            WEAV_INSERTMCSTOPParameter dbPara = new WEAV_INSERTMCSTOPParameter();

            dbPara.P_LOOMNO = P_LOOMNO;
            dbPara.P_DOFFNO = P_DOFFNO;
            dbPara.P_BEAMROLL = P_BEAMROLL;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;
            dbPara.P_DEFECT = P_DEFECT;
            dbPara.P_LENGTH = P_LENGTH;
            dbPara.P_POSITION = P_POSITION;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_DATE = P_DATE;

            WEAV_INSERTMCSTOPResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.WEAV_INSERTMCSTOP(dbPara);

                if (null != dbResults)
                {
                    results = dbResults.R_RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region WEAV_DELETEWEAVINGLOT

        public bool WEAV_DELETEWEAVINGLOT(string P_WEAVINGLOT, string P_REMARK, string P_OPERATOR)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WEAVINGLOT))
                return result;

            if (!HasConnection())
                return result;

            WEAV_DELETEWEAVINGLOTParameter dbPara = new WEAV_DELETEWEAVINGLOTParameter();
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_OPERATOR = P_OPERATOR;

            WEAV_DELETEWEAVINGLOTResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WEAV_DELETEWEAVINGLOT(dbPara);

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

        #region WEAV_DELETEMCSTOP

        public bool WEAV_DELETEMCSTOP(string P_LOOMNO, decimal? P_DOFFNO, string P_BEAMROLL, string P_DEFECT, DateTime? P_DATE)
        {
            bool result = false;

            if (!HasConnection())
                return result;

            WEAV_DELETEMCSTOPParameter dbPara = new WEAV_DELETEMCSTOPParameter();
            dbPara.P_LOOMNO = P_LOOMNO;
            dbPara.P_DOFFNO = P_DOFFNO;
            dbPara.P_BEAMROLL = P_BEAMROLL;
            dbPara.P_DEFECT = P_DEFECT;
            dbPara.P_DATE = P_DATE;

            WEAV_DELETEMCSTOPResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WEAV_DELETEMCSTOP(dbPara);

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

        #region WEAV_UPDATEWEAVINGLOT

        public bool WEAV_UPDATEWEAVINGLOT(string P_WEAVINGLOT,decimal? P_LENGHT, string P_SHIFT,
                decimal? P_DENSITYWARP,decimal? P_DENSITYWEFT,decimal? P_TENSION,decimal? P_WASTE)
        {
            bool result = false;

            if (!HasConnection())
                return result;

            WEAV_UPDATEWEAVINGLOTParameter dbPara = new WEAV_UPDATEWEAVINGLOTParameter();
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;
            dbPara.P_LENGHT = P_LENGHT;
            dbPara.P_SHIFT = P_SHIFT;
            dbPara.P_DENSITYWARP = P_DENSITYWARP;
            dbPara.P_DENSITYWEFT = P_DENSITYWEFT;
            dbPara.P_TENSION = P_TENSION;
            dbPara.P_WASTE = P_WASTE;

            WEAV_UPDATEWEAVINGLOTResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WEAV_UPDATEWEAVINGLOT(dbPara);

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


        #region WEAV_UPDATEWEFTSTOCK

        public string WEAV_UPDATEWEFTSTOCK(string P_BEAMLOT, string P_WEAVINGLOT, decimal? P_DOFFNO, string P_LOOMNO, string P_ITMYARN)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return results;

            if (string.IsNullOrWhiteSpace(P_WEAVINGLOT))
                return results;

            if (!HasConnection())
                return results;

            WEAV_UPDATEWEFTSTOCKParameter dbPara = new WEAV_UPDATEWEFTSTOCKParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;
            dbPara.P_DOFFNO = P_DOFFNO;
            dbPara.P_LOOMNO = P_LOOMNO;
            dbPara.P_ITMYARN = P_ITMYARN;

            WEAV_UPDATEWEFTSTOCKResult dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_UPDATEWEFTSTOCK(dbPara);

                if (null != dbResults)
                {
                    results = dbResults.R_RESULT;
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








