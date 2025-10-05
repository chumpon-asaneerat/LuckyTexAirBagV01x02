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

using NLib.Components;

#endregion

namespace LuckyTex.Services
{
    #region BCSPRFTP Data Service

    /// <summary>
    /// The data service for BCSPRFTP.
    /// </summary>
    public class BCSPRFTPDataService : BaseDataService
    {
        #region Singelton

        private static BCSPRFTPDataService _instance = null;

        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static BCSPRFTPDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(BCSPRFTPDataService))
                    {
                        _instance = new BCSPRFTPDataService();
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
        private BCSPRFTPDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~BCSPRFTPDataService()
        {
        }

        #endregion

        #region Public Methods AS400

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

        #region เพิ่มใหม่ FG_SEARCHDATASEND400 ใช้ในการ Load AS400
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_STARTDATE"></param>
        /// <param name="P_ENDDATE"></param>
        /// <param name="P_STOCK"></param>
        /// <returns></returns>
        public List<FG_SEARCHDATASEND400> FG_SEARCHDATASEND400(string P_STARTDATE, string P_ENDDATE, string P_STOCK, string P_PALLETNO)
        {
            List<FG_SEARCHDATASEND400> results = null;

            if (!HasConnection())
                return results;

            FG_SEARCHDATASEND400Parameter dbPara = new FG_SEARCHDATASEND400Parameter();
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_ENDDATE = P_ENDDATE;
            dbPara.P_STOCK = P_STOCK;
            dbPara.P_PALLETNO = P_PALLETNO;

            List<FG_SEARCHDATASEND400Result> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FG_SEARCHDATASEND400(dbPara);
                if (null != dbResults)
                {
                    results = new List<FG_SEARCHDATASEND400>();
                    foreach (FG_SEARCHDATASEND400Result dbResult in dbResults)
                    {
                        FG_SEARCHDATASEND400 inst = new FG_SEARCHDATASEND400();

                        inst.SelectData = false;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.GRADE = dbResult.GRADE;
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;
                        inst.ISLAB = dbResult.ISLAB;
                        inst.INSPECTIONDATE = dbResult.INSPECTIONDATE;
                        inst.FLAG = dbResult.FLAG;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;
                        inst.STOCK = dbResult.STOCK;

                        // เพิ่มใหม่ 10/03/16
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.ORDERNO = dbResult.ORDERNO;
                        //-------------------------------------//

                        inst.RETEST = dbResult.RETEST;
                        inst.PRODUCTTYPE = dbResult.PRODUCTTYPE;
                        inst.ROLLNO = dbResult.ROLLNO;
                        inst.CUSTOMERITEM = dbResult.CUSTOMERITEM;

                        // เพิ่มใหม่ 11/03/16
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        //-------------------------------------//

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

        #region FG_UPDATEDATASEND400
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_INSPECTLOT"></param>
        /// <param name="P_INSPECTIONDATE"></param>
        /// <param name="P_PALLETNO"></param>
        /// <returns></returns>
        public bool FG_UPDATEDATASEND400(string P_INSPECTLOT, DateTime? P_INSPECTIONDATE, string P_PALLETNO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_INSPECTLOT))
                return result;

            if (string.IsNullOrWhiteSpace(P_PALLETNO))
                return result;

            if (!HasConnection())
                return result;

            FG_UPDATEDATASEND400Parameter dbPara = new FG_UPDATEDATASEND400Parameter();
            dbPara.P_INSPECTLOT = P_INSPECTLOT;
            dbPara.P_INSPECTIONDATE = P_INSPECTIONDATE;
            dbPara.P_PALLETNO = P_PALLETNO;

            FG_UPDATEDATASEND400Result dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.FG_UPDATEDATASEND400(dbPara);

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

        #region GetBCSPRFTP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConStr"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<BCSPRFTPResult> GetBCSPRFTP(string ConStr,string where)
        {
            List<BCSPRFTPResult> results = null;

            if (string.IsNullOrWhiteSpace(ConStr))
                return results;

            try
            {
                string query = string.Empty;
                StringBuilder q = new StringBuilder();
                //q.Append("Select * From BCSPRFTP ");

                q.Append("Select #ANNUL AS ANNUL ,#FLAGS AS FLAGS ,#RECTY AS RECTY , #CDSTO AS CDSTO "
                + " , #USRNM AS USRNM , #DTTRA AS DTTRA , #DTINP AS DTINP ,#CDEL0 AS CDEL0 "
                + " , #CDCON AS CDCON , #BLELE AS BLELE , #CDUM0 AS CDUM0 , #CDKE1 AS CDKE1 "
                + " , #CDKE2 AS CDKE2 , #CDKE3 AS CDKE3 , #CDKE4 AS CDKE4 , #CDKE5 AS CDKE5 "
                + " , #CDLOT AS CDLOT , #CDTRA AS CDTRA , #REFER AS REFER , #LOCAT AS LOCAT "
                + " , #CDQUA AS CDQUA , #QUACA AS QUACA , #TECU1 AS TECU1 , #TECU2 AS TECU2 "
                + " , #TECU3 AS TECU3 , #TECU4 AS TECU4 , #TECU5 AS TECU5 , #TECU6 AS TECU6 "
                + " , #COMM0 AS COMM0 , #DTORA AS DTORA "
                + " From BCSPRFTP ");

                q.Append(where);

                query = q.ToString();


                DataSet ds = new DataSet();

                OleDbConnection connection;

                connection = new OleDbConnection(ConStr);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                OleDbDataAdapter oledbAdapter;
                oledbAdapter = new OleDbDataAdapter(query, connection);

                oledbAdapter.Fill(ds, "BCSPRFTP");

                if (ds.Tables["BCSPRFTP"].Rows.Count > 0)
                {
                    results = new List<BCSPRFTPResult>();
                    BCSPRFTPResult inst = null;


                    foreach (DataRow row in ds.Tables["BCSPRFTP"].Rows)
                    {
                        inst = new BCSPRFTPResult();

                        inst.ANNUL = row["ANNUL"].ToString();
                        inst.FLAGS = row["FLAGS"].ToString();
                        inst.RECTY = row["RECTY"].ToString();
                        inst.CDSTO = row["CDSTO"].ToString();
                        inst.USRNM = row["USRNM"].ToString();

                        if (row["DTTRA"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["DTTRA"].ToString()))
                            {
                                inst.DTTRA = int.Parse(row["DTTRA"].ToString());
                            }
                        }

                        if (row["DTINP"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["DTINP"].ToString()))
                            {
                                inst.DTINP = int.Parse(row["DTINP"].ToString());
                            }
                        }

                        inst.CDEL0 = row["CDEL0"].ToString();
                        inst.CDCON = row["CDCON"].ToString();

                        if (row["BLELE"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["BLELE"].ToString()))
                            {
                                inst.BLELE = decimal.Parse(row["BLELE"].ToString());
                            }
                        }

                        inst.CDUM0 = row["CDUM0"].ToString();
                        inst.CDKE1 = row["CDKE1"].ToString();
                        inst.CDKE2 = row["CDKE2"].ToString();
                        inst.CDKE3 = row["CDKE3"].ToString();
                        inst.CDKE4 = row["CDKE4"].ToString();
                        inst.CDKE5 = row["CDKE5"].ToString();
                        inst.CDLOT = row["CDLOT"].ToString();
                        inst.CDTRA = row["CDTRA"].ToString();
                        inst.REFER = row["REFER"].ToString();
                        inst.LOCAT = row["LOCAT"].ToString();
                        inst.CDQUA = row["CDQUA"].ToString();
                        inst.QUACA = row["QUACA"].ToString();

                        if (row["TECU1"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["TECU1"].ToString()))
                            {
                                inst.TECU1 = decimal.Parse(row["TECU1"].ToString());
                            }
                        }

                        if (row["TECU2"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["TECU2"].ToString()))
                            {
                                inst.TECU2 = decimal.Parse(row["TECU2"].ToString());
                            }
                        }

                        if (row["TECU3"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["TECU3"].ToString()))
                            {
                                inst.TECU3 = decimal.Parse(row["TECU3"].ToString());
                            }
                        }

                        if (row["TECU4"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["TECU4"].ToString()))
                            {
                                inst.TECU4 = decimal.Parse(row["TECU4"].ToString());
                            }
                        }

                        if (row["TECU5"] != null)
                            inst.TECU5 = row["TECU5"].ToString();

                        if (row["TECU6"] != null)
                            inst.TECU6 = row["TECU6"].ToString();

                        inst.COMM0 = row["COMM0"].ToString();

                        if (row["DTORA"] != null)
                        {
                            if (!string.IsNullOrEmpty(row["DTORA"].ToString()))
                            {
                                inst.DTORA = Int64.Parse(row["DTORA"].ToString());
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

        #region InsertDataBCSPRFTP

        public bool InsertDataBCSPRFTP(string ConStr,
            string ANNUL, string FLAGS, string RECTY, string CDSTO, string USRNM, int? DTTRA, int? DTINP,
        string CDEL0, string CDCON, decimal? BLELE, string CDUM0, string CDKE1, string CDKE2, string CDKE3, string CDKE4, string CDKE5, string CDLOT, string CDTRA,
        string REFER, string LOCAT, string CDQUA, string QUACA, decimal? TECU1, decimal? TECU2, decimal? TECU3, decimal? TECU4, string TECU5, string TECU6, string COMM0,
        Int64? DTORA)
        {

            string queryString = string.Empty;

            queryString = " INSERT INTO BCSPRFTP( #ANNUL, #FLAGS, #RECTY, #CDSTO, #USRNM "
                            + " , #DTTRA  , #DTINP"
                            + " , #CDEL0, #CDCON "
                            + " , #BLELE "
                            + " , #CDUM0 , #CDKE1 , #CDKE2, #CDKE3, #CDKE4, #CDKE5 "
                            + " , #CDLOT, #CDTRA, #REFER , #LOCAT, #CDQUA, #QUACA "
                            + " , #TECU1, #TECU2 , #TECU3, #TECU4 "
                            + " , #TECU5 , #TECU6, #COMM0 "
                            + " , #DTORA ) "
                            + " VALUES( ? , ? , ? , ? , ? "
                            + " , ? , ? "
                            + " , ? , ? "
                            + " , ? "
                            + " , ? , ? , ? , ? , ? , ? "
                            + " , ? , ? , ? , ? , ? , ? "
                            + " , ? , ? , ? , ? "
                            + " , ? , ?, ? "
                            + " , ? ) ";

            //queryString = " INSERT INTO BCSPRFTP( ANNUL, FLAGS, RECTY, CDSTO, USRNM "
            //               + " , DTTRA  , DTINP"
            //               + " , CDEL0, CDCON "
            //               + " , BLELE "
            //               + " , CDUM0 , CDKE1 , CDKE2, CDKE3, CDKE4, CDKE5 "
            //               + " , CDLOT, CDTRA, REFER , LOCAT, CDQUA, QUACA "
            //               + " , TECU1, TECU2 , TECU3, TECU4 "
            //               + " , TECU5 , TECU6, COMM0 "
            //               + " , DTORA ) "
            //               + " VALUES( ? , ? , ? , ? , ? "
            //               + " , ? , ? "
            //               + " , ? , ? "
            //               + " , ? "
            //               + " , ? , ? , ? , ? , ? , ? "
            //               + " , ? , ? , ? , ? , ? , ? "
            //               + " , ? , ? , ? , ? "
            //               + " , ? , ?, ? "
            //               + " , ? ) ";

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
                        cmd.Parameters.AddWithValue("@#FLAGS", OleDbType.Char).Value = FLAGS;
                        cmd.Parameters.AddWithValue("@#RECTY", OleDbType.Char).Value = RECTY;
                        cmd.Parameters.AddWithValue("@#CDSTO", OleDbType.Char).Value = CDSTO;
                        cmd.Parameters.AddWithValue("@#USRNM", OleDbType.Char).Value = USRNM;
                        cmd.Parameters.AddWithValue("@#DTTRA", OleDbType.Numeric).Value = DTTRA;
                        cmd.Parameters.AddWithValue("@#DTINP", OleDbType.Numeric).Value = DTINP;
                        cmd.Parameters.AddWithValue("@#CDEL0", OleDbType.Char).Value = CDEL0;
                        cmd.Parameters.AddWithValue("@#CDCON", OleDbType.Char).Value = CDCON;
                        cmd.Parameters.AddWithValue("@#BLELE", OleDbType.Numeric).Value = BLELE;
                        cmd.Parameters.AddWithValue("@#CDUM0", OleDbType.Char).Value = CDUM0;
                        cmd.Parameters.AddWithValue("@#CDKE1", OleDbType.Char).Value = CDKE1;
                        cmd.Parameters.AddWithValue("@#CDKE2", OleDbType.Char).Value = CDKE2;
                        cmd.Parameters.AddWithValue("@#CDKE3", OleDbType.Char).Value = CDKE3;
                        cmd.Parameters.AddWithValue("@#CDKE4", OleDbType.Char).Value = CDKE4;
                        cmd.Parameters.AddWithValue("@#CDKE5", OleDbType.Char).Value = CDKE5;
                        cmd.Parameters.AddWithValue("@#CDLOT", OleDbType.Char).Value = CDLOT;
                        cmd.Parameters.AddWithValue("@#CDTRA", OleDbType.Char).Value = CDTRA;
                        cmd.Parameters.AddWithValue("@#REFER", OleDbType.Char).Value = REFER;
                        cmd.Parameters.AddWithValue("@#LOCAT", OleDbType.Char).Value = LOCAT;
                        cmd.Parameters.AddWithValue("@#CDQUA", OleDbType.Char).Value = CDQUA;
                        cmd.Parameters.AddWithValue("@#QUACA", OleDbType.Char).Value = QUACA;
                        cmd.Parameters.AddWithValue("@#TECU1", OleDbType.Numeric).Value = TECU1;
                        cmd.Parameters.AddWithValue("@#TECU2", OleDbType.Numeric).Value = TECU2;
                        cmd.Parameters.AddWithValue("@#TECU3", OleDbType.Numeric).Value = TECU3;
                        cmd.Parameters.AddWithValue("@#TECU4", OleDbType.Numeric).Value = TECU4;
                        cmd.Parameters.AddWithValue("@#TECU5", OleDbType.Char).Value = TECU5;
                        cmd.Parameters.AddWithValue("@#TECU6", OleDbType.Char).Value = TECU6;
                        cmd.Parameters.AddWithValue("@#COMM0", OleDbType.Char).Value = COMM0;
                        cmd.Parameters.AddWithValue("@#DTORA", OleDbType.Numeric).Value = DTORA;

                        //cmd.Parameters.AddWithValue("@ANNUL", OleDbType.Char).Value = ANNUL;
                        //cmd.Parameters.AddWithValue("@FLAGS", OleDbType.Char).Value = FLAGS;
                        //cmd.Parameters.AddWithValue("@RECTY", OleDbType.Char).Value = RECTY;
                        //cmd.Parameters.AddWithValue("@CDSTO", OleDbType.Char).Value = CDSTO;
                        //cmd.Parameters.AddWithValue("@USRNM", OleDbType.Char).Value = USRNM;
                        //cmd.Parameters.AddWithValue("@DTTRA", OleDbType.Numeric).Value = DTTRA;
                        //cmd.Parameters.AddWithValue("@DTINP", OleDbType.Numeric).Value = DTINP;
                        //cmd.Parameters.AddWithValue("@CDEL0", OleDbType.Char).Value = CDEL0;
                        //cmd.Parameters.AddWithValue("@CDCON", OleDbType.Char).Value = CDCON;
                        //cmd.Parameters.AddWithValue("@BLELE", OleDbType.Numeric).Value = BLELE;
                        //cmd.Parameters.AddWithValue("@CDUM0", OleDbType.Char).Value = CDUM0;
                        //cmd.Parameters.AddWithValue("@CDKE1", OleDbType.Char).Value = CDKE1;
                        //cmd.Parameters.AddWithValue("@CDKE2", OleDbType.Char).Value = CDKE2;
                        //cmd.Parameters.AddWithValue("@CDKE3", OleDbType.Char).Value = CDKE3;
                        //cmd.Parameters.AddWithValue("@CDKE4", OleDbType.Char).Value = CDKE4;
                        //cmd.Parameters.AddWithValue("@CDKE5", OleDbType.Char).Value = CDKE5;
                        //cmd.Parameters.AddWithValue("@CDLOT", OleDbType.Char).Value = CDLOT;
                        //cmd.Parameters.AddWithValue("@CDTRA", OleDbType.Char).Value = CDTRA;
                        //cmd.Parameters.AddWithValue("@REFER", OleDbType.Char).Value = REFER;
                        //cmd.Parameters.AddWithValue("@LOCAT", OleDbType.Char).Value = LOCAT;
                        //cmd.Parameters.AddWithValue("@CDQUA", OleDbType.Char).Value = CDQUA;
                        //cmd.Parameters.AddWithValue("@QUACA", OleDbType.Char).Value = QUACA;
                        //cmd.Parameters.AddWithValue("@TECU1", OleDbType.Numeric).Value = TECU1;
                        //cmd.Parameters.AddWithValue("@TECU2", OleDbType.Numeric).Value = TECU2;
                        //cmd.Parameters.AddWithValue("@TECU3", OleDbType.Numeric).Value = TECU3;
                        //cmd.Parameters.AddWithValue("@TECU4", OleDbType.Numeric).Value = TECU4;
                        //cmd.Parameters.AddWithValue("@TECU5", OleDbType.Char).Value = TECU5;
                        //cmd.Parameters.AddWithValue("@TECU6", OleDbType.Char).Value = TECU6;
                        //cmd.Parameters.AddWithValue("@COMM0", OleDbType.Char).Value = COMM0;
                        //cmd.Parameters.AddWithValue("@DTORA", OleDbType.Numeric).Value = DTORA;

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

        #region UpdateBCSPRFTP_FLAGS

        public bool UpdateBCSPRFTP_FLAGS(string ConStr,
            string ANNUL, string FLAGS, string RECTY, string CDSTO, string USRNM, int? DTTRA, int? DTINP,
        string CDEL0, string CDCON, decimal? BLELE, string CDUM0, string CDKE1, string CDKE2, string CDKE3, string CDKE4, string CDKE5, string CDLOT, string CDTRA,
        string REFER, string LOCAT, string CDQUA, string QUACA, decimal? TECU1, decimal? TECU2, decimal? TECU3, decimal? TECU4, string TECU5, string TECU6, string COMM0,
        Int64? DTORA)
        {
            bool chkError = true;

            if (string.IsNullOrWhiteSpace(ConStr))
                return false;

            try
            {
                string query = string.Empty;

                //query = "Update BCSPRFTP "
                //        + " Set ANNUL = 'A' "
                //        + " , FLAGS = 'R' "
                //        + " Where CDEL0 = '" + CDEL0 + "'"
                //        + " And CDCON = '" + CDCON + "'"
                //        + " And CDKE1 = '" + CDKE1 + "'"
                //        + " And FLAGS = 'S' ";

                query = "Update BCSPRFTP "
                     + " Set #ANNUL = 'A' "
                     + " , #FLAGS = 'R' "
                     + " Where #CDEL0 = '" + CDEL0 + "'"
                     + " And #CDCON = '" + CDCON + "'"
                     + " And #CDKE1 = '" + CDKE1 + "'"
                     + " And #FLAGS = 'S' And #RECTY = 'S' And #CDSTO = '3N' ";

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

        #region DeleteBCSPRFTP

        public bool DeleteBCSPRFTP(string ConStr, string CDEL0, string CDCON, string CDKE1)
        {
            bool chkError = true;

            if (string.IsNullOrWhiteSpace(ConStr))
                return false;

            try
            {
                string query = string.Empty;

                StringBuilder q = new StringBuilder();

                //q.Append("DELETE FROM BCSPRFTP ");
                //q.Append(" Where CDEL0 = '");
                //q.Append(CDEL0);
                //q.Append("' And CDCON = '");
                //q.Append(CDCON);
                //q.Append("' And CDKE1 = '");
                //q.Append(CDKE1);
                //q.Append("' And FLAGS = 'S' And RECTY = 'S' And CDSTO = '3N' ");

                q.Append("DELETE FROM BCSPRFTP ");
                q.Append(" Where #CDEL0 = '");
                q.Append(CDEL0);
                q.Append("' And #CDCON = '");
                q.Append(CDCON);
                q.Append("' And #CDKE1 = '");
                q.Append(CDKE1);
                q.Append("' And #FLAGS = 'S' And #RECTY = 'S' And #CDSTO = '3N' ");

                //query = "DELETE FROM BCSPRFTP "
                //     + " Where #CDEL0 = '" + CDEL0 + "'"
                //     + " And #CDCON = '" + CDCON + "'"
                //     + " And #CDKE1 = '" + CDKE1 + "'"
                //     + " And #FLAGS = 'S' And #RECTY = 'S' And #CDSTO = '3N' ";

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

        #endregion

        #region Public Methods D365

        #region GetBCSPRFTP_D365
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FLAGS"></param>
        /// <param name="P_RECTY"></param>
        /// <param name="P_CDSTO"></param>
        /// <returns></returns>
        public List<BCSPRFTP_D365Result> GetBCSPRFTP_D365(string P_FLAGS, string P_RECTY, string P_CDSTO)
        {
            List<BCSPRFTP_D365Result> results = null;

            if (!HasConnectionD365())
                return results;

            GetBCSPRFTPParameter dbPara = new GetBCSPRFTPParameter();

            if (!string.IsNullOrEmpty(P_FLAGS))
                dbPara.FLAGS = P_FLAGS;

            if (!string.IsNullOrEmpty(P_RECTY))
                dbPara.RECTY = P_RECTY;

            if (!string.IsNullOrEmpty(P_CDSTO))
                dbPara.CDSTO = P_CDSTO;

            List<GetBCSPRFTPResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager_D365.Instance.GetBCSPRFTP(dbPara);
                if (null != dbResults)
                {
                    results = new List<BCSPRFTP_D365Result>();
                    foreach (GetBCSPRFTPResult dbResult in dbResults)
                    {
                        BCSPRFTP_D365Result inst = new BCSPRFTP_D365Result();

                        inst.IFNAME = dbResult.IFNAME;
                        inst.ANNUL = dbResult.ANNUL;
                        inst.FLAGS = dbResult.FLAGS;
                        inst.RECTY = dbResult.RECTY;
                        inst.CDSTO = dbResult.CDSTO;
                        inst.USRNM = dbResult.USRNM;
                        inst.DTTRA = dbResult.DTTRA;
                        inst.DTINP = dbResult.DTINP;
                        inst.CDEL0 = dbResult.CDEL0;
                        inst.CDCON = dbResult.CDCON;
                        inst.BLELE = dbResult.BLELE;
                        inst.CDUM0 = dbResult.CDUM0;
                        inst.CDKE1 = dbResult.CDKE1;
                        inst.CDKE2 = dbResult.CDKE2;
                        inst.CDKE3 = dbResult.CDKE3;
                        inst.CDKE4 = dbResult.CDKE4;
                        inst.CDKE5 = dbResult.CDKE5;
                        inst.CDLOT = dbResult.CDLOT;
                        inst.CDTRA = dbResult.CDTRA;
                        inst.REFER = dbResult.REFER;
                        inst.LOCAT = dbResult.LOCAT;
                        inst.CDQUA = dbResult.CDQUA;
                        inst.QUACA = dbResult.QUACA;
                        inst.TECU1 = dbResult.TECU1;
                        inst.TECU2 = dbResult.TECU2;
                        inst.TECU3 = dbResult.TECU3;
                        inst.TECU4 = dbResult.TECU4;
                        inst.TECU5 = dbResult.TECU5;
                        inst.TECU6 = dbResult.TECU6;
                        inst.COMM0 = dbResult.COMM0;
                        inst.DTORA = dbResult.DTORA;

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

        #region DeleteBCSPRFTP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CDEL0"></param>
        /// <param name="CDCON"></param>
        /// <param name="CDKE1"></param>
        /// <returns></returns>
        public bool DeleteBCSPRFTP(string CDEL0, string CDCON, string CDKE1)
        {
            bool result = false;

            if (string.IsNullOrEmpty(CDEL0))
                return result;

            if (string.IsNullOrEmpty(CDCON))
                return result;

            if (string.IsNullOrEmpty(CDKE1))
                return result;

            if (!HasConnectionD365())
                return result;

            DelBCSPRFTPParameter dbPara = new DelBCSPRFTPParameter();
            dbPara.CDEL0 = CDEL0;
            dbPara.CDCON = CDCON;
            dbPara.CDKE1 = CDKE1;

            DelBCSPRFTPResult dbResult = null;
            try
            {
                dbResult = DatabaseManager_D365.Instance.DelBCSPRFTP(dbPara);

                if (dbResult.ErrNum != 0)
                {
                    dbResult.ErrMsg.Err();
                    result = false;
                }
                else
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







