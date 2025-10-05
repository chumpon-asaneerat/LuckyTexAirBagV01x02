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
    #region Master Data Service

    /// <summary>
    /// The data service for Master data.
    /// </summary>
    public class QualityAssuranceService : BaseDataService
    {
        #region Singelton

        private static QualityAssuranceService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static QualityAssuranceService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(QualityAssuranceService))
                    {
                        _instance = new QualityAssuranceService();
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
        private QualityAssuranceService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~QualityAssuranceService()
        {
        }

        #endregion

        #region Public Methods

        #region เพิ่มใหม่ CUS_GETLIST ใช้ในการ Load Customer

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CUS_GETLISTData> GetCUS_GETLISTDataList()
        {
            List<CUS_GETLISTData> results = null;

            if (!HasConnection())
                return results;

            CUS_GETLISTParameter dbPara = new CUS_GETLISTParameter();

            List<CUS_GETLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.CUS_GETLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<CUS_GETLISTData>();
                    foreach (CUS_GETLISTResult dbResult in dbResults)
                    {
                        CUS_GETLISTData inst = new CUS_GETLISTData();

                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.CUSTOMERNAME = dbResult.CUSTOMERNAME;
                        inst.METHODID = dbResult.METHODID;
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

        #region เพิ่มใหม่ QA_SEARCHCHECKINGDATA ใช้ในการ Load QA_CHECKINGDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_CUSID"></param>
        /// <param name="P_DATE"></param>
        /// <param name="P_LABITMCODE"></param>
        /// <param name="P_RESULT"></param>
        /// <returns></returns>
        public List<QA_SEARCHCHECKINGDATA> QA_SEARCHCHECKINGDATA(string P_CUSID, string P_DATE, string P_LABITMCODE, string P_RESULT)
        {
            List<QA_SEARCHCHECKINGDATA> results = null;

            if (!HasConnection())
                return results;

            QA_SEARCHCHECKINGDATAParameter dbPara = new QA_SEARCHCHECKINGDATAParameter();

            if (!string.IsNullOrEmpty(P_CUSID))
                dbPara.P_CUSID = P_CUSID;

            if (!string.IsNullOrEmpty(P_DATE))
                dbPara.P_DATE = P_DATE;

            if (!string.IsNullOrEmpty(P_LABITMCODE))
                dbPara.P_LABITMCODE = P_LABITMCODE;

            if (!string.IsNullOrEmpty(P_RESULT))
                dbPara.P_RESULT = P_RESULT;

            List<QA_SEARCHCHECKINGDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.QA_SEARCHCHECKINGDATA(dbPara);
                if (null != dbResults)
                {
                    int? rowNo = 1;
                    results = new List<QA_SEARCHCHECKINGDATA>();
                    foreach (QA_SEARCHCHECKINGDATAResult dbResult in dbResults)
                    {
                        QA_SEARCHCHECKINGDATA inst = new QA_SEARCHCHECKINGDATA();

                        inst.RowNo = rowNo;
                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.LAB_ITMCODE = dbResult.LAB_ITMCODE;
                        inst.LAB_LOT = dbResult.LAB_LOT;
                        inst.LAB_BATCHNO = dbResult.LAB_BATCHNO;
                        inst.INS_ITMCODE = dbResult.INS_ITMCODE;
                        inst.INS_LOT = dbResult.INS_LOT;
                        inst.INS_BATCHNO = dbResult.INS_BATCHNO;
                        inst.CUS_CODE = dbResult.CUS_CODE;
                        inst.CHECK_RESULT = dbResult.CHECK_RESULT;
                        inst.CHECKDATE = dbResult.CHECKDATE;
                        inst.CHECKEDBY = dbResult.CHECKEDBY;
                        inst.CUSTOMERNAME = dbResult.CUSTOMERNAME;

                        inst.DELETEFLAG = dbResult.DELETEFLAG;
                        inst.DELETEBY = dbResult.DELETEBY;
                        inst.DELETEDATE = dbResult.DELETEDATE;
                        inst.SHIFT = dbResult.SHIFT;
                        inst.REMARK = dbResult.REMARK;

                        results.Add(inst);

                        rowNo++;
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

        #region เพิ่มใหม่ ITM_GETITEMBYITEMCODEANDCUSID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_CUSTOMERID"></param>
        /// <param name="P_ITMCODE"></param>
        /// <returns></returns>
        public List<ITM_GETITEMBYITEMCODEANDCUSIDDATA> ITM_GETITEMBYITEMCODEANDCUSID(string P_CUSTOMERID, string P_ITMCODE)
        {
            List<ITM_GETITEMBYITEMCODEANDCUSIDDATA> results = null;

            if (!HasConnection())
                return results;

            ITM_GETITEMBYITEMCODEANDCUSIDParameter dbPara = new ITM_GETITEMBYITEMCODEANDCUSIDParameter();
            dbPara.P_CUSTOMERID = P_CUSTOMERID;
            dbPara.P_ITMCODE = P_ITMCODE;


            List<ITM_GETITEMBYITEMCODEANDCUSIDResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.ITM_GETITEMBYITEMCODEANDCUSID(dbPara);
                if (null != dbResults)
                {
                    results = new List<ITM_GETITEMBYITEMCODEANDCUSIDDATA>();
                    foreach (ITM_GETITEMBYITEMCODEANDCUSIDResult dbResult in dbResults)
                    {
                        ITM_GETITEMBYITEMCODEANDCUSIDDATA inst = new ITM_GETITEMBYITEMCODEANDCUSIDDATA();


                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.PARTNO = dbResult.PARTNO;
                        inst.FABRIC = dbResult.FABRIC;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.DENSITY_W = dbResult.DENSITY_W;
                        inst.DENSITY_F = dbResult.DENSITY_F;
                        inst.WIDTH_ALL = dbResult.WIDTH_ALL;
                        inst.WIDTH_PIN = dbResult.WIDTH_PIN;
                        inst.WIDTH_COAT = dbResult.WIDTH_COAT;
                        inst.TRIM_L = dbResult.TRIM_L;
                        inst.TRIM_R = dbResult.TRIM_R;

                        inst.FLOPPY_L = dbResult.FLOPPY_L;
                        inst.FLOPPY_R = dbResult.FLOPPY_R;
                        inst.HARDNESS_L = dbResult.HARDNESS_L;
                        inst.HARDNESS_C = dbResult.HARDNESS_C;
                        inst.HARDNESS_R = dbResult.HARDNESS_R;
                        inst.UNWINDER = dbResult.UNWINDER;
                        inst.WINDER = dbResult.WINDER;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.DESCRIPTION = dbResult.DESCRIPTION;
                        inst.SUPPLIERCODE = dbResult.SUPPLIERCODE;
                        inst.WIDTH = dbResult.WIDTH;

                        // เพิ่ม 17/10/22
                        inst.WIDTH_SELVAGEL = dbResult.WIDTH_SELVAGEL;
                        inst.WIDTH_SELVAGER = dbResult.WIDTH_SELVAGER;
                        inst.RESETSTARTLENGTH = dbResult.RESETSTARTLENGTH;

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

        #region เพิ่ม Save QA_INSERTCHECKINGDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_CUSTOMERID"></param>
        /// <param name="P_LABITMCODE"></param>
        /// <param name="P_LABLOT"></param>
        /// <param name="P_LABBATCHNO"></param>
        /// <param name="P_INSITMCODE"></param>
        /// <param name="P_INSLOT"></param>
        /// <param name="P_INSBATCHNO"></param>
        /// <param name="P_CUSCODE"></param>
        /// <param name="P_RESULT"></param>
        /// <param name="P_CHECKDATE"></param>
        /// <param name="P_CHECKEDBY"></param>
        /// <param name="P_SHIFT"></param>
        /// <param name="P_REMARK"></param>
        /// <returns></returns>
        public bool QA_INSERTCHECKINGDATA(string P_CUSTOMERID,string P_LABITMCODE,string P_LABLOT,string P_LABBATCHNO,
        string P_INSITMCODE,string P_INSLOT,string P_INSBATCHNO,string P_CUSCODE,string P_RESULT,DateTime? P_CHECKDATE,string P_CHECKEDBY
         , string P_SHIFT , string P_REMARK)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_CUSTOMERID))
                return result;
            
            if (!HasConnection())
                return result;

            QA_INSERTCHECKINGDATAParameter dbPara = new QA_INSERTCHECKINGDATAParameter();
            dbPara.P_CUSTOMERID = P_CUSTOMERID;
            dbPara.P_LABITMCODE = P_LABITMCODE;
            dbPara.P_LABLOT = P_LABLOT;
            dbPara.P_LABBATCHNO = P_LABBATCHNO;
            dbPara.P_INSITMCODE = P_INSITMCODE;
            dbPara.P_INSLOT = P_INSLOT;
            dbPara.P_INSBATCHNO = P_INSBATCHNO;
            dbPara.P_CUSCODE = P_CUSCODE;
            dbPara.P_RESULT = P_RESULT;
            dbPara.P_CHECKDATE = P_CHECKDATE;
            dbPara.P_CHECKEDBY = P_CHECKEDBY;
            dbPara.P_SHIFT = P_SHIFT;
            dbPara.P_REMARK = P_REMARK;

            QA_INSERTCHECKINGDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.QA_INSERTCHECKINGDATA(dbPara);
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

        #region เพิ่ม Delete QA_DELETECHECKINGDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_LABITMCODE"></param>
        /// <param name="P_LABLOT"></param>
        /// <param name="P_LABBATCHNO"></param>
        /// <param name="P_CHECKDATE"></param>
        /// <param name="P_DELETEBY"></param>
        /// <param name="P_DELETEDATE"></param>
        /// <returns></returns>
        public bool QA_DELETECHECKINGDATA( string P_LABITMCODE, string P_LABLOT, string P_LABBATCHNO,DateTime? P_CHECKDATE, string P_DELETEBY , DateTime? P_DELETEDATE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_DELETEBY))
                return result;

            if (!HasConnection())
                return result;

            QA_DELETECHECKINGDATAParameter dbPara = new QA_DELETECHECKINGDATAParameter();
           
            dbPara.P_LABITMCODE = P_LABITMCODE;
            dbPara.P_LABLOT = P_LABLOT;
            dbPara.P_LABBATCHNO = P_LABBATCHNO;
            dbPara.P_CHECKDATE = P_CHECKDATE;
            dbPara.P_DELETEBY = P_DELETEBY;
            dbPara.P_DELETEDATE = P_DELETEDATE;

            QA_DELETECHECKINGDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.QA_DELETECHECKINGDATA(dbPara);
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







