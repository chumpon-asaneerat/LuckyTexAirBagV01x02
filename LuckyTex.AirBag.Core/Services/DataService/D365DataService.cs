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
    #region D365 Data Service

    /// <summary>
    /// The data service for User and D365.
    /// </summary>
    public class D365DataService : BaseDataService
    {
        #region Singelton

        private static D365DataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static D365DataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(D365DataService))
                    {
                        _instance = new D365DataService();
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
        private D365DataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~D365DataService()
        {
        }

        #endregion

        #region Public Methods

        #region D365_WP_OUL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<ListD365_WP_OULData> D365_WP_OUL(string P_WARPHEADNO)
        {
            List<ListD365_WP_OULData> results = null;

            if (!HasConnection())
                return results;

            D365_WP_OULParameter dbPara = new D365_WP_OULParameter();

            if (!string.IsNullOrEmpty(P_WARPHEADNO))
                dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<D365_WP_OULResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_WP_OUL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_WP_OULData>();
                    foreach (D365_WP_OULResult dbResult in dbResults)
                    {
                        ListD365_WP_OULData inst = new ListD365_WP_OULData();

                        inst.LINENO = dbResult.LINENO;
                        inst.OUTPUTDATE = dbResult.OUTPUTDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.GRADE = dbResult.GRADE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.SERIALID = dbResult.SERIALID;
                        inst.FINISH = dbResult.FINISH;
                        inst.MOVEMENTTRANS = dbResult.MOVEMENTTRANS;

                        inst.WAREHOUSE = dbResult.WAREHOUSE;
                        inst.LOCATION = dbResult.LOCATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_WP_OUH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<D365_WP_OUHData> D365_WP_OUH(string P_WARPHEADNO)
        {
            List<D365_WP_OUHData> results = null;

            if (!HasConnection())
                return results;

            D365_WP_OUHParameter dbPara = new D365_WP_OUHParameter();

            if (!string.IsNullOrEmpty(P_WARPHEADNO))
                dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<D365_WP_OUHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_WP_OUH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_WP_OUHData>();
                    foreach (D365_WP_OUHResult dbResult in dbResults)
                    {
                        D365_WP_OUHData inst = new D365_WP_OUHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;
                       
                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_WP_OPL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<ListD365_WP_OPLData> D365_WP_OPL(string P_WARPHEADNO)
        {
            List<ListD365_WP_OPLData> results = null;

            if (!HasConnection())
                return results;

            D365_WP_OPLParameter dbPara = new D365_WP_OPLParameter();

            if (!string.IsNullOrEmpty(P_WARPHEADNO))
                dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<D365_WP_OPLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_WP_OPL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_WP_OPLData>();
                    foreach (D365_WP_OPLResult dbResult in dbResults)
                    {
                        ListD365_WP_OPLData inst = new ListD365_WP_OPLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.PROCQTY = dbResult.PROCQTY;
                        inst.OPRNO = dbResult.OPRNO;
                        inst.OPRID = dbResult.OPRID;
                        inst.MACHINENO = dbResult.MACHINENO;
                        inst.STARTDATETIME = dbResult.STARTDATETIME;
                        inst.ENDDATETIME = dbResult.ENDDATETIME;
                       
                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_WP_OPH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<D365_WP_OPHData> D365_WP_OPH(string P_WARPHEADNO)
        {
            List<D365_WP_OPHData> results = null;

            if (!HasConnection())
                return results;

            D365_WP_OPHParameter dbPara = new D365_WP_OPHParameter();

            if (!string.IsNullOrEmpty(P_WARPHEADNO))
                dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<D365_WP_OPHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_WP_OPH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_WP_OPHData>();
                    foreach (D365_WP_OPHResult dbResult in dbResults)
                    {
                        D365_WP_OPHData inst = new D365_WP_OPHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_WP_ISL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<ListD365_WP_ISLData> D365_WP_ISL(string P_WARPHEADNO)
        {
            List<ListD365_WP_ISLData> results = null;

            if (!HasConnection())
                return results;

            D365_WP_ISLParameter dbPara = new D365_WP_ISLParameter();

            if (!string.IsNullOrEmpty(P_WARPHEADNO))
                dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<D365_WP_ISLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_WP_ISL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_WP_ISLData>();
                    foreach (D365_WP_ISLResult dbResult in dbResults)
                    {
                        ListD365_WP_ISLData inst = new ListD365_WP_ISLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.ISSUEDATE = dbResult.ISSUEDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.STYLEID = dbResult.STYLEID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.SERIALID = dbResult.SERIALID;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_WP_ISH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<D365_WP_ISHData> D365_WP_ISH(string P_WARPHEADNO)
        {
            List<D365_WP_ISHData> results = null;

            if (!HasConnection())
                return results;

            D365_WP_ISHParameter dbPara = new D365_WP_ISHParameter();

            if (!string.IsNullOrEmpty(P_WARPHEADNO))
                dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<D365_WP_ISHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_WP_ISH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_WP_ISHData>();
                    foreach (D365_WP_ISHResult dbResult in dbResults)
                    {
                        D365_WP_ISHData inst = new D365_WP_ISHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_WP_BPO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<ListD365_WP_BPOData> D365_WP_BPO(string P_WARPHEADNO)
        {
            List<ListD365_WP_BPOData> results = null;

            if (!HasConnection())
                return results;

            D365_WP_BPOParameter dbPara = new D365_WP_BPOParameter();

            if (!string.IsNullOrEmpty(P_WARPHEADNO))
                dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<D365_WP_BPOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_WP_BPO(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_WP_BPOData>();
                    foreach (D365_WP_BPOResult dbResult in dbResults)
                    {
                        ListD365_WP_BPOData inst = new ListD365_WP_BPOData();

                        inst.PRODID = dbResult.PRODID;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.OPERATION = dbResult.OPERATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_PK_TOTALHEADER
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <returns></returns>
        public List<ListD365_PK_TOTALHEADERData> D365_PK_TOTALHEADER(string P_PALLETNO)
        {
            List<ListD365_PK_TOTALHEADERData> results = null;

            if (!HasConnection())
                return results;

            D365_PK_TOTALHEADERParameter dbPara = new D365_PK_TOTALHEADERParameter();

            if (!string.IsNullOrEmpty(P_PALLETNO))
                dbPara.P_PLALLETNO = P_PALLETNO;

            List<D365_PK_TOTALHEADERResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_PK_TOTALHEADER(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_PK_TOTALHEADERData>();
                    foreach (D365_PK_TOTALHEADERResult dbResult in dbResults)
                    {
                        ListD365_PK_TOTALHEADERData inst = new ListD365_PK_TOTALHEADERData();

                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_PK_OUL_C
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public List<ListD365_PK_OUL_CData> D365_PK_OUL_C(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            List<ListD365_PK_OUL_CData> results = null;

            if (!HasConnection())
                return results;

            D365_PK_OUL_CParameter dbPara = new D365_PK_OUL_CParameter();

            if (!string.IsNullOrEmpty(P_PALLETNO))
                dbPara.P_PALLETNO = P_PALLETNO;

            if (!string.IsNullOrEmpty(P_ITEMCODE))
                dbPara.P_ITEMCODE = P_ITEMCODE;

            if (!string.IsNullOrEmpty(P_LOADINGTYPE))
                dbPara.P_LOADINGTYPE = P_LOADINGTYPE;

            List<D365_PK_OUL_CResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_PK_OUL_C(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_PK_OUL_CData>();
                    foreach (D365_PK_OUL_CResult dbResult in dbResults)
                    {
                        ListD365_PK_OUL_CData inst = new ListD365_PK_OUL_CData();

                        inst.LINENO = dbResult.LINENO;
                        inst.OUTPUTDATE = dbResult.OUTPUTDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.GRADE = dbResult.GRADE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.SERIALID = dbResult.SERIALID;
                        inst.FINISH = dbResult.FINISH;
                        inst.MOVEMENTTRANS = dbResult.MOVEMENTTRANS;

                        inst.WAREHOUSE = dbResult.WAREHOUSE;
                        inst.LOCATION = dbResult.LOCATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_PK_OUL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public List<ListD365_PK_OULData> D365_PK_OUL(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            List<ListD365_PK_OULData> results = null;

            if (!HasConnection())
                return results;

            D365_PK_OULParameter dbPara = new D365_PK_OULParameter();

            if (!string.IsNullOrEmpty(P_PALLETNO))
                dbPara.P_PALLETNO = P_PALLETNO;

            if (!string.IsNullOrEmpty(P_ITEMCODE))
                dbPara.P_ITEMCODE = P_ITEMCODE;

            if (!string.IsNullOrEmpty(P_LOADINGTYPE))
                dbPara.P_LOADINGTYPE = P_LOADINGTYPE;

            List<D365_PK_OULResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_PK_OUL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_PK_OULData>();
                    foreach (D365_PK_OULResult dbResult in dbResults)
                    {
                        ListD365_PK_OULData inst = new ListD365_PK_OULData();

                        inst.LINENO = dbResult.LINENO;
                        inst.OUTPUTDATE = dbResult.OUTPUTDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.GRADE = dbResult.GRADE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.SERIALID = dbResult.SERIALID;
                        inst.FINISH = dbResult.FINISH;
                        inst.MOVEMENTTRANS = dbResult.MOVEMENTTRANS;

                        inst.WAREHOUSE = dbResult.WAREHOUSE;
                        inst.LOCATION = dbResult.LOCATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_PK_OUH_C
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public List<D365_PK_OUH_CData> D365_PK_OUH_C(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            List<D365_PK_OUH_CData> results = null;

            if (!HasConnection())
                return results;

            D365_PK_OUH_CParameter dbPara = new D365_PK_OUH_CParameter();

            if (!string.IsNullOrEmpty(P_PALLETNO))
                dbPara.P_PALLETNO = P_PALLETNO;

            if (!string.IsNullOrEmpty(P_ITEMCODE))
                dbPara.P_ITEMCODE = P_ITEMCODE;

            if (!string.IsNullOrEmpty(P_LOADINGTYPE))
                dbPara.P_LOADINGTYPE = P_LOADINGTYPE;

            List<D365_PK_OUH_CResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_PK_OUH_C(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_PK_OUH_CData>();
                    foreach (D365_PK_OUH_CResult dbResult in dbResults)
                    {
                        D365_PK_OUH_CData inst = new D365_PK_OUH_CData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_PK_OUH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public List<D365_PK_OUHData> D365_PK_OUH(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            List<D365_PK_OUHData> results = null;

            if (!HasConnection())
                return results;

            D365_PK_OUHParameter dbPara = new D365_PK_OUHParameter();

            if (!string.IsNullOrEmpty(P_PALLETNO))
                dbPara.P_PALLETNO = P_PALLETNO;

            if (!string.IsNullOrEmpty(P_ITEMCODE))
                dbPara.P_ITEMCODE = P_ITEMCODE;

            if (!string.IsNullOrEmpty(P_LOADINGTYPE))
                dbPara.P_LOADINGTYPE = P_LOADINGTYPE;

            List<D365_PK_OUHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_PK_OUH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_PK_OUHData>();
                    foreach (D365_PK_OUHResult dbResult in dbResults)
                    {
                        D365_PK_OUHData inst = new D365_PK_OUHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_PK_ISL_C
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public List<ListD365_PK_ISL_CData> D365_PK_ISL_C(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            List<ListD365_PK_ISL_CData> results = null;

            if (!HasConnection())
                return results;

            D365_PK_ISL_CParameter dbPara = new D365_PK_ISL_CParameter();

            if (!string.IsNullOrEmpty(P_PALLETNO))
                dbPara.P_PALLETNO = P_PALLETNO;

            if (!string.IsNullOrEmpty(P_ITEMCODE))
                dbPara.P_ITEMCODE = P_ITEMCODE;

            if (!string.IsNullOrEmpty(P_LOADINGTYPE))
                dbPara.P_LOADINGTYPE = P_LOADINGTYPE;

            List<D365_PK_ISL_CResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_PK_ISL_C(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_PK_ISL_CData>();
                    foreach (D365_PK_ISL_CResult dbResult in dbResults)
                    {
                        ListD365_PK_ISL_CData inst = new ListD365_PK_ISL_CData();

                        inst.LINENO = dbResult.LINENO;
                        inst.ISSUEDATE = dbResult.ISSUEDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.STYLEID = dbResult.STYLEID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.SERIALID = dbResult.SERIALID;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_PK_ISL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public List<ListD365_PK_ISLData> D365_PK_ISL(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            List<ListD365_PK_ISLData> results = null;

            if (!HasConnection())
                return results;

            D365_PK_ISLParameter dbPara = new D365_PK_ISLParameter();

            if (!string.IsNullOrEmpty(P_PALLETNO))
                dbPara.P_PALLETNO = P_PALLETNO;

            if (!string.IsNullOrEmpty(P_ITEMCODE))
                dbPara.P_ITEMCODE = P_ITEMCODE;

            if (!string.IsNullOrEmpty(P_LOADINGTYPE))
                dbPara.P_LOADINGTYPE = P_LOADINGTYPE;

            List<D365_PK_ISLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_PK_ISL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_PK_ISLData>();
                    foreach (D365_PK_ISLResult dbResult in dbResults)
                    {
                        ListD365_PK_ISLData inst = new ListD365_PK_ISLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.ISSUEDATE = dbResult.ISSUEDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.STYLEID = dbResult.STYLEID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.SERIALID = dbResult.SERIALID;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_PK_ISH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public List<D365_PK_ISHData> D365_PK_ISH(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            List<D365_PK_ISHData> results = null;

            if (!HasConnection())
                return results;

            D365_PK_ISHParameter dbPara = new D365_PK_ISHParameter();

            if (!string.IsNullOrEmpty(P_PALLETNO))
                dbPara.P_PALLETNO = P_PALLETNO;

            if (!string.IsNullOrEmpty(P_ITEMCODE))
                dbPara.P_ITEMCODE = P_ITEMCODE;

            if (!string.IsNullOrEmpty(P_LOADINGTYPE))
                dbPara.P_LOADINGTYPE = P_LOADINGTYPE;

            List<D365_PK_ISHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_PK_ISH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_PK_ISHData>();
                    foreach (D365_PK_ISHResult dbResult in dbResults)
                    {
                        D365_PK_ISHData inst = new D365_PK_ISHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_PK_BPO_C
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public List<ListD365_PK_BPO_CData> D365_PK_BPO_C(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            List<ListD365_PK_BPO_CData> results = null;

            if (!HasConnection())
                return results;

            D365_PK_BPO_CParameter dbPara = new D365_PK_BPO_CParameter();

            if (!string.IsNullOrEmpty(P_PALLETNO))
                dbPara.P_PALLETNO = P_PALLETNO;

            if (!string.IsNullOrEmpty(P_ITEMCODE))
                dbPara.P_ITEMCODE = P_ITEMCODE;

            if (!string.IsNullOrEmpty(P_LOADINGTYPE))
                dbPara.P_LOADINGTYPE = P_LOADINGTYPE;

            List<D365_PK_BPO_CResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_PK_BPO_C(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_PK_BPO_CData>();
                    foreach (D365_PK_BPO_CResult dbResult in dbResults)
                    {
                        ListD365_PK_BPO_CData inst = new ListD365_PK_BPO_CData();

                        inst.PRODID = dbResult.PRODID;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.OPERATION = dbResult.OPERATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_PK_BPO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public List<ListD365_PK_BPOData> D365_PK_BPO(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            List<ListD365_PK_BPOData> results = null;

            if (!HasConnection())
                return results;

            D365_PK_BPOParameter dbPara = new D365_PK_BPOParameter();

            if (!string.IsNullOrEmpty(P_PALLETNO))
                dbPara.P_PALLETNO = P_PALLETNO;

            if (!string.IsNullOrEmpty(P_ITEMCODE))
                dbPara.P_ITEMCODE = P_ITEMCODE;

            if (!string.IsNullOrEmpty(P_LOADINGTYPE))
                dbPara.P_LOADINGTYPE = P_LOADINGTYPE;

            List<D365_PK_BPOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_PK_BPO(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_PK_BPOData>();
                    foreach (D365_PK_BPOResult dbResult in dbResults)
                    {
                        ListD365_PK_BPOData inst = new ListD365_PK_BPOData();

                        inst.PRODID = dbResult.PRODID;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.OPERATION = dbResult.OPERATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_IN_OUL_AUTO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_INSPECTIONLOT"></param>
        /// <param name="P_FINISH"></param>
        /// <returns></returns>
        public List<ListD365_IN_OUL_AUTOData> D365_IN_OUL_AUTO(string P_FINISHINGLOT, string P_INSPECTIONLOT, decimal? P_FINISH)
        {
            List<ListD365_IN_OUL_AUTOData> results = null;

            if (!HasConnection())
                return results;

            D365_IN_OUL_AUTOParameter dbPara = new D365_IN_OUL_AUTOParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                dbPara.P_INSPECTIONLOT = P_INSPECTIONLOT;

            if (P_FINISH != null)
                dbPara.P_FINISH = P_FINISH;

            List<D365_IN_OUL_AUTOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_IN_OUL_AUTO(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_IN_OUL_AUTOData>();
                    foreach (D365_IN_OUL_AUTOResult dbResult in dbResults)
                    {
                        ListD365_IN_OUL_AUTOData inst = new ListD365_IN_OUL_AUTOData();

                        inst.LINENO = dbResult.LINENO;
                        inst.OUTPUTDATE = dbResult.OUTPUTDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.GRADE = dbResult.GRADE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.SERIALID = dbResult.SERIALID;
                        inst.FINISH = dbResult.FINISH;
                        inst.MOVEMENTTRANS = dbResult.MOVEMENTTRANS;

                        inst.WAREHOUSE = dbResult.WAREHOUSE;
                        inst.LOCATION = dbResult.LOCATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_IN_OUL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_INSPECTIONLOT"></param>
        /// <param name="P_STARTDATE"></param>
        /// <returns></returns>
        public List<ListD365_IN_OULData> D365_IN_OUL(string P_FINISHINGLOT, string P_INSPECTIONLOT, DateTime? P_STARTDATE)
        {
            List<ListD365_IN_OULData> results = null;

            if (!HasConnection())
                return results;

            D365_IN_OULParameter dbPara = new D365_IN_OULParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                dbPara.P_INSPECTIONLOT = P_INSPECTIONLOT;

            if(P_STARTDATE != null)
                dbPara.P_STARTDATE = P_STARTDATE;

            List<D365_IN_OULResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_IN_OUL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_IN_OULData>();
                    foreach (D365_IN_OULResult dbResult in dbResults)
                    {
                        ListD365_IN_OULData inst = new ListD365_IN_OULData();

                        inst.LINENO = dbResult.LINENO;
                        inst.OUTPUTDATE = dbResult.OUTPUTDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.GRADE = dbResult.GRADE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.SERIALID = dbResult.SERIALID;
                        inst.FINISH = dbResult.FINISH;
                        inst.MOVEMENTTRANS = dbResult.MOVEMENTTRANS;

                        inst.WAREHOUSE = dbResult.WAREHOUSE;
                        inst.LOCATION = dbResult.LOCATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_IN_OUH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INSPECTIONLOT"></param>
        /// <returns></returns>
        public List<D365_IN_OUHData> D365_IN_OUH(string P_INSPECTIONLOT)
        {
            List<D365_IN_OUHData> results = null;

            if (!HasConnection())
                return results;

            D365_IN_OUHParameter dbPara = new D365_IN_OUHParameter();

            if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                dbPara.P_INSPECTIONLOT = P_INSPECTIONLOT;

            List<D365_IN_OUHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_IN_OUH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_IN_OUHData>();
                    foreach (D365_IN_OUHResult dbResult in dbResults)
                    {
                        D365_IN_OUHData inst = new D365_IN_OUHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_IN_OPL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INSPECTIONLOT"></param>
        /// <param name="P_STARTDATE"></param>
        /// <returns></returns>
        public List<ListD365_IN_OPLData> D365_IN_OPL(string P_INSPECTIONLOT, DateTime? P_STARTDATE)
        {
            List<ListD365_IN_OPLData> results = null;

            if (!HasConnection())
                return results;

            D365_IN_OPLParameter dbPara = new D365_IN_OPLParameter();

            if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                dbPara.P_INSPECTIONLOT = P_INSPECTIONLOT;

            if (P_STARTDATE != null)
                dbPara.P_STARTDATE = P_STARTDATE;

            List<D365_IN_OPLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_IN_OPL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_IN_OPLData>();
                    foreach (D365_IN_OPLResult dbResult in dbResults)
                    {
                        ListD365_IN_OPLData inst = new ListD365_IN_OPLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.PROCQTY = dbResult.PROCQTY;
                        inst.OPRNO = dbResult.OPRNO;
                        inst.OPRID = dbResult.OPRID;
                        inst.MACHINENO = dbResult.MACHINENO;
                        inst.STARTDATETIME = dbResult.STARTDATETIME;
                        inst.ENDDATETIME = dbResult.ENDDATETIME;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_IN_OPH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INSPECTIONLOT"></param>
        /// <returns></returns>
        public List<D365_IN_OPHData> D365_IN_OPH(string P_INSPECTIONLOT)
        {
            List<D365_IN_OPHData> results = null;

            if (!HasConnection())
                return results;

            D365_IN_OPHParameter dbPara = new D365_IN_OPHParameter();

            if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                dbPara.P_INSPECTIONLOT = P_INSPECTIONLOT;

            List<D365_IN_OPHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_IN_OPH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_IN_OPHData>();
                    foreach (D365_IN_OPHResult dbResult in dbResults)
                    {
                        D365_IN_OPHData inst = new D365_IN_OPHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_IN_ISL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_INSPECTIONLOT"></param>
        /// <param name="P_STARTDATE"></param>
        /// <returns></returns>
        public List<ListD365_IN_ISLData> D365_IN_ISL(string P_FINISHINGLOT, string P_INSPECTIONLOT, DateTime? P_STARTDATE)
        {
            List<ListD365_IN_ISLData> results = null;

            if (!HasConnection())
                return results;

            D365_IN_ISLParameter dbPara = new D365_IN_ISLParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

             if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                dbPara.P_INSPECTIONLOT = P_INSPECTIONLOT;

             if (P_STARTDATE != null)
                 dbPara.P_STARTDATE = P_STARTDATE;

            List<D365_IN_ISLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_IN_ISL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_IN_ISLData>();
                    foreach (D365_IN_ISLResult dbResult in dbResults)
                    {
                        ListD365_IN_ISLData inst = new ListD365_IN_ISLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.ISSUEDATE = dbResult.ISSUEDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.STYLEID = dbResult.STYLEID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.SERIALID = dbResult.SERIALID;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_IN_ISH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_INSPECTIONLOT"></param>
        /// <returns></returns>
        public List<D365_IN_ISHData> D365_IN_ISH(string P_FINISHINGLOT, string P_INSPECTIONLOT)
        {
            List<D365_IN_ISHData> results = null;

            if (!HasConnection())
                return results;

            D365_IN_ISHParameter dbPara = new D365_IN_ISHParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                dbPara.P_INSPECTIONLOT = P_INSPECTIONLOT;

            List<D365_IN_ISHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_IN_ISH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_IN_ISHData>();
                    foreach (D365_IN_ISHResult dbResult in dbResults)
                    {
                        D365_IN_ISHData inst = new D365_IN_ISHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_IN_BPO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_INSPECTIONLOT"></param>
        /// <returns></returns>
        public List<ListD365_IN_BPOData> D365_IN_BPO(string P_FINISHINGLOT, string P_INSPECTIONLOT, DateTime? P_STARTDATE)
        {
            List<ListD365_IN_BPOData> results = null;

            if (!HasConnection())
                return results;

            D365_IN_BPOParameter dbPara = new D365_IN_BPOParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                dbPara.P_INSPECTIONLOT = P_INSPECTIONLOT;

            if (P_STARTDATE != null)
                dbPara.P_STARTDATE = P_STARTDATE;

            List<D365_IN_BPOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_IN_BPO(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_IN_BPOData>();
                    foreach (D365_IN_BPOResult dbResult in dbResults)
                    {
                        ListD365_IN_BPOData inst = new ListD365_IN_BPOData();

                        inst.PRODID = dbResult.PRODID;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.OPERATION = dbResult.OPERATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_GR_OUL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_DOFFNO"></param>
        /// <param name="P_LOOMNO"></param>
        /// <returns></returns>
        public List<ListD365_GR_OULData> D365_GR_OUL(string P_BEAMLOT, string P_WEAVINGLOT, decimal? P_DOFFNO, string P_LOOMNO)
        {
            List<ListD365_GR_OULData> results = null;

            if (!HasConnection())
                return results;

            D365_GR_OULParameter dbPara = new D365_GR_OULParameter();

            if (!string.IsNullOrEmpty(P_BEAMLOT))
                dbPara.P_BEAMLOT = P_BEAMLOT;

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            if (P_DOFFNO != null)
                dbPara.P_DOFFNO = P_DOFFNO;

            if (!string.IsNullOrEmpty(P_LOOMNO))
                dbPara.P_LOOMNO = P_LOOMNO;

            List<D365_GR_OULResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_GR_OUL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_GR_OULData>();
                    foreach (D365_GR_OULResult dbResult in dbResults)
                    {
                        ListD365_GR_OULData inst = new ListD365_GR_OULData();

                        inst.LINENO = dbResult.LINENO;
                        inst.OUTPUTDATE = dbResult.OUTPUTDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.GRADE = dbResult.GRADE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.SERIALID = dbResult.SERIALID;
                        inst.FINISH = dbResult.FINISH;
                        inst.MOVEMENTTRANS = dbResult.MOVEMENTTRANS;

                        inst.WAREHOUSE = dbResult.WAREHOUSE;
                        inst.LOCATION = dbResult.LOCATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_GR_OUH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WEAVINGLOT"></param>
        /// <returns></returns>
        public List<D365_GR_OUHData> D365_GR_OUH(string P_WEAVINGLOT)
        {
            List<D365_GR_OUHData> results = null;

            if (!HasConnection())
                return results;

            D365_GR_OUHParameter dbPara = new D365_GR_OUHParameter();

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<D365_GR_OUHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_GR_OUH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_GR_OUHData>();
                    foreach (D365_GR_OUHResult dbResult in dbResults)
                    {
                        D365_GR_OUHData inst = new D365_GR_OUHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_GR_OPL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WEAVINGLOT"></param>
        /// <returns></returns>
        public List<ListD365_GR_OPLData> D365_GR_OPL(string P_WEAVINGLOT)
        {
            List<ListD365_GR_OPLData> results = null;

            if (!HasConnection())
                return results;

            D365_GR_OPLParameter dbPara = new D365_GR_OPLParameter();

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<D365_GR_OPLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_GR_OPL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_GR_OPLData>();
                    foreach (D365_GR_OPLResult dbResult in dbResults)
                    {
                        ListD365_GR_OPLData inst = new ListD365_GR_OPLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.PROCQTY = dbResult.PROCQTY;
                        inst.OPRNO = dbResult.OPRNO;
                        inst.OPRID = dbResult.OPRID;
                        inst.MACHINENO = dbResult.MACHINENO;
                        inst.STARTDATETIME = dbResult.STARTDATETIME;
                        inst.ENDDATETIME = dbResult.ENDDATETIME;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_GR_OPH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WEAVINGLOT"></param>
        /// <returns></returns>
        public List<D365_GR_OPHData> D365_GR_OPH(string P_WEAVINGLOT)
        {
            List<D365_GR_OPHData> results = null;

            if (!HasConnection())
                return results;

            D365_GR_OPHParameter dbPara = new D365_GR_OPHParameter();

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<D365_GR_OPHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_GR_OPH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_GR_OPHData>();
                    foreach (D365_GR_OPHResult dbResult in dbResults)
                    {
                        D365_GR_OPHData inst = new D365_GR_OPHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_GR_ISL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMLOT"></param>
        /// <returns></returns>
        public List<ListD365_GR_ISLData> D365_GR_ISL(string P_BEAMLOT, string P_WEAVINGLOT, decimal? P_DOFFNO, string P_LOOMNO)
        {
            List<ListD365_GR_ISLData> results = null;

            if (!HasConnection())
                return results;

            D365_GR_ISLParameter dbPara = new D365_GR_ISLParameter();

            if (!string.IsNullOrEmpty(P_BEAMLOT))
                dbPara.P_BEAMLOT = P_BEAMLOT;

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            if (P_DOFFNO != null)
                dbPara.P_DOFFNO = P_DOFFNO;

            if (!string.IsNullOrEmpty(P_LOOMNO))
                dbPara.P_LOOMNO = P_LOOMNO;

            List<D365_GR_ISLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_GR_ISL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_GR_ISLData>();
                    foreach (D365_GR_ISLResult dbResult in dbResults)
                    {
                        ListD365_GR_ISLData inst = new ListD365_GR_ISLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.ISSUEDATE = dbResult.ISSUEDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.STYLEID = dbResult.STYLEID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.SERIALID = dbResult.SERIALID;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_GR_ISH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_DOFFNO"></param>
        /// <returns></returns>
        public List<D365_GR_ISHData> D365_GR_ISH(string P_BEAMLOT, string P_WEAVINGLOT, decimal? P_DOFFNO)
        {
            List<D365_GR_ISHData> results = null;

            if (!HasConnection())
                return results;

            D365_GR_ISHParameter dbPara = new D365_GR_ISHParameter();

            if (!string.IsNullOrEmpty(P_BEAMLOT))
                dbPara.P_BEAMLOT = P_BEAMLOT;

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            if (P_DOFFNO != null)
                dbPara.P_DOFFNO = P_DOFFNO;

            List<D365_GR_ISHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_GR_ISH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_GR_ISHData>();
                    foreach (D365_GR_ISHResult dbResult in dbResults)
                    {
                        D365_GR_ISHData inst = new D365_GR_ISHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_GR_BPO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <param name="P_LOOMNO"></param>
        /// <param name="P_DOFFNO"></param>
        /// <returns></returns>
        public List<ListD365_GR_BPOData> D365_GR_BPO(string P_BEAMLOT, string P_LOOMNO, decimal? P_DOFFNO)
        {
            List<ListD365_GR_BPOData> results = null;

            if (!HasConnection())
                return results;

            D365_GR_BPOParameter dbPara = new D365_GR_BPOParameter();

            if (!string.IsNullOrEmpty(P_BEAMLOT))
                dbPara.P_BEAMLOT = P_BEAMLOT;

            if (!string.IsNullOrEmpty(P_LOOMNO))
                dbPara.P_LOOMNO = P_LOOMNO;

            if (P_DOFFNO != null)
                dbPara.P_DOFFNO = P_DOFFNO;

            List<D365_GR_BPOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_GR_BPO(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_GR_BPOData>();
                    foreach (D365_GR_BPOResult dbResult in dbResults)
                    {
                        ListD365_GR_BPOData inst = new ListD365_GR_BPOData();

                        inst.PRODID = dbResult.PRODID;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.OPERATION = dbResult.OPERATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_FN_OUL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_PROCESS"></param>
        /// <returns></returns>
        public List<ListD365_FN_OULData> D365_FN_OUL(string P_FINISHINGLOT, string P_WEAVINGLOT, string P_PROCESS)
        {
            List<ListD365_FN_OULData> results = null;

            if (!HasConnection())
                return results;

            D365_FN_OULParameter dbPara = new D365_FN_OULParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            if (!string.IsNullOrEmpty(P_PROCESS))
                dbPara.P_PROCESS = P_PROCESS;

            List<D365_FN_OULResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_FN_OUL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_FN_OULData>();
                    foreach (D365_FN_OULResult dbResult in dbResults)
                    {
                        ListD365_FN_OULData inst = new ListD365_FN_OULData();

                        inst.LINENO = dbResult.LINENO;
                        inst.OUTPUTDATE = dbResult.OUTPUTDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.GRADE = dbResult.GRADE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.SERIALID = dbResult.SERIALID;
                        inst.FINISH = dbResult.FINISH;
                        inst.MOVEMENTTRANS = dbResult.MOVEMENTTRANS;
                        inst.WAREHOUSE = dbResult.WAREHOUSE;
                        inst.LOCATION = dbResult.LOCATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_FN_OUH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_PROCESS"></param>
        /// <returns></returns>
        public List<D365_FN_OUHData> D365_FN_OUH(string P_FINISHINGLOT, string P_WEAVINGLOT, string P_PROCESS)
        {
            List<D365_FN_OUHData> results = null;

            if (!HasConnection())
                return results;

            D365_FN_OUHParameter dbPara = new D365_FN_OUHParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            if (!string.IsNullOrEmpty(P_PROCESS))
                dbPara.P_PROCESS = P_PROCESS;

            List<D365_FN_OUHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_FN_OUH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_FN_OUHData>();
                    foreach (D365_FN_OUHResult dbResult in dbResults)
                    {
                        D365_FN_OUHData inst = new D365_FN_OUHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_FN_OPL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_PROCESS"></param>
        /// <returns></returns>
        public List<ListD365_FN_OPLData> D365_FN_OPL(string P_FINISHINGLOT, string P_WEAVINGLOT, string P_PROCESS)
        {
            List<ListD365_FN_OPLData> results = null;

            if (!HasConnection())
                return results;

            D365_FN_OPLParameter dbPara = new D365_FN_OPLParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            if (!string.IsNullOrEmpty(P_PROCESS))
                dbPara.P_PROCESS = P_PROCESS;

            List<D365_FN_OPLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_FN_OPL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_FN_OPLData>();
                    foreach (D365_FN_OPLResult dbResult in dbResults)
                    {
                        ListD365_FN_OPLData inst = new ListD365_FN_OPLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.PROCQTY = dbResult.PROCQTY;
                        inst.OPRNO = dbResult.OPRNO;
                        inst.OPRID = dbResult.OPRID;
                        inst.MACHINENO = dbResult.MACHINENO;
                        inst.STARTDATETIME = dbResult.STARTDATETIME;
                        inst.ENDDATETIME = dbResult.ENDDATETIME;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_FN_OPH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_PROCESS"></param>
        /// <returns></returns>
        public List<D365_FN_OPHData> D365_FN_OPH(string P_FINISHINGLOT, string P_WEAVINGLOT, string P_PROCESS)
        {
            List<D365_FN_OPHData> results = null;

            if (!HasConnection())
                return results;

            D365_FN_OPHParameter dbPara = new D365_FN_OPHParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            if (!string.IsNullOrEmpty(P_PROCESS))
                dbPara.P_PROCESS = P_PROCESS;

            List<D365_FN_OPHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_FN_OPH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_FN_OPHData>();
                    foreach (D365_FN_OPHResult dbResult in dbResults)
                    {
                        D365_FN_OPHData inst = new D365_FN_OPHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_FN_ISL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_PROCESS"></param>
        /// <returns></returns>
        public List<ListD365_FN_ISLData> D365_FN_ISL(string P_FINISHINGLOT, string P_WEAVINGLOT, string P_PROCESS)
        {
            List<ListD365_FN_ISLData> results = null;

            if (!HasConnection())
                return results;

            D365_FN_ISLParameter dbPara = new D365_FN_ISLParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            if (!string.IsNullOrEmpty(P_PROCESS))
                dbPara.P_PROCESS = P_PROCESS;

            List<D365_FN_ISLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_FN_ISL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_FN_ISLData>();
                    foreach (D365_FN_ISLResult dbResult in dbResults)
                    {
                        ListD365_FN_ISLData inst = new ListD365_FN_ISLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.ISSUEDATE = dbResult.ISSUEDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.STYLEID = dbResult.STYLEID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.SERIALID = dbResult.SERIALID;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_FN_ISH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_PROCESS"></param>
        /// <returns></returns>
        public List<D365_FN_ISHData> D365_FN_ISH(string P_FINISHINGLOT, string P_WEAVINGLOT, string P_PROCESS)
        {
            List<D365_FN_ISHData> results = null;

            if (!HasConnection())
                return results;

            D365_FN_ISHParameter dbPara = new D365_FN_ISHParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            if (!string.IsNullOrEmpty(P_PROCESS))
                dbPara.P_PROCESS = P_PROCESS;

            List<D365_FN_ISHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_FN_ISH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_FN_ISHData>();
                    foreach (D365_FN_ISHResult dbResult in dbResults)
                    {
                        D365_FN_ISHData inst = new D365_FN_ISHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_FN_BPO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_PROCESS"></param>
        /// <returns></returns>
        public List<ListD365_FN_BPOData> D365_FN_BPO(string P_FINISHINGLOT, string P_WEAVINGLOT, string P_PROCESS)
        {
            List<ListD365_FN_BPOData> results = null;

            if (!HasConnection())
                return results;

            D365_FN_BPOParameter dbPara = new D365_FN_BPOParameter();

            if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            if (!string.IsNullOrEmpty(P_PROCESS))
                dbPara.P_PROCESS = P_PROCESS;

            List<D365_FN_BPOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_FN_BPO(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_FN_BPOData>();
                    foreach (D365_FN_BPOResult dbResult in dbResults)
                    {
                        ListD365_FN_BPOData inst = new ListD365_FN_BPOData();

                        inst.PRODID = dbResult.PRODID;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.OPERATION = dbResult.OPERATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_DT_OUL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <returns></returns>
        public List<ListD365_DT_OULData> D365_DT_OUL(string P_BEAMERNO)
        {
            List<ListD365_DT_OULData> results = null;

            if (!HasConnection())
                return results;

            D365_DT_OULParameter dbPara = new D365_DT_OULParameter();

            if (!string.IsNullOrEmpty(P_BEAMERNO))
                dbPara.P_BEAMERNO = P_BEAMERNO;

            List<D365_DT_OULResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_DT_OUL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_DT_OULData>();
                    foreach (D365_DT_OULResult dbResult in dbResults)
                    {
                        ListD365_DT_OULData inst = new ListD365_DT_OULData();

                        inst.LINENO = dbResult.LINENO;
                        inst.OUTPUTDATE = dbResult.OUTPUTDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.GRADE = dbResult.GRADE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.SERIALID = dbResult.SERIALID;
                        inst.FINISH = dbResult.FINISH;
                        inst.MOVEMENTTRANS = dbResult.MOVEMENTTRANS;

                        inst.WAREHOUSE = dbResult.WAREHOUSE;
                        inst.LOCATION = dbResult.LOCATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_DT_OUH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <returns></returns>
        public List<D365_DT_OUHData> D365_DT_OUH(string P_BEAMLOT)
        {
            List<D365_DT_OUHData> results = null;

            if (!HasConnection())
                return results;

            D365_DT_OUHParameter dbPara = new D365_DT_OUHParameter();

            if (!string.IsNullOrEmpty(P_BEAMLOT))
                dbPara.P_BEAMLOT = P_BEAMLOT;

            List<D365_DT_OUHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_DT_OUH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_DT_OUHData>();
                    foreach (D365_DT_OUHResult dbResult in dbResults)
                    {
                        D365_DT_OUHData inst = new D365_DT_OUHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_DT_OPL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <returns></returns>
        public List<ListD365_DT_OPLData> D365_DT_OPL(string P_BEAMLOT)
        {
            List<ListD365_DT_OPLData> results = null;

            if (!HasConnection())
                return results;

            D365_DT_OPLParameter dbPara = new D365_DT_OPLParameter();

            if (!string.IsNullOrEmpty(P_BEAMLOT))
                dbPara.P_BEAMLOT = P_BEAMLOT;

            List<D365_DT_OPLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_DT_OPL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_DT_OPLData>();
                    foreach (D365_DT_OPLResult dbResult in dbResults)
                    {
                        ListD365_DT_OPLData inst = new ListD365_DT_OPLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.PROCQTY = dbResult.PROCQTY;
                        inst.OPRNO = dbResult.OPRNO;
                        inst.OPRID = dbResult.OPRID;
                        inst.MACHINENO = dbResult.MACHINENO;
                        inst.STARTDATETIME = dbResult.STARTDATETIME;
                        inst.ENDDATETIME = dbResult.ENDDATETIME;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_DT_OPH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <returns></returns>
        public List<D365_DT_OPHData> D365_DT_OPH(string P_BEAMLOT)
        {
            List<D365_DT_OPHData> results = null;

            if (!HasConnection())
                return results;

            D365_DT_OPHParameter dbPara = new D365_DT_OPHParameter();

            if (!string.IsNullOrEmpty(P_BEAMLOT))
                dbPara.P_BEAMLOT = P_BEAMLOT;

            List<D365_DT_OPHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_DT_OPH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_DT_OPHData>();
                    foreach (D365_DT_OPHResult dbResult in dbResults)
                    {
                        D365_DT_OPHData inst = new D365_DT_OPHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_DT_ISL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <returns></returns>
        public List<ListD365_DT_ISLData> D365_DT_ISL(string P_BEAMLOT)
        {
            List<ListD365_DT_ISLData> results = null;

            if (!HasConnection())
                return results;

            D365_DT_ISLParameter dbPara = new D365_DT_ISLParameter();

            if (!string.IsNullOrEmpty(P_BEAMLOT))
                dbPara.P_BEAMLOT = P_BEAMLOT;

            List<D365_DT_ISLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_DT_ISL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_DT_ISLData>();
                    foreach (D365_DT_ISLResult dbResult in dbResults)
                    {
                        ListD365_DT_ISLData inst = new ListD365_DT_ISLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.ISSUEDATE = dbResult.ISSUEDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.STYLEID = dbResult.STYLEID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.SERIALID = dbResult.SERIALID;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_DT_ISH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <returns></returns>
        public List<D365_DT_ISHData> D365_DT_ISH(string P_BEAMLOT)
        {
            List<D365_DT_ISHData> results = null;

            if (!HasConnection())
                return results;

            D365_DT_ISHParameter dbPara = new D365_DT_ISHParameter();

            if (!string.IsNullOrEmpty(P_BEAMLOT))
                dbPara.P_BEAMLOT = P_BEAMLOT;

            List<D365_DT_ISHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_DT_ISH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_DT_ISHData>();
                    foreach (D365_DT_ISHResult dbResult in dbResults)
                    {
                        D365_DT_ISHData inst = new D365_DT_ISHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_DT_BPO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <returns></returns>
        public List<ListD365_DT_BPOData> D365_DT_BPO(string P_BEAMLOT)
        {
            List<ListD365_DT_BPOData> results = null;

            if (!HasConnection())
                return results;

            D365_DT_BPOParameter dbPara = new D365_DT_BPOParameter();

            if (!string.IsNullOrEmpty(P_BEAMLOT))
                dbPara.P_BEAMLOT = P_BEAMLOT;

            List<D365_DT_BPOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_DT_BPO(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_DT_BPOData>();
                    foreach (D365_DT_BPOResult dbResult in dbResults)
                    {
                        ListD365_DT_BPOData inst = new ListD365_DT_BPOData();

                        inst.PRODID = dbResult.PRODID;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.OPERATION = dbResult.OPERATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_CP_OUL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMLOT"></param>
        /// <returns></returns>
        public List<ListD365_CP_OULData> D365_CP_OUL(string P_ITEMLOT)
        {
            List<ListD365_CP_OULData> results = null;

            if (!HasConnection())
                return results;

            D365_CP_OULParameter dbPara = new D365_CP_OULParameter();

            if (!string.IsNullOrEmpty(P_ITEMLOT))
                dbPara.P_ITEMLOT = P_ITEMLOT;

            List<D365_CP_OULResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_CP_OUL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_CP_OULData>();
                    foreach (D365_CP_OULResult dbResult in dbResults)
                    {
                        ListD365_CP_OULData inst = new ListD365_CP_OULData();

                        inst.LINENO = dbResult.LINENO;
                        inst.OUTPUTDATE = dbResult.OUTPUTDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.GRADE = dbResult.GRADE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.SERIALID = dbResult.SERIALID;
                        inst.FINISH = dbResult.FINISH;
                        inst.MOVEMENTTRANS = dbResult.MOVEMENTTRANS;

                        inst.WAREHOUSE = dbResult.WAREHOUSE;
                        inst.LOCATION = dbResult.LOCATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_CP_OUH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMLOT"></param>
        /// <returns></returns>
        public List<D365_CP_OUHData> D365_CP_OUH(string P_ITEMLOT)
        {
            List<D365_CP_OUHData> results = null;

            if (!HasConnection())
                return results;

            D365_CP_OUHParameter dbPara = new D365_CP_OUHParameter();

            if (!string.IsNullOrEmpty(P_ITEMLOT))
                dbPara.P_ITEMLOT = P_ITEMLOT;

            List<D365_CP_OUHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_CP_OUH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_CP_OUHData>();
                    foreach (D365_CP_OUHResult dbResult in dbResults)
                    {
                        D365_CP_OUHData inst = new D365_CP_OUHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_CP_OPL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMLOT"></param>
        /// <returns></returns>
        public List<ListD365_CP_OPLData> D365_CP_OPL(string P_ITEMLOT)
        {
            List<ListD365_CP_OPLData> results = null;

            if (!HasConnection())
                return results;

            D365_CP_OPLParameter dbPara = new D365_CP_OPLParameter();

            if (!string.IsNullOrEmpty(P_ITEMLOT))
                dbPara.P_ITEMLOT = P_ITEMLOT;

            List<D365_CP_OPLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_CP_OPL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_CP_OPLData>();
                    foreach (D365_CP_OPLResult dbResult in dbResults)
                    {
                        ListD365_CP_OPLData inst = new ListD365_CP_OPLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.PROCQTY = dbResult.PROCQTY;
                        inst.OPRNO = dbResult.OPRNO;
                        inst.OPRID = dbResult.OPRID;
                        inst.MACHINENO = dbResult.MACHINENO;
                        inst.STARTDATETIME = dbResult.STARTDATETIME;
                        inst.ENDDATETIME = dbResult.ENDDATETIME;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_CP_OPH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMLOT"></param>
        /// <returns></returns>
        public List<D365_CP_OPHData> D365_CP_OPH(string P_ITEMLOT)
        {
            List<D365_CP_OPHData> results = null;

            if (!HasConnection())
                return results;

            D365_CP_OPHParameter dbPara = new D365_CP_OPHParameter();

            if (!string.IsNullOrEmpty(P_ITEMLOT))
                dbPara.P_ITEMLOT = P_ITEMLOT;

            List<D365_CP_OPHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_CP_OPH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_CP_OPHData>();
                    foreach (D365_CP_OPHResult dbResult in dbResults)
                    {
                        D365_CP_OPHData inst = new D365_CP_OPHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_CP_ISL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMLOT"></param>
        /// <returns></returns>
        public List<ListD365_CP_ISLData> D365_CP_ISL(string P_ITEMLOT)
        {
            List<ListD365_CP_ISLData> results = null;

            if (!HasConnection())
                return results;

            D365_CP_ISLParameter dbPara = new D365_CP_ISLParameter();

            if (!string.IsNullOrEmpty(P_ITEMLOT))
                dbPara.P_ITEMLOT = P_ITEMLOT;

            List<D365_CP_ISLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_CP_ISL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_CP_ISLData>();
                    foreach (D365_CP_ISLResult dbResult in dbResults)
                    {
                        ListD365_CP_ISLData inst = new ListD365_CP_ISLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.ISSUEDATE = dbResult.ISSUEDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.STYLEID = dbResult.STYLEID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.SERIALID = dbResult.SERIALID;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_CP_ISH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMLOT"></param>
        /// <returns></returns>
        public List<D365_CP_ISHData> D365_CP_ISH(string P_ITEMLOT)
        {
            List<D365_CP_ISHData> results = null;

            if (!HasConnection())
                return results;

            D365_CP_ISHParameter dbPara = new D365_CP_ISHParameter();

            if (!string.IsNullOrEmpty(P_ITEMLOT))
                dbPara.P_ITEMLOT = P_ITEMLOT;

            List<D365_CP_ISHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_CP_ISH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_CP_ISHData>();
                    foreach (D365_CP_ISHResult dbResult in dbResults)
                    {
                        D365_CP_ISHData inst = new D365_CP_ISHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_CP_BPO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMLOT"></param>
        /// <returns></returns>
        public List<ListD365_CP_BPOData> D365_CP_BPO(string P_ITEMLOT)
        {
            List<ListD365_CP_BPOData> results = null;

            if (!HasConnection())
                return results;

            D365_CP_BPOParameter dbPara = new D365_CP_BPOParameter();

            if (!string.IsNullOrEmpty(P_ITEMLOT))
                dbPara.P_ITEMLOT = P_ITEMLOT;

            List<D365_CP_BPOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_CP_BPO(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_CP_BPOData>();
                    foreach (D365_CP_BPOResult dbResult in dbResults)
                    {
                        ListD365_CP_BPOData inst = new ListD365_CP_BPOData();

                        inst.PRODID = dbResult.PRODID;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.OPERATION = dbResult.OPERATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_BM_OUL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <returns></returns>
        public List<ListD365_BM_OULData> D365_BM_OUL(string P_BEAMERNO)
        {
            List<ListD365_BM_OULData> results = null;

            if (!HasConnection())
                return results;

            D365_BM_OULParameter dbPara = new D365_BM_OULParameter();

            if (!string.IsNullOrEmpty(P_BEAMERNO))
                dbPara.P_BEAMERNO = P_BEAMERNO;

            List<D365_BM_OULResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_BM_OUL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_BM_OULData>();
                    foreach (D365_BM_OULResult dbResult in dbResults)
                    {
                        ListD365_BM_OULData inst = new ListD365_BM_OULData();

                        inst.LINENO = dbResult.LINENO;
                        inst.OUTPUTDATE = dbResult.OUTPUTDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.GRADE = dbResult.GRADE;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.SERIALID = dbResult.SERIALID;
                        inst.FINISH = dbResult.FINISH;
                        inst.MOVEMENTTRANS = dbResult.MOVEMENTTRANS;

                        inst.WAREHOUSE = dbResult.WAREHOUSE;
                        inst.LOCATION = dbResult.LOCATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_BM_OUH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <returns></returns>
        public List<D365_BM_OUHData> D365_BM_OUH(string P_BEAMERNO)
        {
            List<D365_BM_OUHData> results = null;

            if (!HasConnection())
                return results;

            D365_BM_OUHParameter dbPara = new D365_BM_OUHParameter();

            if (!string.IsNullOrEmpty(P_BEAMERNO))
                dbPara.P_BEAMERNO = P_BEAMERNO;

            List<D365_BM_OUHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_BM_OUH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_BM_OUHData>();
                    foreach (D365_BM_OUHResult dbResult in dbResults)
                    {
                        D365_BM_OUHData inst = new D365_BM_OUHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_BM_OPL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <returns></returns>
        public List<ListD365_BM_OPLData> D365_BM_OPL(string P_BEAMERNO)
        {
            List<ListD365_BM_OPLData> results = null;

            if (!HasConnection())
                return results;

            D365_BM_OPLParameter dbPara = new D365_BM_OPLParameter();

            if (!string.IsNullOrEmpty(P_BEAMERNO))
                dbPara.P_BEAMERNO = P_BEAMERNO;

            List<D365_BM_OPLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_BM_OPL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_BM_OPLData>();
                    foreach (D365_BM_OPLResult dbResult in dbResults)
                    {
                        ListD365_BM_OPLData inst = new ListD365_BM_OPLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.PROCQTY = dbResult.PROCQTY;
                        inst.OPRNO = dbResult.OPRNO;
                        inst.OPRID = dbResult.OPRID;
                        inst.MACHINENO = dbResult.MACHINENO;
                        inst.STARTDATETIME = dbResult.STARTDATETIME;
                        inst.ENDDATETIME = dbResult.ENDDATETIME;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_BM_OPH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <returns></returns>
        public List<D365_BM_OPHData> D365_BM_OPH(string P_BEAMERNO)
        {
            List<D365_BM_OPHData> results = null;

            if (!HasConnection())
                return results;

            D365_BM_OPHParameter dbPara = new D365_BM_OPHParameter();

            if (!string.IsNullOrEmpty(P_BEAMERNO))
                dbPara.P_BEAMERNO = P_BEAMERNO;

            List<D365_BM_OPHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_BM_OPH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_BM_OPHData>();
                    foreach (D365_BM_OPHResult dbResult in dbResults)
                    {
                        D365_BM_OPHData inst = new D365_BM_OPHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_BM_ISL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <returns></returns>
        public List<ListD365_BM_ISLData> D365_BM_ISL(string P_BEAMERNO)
        {
            List<ListD365_BM_ISLData> results = null;

            if (!HasConnection())
                return results;

            D365_BM_ISLParameter dbPara = new D365_BM_ISLParameter();

            if (!string.IsNullOrEmpty(P_BEAMERNO))
                dbPara.P_BEAMERNO = P_BEAMERNO;

            List<D365_BM_ISLResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_BM_ISL(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_BM_ISLData>();
                    foreach (D365_BM_ISLResult dbResult in dbResults)
                    {
                        ListD365_BM_ISLData inst = new ListD365_BM_ISLData();

                        inst.LINENO = dbResult.LINENO;
                        inst.ISSUEDATE = dbResult.ISSUEDATE;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.STYLEID = dbResult.STYLEID;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.SERIALID = dbResult.SERIALID;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_BM_ISH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <returns></returns>
        public List<D365_BM_ISHData> D365_BM_ISH(string P_BEAMERNO)
        {
            List<D365_BM_ISHData> results = null;

            if (!HasConnection())
                return results;

            D365_BM_ISHParameter dbPara = new D365_BM_ISHParameter();

            if (!string.IsNullOrEmpty(P_BEAMERNO))
                dbPara.P_BEAMERNO = P_BEAMERNO;

            List<D365_BM_ISHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_BM_ISH(dbPara);
                if (null != dbResults)
                {
                    results = new List<D365_BM_ISHData>();
                    foreach (D365_BM_ISHResult dbResult in dbResults)
                    {
                        D365_BM_ISHData inst = new D365_BM_ISHData();

                        inst.HEADERID = dbResult.HEADERID;
                        inst.TOTALRECORD = dbResult.TOTALRECORD;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region D365_BM_BPO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <returns></returns>
        public List<ListD365_BM_BPOData> D365_BM_BPO(string P_BEAMERNO)
        {
            List<ListD365_BM_BPOData> results = null;

            if (!HasConnection())
                return results;

            D365_BM_BPOParameter dbPara = new D365_BM_BPOParameter();

            if (!string.IsNullOrEmpty(P_BEAMERNO))
                dbPara.P_BEAMERNO = P_BEAMERNO;

            List<D365_BM_BPOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.D365_BM_BPO(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListD365_BM_BPOData>();
                    foreach (D365_BM_BPOResult dbResult in dbResults)
                    {
                        ListD365_BM_BPOData inst = new ListD365_BM_BPOData();

                        inst.PRODID = dbResult.PRODID;
                        inst.LOTNO = dbResult.LOTNO;
                        inst.ITEMID = dbResult.ITEMID;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.QTY = dbResult.QTY;
                        inst.UNIT = dbResult.UNIT;
                        inst.OPERATION = dbResult.OPERATION;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                this.Err(ex);
            }

            return results;
        }

        #endregion

        #region Insert D365

        #region Insert_ABBPO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PRODID"></param>
        /// <param name="P_LOTNO"></param>
        /// <param name="P_ITEMID"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <param name="P_RESENT"></param>
        /// <param name="P_ACTION"></param>
        /// <param name="P_QTY"></param>
        /// <param name="P_UNIT"></param>
        /// <param name="P_OPERATION"></param>
        /// <returns></returns>
        public string Insert_ABBPO(long? P_PRODID,string P_LOTNO,string P_ITEMID,string P_LOADINGTYPE,decimal? P_RESENT,string P_ACTION,decimal? P_QTY,string P_UNIT,string P_OPERATION)
        {
            string result = string.Empty;

            if (P_PRODID == null)
                return result;

            if (!HasConnectionD365())
                return result;

            Insert_ABBPOParameter dbPara = new Insert_ABBPOParameter();
            dbPara.PRODID = P_PRODID;
            dbPara.LOTNO = P_LOTNO;
            dbPara.ITEMID = P_ITEMID;
            dbPara.LOADINGTYPE = P_LOADINGTYPE;
            dbPara.RESENT = P_RESENT;
            dbPara.ACTION = P_ACTION;
            dbPara.QTY = P_QTY;
            dbPara.UNIT = P_UNIT;
            dbPara.OPERATION = P_OPERATION;

            Insert_ABBPOResult dbResult = null;

            try
            {
                dbResult = DatabaseManager_D365.Instance.Insert_ABBPO(dbPara);

                if (dbResult.ErrNum != 0)
                {
                    result = dbResult.ErrMsg;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region Insert_ABISH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_HEADERID"></param>
        /// <param name="P_PRODID"></param>
        /// <param name="P_ACTION"></param>
        /// <param name="P_RESENT"></param>
        /// <param name="P_TOTALRECORD"></param>
        /// <param name="P_LOTNO"></param>
        /// <param name="P_ITEMID"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public string Insert_ABISH(long? P_HEADERID, long? P_PRODID, string P_ACTION, decimal? P_RESENT, decimal? P_TOTALRECORD,string P_LOTNO,string P_ITEMID,string P_LOADINGTYPE)
        {
            string result = string.Empty;

            if (P_HEADERID == null)
                return result;

            if (P_PRODID == null)
                return result;

            if (!HasConnectionD365())
                return result;

            Insert_ABISHParameter dbPara = new Insert_ABISHParameter();
            dbPara.HEADERID = P_HEADERID;
            dbPara.PRODID = P_PRODID;
            dbPara.ACTION = P_ACTION;
            dbPara.RESENT = P_RESENT;
            dbPara.TOTALRECORD = P_TOTALRECORD;
            dbPara.LOTNO = P_LOTNO;
            dbPara.ITEMID = P_ITEMID;
            dbPara.LOADINGTYPE = P_LOADINGTYPE;

            Insert_ABISHResult dbResult = null;

            try
            {
                dbResult = DatabaseManager_D365.Instance.Insert_ABISH(dbPara);

                if (dbResult.ErrNum != 0)
                {
                    result = dbResult.ErrMsg;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region Insert_ABISL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_HEADERID"></param>
        /// <param name="P_LINENO"></param>
        /// <param name="P_ACTION"></param>
        /// <param name="P_RESENT"></param>
        /// <param name="P_ISSUEDATE"></param>
        /// <param name="P_ITEMID"></param>
        /// <param name="P_STYLEID"></param>
        /// <param name="P_QTY"></param>
        /// <param name="P_UNIT"></param>
        /// <param name="P_SERIALID"></param>
        /// <returns></returns>
        public string Insert_ABISL(long? P_HEADERID, decimal? P_LINENO, string P_ACTION,decimal? P_RESENT,string P_ISSUEDATE,string P_ITEMID,string P_STYLEID,decimal? P_QTY,string P_UNIT,string P_SERIALID)
        {
            string result = string.Empty;

            if (P_HEADERID == null)
                return result;

            if (!HasConnectionD365())
                return result;

            Insert_ABISLParameter dbPara = new Insert_ABISLParameter();
            dbPara.HEADERID = P_HEADERID;
            dbPara.LINENO = P_LINENO;
            dbPara.ACTION = P_ACTION;
            dbPara.RESENT = P_RESENT;
            dbPara.ISSUEDATE = P_ISSUEDATE;
            dbPara.ITEMID = P_ITEMID;
            dbPara.STYLEID = P_STYLEID;
            dbPara.QTY = P_QTY;
            dbPara.UNIT = P_UNIT;
            dbPara.SERIALID = P_SERIALID;

            Insert_ABISLResult dbResult = null;

            try
            {
                dbResult = DatabaseManager_D365.Instance.Insert_ABISL(dbPara);

                if (dbResult.ErrNum != 0)
                {
                    result = dbResult.ErrMsg;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region Insert_ABOPH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_HEADERID"></param>
        /// <param name="P_PRODID"></param>
        /// <param name="P_ACTION"></param>
        /// <param name="P_RESENT"></param>
        /// <param name="P_TOTALRECORD"></param>
        /// <param name="P_LOTNO"></param>
        /// <param name="P_ITEMID"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public string Insert_ABOPH(long? P_HEADERID, long? P_PRODID, string P_ACTION, decimal? P_RESENT, decimal? P_TOTALRECORD, string P_LOTNO, string P_ITEMID, string P_LOADINGTYPE)
        {
            string result = string.Empty;

            if (P_HEADERID == null)
                return result;

            if (P_PRODID == null)
                return result;

            if (!HasConnectionD365())
                return result;

            Insert_ABOPHParameter dbPara = new Insert_ABOPHParameter();
            dbPara.HEADERID = P_HEADERID;
            dbPara.PRODID = P_PRODID;
            dbPara.ACTION = P_ACTION;
            dbPara.RESENT = P_RESENT;
            dbPara.TOTALRECORD = P_TOTALRECORD;
            dbPara.LOTNO = P_LOTNO;
            dbPara.ITEMID = P_ITEMID;
            dbPara.LOADINGTYPE = P_LOADINGTYPE;

            Insert_ABOPHResult dbResult = null;

            try
            {
                dbResult = DatabaseManager_D365.Instance.Insert_ABOPH(dbPara);

                if (dbResult.ErrNum != 0)
                {
                    result = dbResult.ErrMsg;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region Insert_ABOPL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_HEADERID"></param>
        /// <param name="P_LINENO"></param>
        /// <param name="P_ACTION"></param>
        /// <param name="P_RESENT"></param>
        /// <param name="P_PROCQTY"></param>
        /// <param name="P_OPRNO"></param>
        /// <param name="P_OPRID"></param>
        /// <param name="P_MACHINENO"></param>
        /// <param name="P_STARTDATETIME"></param>
        /// <param name="P_ENDDATETIME"></param>
        /// <returns></returns>
        public string Insert_ABOPL(long? P_HEADERID, decimal? P_LINENO, string P_ACTION, decimal? P_RESENT, decimal? P_PROCQTY, string P_OPRNO,string P_OPRID,string P_MACHINENO,DateTime? P_STARTDATETIME, DateTime? P_ENDDATETIME)
        {
            string result = string.Empty;

            if (P_HEADERID == null)
                return result;

            if (!HasConnectionD365())
                return result;

            Insert_ABOPLParameter dbPara = new Insert_ABOPLParameter();
            dbPara.HEADERID = P_HEADERID;
            dbPara.LINENO = P_LINENO;
            dbPara.ACTION = P_ACTION;
            dbPara.RESENT = P_RESENT;
            dbPara.PROCQTY = P_PROCQTY;
            dbPara.OPRNO = P_OPRNO;
            dbPara.OPRID = P_OPRID;
            dbPara.MACHINENO = P_MACHINENO;
            dbPara.STARTDATETIME = P_STARTDATETIME;
            dbPara.ENDDATETIME = P_ENDDATETIME;

            Insert_ABOPLResult dbResult = null;

            try
            {
                dbResult = DatabaseManager_D365.Instance.Insert_ABOPL(dbPara);

                if (dbResult.ErrNum != 0)
                {
                    result = dbResult.ErrMsg;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region Insert_ABOUH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_HEADERID"></param>
        /// <param name="P_PRODID"></param>
        /// <param name="P_ACTION"></param>
        /// <param name="P_RESENT"></param>
        /// <param name="P_TOTALRECORD"></param>
        /// <param name="P_LOTNO"></param>
        /// <param name="P_ITEMID"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <returns></returns>
        public string Insert_ABOUH(long? P_HEADERID, long? P_PRODID, string P_ACTION, decimal? P_RESENT, decimal? P_TOTALRECORD,string P_LOTNO,string P_ITEMID,string P_LOADINGTYPE)
        {
            string result = string.Empty;

            if (P_HEADERID == null)
                return result;

            if (P_PRODID == null)
                return result;

            if (!HasConnectionD365())
                return result;

            Insert_ABOUHParameter dbPara = new Insert_ABOUHParameter();
            dbPara.HEADERID = P_HEADERID;
            dbPara.PRODID = P_PRODID;
            dbPara.ACTION = P_ACTION;
            dbPara.RESENT = P_RESENT;
            dbPara.TOTALRECORD = P_TOTALRECORD;
            dbPara.LOTNO = P_LOTNO;
            dbPara.ITEMID = P_ITEMID;
            dbPara.LOADINGTYPE = P_LOADINGTYPE;

            Insert_ABOUHResult dbResult = null;

            try
            {
                dbResult = DatabaseManager_D365.Instance.Insert_ABOUH(dbPara);

                if (dbResult.ErrNum != 0)
                {
                    result = dbResult.ErrMsg;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region Insert_ABOUL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_HEADERID"></param>
        /// <param name="P_LINENO"></param>
        /// <param name="P_ACTION"></param>
        /// <param name="P_RESENT"></param>
        /// <param name="P_OUTPUTDATE"></param>
        /// <param name="P_ITEMID"></param>
        /// <param name="P_QTY"></param>
        /// <param name="P_UNIT"></param>
        /// <param name="P_GROSSLENGTH"></param>
        /// <param name="P_NETLENGTH"></param>
        /// <param name="P_GROSSWEIGHT"></param>
        /// <param name="P_NETWEIGHT"></param>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_GRADE"></param>
        /// <param name="P_SERIALID"></param>
        /// <param name="P_LOADINGTYPE"></param>
        /// <param name="P_FINISH"></param>
        /// <param name="P_MOVEMENTTRANS"></param>
        /// <param name="P_WAREHOUSE"></param>
        /// <param name="P_LOCATION"></param>
        /// <returns></returns>
        public string Insert_ABOUL(long? P_HEADERID, decimal? P_LINENO, string P_ACTION, decimal? P_RESENT, string P_OUTPUTDATE,string P_ITEMID, decimal? P_QTY,string P_UNIT,decimal? P_GROSSLENGTH,
                                   decimal? P_NETLENGTH, decimal? P_GROSSWEIGHT, decimal? P_NETWEIGHT, string P_PALLETNO, string P_GRADE, string P_SERIALID, string P_LOADINGTYPE, int? P_FINISH, string P_MOVEMENTTRANS,
                                   string P_WAREHOUSE, string P_LOCATION)
        {
            string result = string.Empty;

            if (P_HEADERID == null)
                return result;

            if (!HasConnectionD365())
                return result;

            Insert_ABOULParameter dbPara = new Insert_ABOULParameter();
            dbPara.HEADERID = P_HEADERID;
            dbPara.LINENO = P_LINENO;
            dbPara.ACTION = P_ACTION;
            dbPara.RESENT = P_RESENT;
            dbPara.OUTPUTDATE = P_OUTPUTDATE;
            dbPara.ITEMID = P_ITEMID;
            dbPara.QTY = P_QTY;
            dbPara.UNIT = P_UNIT;
            dbPara.GROSSLENGTH = P_GROSSLENGTH;
            dbPara.NETLENGTH = P_NETLENGTH;
            dbPara.GROSSWEIGHT = P_GROSSWEIGHT;
            dbPara.NETWEIGHT = P_NETWEIGHT;
            dbPara.PALLETNO = P_PALLETNO;
            dbPara.GRADE = P_GRADE;
            dbPara.SERIALID = P_SERIALID;
            dbPara.LOADINGTYPE = P_LOADINGTYPE;
            dbPara.FINISH = P_FINISH;
            dbPara.MOVEMENTTRANS = P_MOVEMENTTRANS;
            dbPara.WAREHOUSE = P_WAREHOUSE;
            dbPara.LOCATION = P_LOCATION;

            Insert_ABOULResult dbResult = null;

            try
            {
                dbResult = DatabaseManager_D365.Instance.Insert_ABOUL(dbPara);

                if (dbResult.ErrNum != 0)
                {
                    result = dbResult.ErrMsg;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #endregion

        #region GetPCKPRFTP
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PCKPRFTPD365Result> GetPCKPRFTP()
        {
            List<PCKPRFTPD365Result> results = null;

            if (!HasConnectionD365())
                return results;

            try
            {
                GetPCKPRFTPParameter dbPara = new GetPCKPRFTPParameter();

                List<GetPCKPRFTPResult> dbResult = null;

                dbResult = DatabaseManager_D365.Instance.GetPCKPRFTP(dbPara);

                results = new List<PCKPRFTPD365Result>();
                PCKPRFTPD365Result inst = null;

                foreach (GetPCKPRFTPResult row in dbResult)
                {
                    inst = new PCKPRFTPD365Result();

                    inst.ANNUL = row.ANNUL;
                    inst.CDDIV = row.CDDIV;
                    inst.INVTY = row.INVTY;
                    inst.INVNO = row.INVNO;
                    inst.CDORD = row.CDORD;

                    if (row.RELNO != null)
                    {
                        inst.RELNO = Convert.ToInt32(row.RELNO);
                    }

                    inst.CUSCD = row.CUSCD;
                    inst.CUSNM = row.CUSNM;
                    inst.RECTY = row.RECTY;
                    inst.CDKE1 = row.CDKE1;
                    inst.CDKE2 = row.CDKE2;
                    inst.CSITM = row.CSITM;
                    inst.CDCON = row.CDCON;
                    inst.CDEL0 = row.CDEL0;
                    inst.GRADE = row.GRADE;

                    if (row.PIELN != null)
                    {
                        inst.PIELN = row.PIELN;
                    }

                    if (row.NETWH != null)
                    {
                        inst.NETWH = row.NETWH;
                    }

                    if (row.GRSWH != null)
                    {
                        inst.GRSWH = row.GRSWH;
                    }

                    if (row.GRSLN != null)
                    {
                        inst.GRSLN = row.GRSLN;
                    }

                    inst.PALSZ = row.PALSZ;

                    if (row.DTTRA != null)
                    {
                        inst.DTTRA = row.DTTRA;
                    }

                    if (row.DTORA != null)
                    {
                        inst.DTORA = row.DTORA;
                    }

                    inst.DTORASTR = row.DTORASTR;

                    results.Add(inst);
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;

        }

        #endregion

        #region DelPCKPRFTP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INVNO"></param>
        /// <param name="P_CDEL0"></param>
        /// <returns></returns>
        public string DelPCKPRFTP(string P_INVNO, string P_CDEL0)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(P_INVNO))
                return result;

            if (!string.IsNullOrEmpty(P_CDEL0))
                return result;

            if (!HasConnectionD365())
                return result;

            DelPCKPRFTPParameter dbPara = new DelPCKPRFTPParameter();
            dbPara.INVNO = P_INVNO;
            dbPara.CDEL0 = P_CDEL0;

            DelPCKPRFTPResult dbResult = null;

            try
            {
                dbResult = DatabaseManager_D365.Instance.DelPCKPRFTP(dbPara);

                if (dbResult.ErrNum != 0)
                {
                    result = dbResult.ErrMsg;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #endregion
    }

    #endregion
}








