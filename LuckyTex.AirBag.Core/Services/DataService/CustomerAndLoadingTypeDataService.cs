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
    #region CustomerAndLoadingType Data Service

    /// <summary>
    /// The data service for Packing process.
    /// </summary>
    public class CustomerAndLoadingTypeDataService : BaseDataService
    {
        #region Singelton

        private static CustomerAndLoadingTypeDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static CustomerAndLoadingTypeDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(CustomerAndLoadingTypeDataService))
                    {
                        _instance = new CustomerAndLoadingTypeDataService();
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
        private CustomerAndLoadingTypeDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~CustomerAndLoadingTypeDataService()
        {
        }

        #endregion

        #region Public Methods

        #region Create new Session

        public CustomerAndLoadingTypeSession GetSession(LogInResult loginResult)
        {
            CustomerAndLoadingTypeSession result = new CustomerAndLoadingTypeSession();
            result.Init(loginResult);
            return result;
        }

        #endregion

        #region เพิ่มใหม่ MASTER_CUSTOMERTYPELIST ใช้ในการ Load Customer

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MASTER_CUSTOMERTYPELIST> MASTER_CUSTOMERTYPELIST()
        {
            List<MASTER_CUSTOMERTYPELIST> results = null;

            if (!HasConnection())
                return results;

            MASTER_CUSTOMERTYPELISTParameter dbPara = new MASTER_CUSTOMERTYPELISTParameter();

            List<MASTER_CUSTOMERTYPELISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.MASTER_CUSTOMERTYPELIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<MASTER_CUSTOMERTYPELIST>();
                    foreach (MASTER_CUSTOMERTYPELISTResult dbResult in dbResults)
                    {
                        MASTER_CUSTOMERTYPELIST inst = new MASTER_CUSTOMERTYPELIST();

                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;

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

        #region เพิ่มใหม่ MASTER_GETLOADINGBYCUSTYPE ใช้ในการ Load Loading Type

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MASTER_GETLOADINGBYCUSTYPE> MASTER_GETLOADINGBYCUSTYPE(string P_CUSTYPE)
        {
            List<MASTER_GETLOADINGBYCUSTYPE> results = null;

            if (!HasConnection())
                return results;

            MASTER_GETLOADINGBYCUSTYPEParameter dbPara = new MASTER_GETLOADINGBYCUSTYPEParameter();
            dbPara.P_CUSTYPE = P_CUSTYPE;

            List<MASTER_GETLOADINGBYCUSTYPEResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.MASTER_GETLOADINGBYCUSTYPE(dbPara);
                if (null != dbResults)
                {
                    results = new List<MASTER_GETLOADINGBYCUSTYPE>();
                    foreach (MASTER_GETLOADINGBYCUSTYPEResult dbResult in dbResults)
                    {
                        MASTER_GETLOADINGBYCUSTYPE inst = new MASTER_GETLOADINGBYCUSTYPE();

                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;

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

        #region MASTER_EDITCUSLOADTYPE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_CUSTYPE"></param>
        /// <param name="P_LOADTYPE"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public string MASTER_EDITCUSLOADTYPE(string P_CUSTYPE, string P_LOADTYPE, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_CUSTYPE))
                return result;

            if (string.IsNullOrWhiteSpace(P_LOADTYPE))
                return result;

            if (!HasConnection())
                return result;

            MASTER_EDITCUSLOADTYPEParameter dbPara = new MASTER_EDITCUSLOADTYPEParameter();
            dbPara.P_CUSTYPE = P_CUSTYPE;
            dbPara.P_LOADTYPE = P_LOADTYPE;
            dbPara.P_OPERATOR = P_OPERATOR;

            MASTER_EDITCUSLOADTYPEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.MASTER_EDITCUSLOADTYPE(dbPara);

                if (!string.IsNullOrEmpty(dbResult.RESULT))
                    result = dbResult.RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion

        #region MASTER_EDITCUS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_CUSTYPE"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public string MASTER_EDITCUS(string P_CUSTYPE, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_CUSTYPE))
                return result;

            if (!HasConnection())
                return result;

            MASTER_EDITCUSLOADTYPEParameter dbPara = new MASTER_EDITCUSLOADTYPEParameter();
            dbPara.P_CUSTYPE = P_CUSTYPE;
            dbPara.P_OPERATOR = P_OPERATOR;

            MASTER_EDITCUSLOADTYPEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.MASTER_EDITCUSLOADTYPE(dbPara);

                if (!string.IsNullOrEmpty(dbResult.RESULT))
                    result = dbResult.RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion

        #region MASTER_DELETECUSLOADTYPE
       /// <summary>
       /// 
       /// </summary>
       /// <param name="P_CUSTYPE"></param>
       /// <returns></returns>
        public bool MASTER_DELETECUS(string P_CUSTYPE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_CUSTYPE))
                return result;

            if (!HasConnection())
                return result;

            MASTER_DELETECUSLOADTYPEParameter dbPara = new MASTER_DELETECUSLOADTYPEParameter();
            dbPara.P_CUSTYPE = P_CUSTYPE;

            MASTER_DELETECUSLOADTYPEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.MASTER_DELETECUSLOADTYPE(dbPara);

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

        #region MASTER_DELETECUSLOADTYPE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_LOADTYPE"></param>
        /// <returns></returns>
        public bool MASTER_DELETELOADTYPE( string P_LOADTYPE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_LOADTYPE))
                return result;

            if (!HasConnection())
                return result;

            MASTER_DELETECUSLOADTYPEParameter dbPara = new MASTER_DELETECUSLOADTYPEParameter();
            dbPara.P_LOADTYPE = P_LOADTYPE;

            MASTER_DELETECUSLOADTYPEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.MASTER_DELETECUSLOADTYPE(dbPara);

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