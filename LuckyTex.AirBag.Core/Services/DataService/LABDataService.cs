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
    #region LAB Data Service

    /// <summary>
    /// The data service for Packing process.
    /// </summary>
    public class LABDataService : BaseDataService
    {
        #region Singelton

        private static LABDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static LABDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(LABDataService))
                    {
                        _instance = new LABDataService();
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
        private LABDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~LABDataService()
        {
        }

        #endregion

        #region Public Methods

        #region Create new Session

        public LABSession GetSession(LogInResult loginResult)
        {
            LABSession result = new LABSession();
            result.Init(loginResult);
            return result;
        }

        #endregion

        #region เพิ่มใหม่ ITM_GETITEMCODELIST ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ITM_GETITEMCODELIST> ITM_GETITEMCODELIST()
        {
            List<ITM_GETITEMCODELIST> results = null;

            if (!HasConnection())
                return results;

            ITM_GETITEMCODELISTParameter dbPara = new ITM_GETITEMCODELISTParameter();

            List<ITM_GETITEMCODELISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.ITM_GETITEMCODELIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<ITM_GETITEMCODELIST>();
                    foreach (ITM_GETITEMCODELISTResult dbResult in dbResults)
                    {
                        ITM_GETITEMCODELIST inst = new ITM_GETITEMCODELIST();

                        inst.ITM_CODE = dbResult.ITM_CODE;

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

        #region เพิ่มใหม่ LAB_GETINSPECTIONLIST ใช้ในการ Load LAB

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<LAB_GETINSPECTIONLIST> LAB_GETINSPECTIONLIST(string INSLOT, string strDATE)
        {
            List<LAB_GETINSPECTIONLIST> results = null;

            if (!HasConnection())
                return results;

            LAB_GETINSPECTIONLISTParameter dbPara = new LAB_GETINSPECTIONLISTParameter();
            dbPara.P_INSLOT = INSLOT;
            dbPara.P_DATE = strDATE;

            List<LAB_GETINSPECTIONLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETINSPECTIONLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETINSPECTIONLIST>();
                    foreach (LAB_GETINSPECTIONLISTResult dbResult in dbResults)
                    {
                        LAB_GETINSPECTIONLIST inst = new LAB_GETINSPECTIONLIST();

                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.GRADE = dbResult.GRADE;
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;
                        inst.ISLAB = dbResult.ISLAB;
                        inst.INSPECTIONDATE = dbResult.INSPECTIONDATE;

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

        #region เพิ่มใหม่ LAB_CHECKRECEIVESAMPLING ใช้ในการ Load LAB

      /// <summary>
      /// 
      /// </summary>
      /// <param name="P_WEAVLOT"></param>
      /// <param name="P_ITEMCODE"></param>
      /// <returns></returns>
        public string LAB_CHECKRECEIVESAMPLING(string P_WEAVLOT, string P_ITEMCODE)
        {
            string results = string.Empty;

            if (!HasConnection())
                return results;

            LAB_CHECKRECEIVESAMPLINGParameter dbPara = new LAB_CHECKRECEIVESAMPLINGParameter();
            dbPara.P_WEAVLOT = P_WEAVLOT;
            dbPara.P_ITEMCODE = P_ITEMCODE;

            LAB_CHECKRECEIVESAMPLINGResult dbResults = new LAB_CHECKRECEIVESAMPLINGResult();

            try
            {
                dbResults = DatabaseManager.Instance.LAB_CHECKRECEIVESAMPLING(dbPara);

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

        #region เพิ่มใหม่ LAB_CHECKRECEIVEGREIGESAMPLING ใช้ในการ Load LAB

        public string LAB_CHECKRECEIVEGREIGESAMPLING(string P_BEAMERROLL, string P_LOOMNO)
        {
            string results = string.Empty;

            if (!HasConnection())
                return results;

            LAB_CHECKRECEIVEGREIGESAMPLINGParameter dbPara = new LAB_CHECKRECEIVEGREIGESAMPLINGParameter();
            dbPara.P_BEAMERROLL = P_BEAMERROLL;
            dbPara.P_LOOMNO = P_LOOMNO;

            LAB_CHECKRECEIVEGREIGESAMPLINGResult dbResults = new LAB_CHECKRECEIVEGREIGESAMPLINGResult();

            try
            {
                dbResults = DatabaseManager.Instance.LAB_CHECKRECEIVEGREIGESAMPLING(dbPara);

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

        #region เพิ่มใหม่ LAB_GETFINISHINGSAMPLING ใช้ในการ Load LAB

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<LAB_GETFINISHINGSAMPLING> LAB_GETFINISHINGSAMPLING(string P_WEAVLOT, string P_ITEMCODE)
        {
            List<LAB_GETFINISHINGSAMPLING> results = null;

            if (!HasConnection())
                return results;

            LAB_GETFINISHINGSAMPLINGParameter dbPara = new LAB_GETFINISHINGSAMPLINGParameter();
            dbPara.P_WEAVLOT = P_WEAVLOT;
            dbPara.P_ITEMCODE = P_ITEMCODE;

            List<LAB_GETFINISHINGSAMPLINGResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETFINISHINGSAMPLING(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETFINISHINGSAMPLING>();
                    foreach (LAB_GETFINISHINGSAMPLINGResult dbResult in dbResults)
                    {
                        LAB_GETFINISHINGSAMPLING inst = new LAB_GETFINISHINGSAMPLING();

                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.PRODUCTID = dbResult.PRODUCTID;
                        inst.SAMPLING_WIDTH = dbResult.SAMPLING_WIDTH;
                        inst.SAMPLING_LENGTH = dbResult.SAMPLING_LENGTH;
                        inst.PROCESS = dbResult.PROCESS;
                        inst.REMARK = dbResult.REMARK;
                        inst.FABRICTYPE = dbResult.FABRICTYPE;
                        inst.RETESTFLAG = dbResult.RETESTFLAG;

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

        #region เพิ่มใหม่ LAB_GETWEAVINGSAMPLING ใช้ในการ Load LAB
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERROLL"></param>
        /// <param name="P_LOOMNO"></param>
        /// <returns></returns>
        public List<LAB_GETWEAVINGSAMPLING> LAB_GETWEAVINGSAMPLING(string P_BEAMERROLL, string P_LOOMNO)
        {
            List<LAB_GETWEAVINGSAMPLING> results = null;

            if (!HasConnection())
                return results;

            LAB_GETWEAVINGSAMPLINGParameter dbPara = new LAB_GETWEAVINGSAMPLINGParameter();
            dbPara.P_BEAMERROLL = P_BEAMERROLL;
            dbPara.P_LOOMNO = P_LOOMNO;

            List<LAB_GETWEAVINGSAMPLINGResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETWEAVINGSAMPLING(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETWEAVINGSAMPLING>();
                    foreach (LAB_GETWEAVINGSAMPLINGResult dbResult in dbResults)
                    {
                        LAB_GETWEAVINGSAMPLING inst = new LAB_GETWEAVINGSAMPLING();

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

        #region เพิ่มใหม่ LAB_SEARCHLABGREIGE ใช้ในการ Load LAB_SEARCHLABGREIGE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERROLL"></param>
        /// <param name="P_LOOM"></param>
        /// <param name="P_ITMWEAVE"></param>
        /// <param name="P_SETTINGDATE"></param>
        /// <param name="P_SENDDATE"></param>
        /// <param name="P_TESTRESULT"></param>
        /// <returns></returns>
        public List<LAB_SEARCHLABGREIGE> LAB_SEARCHLABGREIGE(string P_BEAMERROLL, string P_LOOM, string P_ITMWEAVE
            , string P_SETTINGDATE, string P_SENDDATE, string P_TESTRESULT)
        {
            List<LAB_SEARCHLABGREIGE> results = null;

            if (!HasConnection())
                return results;

            LAB_SEARCHLABGREIGEParameter dbPara = new LAB_SEARCHLABGREIGEParameter();
            dbPara.P_BEAMERROLL = P_BEAMERROLL;
            dbPara.P_LOOM = P_LOOM;
            dbPara.P_ITMWEAVE = P_ITMWEAVE;
            dbPara.P_SETTINGDATE = P_SETTINGDATE;
            dbPara.P_SENDDATE = P_SENDDATE;
            dbPara.P_TESTRESULT = P_TESTRESULT;

            List<LAB_SEARCHLABGREIGEResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_SEARCHLABGREIGE(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_SEARCHLABGREIGE>();
                    foreach (LAB_SEARCHLABGREIGEResult dbResult in dbResults)
                    {
                        LAB_SEARCHLABGREIGE inst = new LAB_SEARCHLABGREIGE();

                        inst.BEAMERROLL = dbResult.BEAMERROLL;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.TESTNO = dbResult.TESTNO;

                        if (inst.TESTNO != null)
                        {
                            if (inst.TESTNO >= 2)
                                inst.Recut = "Y";
                        }

                        inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                        inst.RECEIVEBY = dbResult.RECEIVEBY;
                        inst.STATUS = dbResult.STATUS;
                        inst.CONDITIONINGTIME = dbResult.CONDITIONINGTIME;
                        inst.TESTDATE = dbResult.TESTDATE;
                        inst.TESTRESULT = dbResult.TESTRESULT;

                        if (!string.IsNullOrEmpty(inst.TESTRESULT))
                            inst.Judgment = inst.TESTRESULT;

                        inst.REMARK = dbResult.REMARK;
                        inst.TESTBY = dbResult.TESTBY;
                        inst.APPROVESTATUS = dbResult.APPROVESTATUS;
                        inst.APPROVEBY = dbResult.APPROVEBY;
                        inst.SENDDATE = dbResult.SENDDATE;
                        inst.APPROVEDATE = dbResult.APPROVEDATE;
                        inst.SETTINGDATE = dbResult.SETTINGDATE;

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

        #region เพิ่มใหม่ LAB_SEARCHLABMASSPRO ใช้ในการ Load LAB_SEARCHLABMASSPRO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WEAVELOT"></param>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_FABRICTPE"></param>
        /// <param name="P_SENDDATE"></param>
        /// <param name="P_TESTRESULT"></param>
        /// <returns></returns>
        public List<LAB_SEARCHLABMASSPRO> LAB_SEARCHLABMASSPRO(string P_WEAVELOT, string P_ITMCODE, string P_FABRICTPE, string P_SENDDATE, string P_TESTRESULT)
        {
            List<LAB_SEARCHLABMASSPRO> results = null;

            if (!HasConnection())
                return results;

            LAB_SEARCHLABMASSPROParameter dbPara = new LAB_SEARCHLABMASSPROParameter();
            dbPara.P_WEAVELOT = P_WEAVELOT;
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_FABRICTPE = P_FABRICTPE;
            dbPara.P_SENDDATE = P_SENDDATE;
            dbPara.P_TESTRESULT = P_TESTRESULT;

            List<LAB_SEARCHLABMASSPROResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_SEARCHLABMASSPRO(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_SEARCHLABMASSPRO>();
                    foreach (LAB_SEARCHLABMASSPROResult dbResult in dbResults)
                    {
                        LAB_SEARCHLABMASSPRO inst = new LAB_SEARCHLABMASSPRO();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                        inst.RECEIVEBY = dbResult.RECEIVEBY;
                        inst.STATUS = dbResult.STATUS;
                        inst.CONDITIONINGTIME = dbResult.CONDITIONINGTIME;
                        inst.TESTDATE = dbResult.TESTDATE;
                        inst.TESTRESULT = dbResult.TESTRESULT;

                        if (!string.IsNullOrEmpty(inst.TESTRESULT))
                            inst.Judgment = inst.TESTRESULT;

                        inst.REMARK = dbResult.REMARK;
                        inst.TESTBY = dbResult.TESTBY;
                        inst.APPROVESTATUS = dbResult.APPROVESTATUS;
                        inst.APPROVEBY = dbResult.APPROVEBY;
                        inst.SENDDATE = dbResult.SENDDATE;
                        inst.APPROVEDATE = dbResult.APPROVEDATE;
                        inst.FABRICTYPE = dbResult.FABRICTYPE;

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

        #region เพิ่มใหม่ LAB_SEARCHLABMASSPRO ใช้ในการ Load LAB_SEARCHLABMASSPRO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WEAVELOT"></param>
        /// <param name="P_RECEIVEDATE"></param>
        /// <returns></returns>
        public List<LAB_MASSPROSTOCKSTATUS> LAB_MASSPROSTOCKSTATUS(string P_WEAVELOT, string P_RECEIVEDATE)
        {
            List<LAB_MASSPROSTOCKSTATUS> results = null;

            if (!HasConnection())
                return results;

            LAB_MASSPROSTOCKSTATUSParameter dbPara = new LAB_MASSPROSTOCKSTATUSParameter();
            dbPara.P_WEAVELOT = P_WEAVELOT;
            dbPara.P_RECEIVEDATE = P_RECEIVEDATE;

            List<LAB_MASSPROSTOCKSTATUSResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_MASSPROSTOCKSTATUS(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_MASSPROSTOCKSTATUS>();
                    foreach (LAB_MASSPROSTOCKSTATUSResult dbResult in dbResults)
                    {
                        LAB_MASSPROSTOCKSTATUS inst = new LAB_MASSPROSTOCKSTATUS();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                        inst.RECEIVEBY = dbResult.RECEIVEBY;
                        inst.STATUS = dbResult.STATUS;
                        
                        // ปรับ 18/02/16
                        inst.CONDITIONINGTIME = dbResult.CONDITIONINGTIME;

                        inst.TESTDATE = dbResult.TESTDATE;
                        inst.TESTRESULT = dbResult.TESTRESULT;

                        if (!string.IsNullOrEmpty(inst.TESTRESULT))
                            inst.Judgment = inst.TESTRESULT;

                        inst.REMARK = dbResult.REMARK;
                        inst.TESTBY = dbResult.TESTBY;
                        inst.APPROVESTATUS = dbResult.APPROVESTATUS;
                        inst.APPROVEBY = dbResult.APPROVEBY;
                        inst.SENDDATE = dbResult.SENDDATE;
                        inst.APPROVEDATE = dbResult.APPROVEDATE;
                        inst.LABFORM = dbResult.LABFORM;

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

        #region เพิ่มใหม่ LAB_GREIGESTOCKSTATUS ใช้ในการ Load LAB_GREIGESTOCKSTATUS

        public List<LAB_GREIGESTOCKSTATUS> LAB_GREIGESTOCKSTATUS(string P_BEAMERROLL,string P_LOOMNO , string P_RECEIVEDATE)
        {
            List<LAB_GREIGESTOCKSTATUS> results = null;

            if (!HasConnection())
                return results;

            LAB_GREIGESTOCKSTATUSParameter dbPara = new LAB_GREIGESTOCKSTATUSParameter();
            dbPara.P_BEAMERROLL = P_BEAMERROLL;
            dbPara.P_LOOMNO = P_LOOMNO;
            dbPara.P_RECEIVEDATE = P_RECEIVEDATE;

            List<LAB_GREIGESTOCKSTATUSResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GREIGESTOCKSTATUS(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GREIGESTOCKSTATUS>();
                    foreach (LAB_GREIGESTOCKSTATUSResult dbResult in dbResults)
                    {
                        LAB_GREIGESTOCKSTATUS inst = new LAB_GREIGESTOCKSTATUS();

                        inst.BEAMERROLL = dbResult.BEAMERROLL;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                        inst.RECEIVEBY = dbResult.RECEIVEBY;
                        inst.STATUS = dbResult.STATUS;
                        inst.CONDITIONINGTIME = dbResult.CONDITIONINGTIME;
                        inst.TESTDATE = dbResult.TESTDATE;
                        inst.TESTRESULT = dbResult.TESTRESULT;

                        if (!string.IsNullOrEmpty(inst.TESTRESULT))
                            inst.Judgment = inst.TESTRESULT;

                        inst.REMARK = dbResult.REMARK;
                        inst.TESTBY = dbResult.TESTBY;
                        inst.APPROVESTATUS = dbResult.APPROVESTATUS;
                        inst.APPROVEBY = dbResult.APPROVEBY;
                        inst.SENDDATE = dbResult.SENDDATE;
                        inst.APPROVEDATE = dbResult.APPROVEDATE;
                        inst.TESTNO = dbResult.TESTNO;

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

        #region เพิ่มใหม่ LAB_WEAVINGHISTORY ใช้ในการ Load LAB_WEAVINGHISTORY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WEAVINGLOT"></param>
        /// <returns></returns>
        public List<LAB_WEAVINGHISTORY> LAB_WEAVINGHISTORY(string P_WEAVINGLOT)
        {
            List<LAB_WEAVINGHISTORY> results = null;

            if (!HasConnection())
                return results;

            LAB_WEAVINGHISTORYParameter dbPara = new LAB_WEAVINGHISTORYParameter();
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<LAB_WEAVINGHISTORYResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_WEAVINGHISTORY(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_WEAVINGHISTORY>();
                    foreach (LAB_WEAVINGHISTORYResult dbResult in dbResults)
                    {
                        LAB_WEAVINGHISTORY inst = new LAB_WEAVINGHISTORY();

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

        #region LAB_SAVELABRESULT

        public void LAB_SAVELABRESULT(string INSLOT, string RESULT)
        {
            if (string.IsNullOrWhiteSpace(INSLOT))
                return;

            if (!HasConnection())
                return;

            LAB_SAVELABRESULTParameter dbPara = new LAB_SAVELABRESULTParameter();
            dbPara.P_INSLOT = INSLOT;
            dbPara.P_RESULT = RESULT;
            
            LAB_SAVELABRESULTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_SAVELABRESULT(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #region LAB_ReceiveSampling

        public void LAB_ReceiveSampling(string P_WEAVELOT, string P_FINISHLOT,string P_ITMCODE, DateTime? P_RECEIVEDATE, string P_RECEIVEBY, string P_STATUS)
        {
            if (string.IsNullOrWhiteSpace(P_WEAVELOT))
                return;

            if (!HasConnection())
                return;

            LAB_UPDATEMASSPROSTOCKParameter dbPara = new LAB_UPDATEMASSPROSTOCKParameter();
            dbPara.P_WEAVELOT = P_WEAVELOT;
            dbPara.P_FINISHLOT = P_FINISHLOT;
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_RECEIVEDATE = P_RECEIVEDATE;
            dbPara.P_RECEIVEBY = P_RECEIVEBY;
            dbPara.P_STATUS = P_STATUS;

            LAB_UPDATEMASSPROSTOCKResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_UPDATEMASSPROSTOCK(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #region LAB_GreigeReceiveSampling

        public void LAB_GreigeReceiveSampling(string P_BEAMERROLL,string P_LOOMNO,decimal? P_TESTNO,DateTime? P_RECEIVEDATE,string P_RECEIVEBY,string P_STATUS)
        {
            if (string.IsNullOrWhiteSpace(P_BEAMERROLL))
                return;

            if (!HasConnection())
                return;

            LAB_UPDATEGREIGESTOCKParameter dbPara = new LAB_UPDATEGREIGESTOCKParameter();
            dbPara.P_BEAMERROLL = P_BEAMERROLL;
            dbPara.P_LOOMNO = P_LOOMNO;
            dbPara.P_TESTNO = P_TESTNO;
            dbPara.P_RECEIVEDATE = P_RECEIVEDATE;
            dbPara.P_RECEIVEBY = P_RECEIVEBY;
            dbPara.P_STATUS = P_STATUS;

            LAB_UPDATEGREIGESTOCKResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_UPDATEGREIGESTOCK(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #region LAB_SamplingStatus

        public void LAB_SamplingStatus(string P_WEAVELOT, string P_FINISHLOT, string P_ITMCODE, DateTime? P_CONDITONTIME, string P_STATUS, string P_TESTBY)
        {
            if (string.IsNullOrWhiteSpace(P_WEAVELOT))
                return;

            if (!HasConnection())
                return;

            LAB_UPDATEMASSPROSTOCKParameter dbPara = new LAB_UPDATEMASSPROSTOCKParameter();
            dbPara.P_WEAVELOT = P_WEAVELOT;
            dbPara.P_FINISHLOT = P_FINISHLOT;
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_CONDITONTIME = P_CONDITONTIME;
            dbPara.P_STATUS = P_STATUS;
            dbPara.P_TESTBY = P_TESTBY;

            LAB_UPDATEMASSPROSTOCKResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_UPDATEMASSPROSTOCK(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #region LAB_GreigeSamplingStatus

        public bool LAB_GreigeSamplingStatus(string P_BEAMERROLL, string P_LOOMNO, decimal? P_TESTNO, DateTime? P_CONDITONTIME, string P_STATUS, string P_TESTBY)
        {
            if (string.IsNullOrWhiteSpace(P_BEAMERROLL))
                return false;

            if (!HasConnection())
                return false;

            LAB_UPDATEGREIGESTOCKParameter dbPara = new LAB_UPDATEGREIGESTOCKParameter();
            dbPara.P_BEAMERROLL = P_BEAMERROLL;
            dbPara.P_LOOMNO = P_LOOMNO;
            dbPara.P_TESTNO = P_TESTNO;
            dbPara.P_CONDITONTIME = P_CONDITONTIME;
            dbPara.P_STATUS = P_STATUS;
            dbPara.P_TESTBY = P_TESTBY;

            LAB_UPDATEGREIGESTOCKResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_UPDATEGREIGESTOCK(dbPara);

                if (dbResult != null)
                {
                    if (string.IsNullOrEmpty(dbResult.RESULT))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                ex.Err();
                return false;
            }
        }
        #endregion

        #region LAB_SAVELABGREIGERESULT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERROLL"></param>
        /// <param name="P_LOOM"></param>
        /// <param name="P_ITMWEAVE"></param>
        /// <param name="P_TESTRESULT"></param>
        /// <param name="P_TESTNO"></param>
        public bool LAB_SAVELABGREIGERESULT(string P_BEAMERROLL,string P_LOOM,string P_ITMWEAVE,string P_TESTRESULT,decimal? P_TESTNO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_BEAMERROLL))
                return result;

            if (!HasConnection())
                return result;

            LAB_SAVELABGREIGERESULTParameter dbPara = new LAB_SAVELABGREIGERESULTParameter();
            dbPara.P_BEAMERROLL = P_BEAMERROLL;
            dbPara.P_LOOM = P_LOOM;
            dbPara.P_ITMWEAVE = P_ITMWEAVE;
            dbPara.P_TESTRESULT = P_TESTRESULT;
            dbPara.P_TESTNO = P_TESTNO;

            LAB_SAVELABGREIGERESULTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_SAVELABGREIGERESULT(dbPara);

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

        #region LAB_SAVELABMASSPRORESULT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WEAVELOT"></param>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_TESTRESULT"></param>
        public bool LAB_SAVELABMASSPRORESULT(string P_WEAVELOT, string P_ITMCODE, string P_FINISHINGLOT, string P_TESTRESULT)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WEAVELOT))
                return result;

            if (!HasConnection())
                return result;

            LAB_SAVELABMASSPRORESULTParameter dbPara = new LAB_SAVELABMASSPRORESULTParameter();
            dbPara.P_WEAVELOT = P_WEAVELOT;
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_TESTRESULT = P_TESTRESULT;

            LAB_SAVELABMASSPRORESULTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_SAVELABMASSPRORESULT(dbPara);
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

        // New 31/08/20
        #region LAB_SEARCHLABENTRYPRODUCTION
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_ENTRYSTARTDATE"></param>
        /// <param name="P_ENTRYENDDATE"></param>
        /// <param name="P_LOOM"></param>
        /// <param name="P_FINISHPROCESS"></param>
        /// <returns></returns>
        public List<LAB_SEARCHLABENTRYPRODUCTION_Rep> LAB_SEARCHLABENTRYPRODUCTION(string P_ITMCODE, string P_ENTRYSTARTDATE, string P_ENTRYENDDATE, string P_LOOM, string P_FINISHPROCESS
            , string P_WEAVINGLOG, string P_FINISHINGLOT, DateTime? ENTRYDATE)
        {
            List<LAB_SEARCHLABENTRYPRODUCTION_Rep> results = null;

            if (!HasConnection())
                return results;

            LAB_SEARCHLABENTRYPRODUCTIONParameter dbPara = new LAB_SEARCHLABENTRYPRODUCTIONParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_ENTRYSTARTDATE = P_ENTRYSTARTDATE;
            dbPara.P_ENTRYENDDATE = P_ENTRYENDDATE;
            dbPara.P_LOOM = P_LOOM;
            dbPara.P_FINISHPROCESS = P_FINISHPROCESS;

            List<LAB_SEARCHLABENTRYPRODUCTIONResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_SEARCHLABENTRYPRODUCTION(dbPara);
                if (null != dbResults)
                {
                    int? i = 0;
                    results = new List<LAB_SEARCHLABENTRYPRODUCTION_Rep>();

                    LAB_GETREPORTINFO_Rep reportInfo = null;
                    LAB_GETITEMTESTSPECIFICATION_Rep itemSpe = null;

                    foreach (LAB_SEARCHLABENTRYPRODUCTIONResult dbResult in dbResults)
                    {
                        LAB_SEARCHLABENTRYPRODUCTION_Rep inst = new LAB_SEARCHLABENTRYPRODUCTION_Rep();
                        i = 0;

                        if (dbResult.WEAVINGLOT == P_WEAVINGLOG && dbResult.FINISHINGLOT == P_FINISHINGLOT && dbResult.ITM_CODE == P_ITMCODE
                            && dbResult.ENTRYDATE == ENTRYDATE)
                        {
                            inst.PARTNO = dbResult.PARTNO;

                            inst.ITM_CODE = dbResult.ITM_CODE;
                            //inst.ITM_CODE_B = CheckItemCode(dbResult.ITM_CODE);
                            inst.ITM_CODE_H = dbResult.ITM_CODE_H;
                            inst.ITM_CODE_B = CheckItemCode(dbResult.ITM_CODE_H);

                            #region LAB_GETITEMTESTSPECIFICATION_Rep
                            itemSpe = new LAB_GETITEMTESTSPECIFICATION_Rep();
                            itemSpe = LAB_GETITEMTESTSPECIFICATION(dbResult.ITM_CODE);
                            if (null != itemSpe)
                            {
                                inst.NUMTHREADS_W_Min = itemSpe.NUMTHREADS_W_Min;
                                inst.NUMTHREADS_W_Max = itemSpe.NUMTHREADS_W_Max;

                                inst.NUMTHREADS_F_Min = itemSpe.NUMTHREADS_F_Min;
                                inst.NUMTHREADS_F_Max = itemSpe.NUMTHREADS_F_Max;

                                inst.USABLE_WIDTH_Min = itemSpe.USABLE_WIDTH_Min;
                                inst.WIDTH_SILICONE_Min = itemSpe.WIDTH_SILICONE_Min;

                                inst.TOTALWEIGHT_Min = itemSpe.TOTALWEIGHT_Min;
                                inst.TOTALWEIGHT_Max = itemSpe.TOTALWEIGHT_Max;

                                inst.UNCOATEDWEIGHT_Min = itemSpe.UNCOATEDWEIGHT_Min;
                                inst.UNCOATEDWEIGHT_Max = itemSpe.UNCOATEDWEIGHT_Max;

                                inst.COATINGWEIGHT_Min = itemSpe.COATINGWEIGHT_Min;
                                inst.COATINGWEIGHT_Max = itemSpe.COATINGWEIGHT_Max;

                                inst.MAXFORCE_W_Min = itemSpe.MAXFORCE_W_Min;
                                inst.MAXFORCE_F_Min = itemSpe.MAXFORCE_F_Min;

                                inst.ELONGATIONFORCE_W_TOR = itemSpe.ELONGATIONFORCE_W_TOR;
                                inst.ELONGATIONFORCE_W_Min = itemSpe.ELONGATIONFORCE_W_Min;
                                inst.ELONGATIONFORCE_W_Max = itemSpe.ELONGATIONFORCE_W_Max;

                                inst.ELONGATIONFORCE_F_TOR = itemSpe.ELONGATIONFORCE_F_TOR;
                                inst.ELONGATIONFORCE_F_Min = itemSpe.ELONGATIONFORCE_F_Min;
                                inst.ELONGATIONFORCE_F_Max = itemSpe.ELONGATIONFORCE_F_Max;

                                inst.FLAMMABILITY_W_Max = itemSpe.FLAMMABILITY_W_Max;
                                inst.FLAMMABILITY_F_Max = itemSpe.FLAMMABILITY_F_Max;

                                inst.EDGECOMB_W_Min = itemSpe.EDGECOMB_W_Min;
                                inst.EDGECOMB_F_Min = itemSpe.EDGECOMB_F_Min;

                                inst.STIFFNESS_W_Max = itemSpe.STIFFNESS_W_Max;
                                inst.STIFFNESS_F_Max = itemSpe.STIFFNESS_F_Max;

                                inst.TEAR_W_Min = itemSpe.TEAR_W_Min;
                                inst.TEAR_F_Min = itemSpe.TEAR_F_Min;

                                inst.STATIC_AIR_Max = itemSpe.STATIC_AIR_Max;

                                inst.FLEXABRASION_W_Min = itemSpe.FLEXABRASION_W_Min;
                                inst.FLEXABRASION_F_Min = itemSpe.FLEXABRASION_F_Min;

                                inst.DIMENSCHANGE_W_TOR = itemSpe.DIMENSCHANGE_W_TOR;
                                inst.DIMENSCHANGE_W_Min = itemSpe.DIMENSCHANGE_W_Min;
                                inst.DIMENSCHANGE_W_Max = itemSpe.DIMENSCHANGE_W_Max;

                                inst.DIMENSCHANGE_F_TOR = itemSpe.DIMENSCHANGE_F_TOR;
                                inst.DIMENSCHANGE_F_Min = itemSpe.DIMENSCHANGE_F_Min;
                                inst.DIMENSCHANGE_F_Max = itemSpe.DIMENSCHANGE_F_Max;

                                inst.NUMTHREADS_W_Spe = itemSpe.NUMTHREADS_W_Spe;
                                inst.NUMTHREADS_F_Spe = itemSpe.NUMTHREADS_F_Spe;

                                inst.USABLE_Spe = itemSpe.USABLE_Spe;
                                inst.WIDTH_SILICONE_Spe = itemSpe.WIDTH_SILICONE_Spe;

                                inst.TOTALWEIGHT_Spe = itemSpe.TOTALWEIGHT_Spe;
                                inst.UNCOATEDWEIGHT_Spe = itemSpe.UNCOATEDWEIGHT_Spe;
                                inst.COATINGWEIGHT_Spe = itemSpe.COATINGWEIGHT_Spe;
                                inst.MAXFORCE_W_Spe = itemSpe.MAXFORCE_W_Spe;
                                inst.MAXFORCE_F_Spe = itemSpe.MAXFORCE_F_Spe;
                                inst.ELONGATIONFORCE_W_Spe = itemSpe.ELONGATIONFORCE_W_Spe;
                                inst.ELONGATIONFORCE_F_Spe = itemSpe.ELONGATIONFORCE_F_Spe;
                                inst.FLAMMABILITY_W_Spe = itemSpe.FLAMMABILITY_W_Spe;
                                inst.FLAMMABILITY_F_Spe = itemSpe.FLAMMABILITY_F_Spe;
                                inst.EDGECOMB_W_Spe = itemSpe.EDGECOMB_W_Spe;
                                inst.EDGECOMB_F_Spe = itemSpe.EDGECOMB_F_Spe;
                                inst.STIFFNESS_W_Spe = itemSpe.STIFFNESS_W_Spe;
                                inst.STIFFNESS_F_Spe = itemSpe.STIFFNESS_F_Spe;
                                inst.TEAR_W_Spe = itemSpe.TEAR_W_Spe;
                                inst.TEAR_F_Spe = itemSpe.TEAR_F_Spe;
                                inst.STATIC_AIR_Spe = itemSpe.STATIC_AIR_Spe;
                                inst.FLEXABRASION_W_Spe = itemSpe.FLEXABRASION_W_Spe;
                                inst.FLEXABRASION_F_Spe = itemSpe.FLEXABRASION_F_Spe;
                                inst.DIMENSCHANGE_W_Spe = itemSpe.DIMENSCHANGE_W_Spe;
                                inst.DIMENSCHANGE_F_Spe = itemSpe.DIMENSCHANGE_F_Spe;

                                inst.THICKNESS_Spe = itemSpe.THICKNESS_Spe;
                                inst.THICKNESS_Min = itemSpe.THICKNESS_Min;
                                inst.THICKNESS_Max = itemSpe.THICKNESS_Max;

                                inst.BENDING_W_TOR = itemSpe.BENDING_W_TOR;
                                inst.BENDING_W_Spe = itemSpe.BENDING_W_Spe;
                                inst.BENDING_W_Min = itemSpe.BENDING_W_Min;
                                inst.BENDING_W_Max = itemSpe.BENDING_W_Max;

                                inst.BENDING_F_TOR = itemSpe.BENDING_F_TOR;
                                inst.BENDING_F_Spe = itemSpe.BENDING_F_Spe;
                                inst.BENDING_F_Min = itemSpe.BENDING_F_Min;
                                inst.BENDING_F_Max = itemSpe.BENDING_F_Max;

                                inst.DYNAMIC_AIR_Spe = itemSpe.DYNAMIC_AIR_Spe;
                                inst.DYNAMIC_AIR_Min = itemSpe.DYNAMIC_AIR_Min;
                                inst.DYNAMIC_AIR_Max = itemSpe.DYNAMIC_AIR_Max;

                                inst.EXPONENT_Spe = itemSpe.EXPONENT_Spe;
                                inst.EXPONENT_Min = itemSpe.EXPONENT_Min;
                                inst.EXPONENT_Max = itemSpe.EXPONENT_Max;

                                inst.BOW_TOR = itemSpe.BOW_TOR;
                                inst.BOW_Spe = itemSpe.BOW_Spe;
                                inst.BOW_Min = itemSpe.BOW_Min;
                                inst.BOW_Max = itemSpe.BOW_Max;

                                inst.SKEW_TOR = itemSpe.SKEW_TOR;
                                inst.SKEW_Spe = itemSpe.SKEW_Spe;
                                inst.SKEW_Min = itemSpe.SKEW_Min;
                                inst.SKEW_Max = itemSpe.SKEW_Max;

                                inst.FLEX_SCOTT_W_TOR = itemSpe.FLEX_SCOTT_W_TOR;
                                inst.FLEX_SCOTT_W_Spe = itemSpe.FLEX_SCOTT_W_Spe;
                                inst.FLEX_SCOTT_W_Min = itemSpe.FLEX_SCOTT_W_Min;
                                inst.FLEX_SCOTT_W_Max = itemSpe.FLEX_SCOTT_W_Max;

                                inst.FLEX_SCOTT_F_TOR = itemSpe.FLEX_SCOTT_F_TOR;
                                inst.FLEX_SCOTT_F_Spe = itemSpe.FLEX_SCOTT_F_Spe;
                                inst.FLEX_SCOTT_F_Min = itemSpe.FLEX_SCOTT_F_Min;
                                inst.FLEX_SCOTT_F_Max = itemSpe.FLEX_SCOTT_F_Max;
                            }
                            else
                            {
                                inst.NUMTHREADS_W_Min = 0;
                                inst.NUMTHREADS_W_Max = 0;

                                inst.NUMTHREADS_F_Min = 0;
                                inst.NUMTHREADS_F_Max = 0;

                                inst.USABLE_WIDTH_Min = 0;
                                inst.WIDTH_SILICONE_Min = 0;

                                inst.TOTALWEIGHT_Min = 0;
                                inst.TOTALWEIGHT_Max = 0;

                                inst.UNCOATEDWEIGHT_Min = 0;
                                inst.UNCOATEDWEIGHT_Max = 0;

                                inst.COATINGWEIGHT_Min = 0;
                                inst.COATINGWEIGHT_Max = 0;

                                inst.MAXFORCE_W_Min = 0;
                                inst.MAXFORCE_F_Min = 0;

                                inst.ELONGATIONFORCE_W_Min = 0;
                                inst.ELONGATIONFORCE_W_Max = 0;

                                inst.ELONGATIONFORCE_F_Min = 0;
                                inst.ELONGATIONFORCE_F_Max = 0;

                                inst.FLAMMABILITY_W_Max = 0;
                                inst.FLAMMABILITY_F_Max = 0;

                                inst.EDGECOMB_W_Min = 0;
                                inst.EDGECOMB_F_Min = 0;

                                inst.STIFFNESS_W_Max = 0;
                                inst.STIFFNESS_F_Max = 0;

                                inst.TEAR_W_Min = 0;
                                inst.TEAR_F_Min = 0;

                                inst.STATIC_AIR_Max = 0;

                                inst.FLEXABRASION_W_Min = 0;
                                inst.FLEXABRASION_F_Min = 0;

                                inst.DIMENSCHANGE_W_Min = 0;
                                inst.DIMENSCHANGE_W_Max = 0;

                                inst.DIMENSCHANGE_F_Min = 0;
                                inst.DIMENSCHANGE_F_Max = 0;

                                inst.THICKNESS_Min = 0;
                                inst.THICKNESS_Max = 0;

                                inst.BENDING_W_Min = 0;
                                inst.BENDING_W_Max = 0;

                                inst.BENDING_F_Min = 0;
                                inst.BENDING_F_Max = 0;

                                inst.DYNAMIC_AIR_Min = 0;
                                inst.DYNAMIC_AIR_Max = 0;

                                inst.EXPONENT_Min = 0;
                                inst.EXPONENT_Max = 0;

                                inst.BOW_Min = 0;
                                inst.BOW_Max = 0;

                                inst.SKEW_Min = 0;
                                inst.SKEW_Max = 0;

                                inst.FLEX_SCOTT_W_Min = 0;
                                inst.FLEX_SCOTT_W_Max = 0;

                                inst.FLEX_SCOTT_F_Min = 0;
                                inst.FLEX_SCOTT_F_Max = 0;
                            }
                            #endregion

                            inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                            inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                            inst.CUSTOMERID = dbResult.CUSTOMERID;
                            inst.FINISHINGPROCESS = dbResult.FINISHINGPROCESS;
                            inst.ITEMLOT = dbResult.ITEMLOT;
                            inst.LOOMNO = dbResult.LOOMNO;
                            inst.FINISHINGMC = dbResult.FINISHINGMC;
                            inst.BATCHNO = dbResult.BATCHNO;
                            inst.ENTRYDATE = dbResult.ENTRYDATE;

                            #region NUMTHREADS_W
                            inst.NUMTHREADS_W_AVG = CheckAVG(dbResult.NUMTHREADS_W1, dbResult.NUMTHREADS_W2, dbResult.NUMTHREADS_W3, null, null, null);

                            if (inst.NUMTHREADS_W_AVG != null && inst.NUMTHREADS_W_Min != null && inst.NUMTHREADS_W_Max != null)
                            {
                                if (Between(inst.NUMTHREADS_W_AVG, inst.NUMTHREADS_W_Min, inst.NUMTHREADS_W_Max) == true)
                                    inst.NUMTHREADS_W_JUD = "OK";
                                else
                                {
                                    inst.NUMTHREADS_W_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.NUMTHREADS_W_JUD = "OK";
                            }
                            #endregion

                            #region NUMTHREADS_F
                            inst.NUMTHREADS_F_AVG = CheckAVG(dbResult.NUMTHREADS_F1, dbResult.NUMTHREADS_F2, dbResult.NUMTHREADS_F3, null, null, null);

                            if (inst.NUMTHREADS_F_AVG != null && inst.NUMTHREADS_F_Min != null && inst.NUMTHREADS_F_Max != null)
                            {
                                if (Between(inst.NUMTHREADS_F_AVG, inst.NUMTHREADS_F_Min, inst.NUMTHREADS_F_Max) == true)
                                    inst.NUMTHREADS_F_JUD = "OK";
                                else
                                {
                                    inst.NUMTHREADS_F_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.NUMTHREADS_F_JUD = "OK";
                            }
                            #endregion

                            #region USABLE_WIDTH
                            inst.USABLE_WIDTH_AVG = CheckAVG(dbResult.USABLE_WIDTH1, dbResult.USABLE_WIDTH2, dbResult.USABLE_WIDTH3, null, null, null);

                            if (inst.USABLE_WIDTH_AVG != null && inst.USABLE_WIDTH_Min != null)
                            {
                                if (Min(inst.USABLE_WIDTH_AVG, inst.USABLE_WIDTH_Min) == true)
                                    inst.USABLE_WIDTH_JUD = "OK";
                                else
                                {
                                    inst.USABLE_WIDTH_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.USABLE_WIDTH_JUD = "OK";
                            }
                            #endregion

                            #region WIDTH_SILICONE
                            inst.WIDTH_SILICONE_AVG = CheckAVG(dbResult.WIDTH_SILICONE1, dbResult.WIDTH_SILICONE2, dbResult.WIDTH_SILICONE3, null, null, null);

                            if (inst.WIDTH_SILICONE_AVG != null && inst.WIDTH_SILICONE_Min != null)
                            {
                                if (Min(inst.WIDTH_SILICONE_AVG, inst.WIDTH_SILICONE_Min) == true)
                                    inst.WIDTH_SILICONE_JUD = "OK";
                                else
                                {
                                    inst.WIDTH_SILICONE_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.WIDTH_SILICONE_JUD = "OK";
                            }
                            #endregion

                            #region TOTALWEIGHT
                            inst.TOTALWEIGHT_AVG = CheckAVG(dbResult.TOTALWEIGHT1, dbResult.TOTALWEIGHT2, dbResult.TOTALWEIGHT3, dbResult.TOTALWEIGHT4, dbResult.TOTALWEIGHT5, dbResult.TOTALWEIGHT6);

                            if (inst.TOTALWEIGHT_AVG != null && inst.TOTALWEIGHT_Min != null && inst.TOTALWEIGHT_Max != null)
                            {
                                if (Between(inst.TOTALWEIGHT_AVG, inst.TOTALWEIGHT_Min, inst.TOTALWEIGHT_Max) == true)
                                    inst.TOTALWEIGHT_JUD = "OK";
                                else
                                {
                                    inst.TOTALWEIGHT_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.TOTALWEIGHT_JUD = "OK";
                            }
                            #endregion

                            #region UNCOATEDWEIGHT
                            inst.UNCOATEDWEIGHT_AVG = CheckAVG(dbResult.UNCOATEDWEIGHT1, dbResult.UNCOATEDWEIGHT2, dbResult.UNCOATEDWEIGHT3, dbResult.UNCOATEDWEIGHT4, dbResult.UNCOATEDWEIGHT5, dbResult.UNCOATEDWEIGHT6);

                            if (inst.UNCOATEDWEIGHT_AVG != null && inst.UNCOATEDWEIGHT_Min != null && inst.UNCOATEDWEIGHT_Max != null)
                            {
                                if (Between(inst.UNCOATEDWEIGHT_AVG, inst.UNCOATEDWEIGHT_Min, inst.UNCOATEDWEIGHT_Max) == true)
                                    inst.UNCOATEDWEIGHT_JUD = "OK";
                                else
                                {
                                    inst.UNCOATEDWEIGHT_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.UNCOATEDWEIGHT_JUD = "OK";
                            }
                            #endregion

                            #region COATINGWEIGHT
                            inst.COATINGWEIGHT_AVG = CheckAVG(dbResult.COATINGWEIGHT1, dbResult.COATINGWEIGHT2, dbResult.COATINGWEIGHT3, dbResult.COATINGWEIGHT4, dbResult.COATINGWEIGHT5, dbResult.COATINGWEIGHT6);

                            if (inst.COATINGWEIGHT_AVG != null && inst.COATINGWEIGHT_Min != null && inst.COATINGWEIGHT_Max != null)
                            {
                                if (Between(inst.COATINGWEIGHT_AVG, inst.COATINGWEIGHT_Min, inst.COATINGWEIGHT_Max) == true)
                                    inst.COATINGWEIGHT_JUD = "OK";
                                else
                                {
                                    inst.COATINGWEIGHT_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.COATINGWEIGHT_JUD = "OK";
                            }
                            #endregion

                            #region MAXFORCE_W
                            inst.MAXFORCE_W_AVG = CheckAVG(dbResult.MAXFORCE_W1, dbResult.MAXFORCE_W2, dbResult.MAXFORCE_W3, null, null, null);

                            if (inst.MAXFORCE_W_AVG != null && inst.MAXFORCE_W_Min != null)
                            {
                                if (Min(inst.MAXFORCE_W_AVG, inst.MAXFORCE_W_Min) == true)
                                    inst.MAXFORCE_W_JUD = "OK";
                                else
                                {
                                    inst.MAXFORCE_W_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.MAXFORCE_W_JUD = "OK";
                            }
                            #endregion

                            #region MAXFORCE_F
                            inst.MAXFORCE_F_AVG = CheckAVG(dbResult.MAXFORCE_F1, dbResult.MAXFORCE_F2, dbResult.MAXFORCE_F3, null, null, null);

                            if (inst.MAXFORCE_F_AVG != null && inst.MAXFORCE_F_Min != null)
                            {
                                if (Min(inst.MAXFORCE_F_AVG, inst.MAXFORCE_F_Min) == true)
                                    inst.MAXFORCE_F_JUD = "OK";
                                else
                                {
                                    inst.MAXFORCE_F_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.MAXFORCE_F_JUD = "OK";
                            }
                            #endregion

                            #region ELONGATIONFORCE_W
                            inst.ELONGATIONFORCE_W_AVG = CheckAVG(dbResult.ELONGATIONFORCE_W1, dbResult.ELONGATIONFORCE_W2, dbResult.ELONGATIONFORCE_W3, null, null, null);

                            if (inst.ELONGATIONFORCE_W_AVG != null)
                            {
                                if (!string.IsNullOrEmpty(inst.ELONGATIONFORCE_W_TOR))
                                {
                                    if (inst.ELONGATIONFORCE_W_TOR.Contains("MAX"))
                                    {
                                        if (inst.ELONGATIONFORCE_W_Max != null)
                                        {
                                            if (Max(inst.ELONGATIONFORCE_W_AVG, inst.ELONGATIONFORCE_W_Max) == true)
                                                inst.ELONGATIONFORCE_W_JUD = "OK";
                                            else
                                            {
                                                inst.ELONGATIONFORCE_W_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.ELONGATIONFORCE_W_JUD = "OK";
                                        }
                                    }
                                    else if (inst.ELONGATIONFORCE_W_TOR.Contains("MIN"))
                                    {
                                        if (inst.ELONGATIONFORCE_W_Min != null)
                                        {
                                            if (Min(inst.ELONGATIONFORCE_W_AVG, inst.ELONGATIONFORCE_W_Min) == true)
                                                inst.ELONGATIONFORCE_W_JUD = "OK";
                                            else
                                            {
                                                inst.ELONGATIONFORCE_W_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.ELONGATIONFORCE_W_JUD = "OK";
                                        }
                                    }
                                    else
                                    {
                                        if (inst.ELONGATIONFORCE_W_Min != null && inst.ELONGATIONFORCE_W_Max != null)
                                        {
                                            if (Between(inst.ELONGATIONFORCE_W_AVG, inst.ELONGATIONFORCE_W_Min, inst.ELONGATIONFORCE_W_Max) == true)
                                                inst.ELONGATIONFORCE_W_JUD = "OK";
                                            else
                                            {
                                                inst.ELONGATIONFORCE_W_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.ELONGATIONFORCE_W_JUD = "OK";
                                        }
                                    }
                                }
                                else
                                {
                                    //inst.ELONGATIONFORCE_W_JUD = "FAIL";
                                    //i++;
                                    inst.ELONGATIONFORCE_W_JUD = "OK";
                                }
                            }
                            else
                            {
                                inst.ELONGATIONFORCE_W_JUD = "OK";
                            }
                            #endregion

                            #region ELONGATIONFORCE_F
                            inst.ELONGATIONFORCE_F_AVG = CheckAVG(dbResult.ELONGATIONFORCE_F1, dbResult.ELONGATIONFORCE_F2, dbResult.ELONGATIONFORCE_F3, null, null, null);

                            if (inst.ELONGATIONFORCE_F_AVG != null)
                            {
                                if (!string.IsNullOrEmpty(inst.ELONGATIONFORCE_F_TOR))
                                {
                                    if (inst.ELONGATIONFORCE_F_TOR.Contains("MAX"))
                                    {
                                        if (inst.ELONGATIONFORCE_F_Max != null)
                                        {
                                            if (Max(inst.ELONGATIONFORCE_F_AVG, inst.ELONGATIONFORCE_F_Max) == true)
                                                inst.ELONGATIONFORCE_F_JUD = "OK";
                                            else
                                            {
                                                inst.ELONGATIONFORCE_F_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.ELONGATIONFORCE_F_JUD = "OK";
                                        }
                                    }
                                    else if (inst.ELONGATIONFORCE_F_TOR.Contains("MIN"))
                                    {
                                        if (inst.ELONGATIONFORCE_F_Min != null)
                                        {
                                            if (Min(inst.ELONGATIONFORCE_F_AVG, inst.ELONGATIONFORCE_F_Min) == true)
                                                inst.ELONGATIONFORCE_F_JUD = "OK";
                                            else
                                            {
                                                inst.ELONGATIONFORCE_F_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.ELONGATIONFORCE_F_JUD = "OK";
                                        }
                                    }
                                    else
                                    {
                                        if (inst.ELONGATIONFORCE_F_Min != null && inst.ELONGATIONFORCE_F_Max != null)
                                        {
                                            if (Between(inst.ELONGATIONFORCE_F_AVG, inst.ELONGATIONFORCE_F_Min, inst.ELONGATIONFORCE_F_Max) == true)
                                                inst.ELONGATIONFORCE_F_JUD = "OK";
                                            else
                                            {
                                                inst.ELONGATIONFORCE_F_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.ELONGATIONFORCE_F_JUD = "OK";
                                        }
                                    }
                                }
                                else
                                {
                                    inst.ELONGATIONFORCE_F_JUD = "OK";
                                    //inst.ELONGATIONFORCE_F_JUD = "FAIL";
                                    //i++;
                                }
                            }
                            else
                            {
                                inst.ELONGATIONFORCE_F_JUD = "OK";
                            }
                            #endregion

                            #region FLAMMABILITY_W
                            if (inst.CUSTOMERID == "01")
                            {
                                inst.FLAMMABILITY_W = CheckMax(dbResult.FLAMMABILITY_W, dbResult.FLAMMABILITY_W2, dbResult.FLAMMABILITY_W3, dbResult.FLAMMABILITY_W4, dbResult.FLAMMABILITY_W5, null);
                            }
                            else
                            {
                                inst.FLAMMABILITY_W = CheckAVG(dbResult.FLAMMABILITY_W, dbResult.FLAMMABILITY_W2, dbResult.FLAMMABILITY_W3, dbResult.FLAMMABILITY_W4, dbResult.FLAMMABILITY_W5, null);
                            }
                            //inst.FLAMMABILITY_W = dbResult.FLAMMABILITY_W;

                            if (inst.FLAMMABILITY_W != null && inst.FLAMMABILITY_W_Max != null)
                            {
                                if (Max(inst.FLAMMABILITY_W, inst.FLAMMABILITY_W_Max) == true)
                                    inst.FLAMMABILITY_W_JUD = "OK";
                                else
                                {
                                    inst.FLAMMABILITY_W_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.FLAMMABILITY_W_JUD = "OK";
                            }
                            #endregion

                            #region FLAMMABILITY_F
                            if (inst.CUSTOMERID == "01")
                            {
                                inst.FLAMMABILITY_F = CheckMax(dbResult.FLAMMABILITY_F, dbResult.FLAMMABILITY_F2, dbResult.FLAMMABILITY_F3, dbResult.FLAMMABILITY_F4, dbResult.FLAMMABILITY_F5, null);
                            }
                            else
                            {
                                inst.FLAMMABILITY_F = CheckAVG(dbResult.FLAMMABILITY_F, dbResult.FLAMMABILITY_F2, dbResult.FLAMMABILITY_F3, dbResult.FLAMMABILITY_F4, dbResult.FLAMMABILITY_F5, null);
                            }
                            //inst.FLAMMABILITY_F = dbResult.FLAMMABILITY_F;

                            if (inst.FLAMMABILITY_F != null && inst.FLAMMABILITY_F_Max != null)
                            {
                                if (Max(inst.FLAMMABILITY_F, inst.FLAMMABILITY_F_Max) == true)
                                    inst.FLAMMABILITY_F_JUD = "OK";
                                else
                                {
                                    inst.FLAMMABILITY_F_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.FLAMMABILITY_F_JUD = "OK";
                            }
                            #endregion

                            #region EDGECOMB_W
                            inst.EDGECOMB_W_AVG = CheckAVG(dbResult.EDGECOMB_W1, dbResult.EDGECOMB_W2, dbResult.EDGECOMB_W3, null, null, null);

                            if (inst.EDGECOMB_W_AVG != null && inst.EDGECOMB_W_Min != null)
                            {
                                if (Min(inst.EDGECOMB_W_AVG, inst.EDGECOMB_W_Min) == true)
                                    inst.EDGECOMB_W_JUD = "OK";
                                else
                                {
                                    inst.EDGECOMB_W_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.EDGECOMB_W_JUD = "OK";
                            }
                            #endregion

                            #region EDGECOMB_F
                            inst.EDGECOMB_F_AVG = CheckAVG(dbResult.EDGECOMB_F1, dbResult.EDGECOMB_F2, dbResult.EDGECOMB_F3, null, null, null);

                            if (inst.EDGECOMB_F_AVG != null && inst.EDGECOMB_F_Min != null)
                            {
                                if (Min(inst.EDGECOMB_F_AVG, inst.EDGECOMB_F_Min) == true)
                                    inst.EDGECOMB_F_JUD = "OK";
                                else
                                {
                                    inst.EDGECOMB_F_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.EDGECOMB_F_JUD = "OK";
                            }
                            #endregion

                            #region STIFFNESS_W
                            inst.STIFFNESS_W_AVG = CheckAVG(dbResult.STIFFNESS_W1, dbResult.STIFFNESS_W2, dbResult.STIFFNESS_W3, null, null, null);

                            if (inst.STIFFNESS_W_AVG != null && inst.STIFFNESS_W_Max != null)
                            {
                                if (Max(inst.STIFFNESS_W_AVG, inst.STIFFNESS_W_Max) == true)
                                    inst.STIFFNESS_W_JUD = "OK";
                                else
                                {
                                    inst.STIFFNESS_W_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.STIFFNESS_W_JUD = "OK";
                            }
                            #endregion

                            #region STIFFNESS_F
                            inst.STIFFNESS_F_AVG = CheckAVG(dbResult.STIFFNESS_F1, dbResult.STIFFNESS_F2, dbResult.STIFFNESS_F3, null, null, null);

                            if (inst.STIFFNESS_F_AVG != null && inst.STIFFNESS_F_Max != null)
                            {
                                if (Max(inst.STIFFNESS_F_AVG, inst.STIFFNESS_F_Max) == true)
                                    inst.STIFFNESS_F_JUD = "OK";
                                else
                                {
                                    inst.STIFFNESS_F_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.STIFFNESS_F_JUD = "OK";
                            }
                            #endregion

                            #region TEAR_W
                            inst.TEAR_W_AVG = CheckAVG(dbResult.TEAR_W1, dbResult.TEAR_W2, dbResult.TEAR_W3, null, null, null);

                            if (inst.TEAR_W_AVG != null && inst.TEAR_W_Min != null)
                            {
                                if (Min(inst.TEAR_W_AVG, inst.TEAR_W_Min) == true)
                                    inst.TEAR_W_JUD = "OK";
                                else
                                {
                                    inst.TEAR_W_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.TEAR_W_JUD = "OK";
                            }
                            #endregion

                            #region TEAR_F
                            inst.TEAR_F_AVG = CheckAVG(dbResult.TEAR_F1, dbResult.TEAR_F2, dbResult.TEAR_F3, null, null, null);

                            if (inst.TEAR_F_AVG != null && inst.TEAR_F_Min != null)
                            {
                                if (Min(inst.TEAR_F_AVG, inst.TEAR_F_Min) == true)
                                    inst.TEAR_F_JUD = "OK";
                                else
                                {
                                    inst.TEAR_F_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.TEAR_F_JUD = "OK";
                            }
                            #endregion

                            #region STATIC_AIR
                            inst.STATIC_AIR_AVG = CheckAVG(dbResult.STATIC_AIR1, dbResult.STATIC_AIR2, dbResult.STATIC_AIR3, null, null, null);

                            if (inst.STATIC_AIR_AVG != null && inst.STATIC_AIR_Max != null)
                            {
                                if (Max(inst.STATIC_AIR_AVG, inst.STATIC_AIR_Max) == true)
                                    inst.STATIC_AIR_JUD = "OK";
                                else
                                {
                                    inst.STATIC_AIR_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.STATIC_AIR_JUD = "OK";
                            }
                            #endregion

                            #region FLEXABRASION_W
                            inst.FLEXABRASION_W_AVG = CheckAVG(dbResult.FLEXABRASION_W1, dbResult.FLEXABRASION_W2, dbResult.FLEXABRASION_W3, null, null, null);

                            if (inst.FLEXABRASION_W_AVG != null && inst.FLEXABRASION_W_Min != null)
                            {
                                if (Min(inst.FLEXABRASION_W_AVG, inst.FLEXABRASION_W_Min) == true)
                                    inst.FLEXABRASION_W_JUD = "PASS";
                                else
                                {
                                    inst.FLEXABRASION_W_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.FLEXABRASION_W_JUD = "PASS";
                            }
                            #endregion

                            #region FLEXABRASION_F
                            inst.FLEXABRASION_F_AVG = CheckAVG(dbResult.FLEXABRASION_F1, dbResult.FLEXABRASION_F2, dbResult.FLEXABRASION_F3, null, null, null);

                            if (inst.FLEXABRASION_F_AVG != null && inst.FLEXABRASION_F_Min != null)
                            {
                                if (Min(inst.FLEXABRASION_F_AVG, inst.FLEXABRASION_F_Min) == true)
                                    inst.FLEXABRASION_F_JUD = "PASS";
                                else
                                {
                                    inst.FLEXABRASION_F_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.FLEXABRASION_F_JUD = "PASS";
                            }
                            #endregion

                            #region DIMENSCHANGE_W
                            inst.DIMENSCHANGE_W_AVG = CheckAVG(dbResult.DIMENSCHANGE_W1, dbResult.DIMENSCHANGE_W2, dbResult.DIMENSCHANGE_W3, null, null, null);

                            if (inst.DIMENSCHANGE_W_AVG != null)
                            {
                                if (!string.IsNullOrEmpty(inst.DIMENSCHANGE_W_TOR))
                                {
                                    if (inst.DIMENSCHANGE_W_TOR.Contains("MAX"))
                                    {
                                        if (inst.DIMENSCHANGE_W_Max != null)
                                        {
                                            if (Max(inst.DIMENSCHANGE_W_AVG, inst.DIMENSCHANGE_W_Max) == true)
                                                inst.DIMENSCHANGE_W_JUD = "OK";
                                            else
                                            {
                                                inst.DIMENSCHANGE_W_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.DIMENSCHANGE_W_JUD = "OK";
                                        }
                                    }
                                    else if (inst.DIMENSCHANGE_W_TOR.Contains("MIN"))
                                    {
                                        if (inst.DIMENSCHANGE_W_Min != null)
                                        {
                                            if (Min(inst.DIMENSCHANGE_W_AVG, inst.DIMENSCHANGE_W_Min) == true)
                                                inst.DIMENSCHANGE_W_JUD = "OK";
                                            else
                                            {
                                                inst.DIMENSCHANGE_W_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.DIMENSCHANGE_W_JUD = "OK";
                                        }
                                    }
                                    else
                                    {
                                        if (inst.DIMENSCHANGE_W_Min != null && inst.DIMENSCHANGE_W_Max != null)
                                        {
                                            if (Between(inst.DIMENSCHANGE_W_AVG, inst.DIMENSCHANGE_W_Min, inst.DIMENSCHANGE_W_Max) == true)
                                                inst.DIMENSCHANGE_W_JUD = "OK";
                                            else
                                            {
                                                inst.DIMENSCHANGE_W_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.DIMENSCHANGE_W_JUD = "OK";
                                        }
                                    }
                                }
                                else
                                {
                                    inst.DIMENSCHANGE_W_JUD = "OK";
                                    //inst.DIMENSCHANGE_W_JUD = "FAIL";
                                    //i++;
                                }
                            }
                            else
                            {
                                inst.DIMENSCHANGE_W_JUD = "OK";
                            }
                            #endregion

                            #region DIMENSCHANGE_F
                            inst.DIMENSCHANGE_F_AVG = CheckAVG(dbResult.DIMENSCHANGE_F1, dbResult.DIMENSCHANGE_F2, dbResult.DIMENSCHANGE_F3, null, null, null);

                            if (inst.DIMENSCHANGE_F_AVG != null)
                            {
                                if (!string.IsNullOrEmpty(inst.DIMENSCHANGE_F_TOR))
                                {
                                    if (inst.DIMENSCHANGE_F_TOR.Contains("MAX"))
                                    {
                                        if (inst.DIMENSCHANGE_F_Max != null)
                                        {
                                            if (Max(inst.DIMENSCHANGE_F_AVG, inst.DIMENSCHANGE_F_Max) == true)
                                                inst.DIMENSCHANGE_F_JUD = "OK";
                                            else
                                            {
                                                inst.DIMENSCHANGE_F_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.DIMENSCHANGE_F_JUD = "OK";
                                        }
                                    }
                                    else if (inst.DIMENSCHANGE_F_TOR.Contains("MIN"))
                                    {
                                        if (inst.DIMENSCHANGE_F_Min != null)
                                        {
                                            if (Min(inst.DIMENSCHANGE_F_AVG, inst.DIMENSCHANGE_F_Min) == true)
                                                inst.DIMENSCHANGE_F_JUD = "OK";
                                            else
                                            {
                                                inst.DIMENSCHANGE_F_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.DIMENSCHANGE_F_JUD = "OK";
                                        }
                                    }
                                    else
                                    {
                                        if (inst.DIMENSCHANGE_F_Min != null && inst.DIMENSCHANGE_F_Max != null)
                                        {
                                            if (Between(inst.DIMENSCHANGE_F_AVG, inst.DIMENSCHANGE_F_Min, inst.DIMENSCHANGE_F_Max) == true)
                                                inst.DIMENSCHANGE_F_JUD = "OK";
                                            else
                                            {
                                                inst.DIMENSCHANGE_F_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.DIMENSCHANGE_F_JUD = "OK";
                                        }
                                    }
                                }
                                else
                                {
                                    //inst.DIMENSCHANGE_F_JUD = "FAIL";
                                    //i++;
                                    inst.DIMENSCHANGE_F_JUD = "OK";
                                }
                            }
                            else
                            {
                                inst.DIMENSCHANGE_F_JUD = "OK";
                            }
                            #endregion

                            if (i > 0)
                                inst.JUDGEMENT = "FAILED";
                            else
                                inst.JUDGEMENT = "PASSED";

                            // Update 26/10/20
                            inst.FILENAME = dbResult.FILENAME;
                            inst.UPLOADDATE = dbResult.UPLOADDATE;
                            inst.UPLOADBY = dbResult.UPLOADBY;

                            #region THICKNESS
                            inst.THICKNESS_AVG = CheckAVG(dbResult.THICKNESS1, dbResult.THICKNESS2, dbResult.THICKNESS3, null, null, null);

                            if (inst.THICKNESS_AVG != null && inst.THICKNESS_Min != null && inst.THICKNESS_Max != null)
                            {
                                if (Between(inst.THICKNESS_AVG, inst.THICKNESS_Min, inst.THICKNESS_Max) == true)
                                    inst.THICKNESS_JUD = "OK";
                                else
                                {
                                    inst.THICKNESS_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.THICKNESS_JUD = "OK";
                            }
                            #endregion

                            #region BENDING_W
                            inst.BENDING_W_AVG = CheckAVG(dbResult.BENDING_W1, dbResult.BENDING_W2, dbResult.BENDING_W3, null, null, null);

                            if (inst.BENDING_W_AVG != null)
                            {
                                if (!string.IsNullOrEmpty(inst.BENDING_W_TOR))
                                {
                                    if (inst.BENDING_W_TOR.Contains("MAX"))
                                    {
                                        if (inst.BENDING_W_Max != null)
                                        {
                                            if (Max(inst.BENDING_W_AVG, inst.BENDING_W_Max) == true)
                                                inst.BENDING_W_JUD = "OK";
                                            else
                                            {
                                                inst.BENDING_W_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.BENDING_W_JUD = "OK";
                                        }
                                    }
                                    else if (inst.BENDING_W_TOR.Contains("MIN"))
                                    {
                                        if (inst.BENDING_W_Min != null)
                                        {
                                            if (Min(inst.BENDING_W_AVG, inst.BENDING_W_Min) == true)
                                                inst.BENDING_W_JUD = "OK";
                                            else
                                            {
                                                inst.BENDING_W_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.BENDING_W_JUD = "OK";
                                        }
                                    }
                                    else
                                    {
                                        if (inst.BENDING_W_Min != null && inst.BENDING_W_Max != null)
                                        {
                                            if (Between(inst.BENDING_W_AVG, inst.BENDING_W_Min, inst.BENDING_W_Max) == true)
                                                inst.BENDING_W_JUD = "OK";
                                            else
                                            {
                                                inst.BENDING_W_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.BENDING_W_JUD = "OK";
                                        }
                                    }
                                }
                                else
                                {
                                    inst.BENDING_W_JUD = "OK";
                                    //i++;
                                }
                            }
                            else
                            {
                                inst.BENDING_W_JUD = "OK";
                            }
                            #endregion

                            #region BENDING_F
                            inst.BENDING_F_AVG = CheckAVG(dbResult.BENDING_F1, dbResult.BENDING_F2, dbResult.BENDING_F3, null, null, null);

                            if (inst.BENDING_F_AVG != null)
                            {
                                if (!string.IsNullOrEmpty(inst.BENDING_F_TOR))
                                {
                                    if (inst.BENDING_F_TOR.Contains("MAX"))
                                    {
                                        if (inst.BENDING_F_Max != null)
                                        {
                                            if (Max(inst.BENDING_F_AVG, inst.BENDING_F_Max) == true)
                                                inst.BENDING_F_JUD = "OK";
                                            else
                                            {
                                                inst.BENDING_F_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.BENDING_F_JUD = "OK";
                                        }
                                    }
                                    else if (inst.BENDING_F_TOR.Contains("MIN"))
                                    {
                                        if (inst.BENDING_F_Min != null)
                                        {
                                            if (Min(inst.BENDING_F_AVG, inst.BENDING_F_Min) == true)
                                                inst.BENDING_F_JUD = "OK";
                                            else
                                            {
                                                inst.BENDING_F_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.BENDING_F_JUD = "OK";
                                        }
                                    }
                                    else
                                    {
                                        if (inst.BENDING_F_Min != null && inst.BENDING_F_Max != null)
                                        {
                                            if (Between(inst.BENDING_F_AVG, inst.BENDING_F_Min, inst.BENDING_F_Max) == true)
                                                inst.BENDING_F_JUD = "OK";
                                            else
                                            {
                                                inst.BENDING_F_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.BENDING_F_JUD = "OK";
                                        }
                                    }
                                }
                                else
                                {
                                    inst.BENDING_F_JUD = "OK";
                                    //i++;
                                }
                            }
                            else
                            {
                                inst.BENDING_F_JUD = "OK";
                            }
                            #endregion

                            #region DYNAMIC_AIR
                            inst.DYNAMIC_AIR_AVG = CheckAVG(dbResult.DYNAMIC_AIR1, dbResult.DYNAMIC_AIR2, dbResult.DYNAMIC_AIR3, null, null, null);

                            if (inst.DYNAMIC_AIR_AVG != null && inst.DYNAMIC_AIR_Min != null && inst.DYNAMIC_AIR_Max != null)
                            {
                                if (Between(inst.DYNAMIC_AIR_AVG, inst.DYNAMIC_AIR_Min, inst.DYNAMIC_AIR_Max) == true)
                                    inst.DYNAMIC_AIR_JUD = "OK";
                                else
                                {
                                    inst.DYNAMIC_AIR_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.DYNAMIC_AIR_JUD = "OK";
                            }
                            #endregion

                            #region EXPONENT
                            inst.EXPONENT_AVG = CheckAVG(dbResult.EXPONENT1, dbResult.EXPONENT2, dbResult.EXPONENT3, null, null, null);

                            if (inst.EXPONENT_AVG != null && inst.EXPONENT_Min != null && inst.EXPONENT_Max != null)
                            {
                                if (Between(inst.EXPONENT_AVG, inst.EXPONENT_Min, inst.EXPONENT_Max) == true)
                                    inst.EXPONENT_JUD = "OK";
                                else
                                {
                                    inst.EXPONENT_JUD = "FAIL";
                                    i++;
                                }
                            }
                            else
                            {
                                inst.EXPONENT_JUD = "OK";
                            }
                            #endregion

                            #region LAB_GETREPORTINFO
                            reportInfo = new LAB_GETREPORTINFO_Rep();
                            reportInfo = LAB_GETREPORTINFO(dbResult.ITM_CODE, dbResult.CUSTOMERID);
                            if (null != reportInfo)
                            {
                                inst.REPORT_ID = reportInfo.REPORT_ID;
                                inst.REVESION = reportInfo.REVESION;
                                inst.USABLE_WIDTH = reportInfo.USABLE_WIDTH;

                                inst.NUMTHREADS = reportInfo.NUMTHREADS;
                                inst.WEIGHT = reportInfo.WEIGHT;                                
                                inst.FLAMMABILITY = reportInfo.FLAMMABILITY;
                                inst.EDGECOMB = reportInfo.EDGECOMB;
                                inst.STIFFNESS = reportInfo.STIFFNESS;
                                inst.TEAR = reportInfo.TEAR;
                                inst.STATIC_AIR = reportInfo.STATIC_AIR;
                                inst.FLEXABRASION = reportInfo.FLEXABRASION;
                                inst.DIMENSCHANGE = reportInfo.DIMENSCHANGE;
                                inst.EFFECTIVE_DATE = reportInfo.EFFECTIVE_DATE;

                                // ปรับเพิ่ม
                                inst.YARNTYPE = reportInfo.YARNTYPE;
                                inst.COATWEIGHT = reportInfo.COATWEIGHT;
                                inst.THICKNESS = reportInfo.THICKNESS;
                                inst.MAXFORCE = reportInfo.MAXFORCE;
                                inst.ELONGATIONFORCE = reportInfo.ELONGATIONFORCE;
                                inst.DYNAMIC_AIR = reportInfo.DYNAMIC_AIR;
                                inst.EXPONENT = reportInfo.EXPONENT;

                                inst.BOW = reportInfo.BOW;
                                inst.SKEW = reportInfo.SKEW;
                                inst.FLEX_SCOTT = reportInfo.FLEX_SCOTT;

                                inst.REPORT_NAME = reportInfo.REPORT_NAME;

                                inst.BENDING = reportInfo.BENDING;
                            }

                            #endregion

                            #region BOW
                            inst.BOW1 = dbResult.BOW1;
                            inst.BOW2 = dbResult.BOW2;
                            inst.BOW3 = dbResult.BOW3;

                            inst.BOW_AVG = CheckAVG(dbResult.BOW1, dbResult.BOW2, dbResult.BOW3, null, null, null);

                            if (inst.BOW_AVG != null)
                            {
                                if (!string.IsNullOrEmpty(inst.BOW_TOR))
                                {
                                    if (inst.BOW_TOR.Contains("MAX"))
                                    {
                                        if (inst.BOW_Max != null)
                                        {
                                            if (Max(inst.BOW_AVG, inst.BOW_Max) == true)
                                                inst.BOW_JUD = "OK";
                                            else
                                            {
                                                inst.BOW_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.BOW_JUD = "OK";
                                        }
                                    }
                                    else if (inst.BOW_TOR.Contains("MIN"))
                                    {
                                        if (inst.BOW_Min != null)
                                        {
                                            if (Min(inst.BOW_AVG, inst.BOW_Min) == true)
                                                inst.BOW_JUD = "OK";
                                            else
                                            {
                                                inst.BOW_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.BOW_JUD = "OK";
                                        }
                                    }
                                    else
                                    {
                                        if (inst.BOW_Min != null && inst.BOW_Max != null)
                                        {
                                            if (Between(inst.BOW_AVG, inst.BOW_Min, inst.BOW_Max) == true)
                                                inst.BOW_JUD = "OK";
                                            else
                                            {
                                                inst.BOW_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.BOW_JUD = "OK";
                                        }
                                    }
                                }
                                else
                                {
                                    inst.BOW_JUD = "OK";
                                    //inst.DIMENSCHANGE_W_JUD = "FAIL";
                                    //i++;
                                }
                            }
                            else
                            {
                                inst.BOW_JUD = "OK";
                            }
                            #endregion

                            #region SKEW
                            inst.SKEW1 = dbResult.SKEW1;
                            inst.SKEW2 = dbResult.SKEW2;
                            inst.SKEW3 = dbResult.SKEW3;

                            inst.SKEW_AVG = CheckAVG(dbResult.SKEW1, dbResult.SKEW2, dbResult.SKEW3, null, null, null);

                            if (inst.SKEW_AVG != null)
                            {
                                if (!string.IsNullOrEmpty(inst.SKEW_TOR))
                                {
                                    if (inst.SKEW_TOR.Contains("MAX"))
                                    {
                                        if (inst.SKEW_Max != null)
                                        {
                                            if (Max(inst.SKEW_AVG, inst.SKEW_Max) == true)
                                                inst.SKEW_JUD = "OK";
                                            else
                                            {
                                                inst.SKEW_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.SKEW_JUD = "OK";
                                        }
                                    }
                                    else if (inst.SKEW_TOR.Contains("MIN"))
                                    {
                                        if (inst.SKEW_Min != null)
                                        {
                                            if (Min(inst.SKEW_AVG, inst.SKEW_Min) == true)
                                                inst.SKEW_JUD = "OK";
                                            else
                                            {
                                                inst.SKEW_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.SKEW_JUD = "OK";
                                        }
                                    }
                                    else
                                    {
                                        if (inst.SKEW_Min != null && inst.SKEW_Max != null)
                                        {
                                            if (Between(inst.SKEW_AVG, inst.SKEW_Min, inst.SKEW_Max) == true)
                                                inst.SKEW_JUD = "OK";
                                            else
                                            {
                                                inst.SKEW_JUD = "FAIL";
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            inst.SKEW_JUD = "OK";
                                        }
                                    }
                                }
                                else
                                {
                                    inst.SKEW_JUD = "OK";
                                    //inst.DIMENSCHANGE_W_JUD = "FAIL";
                                    //i++;
                                }
                            }
                            else
                            {
                                inst.SKEW_JUD = "OK";
                            }
                            #endregion

                            #region FLEX_SCOTT_W
                            inst.FLEX_SCOTT_W1 = dbResult.FLEX_SCOTT_W1;
                            inst.FLEX_SCOTT_W2 = dbResult.FLEX_SCOTT_W2;
                            inst.FLEX_SCOTT_W3 = dbResult.FLEX_SCOTT_W3;

                            inst.FLEX_SCOTT_W_AVG = CheckAVG(dbResult.FLEX_SCOTT_W1, dbResult.FLEX_SCOTT_W2, dbResult.FLEX_SCOTT_W3, null, null, null);

                            if (inst.FLEX_SCOTT_W_AVG != null)
                            {
                                if (inst.FLEX_SCOTT_W_AVG >= 0 && inst.FLEX_SCOTT_W_AVG <= 3)
                                {
                                    inst.FLEX_SCOTT_W_JUD = "NG";
                                    inst.FLEX_SCOTT_W_JUD2 = "NG";
                                }
                                else if (inst.FLEX_SCOTT_W_AVG > 3)
                                {
                                    inst.FLEX_SCOTT_W_JUD = "OK";
                                    inst.FLEX_SCOTT_W_JUD2 = "GOOD";
                                }
                                else
                                {
                                    inst.FLEX_SCOTT_W_JUD = "NG";
                                    inst.FLEX_SCOTT_W_JUD2 = "NG";
                                }
                            }
                            else
                            {
                                inst.FLEX_SCOTT_W_JUD = "OK";
                                inst.FLEX_SCOTT_W_JUD2 = "GOOD";
                            }

                            #region Old
                            //if (inst.FLEX_SCOTT_W_AVG != null && inst.FLEX_SCOTT_W_Min != null && inst.FLEX_SCOTT_W_Max != null)
                            //{
                            //    if (Between(inst.FLEX_SCOTT_W_AVG, inst.FLEX_SCOTT_W_Min, inst.FLEX_SCOTT_W_Max) == true)
                            //        inst.FLEX_SCOTT_W_JUD = "OK";
                            //    else
                            //    {
                            //        inst.FLEX_SCOTT_W_JUD = "FAIL";
                            //        i++;
                            //    }
                            //}
                            //else
                            //{
                            //    inst.FLEX_SCOTT_W_JUD = "OK";
                            //}
                            #endregion

                            #endregion

                            #region FLEX_SCOTT_F
                            inst.FLEX_SCOTT_F1 = dbResult.FLEX_SCOTT_F1;
                            inst.FLEX_SCOTT_F2 = dbResult.FLEX_SCOTT_F2;
                            inst.FLEX_SCOTT_F3 = dbResult.FLEX_SCOTT_F3;

                            inst.FLEX_SCOTT_F_AVG = CheckAVG(dbResult.FLEX_SCOTT_F1, dbResult.FLEX_SCOTT_F2, dbResult.FLEX_SCOTT_F3, null, null, null);

                            if (inst.FLEX_SCOTT_F_AVG != null)
                            {
                                if (inst.FLEX_SCOTT_F_AVG >= 0 && inst.FLEX_SCOTT_F_AVG <= 3)
                                {
                                    inst.FLEX_SCOTT_F_JUD = "NG";
                                    inst.FLEX_SCOTT_F_JUD2 = "NG";
                                }
                                else if (inst.FLEX_SCOTT_F_AVG > 3)
                                {
                                    inst.FLEX_SCOTT_F_JUD = "OK";
                                    inst.FLEX_SCOTT_F_JUD2 = "GOOD";
                                }
                                else
                                {
                                    inst.FLEX_SCOTT_F_JUD = "NG";
                                    inst.FLEX_SCOTT_F_JUD2 = "NG";
                                }
                            }
                            else
                            {
                                inst.FLEX_SCOTT_F_JUD = "OK";
                                inst.FLEX_SCOTT_F_JUD2 = "GOOD";
                            }

                            #region Old
                            //if (inst.FLEX_SCOTT_F_AVG != null && inst.FLEX_SCOTT_F_Min != null && inst.FLEX_SCOTT_F_Max != null)
                            //{
                            //    if (Between(inst.FLEX_SCOTT_F_AVG, inst.FLEX_SCOTT_F_Min, inst.FLEX_SCOTT_F_Max) == true)
                            //        inst.FLEX_SCOTT_F_JUD = "OK";
                            //    else
                            //    {
                            //        inst.FLEX_SCOTT_F_JUD = "FAIL";
                            //        i++;
                            //    }
                            //}
                            //else
                            //{
                            //    inst.FLEX_SCOTT_F_JUD = "OK";
                            //}
                            #endregion

                            #endregion

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

        #region CheckData
        private string CheckItemCode(string sub1)
        {
            string sub2 = "-AF01";
            string itemCode = string.Empty;

            // Check if the substring is 
            // present in the main string 
            bool b = sub1.Contains(sub2);
            if (b)
            {
                int index = sub1.IndexOf(sub2);
                if (index >= 0)
                    itemCode = sub1.Substring(0, index);
                else
                    itemCode = sub1;
            }
            else
            {
                itemCode = sub1;
            }

            return itemCode;
        }

        private decimal? CheckAVG(decimal? cal1, decimal? cal2, decimal? cal3, decimal? cal4, decimal? cal5, decimal? cal6)
        {
            List<decimal?> grades = new List<decimal?> { cal1, cal2, cal3, cal4, cal5, cal6 };
            decimal? average = grades.Average();

            return average;
        }

        private decimal? CheckMax(decimal? cal1, decimal? cal2, decimal? cal3, decimal? cal4, decimal? cal5, decimal? cal6)
        {
            List<decimal?> grades = new List<decimal?> { cal1, cal2, cal3, cal4, cal5, cal6 };
            decimal? max = grades.Max();

            return max;
        }

        private bool Between(decimal? num, decimal? lower, decimal? upper, bool inclusive = false)
        {
            //return inclusive
            //    ? lower <= num && num <= upper
            //    : lower < num && num < upper;

            if (lower <= num && num <= upper)
            {
                inclusive = true;
            }
            else
            {
                inclusive = false;
            }

            return inclusive;
        }

        private bool Min(decimal? num, decimal? lower, bool inclusive = false)
        {
            //return inclusive
            //    ? lower <= num 
            //    : lower < num ;

            if (lower <= num)
            {
                inclusive = true;
            }
            else
            {
                inclusive = false;
            }

            return inclusive;
        }

        private bool Max(decimal? num, decimal? upper, bool inclusive = false)
        {
            //return inclusive
            //    ? num <= upper
            //    : num < upper;

            if(num <= upper)
            {
                inclusive = true;
            }
            else
            {
                inclusive = false;
            }

            return inclusive;
        }
        #endregion

        #region LAB_GETREPORTINFO

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_CUSTOMERID"></param>
        /// <returns></returns>
        public LAB_GETREPORTINFO_Rep LAB_GETREPORTINFO(string P_ITMCODE, string P_CUSTOMERID)
        {
            LAB_GETREPORTINFO_Rep results = null;

            if (!HasConnection())
                return results;

            LAB_GETREPORTINFOParameter dbPara = new LAB_GETREPORTINFOParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_CUSTOMERID = P_CUSTOMERID;

            List<LAB_GETREPORTINFOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETREPORTINFO(dbPara);
                if (null != dbResults && dbResults.Count > 0 && null != dbResults[0])
                {
                    LAB_GETREPORTINFOResult dbResult = dbResults[0];

                    results = new LAB_GETREPORTINFO_Rep();

                    results.ITM_CODE = dbResult.ITM_CODE;
                    results.REPORT_ID = dbResult.REPORT_ID;
                    results.REVESION = dbResult.REVESION;
                    results.CUSTOMERID = dbResult.CUSTOMERID;
                    results.YARNTYPE = dbResult.YARNTYPE;
                    results.WEIGHT = dbResult.WEIGHT;
                    results.COATWEIGHT = dbResult.COATWEIGHT;
                    results.NUMTHREADS = dbResult.NUMTHREADS;
                    results.USABLE_WIDTH = dbResult.USABLE_WIDTH;
                    results.THICKNESS = dbResult.THICKNESS;
                    results.MAXFORCE = dbResult.MAXFORCE;
                    results.ELONGATIONFORCE = dbResult.ELONGATIONFORCE;
                    results.FLAMMABILITY = dbResult.FLAMMABILITY;
                    results.EDGECOMB = dbResult.EDGECOMB;
                    results.STIFFNESS = dbResult.STIFFNESS;
                    results.TEAR = dbResult.TEAR;
                    results.STATIC_AIR = dbResult.STATIC_AIR;
                    results.DYNAMIC_AIR = dbResult.DYNAMIC_AIR;
                    results.EXPONENT = dbResult.EXPONENT;
                    results.DIMENSCHANGE = dbResult.DIMENSCHANGE;
                    results.FLEXABRASION = dbResult.FLEXABRASION;
                    results.EFFECTIVE_DATE = dbResult.EFFECTIVE_DATE;

                    results.BOW = dbResult.BOW;
                    results.SKEW = dbResult.SKEW;
                    results.FLEX_SCOTT = dbResult.FLEX_SCOTT;

                    results.REPORT_NAME = dbResult.REPORT_NAME;

                    results.BENDING = dbResult.BENDING;

                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region LAB_GETITEMTESTSPECIFICATION
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <returns></returns>
        public LAB_GETITEMTESTSPECIFICATION_Rep LAB_GETITEMTESTSPECIFICATION(string P_ITMCODE)
        {
            LAB_GETITEMTESTSPECIFICATION_Rep results = null;

            if (!HasConnection())
                return results;

            LAB_GETITEMTESTSPECIFICATIONParameter dbPara = new LAB_GETITEMTESTSPECIFICATIONParameter();
            dbPara.P_ITMCODE = P_ITMCODE;

            List<LAB_GETITEMTESTSPECIFICATIONResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETITEMTESTSPECIFICATION(dbPara);
                if (null != dbResults && dbResults.Count > 0 && null != dbResults[0])
                {
                    LAB_GETITEMTESTSPECIFICATIONResult dbResult = dbResults[0];

                    results = new LAB_GETITEMTESTSPECIFICATION_Rep();

                    results.ITM_CODE = dbResult.ITM_CODE;

                    results.NUMTHREADS_W = dbResult.NUMTHREADS_W;
                    results.NUMTHREADS_W_TOR = dbResult.NUMTHREADS_W_TOR;

                    if (results.NUMTHREADS_W != null && results.NUMTHREADS_W_TOR != null)
                    {
                        results.NUMTHREADS_W_Spe = results.NUMTHREADS_W.Value.ToString("#,##0.##") + " +/- " + results.NUMTHREADS_W_TOR.Value.ToString("#,##0.##");
                        results.NUMTHREADS_W_Min = (results.NUMTHREADS_W - results.NUMTHREADS_W_TOR);
                        results.NUMTHREADS_W_Max = (results.NUMTHREADS_W + results.NUMTHREADS_W_TOR);
                    }

                    results.NUMTHREADS_F = dbResult.NUMTHREADS_F;
                    results.NUMTHREADS_F_TOR = dbResult.NUMTHREADS_F_TOR;

                    if (results.NUMTHREADS_F != null && results.NUMTHREADS_F_TOR != null)
                    {
                        results.NUMTHREADS_F_Spe = results.NUMTHREADS_F.Value.ToString("#,##0.##") + " +/- " + results.NUMTHREADS_F_TOR.Value.ToString("#,##0.##");
                        results.NUMTHREADS_F_Min = (results.NUMTHREADS_F - results.NUMTHREADS_F_TOR);
                        results.NUMTHREADS_F_Max = (results.NUMTHREADS_F + results.NUMTHREADS_F_TOR);
                    }

                    results.USABLE_WIDTH = dbResult.USABLE_WIDTH;
                    results.USABLE_WIDTH_TOR = dbResult.USABLE_WIDTH_TOR;

                    if (results.USABLE_WIDTH != null && !string.IsNullOrEmpty(results.USABLE_WIDTH_TOR))
                    {
                        results.USABLE_Spe = results.USABLE_WIDTH_TOR + " " + results.USABLE_WIDTH.Value.ToString("#,##0.##");
                        //if (results.USABLE_WIDTH_TOR.Contains("MIN"))
                        results.USABLE_WIDTH_Min = results.USABLE_WIDTH;
                    }
                    else if (results.USABLE_WIDTH != null && string.IsNullOrEmpty(results.USABLE_WIDTH_TOR))
                    {
                        results.USABLE_Spe = "MIN." + " " + results.USABLE_WIDTH.Value.ToString("#,##0.##");
                        results.USABLE_WIDTH_Min = results.USABLE_WIDTH;
                    }

                    results.WIDTH_SILICONE = dbResult.WIDTH_SILICONE;
                    results.WIDTH_SILICONE_TOR = dbResult.WIDTH_SILICONE_TOR;

                    if (results.WIDTH_SILICONE != null && !string.IsNullOrEmpty(results.WIDTH_SILICONE_TOR))
                    {
                        results.WIDTH_SILICONE_Spe = results.WIDTH_SILICONE_TOR + " " + results.WIDTH_SILICONE.Value.ToString("#,##0.##");
                        //if (results.USABLE_WIDTH_TOR.Contains("MIN"))
                        results.WIDTH_SILICONE_Min = results.WIDTH_SILICONE;
                    }
                    else if (results.WIDTH_SILICONE != null && string.IsNullOrEmpty(results.WIDTH_SILICONE_TOR))
                    {
                        results.WIDTH_SILICONE_Spe = "MIN." + " " + results.WIDTH_SILICONE.Value.ToString("#,##0.##");
                        results.WIDTH_SILICONE_Min = results.WIDTH_SILICONE;
                    }

                    results.TOTALWEIGHT = dbResult.TOTALWEIGHT;
                    results.TOTALWEIGHT_TOR = dbResult.TOTALWEIGHT_TOR;

                    if (results.TOTALWEIGHT != null && results.TOTALWEIGHT_TOR != null)
                    {
                        results.TOTALWEIGHT_Spe = results.TOTALWEIGHT.Value.ToString("#,##0.##") + " +/- " + results.TOTALWEIGHT_TOR.Value.ToString("#,##0.##");
                        results.TOTALWEIGHT_Min = (results.TOTALWEIGHT - results.TOTALWEIGHT_TOR);
                        results.TOTALWEIGHT_Max = (results.TOTALWEIGHT + results.TOTALWEIGHT_TOR);
                    }

                    results.UNCOATEDWEIGHT = dbResult.UNCOATEDWEIGHT;
                    results.UNCOATEDWEIGHT_TOR = dbResult.UNCOATEDWEIGHT_TOR;

                    if (results.UNCOATEDWEIGHT != null && results.UNCOATEDWEIGHT_TOR != null)
                    {
                        results.UNCOATEDWEIGHT_Spe = results.UNCOATEDWEIGHT.Value.ToString("#,##0.##") + " +/- " + results.UNCOATEDWEIGHT_TOR.Value.ToString("#,##0.##");
                        results.UNCOATEDWEIGHT_Min = (results.UNCOATEDWEIGHT - results.UNCOATEDWEIGHT_TOR);
                        results.UNCOATEDWEIGHT_Max = (results.UNCOATEDWEIGHT + results.UNCOATEDWEIGHT_TOR);
                    }

                    results.COATINGWEIGHT = dbResult.COATINGWEIGHT;
                    results.COATINGWEIGHT_TOR = dbResult.COATINGWEIGHT_TOR;

                    if (results.COATINGWEIGHT != null && results.COATINGWEIGHT_TOR != null)
                    {
                        results.COATINGWEIGHT_Spe = results.COATINGWEIGHT.Value.ToString("#,##0.##") + " +/- " + results.COATINGWEIGHT_TOR.Value.ToString("#,##0.##");
                        results.COATINGWEIGHT_Min = (results.COATINGWEIGHT - results.COATINGWEIGHT_TOR);
                        results.COATINGWEIGHT_Max = (results.COATINGWEIGHT + results.COATINGWEIGHT_TOR);
                    }

                    results.THICKNESS = dbResult.THICKNESS;
                    results.THICKNESS_TOR = dbResult.THICKNESS_TOR;

                    results.MAXFORCE_W = dbResult.MAXFORCE_W;
                    results.MAXFORCE_W_TOR = dbResult.MAXFORCE_W_TOR;

                    if (results.MAXFORCE_W != null && !string.IsNullOrEmpty(results.MAXFORCE_W_TOR))
                    {
                        results.MAXFORCE_W_Spe = results.MAXFORCE_W_TOR + " " + results.MAXFORCE_W.Value.ToString("#,##0.##");
                        results.MAXFORCE_W_Min = results.MAXFORCE_W;
                    }
                    else if (results.MAXFORCE_W != null && string.IsNullOrEmpty(results.MAXFORCE_W_TOR))
                    {
                        results.MAXFORCE_W_Spe = "MIN." + " " + results.MAXFORCE_W.Value.ToString("#,##0.##");
                        results.MAXFORCE_W_Min = results.MAXFORCE_W;
                    }

                    results.MAXFORCE_F = dbResult.MAXFORCE_F;
                    results.MAXFORCE_F_TOR = dbResult.MAXFORCE_F_TOR;

                    if (results.MAXFORCE_F != null && !string.IsNullOrEmpty(results.MAXFORCE_F_TOR))
                    {
                        results.MAXFORCE_F_Spe = results.MAXFORCE_F_TOR + " " + results.MAXFORCE_F.Value.ToString("#,##0.##");
                        results.MAXFORCE_F_Min = results.MAXFORCE_F;
                    }
                    else if (results.MAXFORCE_F != null && string.IsNullOrEmpty(results.MAXFORCE_F_TOR))
                    {
                        results.MAXFORCE_F_Spe = "MIN." + " " + results.MAXFORCE_F.Value.ToString("#,##0.##");
                        results.MAXFORCE_F_Min = results.MAXFORCE_F;
                    }


                    results.ELONGATIONFORCE_W = dbResult.ELONGATIONFORCE_W;
                    results.ELONGATIONFORCE_W_TOR = dbResult.ELONGATIONFORCE_W_TOR;

                    if (results.ELONGATIONFORCE_W != null && !string.IsNullOrEmpty(results.ELONGATIONFORCE_W_TOR))
                    {
                        if (results.ELONGATIONFORCE_W_TOR.Contains("MAX"))
                        {
                            results.ELONGATIONFORCE_W_Spe = results.DIMENSCHANGE_W_TOR + " " + results.ELONGATIONFORCE_W.Value.ToString("#,##0.##");
                            results.ELONGATIONFORCE_W_Max = results.ELONGATIONFORCE_W;
                        }
                        else if (results.ELONGATIONFORCE_W_TOR.Contains("MIN"))
                        {
                            results.ELONGATIONFORCE_W_Spe = results.ELONGATIONFORCE_W_TOR + " " + results.ELONGATIONFORCE_W.Value.ToString("#,##0.##");
                            results.ELONGATIONFORCE_W_Min = results.ELONGATIONFORCE_W;
                        }
                        else
                        {
                            results.ELONGATIONFORCE_W_Spe = results.ELONGATIONFORCE_W.Value.ToString("#,##0.##") + " +/- " + results.ELONGATIONFORCE_W_TOR;
                            results.ELONGATIONFORCE_W_Min = (results.ELONGATIONFORCE_W - Decimal.Parse(results.ELONGATIONFORCE_W_TOR));
                            results.ELONGATIONFORCE_W_Max = (results.ELONGATIONFORCE_W + Decimal.Parse(results.ELONGATIONFORCE_W_TOR));
                        }
                    }


                    results.ELONGATIONFORCE_F = dbResult.ELONGATIONFORCE_F;
                    results.ELONGATIONFORCE_F_TOR = dbResult.ELONGATIONFORCE_F_TOR;

                    if (results.ELONGATIONFORCE_F != null && !string.IsNullOrEmpty(results.ELONGATIONFORCE_F_TOR))
                    {
                        if (results.ELONGATIONFORCE_F_TOR.Contains("MAX"))
                        {
                            results.ELONGATIONFORCE_F_Spe = results.DIMENSCHANGE_F_TOR + " " + results.ELONGATIONFORCE_F.Value.ToString("#,##0.##");
                            results.ELONGATIONFORCE_F_Max = results.ELONGATIONFORCE_F;
                        }
                        else if (results.ELONGATIONFORCE_F_TOR.Contains("MIN"))
                        {
                            results.ELONGATIONFORCE_F_Spe = results.ELONGATIONFORCE_F_TOR + " " + results.ELONGATIONFORCE_F.Value.ToString("#,##0.##");
                            results.ELONGATIONFORCE_F_Min = results.ELONGATIONFORCE_F;
                        }
                        else
                        {
                            results.ELONGATIONFORCE_F_Spe = results.ELONGATIONFORCE_F.Value.ToString("#,##0.##") + " +/- " + results.ELONGATIONFORCE_F_TOR;
                            results.ELONGATIONFORCE_F_Min = (results.ELONGATIONFORCE_F - Decimal.Parse(results.ELONGATIONFORCE_F_TOR));
                            results.ELONGATIONFORCE_F_Max = (results.ELONGATIONFORCE_F + Decimal.Parse(results.ELONGATIONFORCE_F_TOR));
                        }
                    }


                    results.FLAMMABILITY_W = dbResult.FLAMMABILITY_W;
                    results.FLAMMABILITY_W_TOR = dbResult.FLAMMABILITY_W_TOR;

                    if (results.FLAMMABILITY_W != null && !string.IsNullOrEmpty(results.FLAMMABILITY_W_TOR))
                    {
                        results.FLAMMABILITY_W_Spe = results.FLAMMABILITY_W_TOR + " " + results.FLAMMABILITY_W.Value.ToString("#,##0.##");
                        results.FLAMMABILITY_W_Max = results.FLAMMABILITY_W;
                    }
                    else if (results.FLAMMABILITY_W != null && string.IsNullOrEmpty(results.FLAMMABILITY_W_TOR))
                    {
                        results.FLAMMABILITY_W_Spe = "MAX." + " " + results.FLAMMABILITY_W.Value.ToString("#,##0.##");
                        results.FLAMMABILITY_W_Max = results.FLAMMABILITY_W;
                    }

                    results.FLAMMABILITY_F = dbResult.FLAMMABILITY_F;
                    results.FLAMMABILITY_F_TOR = dbResult.FLAMMABILITY_F_TOR;

                    if (results.FLAMMABILITY_F != null && !string.IsNullOrEmpty(results.FLAMMABILITY_F_TOR))
                    {
                        results.FLAMMABILITY_F_Spe = results.FLAMMABILITY_F_TOR + " " + results.FLAMMABILITY_F.Value.ToString("#,##0.##");
                        results.FLAMMABILITY_F_Max = results.FLAMMABILITY_F;
                    }
                    else if (results.FLAMMABILITY_F != null && string.IsNullOrEmpty(results.FLAMMABILITY_F_TOR))
                    {
                        results.FLAMMABILITY_F_Spe = "MAX." + " " + results.FLAMMABILITY_F.Value.ToString("#,##0.##");
                        results.FLAMMABILITY_F_Max = results.FLAMMABILITY_F;
                    }

                    results.EDGECOMB_W = dbResult.EDGECOMB_W;
                    results.EDGECOMB_W_TOR = dbResult.EDGECOMB_W_TOR;

                    if (results.EDGECOMB_W != null && !string.IsNullOrEmpty(results.EDGECOMB_W_TOR))
                    {
                        results.EDGECOMB_W_Spe = results.EDGECOMB_W_TOR + " " + results.EDGECOMB_W.Value.ToString("#,##0.##");
                        results.EDGECOMB_W_Min = results.EDGECOMB_W;
                    }
                    else if (results.EDGECOMB_W != null && string.IsNullOrEmpty(results.EDGECOMB_W_TOR))
                    {
                        results.EDGECOMB_W_Spe = "MIN." + " " + results.EDGECOMB_W.Value.ToString("#,##0.##");
                        results.EDGECOMB_W_Min = results.EDGECOMB_W;
                    }

                    results.EDGECOMB_F = dbResult.EDGECOMB_F;
                    results.EDGECOMB_F_TOR = dbResult.EDGECOMB_F_TOR;

                    if (results.EDGECOMB_F != null && !string.IsNullOrEmpty(results.EDGECOMB_F_TOR))
                    {
                        results.EDGECOMB_F_Spe = results.EDGECOMB_F_TOR + " " + results.EDGECOMB_F.Value.ToString("#,##0.##");
                        results.EDGECOMB_F_Min = results.EDGECOMB_F;
                    }
                    else if (results.EDGECOMB_F != null && string.IsNullOrEmpty(results.EDGECOMB_F_TOR))
                    {
                        results.EDGECOMB_F_Spe = "MIN." + " " + results.EDGECOMB_F.Value.ToString("#,##0.##");
                        results.EDGECOMB_F_Min = results.EDGECOMB_F;
                    }

                    results.STIFFNESS_W = dbResult.STIFFNESS_W;
                    results.STIFFNESS_W_TOR = dbResult.STIFFNESS_W_TOR;

                    if (results.STIFFNESS_W != null && !string.IsNullOrEmpty(results.STIFFNESS_W_TOR))
                    {
                        results.STIFFNESS_W_Spe = results.STIFFNESS_W_TOR + " " + results.STIFFNESS_W.Value.ToString("#,##0.##");
                        results.STIFFNESS_W_Max = results.STIFFNESS_W;
                    }
                    else if (results.STIFFNESS_W != null && string.IsNullOrEmpty(results.STIFFNESS_W_TOR))
                    {
                        results.STIFFNESS_W_Spe = "MAX." + " " + results.STIFFNESS_W.Value.ToString("#,##0.##");
                        results.STIFFNESS_W_Max = results.STIFFNESS_W;
                    }

                    results.STIFFNESS_F = dbResult.STIFFNESS_F;
                    results.STIFFNESS_F_TOR = dbResult.STIFFNESS_F_TOR;

                    if (results.STIFFNESS_F != null && !string.IsNullOrEmpty(results.STIFFNESS_F_TOR))
                    {
                        results.STIFFNESS_F_Spe = results.STIFFNESS_F_TOR + " " + results.STIFFNESS_F.Value.ToString("#,##0.##");
                        results.STIFFNESS_F_Max = results.STIFFNESS_F;
                    }
                    else if (results.STIFFNESS_F != null && string.IsNullOrEmpty(results.STIFFNESS_F_TOR))
                    {
                        results.STIFFNESS_F_Spe = "MAX." + " " + results.STIFFNESS_F.Value.ToString("#,##0.##");
                        results.STIFFNESS_F_Max = results.STIFFNESS_F;
                    }

                    results.TEAR_W = dbResult.TEAR_W;
                    results.TEAR_W_TOR = dbResult.TEAR_W_TOR;

                    if (results.TEAR_W != null && !string.IsNullOrEmpty(results.TEAR_W_TOR))
                    {
                        results.TEAR_W_Spe = results.TEAR_W_TOR + " " + results.TEAR_W.Value.ToString("#,##0.##");
                        results.TEAR_W_Min = results.TEAR_W;
                    }
                    else if (results.TEAR_W != null && string.IsNullOrEmpty(results.TEAR_W_TOR))
                    {
                        results.TEAR_W_Spe = "MIN." + " " + results.TEAR_W.Value.ToString("#,##0.##");
                        results.TEAR_W_Min = results.TEAR_W;
                    }

                    results.TEAR_F = dbResult.TEAR_F;
                    results.TEAR_F_TOR = dbResult.TEAR_F_TOR;

                    if (results.TEAR_F != null && !string.IsNullOrEmpty(results.TEAR_F_TOR))
                    {
                        results.TEAR_F_Spe = results.TEAR_F_TOR + " " + results.TEAR_F.Value.ToString("#,##0.##");
                        results.TEAR_F_Min = results.TEAR_F;
                    }
                    else if (results.TEAR_F != null && string.IsNullOrEmpty(results.TEAR_F_TOR))
                    {
                        results.TEAR_F_Spe = "MIN." + " " + results.TEAR_F.Value.ToString("#,##0.##");
                        results.TEAR_F_Min = results.TEAR_F;
                    }

                    results.STATIC_AIR = dbResult.STATIC_AIR;
                    results.STATIC_AIR_TOR = dbResult.STATIC_AIR_TOR;

                    if (results.STATIC_AIR != null && !string.IsNullOrEmpty(results.STATIC_AIR_TOR))
                    {
                        results.STATIC_AIR_Spe = results.STATIC_AIR_TOR + " " + results.STATIC_AIR.Value.ToString("#,##0.##");
                        results.STATIC_AIR_Max = results.STATIC_AIR;
                    }
                    else if (results.STATIC_AIR != null && string.IsNullOrEmpty(results.STATIC_AIR_TOR))
                    {
                        results.STATIC_AIR_Spe = "MAX." + " " + results.STATIC_AIR.Value.ToString("#,##0.##");
                        results.STATIC_AIR_Max = results.STATIC_AIR;
                    }

                    results.FLEXABRASION_W = dbResult.FLEXABRASION_W;
                    results.FLEXABRASION_W_TOR = dbResult.FLEXABRASION_W_TOR;

                    if (results.FLEXABRASION_W != null && !string.IsNullOrEmpty(results.FLEXABRASION_W_TOR))
                    {
                        results.FLEXABRASION_W_Spe = results.FLEXABRASION_W_TOR + " " + results.FLEXABRASION_W.Value.ToString("#,##0.##");
                        results.FLEXABRASION_W_Min = results.FLEXABRASION_W;
                    }
                    else if (results.FLEXABRASION_W != null && string.IsNullOrEmpty(results.FLEXABRASION_W_TOR))
                    {
                        results.FLEXABRASION_W_Spe = "MIN." + " " + results.FLEXABRASION_W.Value.ToString("#,##0.##");
                        results.FLEXABRASION_W_Min = results.FLEXABRASION_W;
                    }

                    results.FLEXABRASION_F = dbResult.FLEXABRASION_F;
                    results.FLEXABRASION_F_TOR = dbResult.FLEXABRASION_F_TOR;

                    if (results.FLEXABRASION_F != null && !string.IsNullOrEmpty(results.FLEXABRASION_F_TOR))
                    {
                        results.FLEXABRASION_F_Spe = results.FLEXABRASION_F_TOR + " " + results.FLEXABRASION_F.Value.ToString("#,##0.##");
                        results.FLEXABRASION_F_Min = results.FLEXABRASION_F;
                    }
                    else if (results.FLEXABRASION_F != null && string.IsNullOrEmpty(results.FLEXABRASION_F_TOR))
                    {
                        results.FLEXABRASION_F_Spe = "MIN." + " " + results.FLEXABRASION_F.Value.ToString("#,##0.##");
                        results.FLEXABRASION_F_Min = results.FLEXABRASION_F;
                    }

                    results.DIMENSCHANGE_W = dbResult.DIMENSCHANGE_W;
                    results.DIMENSCHANGE_W_TOR = dbResult.DIMENSCHANGE_W_TOR;


                    if (results.DIMENSCHANGE_W != null && !string.IsNullOrEmpty(results.DIMENSCHANGE_W_TOR))
                    {
                        if (results.DIMENSCHANGE_W_TOR.Contains("MAX"))
                        {
                            results.DIMENSCHANGE_W_Spe = results.DIMENSCHANGE_W_TOR + " " + results.DIMENSCHANGE_W.Value.ToString("#,##0.##");
                            results.DIMENSCHANGE_W_Max = results.DIMENSCHANGE_W;
                        }
                        else if (results.DIMENSCHANGE_W_TOR.Contains("MIN"))
                        {
                            results.DIMENSCHANGE_W_Spe = results.DIMENSCHANGE_W_TOR + " " + results.DIMENSCHANGE_W.Value.ToString("#,##0.##");
                            results.DIMENSCHANGE_W_Min = results.DIMENSCHANGE_W;
                        }
                        else
                        {
                            results.DIMENSCHANGE_W_Spe = results.DIMENSCHANGE_W.Value.ToString("#,##0.##") + " +/- " + results.DIMENSCHANGE_W_TOR;
                            results.DIMENSCHANGE_W_Min = (results.DIMENSCHANGE_W - Decimal.Parse(results.DIMENSCHANGE_W_TOR));
                            results.DIMENSCHANGE_W_Max = (results.DIMENSCHANGE_W + Decimal.Parse(results.DIMENSCHANGE_W_TOR));
                        }
                    }

                    results.DIMENSCHANGE_F = dbResult.DIMENSCHANGE_F;
                    results.DIMENSCHANGE_F_TOR = dbResult.DIMENSCHANGE_F_TOR;

                    // ปรับ 09/06/18
                    if (results.DIMENSCHANGE_F != null && !string.IsNullOrEmpty(results.DIMENSCHANGE_F_TOR))
                    {
                        if (results.DIMENSCHANGE_F_TOR.Contains("MAX"))
                        {
                            results.DIMENSCHANGE_F_Spe = results.DIMENSCHANGE_F_TOR + " " + results.DIMENSCHANGE_F.Value.ToString("#,##0.##");
                            results.DIMENSCHANGE_F_Max = results.DIMENSCHANGE_F;
                        }
                        else if (results.DIMENSCHANGE_F_TOR.Contains("MIN"))
                        {
                            results.DIMENSCHANGE_F_Spe = results.DIMENSCHANGE_F_TOR + " " + results.DIMENSCHANGE_F.Value.ToString("#,##0.##");
                            results.DIMENSCHANGE_F_Min = results.DIMENSCHANGE_F;
                        }
                        else
                        {
                            results.DIMENSCHANGE_F_Spe = results.DIMENSCHANGE_F.Value.ToString("#,##0.##") + " +/- " + results.DIMENSCHANGE_F_TOR;
                            results.DIMENSCHANGE_F_Min = (results.DIMENSCHANGE_F - Decimal.Parse(results.DIMENSCHANGE_F_TOR));
                            results.DIMENSCHANGE_F_Max = (results.DIMENSCHANGE_F + Decimal.Parse(results.DIMENSCHANGE_F_TOR));
                        }
                    }

                    // ปรับ 28/10/20
                    if (results.THICKNESS != null && results.THICKNESS_TOR != null)
                    {
                        results.THICKNESS_Spe = results.THICKNESS.Value.ToString("#,##0.00") + " +/- " + results.THICKNESS_TOR.Value.ToString("#,##0.00");
                        results.THICKNESS_Min = (results.THICKNESS - results.THICKNESS_TOR);
                        results.THICKNESS_Max = (results.THICKNESS + results.THICKNESS_TOR);
                    }
                    else
                    {
                        if (results.THICKNESS != null)
                            results.THICKNESS_Spe = results.THICKNESS.Value.ToString("#,##0.00");
                    }

                    // ปรับ 28/10/20
                    results.BENDING_W = dbResult.BENDING_W;
                    results.BENDING_W_TOR = dbResult.BENDING_W_TOR;

                    if (results.BENDING_W != null && !string.IsNullOrEmpty(results.BENDING_W_TOR))
                    {
                        if (results.BENDING_W_TOR.Contains("MAX"))
                        {
                            results.BENDING_W_Spe = results.BENDING_W_TOR + " " + results.BENDING_W.Value.ToString("#,##0.##");
                            results.BENDING_W_Max = results.BENDING_W;
                        }
                        else if (results.BENDING_W_TOR.Contains("MIN"))
                        {
                            results.BENDING_W_Spe = results.BENDING_W_TOR + " " + results.BENDING_W.Value.ToString("#,##0.##");
                            results.BENDING_W_Min = results.BENDING_W;
                        }
                        else
                        {
                            results.BENDING_W_Spe = results.BENDING_W.Value.ToString("#,##0.##") + " +/- " + results.BENDING_W_TOR;
                            results.BENDING_W_Min = (results.BENDING_W - Decimal.Parse(results.BENDING_W_TOR));
                            results.BENDING_W_Max = (results.BENDING_W + Decimal.Parse(results.BENDING_W_TOR));
                        }
                    }

                    results.BENDING_F = dbResult.BENDING_F;
                    results.BENDING_F_TOR = dbResult.BENDING_F_TOR;

                    if (results.BENDING_F != null && !string.IsNullOrEmpty(results.BENDING_F_TOR))
                    {
                        if (results.BENDING_F_TOR.Contains("MAX"))
                        {
                            results.BENDING_F_Spe = results.BENDING_F_TOR + " " + results.BENDING_F.Value.ToString("#,##0.##");
                            results.BENDING_F_Max = results.BENDING_F;
                        }
                        else if (results.BENDING_F_TOR.Contains("MIN"))
                        {
                            results.BENDING_F_Spe = results.BENDING_F_TOR + " " + results.BENDING_F.Value.ToString("#,##0.##");
                            results.BENDING_F_Min = results.BENDING_F;
                        }
                        else
                        {
                            results.BENDING_F_Spe = results.BENDING_F.Value.ToString("#,##0.##") + " +/- " + results.BENDING_F_TOR;
                            results.BENDING_F_Min = (results.BENDING_F - Decimal.Parse(results.BENDING_F_TOR));
                            results.BENDING_F_Max = (results.BENDING_F + Decimal.Parse(results.BENDING_F_TOR));
                        }
                    }

                    results.DYNAMIC_AIR = dbResult.DYNAMIC_AIR;
                    results.DYNAMIC_AIR_TOR = dbResult.DYNAMIC_AIR_TOR;

                    if (results.DYNAMIC_AIR != null && results.DYNAMIC_AIR_TOR != null)
                    {
                        results.DYNAMIC_AIR_Spe = results.DYNAMIC_AIR.Value.ToString("#,##0.##") + " +/- " + results.DYNAMIC_AIR_TOR.Value.ToString("#,##0.##");
                        results.DYNAMIC_AIR_Max = (results.DYNAMIC_AIR + results.DYNAMIC_AIR_TOR);
                        results.DYNAMIC_AIR_Min = (results.DYNAMIC_AIR - results.DYNAMIC_AIR_TOR);
                    }

                    results.EXPONENT = dbResult.EXPONENT;
                    results.EXPONENT_TOR = dbResult.EXPONENT_TOR;

                    if (results.EXPONENT != null && results.EXPONENT_TOR != null)
                    {
                        results.EXPONENT_Spe = results.EXPONENT.Value.ToString("#,##0.##") + " +/- " + results.EXPONENT_TOR.Value.ToString("#,##0.##");
                        results.EXPONENT_Max = (results.EXPONENT + results.EXPONENT_TOR);
                        results.EXPONENT_Min = (results.EXPONENT - results.EXPONENT_TOR);
                    }

                    results.BOW = dbResult.BOW;
                    results.BOW_TOR = dbResult.BOW_TOR;

                    if (results.BOW != null && !string.IsNullOrEmpty(results.BOW_TOR))
                    {
                        if (results.BOW_TOR.Contains("MAX"))
                        {
                            results.BOW_Spe = results.BOW_TOR + " " + results.BOW.Value.ToString("#,##0.##");
                            results.BOW_Max = results.BOW;
                        }
                        else if (results.BOW_TOR.Contains("MIN"))
                        {
                            results.BOW_Spe = results.BOW_TOR + " " + results.BOW.Value.ToString("#,##0.##");
                            results.BOW_Min = results.BOW;
                        }
                        else
                        {
                            results.BOW_Spe = results.BOW.Value.ToString("#,##0.##") + " +/- " + results.BOW_TOR;
                            results.BOW_Min = (results.BOW - Decimal.Parse(results.BOW_TOR));
                            results.BOW_Max = (results.BOW + Decimal.Parse(results.BOW_TOR));
                        }
                    }

                    results.SKEW = dbResult.SKEW;
                    results.SKEW_TOR = dbResult.SKEW_TOR;

                    if (results.SKEW != null && !string.IsNullOrEmpty(results.SKEW_TOR))
                    {
                        if (results.SKEW_TOR.Contains("MAX"))
                        {
                            results.SKEW_Spe = results.SKEW_TOR + " " + results.SKEW.Value.ToString("#,##0.##");
                            results.SKEW_Max = results.SKEW;
                        }
                        else if (results.SKEW_TOR.Contains("MIN"))
                        {
                            results.SKEW_Spe = results.SKEW_TOR + " " + results.SKEW.Value.ToString("#,##0.##");
                            results.SKEW_Min = results.SKEW;
                        }
                        else
                        {
                            results.SKEW_Spe = results.SKEW.Value.ToString("#,##0.##") + " +/- " + results.SKEW_TOR;
                            results.SKEW_Min = (results.SKEW - Decimal.Parse(results.SKEW_TOR));
                            results.SKEW_Max = (results.SKEW + Decimal.Parse(results.SKEW_TOR));
                        }
                    }

                    results.FLEX_SCOTT_W = dbResult.FLEX_SCOTT_W;
                    results.FLEX_SCOTT_W_TOR = dbResult.FLEX_SCOTT_W_TOR;

                    if (results.FLEX_SCOTT_W != null && !string.IsNullOrEmpty(results.FLEX_SCOTT_W_TOR))
                    {
                        if (results.FLEX_SCOTT_W_TOR.Contains("MAX"))
                        {
                            results.FLEX_SCOTT_W_Spe = results.FLEX_SCOTT_W_TOR + " " + results.FLEX_SCOTT_W.Value.ToString("#,##0.##");
                            results.FLEX_SCOTT_W_Max = results.FLEX_SCOTT_W;
                        }
                        else if (results.FLEX_SCOTT_W_TOR.Contains("MIN"))
                        {
                            results.FLEX_SCOTT_W_Spe = results.FLEX_SCOTT_W_TOR + " " + results.FLEX_SCOTT_W.Value.ToString("#,##0.##");
                            results.FLEX_SCOTT_W_Min = results.FLEX_SCOTT_W;
                        }
                        else
                        {
                            results.FLEX_SCOTT_W_Spe = results.FLEX_SCOTT_W.Value.ToString("#,##0.##") + " +/- " + results.FLEX_SCOTT_W_TOR;
                            results.FLEX_SCOTT_W_Min = (results.FLEX_SCOTT_W - Decimal.Parse(results.FLEX_SCOTT_W_TOR));
                            results.FLEX_SCOTT_W_Max = (results.FLEX_SCOTT_W + Decimal.Parse(results.FLEX_SCOTT_W_TOR));
                        }
                    }

                    results.FLEX_SCOTT_F = dbResult.FLEX_SCOTT_F;
                    results.FLEX_SCOTT_F_TOR = dbResult.FLEX_SCOTT_F_TOR;

                    if (results.FLEX_SCOTT_F != null && !string.IsNullOrEmpty(results.FLEX_SCOTT_F_TOR))
                    {
                        if (results.FLEX_SCOTT_F_TOR.Contains("MAX"))
                        {
                            results.FLEX_SCOTT_F_Spe = results.FLEX_SCOTT_F_TOR + " " + results.FLEX_SCOTT_F.Value.ToString("#,##0.##");
                            results.FLEX_SCOTT_F_Max = results.FLEX_SCOTT_F;
                        }
                        else if (results.FLEX_SCOTT_F_TOR.Contains("MIN"))
                        {
                            results.FLEX_SCOTT_F_Spe = results.FLEX_SCOTT_F_TOR + " " + results.FLEX_SCOTT_F.Value.ToString("#,##0.##");
                            results.FLEX_SCOTT_F_Min = results.FLEX_SCOTT_F;
                        }
                        else
                        {
                            results.FLEX_SCOTT_F_Spe = results.FLEX_SCOTT_F.Value.ToString("#,##0.##") + " +/- " + results.FLEX_SCOTT_F_TOR;
                            results.FLEX_SCOTT_F_Min = (results.FLEX_SCOTT_F - Decimal.Parse(results.FLEX_SCOTT_F_TOR));
                            results.FLEX_SCOTT_F_Max = (results.FLEX_SCOTT_F + Decimal.Parse(results.FLEX_SCOTT_F_TOR));
                        }
                    }

                    //New 7/9/22
                    results.USABLE_WIDTH_LCL = dbResult.USABLE_WIDTH_LCL;
                    results.USABLE_WIDTH_UCL = dbResult.USABLE_WIDTH_UCL;
                    results.TOTALWEIGHT_LCL = dbResult.TOTALWEIGHT_LCL;
                    results.TOTALWEIGHT_UCL = dbResult.TOTALWEIGHT_UCL;
                    results.NUMTHREADS_W_LCL = dbResult.NUMTHREADS_W_LCL;
                    results.NUMTHREADS_W_UCL = dbResult.NUMTHREADS_W_UCL;
                    results.NUMTHREADS_F_LCL = dbResult.NUMTHREADS_F_LCL;
                    results.NUMTHREADS_F_UCL = dbResult.NUMTHREADS_F_UCL;
                    results.MAXFORCE_W_LCL = dbResult.MAXFORCE_W_LCL;
                    results.MAXFORCE_W_UCL = dbResult.MAXFORCE_W_UCL;
                    results.MAXFORCE_F_LCL = dbResult.MAXFORCE_F_LCL;
                    results.MAXFORCE_F_UCL = dbResult.MAXFORCE_F_UCL;
                    results.ELONGATIONFORCE_W_LCL = dbResult.ELONGATIONFORCE_W_LCL;
                    results.ELONGATIONFORCE_W_UCL = dbResult.ELONGATIONFORCE_W_UCL;
                    results.ELONGATIONFORCE_F_LCL = dbResult.ELONGATIONFORCE_F_LCL;
                    results.ELONGATIONFORCE_F_UCL = dbResult.ELONGATIONFORCE_F_UCL;
                    results.EDGECOMB_W_LCL = dbResult.EDGECOMB_W_LCL;
                    results.EDGECOMB_W_UCL = dbResult.EDGECOMB_W_UCL;
                    results.EDGECOMB_F_LCL = dbResult.EDGECOMB_F_LCL;
                    results.EDGECOMB_F_UCL = dbResult.EDGECOMB_F_UCL;
                    results.TEAR_W_LCL = dbResult.TEAR_W_LCL;
                    results.TEAR_W_UCL = dbResult.TEAR_W_UCL;
                    results.TEAR_F_LCL = dbResult.TEAR_F_LCL;
                    results.TEAR_F_UCL = dbResult.TEAR_F_UCL;
                    results.STATIC_AIR_LCL = dbResult.STATIC_AIR_LCL;
                    results.STATIC_AIR_UCL = dbResult.STATIC_AIR_UCL;
                    results.DYNAMIC_AIR_LCL = dbResult.DYNAMIC_AIR_LCL;
                    results.DYNAMIC_AIR_UCL = dbResult.DYNAMIC_AIR_UCL;
                    results.EXPONENT_LCL = dbResult.EXPONENT_LCL;
                    results.EXPONENT_UCL = dbResult.EXPONENT_UCL;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        // -- Update 26/10/20 -- //

        #region เพิ่มใหม่ LAB_SEARCHAPPROVELAB
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_ENTRYSTARTDATE"></param>
        /// <param name="P_ENTRYENDDATE"></param>
        /// <param name="P_LOOM"></param>
        /// <param name="P_FINISHPROCESS"></param>
        /// <returns></returns>
        public List<LAB_SEARCHAPPROVELAB> LAB_SEARCHAPPROVELAB(string P_ITMCODE, string P_ENTRYSTARTDATE, string P_ENTRYENDDATE, string P_LOOM, string P_FINISHPROCESS)
        {
            List<LAB_SEARCHAPPROVELAB> results = null;

            if (!HasConnection())
                return results;

            LAB_SEARCHAPPROVELABParameter dbPara = new LAB_SEARCHAPPROVELABParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_ENTRYSTARTDATE = P_ENTRYSTARTDATE;
            dbPara.P_ENTRYENDDATE = P_ENTRYENDDATE;
            dbPara.P_LOOM = P_LOOM;
            dbPara.P_FINISHPROCESS = P_FINISHPROCESS;

            List<LAB_SEARCHAPPROVELABResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_SEARCHAPPROVELAB(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_SEARCHAPPROVELAB>();
                    foreach (LAB_SEARCHAPPROVELABResult dbResult in dbResults)
                    {
                        LAB_SEARCHAPPROVELAB inst = new LAB_SEARCHAPPROVELAB();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ENTRYDATE = dbResult.ENTRYDATE;
                        inst.ENTEYBY = dbResult.ENTEYBY;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.USABLE_WIDTH1 = dbResult.USABLE_WIDTH1;
                        inst.USABLE_WIDTH2 = dbResult.USABLE_WIDTH2;
                        inst.USABLE_WIDTH3 = dbResult.USABLE_WIDTH3;
                        inst.WIDTH_SILICONE1 = dbResult.WIDTH_SILICONE1;
                        inst.WIDTH_SILICONE2 = dbResult.WIDTH_SILICONE2;
                        inst.WIDTH_SILICONE3 = dbResult.WIDTH_SILICONE3;
                        inst.NUMTHREADS_W1 = dbResult.NUMTHREADS_W1;
                        inst.NUMTHREADS_W2 = dbResult.NUMTHREADS_W2;
                        inst.NUMTHREADS_W3 = dbResult.NUMTHREADS_W3;
                        inst.NUMTHREADS_F1 = dbResult.NUMTHREADS_F1;
                        inst.NUMTHREADS_F2 = dbResult.NUMTHREADS_F2;
                        inst.NUMTHREADS_F3 = dbResult.NUMTHREADS_F3;
                        inst.TOTALWEIGHT1 = dbResult.TOTALWEIGHT1;
                        inst.TOTALWEIGHT2 = dbResult.TOTALWEIGHT2;
                        inst.TOTALWEIGHT3 = dbResult.TOTALWEIGHT3;
                        inst.TOTALWEIGHT4 = dbResult.TOTALWEIGHT4;
                        inst.TOTALWEIGHT5 = dbResult.TOTALWEIGHT5;
                        inst.TOTALWEIGHT6 = dbResult.TOTALWEIGHT6;
                        inst.UNCOATEDWEIGHT1 = dbResult.UNCOATEDWEIGHT1;
                        inst.UNCOATEDWEIGHT2 = dbResult.UNCOATEDWEIGHT2;
                        inst.UNCOATEDWEIGHT3 = dbResult.UNCOATEDWEIGHT3;
                        inst.UNCOATEDWEIGHT4 = dbResult.UNCOATEDWEIGHT4;
                        inst.UNCOATEDWEIGHT5 = dbResult.UNCOATEDWEIGHT5;
                        inst.UNCOATEDWEIGHT6 = dbResult.UNCOATEDWEIGHT6;
                        inst.COATINGWEIGHT1 = dbResult.COATINGWEIGHT1;
                        inst.COATINGWEIGHT2 = dbResult.COATINGWEIGHT2;
                        inst.COATINGWEIGHT3 = dbResult.COATINGWEIGHT3;
                        inst.COATINGWEIGHT4 = dbResult.COATINGWEIGHT4;
                        inst.COATINGWEIGHT5 = dbResult.COATINGWEIGHT5;
                        inst.COATINGWEIGHT6 = dbResult.COATINGWEIGHT6;
                        inst.THICKNESS1 = dbResult.THICKNESS1;
                        inst.THICKNESS2 = dbResult.THICKNESS2;
                        inst.THICKNESS3 = dbResult.THICKNESS3;
                        inst.MAXFORCE_W1 = dbResult.MAXFORCE_W1;
                        inst.MAXFORCE_W2 = dbResult.MAXFORCE_W2;
                        inst.MAXFORCE_W3 = dbResult.MAXFORCE_W3;
                        inst.MAXFORCE_F1 = dbResult.MAXFORCE_F1;
                        inst.MAXFORCE_F2 = dbResult.MAXFORCE_F2;
                        inst.MAXFORCE_F3 = dbResult.MAXFORCE_F3;
                        inst.ELONGATIONFORCE_W1 = dbResult.ELONGATIONFORCE_W1;
                        inst.ELONGATIONFORCE_W2 = dbResult.ELONGATIONFORCE_W2;
                        inst.ELONGATIONFORCE_W3 = dbResult.ELONGATIONFORCE_W3;
                        inst.ELONGATIONFORCE_F1 = dbResult.ELONGATIONFORCE_F1;
                        inst.ELONGATIONFORCE_F2 = dbResult.ELONGATIONFORCE_F2;
                        inst.ELONGATIONFORCE_F3 = dbResult.ELONGATIONFORCE_F3;

                        inst.FLAMMABILITY_W = dbResult.FLAMMABILITY_W;
                        inst.FLAMMABILITY_W2 = dbResult.FLAMMABILITY_W2;
                        inst.FLAMMABILITY_W3 = dbResult.FLAMMABILITY_W3;
                        inst.FLAMMABILITY_W4 = dbResult.FLAMMABILITY_W4;
                        inst.FLAMMABILITY_W5 = dbResult.FLAMMABILITY_W5;

                        inst.FLAMMABILITY_F = dbResult.FLAMMABILITY_F;
                        inst.FLAMMABILITY_F2 = dbResult.FLAMMABILITY_F2;
                        inst.FLAMMABILITY_F3 = dbResult.FLAMMABILITY_F3;
                        inst.FLAMMABILITY_F4 = dbResult.FLAMMABILITY_F4;
                        inst.FLAMMABILITY_F5 = dbResult.FLAMMABILITY_F5;

                        inst.EDGECOMB_W1 = dbResult.EDGECOMB_W1;
                        inst.EDGECOMB_W2 = dbResult.EDGECOMB_W2;
                        inst.EDGECOMB_W3 = dbResult.EDGECOMB_W3;
                        inst.EDGECOMB_F1 = dbResult.EDGECOMB_F1;
                        inst.EDGECOMB_F2 = dbResult.EDGECOMB_F2;
                        inst.EDGECOMB_F3 = dbResult.EDGECOMB_F3;
                        inst.STIFFNESS_W1 = dbResult.STIFFNESS_W1;
                        inst.STIFFNESS_W2 = dbResult.STIFFNESS_W2;
                        inst.STIFFNESS_W3 = dbResult.STIFFNESS_W3;
                        inst.STIFFNESS_F1 = dbResult.STIFFNESS_F1;
                        inst.STIFFNESS_F2 = dbResult.STIFFNESS_F2;
                        inst.STIFFNESS_F3 = dbResult.STIFFNESS_F3;
                        inst.TEAR_W1 = dbResult.TEAR_W1;
                        inst.TEAR_W2 = dbResult.TEAR_W2;
                        inst.TEAR_W3 = dbResult.TEAR_W3;
                        inst.TEAR_F1 = dbResult.TEAR_F1;
                        inst.TEAR_F2 = dbResult.TEAR_F2;
                        inst.TEAR_F3 = dbResult.TEAR_F3;
                        inst.STATIC_AIR1 = dbResult.STATIC_AIR1;
                        inst.STATIC_AIR2 = dbResult.STATIC_AIR2;
                        inst.STATIC_AIR3 = dbResult.STATIC_AIR3;

                        inst.STATIC_AIR4 = dbResult.STATIC_AIR4;
                        inst.STATIC_AIR5 = dbResult.STATIC_AIR5;
                        inst.STATIC_AIR6 = dbResult.STATIC_AIR6;

                        inst.DYNAMIC_AIR1 = dbResult.DYNAMIC_AIR1;
                        inst.DYNAMIC_AIR2 = dbResult.DYNAMIC_AIR2;
                        inst.DYNAMIC_AIR3 = dbResult.DYNAMIC_AIR3;
                        inst.EXPONENT1 = dbResult.EXPONENT1;
                        inst.EXPONENT2 = dbResult.EXPONENT2;
                        inst.EXPONENT3 = dbResult.EXPONENT3;
                        inst.DIMENSCHANGE_W1 = dbResult.DIMENSCHANGE_W1;
                        inst.DIMENSCHANGE_W2 = dbResult.DIMENSCHANGE_W2;
                        inst.DIMENSCHANGE_W3 = dbResult.DIMENSCHANGE_W3;
                        inst.DIMENSCHANGE_F1 = dbResult.DIMENSCHANGE_F1;
                        inst.DIMENSCHANGE_F2 = dbResult.DIMENSCHANGE_F2;
                        inst.DIMENSCHANGE_F3 = dbResult.DIMENSCHANGE_F3;
                        inst.FLEXABRASION_W1 = dbResult.FLEXABRASION_W1;
                        inst.FLEXABRASION_W2 = dbResult.FLEXABRASION_W2;
                        inst.FLEXABRASION_W3 = dbResult.FLEXABRASION_W3;
                        inst.FLEXABRASION_F1 = dbResult.FLEXABRASION_F1;
                        inst.FLEXABRASION_F2 = dbResult.FLEXABRASION_F2;
                        inst.FLEXABRASION_F3 = dbResult.FLEXABRASION_F3;

                        inst.STATUS = dbResult.STATUS;
                        inst.REMARK = dbResult.REMARK;
                        inst.APPROVEBY = dbResult.APPROVEBY;
                        inst.APPROVEDATE = dbResult.APPROVEDATE;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.FINISHLENGTH = dbResult.FINISHLENGTH;
                        inst.FINISHINGPROCESS = dbResult.FINISHINGPROCESS;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.FINISHINGMC = dbResult.FINISHINGMC;
                        inst.BOW1 = dbResult.BOW1;
                        inst.BOW2 = dbResult.BOW2;
                        inst.BOW3 = dbResult.BOW3;
                        inst.SKEW1 = dbResult.SKEW1;
                        inst.SKEW2 = dbResult.SKEW2;
                        inst.SKEW3 = dbResult.SKEW3;
                        inst.BENDING_W1 = dbResult.BENDING_W1;
                        inst.BENDING_W2 = dbResult.BENDING_W2;
                        inst.BENDING_W3 = dbResult.BENDING_W3;
                        inst.BENDING_F1 = dbResult.BENDING_F1;
                        inst.BENDING_F2 = dbResult.BENDING_F2;
                        inst.BENDING_F3 = dbResult.BENDING_F3;
                        inst.FLEX_SCOTT_W1 = dbResult.FLEX_SCOTT_W1;
                        inst.FLEX_SCOTT_W2 = dbResult.FLEX_SCOTT_W2;
                        inst.FLEX_SCOTT_W3 = dbResult.FLEX_SCOTT_W3;
                        inst.FLEX_SCOTT_F1 = dbResult.FLEX_SCOTT_F1;
                        inst.FLEX_SCOTT_F2 = dbResult.FLEX_SCOTT_F2;
                        inst.FLEX_SCOTT_F3 = dbResult.FLEX_SCOTT_F3;
                        inst.ITEMLOT = dbResult.ITEMLOT;
                        inst.BATCHNO = dbResult.BATCHNO;
                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.PARTNO = dbResult.PARTNO;
                        inst.FILENAME = dbResult.FILENAME;
                        inst.UPLOADDATE = dbResult.UPLOADDATE;
                        inst.UPLOADBY = dbResult.UPLOADBY;

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

        #region LAB_UPLOADREPORT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WEAVINGLOG"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_ENTRYDATE"></param>
        /// <param name="P_FILENAME"></param>
        /// <param name="P_UPLOADDATE"></param>
        /// <returns></returns>
        public string LAB_UPLOADREPORT(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, DateTime? P_ENTRYDATE, string P_FILENAME, DateTime? P_UPLOADDATE, string P_UPLOADBY)
        {
            string result = string.Empty;

            if (!HasConnection())
                return "Can't connect";

            LAB_UPLOADREPORTParameter dbPara = new LAB_UPLOADREPORTParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOG = P_WEAVINGLOG;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_ENTRYDATE = P_ENTRYDATE;
            dbPara.P_FILENAME = P_FILENAME;
            dbPara.P_UPLOADDATE = P_UPLOADDATE;
            dbPara.P_UPLOADBY = P_UPLOADBY;

            LAB_UPLOADREPORTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_UPLOADREPORT(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        // -- Update 28/10/20 -- //
        #region LAB_GETWEAVINGLOTLIST
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_PROCESS"></param>
        /// <returns></returns>
        public List<LAB_GETWEAVINGLOTLIST> LAB_GETWEAVINGLOTLIST(string P_FINISHINGLOT, string P_ITEMCODE, string P_PROCESS)
        {
            List<LAB_GETWEAVINGLOTLIST> results = null;

            if (!HasConnection())
                return results;

            LAB_GETWEAVINGLOTLISTParameter dbPara = new LAB_GETWEAVINGLOTLISTParameter();
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_ITEMCODE = P_ITEMCODE;
            dbPara.P_PROCESS = P_PROCESS;

            List<LAB_GETWEAVINGLOTLISTResult> dbResults = null;
            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETWEAVINGLOTLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETWEAVINGLOTLIST>();
                    foreach (LAB_GETWEAVINGLOTLISTResult dbResult in dbResults)
                    {
                        LAB_GETWEAVINGLOTLIST inst = new LAB_GETWEAVINGLOTLIST();

                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;

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