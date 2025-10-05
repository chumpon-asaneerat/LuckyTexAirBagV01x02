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
    #region DefectCodeService

    /// <summary>
    /// The data service for Packing process.
    /// </summary>
    public class DefectCodeService : BaseDataService
    {
        #region Singelton

        private static DefectCodeService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static DefectCodeService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(DefectCodeService))
                    {
                        _instance = new DefectCodeService();
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
        private DefectCodeService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~DefectCodeService()
        {
        }

        #endregion

        #region Public Methods

        #region Create new Session

        public DefectCodeSession GetSession(LogInResult loginResult)
        {
            DefectCodeSession result = new DefectCodeSession();
            result.Init(loginResult);
            return result;
        }

        #endregion

        #region เพิ่มใหม่ GetProcessList ใช้ในการ Load Process

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MASTER_AIRBAGPROCESSLIST> GetProcessList()
        {
            List<MASTER_AIRBAGPROCESSLIST> results = null;

            if (!HasConnection())
                return results;

            MASTER_AIRBAGPROCESSLISTParameter dbPara = new MASTER_AIRBAGPROCESSLISTParameter();

            List<MASTER_AIRBAGPROCESSLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.MASTER_AIRBAGPROCESSLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<MASTER_AIRBAGPROCESSLIST>();
                    foreach (MASTER_AIRBAGPROCESSLISTResult dbResult in dbResults)
                    {
                        MASTER_AIRBAGPROCESSLIST inst = new MASTER_AIRBAGPROCESSLIST();

                        inst.PROCESSID = dbResult.PROCESSID;
                        inst.PROCESSDESCRIPTION = dbResult.PROCESSDESCRIPTION;

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

        #region เพิ่มใหม่ DEFECT_SEARCH ใช้ในการ Load Defect

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DEFECT_SEARCH> DEFECT_SEARCH(string P_DEFECTID, string P_PROCESSID, string P_THAIDESC, string P_ENGDESC)
        {
            List<DEFECT_SEARCH> results = null;

            if (!HasConnection())
                return results;

            DEFECT_SEARCHParameter dbPara = new DEFECT_SEARCHParameter();

            dbPara.P_DEFECTID = P_DEFECTID;
            dbPara.P_PROCESSID = P_PROCESSID;
            dbPara.P_THAIDESC = P_THAIDESC;
            dbPara.P_ENGDESC = P_ENGDESC;

            List<DEFECT_SEARCHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.DEFECT_SEARCH(dbPara);
                if (null != dbResults)
                {
                    results = new List<DEFECT_SEARCH>();
                    foreach (DEFECT_SEARCHResult dbResult in dbResults)
                    {
                        DEFECT_SEARCH inst = new DEFECT_SEARCH();

                        inst.DEFECTCODE = dbResult.DEFECTCODE;
                        inst.DESCRIPTION_TH = dbResult.DESCRIPTION_TH;
                        inst.DESCRIPTION_EN = dbResult.DESCRIPTION_EN;
                        inst.PROCESSID = dbResult.PROCESSID;
                        inst.DEFECTPROCESSCODE = dbResult.DEFECTPROCESSCODE;
                        inst.POINT = dbResult.POINT;
                        inst.PROCESSDESCRIPTION = dbResult.PROCESSDESCRIPTION;

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

        #region DEFECT_INSERTUPDATE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OPERATOR"></param>
        public string DEFECT_INSERTUPDATE(string P_DEFECTID, string P_PROCESSID, string P_THAIDESC, string P_ENGDESC, decimal? P_POINT)
        {
            if (string.IsNullOrWhiteSpace(P_DEFECTID))
                return string.Empty;

            if (!HasConnection())
                return string.Empty;

            DEFECT_INSERTUPDATEParameter dbPara = new DEFECT_INSERTUPDATEParameter();
            dbPara.P_DEFECTID = P_DEFECTID;
            dbPara.P_PROCESSID = P_PROCESSID;
            dbPara.P_THAIDESC = P_THAIDESC;
            dbPara.P_ENGDESC = P_ENGDESC;
            dbPara.P_POINT = P_POINT;

            DEFECT_INSERTUPDATEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.DEFECT_INSERTUPDATE(dbPara);

                return dbResult.R_RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                return string.Empty;
            }
        }
        #endregion

        #region DEFECT_DELETE

        public bool DEFECT_DELETE(string P_DEFECTID)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_DEFECTID))
                return result;

            if (!HasConnection())
                return result;

            DEFECT_DELETEParameter dbPara = new DEFECT_DELETEParameter();

            dbPara.P_DEFECTID = P_DEFECTID;

            DEFECT_DELETEResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.DEFECT_DELETE(dbPara);

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

