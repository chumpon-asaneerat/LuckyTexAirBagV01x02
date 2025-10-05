#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Text;

#endregion

#region Extra Using

using System.Reflection;

using NLib;
using NLib.Components;
using NLib.Data;
using NLib.Logs;
using NLib.Xml;

using LuckyTex.Domains;

#endregion

namespace LuckyTex.Domains
{
    #region DelBCSPRFTP Parameter

    public class DelBCSPRFTPParameter
    {
        public System.String CDEL0 { get; set; }
        public System.String CDCON { get; set; }
        public System.String CDKE1 { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region DelBCSPRFTP Result

    public class DelBCSPRFTPResult
    {
        public System.Int32? RETURN_VALUE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region DelPCKPRFTP Parameter

    public class DelPCKPRFTPParameter
    {
        public System.String INVNO { get; set; }
        public System.String CDEL0 { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region DelPCKPRFTP Result

    public class DelPCKPRFTPResult
    {
        public System.Int32? RETURN_VALUE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region GetBCSPRFTP Parameter

    public class GetBCSPRFTPParameter
    {
        public System.String FLAGS { get; set; }
        public System.String RECTY { get; set; }
        public System.String CDSTO { get; set; }
    }

    #endregion

    #region GetBCSPRFTP Result

    public class GetBCSPRFTPResult
    {
        public System.String IFNAME { get; set; }
        public System.String ANNUL { get; set; }
        public System.String FLAGS { get; set; }
        public System.String RECTY { get; set; }
        public System.String CDSTO { get; set; }
        public System.String USRNM { get; set; }
        public System.Decimal? DTTRA { get; set; }
        public System.Decimal? DTINP { get; set; }
        public System.String CDEL0 { get; set; }
        public System.String CDCON { get; set; }
        public System.Decimal? BLELE { get; set; }
        public System.String CDUM0 { get; set; }
        public System.String CDKE1 { get; set; }
        public System.String CDKE2 { get; set; }
        public System.String CDKE3 { get; set; }
        public System.String CDKE4 { get; set; }
        public System.String CDKE5 { get; set; }
        public System.String CDLOT { get; set; }
        public System.String CDTRA { get; set; }
        public System.String REFER { get; set; }
        public System.String LOCAT { get; set; }
        public System.String CDQUA { get; set; }
        public System.String QUACA { get; set; }
        public System.Decimal? TECU1 { get; set; }
        public System.Decimal? TECU2 { get; set; }
        public System.Decimal? TECU3 { get; set; }
        public System.Decimal? TECU4 { get; set; }
        public System.String TECU5 { get; set; }
        public System.String TECU6 { get; set; }
        public System.String COMM0 { get; set; }
        public System.Decimal? DTORA { get; set; }
    }

    #endregion

    #region GetPCKPRFTP Parameter

    public class GetPCKPRFTPParameter
    {
    }

    #endregion

    #region GetPCKPRFTP Result

    public class GetPCKPRFTPResult
    {
        public System.String IFNAME { get; set; }
        public System.String ANNUL { get; set; }
        public System.String CDDIV { get; set; }
        public System.String INVTY { get; set; }
        public System.String INVNO { get; set; }
        public System.String CDORD { get; set; }
        public System.Decimal? RELNO { get; set; }
        public System.String CUSCD { get; set; }
        public System.String CUSNM { get; set; }
        public System.String RECTY { get; set; }
        public System.String CDKE1 { get; set; }
        public System.String CDKE2 { get; set; }
        public System.String CSITM { get; set; }
        public System.String CDCON { get; set; }
        public System.String CDEL0 { get; set; }
        public System.String GRADE { get; set; }
        public System.Decimal? PIELN { get; set; }
        public System.Decimal? NETWH { get; set; }
        public System.Decimal? GRSWH { get; set; }
        public System.Decimal? GRSLN { get; set; }
        public System.String PALSZ { get; set; }
        public System.Decimal? DTTRA { get; set; }
        public System.Decimal? DTORA { get; set; }
        public System.String DTORASTR { get; set; }
    }

    #endregion

    #region Insert_ABBPO Parameter

    public class Insert_ABBPOParameter
    {
        public System.Int64? PRODID { get; set; }
        public System.String LOTNO { get; set; }
        public System.String ITEMID { get; set; }
        public System.String LOADINGTYPE { get; set; }
        public System.Decimal? RESENT { get; set; }
        public System.String ACTION { get; set; }
        public System.Decimal? QTY { get; set; }
        public System.String UNIT { get; set; }
        public System.String OPERATION { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABBPO Result

    public class Insert_ABBPOResult
    {
        public System.Int32? RETURN_VALUE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABISH Parameter

    public class Insert_ABISHParameter
    {
        public System.Int64? HEADERID { get; set; }
        public System.Int64? PRODID { get; set; }
        public System.String ACTION { get; set; }
        public System.Decimal? RESENT { get; set; }
        public System.Decimal? TOTALRECORD { get; set; }
        public System.String LOTNO { get; set; }
        public System.String ITEMID { get; set; }
        public System.String LOADINGTYPE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABISH Result

    public class Insert_ABISHResult
    {
        public System.Int32? RETURN_VALUE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABISL Parameter

    public class Insert_ABISLParameter
    {
        public System.Int64? HEADERID { get; set; }
        public System.Decimal? LINENO { get; set; }
        public System.String ACTION { get; set; }
        public System.Decimal? RESENT { get; set; }
        public System.String ISSUEDATE { get; set; }
        public System.String ITEMID { get; set; }
        public System.String STYLEID { get; set; }
        public System.Decimal? QTY { get; set; }
        public System.String UNIT { get; set; }
        public System.String SERIALID { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABISL Result

    public class Insert_ABISLResult
    {
        public System.Int32? RETURN_VALUE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABOPH Parameter

    public class Insert_ABOPHParameter
    {
        public System.Int64? HEADERID { get; set; }
        public System.Int64? PRODID { get; set; }
        public System.String ACTION { get; set; }
        public System.Decimal? RESENT { get; set; }
        public System.Decimal? TOTALRECORD { get; set; }
        public System.String LOTNO { get; set; }
        public System.String ITEMID { get; set; }
        public System.String LOADINGTYPE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABOPH Result

    public class Insert_ABOPHResult
    {
        public System.Int32? RETURN_VALUE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABOPL Parameter

    public class Insert_ABOPLParameter
    {
        public System.Int64? HEADERID { get; set; }
        public System.Decimal? LINENO { get; set; }
        public System.String ACTION { get; set; }
        public System.Decimal? RESENT { get; set; }
        public System.Decimal? PROCQTY { get; set; }
        public System.String OPRNO { get; set; }
        public System.String OPRID { get; set; }
        public System.String MACHINENO { get; set; }
        public System.DateTime? STARTDATETIME { get; set; }
        public System.DateTime? ENDDATETIME { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABOPL Result

    public class Insert_ABOPLResult
    {
        public System.Int32? RETURN_VALUE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABOUH Parameter

    public class Insert_ABOUHParameter
    {
        public System.Int64? HEADERID { get; set; }
        public System.Int64? PRODID { get; set; }
        public System.String ACTION { get; set; }
        public System.Decimal? RESENT { get; set; }
        public System.Decimal? TOTALRECORD { get; set; }
        public System.String LOTNO { get; set; }
        public System.String ITEMID { get; set; }
        public System.String LOADINGTYPE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABOUH Result

    public class Insert_ABOUHResult
    {
        public System.Int32? RETURN_VALUE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABOUL Parameter

    public class Insert_ABOULParameter
    {
        public System.Int64? HEADERID { get; set; }
        public System.Decimal? LINENO { get; set; }
        public System.String ACTION { get; set; }
        public System.Decimal? RESENT { get; set; }
        public System.String OUTPUTDATE { get; set; }
        public System.String ITEMID { get; set; }
        public System.Decimal? QTY { get; set; }
        public System.String UNIT { get; set; }
        public System.Decimal? GROSSLENGTH { get; set; }
        public System.Decimal? NETLENGTH { get; set; }
        public System.Decimal? GROSSWEIGHT { get; set; }
        public System.Decimal? NETWEIGHT { get; set; }
        public System.String PALLETNO { get; set; }
        public System.String GRADE { get; set; }
        public System.String SERIALID { get; set; }
        public System.String LOADINGTYPE { get; set; }
        public System.Int32? FINISH { get; set; }
        public System.String MOVEMENTTRANS { get; set; }
        public System.String WAREHOUSE { get; set; }
        public System.String LOCATION { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion

    #region Insert_ABOUL Result

    public class Insert_ABOULResult
    {
        public System.Int32? RETURN_VALUE { get; set; }
        public System.String ErrMsg { get; set; }
        public System.Int32? ErrNum { get; set; }
    }

    #endregion
}

namespace LuckyTex.Services
{
    /// <summary>
    /// Database Manager.
    /// </summary>
    public partial class DatabaseManager_D365
    {
        #region Singelton

        private static DatabaseManager_D365 _instance = null;

        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static DatabaseManager_D365 Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(DatabaseManager_D365))
                    {
                        _instance = new DatabaseManager_D365();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Internal Variables

        private NDbConnection _manager = null;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private DatabaseManager_D365() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~DatabaseManager_D365()
        {
            Shutdown();
        }

        #endregion

        // เพิ่มการ check Connection SQL
        #region CheckConnection
        public bool CheckConnection()
        {
            bool result = false;
            if (null == _manager)
            {
                _manager = new NDbConnection();
            }
            if (!_manager.IsConnected)
            {
                _manager.Config = ConfigManager.Instance.DatabaseConfigD365;
                _manager.Connect();
            }
            if (!_manager.IsConnected)
            {
                "Cannot connect to database".Err();
            }

            result = _manager.IsConnected;

            return result;
        }
        #endregion

        #region Private Methods

        private bool HasConnection()
        {
            bool result = false;
            if (null == _manager)
            {
                _manager = new NDbConnection();
            }
            if (!_manager.IsConnected)
            {
                _manager.Config = ConfigManager.Instance.DatabaseConfigD365;
                _manager.Connect();
            }
            if (!_manager.IsConnected)
            {
                "Cannot connect to database".Err();
            }

            result = _manager.IsConnected;

            return result;
        }

        private StoredProcedureResult Execute(string procName,
            string[] paraNames, object[] paraValues)
        {
            ExecuteResult<StoredProcedureResult> ret = null;
            ret = _manager.ExecuteProcedure(procName, paraNames, paraValues);
            if (null == ret)
            {
                string msg =
                    string.Format("Execute {0} error. Null result returns.", procName);
                msg.Err();
            }
            else if (ret.HasException)
            {
                string msg =
                    string.Format("Execute {0} error.", procName);
                msg.Err(ret.Exception);
            }

            return ret;
        }

        #endregion

        #region Start/Shutdown

        /// <summary>
        /// Start service.
        /// </summary>
        public void Start()
        {
            _manager = new NDbConnection();
            _manager.Config = ConfigManager.Instance.DatabaseConfigD365;
            _manager.Connect();
        }
        /// <summary>
        /// Shutdown service.
        /// </summary>
        public void Shutdown()
        {
            if (null != _manager && _manager.IsConnected)
            {
                _manager.Disconnect();
            }
            _manager = null;
        }

        #endregion

        #region Query

        /// <summary>
        /// Execute Query.
        /// </summary>
        /// <param name="queryText">The query text.</param>
        /// <returns>Returns query result in DataTable.</returns>
        public DataTable Query(string queryText)
        {
            DataTable result = null;

            if (!HasConnection())
                return result;
            try
            {
                result = _manager.Query(queryText, null);
            }
            catch (Exception ex)
            {
                this.Err(ex);
                ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Check is database connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return (null != _manager && _manager.IsConnected);
            }
        }

        #endregion

        #region Stored Procedure methods

        #region DelBCSPRFTP

        public DelBCSPRFTPResult DelBCSPRFTP(DelBCSPRFTPParameter para)
        {
            DelBCSPRFTPResult result = null;
            if (!HasConnection())
                return result;

            string[] paraNames = new string[]
            {
                "CDEL0", 
                "CDCON", 
                "CDKE1", 
                "ErrMsg", 
                "ErrNum"
            };
            object[] paraValues = new object[]
            {
                para.CDEL0, 
                para.CDCON, 
                para.CDKE1, 
                para.ErrMsg, 
                para.ErrNum
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "DelBCSPRFTP",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                result = new DelBCSPRFTPResult();
                if (ret.Result.OutParameters["@RETURN_VALUE"] != DBNull.Value)
                    result.RETURN_VALUE = (System.Int32)ret.Result.OutParameters["@RETURN_VALUE"];
                if (ret.Result.OutParameters["@ErrMsg"] != DBNull.Value)
                    result.ErrMsg = (System.String)ret.Result.OutParameters["@ErrMsg"];
                if (ret.Result.OutParameters["@ErrNum"] != DBNull.Value)
                    result.ErrNum = (System.Int32)ret.Result.OutParameters["@ErrNum"];
            }

            return result;
        }

        #endregion

        #region DelPCKPRFTP

        public DelPCKPRFTPResult DelPCKPRFTP(DelPCKPRFTPParameter para)
        {
            DelPCKPRFTPResult result = null;
            if (!HasConnection())
                return result;

            string[] paraNames = new string[]
            {
                "INVNO", 
                "CDEL0", 
                "ErrMsg", 
                "ErrNum"
            };
            object[] paraValues = new object[]
            {
                para.INVNO, 
                para.CDEL0, 
                para.ErrMsg, 
                para.ErrNum
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "DelPCKPRFTP",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                result = new DelPCKPRFTPResult();
                if (ret.Result.OutParameters["@RETURN_VALUE"] != DBNull.Value)
                    result.RETURN_VALUE = (System.Int32)ret.Result.OutParameters["@RETURN_VALUE"];
                if (ret.Result.OutParameters["@ErrMsg"] != DBNull.Value)
                    result.ErrMsg = (System.String)ret.Result.OutParameters["@ErrMsg"];
                if (ret.Result.OutParameters["@ErrNum"] != DBNull.Value)
                    result.ErrNum = (System.Int32)ret.Result.OutParameters["@ErrNum"];
            }

            return result;
        }

        #endregion

        #region GetBCSPRFTP

        public List<GetBCSPRFTPResult> GetBCSPRFTP(GetBCSPRFTPParameter para)
        {
            List<GetBCSPRFTPResult> results = new List<GetBCSPRFTPResult>();
            if (!HasConnection())
                return results;

            string[] paraNames = new string[]
            {
                "FLAGS", 
                "RECTY", 
                "CDSTO"
            };
            object[] paraValues = new object[]
            {
                para.FLAGS, 
                para.RECTY, 
                para.CDSTO
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "GetBCSPRFTP",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                if (null == ret.Result.Table)
                {
                    // check has error code.
                    //ret.Result.OutParameters[""];
                }
                else
                {
                    foreach (DataRow row in ret.Result.Table.Rows)
                    {
                        GetBCSPRFTPResult result =
                            new GetBCSPRFTPResult();

                        result.IFNAME = row.Field<System.String>("IFNAME");
                        result.ANNUL = row.Field<System.String>("ANNUL");
                        result.FLAGS = row.Field<System.String>("FLAGS");
                        result.RECTY = row.Field<System.String>("RECTY");
                        result.CDSTO = row.Field<System.String>("CDSTO");
                        result.USRNM = row.Field<System.String>("USRNM");
                        result.DTTRA = row.Field<System.Decimal?>("DTTRA");
                        result.DTINP = row.Field<System.Decimal?>("DTINP");
                        result.CDEL0 = row.Field<System.String>("CDEL0");
                        result.CDCON = row.Field<System.String>("CDCON");
                        result.BLELE = row.Field<System.Decimal?>("BLELE");
                        result.CDUM0 = row.Field<System.String>("CDUM0");
                        result.CDKE1 = row.Field<System.String>("CDKE1");
                        result.CDKE2 = row.Field<System.String>("CDKE2");
                        result.CDKE3 = row.Field<System.String>("CDKE3");
                        result.CDKE4 = row.Field<System.String>("CDKE4");
                        result.CDKE5 = row.Field<System.String>("CDKE5");
                        result.CDLOT = row.Field<System.String>("CDLOT");
                        result.CDTRA = row.Field<System.String>("CDTRA");
                        result.REFER = row.Field<System.String>("REFER");
                        result.LOCAT = row.Field<System.String>("LOCAT");
                        result.CDQUA = row.Field<System.String>("CDQUA");
                        result.QUACA = row.Field<System.String>("QUACA");
                        result.TECU1 = row.Field<System.Decimal?>("TECU1");
                        result.TECU2 = row.Field<System.Decimal?>("TECU2");
                        result.TECU3 = row.Field<System.Decimal?>("TECU3");
                        result.TECU4 = row.Field<System.Decimal?>("TECU4");
                        result.TECU5 = row.Field<System.String>("TECU5");
                        result.TECU6 = row.Field<System.String>("TECU6");
                        result.COMM0 = row.Field<System.String>("COMM0");
                        result.DTORA = row.Field<System.Decimal?>("DTORA");

                        results.Add(result);
                    }
                }
            }

            return results;
        }

        #endregion

        #region GetPCKPRFTP

        public List<GetPCKPRFTPResult> GetPCKPRFTP(GetPCKPRFTPParameter para)
        {
            List<GetPCKPRFTPResult> results = new List<GetPCKPRFTPResult>();
            if (!HasConnection())
                return results;

            string[] paraNames = new string[]
            {
            };
            object[] paraValues = new object[]
            {
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "GetPCKPRFTP",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                if (null == ret.Result.Table)
                {
                    // check has error code.
                    //ret.Result.OutParameters[""];
                }
                else
                {
                    foreach (DataRow row in ret.Result.Table.Rows)
                    {
                        GetPCKPRFTPResult result =
                            new GetPCKPRFTPResult();

                        result.IFNAME = row.Field<System.String>("IFNAME");
                        result.ANNUL = row.Field<System.String>("ANNUL");
                        result.CDDIV = row.Field<System.String>("CDDIV");
                        result.INVTY = row.Field<System.String>("INVTY");
                        result.INVNO = row.Field<System.String>("INVNO");
                        result.CDORD = row.Field<System.String>("CDORD");
                        result.RELNO = row.Field<System.Decimal?>("RELNO");
                        result.CUSCD = row.Field<System.String>("CUSCD");
                        result.CUSNM = row.Field<System.String>("CUSNM");
                        result.RECTY = row.Field<System.String>("RECTY");
                        result.CDKE1 = row.Field<System.String>("CDKE1");
                        result.CDKE2 = row.Field<System.String>("CDKE2");
                        result.CSITM = row.Field<System.String>("CSITM");
                        result.CDCON = row.Field<System.String>("CDCON");
                        result.CDEL0 = row.Field<System.String>("CDEL0");
                        result.GRADE = row.Field<System.String>("GRADE");
                        result.PIELN = row.Field<System.Decimal?>("PIELN");
                        result.NETWH = row.Field<System.Decimal?>("NETWH");
                        result.GRSWH = row.Field<System.Decimal?>("GRSWH");
                        result.GRSLN = row.Field<System.Decimal?>("GRSLN");
                        result.PALSZ = row.Field<System.String>("PALSZ");
                        result.DTTRA = row.Field<System.Decimal?>("DTTRA");
                        result.DTORA = row.Field<System.Decimal?>("DTORA");
                        result.DTORASTR = row.Field<System.String>("DTORASTR");

                        results.Add(result);
                    }
                }
            }

            return results;
        }

        #endregion

        #region Insert_ABBPO

        public Insert_ABBPOResult Insert_ABBPO(Insert_ABBPOParameter para)
        {
            Insert_ABBPOResult result = null;
            if (!HasConnection())
                return result;

            string[] paraNames = new string[]
            {
                "PRODID", 
                "LOTNO", 
                "ITEMID", 
                "LOADINGTYPE", 
                "RESENT", 
                "ACTION", 
                "QTY", 
                "UNIT", 
                "OPERATION", 
                "ErrMsg", 
                "ErrNum"
            };
            object[] paraValues = new object[]
            {
                para.PRODID, 
                para.LOTNO, 
                para.ITEMID, 
                para.LOADINGTYPE, 
                para.RESENT, 
                para.ACTION, 
                para.QTY, 
                para.UNIT, 
                para.OPERATION, 
                para.ErrMsg, 
                para.ErrNum
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "Insert_ABBPO",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                result = new Insert_ABBPOResult();
                if (ret.Result.OutParameters["@RETURN_VALUE"] != DBNull.Value)
                    result.RETURN_VALUE = (System.Int32)ret.Result.OutParameters["@RETURN_VALUE"];
                if (ret.Result.OutParameters["@ErrMsg"] != DBNull.Value)
                    result.ErrMsg = (System.String)ret.Result.OutParameters["@ErrMsg"];
                if (ret.Result.OutParameters["@ErrNum"] != DBNull.Value)
                    result.ErrNum = (System.Int32)ret.Result.OutParameters["@ErrNum"];
            }

            return result;
        }

        #endregion

        #region Insert_ABISH

        public Insert_ABISHResult Insert_ABISH(Insert_ABISHParameter para)
        {
            Insert_ABISHResult result = null;
            if (!HasConnection())
                return result;

            string[] paraNames = new string[]
            {
                "HEADERID", 
                "PRODID", 
                "ACTION", 
                "RESENT", 
                "TOTALRECORD", 
                "LOTNO", 
                "ITEMID", 
                "LOADINGTYPE", 
                "ErrMsg", 
                "ErrNum"
            };
            object[] paraValues = new object[]
            {
                para.HEADERID, 
                para.PRODID, 
                para.ACTION, 
                para.RESENT, 
                para.TOTALRECORD, 
                para.LOTNO, 
                para.ITEMID, 
                para.LOADINGTYPE, 
                para.ErrMsg, 
                para.ErrNum
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "Insert_ABISH",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                result = new Insert_ABISHResult();
                if (ret.Result.OutParameters["@RETURN_VALUE"] != DBNull.Value)
                    result.RETURN_VALUE = (System.Int32)ret.Result.OutParameters["@RETURN_VALUE"];
                if (ret.Result.OutParameters["@ErrMsg"] != DBNull.Value)
                    result.ErrMsg = (System.String)ret.Result.OutParameters["@ErrMsg"];
                if (ret.Result.OutParameters["@ErrNum"] != DBNull.Value)
                    result.ErrNum = (System.Int32)ret.Result.OutParameters["@ErrNum"];
            }

            return result;
        }

        #endregion

        #region Insert_ABISL

        public Insert_ABISLResult Insert_ABISL(Insert_ABISLParameter para)
        {
            Insert_ABISLResult result = null;
            if (!HasConnection())
                return result;

            string[] paraNames = new string[]
            {
                "HEADERID", 
                "LINENO", 
                "ACTION", 
                "RESENT", 
                "ISSUEDATE", 
                "ITEMID", 
                "STYLEID", 
                "QTY", 
                "UNIT", 
                "SERIALID", 
                "ErrMsg", 
                "ErrNum"
            };
            object[] paraValues = new object[]
            {
                para.HEADERID, 
                para.LINENO, 
                para.ACTION, 
                para.RESENT, 
                para.ISSUEDATE, 
                para.ITEMID, 
                para.STYLEID, 
                para.QTY, 
                para.UNIT, 
                para.SERIALID, 
                para.ErrMsg, 
                para.ErrNum
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "Insert_ABISL",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                result = new Insert_ABISLResult();
                if (ret.Result.OutParameters["@RETURN_VALUE"] != DBNull.Value)
                    result.RETURN_VALUE = (System.Int32)ret.Result.OutParameters["@RETURN_VALUE"];
                if (ret.Result.OutParameters["@ErrMsg"] != DBNull.Value)
                    result.ErrMsg = (System.String)ret.Result.OutParameters["@ErrMsg"];
                if (ret.Result.OutParameters["@ErrNum"] != DBNull.Value)
                    result.ErrNum = (System.Int32)ret.Result.OutParameters["@ErrNum"];
            }

            return result;
        }

        #endregion

        #region Insert_ABOPH

        public Insert_ABOPHResult Insert_ABOPH(Insert_ABOPHParameter para)
        {
            Insert_ABOPHResult result = null;
            if (!HasConnection())
                return result;

            string[] paraNames = new string[]
            {
                "HEADERID", 
                "PRODID", 
                "ACTION", 
                "RESENT", 
                "TOTALRECORD", 
                "LOTNO", 
                "ITEMID", 
                "LOADINGTYPE", 
                "ErrMsg", 
                "ErrNum"
            };
            object[] paraValues = new object[]
            {
                para.HEADERID, 
                para.PRODID, 
                para.ACTION, 
                para.RESENT, 
                para.TOTALRECORD, 
                para.LOTNO, 
                para.ITEMID, 
                para.LOADINGTYPE, 
                para.ErrMsg, 
                para.ErrNum
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "Insert_ABOPH",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                result = new Insert_ABOPHResult();
                if (ret.Result.OutParameters["@RETURN_VALUE"] != DBNull.Value)
                    result.RETURN_VALUE = (System.Int32)ret.Result.OutParameters["@RETURN_VALUE"];
                if (ret.Result.OutParameters["@ErrMsg"] != DBNull.Value)
                    result.ErrMsg = (System.String)ret.Result.OutParameters["@ErrMsg"];
                if (ret.Result.OutParameters["@ErrNum"] != DBNull.Value)
                    result.ErrNum = (System.Int32)ret.Result.OutParameters["@ErrNum"];
            }

            return result;
        }

        #endregion

        #region Insert_ABOPL

        public Insert_ABOPLResult Insert_ABOPL(Insert_ABOPLParameter para)
        {
            Insert_ABOPLResult result = null;
            if (!HasConnection())
                return result;

            string[] paraNames = new string[]
            {
                "HEADERID", 
                "LINENO", 
                "ACTION", 
                "RESENT", 
                "PROCQTY", 
                "OPRNO", 
                "OPRID", 
                "MACHINENO", 
                "STARTDATETIME", 
                "ENDDATETIME", 
                "ErrMsg", 
                "ErrNum"
            };
            object[] paraValues = new object[]
            {
                para.HEADERID, 
                para.LINENO, 
                para.ACTION, 
                para.RESENT, 
                para.PROCQTY, 
                para.OPRNO, 
                para.OPRID, 
                para.MACHINENO, 
                para.STARTDATETIME, 
                para.ENDDATETIME, 
                para.ErrMsg, 
                para.ErrNum
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "Insert_ABOPL",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                result = new Insert_ABOPLResult();
                if (ret.Result.OutParameters["@RETURN_VALUE"] != DBNull.Value)
                    result.RETURN_VALUE = (System.Int32)ret.Result.OutParameters["@RETURN_VALUE"];
                if (ret.Result.OutParameters["@ErrMsg"] != DBNull.Value)
                    result.ErrMsg = (System.String)ret.Result.OutParameters["@ErrMsg"];
                if (ret.Result.OutParameters["@ErrNum"] != DBNull.Value)
                    result.ErrNum = (System.Int32)ret.Result.OutParameters["@ErrNum"];
            }

            return result;
        }

        #endregion

        #region Insert_ABOUH

        public Insert_ABOUHResult Insert_ABOUH(Insert_ABOUHParameter para)
        {
            Insert_ABOUHResult result = null;
            if (!HasConnection())
                return result;

            string[] paraNames = new string[]
            {
                "HEADERID", 
                "PRODID", 
                "ACTION", 
                "RESENT", 
                "TOTALRECORD", 
                "LOTNO", 
                "ITEMID", 
                "LOADINGTYPE", 
                "ErrMsg", 
                "ErrNum"
            };
            object[] paraValues = new object[]
            {
                para.HEADERID, 
                para.PRODID, 
                para.ACTION, 
                para.RESENT, 
                para.TOTALRECORD, 
                para.LOTNO, 
                para.ITEMID, 
                para.LOADINGTYPE, 
                para.ErrMsg, 
                para.ErrNum
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "Insert_ABOUH",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                result = new Insert_ABOUHResult();
                if (ret.Result.OutParameters["@RETURN_VALUE"] != DBNull.Value)
                    result.RETURN_VALUE = (System.Int32)ret.Result.OutParameters["@RETURN_VALUE"];
                if (ret.Result.OutParameters["@ErrMsg"] != DBNull.Value)
                    result.ErrMsg = (System.String)ret.Result.OutParameters["@ErrMsg"];
                if (ret.Result.OutParameters["@ErrNum"] != DBNull.Value)
                    result.ErrNum = (System.Int32)ret.Result.OutParameters["@ErrNum"];
            }

            return result;
        }

        #endregion

        #region Insert_ABOUL

        public Insert_ABOULResult Insert_ABOUL(Insert_ABOULParameter para)
        {
            Insert_ABOULResult result = null;
            if (!HasConnection())
                return result;

            string[] paraNames = new string[]
            {
                "HEADERID", 
                "LINENO", 
                "ACTION", 
                "RESENT", 
                "OUTPUTDATE", 
                "ITEMID", 
                "QTY", 
                "UNIT", 
                "GROSSLENGTH", 
                "NETLENGTH", 
                "GROSSWEIGHT", 
                "NETWEIGHT", 
                "PALLETNO", 
                "GRADE", 
                "SERIALID", 
                "LOADINGTYPE", 
                "FINISH", 
                "MOVEMENTTRANS", 
                "WAREHOUSE", 
                "LOCATION", 
                "ErrMsg", 
                "ErrNum"
            };
            object[] paraValues = new object[]
            {
                para.HEADERID, 
                para.LINENO, 
                para.ACTION, 
                para.RESENT, 
                para.OUTPUTDATE, 
                para.ITEMID, 
                para.QTY, 
                para.UNIT, 
                para.GROSSLENGTH, 
                para.NETLENGTH, 
                para.GROSSWEIGHT, 
                para.NETWEIGHT, 
                para.PALLETNO, 
                para.GRADE, 
                para.SERIALID, 
                para.LOADINGTYPE, 
                para.FINISH, 
                para.MOVEMENTTRANS, 
                para.WAREHOUSE, 
                para.LOCATION, 
                para.ErrMsg, 
                para.ErrNum
            };

            ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
                "Insert_ABOUL",
                paraNames, paraValues);
            if (null != ret && !ret.HasException)
            {
                result = new Insert_ABOULResult();
                if (ret.Result.OutParameters["@RETURN_VALUE"] != DBNull.Value)
                    result.RETURN_VALUE = (System.Int32)ret.Result.OutParameters["@RETURN_VALUE"];
                if (ret.Result.OutParameters["@ErrMsg"] != DBNull.Value)
                    result.ErrMsg = (System.String)ret.Result.OutParameters["@ErrMsg"];
                if (ret.Result.OutParameters["@ErrNum"] != DBNull.Value)
                    result.ErrNum = (System.Int32)ret.Result.OutParameters["@ErrNum"];
            }

            return result;
        }

        #endregion

        #endregion
    }

}
