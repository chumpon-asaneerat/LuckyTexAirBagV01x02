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
    #region G3 Data Service

    /// <summary>
    /// The data service for Master data.
    /// </summary>
    public class G3DataService : BaseDataService
    {
        #region Singelton

        private static G3DataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static G3DataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(G3DataService))
                    {
                        _instance = new G3DataService();
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
        private G3DataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~G3DataService()
        {
        }

        #endregion

        #region Public Methods

        #region เพิ่ม Save G3

        public bool SaveG3_yarn(string truckno, string desc, string palletNo, decimal? ch, decimal? weight,
         string lotorderNo, string itmorder, string receiveDate, string um, string itmyarn, string type)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(truckno))
                return result;
            if (string.IsNullOrWhiteSpace(desc))
                return result;
            if (string.IsNullOrWhiteSpace(itmyarn))
                return result;
            if (string.IsNullOrWhiteSpace(type))
                return result;
            if (string.IsNullOrWhiteSpace(palletNo))
                return result;

            if (!HasConnection())
                return result;

            G3_INSERTYARNParameter dbPara = new G3_INSERTYARNParameter();

            //PK truckno, desc ห้าม Null

            //dbPara.P_TRUCKNO = truckno;
            //dbPara.P_DESC = desc;

            //dbPara.P_PATTETNO = palletNo;
            //dbPara.P_CH = ch;
            //dbPara.P_WEIGHT = weight;
            //dbPara.P_LOTORDERNO = lotorderNo;
            //dbPara.P_ITMORDER = itmorder;
            //dbPara.P_RECEIVEDATE = receiveDate;
            //dbPara.P_UM = um;
            //dbPara.P_ITMYARN = itmyarn;
            //dbPara.P_TYPE = type;

            G3_INSERTYARNResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.G3_INSERTYARN(dbPara);

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

        #region เพิ่ม CheckITMYARN

        public bool CheckITMYARN(string itmyarn)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(itmyarn))
                return result;

            if (!HasConnection())
                return result;

            GETITEMCODEDATAParameter dbPara = new GETITEMCODEDATAParameter();
            dbPara.P_ITMYARN = itmyarn;

            List<GETITEMCODEDATAResult> dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.GETITEMCODEDATA(dbPara);

                if (null != dbResult)
                {
                    if (dbResult.Count > 0)
                        result = true;
                    else
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

        #region เพิ่มใหม่ G3SearchDataList ใช้ในการ Load G3

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<G3_SEARCHBYPALLETNOSearchData> G3_SearchByPalletNoDataList(string palletNo)
        {
            List<G3_SEARCHBYPALLETNOSearchData> results = null;

            if (!HasConnection())
                return results;

            G3_SEARCHBYPALLETNOParameter dbPara = new G3_SEARCHBYPALLETNOParameter();
            dbPara.P_PALLETNO = palletNo;

            List<G3_SEARCHBYPALLETNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.G3_SEARCHBYPALLETNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<G3_SEARCHBYPALLETNOSearchData>();
                    foreach (G3_SEARCHBYPALLETNOResult dbResult in dbResults)
                    {
                        G3_SEARCHBYPALLETNOSearchData inst = new G3_SEARCHBYPALLETNOSearchData();

                        inst.ENTRYDATE = dbResult.ENTRYDATE;
                        inst.ITM_YARN = dbResult.ITM_YARN;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.YARNTYPE = dbResult.YARNTYPE;
                        inst.WEIGHTQTY = dbResult.WEIGHTQTY;
                        inst.CONECH = dbResult.CONECH;
                        inst.VERIFY = dbResult.VERIFY;
                        inst.REMAINQTY = dbResult.REMAINQTY;
                        inst.RECEIVEBY = dbResult.RECEIVEBY;
                        inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.UPDATEDATE = dbResult.UPDATEDATE;
                        inst.PALLETTYPE = dbResult.PALLETTYPE;
                        inst.ITM400 = dbResult.ITM400;
                        inst.UM = dbResult.UM;
                        inst.PACKAING = dbResult.PACKAING;
                        inst.CLEAN = dbResult.CLEAN;
                        inst.TEARING = dbResult.TEARING;
                        inst.FALLDOWN = dbResult.FALLDOWN;
                        inst.CERTIFICATION = dbResult.CERTIFICATION;
                        inst.INVOICE = dbResult.INVOICE;
                        inst.IDENTIFYAREA = dbResult.IDENTIFYAREA;
                        inst.AMOUNTPALLET = dbResult.AMOUNTPALLET;
                        inst.OTHER = dbResult.OTHER;
                        inst.ACTION = dbResult.ACTION;
                        inst.MOVEMENTDATE = dbResult.MOVEMENTDATE;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.TRACENO = dbResult.TRACENO;

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

        #region เพิ่มใหม่ G3_SearchByTRACENO ใช้ในการ Load G3

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public G3_SEARCHBYPALLETNOSearchData G3_SearchByTRACENO(string TRACENO)
        {
            G3_SEARCHBYPALLETNOSearchData results = null;

            if (!HasConnection())
                return results;

            G3_SEARCHBYPALLETNOParameter dbPara = new G3_SEARCHBYPALLETNOParameter();
            List<G3_SEARCHBYPALLETNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.G3_SEARCHBYPALLETNO(dbPara);
                if (null != dbResults)
                {
                    results = new G3_SEARCHBYPALLETNOSearchData();
                    foreach (G3_SEARCHBYPALLETNOResult dbResult in dbResults)
                    {
                        if (!string.IsNullOrEmpty(dbResult.TRACENO))
                        {
                            if (TRACENO == dbResult.TRACENO)
                            {
                                results.ENTRYDATE = dbResult.ENTRYDATE;
                                results.ITM_YARN = dbResult.ITM_YARN;
                                results.PALLETNO = dbResult.PALLETNO;
                                results.YARNTYPE = dbResult.YARNTYPE;
                                results.WEIGHTQTY = dbResult.WEIGHTQTY;
                                results.CONECH = dbResult.CONECH;
                                results.VERIFY = dbResult.VERIFY;
                                results.REMAINQTY = dbResult.REMAINQTY;
                                results.RECEIVEBY = dbResult.RECEIVEBY;
                                results.RECEIVEDATE = dbResult.RECEIVEDATE;
                                results.FINISHFLAG = dbResult.FINISHFLAG;
                                results.UPDATEDATE = dbResult.UPDATEDATE;
                                results.PALLETTYPE = dbResult.PALLETTYPE;

                                if (!string.IsNullOrEmpty(dbResult.ITM400))
                                {
                                    results.ITM400 = dbResult.ITM400;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(results.ITM_YARN))
                                        results.ITM400 = ITM_GETITEMYARN400(results.ITM_YARN);
                                }

                                results.UM = dbResult.UM;
                                results.PACKAING = dbResult.PACKAING;
                                results.CLEAN = dbResult.CLEAN;
                                results.TEARING = dbResult.TEARING;
                                results.FALLDOWN = dbResult.FALLDOWN;
                                results.CERTIFICATION = dbResult.CERTIFICATION;
                                results.INVOICE = dbResult.INVOICE;
                                results.IDENTIFYAREA = dbResult.IDENTIFYAREA;
                                results.AMOUNTPALLET = dbResult.AMOUNTPALLET;
                                results.OTHER = dbResult.OTHER;
                                results.ACTION = dbResult.ACTION;
                                results.MOVEMENTDATE = dbResult.MOVEMENTDATE;
                                results.LOTNO = dbResult.LOTNO;
                                results.TRACENO = dbResult.TRACENO;

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

        #region เพิ่ม G3_UPDATEYARN
        /// <summary>
        /// 
        /// </summary>
        /// <param name="palletNo"></param>
        /// <param name="lotorderNo"></param>
        /// <param name="verify"></param>
        /// <param name="flag"></param>
        /// <param name="operatorID"></param>
        /// <param name="receiveDate"></param>
        /// <param name="packaging"></param>
        /// <param name="clean"></param>
        /// <param name="tearing"></param>
        /// <param name="falldown"></param>
        /// <param name="certification"></param>
        /// <param name="invoice"></param>
        /// <param name="identifyarea"></param>
        /// <param name="amountpallet"></param>
        /// <param name="other"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        //public bool G3_UPDATEYARN(string palletNo, string lotorderNo, string verify, string flag, string operatorID, DateTime? receiveDate,
        //    string packaging, string clean, string tearing, string falldown, string certification, string invoice
        //, string identifyarea, string amountpallet, string other, string action)
        //{
            //bool result = false;

            //if (string.IsNullOrWhiteSpace(palletNo))
            //    return result;

            //if (string.IsNullOrWhiteSpace(lotorderNo))
            //    return result;

            //if (string.IsNullOrWhiteSpace(verify))
            //    return result;

            //if (string.IsNullOrWhiteSpace(operatorID))
            //    return result;

            //if (!HasConnection())
            //    return result;

            //G3_UPDATEYARNParameter dbPara = new G3_UPDATEYARNParameter();

            //dbPara.P_PATTETNO = palletNo;
            //dbPara.P_LOTORDERNO = lotorderNo;
            //dbPara.P_VERIFY = verify;
            //dbPara.P_FLAG = flag;
            //dbPara.P_OPERATORID = operatorID;
            //dbPara.P_RECEIVEDATE = receiveDate;
            //dbPara.P_PACKAGING = packaging;
            //dbPara.P_CLEAN = clean;
            //dbPara.P_TEARING = tearing;
            //dbPara.P_FALLDOWN = falldown;
            //dbPara.P_CERTIFICATION = certification;
            //dbPara.P_INVOICE = invoice;
            //dbPara.P_IDENTIFYAREA = identifyarea;
            //dbPara.P_AMOUNTPALLET = amountpallet;
            //dbPara.P_OTHER = other;
            //dbPara.P_ACTION = action;

        //    G3_UPDATEYARNResult dbResult = null;
        //    try
        //    {
        //        dbResult = DatabaseManager.Instance.G3_UPDATEYARN(dbPara);

        //        if (null != dbResult)
        //        {
        //            result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Err();
        //        result = false;
        //    }

        //    return result;
        //}

        #endregion

        #region GetItemYarnData
        // เพิ่มเพื่อเรียก ItemYarn อย่างเดียว
        /// <summary>
        /// Gets all Inspection Machines.
        /// </summary>
        public List<ItemYarnItem> GetItemYarnData()
        {
            List<ItemYarnItem> results = new List<ItemYarnItem>();

            // Inspection Process ID = 8
            List<ITM_GETITEMYARNLISTResult> dbResults = this.GetItemYarn();
            if (null == dbResults || dbResults.Count <= 0)
                return results;

            ItemYarnItem inst = null;
            foreach (ITM_GETITEMYARNLISTResult dbResult in dbResults)
            {
                inst = new ItemYarnItem();

                inst.ITM_YARN = dbResult.ITM_YARN;

                results.Add(inst);
            }

            return results;
        }

        #endregion

        #region ITM_GETITEMYARN400
       
        public string ITM_GETITEMYARN400(string P_ITEMYARN)
        {
            string results = string.Empty;

            if (!HasConnection())
                return results;

            ITM_GETITEMYARN400Parameter dbPara = new ITM_GETITEMYARN400Parameter();
            dbPara.P_ITEMYARN = P_ITEMYARN;

            List<ITM_GETITEMYARN400Result> lot = DatabaseManager.Instance.ITM_GETITEMYARN400(dbPara);

            if (lot != null && lot.Count > 0)
            {
                results = lot[0].ITM_YARN;
            }

            return results;
        }

        #endregion

        #region GetG3_SEARCHYARNSTOCKData

        // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ Search G3_SEARCHYARNSTOCK Data
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_date"></param>
        /// <param name="_ITMYARN"></param>
        /// <returns></returns>
        public List<G3_SEARCHYARNSTOCKData> GetG3_SEARCHYARNSTOCKData(string _date, string _ITMYARN, string _YARNTYPE)
        {
            List<G3_SEARCHYARNSTOCKData> results = null;

            if (!HasConnection())
                return results;

            G3_SEARCHYARNSTOCKParameter dbPara = new G3_SEARCHYARNSTOCKParameter();
            List<G3_SEARCHYARNSTOCKResult> dbResults = null;
            dbPara.P_RECDATE = _date;
            dbPara.P_ITMYARN = _ITMYARN;
            dbPara.P_YARNTYPE = _YARNTYPE;

            try
            {
                dbResults = DatabaseManager.Instance.G3_SEARCHYARNSTOCK(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    int i = 1;

                    results = new List<G3_SEARCHYARNSTOCKData>();
                    foreach (G3_SEARCHYARNSTOCKResult dbResult in dbResults)
                    {
                        G3_SEARCHYARNSTOCKData inst = new G3_SEARCHYARNSTOCKData();

                        //เพิ่มใหม่ SelectData
                        inst.SelectData = false;
                        //เพิ่ม 08/07/16
                        inst.RowNo = i++;

                        inst.ENTRYDATE = dbResult.ENTRYDATE;
                        inst.ITM_YARN = dbResult.ITM_YARN;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.YARNTYPE = dbResult.YARNTYPE;
                        inst.WEIGHTQTY = dbResult.WEIGHTQTY;
                        inst.CONECH = dbResult.CONECH;
                        inst.VERIFY = dbResult.VERIFY;
                        inst.REMAINQTY = dbResult.REMAINQTY;
                        inst.RECEIVEBY = dbResult.RECEIVEBY;
                        inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.UPDATEDATE = dbResult.UPDATEDATE;
                        inst.PALLETTYPE = dbResult.PALLETTYPE;
                        inst.ITM400 = dbResult.ITM400;
                        inst.UM = dbResult.UM;
                        inst.PACKAING = dbResult.PACKAING;
                        inst.CLEAN = dbResult.CLEAN;
                        inst.TEARING = dbResult.TEARING;
                        inst.FALLDOWN = dbResult.FALLDOWN;
                        inst.CERTIFICATION = dbResult.CERTIFICATION;
                        inst.INVOICE = dbResult.INVOICE;
                        inst.IDENTIFYAREA = dbResult.IDENTIFYAREA;
                        inst.AMOUNTPALLET = dbResult.AMOUNTPALLET;
                        inst.OTHER = dbResult.OTHER;
                        inst.ACTION = dbResult.ACTION;
                        inst.MOVEMENTDATE = dbResult.MOVEMENTDATE;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.TRACENO = dbResult.TRACENO;

                        //เพิ่ม 08/07/16
                        inst.KGPERCH = dbResult.KGPERCH;

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

        #region G3_GETDATAAS400
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DTTRA"></param>
        /// <param name="DTINP"></param>
        /// <param name="CDCON"></param>
        /// <param name="BLELE"></param>
        /// <param name="CDUM0"></param>
        /// <param name="CDKE1"></param>
        /// <param name="CDLOT"></param>
        /// <param name="CDQUA"></param>
        /// <param name="TECU1"></param>
        /// <param name="TECU2"></param>
        /// <param name="TECU3"></param>
        /// <param name="TECU4"></param>
        /// <param name="TECU5"></param>
        /// <param name="TECU6"></param>
        /// <returns></returns>
        public string G3_GETDATAAS400(DateTime? DTTRA,DateTime? DTINP,string CDCON,decimal? BLELE,string CDUM0,string CDKE1,
        string CDLOT, string CDQUA, decimal? TECU1, decimal? TECU2, decimal? TECU3, decimal? TECU4, decimal? TECU5, string TECU6)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(CDCON))
                return result;

            if (string.IsNullOrWhiteSpace(CDKE1))
                return result;

            if (!HasConnection())
                return result;

            G3_GETDATAAS400Parameter dbPara = new G3_GETDATAAS400Parameter();

            dbPara.DTTRA = DTTRA;
            dbPara.DTINP = DTINP;
            dbPara.CDCON = CDCON;
            dbPara.BLELE = BLELE;
            dbPara.CDUM0 = CDUM0;
            dbPara.CDKE1 = CDKE1;
            dbPara.CDLOT = CDLOT;
            dbPara.CDQUA = CDQUA;
            dbPara.TECU1 = TECU1;
            dbPara.TECU2 = TECU2;
            dbPara.TECU3 = TECU3;
            dbPara.TECU4 = TECU4;
            dbPara.TECU5 = TECU5;
            dbPara.TECU6 = TECU6;

            G3_GETDATAAS400Result dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.G3_GETDATAAS400(dbPara);

                if (dbResult != null)
                    result = dbResult.R_RESULT;
                else
                    result = "Can't Insert PALLETNO = " + CDCON + " , ITM400 = "+ CDKE1;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region G3_GETDATAD365
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DTTRA"></param>
        /// <param name="DTINP"></param>
        /// <param name="CDCON"></param>
        /// <param name="BLELE"></param>
        /// <param name="CDUM0"></param>
        /// <param name="CDKE1"></param>
        /// <param name="CDLOT"></param>
        /// <param name="CDQUA"></param>
        /// <param name="TECU1"></param>
        /// <param name="TECU2"></param>
        /// <param name="TECU3"></param>
        /// <param name="TECU4"></param>
        /// <param name="TECU5"></param>
        /// <param name="TECU6"></param>
        /// <returns></returns>
        public string G3_GETDATAD365(DateTime? DTTRA, DateTime? DTINP, string CDCON, decimal? BLELE, string CDUM0, string CDKE1, string CDKE2,
        string CDLOT, string CDQUA, decimal? TECU1, decimal? TECU2, decimal? TECU3, decimal? TECU4, decimal? TECU5, string TECU6)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(CDCON))
                return result;

            if (string.IsNullOrWhiteSpace(CDKE1))
                return result;

            if (!HasConnection())
                return result;

            G3_GETDATAD365Parameter dbPara = new G3_GETDATAD365Parameter();

            dbPara.DTTRA = DTTRA;
            dbPara.DTINP = DTINP;
            dbPara.CDCON = CDCON;
            dbPara.BLELE = BLELE;
            dbPara.CDUM0 = CDUM0;
            dbPara.CDKE1 = CDKE1;
            dbPara.CDKE2 = CDKE2;
            dbPara.CDLOT = CDLOT;
            dbPara.CDQUA = CDQUA;
            dbPara.TECU1 = TECU1;
            dbPara.TECU2 = TECU2;
            dbPara.TECU3 = TECU3;
            dbPara.TECU4 = TECU4;
            dbPara.TECU5 = TECU5;
            dbPara.TECU6 = TECU6;

            G3_GETDATAD365Result dbResult = null;

            try
            {
                dbResult = DatabaseManager.Instance.G3_GETDATAD365(dbPara);

                if (dbResult != null)
                    result = dbResult.R_RESULT;
                else
                    result = "Can't Insert PALLETNO = " + CDCON + " , ITMD365 = " + CDKE1;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region เพิ่ม G3_RECEIVEYARN

        public bool G3_RECEIVEYARN(string P_TRACENO, string P_LOTNO,string P_VERIFY,decimal? P_REMAINQTY,
        string P_FLAG,string P_OPERATORID, DateTime? P_RECEIVEDATE,DateTime? P_UPDATEDATE, string P_TYPE,string P_PACKAGING, string P_CLEAN,string P_TEARING,
        string P_FALLDOWN, string P_CERTIFICATION, string P_INVOICE, string P_IDENTIFYAREA,string P_AMOUNTPALLET,string P_OTHER,string P_ACTION)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_TRACENO))
                return result;

            if (string.IsNullOrWhiteSpace(P_LOTNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_VERIFY))
                return result;

            if (string.IsNullOrWhiteSpace(P_OPERATORID))
                return result;

            if (!HasConnection())
                return result;

            G3_RECEIVEYARNParameter dbPara = new G3_RECEIVEYARNParameter();
            dbPara.P_TRACENO = P_TRACENO;
            dbPara.P_LOTNO = P_LOTNO;
            dbPara.P_VERIFY = P_VERIFY;
            dbPara.P_REMAINQTY = P_REMAINQTY;
            dbPara.P_FLAG = P_FLAG;
            dbPara.P_OPERATORID = P_OPERATORID;
            dbPara.P_RECEIVEDATE = P_RECEIVEDATE;
            dbPara.P_UPDATEDATE = P_UPDATEDATE;
            dbPara.P_TYPE = P_TYPE;
            dbPara.P_PACKAGING = P_PACKAGING;
            dbPara.P_CLEAN = P_CLEAN;
            dbPara.P_TEARING = P_TEARING;
            dbPara.P_FALLDOWN = P_FALLDOWN;
            dbPara.P_CERTIFICATION = P_CERTIFICATION;
            dbPara.P_INVOICE = P_INVOICE;
            dbPara.P_IDENTIFYAREA = P_IDENTIFYAREA;
            dbPara.P_AMOUNTPALLET = P_AMOUNTPALLET;
            dbPara.P_OTHER = P_OTHER;
            dbPara.P_ACTION = P_ACTION;

            G3_RECEIVEYARNResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.G3_RECEIVEYARN(dbPara);

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

        #region G3_GETREQUESTNODETAIL
        public List<G3_GETREQUESTNODETAIL> G3_GETREQUESTNODETAIL(string REQUESTNO)
        {
            List<G3_GETREQUESTNODETAIL> results = null;

            if (!HasConnection())
                return results;

            G3_GETREQUESTNODETAILParameter dbPara = new G3_GETREQUESTNODETAILParameter();
            List<G3_GETREQUESTNODETAILResult> dbResults = null;
            dbPara.P_REQUESTNO = REQUESTNO;

            try
            {
                dbResults = DatabaseManager.Instance.G3_GETREQUESTNODETAIL(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    int i = 1;
                    results = new List<G3_GETREQUESTNODETAIL>();
                    foreach (G3_GETREQUESTNODETAILResult dbResult in dbResults)
                    {
                        G3_GETREQUESTNODETAIL inst = new G3_GETREQUESTNODETAIL();

                        inst.RowNo = i++;
                        inst.ISSUEDATE = dbResult.ISSUEDATE;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.TRACENO = dbResult.TRACENO;
                        inst.WEIGHT = dbResult.WEIGHT;
                        inst.CH = dbResult.CH;
                        inst.ISSUEBY = dbResult.ISSUEBY;
                        inst.ISSUETO = dbResult.ISSUETO;
                        inst.REQUESTNO = dbResult.REQUESTNO;
                        inst.PALLETTYPE = dbResult.PALLETTYPE;
                        inst.ITM_YARN = dbResult.ITM_YARN;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.YARNTYPE = dbResult.YARNTYPE;
                        inst.ITM400 = dbResult.ITM400;
                        inst.ENTRYDATE = dbResult.ENTRYDATE;
                        inst.PACKAING = dbResult.PACKAING;
                        inst.CLEAN = dbResult.CLEAN;
                        inst.FALLDOWN = dbResult.FALLDOWN;
                        inst.TEARING = dbResult.TEARING;
                        inst.NewData = false;

                        inst.DELETEFLAG = dbResult.DELETEFLAG;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.EDITBY = dbResult.EDITBY;
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

        #region G3_GETPALLETDETAIL
        public List<G3_GETREQUESTNODETAIL> G3_GETPALLETDETAIL(string REQUESTNO,string PALLETNO, string ISSUEBY, string ISSUETO)
        {
            List<G3_GETREQUESTNODETAIL> results = null;

            if (!HasConnection())
                return results;

            G3_GETPALLETDETAILParameter dbPara = new G3_GETPALLETDETAILParameter();
            List<G3_GETPALLETDETAILResult> dbResults = null;
            dbPara.P_PALLETNO = PALLETNO;

            try
            {
                dbResults = DatabaseManager.Instance.G3_GETPALLETDETAIL(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    DateTime ISSUEDATE = DateTime.Now;
                    int i = 1;

                    results = new List<G3_GETREQUESTNODETAIL>();
                    foreach (G3_GETPALLETDETAILResult dbResult in dbResults)
                    {
                        G3_GETREQUESTNODETAIL inst = new G3_GETREQUESTNODETAIL();

                        inst.RowNo = i++;
                        inst.ISSUEDATE = ISSUEDATE;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.TRACENO = dbResult.TRACENO;
                        inst.WEIGHT = dbResult.WEIGHTQTY;
                        inst.CH = dbResult.CONECH;
                        inst.ISSUEBY = ISSUEBY;
                        inst.ISSUETO = ISSUETO;
                        inst.REQUESTNO = REQUESTNO;
                        inst.PALLETTYPE = dbResult.PALLETTYPE;
                        inst.ITM_YARN = dbResult.ITM_YARN;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.YARNTYPE = dbResult.YARNTYPE;
                        inst.ITM400 = dbResult.ITM400;
                        inst.ENTRYDATE = dbResult.ENTRYDATE;
                        inst.PACKAING = dbResult.PACKAING;
                        inst.CLEAN = dbResult.CLEAN;
                        inst.FALLDOWN = dbResult.FALLDOWN;
                        inst.TEARING = dbResult.TEARING;
                        inst.NewData = false;

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

        #region เพิ่ม G3_INSERTUPDATEISSUEYARN

        public bool G3_INSERTUPDATEISSUEYARN(string P_REQUESTNO, string P_PATTETNO, string P_TRACENO
            , decimal? P_CH, decimal? P_WEIGHT, DateTime? P_ISSUEDATE, string P_OPERATOR, string P_PALLETTYPE, string P_ISSUETO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_REQUESTNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_PATTETNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_TRACENO))
                return result;

            if (string.IsNullOrWhiteSpace(P_OPERATOR))
                return result;

            if (!HasConnection())
                return result;

            G3_INSERTUPDATEISSUEYARNParameter dbPara = new G3_INSERTUPDATEISSUEYARNParameter();

            dbPara.P_REQUESTNO = P_REQUESTNO;
            dbPara.P_PATTETNO = P_PATTETNO;
            dbPara.P_TRACENO = P_TRACENO;
            dbPara.P_CH = P_CH;
            dbPara.P_WEIGHT = P_WEIGHT;
            dbPara.P_ISSUEDATE = P_ISSUEDATE;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_PALLETTYPE = P_PALLETTYPE;
            dbPara.P_ISSUETO = P_ISSUETO;

            G3_INSERTUPDATEISSUEYARNResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.G3_INSERTUPDATEISSUEYARN(dbPara);

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

        #region เพิ่ม G3_INSERTRETURNYARN

        public bool G3_INSERTRETURNYARN(string P_TRACENO,string P_NEWTRACENO,
                decimal? P_CH,decimal? P_WEIGHT,DateTime? P_RECEIVEDATE,
                string P_OPERATOR,string P_ITEMYARN,string P_YARNTYPE,string P_RETURNBY)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_TRACENO))
                return result;

            if (string.IsNullOrWhiteSpace(P_NEWTRACENO))
                return result;

            if (string.IsNullOrWhiteSpace(P_OPERATOR))
                return result;

            if (!HasConnection())
                return result;

            G3_INSERTRETURNYARNParameter dbPara = new G3_INSERTRETURNYARNParameter();

            dbPara.P_TRACENO = P_TRACENO;
            dbPara.P_NEWTRACENO = P_NEWTRACENO;
            dbPara.P_TRACENO = P_TRACENO;
            dbPara.P_CH = P_CH;
            dbPara.P_WEIGHT = P_WEIGHT;
            dbPara.P_RECEIVEDATE = P_RECEIVEDATE;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_ITEMYARN = P_ITEMYARN;
            dbPara.P_YARNTYPE = P_YARNTYPE;
            dbPara.P_RETURNBY = P_RETURNBY;

            G3_INSERTRETURNYARNResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.G3_INSERTRETURNYARN(dbPara);

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

        #region เพิ่ม G3_CANCELREQUESTNO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_REQUESTNO"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public bool G3_CANCELREQUESTNO(string P_REQUESTNO,string P_OPERATOR)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_REQUESTNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_OPERATOR))
                return result;

            if (!HasConnection())
                return result;

            G3_CANCELREQUESTNOParameter dbPara = new G3_CANCELREQUESTNOParameter();

            dbPara.P_REQUESTNO = P_REQUESTNO;
            dbPara.P_OPERATOR = P_OPERATOR;
           
            G3_CANCELREQUESTNOResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.G3_CANCELREQUESTNO(dbPara);

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

        #region G3_Del

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_PALLETNO"></param>
        /// <param name="_TRACENO"></param>
        /// <param name="_ITM_YARN"></param>
        /// <param name="_LOTNO"></param>
        /// <returns></returns>
        public bool G3_Del(string _PALLETNO, string _TRACENO, string _ITM_YARN, string _LOTNO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(_PALLETNO))
                return result;

            if (string.IsNullOrWhiteSpace(_TRACENO))
                return result;

            if (string.IsNullOrWhiteSpace(_ITM_YARN))
                return result;

            if (string.IsNullOrWhiteSpace(_LOTNO))
                return result;

            if (!HasConnection())
                return result;

            G3_DeleteParameter dbPara = new G3_DeleteParameter();
            dbPara.PALLETNO = _PALLETNO;
            dbPara.TRACENO = _TRACENO;
            dbPara.ITM_YARN = _ITM_YARN;
            dbPara.LOTNO = _LOTNO;


            G3_DeleteResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.G3_Delete(dbPara);

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

        #endregion
    }

    #endregion
}





