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

using System.Configuration;
using System.Data.OleDb;
using System.Windows.Media.Imaging;

#endregion

namespace LuckyTex.Services
{
    #region PCKPRFTP Data Service

    /// <summary>
    /// The data service for PCKPRFTP.
    /// </summary>
    public class PCKPRFTPDataService : BaseDataService
    {
        #region Singelton

        private static PCKPRFTPDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static PCKPRFTPDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(PCKPRFTPDataService))
                    {
                        _instance = new PCKPRFTPDataService();
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
        private PCKPRFTPDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~PCKPRFTPDataService()
        {
        }

        #endregion

        #region Public Methods

        #region AS400

        #region chkConAS400
        public bool chkConAS400(string ConStr)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConStr))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();


                    if (con.State == ConnectionState.Open)
                        con.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }
        #endregion

        #region GetPCKPRFTP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConStr"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<PCKPRFTPResult> GetPCKPRFTP(string ConStr, string where)
        {
            List<PCKPRFTPResult> results = null;

            if (string.IsNullOrWhiteSpace(ConStr))
                return results;

            try
            {
                string query = string.Empty;
                StringBuilder q = new StringBuilder();
                //q.Append("Select * From PCKPRFTP ");

                q.Append("Select #ANNUL AS ANNUL , #CDDIV AS CDDIV, #INVTY AS INVTY, #INVNO AS INVNO, #CDORD AS CDORD, #RELNO AS RELNO, #CUSCD AS CUSCD, #CUSNM AS CUSNM, #RECTY AS RECTY, #CDKE1 AS CDKE1, #CDKE2 AS CDKE2 "
                + " , #CSITM AS CSITM , #CDCON AS CDCON , #CDEL0 AS CDEL0 , #GRADE AS GRADE , #PIELN AS PIELN , #NETWH AS NETWH , #GRSWH AS GRSWH , #GRSLN AS GRSLN , #PALSZ AS PALSZ, #DTTRA AS DTTRA, #DTORA AS DTORA"
                + " From PCKPRFTP ");

                q.Append(where);

                query = q.ToString();


                DataSet ds = new DataSet();

                OleDbConnection connection;

                connection = new OleDbConnection(ConStr);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                OleDbDataAdapter oledbAdapter;
                oledbAdapter = new OleDbDataAdapter(query, connection);

                oledbAdapter.Fill(ds, "PCKPRFTP");

                if (ds.Tables["PCKPRFTP"].Rows.Count > 0)
                {
                    results = new List<PCKPRFTPResult>();
                    PCKPRFTPResult inst = null;

                    foreach (DataRow row in ds.Tables["PCKPRFTP"].Rows)
                    {
                        inst = new PCKPRFTPResult();

                        inst.ANNUL = row["ANNUL"].ToString();
                        inst.CDDIV = row["CDDIV"].ToString();
                        inst.INVTY = row["INVTY"].ToString();
                        inst.INVNO = row["INVNO"].ToString();
                        inst.CDORD = row["CDORD"].ToString();

                        if (row["RELNO"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["RELNO"].ToString()))
                            {
                                inst.RELNO = int.Parse(row["RELNO"].ToString());
                            }
                        }

                        inst.CUSCD = row["CUSCD"].ToString();
                        inst.CUSNM = row["CUSNM"].ToString();
                        inst.RECTY = row["RECTY"].ToString();
                        inst.CDKE1 = row["CDKE1"].ToString();
                        inst.CDKE2 = row["CDKE2"].ToString();
                        inst.CSITM = row["CSITM"].ToString();
                        inst.CDCON = row["CDCON"].ToString();
                        inst.CDEL0 = row["CDEL0"].ToString();
                        inst.GRADE = row["GRADE"].ToString();

                        if (row["PIELN"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["PIELN"].ToString()))
                            {
                                inst.PIELN = decimal.Parse(row["PIELN"].ToString());
                            }
                        }

                        if (row["NETWH"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["NETWH"].ToString()))
                            {
                                inst.NETWH = decimal.Parse(row["NETWH"].ToString());
                            }
                        }

                        if (row["GRSWH"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["GRSWH"].ToString()))
                            {
                                inst.GRSWH = decimal.Parse(row["GRSWH"].ToString());
                            }
                        }

                        if (row["GRSLN"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["GRSLN"].ToString()))
                            {
                                inst.GRSLN = decimal.Parse(row["GRSLN"].ToString());
                            }
                        }

                        inst.PALSZ = row["PALSZ"].ToString();

                        if (row["DTTRA"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["DTTRA"].ToString()))
                            {
                                inst.DTTRA = row["DTTRA"].ToString();
                            }
                        }

                        if (row["DTORA"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["DTORA"].ToString()))
                            {
                                inst.DTORA = row["DTORA"].ToString();
                            }
                        }

                        results.Add(inst);
                    }
                }

                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
            catch (Exception ex)
            {
                ex.Err();
            }
            return results;
        }

        #endregion

        #region DeletePCKPRFTP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConStr"></param>
        /// <param name="INVNO"></param>
        /// <param name="CDEL0"></param>
        /// <returns></returns>
        public bool DeletePCKPRFTP(string ConStr, string INVNO, string CDEL0)
        {
            bool chkError = true;

            if (string.IsNullOrWhiteSpace(ConStr))
                return false;

            try
            {
                string query = string.Empty;

                StringBuilder q = new StringBuilder();

                q.Append("DELETE FROM PCKPRFTP ");
                q.Append(" Where #INVNO = '");
                q.Append(INVNO);
                q.Append("' And #CDEL0 = '");
                q.Append(CDEL0);
                q.Append("'");

                query = q.ToString();

                DataSet ds = new DataSet();

                OleDbConnection connection;

                connection = new OleDbConnection(ConStr);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            catch (Exception ex)
            {
                chkError = false;
                ex.Err();
            }

            return chkError;
        }

        #endregion

        #region ยังไม่ได้ใช้

        #region InsertDataPCKPRFTP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConStr"></param>
        /// <param name="ANNUL"></param>
        /// <param name="CDDIV"></param>
        /// <param name="INVTY"></param>
        /// <param name="INVNO"></param>
        /// <param name="CDORD"></param>
        /// <param name="RELNO"></param>
        /// <param name="CUSCD"></param>
        /// <param name="CUSNM"></param>
        /// <param name="RECTY"></param>
        /// <param name="CDKE1"></param>
        /// <param name="CDKE2"></param>
        /// <param name="CSITM"></param>
        /// <param name="CDCON"></param>
        /// <param name="CDEL0"></param>
        /// <param name="GRADE"></param>
        /// <param name="PIELN"></param>
        /// <param name="NETWH"></param>
        /// <param name="GRSWH"></param>
        /// <param name="GRSLN"></param>
        /// <param name="PALSZ"></param>
        /// <param name="DTTRA"></param>
        /// <param name="DTORA"></param>
        /// <returns></returns>
        public bool InsertDataPCKPRFTP(string ConStr, string ANNUL, string CDDIV, string INVTY, string INVNO, string CDORD, int? RELNO, string CUSCD, string CUSNM, string RECTY, string CDKE1,
                string CDKE2, string CSITM, string CDCON, string CDEL0, string GRADE, decimal? PIELN, decimal? NETWH, decimal? GRSWH, decimal? GRSLN, string PALSZ, int? DTTRA, Int64? DTORA)
        {

            string queryString = string.Empty;

            queryString = " INSERT INTO PCKPRFTP( #ANNUL , #CDDIV , #INVTY , #INVNO , #CDORD , #RELNO , #CUSCD , #CUSNM , #RECTY , #CDKE1 , #CDKE2 , #CSITM "
                            + " , #CDCON , #CDEL0 , #GRADE , #PIELN , #NETWH , #GRSWH , #GRSLN , #PALSZ , #DTTRA , #DTORA ) "
                            + " VALUES( ? , ? , ? , ? , ? , ? , ? , ? , ? , ? , ? , ? "
                            + " , ? , ? , ? , ? , ? , ? , ? , ? , ? , ? ) ";

            try
            {
                DataSet ds = new DataSet();

                using (OleDbConnection con = new OleDbConnection(ConStr))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    using (OleDbCommand cmd = new OleDbCommand(queryString, con))
                    {
                        cmd.Parameters.AddWithValue("@#ANNUL", OleDbType.Char).Value = ANNUL;
                        cmd.Parameters.AddWithValue("@#CDDIV", OleDbType.Char).Value = CDDIV;
                        cmd.Parameters.AddWithValue("@#INVTY", OleDbType.Char).Value = INVTY;
                        cmd.Parameters.AddWithValue("@#INVNO", OleDbType.Char).Value = INVNO;
                        cmd.Parameters.AddWithValue("@#CDORD", OleDbType.Char).Value = CDORD;
                        cmd.Parameters.AddWithValue("@#RELNO", OleDbType.Numeric).Value = RELNO;
                        cmd.Parameters.AddWithValue("@#CUSCD", OleDbType.Char).Value = CUSCD;
                        cmd.Parameters.AddWithValue("@#CUSNM", OleDbType.Char).Value = CUSNM;
                        cmd.Parameters.AddWithValue("@#RECTY", OleDbType.Char).Value = RECTY;
                        cmd.Parameters.AddWithValue("@#CDKE1", OleDbType.Char).Value = CDKE1;
                        cmd.Parameters.AddWithValue("@#CDKE2", OleDbType.Char).Value = CDKE2;
                        cmd.Parameters.AddWithValue("@#CSITM", OleDbType.Char).Value = CSITM;
                        cmd.Parameters.AddWithValue("@#CDCON", OleDbType.Char).Value = CDCON;
                        cmd.Parameters.AddWithValue("@#CDEL0", OleDbType.Char).Value = CDEL0;
                        cmd.Parameters.AddWithValue("@#GRADE", OleDbType.Char).Value = GRADE;
                        cmd.Parameters.AddWithValue("@#PIELN", OleDbType.Numeric).Value = PIELN;
                        cmd.Parameters.AddWithValue("@#NETWH", OleDbType.Numeric).Value = NETWH;
                        cmd.Parameters.AddWithValue("@#GRSWH", OleDbType.Numeric).Value = GRSWH;
                        cmd.Parameters.AddWithValue("@#GRSLN", OleDbType.Numeric).Value = GRSLN;
                        cmd.Parameters.AddWithValue("@#PALSZ", OleDbType.Char).Value = PALSZ;
                        cmd.Parameters.AddWithValue("@#DTTRA", OleDbType.Numeric).Value = DTTRA;
                        cmd.Parameters.AddWithValue("@#DTORA", OleDbType.Numeric).Value = DTORA;

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        if (con.State == ConnectionState.Open)
                            con.Close();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                return false;
            }
        }
        #endregion

        #region UpdatePCKPRFTP_FLAGS

        public bool UpdatePCKPRFTP_FLAGS(string ConStr, string CDEL0, string CDCON, string CDKE1)
        {
            bool chkError = true;

            if (string.IsNullOrWhiteSpace(ConStr))
                return false;

            try
            {
                string query = string.Empty;

                query = "Update PCKPRFTP "
                     + " Set #ANNUL = 'A' "
                     + " Where #CDEL0 = '" + CDEL0 + "'"
                     + " And #CDCON = '" + CDCON + "'"
                     + " And #CDKE1 = '" + CDKE1 + "'"
                     + " And #RECTY = 'S' ";

                DataSet ds = new DataSet();

                OleDbConnection connection;

                connection = new OleDbConnection(ConStr);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            catch (Exception ex)
            {
                chkError = false;
                ex.Err();
            }

            return chkError;
        }

        #endregion

        #endregion

        #endregion

        #region PCKPRFTP_UPDATESCAN
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INVNO"></param>
        /// <param name="P_CDEL0"></param>
        /// <param name="P_SCANBY"></param>
        /// <returns></returns>
        public bool PCKPRFTP_UPDATESCAN(string P_INVNO, string P_CDEL0, string P_SCANBY, decimal? P_RUNNO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_INVNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_CDEL0))
                return result;

            if (string.IsNullOrWhiteSpace(P_SCANBY))
                return result;

            //if (P_RUNNO == null)
            //    return result;

            if (!HasConnection())
                return result;

            PCKPRFTP_UPDATESCANParameter dbPara = new PCKPRFTP_UPDATESCANParameter();

            dbPara.P_INVNO = P_INVNO;
            dbPara.P_CDEL0 = P_CDEL0;
            dbPara.P_SCANBY = P_SCANBY;

            if (P_RUNNO != null)
                dbPara.P_RUNNO = P_RUNNO;

            PCKPRFTP_UPDATESCANResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PCKPRFTP_UPDATESCAN(dbPara);

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

        #region PCKPRFTP_UPDATERUNNO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INVNO"></param>
        /// <param name="P_CDEL0"></param>
        /// <param name="P_RUNNO"></param>
        /// <param name="P_EDITBY"></param>
        /// <param name="P_INUSE"></param>
        /// <returns></returns>
        public bool PCKPRFTP_UPDATERUNNO(string P_INVNO, string P_CDEL0, decimal? P_RUNNO, string P_EDITBY, decimal? P_INUSE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_INVNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_CDEL0))
                return result;

            if (!HasConnection())
                return result;

            PCKPRFTP_UPDATERUNNOParameter dbPara = new PCKPRFTP_UPDATERUNNOParameter();

            dbPara.P_INVNO = P_INVNO;
            dbPara.P_CDEL0 = P_CDEL0;
            dbPara.P_RUNNO = P_RUNNO;
            dbPara.P_EDITBY = P_EDITBY;

            if (P_INUSE != null)
                dbPara.P_INUSE = P_INUSE;
            else
                dbPara.P_INUSE = 1;

            PCKPRFTP_UPDATERUNNOResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PCKPRFTP_UPDATERUNNO(dbPara);

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

        #region PCKPRFTP_UPDATEINUSE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INVNO"></param>
        /// <param name="P_CDEL0"></param>
        /// <param name="P_EDITBY"></param>
        /// <param name="P_INUSE"></param>
        /// <returns></returns>
        public bool PCKPRFTP_UPDATEINUSE(string P_INVNO, string P_CDEL0, string P_EDITBY, decimal? P_INUSE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_INVNO))
                return result;

            //if (string.IsNullOrWhiteSpace(P_CDEL0))
            //    return result;

            if (!HasConnection())
                return result;

            PCKPRFTP_UPDATEINUSEParameter dbPara = new PCKPRFTP_UPDATEINUSEParameter();

            dbPara.P_INVNO = P_INVNO;
            dbPara.P_CDEL0 = P_CDEL0;
            dbPara.P_EDITBY = P_EDITBY;

            if (P_INUSE != null)
                dbPara.P_INUSE = P_INUSE;
            else
                dbPara.P_INUSE = 1;

            PCKPRFTP_UPDATEINUSEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PCKPRFTP_UPDATEINUSE(dbPara);

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

        #region PCKPRFTP_UPDATECUSNO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_CDEL0"></param>
        /// <param name="P_CUSNO"></param>
        /// <param name="P_EDITBY"></param>
        /// <param name="P_INUSE"></param>
        /// <returns></returns>
        public bool PCKPRFTP_UPDATECUSNO(string P_CDEL0, decimal? P_CUSNO, string P_EDITBY, decimal? P_INUSE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_CDEL0))
                return result;

            if (!HasConnection())
                return result;

            PCKPRFTP_UPDATECUSNOParameter dbPara = new PCKPRFTP_UPDATECUSNOParameter();

            dbPara.P_CDEL0 = P_CDEL0;
            dbPara.P_CUSNO = P_CUSNO;
            dbPara.P_EDITBY = P_EDITBY;

            if (P_INUSE != null)
                dbPara.P_INUSE = P_INUSE;
            else
                dbPara.P_INUSE = 0;

            PCKPRFTP_UPDATECUSNOResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PCKPRFTP_UPDATECUSNO(dbPara);

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

        #region PCKPRFTP_INSERTUPDATE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ANNUL"></param>
        /// <param name="P_CDDIV"></param>
        /// <param name="P_INVTY"></param>
        /// <param name="P_INVNO"></param>
        /// <param name="P_CDORD"></param>
        /// <param name="P_RELNO"></param>
        /// <param name="P_CUSCD"></param>
        /// <param name="P_CUSNM"></param>
        /// <param name="P_RECTY"></param>
        /// <param name="P_CDKE1"></param>
        /// <param name="P_CDKE2"></param>
        /// <param name="P_CSITM"></param>
        /// <param name="P_CDCON"></param>
        /// <param name="P_CDEL0"></param>
        /// <param name="P_GRADE"></param>
        /// <param name="P_PIELN"></param>
        /// <param name="P_NETWH"></param>
        /// <param name="P_GRSWH"></param>
        /// <param name="P_GRSLN"></param>
        /// <param name="P_PALSZ"></param>
        /// <param name="P_DTTRA"></param>
        /// <param name="P_DTORA"></param>
        /// <param name="P_AS400NO"></param>
        /// <param name="P_CUSNO"></param>
        /// <param name="P_OPERATORID"></param>
        /// <returns></returns>
        public bool PCKPRFTP_INSERTUPDATE(string P_ANNUL, string P_CDDIV, string P_INVTY, string P_INVNO, string P_CDORD, decimal? P_RELNO, string P_CUSCD, string P_CUSNM, string P_RECTY,
            string P_CDKE1, string P_CDKE2, string P_CSITM, string P_CDCON, string P_CDEL0, string P_GRADE, decimal? P_PIELN, decimal? P_NETWH, decimal? P_GRSWH, decimal? P_GRSLN,
            string P_PALSZ, decimal? P_DTTRA, decimal? P_DTORA, decimal? P_AS400NO, string P_OPERATORID, decimal? P_INUSE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_CDEL0))
                return result;

            if (string.IsNullOrWhiteSpace(P_INVNO))
                return result;

            if (!HasConnection())
                return result;

            PCKPRFTP_INSERTUPDATEParameter dbPara = new PCKPRFTP_INSERTUPDATEParameter();

            dbPara.P_ANNUL = P_ANNUL;
            dbPara.P_CDDIV = P_CDDIV;
            dbPara.P_INVTY = P_INVTY;
            dbPara.P_INVNO = P_INVNO;
            dbPara.P_CDORD = P_CDORD;
            dbPara.P_RELNO = P_RELNO;
            dbPara.P_CUSCD = P_CUSCD;
            dbPara.P_CUSNM = P_CUSNM;
            dbPara.P_RECTY = P_RECTY;
            dbPara.P_CDKE1 = P_CDKE1;
            dbPara.P_CDKE2 = P_CDKE2;
            dbPara.P_CSITM = P_CSITM;
            dbPara.P_CDCON = P_CDCON;
            dbPara.P_CDEL0 = P_CDEL0;
            dbPara.P_GRADE = P_GRADE;
            dbPara.P_PIELN = P_PIELN;
            dbPara.P_NETWH = P_NETWH;
            dbPara.P_GRSWH = P_GRSWH;
            dbPara.P_GRSLN = P_GRSLN;
            dbPara.P_PALSZ = P_PALSZ;
            dbPara.P_DTTRA = P_DTTRA;
            dbPara.P_DTORA = P_DTORA;
            dbPara.P_AS400NO = P_AS400NO;

            if (P_INUSE != null)
                dbPara.P_INUSE = P_INUSE;
            else
                dbPara.P_INUSE = 1;

            dbPara.P_OPERATORID = P_OPERATORID;

            PCKPRFTP_INSERTUPDATEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PCKPRFTP_INSERTUPDATE(dbPara);

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

        #region PCKPRFTP_GETDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invno"></param>
        /// <param name="cdel0"></param>
        /// <returns></returns>
        public List<ListPCKPRFTPData> PCKPRFTP_GETDATA(string invno, string cdel0,decimal? runno)
        {
            List<ListPCKPRFTPData> results = null;

            if (!HasConnection())
                return results;

            PCKPRFTP_GETDATAParameter dbPara = new PCKPRFTP_GETDATAParameter();

            if (!string.IsNullOrEmpty(cdel0))
                dbPara.P_CDEL0 = cdel0;

            if (!string.IsNullOrEmpty(invno))
                dbPara.P_INVNO = invno;

            if(runno != null)
                dbPara.P_RUNNO = runno;

            List<PCKPRFTP_GETDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PCKPRFTP_GETDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListPCKPRFTPData>();
                    foreach (PCKPRFTP_GETDATAResult dbResult in dbResults)
                    {
                        ListPCKPRFTPData inst = new ListPCKPRFTPData();

                        inst.ANNUL = dbResult.ANNUL;
                        inst.CDDIV = dbResult.CDDIV;
                        inst.INVTY = dbResult.INVTY;
                        inst.INVNO = dbResult.INVNO;
                        inst.CDORD = dbResult.CDORD;
                        inst.RELNO = dbResult.RELNO;
                        inst.CUSCD = dbResult.CUSCD;
                        inst.CUSNM = dbResult.CUSNM;
                        inst.RECTY = dbResult.RECTY;
                        inst.CDKE1 = dbResult.CDKE1;
                        inst.CDKE2 = dbResult.CDKE2;
                        inst.CSITM = dbResult.CSITM;
                        inst.CDCON = dbResult.CDCON;
                        inst.CDEL0 = dbResult.CDEL0;
                        inst.GRADE = dbResult.GRADE;
                        inst.PIELN = dbResult.PIELN;
                        inst.NETWH = dbResult.NETWH;
                        inst.GRSWH = dbResult.GRSWH;
                        inst.GRSLN = dbResult.GRSLN;
                        inst.PALSZ = dbResult.PALSZ;
                        inst.DTTRA = dbResult.DTTRA;
                        inst.DTORA = dbResult.DTORA;
                        inst.RUNNO = dbResult.RUNNO;
                        inst.AS400NO = dbResult.AS400NO;
                        inst.CUSNO = dbResult.CUSNO;

                        inst.INSERTBY = dbResult.INSERTBY;
                        inst.INSERTDATE = dbResult.INSERTDATE;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.INUSE = dbResult.INUSE;

                        inst.SCANBY = dbResult.SCANBY;
                        inst.SCANDATE = dbResult.SCANDATE;

                        if (!string.IsNullOrEmpty(inst.SCANBY))
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.PackingList;component/Images/Check.png");
                            BitmapImage source = new BitmapImage(uri);
                            inst.ImgScanFlag = source;
                        }
                    
                        #region CHKNETWEIGHT
                        inst.CHKNETWEIGHT = dbResult.CHKNETWEIGHT;
                        if (inst.CHKNETWEIGHT != null)
                        {
                            if (inst.CHKNETWEIGHT == 1)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkNetWeight = source;
                            }
                            else if (inst.CHKNETWEIGHT == 0)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkNetWeight = source;
                            }
                        }
                        else
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            inst.ImgChkNetWeight = source;
                        }
                        #endregion

                        #region CHKGROSSWEIGHT
                        inst.CHKGROSSWEIGHT = dbResult.CHKGROSSWEIGHT;
                        if (inst.CHKGROSSWEIGHT != null)
                        {
                            if (inst.CHKGROSSWEIGHT == 1)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkGrossWeight = source;
                            }
                            else if (inst.CHKGROSSWEIGHT == 0)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkGrossWeight = source;
                            }
                        }
                        else
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            inst.ImgChkGrossWeight = source;
                        }
                        #endregion

                        #region CHKNETLENGTH

                        inst.CHKNETLENGTH = dbResult.CHKNETLENGTH;
                        if (inst.CHKNETLENGTH != null)
                        {
                            if (inst.CHKNETLENGTH == 1)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkNetLength = source;
                            }
                            else if (inst.CHKNETLENGTH == 0)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkNetLength = source;
                            }
                        }
                        else
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            inst.ImgChkNetLength = source;
                        }
                        #endregion

                        #region CHKGROSSLENGTH

                        inst.CHKGROSSLENGTH = dbResult.CHKGROSSLENGTH;
                        if (inst.CHKGROSSLENGTH != null)
                        {
                            if (inst.CHKGROSSLENGTH == 1)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkGrossLength = source;
                            }
                            else if (inst.CHKGROSSLENGTH == 0)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkGrossLength = source;
                            }
                        }
                        else
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            inst.ImgChkGrossLength = source;
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

        #region PCKPRFTP_SCANBARCODE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cdel0"></param>
        /// <returns></returns>
        public ListPCKPRFTPData PCKPRFTP_SCANBARCODE(string cdel0)
        {
            ListPCKPRFTPData results = null;

            if (!HasConnection())
                return results;

            PCKPRFTP_GETDATAParameter dbPara = new PCKPRFTP_GETDATAParameter();

            if (!string.IsNullOrEmpty(cdel0))
                dbPara.P_CDEL0 = cdel0;

            List<PCKPRFTP_GETDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PCKPRFTP_GETDATA(dbPara);
                if (null != dbResults)
                {
                    results = new ListPCKPRFTPData();

                    results.ANNUL = dbResults[0].ANNUL;
                    results.CDDIV = dbResults[0].CDDIV;
                    results.INVTY = dbResults[0].INVTY;
                    results.INVNO = dbResults[0].INVNO;
                    results.CDORD = dbResults[0].CDORD;
                    results.RELNO = dbResults[0].RELNO;
                    results.CUSCD = dbResults[0].CUSCD;
                    results.CUSNM = dbResults[0].CUSNM;
                    results.RECTY = dbResults[0].RECTY;
                    results.CDKE1 = dbResults[0].CDKE1;
                    results.CDKE2 = dbResults[0].CDKE2;
                    results.CSITM = dbResults[0].CSITM;
                    results.CDCON = dbResults[0].CDCON;
                    results.CDEL0 = dbResults[0].CDEL0;
                    results.GRADE = dbResults[0].GRADE;
                    results.PIELN = dbResults[0].PIELN;
                    results.NETWH = dbResults[0].NETWH;
                    results.GRSWH = dbResults[0].GRSWH;
                    results.GRSLN = dbResults[0].GRSLN;
                    results.PALSZ = dbResults[0].PALSZ;
                    results.DTTRA = dbResults[0].DTTRA;
                    results.DTORA = dbResults[0].DTORA;
                    results.RUNNO = dbResults[0].RUNNO;
                    results.AS400NO = dbResults[0].AS400NO;
                    results.CUSNO = dbResults[0].CUSNO;

                    results.INSERTBY = dbResults[0].INSERTBY;
                    results.INSERTDATE = dbResults[0].INSERTDATE;
                    results.EDITBY = dbResults[0].EDITBY;
                    results.EDITDATE = dbResults[0].EDITDATE;
                    results.INUSE = dbResults[0].INUSE;

                    results.SCANBY = dbResults[0].SCANBY;
                    results.SCANDATE = dbResults[0].SCANDATE;

                    if (!string.IsNullOrEmpty(results.SCANBY))
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.PackingList;component/Images/Check.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgScanFlag = source;
                    }

                    #region CHKNETWEIGHT
                    results.CHKNETWEIGHT = dbResults[0].CHKNETWEIGHT;
                    if (results.CHKNETWEIGHT != null)
                    {
                        if (results.CHKNETWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                        else if (results.CHKNETWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetWeight = source;
                    }
                    #endregion

                    #region CHKGROSSWEIGHT
                    results.CHKGROSSWEIGHT = dbResults[0].CHKGROSSWEIGHT;
                    if (results.CHKGROSSWEIGHT != null)
                    {
                        if (results.CHKGROSSWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                        else if (results.CHKGROSSWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossWeight = source;
                    }
                    #endregion

                    #region CHKNETLENGTH

                    results.CHKNETLENGTH = dbResults[0].CHKNETLENGTH;
                    if (results.CHKNETLENGTH != null)
                    {
                        if (results.CHKNETLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                        else if (results.CHKNETLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetLength = source;
                    }
                    #endregion

                    #region CHKGROSSLENGTH

                    results.CHKGROSSLENGTH = dbResults[0].CHKGROSSLENGTH;
                    if (results.CHKGROSSLENGTH != null)
                    {
                        if (results.CHKGROSSLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                        else if (results.CHKGROSSLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossLength = source;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region PCKPRFTP_SCANBARCODE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invNo"></param>
        /// <param name="cdel0"></param>
        /// <returns></returns>
        public ListPCKPRFTPData PCKPRFTP_SCANBARCODE(string invNo, string cdel0)
        {
            ListPCKPRFTPData results = null;

            if (!HasConnection())
                return results;

            PCKPRFTP_GETDATAParameter dbPara = new PCKPRFTP_GETDATAParameter();

            if (!string.IsNullOrEmpty(invNo))
                dbPara.P_INVNO = invNo;

            if (!string.IsNullOrEmpty(cdel0))
                dbPara.P_CDEL0 = cdel0;

            List<PCKPRFTP_GETDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PCKPRFTP_GETDATA(dbPara);
                if (null != dbResults)
                {
                    results = new ListPCKPRFTPData();

                    results.ANNUL = dbResults[0].ANNUL;
                    results.CDDIV = dbResults[0].CDDIV;
                    results.INVTY = dbResults[0].INVTY;
                    results.INVNO = dbResults[0].INVNO;
                    results.CDORD = dbResults[0].CDORD;
                    results.RELNO = dbResults[0].RELNO;
                    results.CUSCD = dbResults[0].CUSCD;
                    results.CUSNM = dbResults[0].CUSNM;
                    results.RECTY = dbResults[0].RECTY;
                    results.CDKE1 = dbResults[0].CDKE1;
                    results.CDKE2 = dbResults[0].CDKE2;
                    results.CSITM = dbResults[0].CSITM;
                    results.CDCON = dbResults[0].CDCON;
                    results.CDEL0 = dbResults[0].CDEL0;
                    results.GRADE = dbResults[0].GRADE;
                    results.PIELN = dbResults[0].PIELN;
                    results.NETWH = dbResults[0].NETWH;
                    results.GRSWH = dbResults[0].GRSWH;
                    results.GRSLN = dbResults[0].GRSLN;
                    results.PALSZ = dbResults[0].PALSZ;
                    results.DTTRA = dbResults[0].DTTRA;
                    results.DTORA = dbResults[0].DTORA;
                    results.RUNNO = dbResults[0].RUNNO;
                    results.AS400NO = dbResults[0].AS400NO;
                    results.CUSNO = dbResults[0].CUSNO;

                    results.INSERTBY = dbResults[0].INSERTBY;
                    results.INSERTDATE = dbResults[0].INSERTDATE;
                    results.EDITBY = dbResults[0].EDITBY;
                    results.EDITDATE = dbResults[0].EDITDATE;
                    results.INUSE = dbResults[0].INUSE;

                    results.SCANBY = dbResults[0].SCANBY;
                    results.SCANDATE = dbResults[0].SCANDATE;

                    if (!string.IsNullOrEmpty(results.SCANBY))
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.PackingList;component/Images/Check.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgScanFlag = source;
                    }

                    #region CHKNETWEIGHT
                    results.CHKNETWEIGHT = dbResults[0].CHKNETWEIGHT;
                    if (results.CHKNETWEIGHT != null)
                    {
                        if (results.CHKNETWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                        else if (results.CHKNETWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetWeight = source;
                    }
                    #endregion

                    #region CHKGROSSWEIGHT
                    results.CHKGROSSWEIGHT = dbResults[0].CHKGROSSWEIGHT;
                    if (results.CHKGROSSWEIGHT != null)
                    {
                        if (results.CHKGROSSWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                        else if (results.CHKGROSSWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossWeight = source;
                    }
                    #endregion

                    #region CHKNETLENGTH

                    results.CHKNETLENGTH = dbResults[0].CHKNETLENGTH;
                    if (results.CHKNETLENGTH != null)
                    {
                        if (results.CHKNETLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                        else if (results.CHKNETLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetLength = source;
                    }
                    #endregion

                    #region CHKGROSSLENGTH

                    results.CHKGROSSLENGTH = dbResults[0].CHKGROSSLENGTH;
                    if (results.CHKGROSSLENGTH != null)
                    {
                        if (results.CHKGROSSLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                        else if (results.CHKGROSSLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/LuckyTex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossLength = source;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region PCKPRFTP_SCANBARCODE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invNo"></param>
        /// <param name="cdel0"></param>
        /// <param name="runNo"></param>
        /// <returns></returns>
        public ListPCKPRFTPData PCKPRFTP_SCANBARCODE(string invNo, string cdel0,decimal? runNo)
        {
            ListPCKPRFTPData results = null;

            if (!HasConnection())
                return results;

            PCKPRFTP_GETDATAParameter dbPara = new PCKPRFTP_GETDATAParameter();

            if (!string.IsNullOrEmpty(invNo))
                dbPara.P_INVNO = invNo;

            if (!string.IsNullOrEmpty(cdel0))
                dbPara.P_CDEL0 = cdel0;

            if(runNo != null)
                dbPara.P_RUNNO = runNo;

            List<PCKPRFTP_GETDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PCKPRFTP_GETDATA(dbPara);
                if (null != dbResults)
                {
                    results = new ListPCKPRFTPData();

                    results.ANNUL = dbResults[0].ANNUL;
                    results.CDDIV = dbResults[0].CDDIV;
                    results.INVTY = dbResults[0].INVTY;
                    results.INVNO = dbResults[0].INVNO;
                    results.CDORD = dbResults[0].CDORD;
                    results.RELNO = dbResults[0].RELNO;
                    results.CUSCD = dbResults[0].CUSCD;
                    results.CUSNM = dbResults[0].CUSNM;
                    results.RECTY = dbResults[0].RECTY;
                    results.CDKE1 = dbResults[0].CDKE1;
                    results.CDKE2 = dbResults[0].CDKE2;
                    results.CSITM = dbResults[0].CSITM;
                    results.CDCON = dbResults[0].CDCON;
                    results.CDEL0 = dbResults[0].CDEL0;
                    results.GRADE = dbResults[0].GRADE;
                    results.PIELN = dbResults[0].PIELN;
                    results.NETWH = dbResults[0].NETWH;
                    results.GRSWH = dbResults[0].GRSWH;
                    results.GRSLN = dbResults[0].GRSLN;
                    results.PALSZ = dbResults[0].PALSZ;
                    results.DTTRA = dbResults[0].DTTRA;
                    results.DTORA = dbResults[0].DTORA;
                    results.RUNNO = dbResults[0].RUNNO;
                    results.AS400NO = dbResults[0].AS400NO;
                    results.CUSNO = dbResults[0].CUSNO;

                    results.INSERTBY = dbResults[0].INSERTBY;
                    results.INSERTDATE = dbResults[0].INSERTDATE;
                    results.EDITBY = dbResults[0].EDITBY;
                    results.EDITDATE = dbResults[0].EDITDATE;
                    results.INUSE = dbResults[0].INUSE;

                    results.SCANBY = dbResults[0].SCANBY;
                    results.SCANDATE = dbResults[0].SCANDATE;

                    if (!string.IsNullOrEmpty(results.SCANBY))
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.PackingList;component/Images/Check.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgScanFlag = source;
                    }

                    #region CHKNETWEIGHT
                    results.CHKNETWEIGHT = dbResults[0].CHKNETWEIGHT;
                    if (results.CHKNETWEIGHT != null)
                    {
                        if (results.CHKNETWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                        else if (results.CHKNETWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetWeight = source;
                    }
                    #endregion

                    #region CHKGROSSWEIGHT
                    results.CHKGROSSWEIGHT = dbResults[0].CHKGROSSWEIGHT;
                    if (results.CHKGROSSWEIGHT != null)
                    {
                        if (results.CHKGROSSWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                        else if (results.CHKGROSSWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossWeight = source;
                    }
                    #endregion

                    #region CHKNETLENGTH

                    results.CHKNETLENGTH = dbResults[0].CHKNETLENGTH;
                    if (results.CHKNETLENGTH != null)
                    {
                        if (results.CHKNETLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                        else if (results.CHKNETLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetLength = source;
                    }
                    #endregion

                    #region CHKGROSSLENGTH

                    results.CHKGROSSLENGTH = dbResults[0].CHKGROSSLENGTH;
                    if (results.CHKGROSSLENGTH != null)
                    {
                        if (results.CHKGROSSLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                        else if (results.CHKGROSSLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossLength = source;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region PCKPRFTP_GETINVNO

        public List<PackingListINVNOResult> PCKPRFTP_GETINVNO()
        {
            List<PackingListINVNOResult> results = null;

            if (!HasConnection())
                return results;

            PCKPRFTP_GETINVNOParameter dbPara = new PCKPRFTP_GETINVNOParameter();

            List<PCKPRFTP_GETINVNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PCKPRFTP_GETINVNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<PackingListINVNOResult>();
                    foreach (PCKPRFTP_GETINVNOResult dbResult in dbResults)
                    {
                        PackingListINVNOResult inst = new PackingListINVNOResult();

                        inst.INVNO = dbResult.INVNO;

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

        #region D365

        #region PCKPRFTP_UPDATESCAN
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INVNO"></param>
        /// <param name="P_CDEL0"></param>
        /// <param name="P_SCANBY"></param>
        /// <returns></returns>
        public bool PCKPRFTP_D365_UPDATESCAN(string P_INVNO, string P_CDEL0, string P_SCANBY, decimal? P_RUNNO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_INVNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_CDEL0))
                return result;

            if (string.IsNullOrWhiteSpace(P_SCANBY))
                return result;

            //if (P_RUNNO == null)
            //    return result;

            if (!HasConnection())
                return result;

            PCKPRFTP_D365_UPDATESCANParameter dbPara = new PCKPRFTP_D365_UPDATESCANParameter();

            dbPara.P_INVNO = P_INVNO;
            dbPara.P_CDEL0 = P_CDEL0;
            dbPara.P_SCANBY = P_SCANBY;

            if (P_RUNNO != null)
                dbPara.P_RUNNO = P_RUNNO;

            PCKPRFTP_D365_UPDATESCANResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PCKPRFTP_D365_UPDATESCAN(dbPara);

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

        #region PCKPRFTP_D365_UPDATERUNNO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INVNO"></param>
        /// <param name="P_CDEL0"></param>
        /// <param name="P_RUNNO"></param>
        /// <param name="P_EDITBY"></param>
        /// <param name="P_INUSE"></param>
        /// <returns></returns>
        public bool PCKPRFTP_D365_UPDATERUNNO(string P_INVNO, string P_CDEL0, decimal? P_RUNNO, string P_EDITBY, decimal? P_INUSE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_INVNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_CDEL0))
                return result;

            if (!HasConnection())
                return result;

            PCKPRFTP_D365_UPDATERUNNOParameter dbPara = new PCKPRFTP_D365_UPDATERUNNOParameter();

            dbPara.P_INVNO = P_INVNO;
            dbPara.P_CDEL0 = P_CDEL0;
            dbPara.P_RUNNO = P_RUNNO;
            dbPara.P_EDITBY = P_EDITBY;

            if (P_INUSE != null)
                dbPara.P_INUSE = P_INUSE;
            else
                dbPara.P_INUSE = 1;

            PCKPRFTP_D365_UPDATERUNNOResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PCKPRFTP_D365_UPDATERUNNO(dbPara);

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

        #region PCKPRFTP_D365_UPDATEINUSE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INVNO"></param>
        /// <param name="P_CDEL0"></param>
        /// <param name="P_EDITBY"></param>
        /// <param name="P_INUSE"></param>
        /// <returns></returns>
        public bool PCKPRFTP_D365_UPDATEINUSE(string P_INVNO, string P_CDEL0, string P_EDITBY, decimal? P_INUSE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_INVNO))
                return result;

            //if (string.IsNullOrWhiteSpace(P_CDEL0))
            //    return result;

            if (!HasConnection())
                return result;

            PCKPRFTP_D365_UPDATEINUSEParameter dbPara = new PCKPRFTP_D365_UPDATEINUSEParameter();

            dbPara.P_INVNO = P_INVNO;
            dbPara.P_CDEL0 = P_CDEL0;
            dbPara.P_EDITBY = P_EDITBY;

            if (P_INUSE != null)
                dbPara.P_INUSE = P_INUSE;
            else
                dbPara.P_INUSE = 1;

            PCKPRFTP_D365_UPDATEINUSEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PCKPRFTP_D365_UPDATEINUSE(dbPara);

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

        #region PCKPRFTP_D365_UPDATECUSNO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_CDEL0"></param>
        /// <param name="P_CUSNO"></param>
        /// <param name="P_EDITBY"></param>
        /// <param name="P_INUSE"></param>
        /// <returns></returns>
        public bool PCKPRFTP_D365_UPDATECUSNO(string P_CDEL0, decimal? P_CUSNO, string P_EDITBY, decimal? P_INUSE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_CDEL0))
                return result;

            if (!HasConnection())
                return result;

            PCKPRFTP_D365_UPDATECUSNOParameter dbPara = new PCKPRFTP_D365_UPDATECUSNOParameter();

            dbPara.P_CDEL0 = P_CDEL0;
            dbPara.P_CUSNO = P_CUSNO;
            dbPara.P_EDITBY = P_EDITBY;

            if (P_INUSE != null)
                dbPara.P_INUSE = P_INUSE;
            else
                dbPara.P_INUSE = 0;

            PCKPRFTP_D365_UPDATECUSNOResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PCKPRFTP_D365_UPDATECUSNO(dbPara);

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

        #region PCKPRFTP_D365_INSERTUPDATE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ANNUL"></param>
        /// <param name="P_CDDIV"></param>
        /// <param name="P_INVTY"></param>
        /// <param name="P_INVNO"></param>
        /// <param name="P_CDORD"></param>
        /// <param name="P_RELNO"></param>
        /// <param name="P_CUSCD"></param>
        /// <param name="P_CUSNM"></param>
        /// <param name="P_RECTY"></param>
        /// <param name="P_CDKE1"></param>
        /// <param name="P_CDKE2"></param>
        /// <param name="P_CSITM"></param>
        /// <param name="P_CDCON"></param>
        /// <param name="P_CDEL0"></param>
        /// <param name="P_GRADE"></param>
        /// <param name="P_PIELN"></param>
        /// <param name="P_NETWH"></param>
        /// <param name="P_GRSWH"></param>
        /// <param name="P_GRSLN"></param>
        /// <param name="P_PALSZ"></param>
        /// <param name="P_DTTRA"></param>
        /// <param name="P_DTORA"></param>
        /// <param name="P_AS400NO"></param>
        /// <param name="P_CUSNO"></param>
        /// <param name="P_OPERATORID"></param>
        /// <returns></returns>
        public bool PCKPRFTP_D365_INSERTUPDATE(string P_ANNUL, string P_CDDIV, string P_INVTY, string P_INVNO, string P_CDORD, decimal? P_RELNO, string P_CUSCD, string P_CUSNM, string P_RECTY,
            string P_CDKE1, string P_CDKE2, string P_CSITM, string P_CDCON, string P_CDEL0, string P_GRADE, decimal? P_PIELN, decimal? P_NETWH, decimal? P_GRSWH, decimal? P_GRSLN,
            string P_PALSZ, decimal? P_DTTRA, decimal? P_DTORA, decimal? P_AS400NO, string P_OPERATORID, decimal? P_INUSE)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_CDEL0))
                return result;

            if (string.IsNullOrWhiteSpace(P_INVNO))
                return result;

            if (!HasConnection())
                return result;

            PCKPRFTP_D365_INSERTUPDATEParameter dbPara = new PCKPRFTP_D365_INSERTUPDATEParameter();

            dbPara.P_ANNUL = P_ANNUL;
            dbPara.P_CDDIV = P_CDDIV;
            dbPara.P_INVTY = P_INVTY;
            dbPara.P_INVNO = P_INVNO;
            dbPara.P_CDORD = P_CDORD;
            dbPara.P_RELNO = P_RELNO;
            dbPara.P_CUSCD = P_CUSCD;
            dbPara.P_CUSNM = P_CUSNM;
            dbPara.P_RECTY = P_RECTY;
            dbPara.P_CDKE1 = P_CDKE1;
            dbPara.P_CDKE2 = P_CDKE2;
            dbPara.P_CSITM = P_CSITM;
            dbPara.P_CDCON = P_CDCON;
            dbPara.P_CDEL0 = P_CDEL0;
            dbPara.P_GRADE = P_GRADE;
            dbPara.P_PIELN = P_PIELN;
            dbPara.P_NETWH = P_NETWH;
            dbPara.P_GRSWH = P_GRSWH;
            dbPara.P_GRSLN = P_GRSLN;
            dbPara.P_PALSZ = P_PALSZ;
            dbPara.P_DTTRA = P_DTTRA;
            dbPara.P_DTORA = P_DTORA;
            dbPara.P_AS400NO = P_AS400NO;

            if (P_INUSE != null)
                dbPara.P_INUSE = P_INUSE;
            else
                dbPara.P_INUSE = 1;

            dbPara.P_OPERATORID = P_OPERATORID;

            PCKPRFTP_D365_INSERTUPDATEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.PCKPRFTP_D365_INSERTUPDATE(dbPara);

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

        #region PCKPRFTP_D365_GETDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invno"></param>
        /// <param name="cdel0"></param>
        /// <returns></returns>
        public List<ListPCKPRFTPData> PCKPRFTP_D365_GETDATA(string invno, string cdel0, decimal? runno)
        {
            List<ListPCKPRFTPData> results = null;

            if (!HasConnection())
                return results;

            PCKPRFTP_D365_GETDATAParameter dbPara = new PCKPRFTP_D365_GETDATAParameter();

            if (!string.IsNullOrEmpty(cdel0))
                dbPara.P_CDEL0 = cdel0;

            if (!string.IsNullOrEmpty(invno))
                dbPara.P_INVNO = invno;

            if (runno != null)
                dbPara.P_RUNNO = runno;

            List<PCKPRFTP_D365_GETDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PCKPRFTP_D365_GETDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<ListPCKPRFTPData>();
                    foreach (PCKPRFTP_D365_GETDATAResult dbResult in dbResults)
                    {
                        ListPCKPRFTPData inst = new ListPCKPRFTPData();

                        inst.ANNUL = dbResult.ANNUL;
                        inst.CDDIV = dbResult.CDDIV;
                        inst.INVTY = dbResult.INVTY;
                        inst.INVNO = dbResult.INVNO;
                        inst.CDORD = dbResult.CDORD;
                        inst.RELNO = dbResult.RELNO;
                        inst.CUSCD = dbResult.CUSCD;
                        inst.CUSNM = dbResult.CUSNM;
                        inst.RECTY = dbResult.RECTY;
                        inst.CDKE1 = dbResult.CDKE1;
                        inst.CDKE2 = dbResult.CDKE2;
                        inst.CSITM = dbResult.CSITM;
                        inst.CDCON = dbResult.CDCON;
                        inst.CDEL0 = dbResult.CDEL0;
                        inst.GRADE = dbResult.GRADE;
                        inst.PIELN = dbResult.PIELN;
                        inst.NETWH = dbResult.NETWH;
                        inst.GRSWH = dbResult.GRSWH;
                        inst.GRSLN = dbResult.GRSLN;
                        inst.PALSZ = dbResult.PALSZ;
                        inst.DTTRA = dbResult.DTTRA;
                        inst.DTORA = dbResult.DTORA;
                        inst.RUNNO = dbResult.RUNNO;
                        inst.AS400NO = dbResult.AS400NO;
                        inst.CUSNO = dbResult.CUSNO;

                        inst.INSERTBY = dbResult.INSERTBY;
                        inst.INSERTDATE = dbResult.INSERTDATE;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.INUSE = dbResult.INUSE;

                        inst.SCANBY = dbResult.SCANBY;
                        inst.SCANDATE = dbResult.SCANDATE;

                        if (!string.IsNullOrEmpty(inst.SCANBY))
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.PackingList;component/Images/Check.png");
                            BitmapImage source = new BitmapImage(uri);
                            inst.ImgScanFlag = source;
                        }

                        #region CHKNETWEIGHT
                        inst.CHKNETWEIGHT = dbResult.CHKNETWEIGHT;
                        if (inst.CHKNETWEIGHT != null)
                        {
                            if (inst.CHKNETWEIGHT == 1)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkNetWeight = source;
                            }
                            else if (inst.CHKNETWEIGHT == 0)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkNetWeight = source;
                            }
                        }
                        else
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            inst.ImgChkNetWeight = source;
                        }
                        #endregion

                        #region CHKGROSSWEIGHT
                        inst.CHKGROSSWEIGHT = dbResult.CHKGROSSWEIGHT;
                        if (inst.CHKGROSSWEIGHT != null)
                        {
                            if (inst.CHKGROSSWEIGHT == 1)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkGrossWeight = source;
                            }
                            else if (inst.CHKGROSSWEIGHT == 0)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkGrossWeight = source;
                            }
                        }
                        else
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            inst.ImgChkGrossWeight = source;
                        }
                        #endregion

                        #region CHKNETLENGTH

                        inst.CHKNETLENGTH = dbResult.CHKNETLENGTH;
                        if (inst.CHKNETLENGTH != null)
                        {
                            if (inst.CHKNETLENGTH == 1)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkNetLength = source;
                            }
                            else if (inst.CHKNETLENGTH == 0)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkNetLength = source;
                            }
                        }
                        else
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            inst.ImgChkNetLength = source;
                        }
                        #endregion

                        #region CHKGROSSLENGTH

                        inst.CHKGROSSLENGTH = dbResult.CHKGROSSLENGTH;
                        if (inst.CHKGROSSLENGTH != null)
                        {
                            if (inst.CHKGROSSLENGTH == 1)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkGrossLength = source;
                            }
                            else if (inst.CHKGROSSLENGTH == 0)
                            {
                                Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                                BitmapImage source = new BitmapImage(uri);
                                inst.ImgChkGrossLength = source;
                            }
                        }
                        else
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            inst.ImgChkGrossLength = source;
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

        #region PCKPRFTP_D365_SCANBARCODE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cdel0"></param>
        /// <returns></returns>
        public ListPCKPRFTPData PCKPRFTP_D365_SCANBARCODE(string cdel0)
        {
            ListPCKPRFTPData results = null;

            if (!HasConnection())
                return results;

            PCKPRFTP_D365_GETDATAParameter dbPara = new PCKPRFTP_D365_GETDATAParameter();

            if (!string.IsNullOrEmpty(cdel0))
                dbPara.P_CDEL0 = cdel0;

            List<PCKPRFTP_D365_GETDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PCKPRFTP_D365_GETDATA(dbPara);
                if (null != dbResults)
                {
                    results = new ListPCKPRFTPData();

                    results.ANNUL = dbResults[0].ANNUL;
                    results.CDDIV = dbResults[0].CDDIV;
                    results.INVTY = dbResults[0].INVTY;
                    results.INVNO = dbResults[0].INVNO;
                    results.CDORD = dbResults[0].CDORD;
                    results.RELNO = dbResults[0].RELNO;
                    results.CUSCD = dbResults[0].CUSCD;
                    results.CUSNM = dbResults[0].CUSNM;
                    results.RECTY = dbResults[0].RECTY;
                    results.CDKE1 = dbResults[0].CDKE1;
                    results.CDKE2 = dbResults[0].CDKE2;
                    results.CSITM = dbResults[0].CSITM;
                    results.CDCON = dbResults[0].CDCON;
                    results.CDEL0 = dbResults[0].CDEL0;
                    results.GRADE = dbResults[0].GRADE;
                    results.PIELN = dbResults[0].PIELN;
                    results.NETWH = dbResults[0].NETWH;
                    results.GRSWH = dbResults[0].GRSWH;
                    results.GRSLN = dbResults[0].GRSLN;
                    results.PALSZ = dbResults[0].PALSZ;
                    results.DTTRA = dbResults[0].DTTRA;
                    results.DTORA = dbResults[0].DTORA;
                    results.RUNNO = dbResults[0].RUNNO;
                    results.AS400NO = dbResults[0].AS400NO;
                    results.CUSNO = dbResults[0].CUSNO;

                    results.INSERTBY = dbResults[0].INSERTBY;
                    results.INSERTDATE = dbResults[0].INSERTDATE;
                    results.EDITBY = dbResults[0].EDITBY;
                    results.EDITDATE = dbResults[0].EDITDATE;
                    results.INUSE = dbResults[0].INUSE;

                    results.SCANBY = dbResults[0].SCANBY;
                    results.SCANDATE = dbResults[0].SCANDATE;

                    if (!string.IsNullOrEmpty(results.SCANBY))
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.PackingList;component/Images/Check.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgScanFlag = source;
                    }

                    #region CHKNETWEIGHT
                    results.CHKNETWEIGHT = dbResults[0].CHKNETWEIGHT;
                    if (results.CHKNETWEIGHT != null)
                    {
                        if (results.CHKNETWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                        else if (results.CHKNETWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetWeight = source;
                    }
                    #endregion

                    #region CHKGROSSWEIGHT
                    results.CHKGROSSWEIGHT = dbResults[0].CHKGROSSWEIGHT;
                    if (results.CHKGROSSWEIGHT != null)
                    {
                        if (results.CHKGROSSWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                        else if (results.CHKGROSSWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossWeight = source;
                    }
                    #endregion

                    #region CHKNETLENGTH

                    results.CHKNETLENGTH = dbResults[0].CHKNETLENGTH;
                    if (results.CHKNETLENGTH != null)
                    {
                        if (results.CHKNETLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                        else if (results.CHKNETLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetLength = source;
                    }
                    #endregion

                    #region CHKGROSSLENGTH

                    results.CHKGROSSLENGTH = dbResults[0].CHKGROSSLENGTH;
                    if (results.CHKGROSSLENGTH != null)
                    {
                        if (results.CHKGROSSLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                        else if (results.CHKGROSSLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossLength = source;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region PCKPRFTP_D365_SCANBARCODE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invNo"></param>
        /// <param name="cdel0"></param>
        /// <returns></returns>
        public ListPCKPRFTPData PCKPRFTP_D365_SCANBARCODE(string invNo, string cdel0)
        {
            ListPCKPRFTPData results = null;

            if (!HasConnection())
                return results;

            PCKPRFTP_D365_GETDATAParameter dbPara = new PCKPRFTP_D365_GETDATAParameter();

            if (!string.IsNullOrEmpty(invNo))
                dbPara.P_INVNO = invNo;

            if (!string.IsNullOrEmpty(cdel0))
                dbPara.P_CDEL0 = cdel0;

            List<PCKPRFTP_D365_GETDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PCKPRFTP_D365_GETDATA(dbPara);
                if (null != dbResults)
                {
                    results = new ListPCKPRFTPData();

                    results.ANNUL = dbResults[0].ANNUL;
                    results.CDDIV = dbResults[0].CDDIV;
                    results.INVTY = dbResults[0].INVTY;
                    results.INVNO = dbResults[0].INVNO;
                    results.CDORD = dbResults[0].CDORD;
                    results.RELNO = dbResults[0].RELNO;
                    results.CUSCD = dbResults[0].CUSCD;
                    results.CUSNM = dbResults[0].CUSNM;
                    results.RECTY = dbResults[0].RECTY;
                    results.CDKE1 = dbResults[0].CDKE1;
                    results.CDKE2 = dbResults[0].CDKE2;
                    results.CSITM = dbResults[0].CSITM;
                    results.CDCON = dbResults[0].CDCON;
                    results.CDEL0 = dbResults[0].CDEL0;
                    results.GRADE = dbResults[0].GRADE;
                    results.PIELN = dbResults[0].PIELN;
                    results.NETWH = dbResults[0].NETWH;
                    results.GRSWH = dbResults[0].GRSWH;
                    results.GRSLN = dbResults[0].GRSLN;
                    results.PALSZ = dbResults[0].PALSZ;
                    results.DTTRA = dbResults[0].DTTRA;
                    results.DTORA = dbResults[0].DTORA;
                    results.RUNNO = dbResults[0].RUNNO;
                    results.AS400NO = dbResults[0].AS400NO;
                    results.CUSNO = dbResults[0].CUSNO;

                    results.INSERTBY = dbResults[0].INSERTBY;
                    results.INSERTDATE = dbResults[0].INSERTDATE;
                    results.EDITBY = dbResults[0].EDITBY;
                    results.EDITDATE = dbResults[0].EDITDATE;
                    results.INUSE = dbResults[0].INUSE;

                    results.SCANBY = dbResults[0].SCANBY;
                    results.SCANDATE = dbResults[0].SCANDATE;

                    if (!string.IsNullOrEmpty(results.SCANBY))
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.PackingList;component/Images/Check.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgScanFlag = source;
                    }

                    #region CHKNETWEIGHT
                    results.CHKNETWEIGHT = dbResults[0].CHKNETWEIGHT;
                    if (results.CHKNETWEIGHT != null)
                    {
                        if (results.CHKNETWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                        else if (results.CHKNETWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetWeight = source;
                    }
                    #endregion

                    #region CHKGROSSWEIGHT
                    results.CHKGROSSWEIGHT = dbResults[0].CHKGROSSWEIGHT;
                    if (results.CHKGROSSWEIGHT != null)
                    {
                        if (results.CHKGROSSWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                        else if (results.CHKGROSSWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossWeight = source;
                    }
                    #endregion

                    #region CHKNETLENGTH

                    results.CHKNETLENGTH = dbResults[0].CHKNETLENGTH;
                    if (results.CHKNETLENGTH != null)
                    {
                        if (results.CHKNETLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                        else if (results.CHKNETLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetLength = source;
                    }
                    #endregion

                    #region CHKGROSSLENGTH

                    results.CHKGROSSLENGTH = dbResults[0].CHKGROSSLENGTH;
                    if (results.CHKGROSSLENGTH != null)
                    {
                        if (results.CHKGROSSLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                        else if (results.CHKGROSSLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/LuckyTex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossLength = source;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region PCKPRFTP_D365_SCANBARCODE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invNo"></param>
        /// <param name="cdel0"></param>
        /// <param name="runNo"></param>
        /// <returns></returns>
        public ListPCKPRFTPData PCKPRFTP_D365_SCANBARCODE(string invNo, string cdel0, decimal? runNo)
        {
            ListPCKPRFTPData results = null;

            if (!HasConnection())
                return results;

            PCKPRFTP_D365_GETDATAParameter dbPara = new PCKPRFTP_D365_GETDATAParameter();

            if (!string.IsNullOrEmpty(invNo))
                dbPara.P_INVNO = invNo;

            if (!string.IsNullOrEmpty(cdel0))
                dbPara.P_CDEL0 = cdel0;

            if (runNo != null)
                dbPara.P_RUNNO = runNo;

            List<PCKPRFTP_D365_GETDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PCKPRFTP_D365_GETDATA(dbPara);
                if (null != dbResults)
                {
                    results = new ListPCKPRFTPData();

                    results.ANNUL = dbResults[0].ANNUL;
                    results.CDDIV = dbResults[0].CDDIV;
                    results.INVTY = dbResults[0].INVTY;
                    results.INVNO = dbResults[0].INVNO;
                    results.CDORD = dbResults[0].CDORD;
                    results.RELNO = dbResults[0].RELNO;
                    results.CUSCD = dbResults[0].CUSCD;
                    results.CUSNM = dbResults[0].CUSNM;
                    results.RECTY = dbResults[0].RECTY;
                    results.CDKE1 = dbResults[0].CDKE1;
                    results.CDKE2 = dbResults[0].CDKE2;
                    results.CSITM = dbResults[0].CSITM;
                    results.CDCON = dbResults[0].CDCON;
                    results.CDEL0 = dbResults[0].CDEL0;
                    results.GRADE = dbResults[0].GRADE;
                    results.PIELN = dbResults[0].PIELN;
                    results.NETWH = dbResults[0].NETWH;
                    results.GRSWH = dbResults[0].GRSWH;
                    results.GRSLN = dbResults[0].GRSLN;
                    results.PALSZ = dbResults[0].PALSZ;
                    results.DTTRA = dbResults[0].DTTRA;
                    results.DTORA = dbResults[0].DTORA;
                    results.RUNNO = dbResults[0].RUNNO;
                    results.AS400NO = dbResults[0].AS400NO;
                    results.CUSNO = dbResults[0].CUSNO;

                    results.INSERTBY = dbResults[0].INSERTBY;
                    results.INSERTDATE = dbResults[0].INSERTDATE;
                    results.EDITBY = dbResults[0].EDITBY;
                    results.EDITDATE = dbResults[0].EDITDATE;
                    results.INUSE = dbResults[0].INUSE;

                    results.SCANBY = dbResults[0].SCANBY;
                    results.SCANDATE = dbResults[0].SCANDATE;

                    if (!string.IsNullOrEmpty(results.SCANBY))
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.PackingList;component/Images/Check.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgScanFlag = source;
                    }

                    #region CHKNETWEIGHT
                    results.CHKNETWEIGHT = dbResults[0].CHKNETWEIGHT;
                    if (results.CHKNETWEIGHT != null)
                    {
                        if (results.CHKNETWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                        else if (results.CHKNETWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetWeight = source;
                    }
                    #endregion

                    #region CHKGROSSWEIGHT
                    results.CHKGROSSWEIGHT = dbResults[0].CHKGROSSWEIGHT;
                    if (results.CHKGROSSWEIGHT != null)
                    {
                        if (results.CHKGROSSWEIGHT == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                        else if (results.CHKGROSSWEIGHT == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossWeight = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossWeight = source;
                    }
                    #endregion

                    #region CHKNETLENGTH

                    results.CHKNETLENGTH = dbResults[0].CHKNETLENGTH;
                    if (results.CHKNETLENGTH != null)
                    {
                        if (results.CHKNETLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                        else if (results.CHKNETLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkNetLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkNetLength = source;
                    }
                    #endregion

                    #region CHKGROSSLENGTH

                    results.CHKGROSSLENGTH = dbResults[0].CHKGROSSLENGTH;
                    if (results.CHKGROSSLENGTH != null)
                    {
                        if (results.CHKGROSSLENGTH == 1)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/true.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                        else if (results.CHKGROSSLENGTH == 0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                            BitmapImage source = new BitmapImage(uri);
                            results.ImgChkGrossLength = source;
                        }
                    }
                    else
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.AirBag.Pages;component/Images/false.png");
                        BitmapImage source = new BitmapImage(uri);
                        results.ImgChkGrossLength = source;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region PCKPRFTP_D365_GETINVNO

        public List<PackingListINVNOResult> PCKPRFTP_D365_GETINVNO()
        {
            List<PackingListINVNOResult> results = null;

            if (!HasConnection())
                return results;

            PCKPRFTP_D365_GETINVNOParameter dbPara = new PCKPRFTP_D365_GETINVNOParameter();

            List<PCKPRFTP_D365_GETINVNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.PCKPRFTP_D365_GETINVNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<PackingListINVNOResult>();
                    foreach (PCKPRFTP_D365_GETINVNOResult dbResult in dbResults)
                    {
                        PackingListINVNOResult inst = new PackingListINVNOResult();

                        inst.INVNO = dbResult.INVNO;

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







