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
    #region User Operator Data Service

    /// <summary>
    /// The data service for User and Employee.
    /// </summary>
    public class UserDataService : BaseDataService
    {
        #region Singelton

        private static UserDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static UserDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(UserDataService))
                    {
                        _instance = new UserDataService();
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
        private UserDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~UserDataService()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets Operators.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public List<LogInResult> GetOperators(string userName, string password, int processId)
        {
            List<LogInResult> results = null;

            if (string.IsNullOrWhiteSpace(userName))
                return results;
            if (string.IsNullOrWhiteSpace(password))
                return results;

            if (!HasConnection())
                return results;

            GETOPERATORBYPROCESSIDParameter dbPara = new GETOPERATORBYPROCESSIDParameter();
            dbPara.P_PROCESSID = processId.ToString();
            dbPara.P_USER = userName;
            dbPara.P_PASS = password;
            dbPara.P_OPID = null;

            List<GETOPERATORBYPROCESSIDResult> dbResults = null;
            try
            {
                dbResults = DatabaseManager.Instance.GETOPERATORBYPROCESSID(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results = new List<LogInResult>();
                    LogInResult inst = null;
                    foreach (GETOPERATORBYPROCESSIDResult dbResult in dbResults)
                    {
                        inst = new LogInResult();

                        inst.OperatorId = dbResult.OPERATORID;
                        inst.ProcessId = dbResult.PROCESSID;
                        inst.PositionLevel = dbResult.POSITIONLEVEL;

                        inst.UserName = dbResult.USERNAME;
                        inst.Password = dbResult.PASSWORD;

                        inst.Title = dbResult.TITLE;
                        inst.FirstName = dbResult.FNAME;
                        inst.LastName = dbResult.LNAME;

                        inst.CreateBy = dbResult.CREATEDBY;
                        inst.CreateDate = dbResult.CREATEDDATE;
                        if (string.IsNullOrWhiteSpace(dbResult.DELETEFLAG))
                            inst.Deleted = true;
                        else if (dbResult.DELETEFLAG.Trim() == "1")
                            inst.Deleted = false;
                        else inst.Deleted = true; // deleted.

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

       /// <summary>
       /// 
       /// </summary>
       /// <param name="userName"></param>
       /// <param name="password"></param>
       /// <param name="processId"></param>
       /// <returns></returns>
        public string GETAUTHORIZEBYPROCESSID(string userName, string password, int processId)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(userName))
                return results;
            if (string.IsNullOrWhiteSpace(password))
                return results;

            if (!HasConnection())
                return results;

            GETAUTHORIZEBYPROCESSIDParameter dbPara = new GETAUTHORIZEBYPROCESSIDParameter();
            dbPara.P_PROCESSID = processId.ToString();
            dbPara.P_USER = userName;
            dbPara.P_PASS = password;

            GETAUTHORIZEBYPROCESSIDResult dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.GETAUTHORIZEBYPROCESSID(dbPara);
                if (null != dbResults && dbResults.R_OUT != null)
                {
                    if (dbResults.R_OUT != "")
                    {
                        results = dbResults.R_OUT;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        /// <summary>
        /// Gets Operators.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public string GetOperatorsDelete(string userName, string password, int processId)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(userName))
                return results;
            if (string.IsNullOrWhiteSpace(password))
                return results;

            if (!HasConnection())
                return results;

            GETAUTHORIZEBYPROCESSIDParameter dbPara = new GETAUTHORIZEBYPROCESSIDParameter();
            dbPara.P_PROCESSID = processId.ToString();
            dbPara.P_USER = userName;
            dbPara.P_PASS = password;


            GETAUTHORIZEBYPROCESSIDResult dbResults = null;
            try
            {
                dbResults = DatabaseManager.Instance.GETAUTHORIZEBYPROCESSID(dbPara);
                if (null != dbResults && dbResults.R_OUT != null)
                {
                    if (dbResults.R_OUT != "")
                    {
                        results = dbResults.R_OUT;
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
    }

    #endregion
}







