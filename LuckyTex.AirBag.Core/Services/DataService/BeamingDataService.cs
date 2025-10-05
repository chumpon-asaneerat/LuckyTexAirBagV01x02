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

    #region Warping Data Service

    /// <summary>
    /// The data service for User and Beaming.
    /// </summary>
    public class BeamingDataService : BaseDataService
    {
        #region Singelton

        private static BeamingDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static BeamingDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(BeamingDataService))
                    {
                        _instance = new BeamingDataService();
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
        private BeamingDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~BeamingDataService()
        {
        }

        #endregion

        #region Public Methods

        #region GetMachinesData
        // เพิ่มเพื่อเรียก MC อย่างเดียว
        /// <summary>
        /// Gets all Beaming Machines.
        /// </summary>
        /// <returns>Returns Beaming machine.</returns>
        public List<BeamingMCItem> GetMachinesData()
        {
            List<BeamingMCItem> results = new List<BeamingMCItem>();

            // Inspection Process ID = 8
            List<GETMACHINELISTBYPROCESSIDResult> dbResults = this.GetMachines(3);
            if (null == dbResults || dbResults.Count <= 0)
                return results;

            int mcNo = 1;
            BeamingMCItem inst = null;
            foreach (GETMACHINELISTBYPROCESSIDResult dbResult in dbResults)
            {
                inst = new BeamingMCItem();

                inst.MCNo = mcNo;
                inst.MCId = dbResult.MACHINEID;
                inst.DisplayName = dbResult.MCNAME.TrimEnd();

                results.Add(inst);

                ++mcNo;
            }

            return results;
        }
        #endregion

        #region เพิ่มใหม่ WEAV_GETCNTCHINALOT ใช้ในการ Load WEAV

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string WEAV_GETCNTCHINALOT(string P_LOT)
        {
            string results = string.Empty;

            if (!HasConnection())
                return results;

            WEAV_GETCNTCHINALOTParameter dbPara = new WEAV_GETCNTCHINALOTParameter();
            dbPara.P_LOT = P_LOT;

            WEAV_GETCNTCHINALOTResult dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETCNTCHINALOT(dbPara);
                if (null != dbResults)
                {
                    results = dbResults.CNT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ BEAM_GETSPECBYCHOPNO ใช้ในการ Load BEAM

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BEAM_GETSPECBYCHOPNO> BEAM_GETSPECBYCHOPNO(string P_ITMPREPARE)
        {
            List<BEAM_GETSPECBYCHOPNO> results = null;

            if (!HasConnection())
                return results;


            BEAM_GETSPECBYCHOPNOParameter dbPara = new BEAM_GETSPECBYCHOPNOParameter();
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
           
            List<BEAM_GETSPECBYCHOPNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_GETSPECBYCHOPNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_GETSPECBYCHOPNO>();

                    foreach (BEAM_GETSPECBYCHOPNOResult dbResult in dbResults)
                    {
                        BEAM_GETSPECBYCHOPNO inst = new BEAM_GETSPECBYCHOPNO();

                        inst.CHOPNO = dbResult.CHOPNO;
                        inst.NOWARPBEAM = dbResult.NOWARPBEAM;
                        inst.TOTALYARN = dbResult.TOTALYARN;
                        inst.TOTALKEBA = dbResult.TOTALKEBA;
                        inst.BEAMLENGTH = dbResult.BEAMLENGTH;
                        inst.MAXHARDNESS = dbResult.MAXHARDNESS;
                        inst.MINHARDNESS = dbResult.MINHARDNESS;
                        inst.MAXBEAMWIDTH = dbResult.MAXBEAMWIDTH;
                        inst.MINBEAMWIDTH = dbResult.MINBEAMWIDTH;
                        inst.MAXSPEED = dbResult.MAXSPEED;
                        inst.MINSPEED = dbResult.MINSPEED;
                        inst.MAXYARNTENSION = dbResult.MAXYARNTENSION;
                        inst.MINYARNTENSION = dbResult.MINYARNTENSION;
                        inst.MAXWINDTENSION = dbResult.MAXWINDTENSION;
                        inst.MINWINDTENSION = dbResult.MINWINDTENSION;
                        inst.COMBTYPE = dbResult.COMBTYPE;
                        inst.COMBPITCH = dbResult.COMBPITCH;
                        inst.TOTALBEAM = dbResult.TOTALBEAM;

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

        #region เพิ่มใหม่ BEAM_GETWARPNOBYITEMPREPARE ใช้ในการ Load BEAM

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BEAM_GETWARPNOBYITEMPREPARE> BEAM_GETWARPNOBYITEMPREPARE(string P_ITMPREPARE)
        {
            List<BEAM_GETWARPNOBYITEMPREPARE> results = null;

            if (!HasConnection())
                return results;


            BEAM_GETWARPNOBYITEMPREPAREParameter dbPara = new BEAM_GETWARPNOBYITEMPREPAREParameter();
            dbPara.P_ITMPREPARE = P_ITMPREPARE;

            List<BEAM_GETWARPNOBYITEMPREPAREResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_GETWARPNOBYITEMPREPARE(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_GETWARPNOBYITEMPREPARE>();

                    foreach (BEAM_GETWARPNOBYITEMPREPAREResult dbResult in dbResults)
                    {
                        BEAM_GETWARPNOBYITEMPREPARE inst = new BEAM_GETWARPNOBYITEMPREPARE();

                        inst.IsSelect = false;
                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.WARPMC = dbResult.WARPMC;
                        inst.ACTUALCH = dbResult.ACTUALCH;
                        inst.TOTALBEAM = dbResult.TOTALBEAM;
                       
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

        #region เพิ่มใหม่ WARP_GETWARPERLOTBYHEADNO ใช้ในการ Load BEAM

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WARP_GETWARPERLOTBYHEADNO> WARP_GETWARPERLOTBYHEADNO(string P_WARPHEADNO)
        {
            List<WARP_GETWARPERLOTBYHEADNO> results = null;

            if (!HasConnection())
                return results;


            WARP_GETWARPERLOTBYHEADNOParameter dbPara = new WARP_GETWARPERLOTBYHEADNOParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<WARP_GETWARPERLOTBYHEADNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETWARPERLOTBYHEADNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_GETWARPERLOTBYHEADNO>();

                    foreach (WARP_GETWARPERLOTBYHEADNOResult dbResult in dbResults)
                    {
                        WARP_GETWARPERLOTBYHEADNO inst = new WARP_GETWARPERLOTBYHEADNO();

                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.WARPERLOT = dbResult.WARPERLOT;
                        inst.BEAMNO = dbResult.BEAMNO;
                        inst.SIDE = dbResult.SIDE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.SPEED = dbResult.SPEED;
                        inst.HARDNESS_L = dbResult.HARDNESS_L;
                        inst.HARDNESS_N = dbResult.HARDNESS_N;
                        inst.HARDNESS_R = dbResult.HARDNESS_R;
                        inst.TENSION = dbResult.TENSION;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.FLAG = dbResult.FLAG;
                        inst.WARPMC = dbResult.WARPMC;
                       
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

        #region เพิ่มใหม่ CheckWarpheadNo_WarperLot ใช้ในการ Load BEAM

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BEAM_GETWARPERLOT> CheckWarpheadNo_WarperLot(string P_WARPHEADNO, string WarperLot, decimal? NoCH)
        {
            List<BEAM_GETWARPERLOT> results = null;

            if (!HasConnection())
                return results;


            WARP_GETWARPERLOTBYHEADNOParameter dbPara = new WARP_GETWARPERLOTBYHEADNOParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<WARP_GETWARPERLOTBYHEADNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETWARPERLOTBYHEADNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_GETWARPERLOT>();

                    foreach (WARP_GETWARPERLOTBYHEADNOResult dbResult in dbResults)
                    {
                        BEAM_GETWARPERLOT inst = new BEAM_GETWARPERLOT();

                        if (P_WARPHEADNO == dbResult.WARPHEADNO)
                        {
                            if (WarperLot == dbResult.WARPERLOT)
                            {
                                inst.WARPHEADNO = dbResult.WARPHEADNO;
                                inst.WARPERLOT = dbResult.WARPERLOT;

                                inst.LENGTH = dbResult.LENGTH;
                                inst.OLDTOTALEND = NoCH;
                                inst.TOTALEND = NoCH;
                                inst.TAKEOUT = 0;

                                results.Add(inst);
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

        #region เพิ่มใหม่ BEAM_GETSTOPREASONBYBEAMLOT ใช้ในการ Load BEAM
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <param name="P_WARPLOT"></param>
        /// <returns></returns>
        public List<BEAM_GETSTOPREASONBYBEAMLOT> BEAM_GETSTOPREASONBYBEAMLOT(string P_BEAMERNO, string P_BEAMLOT)
        {
            List<BEAM_GETSTOPREASONBYBEAMLOT> results = null;

            if (!HasConnection())
                return results;

            BEAM_GETSTOPREASONBYBEAMLOTParameter dbPara = new BEAM_GETSTOPREASONBYBEAMLOTParameter();
            dbPara.P_BEAMERNO = P_BEAMERNO;
            dbPara.P_BEAMLOT = P_BEAMLOT;

            List<BEAM_GETSTOPREASONBYBEAMLOTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_GETSTOPREASONBYBEAMLOT(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_GETSTOPREASONBYBEAMLOT>();
                    foreach (BEAM_GETSTOPREASONBYBEAMLOTResult dbResult in dbResults)
                    {
                        BEAM_GETSTOPREASONBYBEAMLOT inst = new BEAM_GETSTOPREASONBYBEAMLOT();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.REASON = dbResult.REASON;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.OPERATOR = dbResult.OPERATOR;
                        inst.OTHERFLAG = dbResult.OTHERFLAG;
                        inst.CREATEDATE = dbResult.CREATEDATE;

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

        #region เพิ่มใหม่ BEAM_GETBEAMLOTBYBEAMERNO ใช้ในการ Load BEAM
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<BEAM_GETBEAMLOTBYBEAMERNO> BEAM_GETBEAMLOTBYBEAMERNO(string P_BEAMERNO)
        {
            List<BEAM_GETBEAMLOTBYBEAMERNO> results = null;

            if (!HasConnection())
                return results;

            BEAM_GETBEAMLOTBYBEAMERNOParameter dbPara = new BEAM_GETBEAMLOTBYBEAMERNOParameter();
            dbPara.P_BEAMERNO = P_BEAMERNO;

            List<BEAM_GETBEAMLOTBYBEAMERNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_GETBEAMLOTBYBEAMERNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_GETBEAMLOTBYBEAMERNO>();
                    foreach (BEAM_GETBEAMLOTBYBEAMERNOResult dbResult in dbResults)
                    {
                        BEAM_GETBEAMLOTBYBEAMERNO inst = new BEAM_GETBEAMLOTBYBEAMERNO();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.BEAMNO = dbResult.BEAMNO;
 
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.SPEED = dbResult.SPEED;
                        inst.HARDNESS_L = dbResult.HARDNESS_L;
                        inst.HARDNESS_N = dbResult.HARDNESS_N;
                        inst.HARDNESS_R = dbResult.HARDNESS_R;
                        inst.BEAMSTANDTENSION = dbResult.BEAMSTANDTENSION;

                        inst.BEAMSTANDTENSION = dbResult.BEAMSTANDTENSION;
                        inst.WINDINGTENSION = dbResult.WINDINGTENSION;
                        inst.INSIDE_WIDTH = dbResult.INSIDE_WIDTH;
                        inst.OUTSIDE_WIDTH = dbResult.OUTSIDE_WIDTH;
                        inst.FULL_WIDTH = dbResult.FULL_WIDTH;
                       
                        inst.STARTBY = dbResult.STARTBY;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.FLAG = dbResult.FLAG;
                        inst.BEAMMC = dbResult.BEAMMC;

                        inst.REMARK = dbResult.REMARK;
                        inst.TENSION_ST1 = dbResult.TENSION_ST1;
                        inst.TENSION_ST2 = dbResult.TENSION_ST2;
                        inst.TENSION_ST3 = dbResult.TENSION_ST3;
                        inst.TENSION_ST4 = dbResult.TENSION_ST4;
                        inst.TENSION_ST5 = dbResult.TENSION_ST5;
                        inst.TENSION_ST6 = dbResult.TENSION_ST6;
                        inst.TENSION_ST7 = dbResult.TENSION_ST7;
                        inst.TENSION_ST8 = dbResult.TENSION_ST8;
                        inst.TENSION_ST9 = dbResult.TENSION_ST9;
                        inst.TENSION_ST10 = dbResult.TENSION_ST10;

                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.OLDBEAMNO = dbResult.OLDBEAMNO;
                        inst.KEBA = dbResult.KEBA;
                        inst.MISSYARN = dbResult.MISSYARN;
                        inst.OTHER = dbResult.OTHER;

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

        #region เพิ่มใหม่ BEAM_TRANFERSLIP ใช้ในการ Load BEAM

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BEAM_TRANFERSLIP> BEAM_TRANFERSLIP(string P_BEAMLOT)
        {
            List<BEAM_TRANFERSLIP> results = null;

            if (!HasConnection())
                return results;


            BEAM_TRANFERSLIPParameter dbPara = new BEAM_TRANFERSLIPParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;

            List<BEAM_TRANFERSLIPResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_TRANFERSLIP(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_TRANFERSLIP>();

                    foreach (BEAM_TRANFERSLIPResult dbResult in dbResults)
                    {
                        BEAM_TRANFERSLIP inst = new BEAM_TRANFERSLIP();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.BEAMNO = dbResult.BEAMNO;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.SPEED = dbResult.SPEED;
                        inst.BEAMSTANDTENSION = dbResult.BEAMSTANDTENSION;
                        inst.WINDINGTENSION = dbResult.WINDINGTENSION;
                        inst.HARDNESS_L = dbResult.HARDNESS_L;
                        inst.HARDNESS_N = dbResult.HARDNESS_N;
                        inst.HARDNESS_R = dbResult.HARDNESS_R;
                        inst.INSIDE_WIDTH = dbResult.INSIDE_WIDTH;
                        inst.OUTSIDE_WIDTH = dbResult.OUTSIDE_WIDTH;
                        inst.FULL_WIDTH = dbResult.FULL_WIDTH;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.BEAMMC = dbResult.BEAMMC;
                        inst.FLAG = dbResult.FLAG;
                        inst.REMARK = dbResult.REMARK;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;

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

        #region เพิ่มใหม่ BEAM_GETBEAMERMCSTATUS ใช้ในการ Load BEAM

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BEAM_GETBEAMERMCSTATUS> BEAM_GETBEAMERMCSTATUS(string P_MCNO)
        {
            List<BEAM_GETBEAMERMCSTATUS> results = null;

            if (!HasConnection())
                return results;


            BEAM_GETBEAMERMCSTATUSParameter dbPara = new BEAM_GETBEAMERMCSTATUSParameter();
            dbPara.P_MCNO = P_MCNO;

            List<BEAM_GETBEAMERMCSTATUSResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_GETBEAMERMCSTATUS(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_GETBEAMERMCSTATUS>();

                    foreach (BEAM_GETBEAMERMCSTATUSResult dbResult in dbResults)
                    {
                        BEAM_GETBEAMERMCSTATUS inst = new BEAM_GETBEAMERMCSTATUS();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.TOTALYARN = dbResult.TOTALYARN;
                        inst.TOTALKEBA = dbResult.TOTALKEBA;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.STATUS = dbResult.STATUS;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.MCNO = dbResult.MCNO;
                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.ADJUSTKEBA = dbResult.ADJUSTKEBA;
                        inst.REMARK = dbResult.REMARK;
                        inst.NOWARPBEAM = dbResult.NOWARPBEAM;
                        inst.TOTALBEAM = dbResult.TOTALBEAM;

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

        #region เพิ่มใหม่ BEAM_GETBEAMERMCSTATUS ใช้ในการ Load BEAM

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BEAM_GETBEAMERMCSTATUS> GETBEAMERMCSTATUS()
        {
            List<BEAM_GETBEAMERMCSTATUS> results = null;

            if (!HasConnection())
                return results;


            BEAM_GETBEAMERMCSTATUSParameter dbPara = new BEAM_GETBEAMERMCSTATUSParameter();

            List<BEAM_GETBEAMERMCSTATUSResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_GETBEAMERMCSTATUS(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_GETBEAMERMCSTATUS>();

                    foreach (BEAM_GETBEAMERMCSTATUSResult dbResult in dbResults)
                    {
                        BEAM_GETBEAMERMCSTATUS inst = new BEAM_GETBEAMERMCSTATUS();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.TOTALYARN = dbResult.TOTALYARN;
                        inst.TOTALKEBA = dbResult.TOTALKEBA;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.STATUS = dbResult.STATUS;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.MCNO = dbResult.MCNO;
                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.ADJUSTKEBA = dbResult.ADJUSTKEBA;
                        inst.REMARK = dbResult.REMARK;
                        inst.NOWARPBEAM = dbResult.NOWARPBEAM;
                        inst.TOTALBEAM = dbResult.TOTALBEAM;

                        if (inst.STATUS == "P")
                            inst.strSTATUS = "Processing";
                        else if (inst.STATUS == "S")
                            inst.strSTATUS = "Setting";
                        else
                            inst.strSTATUS = inst.STATUS;

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

        #region เพิ่มใหม่ BEAM_GETINPROCESSLOTBYBEAMNO ใช้ในการ Load BEAM

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <returns></returns>
        public List<BEAM_GETINPROCESSLOTBYBEAMNO> BEAM_GETINPROCESSLOTBYBEAMNO(string P_BEAMERNO)
        {
            List<BEAM_GETINPROCESSLOTBYBEAMNO> results = null;

            if (string.IsNullOrWhiteSpace(P_BEAMERNO))
                return results;

            if (!HasConnection())
                return results;


            BEAM_GETINPROCESSLOTBYBEAMNOParameter dbPara = new BEAM_GETINPROCESSLOTBYBEAMNOParameter();
            dbPara.P_BEAMERNO = P_BEAMERNO;

            List<BEAM_GETINPROCESSLOTBYBEAMNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_GETINPROCESSLOTBYBEAMNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_GETINPROCESSLOTBYBEAMNO>();

                    foreach (BEAM_GETINPROCESSLOTBYBEAMNOResult dbResult in dbResults)
                    {
                        BEAM_GETINPROCESSLOTBYBEAMNO inst = new BEAM_GETINPROCESSLOTBYBEAMNO();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.BEAMNO = dbResult.BEAMNO;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.SPEED = dbResult.SPEED;
                        inst.BEAMSTANDTENSION = dbResult.BEAMSTANDTENSION;
                        inst.WINDINGTENSION = dbResult.WINDINGTENSION;
                        inst.HARDNESS_L = dbResult.HARDNESS_L;
                        inst.HARDNESS_N = dbResult.HARDNESS_N;
                        inst.HARDNESS_R = dbResult.HARDNESS_R;
                        inst.INSIDE_WIDTH = dbResult.INSIDE_WIDTH;
                        inst.OUTSIDE_WIDTH = dbResult.OUTSIDE_WIDTH;
                        inst.FULL_WIDTH = dbResult.FULL_WIDTH;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.BEAMMC = dbResult.BEAMMC;
                        inst.FLAG = dbResult.FLAG;
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

        #region เพิ่มใหม่ WARP_SEARCHWARPRECORD ใช้ในการ Load Beaming
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <param name="P_MC"></param>
        /// <param name="P_ITMPREPARE"></param>
        /// <param name="P_STARTDATE"></param>
        /// <returns></returns>
        public List<BEAM_SEARCHBEAMRECORD> BEAM_SEARCHBEAMRECORD(string P_BEAMERNO, string P_MC, string P_ITMPREPARE, string P_STARTDATE)
        {
            List<BEAM_SEARCHBEAMRECORD> results = null;

            if (!HasConnection())
                return results;

            BEAM_SEARCHBEAMRECORDParameter dbPara = new BEAM_SEARCHBEAMRECORDParameter();
            dbPara.P_BEAMERNO = P_BEAMERNO;
            dbPara.P_MC = P_MC;
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_STARTDATE = P_STARTDATE;

            List<BEAM_SEARCHBEAMRECORDResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_SEARCHBEAMRECORD(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_SEARCHBEAMRECORD>();
                    foreach (BEAM_SEARCHBEAMRECORDResult dbResult in dbResults)
                    {
                        BEAM_SEARCHBEAMRECORD inst = new BEAM_SEARCHBEAMRECORD();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.TOTALYARN = dbResult.TOTALYARN;
                        inst.TOTALKEBA = dbResult.TOTALKEBA;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.STATUS = dbResult.STATUS;
                        inst.MCNO = dbResult.MCNO;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.ADJUSTKEBA = dbResult.ADJUSTKEBA;
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

        #region เพิ่มใหม่ BEAM_GETWARPROLLBYBEAMERNO ใช้ในการ Load Beaming
       /// <summary>
       /// 
       /// </summary>
       /// <param name="P_BEAMERNO"></param>
       /// <returns></returns>
        public List<BEAM_GETWARPROLLBYBEAMERNO> BEAM_GETWARPROLLBYBEAMERNO(string P_BEAMERNO)
        {
            List<BEAM_GETWARPROLLBYBEAMERNO> results = null;

            if (!HasConnection())
                return results;

            BEAM_GETWARPROLLBYBEAMERNOParameter dbPara = new BEAM_GETWARPROLLBYBEAMERNOParameter();
            dbPara.P_BEAMERNO = P_BEAMERNO;

            List<BEAM_GETWARPROLLBYBEAMERNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_GETWARPROLLBYBEAMERNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_GETWARPROLLBYBEAMERNO>();
                    foreach (BEAM_GETWARPROLLBYBEAMERNOResult dbResult in dbResults)
                    {
                        BEAM_GETWARPROLLBYBEAMERNO inst = new BEAM_GETWARPROLLBYBEAMERNO();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.WARPERLOT = dbResult.WARPERLOT;
                        
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

        #region เพิ่มใหม่ BEAM_GETBEAMROLLDETAIL ใช้ในการ Load Beaming
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMROLL"></param>
        /// <returns></returns>
        public List<BEAM_GETBEAMROLLDETAIL> BEAM_GETBEAMROLLDETAIL(string P_BEAMROLL)
        {
            List<BEAM_GETBEAMROLLDETAIL> results = null;

            if (!HasConnection())
                return results;

            BEAM_GETBEAMROLLDETAILParameter dbPara = new BEAM_GETBEAMROLLDETAILParameter();
            dbPara.P_BEAMROLL = P_BEAMROLL;

            List<BEAM_GETBEAMROLLDETAILResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_GETBEAMROLLDETAIL(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_GETBEAMROLLDETAIL>();
                    foreach (BEAM_GETBEAMROLLDETAILResult dbResult in dbResults)
                    {
                        BEAM_GETBEAMROLLDETAIL inst = new BEAM_GETBEAMROLLDETAIL();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.BEAMNO = dbResult.BEAMNO;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.SPEED = dbResult.SPEED;
                        inst.BEAMSTANDTENSION = dbResult.BEAMSTANDTENSION;
                        inst.WINDINGTENSION = dbResult.WINDINGTENSION;
                        inst.HARDNESS_L = dbResult.HARDNESS_L;
                        inst.HARDNESS_N = dbResult.HARDNESS_N;
                        inst.HARDNESS_R = dbResult.HARDNESS_R;
                        inst.INSIDE_WIDTH = dbResult.INSIDE_WIDTH;
                        inst.OUTSIDE_WIDTH = dbResult.OUTSIDE_WIDTH;
                        inst.FULL_WIDTH = dbResult.FULL_WIDTH;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.BEAMMC = dbResult.BEAMMC;
                        inst.FLAG = dbResult.FLAG;
                        inst.REMARK = dbResult.REMARK;
                        inst.TENSION_ST1 = dbResult.TENSION_ST1;
                        inst.TENSION_ST2 = dbResult.TENSION_ST2;
                        inst.TENSION_ST3 = dbResult.TENSION_ST3;
                        inst.TENSION_ST4 = dbResult.TENSION_ST4;
                        inst.TENSION_ST5 = dbResult.TENSION_ST5;
                        inst.TENSION_ST6 = dbResult.TENSION_ST6;
                        inst.TENSION_ST7 = dbResult.TENSION_ST7;
                        inst.TENSION_ST8 = dbResult.TENSION_ST8;
                        inst.TENSION_ST9 = dbResult.TENSION_ST9;
                        inst.TENSION_ST10 = dbResult.TENSION_ST10;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.OLDBEAMNO = dbResult.OLDBEAMNO;
                        inst.EDITDATE = dbResult.EDITDATE;

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

        #region เพิ่มใหม่ BeamingRecordHead ใช้ในการ Load Beaming

        public BeamingRecordHead BeamingRecordHead(string P_BEAMERNO, string P_ITM_PREPARE, string P_MCNO, decimal? P_TOTALYARN, decimal? P_TOTALKEBA, decimal? P_ADJUSTKEBA
                , DateTime? P_STARTDATE, DateTime? P_ENDDATE, string P_REMARK)
        {
            BeamingRecordHead results = new BeamingRecordHead();

            try
            {
                results.BEAMERNO = P_BEAMERNO;
                results.ITM_PREPARE = P_ITM_PREPARE;
                results.MCNO = P_MCNO;
                results.TOTALYARN = P_TOTALYARN;
                results.TOTALKEBA = P_TOTALKEBA;
                results.ADJUSTKEBA = P_ADJUSTKEBA;
                results.STARTDATE = P_STARTDATE;
                results.ENDDATE = P_ENDDATE;
                results.REMARK = P_REMARK;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        //เพิ่ม 25/08/17
        #region เพิ่มใหม่ BEAM_BEAMLIST ใช้ในการ Load Beaming
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <param name="P_MC"></param>
        /// <param name="P_ITMPREPARE"></param>
        /// <param name="P_STARTDATE"></param>
        /// <param name="P_ENDDATE"></param>
        /// <returns></returns>
        public List<BEAM_BEAMLIST> BEAM_BEAMLIST(string P_BEAMERNO, string P_MC, string P_ITMPREPARE, string P_STARTDATE, string P_ENDDATE)
        {
            List<BEAM_BEAMLIST> results = null;

            if (!HasConnection())
                return results;

            BEAM_BEAMLISTParameter dbPara = new BEAM_BEAMLISTParameter();
            dbPara.P_BEAMERNO = P_BEAMERNO;
            dbPara.P_MC = P_MC;
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_ENDDATE = P_ENDDATE;

            List<BEAM_BEAMLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.BEAM_BEAMLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<BEAM_BEAMLIST>();
                    foreach (BEAM_BEAMLISTResult dbResult in dbResults)
                    {
                        BEAM_BEAMLIST inst = new BEAM_BEAMLIST();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.BEAMNO = dbResult.BEAMNO;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.SPEED = dbResult.SPEED;
                        inst.BEAMSTANDTENSION = dbResult.BEAMSTANDTENSION;
                        inst.WINDINGTENSION = dbResult.WINDINGTENSION;
                        inst.HARDNESS_L = dbResult.HARDNESS_L;
                        inst.HARDNESS_N = dbResult.HARDNESS_N;
                        inst.HARDNESS_R = dbResult.HARDNESS_R;
                        inst.INSIDE_WIDTH = dbResult.INSIDE_WIDTH;
                        inst.OUTSIDE_WIDTH = dbResult.OUTSIDE_WIDTH;
                        inst.FULL_WIDTH = dbResult.FULL_WIDTH;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.BEAMMC = dbResult.BEAMMC;
                        inst.FLAG = dbResult.FLAG;
                        inst.REMARK = dbResult.REMARK;
                        inst.TENSION_ST1 = dbResult.TENSION_ST1;
                        inst.TENSION_ST2 = dbResult.TENSION_ST2;
                        inst.TENSION_ST3 = dbResult.TENSION_ST3;
                        inst.TENSION_ST4 = dbResult.TENSION_ST4;
                        inst.TENSION_ST5 = dbResult.TENSION_ST5;
                        inst.TENSION_ST6 = dbResult.TENSION_ST6;
                        inst.TENSION_ST7 = dbResult.TENSION_ST7;
                        inst.TENSION_ST8 = dbResult.TENSION_ST8;
                        inst.TENSION_ST9 = dbResult.TENSION_ST9;
                        inst.TENSION_ST10 = dbResult.TENSION_ST10;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.OLDBEAMNO = dbResult.OLDBEAMNO;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.TOTALYARN = dbResult.TOTALYARN;
                        inst.TOTALKEBA = dbResult.TOTALKEBA;

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

        #region BEAM_GETBEAMERROLLREMARK

        public string BEAM_GETBEAMERROLLREMARK(string P_BEAMLOT)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMLOT))
                return result;

            if (!HasConnection())
                return result;

            BEAM_GETBEAMERROLLREMARKParameter dbPara = new BEAM_GETBEAMERROLLREMARKParameter();
            dbPara.P_BEAMLOT = P_BEAMLOT;


            BEAM_GETBEAMERROLLREMARKResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_GETBEAMERROLLREMARK(dbPara);

                result = dbResult.R_REMARK;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region BEAM_INSERTBEAMNO

        public string BEAM_INSERTBEAMNO(string P_BEAMNO, string P_WARPERHEADNO, string P_ITMPREPARE,string P_PRODUCTID
            , string P_MCNO, decimal? P_TOTALYARN, decimal? P_TOTALKEBA, string P_OPERATOR, decimal? P_ADJUSTKEBA, string P_REMARK)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMNO))
                return result;

            if (!HasConnection())
                return result;

            BEAM_INSERTBEAMNOParameter dbPara = new BEAM_INSERTBEAMNOParameter();
            dbPara.P_BEAMNO = P_BEAMNO;
            dbPara.P_WARPERHEADNO = P_WARPERHEADNO;
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_PRODUCTID = P_PRODUCTID;
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_TOTALYARN = P_TOTALYARN;
            dbPara.P_TOTALKEBA = P_TOTALKEBA;
            dbPara.P_OPERATOR = P_OPERATOR;

            // เพิ่มใหม่
            dbPara.P_ADJUSTKEBA = P_ADJUSTKEBA;
            dbPara.P_REMARK = P_REMARK;
            
            BEAM_INSERTBEAMNOResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_INSERTBEAMNO(dbPara);

                result = dbResult.R_RESULT;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region BEAM_INSERTBEAMINGDETAIL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMERNO"></param>
        /// <param name="P_MCNO"></param>
        /// <param name="P_BEAMNO"></param>
        /// <param name="P_STARTDATE"></param>
        /// <param name="P_STARTBY"></param>
        /// <returns></returns>
        public BEAM_INSERTBEAMINGDETAIL BEAM_INSERTBEAMINGDETAIL(string P_BEAMERNO, string P_MCNO, string P_BEAMNO, DateTime? P_STARTDATE, string P_STARTBY)
        {
            BEAM_INSERTBEAMINGDETAIL results = null;

            if (string.IsNullOrWhiteSpace(P_BEAMERNO))
                return results;

            if (!HasConnection())
                return results;

            BEAM_INSERTBEAMINGDETAILParameter dbPara = new BEAM_INSERTBEAMINGDETAILParameter();

            dbPara.P_BEAMERNO = P_BEAMERNO;
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_BEAMNO = P_BEAMNO;
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_STARTBY = P_STARTBY;

            BEAM_INSERTBEAMINGDETAILResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.BEAM_INSERTBEAMINGDETAIL(dbPara);

                results = new BEAM_INSERTBEAMINGDETAIL();

                if (null != dbResults)
                {
                    results.R_BEAMLOT = dbResults.R_BEAMLOT;
                    results.RESULT = dbResults.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region BEAM_UPDATEBEAMNO

        public string BEAM_UPDATEBEAMNO(string P_BEAMNO,DateTime P_ENDDATE, string P_STATUS)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMNO))
                return result;

            if (!HasConnection())
                return result;

            BEAM_UPDATEBEAMNOParameter dbPara = new BEAM_UPDATEBEAMNOParameter();

            dbPara.P_BEAMNO = P_BEAMNO;
            dbPara.P_ENDDATE = P_ENDDATE;
            dbPara.P_STATUS = P_STATUS;


            BEAM_UPDATEBEAMNOResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_UPDATEBEAMNO(dbPara);

                result = dbResult.R_RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region BEAM_INSERTBEAMMCSTOP

        public string BEAM_INSERTBEAMMCSTOP(string P_BEAMERNO, string P_BEAMLOT, string P_REASON, decimal? P_LENGTH, string P_OTHER, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMERNO))
                return result;

            if (!HasConnection())
                return result;

            BEAM_INSERTBEAMMCSTOPParameter dbPara = new BEAM_INSERTBEAMMCSTOPParameter();
            dbPara.P_BEAMERNO = P_BEAMERNO;
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_REASON = P_REASON;
            dbPara.P_LENGTH = P_LENGTH;
            dbPara.P_OTHER = P_OTHER;
            dbPara.P_OPERATOR = P_OPERATOR;

            BEAM_INSERTBEAMMCSTOPResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_INSERTBEAMMCSTOP(dbPara);

                result = dbResult.R_RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region BEAM_UPDATEBEAMDETAIL

        public bool BEAM_UPDATEBEAMDETAIL(string P_BEAMERNO, string P_BEAMLOT, decimal? P_LENGTH, DateTime? P_ENDDATE, decimal? P_SPEED,
            decimal? P_HARDL, decimal? P_HARDN, decimal? P_HARDR, decimal? P_STANDTENSION,
            decimal? P_WINDTENSION, decimal? P_INSIDE, decimal? P_OUTSIDE, decimal? P_FULL,
            string P_DOFFBY
            , decimal? P_TENSION_ST1, decimal? P_TENSION_ST2, decimal? P_TENSION_ST3, decimal? P_TENSION_ST4, decimal? P_TENSION_ST5
            , decimal? P_TENSION_ST6, decimal? P_TENSION_ST7, decimal? P_TENSION_ST8, decimal? P_TENSION_ST9, decimal? P_TENSION_ST10, string P_OPERATOR)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_BEAMERNO))
                return result;

            if (!HasConnection())
                return result;

            BEAM_UPDATEBEAMDETAILParameter dbPara = new BEAM_UPDATEBEAMDETAILParameter();

            dbPara.P_BEAMERNO = P_BEAMERNO;
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_LENGTH = P_LENGTH;
            dbPara.P_ENDDATE = P_ENDDATE;
            dbPara.P_SPEED = P_SPEED;
            dbPara.P_HARDL = P_HARDL;
            dbPara.P_HARDN = P_HARDN;
            dbPara.P_HARDR = P_HARDR;
            dbPara.P_STANDTENSION = P_STANDTENSION;
            dbPara.P_WINDTENSION = P_WINDTENSION;
            dbPara.P_INSIDE = P_INSIDE;
            dbPara.P_OUTSIDE = P_OUTSIDE;
            dbPara.P_FULL = P_FULL;
            dbPara.P_DOFFBY = P_DOFFBY;

            dbPara.P_TENSION_ST1 = P_TENSION_ST1;
            dbPara.P_TENSION_ST2 = P_TENSION_ST2;
            dbPara.P_TENSION_ST3 = P_TENSION_ST3;
            dbPara.P_TENSION_ST4 = P_TENSION_ST4;
            dbPara.P_TENSION_ST5 = P_TENSION_ST5;
            dbPara.P_TENSION_ST6 = P_TENSION_ST6;
            dbPara.P_TENSION_ST7 = P_TENSION_ST7;
            dbPara.P_TENSION_ST8 = P_TENSION_ST8;
            dbPara.P_TENSION_ST9 = P_TENSION_ST9;
            dbPara.P_TENSION_ST10 = P_TENSION_ST10;
            dbPara.P_OPERATOR = P_OPERATOR;

            BEAM_UPDATEBEAMDETAILResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_UPDATEBEAMDETAIL(dbPara);

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

        #region BEAM_UPDATEBEAMDETAIL

        public bool BEAM_UPDATEBEAMDETAIL(string P_BEAMERNO, string P_BEAMLOT, decimal? P_LENGTH, decimal? P_SPEED,
            decimal? P_HARDL, decimal? P_HARDN, decimal? P_HARDR, decimal? P_STANDTENSION,
            decimal? P_WINDTENSION, decimal? P_INSIDE, decimal? P_OUTSIDE, decimal? P_FULL
            , decimal? P_TENSION_ST1, decimal? P_TENSION_ST2, decimal? P_TENSION_ST3, decimal? P_TENSION_ST4, decimal? P_TENSION_ST5
            , decimal? P_TENSION_ST6, decimal? P_TENSION_ST7, decimal? P_TENSION_ST8, decimal? P_TENSION_ST9, decimal? P_TENSION_ST10)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_BEAMERNO))
                return result;

            if (!HasConnection())
                return result;

            BEAM_UPDATEBEAMDETAILParameter dbPara = new BEAM_UPDATEBEAMDETAILParameter();

            dbPara.P_BEAMERNO = P_BEAMERNO;
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_LENGTH = P_LENGTH;

            dbPara.P_SPEED = P_SPEED;
            dbPara.P_HARDL = P_HARDL;
            dbPara.P_HARDN = P_HARDN;
            dbPara.P_HARDR = P_HARDR;
            dbPara.P_STANDTENSION = P_STANDTENSION;
            dbPara.P_WINDTENSION = P_WINDTENSION;
            dbPara.P_INSIDE = P_INSIDE;
            dbPara.P_OUTSIDE = P_OUTSIDE;
            dbPara.P_FULL = P_FULL;

            dbPara.P_TENSION_ST1 = P_TENSION_ST1;
            dbPara.P_TENSION_ST2 = P_TENSION_ST2;
            dbPara.P_TENSION_ST3 = P_TENSION_ST3;
            dbPara.P_TENSION_ST4 = P_TENSION_ST4;
            dbPara.P_TENSION_ST5 = P_TENSION_ST5;
            dbPara.P_TENSION_ST6 = P_TENSION_ST6;
            dbPara.P_TENSION_ST7 = P_TENSION_ST7;
            dbPara.P_TENSION_ST8 = P_TENSION_ST8;
            dbPara.P_TENSION_ST9 = P_TENSION_ST9;
            dbPara.P_TENSION_ST10 = P_TENSION_ST10;

            BEAM_UPDATEBEAMDETAILResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_UPDATEBEAMDETAIL(dbPara);

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

        #region WARP_UPDATESETTINGHEADFLAG

        public bool WARP_UPDATESETTINGHEADFLAG(string P_WARPHEADNO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_UPDATESETTINGHEADParameter dbPara = new WARP_UPDATESETTINGHEADParameter();

            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_FLAG= "0";


            WARP_UPDATESETTINGHEADResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_UPDATESETTINGHEAD(dbPara);

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

        #region BEAM_UPDATEBEAMDETAIL_REMARK

        public bool BEAM_UPDATEBEAMDETAIL_REMARK(string P_BEAMERNO, string P_BEAMLOT, string P_REMARK)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_BEAMERNO))
                return result;

            if (!HasConnection())
                return result;

            BEAM_UPDATEBEAMDETAILParameter dbPara = new BEAM_UPDATEBEAMDETAILParameter();
            dbPara.P_BEAMERNO = P_BEAMERNO;
            dbPara.P_BEAMLOT = P_BEAMLOT;
            dbPara.P_REMARK = P_REMARK;

            BEAM_UPDATEBEAMDETAILResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_UPDATEBEAMDETAIL(dbPara);

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

        #region BEAM_INSERTBEAMERROLLSETTING

        public bool BEAM_INSERTBEAMERROLLSETTING(string P_BEAMERNO, string P_WARPERHEADNO, string P_WARPLOT)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_BEAMERNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_WARPERHEADNO))
                return result;

            if (!HasConnection())
                return result;

            BEAM_INSERTBEAMERROLLSETTINGParameter dbPara = new BEAM_INSERTBEAMERROLLSETTINGParameter();

            dbPara.P_BEAMERNO = P_BEAMERNO;
            dbPara.P_WARPERHEADNO = P_WARPERHEADNO;
            dbPara.P_WARPLOT = P_WARPLOT;

            BEAM_INSERTBEAMERROLLSETTINGResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_INSERTBEAMERROLLSETTING(dbPara);

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

        #region BEAM_EDITNOBEAM

        public string BEAM_EDITNOBEAM(string P_BEAMROLL, string P_OLDNO, string P_NEWNO, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMROLL))
                return result;

            if (!HasConnection())
                return result;

            BEAM_EDITNOBEAMParameter dbPara = new BEAM_EDITNOBEAMParameter();
            dbPara.P_BEAMROLL = P_BEAMROLL;
            dbPara.P_OLDNO = P_OLDNO;
            dbPara.P_NEWNO = P_NEWNO;
            dbPara.P_OPERATOR = P_OPERATOR;

            BEAM_EDITNOBEAMResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_EDITNOBEAM(dbPara);

                result = dbResult.R_RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region BEAM_EDITBEAMERMC

        public string BEAM_EDITBEAMERMC(string P_BEAMERNO, string P_BEAMMC, string P_NEWBEAMMC, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_BEAMERNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_BEAMMC))
                return result;

            if (!HasConnection())
                return result;

            BEAM_EDITBEAMERMCParameter dbPara = new BEAM_EDITBEAMERMCParameter();
            dbPara.P_BEAMERNO = P_BEAMERNO;
            dbPara.P_BEAMMC = P_BEAMMC;
            dbPara.P_NEWBEAMMC = P_NEWBEAMMC;
            dbPara.P_OPERATOR = P_OPERATOR;

            BEAM_EDITBEAMERMCResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.BEAM_EDITBEAMERMC(dbPara);

                result = dbResult.RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #endregion
    }

    #endregion
}








