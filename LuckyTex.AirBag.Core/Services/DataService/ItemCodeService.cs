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
    #region ItemCodeService

    /// <summary>
    /// The data service for ItemCode process.
    /// </summary>
    public class ItemCodeService : BaseDataService
    {
        #region Singelton

        private static ItemCodeService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static ItemCodeService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ItemCodeService))
                    {
                        _instance = new ItemCodeService();
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
        private ItemCodeService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~ItemCodeService()
        {
        }

        #endregion

        #region Public Methods

        #region Create new Session

        public ItemCodeSession GetSession(LogInResult loginResult)
        {
            ItemCodeSession result = new ItemCodeSession();
            result.Init(loginResult);
            return result;
        }

        #endregion

        #region เพิ่มใหม่ ITM_SEARCHITEMCODE ใช้ในการ Load Item

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_ITEMWEAV"></param>
        /// <param name="P_ITEMPREPARE"></param>
        /// <param name="P_ITEMYARN"></param>
        /// <param name="P_YARNCODE"></param>
        /// <returns></returns>
        public List<ITM_SEARCHITEMCODE> ITM_SEARCHITEMCODE(string P_ITEMCODE,string P_ITEMWEAV,string P_ITEMPREPARE,string P_ITEMYARN, string P_YARNCODE)
        {
            List<ITM_SEARCHITEMCODE> results = null;

            if (!HasConnection())
                return results;

            ITM_SEARCHITEMCODEParameter dbPara = new ITM_SEARCHITEMCODEParameter();
            dbPara.P_ITEMCODE = P_ITEMCODE;
            dbPara.P_ITEMWEAV = P_ITEMWEAV;
            dbPara.P_ITEMPREPARE = P_ITEMPREPARE;
            dbPara.P_ITEMYARN = P_ITEMYARN;
            dbPara.P_YARNCODE = P_YARNCODE;

            List<ITM_SEARCHITEMCODEResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.ITM_SEARCHITEMCODE(dbPara);
                if (null != dbResults)
                {
                    results = new List<ITM_SEARCHITEMCODE>();
                    foreach (ITM_SEARCHITEMCODEResult dbResult in dbResults)
                    {
                        ITM_SEARCHITEMCODE inst = new ITM_SEARCHITEMCODE();

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

                        #region PROC1

                        if (!string.IsNullOrEmpty(inst.ITM_PROC1))
                        {
                            inst.PROC1 = true;
                        }
                        else
                        {
                            inst.PROC1 = false;
                        }

                        #endregion

                        #region PROC2

                        if (!string.IsNullOrEmpty(inst.ITM_PROC2))
                        {
                            inst.PROC2 = true;
                        }
                        else
                        {
                            inst.PROC2 = false;
                        }

                        #endregion

                        #region PROC3

                        if (!string.IsNullOrEmpty(inst.ITM_PROC3))
                        {
                            inst.PROC3 = true;
                        }
                        else
                        {
                            inst.PROC3 = false;
                        }

                        #endregion

                        #region PROC4

                        if (!string.IsNullOrEmpty(inst.ITM_PROC4))
                        {
                            inst.PROC4 = true;
                        }
                        else
                        {
                            inst.PROC4 = false;
                        }

                        #endregion

                        #region PROC5

                        if (!string.IsNullOrEmpty(inst.ITM_PROC5))
                        {
                            inst.PROC5 = true;
                        }
                        else
                        {
                            inst.PROC5 = false;
                        }

                        #endregion

                        #region PROC6

                        if (!string.IsNullOrEmpty(inst.ITM_PROC6))
                        {
                            inst.PROC6 = true;
                        }
                        else
                        {
                            inst.PROC6 = false;
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

        #region ITM_INSERTUPDATEITEMCODE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_ITEMWEAV"></param>
        /// <param name="P_ITEMPREPARE"></param>
        /// <param name="P_ITEMYARN"></param>
        /// <param name="P_WIDTH"></param>
        /// <param name="P_WEAVEWIDTH"></param>
        /// <param name="P_COREWEIGHT"></param>
        /// <param name="P_YARNCODE"></param>
        /// <param name="P_PROC1"></param>
        /// <param name="P_PROC2"></param>
        /// <param name="P_PROC3"></param>
        /// <param name="P_PROC4"></param>
        /// <param name="P_PROC5"></param>
        /// <param name="P_PROC6"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public string ITM_INSERTUPDATEITEMCODE(string P_ITEMCODE, string P_ITEMWEAV, string P_ITEMPREPARE, string P_ITEMYARN,
        decimal? P_WIDTH, decimal? P_WEAVEWIDTH, decimal? P_COREWEIGHT, string P_YARNCODE, string P_PROC1, string P_PROC2,
        string P_PROC3, string P_PROC4, string P_PROC5, string P_PROC6, string P_OPERATOR)
        {
            if (string.IsNullOrWhiteSpace(P_ITEMCODE))
                return string.Empty;

            if (!HasConnection())
                return string.Empty;

            ITM_INSERTUPDATEITEMCODEParameter dbPara = new ITM_INSERTUPDATEITEMCODEParameter();
            dbPara.P_ITEMCODE = P_ITEMCODE;
            dbPara.P_ITEMWEAV = P_ITEMWEAV;
            dbPara.P_ITEMPREPARE = P_ITEMPREPARE;
            dbPara.P_ITEMYARN = P_ITEMYARN;
            dbPara.P_WIDTH = P_WIDTH;
            dbPara.P_WEAVEWIDTH = P_WEAVEWIDTH;
            dbPara.P_COREWEIGHT = P_COREWEIGHT;
            dbPara.P_YARNCODE = P_YARNCODE;
            dbPara.P_PROC1 = P_PROC1;
            dbPara.P_PROC2 = P_PROC2;
            dbPara.P_PROC3 = P_PROC3;
            dbPara.P_PROC4 = P_PROC4;
            dbPara.P_PROC5 = P_PROC5;
            dbPara.P_PROC6 = P_PROC6;
            dbPara.P_OPERATOR = P_OPERATOR;

            ITM_INSERTUPDATEITEMCODEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.ITM_INSERTUPDATEITEMCODE(dbPara);

                if (dbResult != null)
                    return dbResult.RESULT;
                else
                    return "Can't Save Item Code";
            }
            catch (Exception ex)
            {
                ex.Err();
                return string.Empty;
            }
        }

        #endregion

        #endregion
    }

    #endregion
}

