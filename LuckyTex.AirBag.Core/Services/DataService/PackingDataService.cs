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
    #region Packing Data Service

    /// <summary>
    /// The data service for Packing process.
    /// </summary>
    public class PackingDataService : BaseDataService
    {
        #region Singelton

        private static PackingDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static PackingDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(PackingDataService))
                    {
                        _instance = new PackingDataService();
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
        private PackingDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~PackingDataService()
        {
        }

        #endregion

        #region Public Methods

        #region Get related Finishing methods

        /// <summary>
        /// Gets all Finishing Machines.
        /// </summary>
        /// <returns>Returns inspection machine.</returns>
        public List<PackingMCItem> GetMachines()
        {
            List<PackingMCItem> results = new List<PackingMCItem>();

            PackingMCItem inst = null;
          
            inst = new PackingMCItem();
            inst.MCNo = 1;
            inst.MCId = "1";
            inst.DisplayName = "Packing Label";
            results.Add(inst);
            inst = new PackingMCItem();
            inst.MCNo = 2;
            inst.MCId = "2";
            inst.DisplayName = "Pallet Setup";
            results.Add(inst);
            inst = new PackingMCItem();
            inst.MCNo = 3;
            inst.MCId = "3";
            inst.DisplayName = "Pallet List";
            results.Add(inst);
            inst = new PackingMCItem();
            inst.MCNo = 4;
            inst.MCId = "4";
            inst.DisplayName = "Pallet Edit";
            results.Add(inst);

            inst = new PackingMCItem();
            inst.MCNo = 5;
            inst.MCId = "5";
            inst.DisplayName = "Re-Print Label";
            results.Add(inst);

            inst = new PackingMCItem();
            inst.MCNo = 6;
            inst.MCId = "6";
            inst.DisplayName = "Autoliv Label";
            results.Add(inst);

            return results;
        }

        #endregion

        #region Create new Session

        public PackingSession GetSession(LogInResult loginResult)
        {
            PackingSession result = new PackingSession();
            result.Init(loginResult);
            return result;
        }

        #endregion

        #region เพิ่มใหม่ GetItemCodeData ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<GetItemCodeData> GetItemCodeData()
        {
            List<GetItemCodeData> results = null;

            if (!HasConnection())
                return results;

            GETITEMCODEDATAParameter dbPara = new GETITEMCODEDATAParameter();

            List<GETITEMCODEDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.GETITEMCODEDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<GetItemCodeData>();
                    foreach (GETITEMCODEDATAResult dbResult in dbResults)
                    {
                        GetItemCodeData inst = new GetItemCodeData();

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

        #region TPACKINGPALLET

        #region เพิ่มใหม่ PACK_SEARCHINSPECTIONDATA ใช้ในการ Load Pack

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PACK_SEARCHINSPECTIONDATA> Pack_SearchDataList(string strDATE, string GRADE, string ITMCODE, string INSLOT)
        {
            List<PACK_SEARCHINSPECTIONDATA> results = null;

            if (!HasConnection())
                return results;

            PACK_SEARCHINSPECTIONDATAParameter dbPara = new PACK_SEARCHINSPECTIONDATAParameter();

            dbPara.P_DATE = strDATE;
            dbPara.P_GRADE = GRADE;
            dbPara.P_ITMCODE = ITMCODE;
            dbPara.P_INSLOT = INSLOT;

            List<PACK_SEARCHINSPECTIONDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PACK_SEARCHINSPECTIONDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<PACK_SEARCHINSPECTIONDATA>();
                    foreach (PACK_SEARCHINSPECTIONDATAResult dbResult in dbResults)
                    {
                        PACK_SEARCHINSPECTIONDATA inst = new PACK_SEARCHINSPECTIONDATA();

                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.GRADE = dbResult.GRADE;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PEINSPECTIONLOT = dbResult.PEINSPECTIONLOT;
                        inst.DEFECTID = dbResult.DEFECTID;
                        inst.REMARK = dbResult.REMARK;
                        inst.ATTACHID = dbResult.ATTACHID;
                        inst.TESTRECORDID = dbResult.TESTRECORDID;
                        inst.INSPECTEDBY = dbResult.INSPECTEDBY;
                        inst.MCNO = dbResult.MCNO;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.SUSPENDDATE = dbResult.SUSPENDDATE;
                        inst.INSPECTIONID = dbResult.INSPECTIONID;
                        inst.RETEST = dbResult.RETEST;
                        inst.PREITEMCODE = dbResult.PREITEMCODE;
                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.CLEARREMARK = dbResult.CLEARREMARK;
                        inst.SUSPENDBY = dbResult.SUSPENDBY;
                        inst.STARTDATE1 = dbResult.STARTDATE1;
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;
                        inst.DEFECTFILENAME = dbResult.DEFECTFILENAME;
                        inst.ISPACKED = dbResult.ISPACKED;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.ITM_GROUP = dbResult.ITM_GROUP;

                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;
                        inst.DF_CODE = dbResult.DF_CODE;
                        inst.DF_AMOUNT = dbResult.DF_AMOUNT;
                        inst.DF_POINT = dbResult.DF_POINT;

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

        #region UpdateInspectionProcess
        /// <summary>
       /// 
       /// </summary>
       /// <param name="insLotNo"></param>
       /// <param name="startDate"></param>
        public void UpdateInspectionProcess(string insLotNo, DateTime? startDate)
        {
            if (string.IsNullOrWhiteSpace(insLotNo))
                return;

            if (!HasConnection())
                return;

            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();
            dbPara.P_INSLOT = insLotNo;
            dbPara.P_STARTDATE = startDate;
            dbPara.P_PACK = "Y";

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #region PACK_INSERTPACKINGPALLET
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OPERATOR"></param>
        public string PACK_INSERTPACKINGPALLET(string OPERATOR)
        {
            if (string.IsNullOrWhiteSpace(OPERATOR))
                return string.Empty;

            if (!HasConnection())
                return string.Empty;

            PACK_INSERTPACKINGPALLETParameter dbPara = new PACK_INSERTPACKINGPALLETParameter();
            dbPara.P_OPERATOR = OPERATOR;
           
            PACK_INSERTPACKINGPALLETResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PACK_INSERTPACKINGPALLET(dbPara);

                return dbResult.R_PALLETNO;
            }
            catch (Exception ex)
            {
                ex.Err();
                return string.Empty;
            }
        }
        #endregion

        #region PACK_INSPACKINGPALLETDETAIL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="insLotNo"></param>
        /// <param name="startDate"></param>
        public void PACK_INSPACKINGPALLETDETAIL(string PALLETNO,decimal? ORDERNO ,string insLotNo, string ITMCODE, string GRADE
            , decimal? NETLENGTH, decimal? NETWEIGHT, decimal? GROSSWEIGHT, string CUSTYPE, DateTime? INSPECTDATE, string LOADTYPE,decimal? P_GROSSLENGTH)
        {
            if (string.IsNullOrWhiteSpace(insLotNo))
                return;

            if (!HasConnection())
                return;

            PACK_INSPACKINGPALLETDETAILParameter dbPara = new PACK_INSPACKINGPALLETDETAILParameter();

            dbPara.P_PALLETNO = PALLETNO;
            dbPara.P_ORDERNO = ORDERNO;
            dbPara.P_INSLOT = insLotNo;
            dbPara.P_ITMCODE = ITMCODE;
            dbPara.P_GRADE = GRADE;
            dbPara.P_NETLENGTH = NETLENGTH;
            dbPara.P_NETWEIGHT = NETWEIGHT;
            dbPara.P_GROSSWEIGHT = GROSSWEIGHT;
            dbPara.P_CUSTYPE = CUSTYPE;
            dbPara.P_INSPECTDATE = INSPECTDATE;
            dbPara.P_LOADTYPE = LOADTYPE;
            dbPara.P_GROSSLENGTH = P_GROSSLENGTH;

            PACK_INSPACKINGPALLETDETAILResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PACK_INSPACKINGPALLETDETAIL(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #endregion

        #region เพิ่มใหม่ Pack_SearchPalletList ใช้ในการ Load Pack

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PACK_SEARCHPALLETLIST> Pack_SearchPalletList(string PalletNo, string strDATE, string CheckingStatus)
        {
            List<PACK_SEARCHPALLETLIST> results = null;

            if (!HasConnection())
                return results;

            PACK_SEARCHPALLETLISTParameter dbPara = new PACK_SEARCHPALLETLISTParameter();
            dbPara.P_PALLET = PalletNo;
            dbPara.P_DATE = strDATE;
            dbPara.P_STATUS = CheckingStatus;

            List<PACK_SEARCHPALLETLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PACK_SEARCHPALLETLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<PACK_SEARCHPALLETLIST>();
                    foreach (PACK_SEARCHPALLETLISTResult dbResult in dbResults)
                    {
                        PACK_SEARCHPALLETLIST inst = new PACK_SEARCHPALLETLIST();

                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.PACKINGDATE = dbResult.PACKINGDATE;
                        inst.PACKINGBY = dbResult.PACKINGBY;
                        inst.CHECKBY = dbResult.CHECKBY;

                        if (dbResult.CHECKBY != null)
                            inst.PalletChecking = true;

                        inst.CHECKINGDATE = dbResult.CHECKINGDATE;
                        inst.REMARK = dbResult.REMARK;
                        inst.COMPLETELAB = dbResult.COMPLETELAB;
                        inst.TRANSFERAS400 = dbResult.TRANSFERAS400;
                        inst.FLAG = dbResult.FLAG;

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

        #region เพิ่มใหม่ PACK_GETPALLETDETAIL ใช้ในการ Load Pack

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PACK_GETPALLETDETAIL> PACK_GETPALLETDETAIL(string PalletNo)
        {
            List<PACK_GETPALLETDETAIL> results = null;

            if (!HasConnection())
                return results;

            PACK_GETPALLETDETAILParameter dbPara = new PACK_GETPALLETDETAILParameter();
            dbPara.P_PALLET = PalletNo;

            List<PACK_GETPALLETDETAILResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PACK_GETPALLETDETAIL(dbPara);
                if (null != dbResults)
                {
                    int i = 0;
                    results = new List<PACK_GETPALLETDETAIL>();
                    foreach (PACK_GETPALLETDETAILResult dbResult in dbResults)
                    {
                        PACK_GETPALLETDETAIL inst = new PACK_GETPALLETDETAIL();

                        inst.RowNo = i + 1;
                        inst.PALLETNO = dbResult.PALLETNO;

                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.INSPECTIONLOT_OLD = dbResult.INSPECTIONLOT;

                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.ITM_GROUP = dbResult.ITM_GROUP;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.GRADE = dbResult.GRADE;
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;
                        inst.ISLAB = dbResult.ISLAB;
                        inst.INSPECTIONDATE = dbResult.INSPECTIONDATE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.FLAG = dbResult.FLAG;
                        inst.STOCK = dbResult.STOCK;
                        inst.ORDERNO = dbResult.ORDERNO;

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

        #region เพิ่มใหม่ PACK_PALLETSHEET ใช้ในการ Load Pack

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PACK_PALLETSHEET> PACK_PALLETSHEET(string PalletNo)
        {
            List<PACK_PALLETSHEET> results = null;

            if (!HasConnection())
                return results;

            PACK_PALLETSHEETParameter dbPara = new PACK_PALLETSHEETParameter();
            dbPara.P_PALLET = PalletNo;

            List<PACK_PALLETSHEETResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PACK_PALLETSHEET(dbPara);
                if (null != dbResults)
                {
                    int i = 0;
                    results = new List<PACK_PALLETSHEET>();
                    foreach (PACK_PALLETSHEETResult dbResult in dbResults)
                    {
                        PACK_PALLETSHEET inst = new PACK_PALLETSHEET();

                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.GRADE = dbResult.GRADE;
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;
                        inst.PACKINGDATE = dbResult.PACKINGDATE;
                        inst.PACKINGBY = dbResult.PACKINGBY;
                        inst.CHECKBY = dbResult.CHECKBY;
                        inst.CHECKINGDATE = dbResult.CHECKINGDATE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.YARNCODE = dbResult.YARNCODE;

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

        #region เพิ่มใหม่ Count_PACK_PALLETSHEET ใช้ในการ Load Pack

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Count_PACK_PALLETSHEET(string PalletNo)
        {
            int results = 0;

            if (!HasConnection())
                return results;

            PACK_PALLETSHEETParameter dbPara = new PACK_PALLETSHEETParameter();
            dbPara.P_PALLET = PalletNo;

            List<PACK_PALLETSHEETResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PACK_PALLETSHEET(dbPara);
                if (null != dbResults)
                {
                    results = dbResults.Count;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ PACK_PRINTLABEL ใช้ในการ Load Pack

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PACK_PRINTLABEL> PACK_PRINTLABEL(string INSLOT)
        {
           
            List<PACK_PRINTLABEL> results = null;

            if (string.IsNullOrWhiteSpace(INSLOT))
                return results;

            if (!HasConnection())
                return results;

            PACK_PRINTLABELParameter dbPara = new PACK_PRINTLABELParameter();
            dbPara.P_INSLOT = INSLOT;

            List<PACK_PRINTLABELResult > dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PACK_PRINTLABEL(dbPara);
                if (null != dbResults)
                {
                    results = new List<PACK_PRINTLABEL>();
                    foreach (PACK_PRINTLABELResult dbResult in dbResults)
                    {
                        PACK_PRINTLABEL inst = new PACK_PRINTLABEL();

                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        
                        inst.GRADE = dbResult.GRADE;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;

                        inst.QUANTITY = dbResult.QUANTITY;
                        inst.DESCRIPTION = dbResult.DESCRIPTION;
                        inst.SUPPLIERCODE = dbResult.SUPPLIERCODE;
                        inst.BARCODEBACTHNO = dbResult.BARCODEBACTHNO;
                        inst.CUSTOMERPARTNO = dbResult.CUSTOMERPARTNO;
                        inst.BATCHNO = dbResult.BATCHNO;
                        inst.PDATE = dbResult.PDATE;

                        inst.CUSTOMERID = dbResult.CUSTOMERID;

                        //INC เพิ่มเอง
                        inst.FINISHINGPROCESS = dbResult.FINISHINGPROCESS;

                        inst.DBARCODE = dbResult.DBARCODE;
                        inst.BDate = dbResult.BDate;
                        inst.CUSPARTNO2D = dbResult.CUSPARTNO2D;

                        if (!string.IsNullOrEmpty(inst.CUSTOMERID))
                        {
                            if (inst.CUSTOMERID == "09")
                                inst.BarcodeCMPARTNO = "P" + dbResult.CUSTOMERPARTNO;
                            else
                                inst.BarcodeCMPARTNO = dbResult.CUSTOMERPARTNO;
                        }
                        else
                        {
                            inst.BarcodeCMPARTNO = dbResult.CUSTOMERPARTNO;
                        }

                        // เพิ่ม 24/06/25
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;

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

        #region PACK_UPDATEPACKINGPALLET

        public void PACK_UPDATEPACKINGPALLET(string PALLET, string OPERATOR, DateTime? CHECKDATE
            , string REMARK)
        {
            if (string.IsNullOrWhiteSpace(PALLET))
                return;

            if (!HasConnection())
                return;

            PACK_UPDATEPACKINGPALLETParameter dbPara = new PACK_UPDATEPACKINGPALLETParameter();
            dbPara.P_PALLET = PALLET;
            dbPara.P_OPERATOR = OPERATOR;
            dbPara.P_CHECKDATE = CHECKDATE;
            dbPara.P_REMARK = REMARK;

            PACK_UPDATEPACKINGPALLETResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PACK_UPDATEPACKINGPALLET(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #region PACK_CHECKPRINTLABEL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INSLOT"></param>
        /// <returns></returns>
        public DateTime? PACK_CHECKPRINTLABEL(string P_INSLOT)
        {
            DateTime? result = null;

            if (string.IsNullOrWhiteSpace(P_INSLOT))
                return result;

            if (!HasConnection())
                return result;

            PACK_CHECKPRINTLABELParameter dbPara = new PACK_CHECKPRINTLABELParameter();
            dbPara.P_INSLOT = P_INSLOT;

            PACK_CHECKPRINTLABELResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PACK_CHECKPRINTLABEL(dbPara);

                if (null != dbResult)
                {
                    result = dbResult.PRINTDATE;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = null;
            }

            return result;
        }
        #endregion

        #region PACK_CANCELPALLET

        public bool PACK_CANCELPALLET(string P_PALLETNO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_PALLETNO))
                return result;

            if (!HasConnection())
                return result;

            PACK_CANCELPALLETParameter dbPara = new PACK_CANCELPALLETParameter();
            dbPara.P_PALLETNO = P_PALLETNO;

            PACK_CANCELPALLETResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PACK_CANCELPALLET(dbPara);

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

        #region PACK_EDITPACKINGPALLETDETAIL

        public bool PACK_EDITPACKINGPALLETDETAIL(string P_PALLETNO, decimal? P_ORDERNO, string P_INSLOT_OLD, string P_INSLOT_NEW,
        string P_ITMCODE,string P_GRADE, decimal? P_NETLENGTH,decimal? P_GROSSLENGTH,decimal? P_NETWEIGHT,decimal? P_GROSSWEIGHT,
        string P_CUSTYPE, DateTime? P_INSPECTDATE, string P_LOADTYPE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_PALLETNO))
                return result;

            if (!HasConnection())
                return result;

            PACK_EDITPACKINGPALLETDETAILParameter dbPara = new PACK_EDITPACKINGPALLETDETAILParameter();
            dbPara.P_PALLETNO = P_PALLETNO;
            dbPara.P_ORDERNO = P_ORDERNO;
            dbPara.P_INSLOT_OLD = P_INSLOT_OLD;
            dbPara.P_INSLOT_NEW = P_INSLOT_NEW;
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_GRADE = P_GRADE;
            dbPara.P_NETLENGTH = P_NETLENGTH;
            dbPara.P_GROSSLENGTH = P_GROSSLENGTH;
            dbPara.P_NETWEIGHT = P_NETWEIGHT;
            dbPara.P_GROSSWEIGHT = P_GROSSWEIGHT;
            dbPara.P_CUSTYPE = P_CUSTYPE;
            dbPara.P_INSPECTDATE = P_INSPECTDATE;
            dbPara.P_LOADTYPE = P_LOADTYPE;

            PACK_EDITPACKINGPALLETDETAILResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PACK_EDITPACKINGPALLETDETAIL(dbPara);

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

        #region LOG_INSERT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_COMPUTORNAME"></param>
        /// <param name="OPERATOR"></param>
        /// <param name="CHECKDATE"></param>
        /// <param name="P_LOT"></param>
        /// <param name="P_OPERATION"></param>
        /// <param name="P_OPERATORID"></param>
        /// <param name="P_PROCESSID"></param>
        public bool LOG_INSERT(string P_COMPUTORNAME, string OPERATOR
            , string P_LOT, string P_OPERATION, string P_OPERATORID, string P_PROCESSID)
        {
            bool chkLog = false;

            if (string.IsNullOrWhiteSpace(P_LOT))
                return chkLog;

            if (!HasConnection())
                return chkLog;

            LOG_INSERTParameter dbPara = new LOG_INSERTParameter();
            dbPara.P_COMPUTORNAME = P_COMPUTORNAME;
            dbPara.P_DATE = DateTime.Now;
            dbPara.P_LOT = P_LOT;
            dbPara.P_OPERATION = P_OPERATION;
            dbPara.P_OPERATORID = P_OPERATORID;
            dbPara.P_PROCESSID = P_PROCESSID;

            LOG_INSERTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LOG_INSERT(dbPara);
                chkLog = true;
            }
            catch (Exception ex)
            {
                ex.Err();
                chkLog = false;
            }

            return chkLog;
        }
        #endregion

        #endregion
    }

    #endregion
}

