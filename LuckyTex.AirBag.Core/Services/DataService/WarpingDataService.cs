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
    /// The data service for User and Warping.
    /// </summary>
    public class WarpingDataService : BaseDataService
    {
        #region Singelton

        private static WarpingDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static WarpingDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(WarpingDataService))
                    {
                        _instance = new WarpingDataService();
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
        private WarpingDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~WarpingDataService()
        {
        }

        #endregion

        #region Public Methods

        #region GetMachinesData
        // เพิ่มเพื่อเรียก MC อย่างเดียว
        /// <summary>
        /// Gets all Inspection Machines.
        /// </summary>
        /// <returns>Returns inspection machine.</returns>
        public List<WarpingMCItem> GetMachinesData()
        {
            List<WarpingMCItem> results = new List<WarpingMCItem>();

            // Inspection Process ID = 8
            List<GETMACHINELISTBYPROCESSIDResult> dbResults = this.GetMachines(2);
            if (null == dbResults || dbResults.Count <= 0)
                return results;

            int mcNo = 1;
            WarpingMCItem inst = null;
            foreach (GETMACHINELISTBYPROCESSIDResult dbResult in dbResults)
            {
                inst = new WarpingMCItem();

                inst.MCNo = mcNo;
                inst.MCId = dbResult.MACHINEID;
                inst.DisplayName = dbResult.MCNAME.TrimEnd();

                results.Add(inst);

                ++mcNo;
            }

            return results;
        }
        #endregion

        #region เพิ่มใหม่ WEAV_GETITEMWEAVINGLIST ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WEAV_GETITEMWEAVINGLIST> WEAV_GETITEMWEAVINGLIST()
        {
            List<WEAV_GETITEMWEAVINGLIST> results = null;

            if (!HasConnection())
                return results;

            WEAV_GETITEMWEAVINGLISTParameter dbPara = new WEAV_GETITEMWEAVINGLISTParameter();

            List<WEAV_GETITEMWEAVINGLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WEAV_GETITEMWEAVINGLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<WEAV_GETITEMWEAVINGLIST>();
                    foreach (WEAV_GETITEMWEAVINGLISTResult dbResult in dbResults)
                    {
                        WEAV_GETITEMWEAVINGLIST inst = new WEAV_GETITEMWEAVINGLIST();

                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;

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

        #region เพิ่มใหม่ ITM_GETITEMPREPARELIST ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ITM_GETITEMPREPARELIST> ITM_GETITEMPREPARELIST()
        {
            List<ITM_GETITEMPREPARELIST> results = null;

            if (!HasConnection())
                return results;

            ITM_GETITEMPREPARELISTParameter dbPara = new ITM_GETITEMPREPARELISTParameter();

            List<ITM_GETITEMPREPARELISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.ITM_GETITEMPREPARELIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<ITM_GETITEMPREPARELIST>();
                    foreach (ITM_GETITEMPREPARELISTResult dbResult in dbResults)
                    {
                        ITM_GETITEMPREPARELIST inst = new ITM_GETITEMPREPARELIST();

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

        #region เพิ่มใหม่ ITM_GETITEMYARNLIST ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ITM_GETITEMYARNLIST> ITM_GETITEMYARNLIST()
        {
            List<ITM_GETITEMYARNLIST> results = null;

            if (!HasConnection())
                return results;

            ITM_GETITEMYARNLISTParameter dbPara = new ITM_GETITEMYARNLISTParameter();

            List<ITM_GETITEMYARNLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.ITM_GETITEMYARNLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<ITM_GETITEMYARNLIST>();
                    foreach (ITM_GETITEMYARNLISTResult dbResult in dbResults)
                    {
                        ITM_GETITEMYARNLIST inst = new ITM_GETITEMYARNLIST();

                        inst.ITM_YARN = dbResult.ITM_YARN;

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

        #region เพิ่มใหม่ WARP_GETSPECBYCHOPNOANDMC ใช้ในการ Load Warping

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WARP_GETSPECBYCHOPNOANDMC> WARP_GETSPECBYCHOPNOANDMC(string P_ITMPREPARE, string P_MCNO)
        {
            List<WARP_GETSPECBYCHOPNOANDMC> results = null;

            if (!HasConnection())
                return results;

            WARP_GETSPECBYCHOPNOANDMCParameter dbPara = new WARP_GETSPECBYCHOPNOANDMCParameter();
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_MCNO = P_MCNO;
           
            List<WARP_GETSPECBYCHOPNOANDMCResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETSPECBYCHOPNOANDMC(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_GETSPECBYCHOPNOANDMC>();
                    foreach (WARP_GETSPECBYCHOPNOANDMCResult dbResult in dbResults)
                    {
                        WARP_GETSPECBYCHOPNOANDMC inst = new WARP_GETSPECBYCHOPNOANDMC();

                        inst.CHOPNO = dbResult.CHOPNO;
                        inst.ITM_YARN = dbResult.ITM_YARN;
                        inst.WARPERENDS = dbResult.WARPERENDS;
                        inst.MAXLENGTH = dbResult.MAXLENGTH;
                        inst.MINLENGTH = dbResult.MINLENGTH;
                        inst.WAXING = dbResult.WAXING;
                        inst.COMBTYPE = dbResult.COMBTYPE;
                        inst.COMBPITCH = dbResult.COMBPITCH;
                        inst.KEBAYARN = dbResult.KEBAYARN;
                        inst.NOWARPBEAM = dbResult.NOWARPBEAM;
                        inst.MAXHARDNESS = dbResult.MAXHARDNESS;
                        inst.MINHARDNESS = dbResult.MINHARDNESS;
                        inst.MCNO = dbResult.MCNO;
                        inst.SPEED = dbResult.SPEED;
                        inst.SPEED_MARGIN = dbResult.SPEED_MARGIN;
                        inst.YARN_TENSION = dbResult.YARN_TENSION;
                        inst.YARN_TENSION_MARGIN = dbResult.YARN_TENSION_MARGIN;
                        inst.WINDING_TENSION = dbResult.WINDING_TENSION;
                        inst.WINDING_TENSION_MARGIN = dbResult.WINDING_TENSION_MARGIN;
                        inst.NOCH = dbResult.NOCH;

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

        #region เพิ่มใหม่ WARP_PALLETLISTBYITMYARN 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WARP_PALLETLISTBYITMYARN> WARP_PALLETLISTBYITMYARN(string P_ITEM_YARN, string P_WARPHEADNO)
        {
            List<WARP_PALLETLISTBYITMYARN> results = null;

            if (!HasConnection())
                return results;

            WARP_PALLETLISTBYITMYARNParameter dbPara = new WARP_PALLETLISTBYITMYARNParameter();
            dbPara.P_ITEM_YARN = P_ITEM_YARN;
            dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<WARP_PALLETLISTBYITMYARNResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_PALLETLISTBYITMYARN(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_PALLETLISTBYITMYARN>();

                    decimal RECEIVECH = 0;
                    decimal USEDCH = 0;
                    decimal REJECTCH = 0;

                    foreach (WARP_PALLETLISTBYITMYARNResult dbResult in dbResults)
                    {
                        WARP_PALLETLISTBYITMYARN inst = new WARP_PALLETLISTBYITMYARN();

                        inst.IsSelect = false;
                        inst.ITM_YARN = dbResult.ITM_YARN;
                        inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.RECEIVEWEIGHT = dbResult.RECEIVEWEIGHT;
                        
                        inst.USEDWEIGHT = dbResult.USEDWEIGHT;
                        
                        inst.VERIFY = dbResult.VERIFY;
                        inst.REJECTID = dbResult.REJECTID;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.RETURNFLAG = dbResult.RETURNFLAG;
                        
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.CREATEBY = dbResult.CREATEBY;

                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.REMARK = dbResult.REMARK;
                        inst.CLEARDATE = dbResult.CLEARDATE;

                        RECEIVECH = 0;
                        USEDCH = 0;
                        REJECTCH = 0;

                        if (dbResult.RECEIVECH != null)
                        {
                            RECEIVECH = dbResult.RECEIVECH.Value;
                            inst.RECEIVECH = RECEIVECH;
                        }
                        else
                            inst.RECEIVECH = 0;

                        if (dbResult.USEDCH != null)
                        {
                            USEDCH = dbResult.USEDCH.Value;
                            inst.USEDCH = USEDCH;
                        }
                        else
                            inst.USEDCH = 0;

                        if (dbResult.REJECTCH != null)
                        {
                            REJECTCH = dbResult.REJECTCH.Value;
                            inst.REJECTCH = REJECTCH;
                        }
                        else
                            inst.REJECTCH = 0;

                        inst.NoCH = (RECEIVECH - dbResult.USEDCH - dbResult.REJECTCH);
                        inst.Use = inst.NoCH;
                        inst.Reject = 0;
                        inst.Remain = (inst.NoCH - inst.Use - inst.Reject);

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

        #region เพิ่มใหม่ Warp_GetWarperMCStatusSideA ใช้ในการ Load Warping

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WARP_GETWARPERMCSTATUS> Warp_GetWarperMCStatusSideA(string P_MCNO)
        {
            List<WARP_GETWARPERMCSTATUS> results = null;

            if (!HasConnection())
                return results;

            WARP_GETWARPERMCSTATUSParameter dbPara = new WARP_GETWARPERMCSTATUSParameter();
            dbPara.P_MCNO = P_MCNO;

            List<WARP_GETWARPERMCSTATUSResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETWARPERMCSTATUS(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_GETWARPERMCSTATUS>();
                    foreach (WARP_GETWARPERMCSTATUSResult dbResult in dbResults)
                    {
                        WARP_GETWARPERMCSTATUS inst = new WARP_GETWARPERMCSTATUS();

                        inst.SIDE = dbResult.SIDE;

                        if (inst.SIDE == "A")
                        {
                            inst.WARPHEADNO = dbResult.WARPHEADNO;
                            inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                            inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                            inst.WARPMC = dbResult.WARPMC;

                            inst.ACTUALCH = dbResult.ACTUALCH;
                            inst.WTYPE = dbResult.WTYPE;
                            inst.STARTDATE = dbResult.STARTDATE;
                            inst.CREATEBY = dbResult.CREATEBY;
                            inst.CONDITIONSTART = dbResult.CONDITIONSTART;
                            inst.CONDITIONBY = dbResult.CONDITIONBY;
                            inst.ENDDATE = dbResult.ENDDATE;
                            inst.STATUS = dbResult.STATUS;
                            inst.FINISHBY = dbResult.FINISHBY;
                            inst.CONDITIONING = dbResult.CONDITIONING;

                            results.Add(inst);
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

        #region เพิ่มใหม่ Warp_GetWarperMCStatusSideB ใช้ในการ Load Warping

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WARP_GETWARPERMCSTATUS> Warp_GetWarperMCStatusSideB(string P_MCNO)
        {
            List<WARP_GETWARPERMCSTATUS> results = null;

            if (!HasConnection())
                return results;

            WARP_GETWARPERMCSTATUSParameter dbPara = new WARP_GETWARPERMCSTATUSParameter();
            dbPara.P_MCNO = P_MCNO;

            List<WARP_GETWARPERMCSTATUSResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETWARPERMCSTATUS(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_GETWARPERMCSTATUS>();
                    foreach (WARP_GETWARPERMCSTATUSResult dbResult in dbResults)
                    {
                        WARP_GETWARPERMCSTATUS inst = new WARP_GETWARPERMCSTATUS();

                        inst.SIDE = dbResult.SIDE;

                        if (inst.SIDE == "B")
                        {
                            inst.WARPHEADNO = dbResult.WARPHEADNO;
                            inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                            inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                            inst.WARPMC = dbResult.WARPMC;

                            inst.ACTUALCH = dbResult.ACTUALCH;
                            inst.WTYPE = dbResult.WTYPE;
                            inst.STARTDATE = dbResult.STARTDATE;
                            inst.CREATEBY = dbResult.CREATEBY;
                            inst.CONDITIONSTART = dbResult.CONDITIONSTART;
                            inst.CONDITIONBY = dbResult.CONDITIONBY;
                            inst.ENDDATE = dbResult.ENDDATE;
                            inst.STATUS = dbResult.STATUS;
                            inst.FINISHBY = dbResult.FINISHBY;
                            inst.CONDITIONING = dbResult.CONDITIONING;

                            results.Add(inst);
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

        #region เพิ่มใหม่ WARP_GETWARPERLOTBYHEADNO ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
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

                        inst.REMARK = dbResult.REMARK;
                        inst.TENSION_IT = dbResult.TENSION_IT;
                        inst.TENSION_TAKEUP = dbResult.TENSION_TAKEUP;
                        inst.MC_COUNT_L = dbResult.MC_COUNT_L;
                        inst.MC_COUNT_S = dbResult.MC_COUNT_S;

                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.KEBA = dbResult.KEBA;
                        inst.TIGHTEND = dbResult.TIGHTEND;
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

        #region เพิ่มใหม่ WARP_TRANFERSLIP ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<WARP_TRANFERSLIP> WARP_TRANFERSLIP(string P_WARPHEADNO, string P_WARPLOT)
        {
            List<WARP_TRANFERSLIP> results = null;

            if (!HasConnection())
                return results;

            WARP_TRANFERSLIPParameter dbPara = new WARP_TRANFERSLIPParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPLOT = P_WARPLOT;

            List<WARP_TRANFERSLIPResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_TRANFERSLIP(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_TRANFERSLIP>();
                    foreach (WARP_TRANFERSLIPResult dbResult in dbResults)
                    {
                        WARP_TRANFERSLIP inst = new WARP_TRANFERSLIP();

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

                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.ITM_YARN = dbResult.ITM_YARN;

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

        #region เพิ่มใหม่ WARP_GETREMAINPALLET ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<WARP_GETREMAINPALLET> WARP_GETREMAINPALLET(string P_ITEM_YARN)
        {
            List<WARP_GETREMAINPALLET> results = null;

            if (!HasConnection())
                return results;

            WARP_GETREMAINPALLETParameter dbPara = new WARP_GETREMAINPALLETParameter();
            dbPara.P_ITEM_YARN = P_ITEM_YARN;

            List<WARP_GETREMAINPALLETResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETREMAINPALLET(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_GETREMAINPALLET>();
                    foreach (WARP_GETREMAINPALLETResult dbResult in dbResults)
                    {
                        WARP_GETREMAINPALLET inst = new WARP_GETREMAINPALLET();

                        inst.ITM_YARN = dbResult.ITM_YARN;
                        inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.RECEIVEWEIGHT = dbResult.RECEIVEWEIGHT;
                        inst.RECEIVECH = dbResult.RECEIVECH;
                        inst.USEDWEIGHT = dbResult.USEDWEIGHT;
                        inst.USEDCH = dbResult.USEDCH;
                        inst.VERIFY = dbResult.VERIFY;
                        inst.REJECTID = dbResult.REJECTID;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.RETURNFLAG = dbResult.RETURNFLAG;
                        inst.REJECTCH = dbResult.REJECTCH;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.REMARK = dbResult.REMARK;
                        inst.CLEARDATE = dbResult.CLEARDATE;
                        inst.REMAINCH = dbResult.REMAINCH;

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

        #region เพิ่มใหม่ WARP_GETSTOPREASONBYWARPERLOT ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <param name="P_WARPLOT"></param>
        /// <returns></returns>
        public List<WARP_GETSTOPREASONBYWARPERLOT> WARP_GETSTOPREASONBYWARPERLOT(string P_WARPHEADNO, string P_WARPLOT)
        {
            List<WARP_GETSTOPREASONBYWARPERLOT> results = null;

            if (!HasConnection())
                return results;

            WARP_GETSTOPREASONBYWARPERLOTParameter dbPara = new WARP_GETSTOPREASONBYWARPERLOTParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPLOT = P_WARPLOT;

            List<WARP_GETSTOPREASONBYWARPERLOTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETSTOPREASONBYWARPERLOT(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_GETSTOPREASONBYWARPERLOT>();
                    foreach (WARP_GETSTOPREASONBYWARPERLOTResult dbResult in dbResults)
                    {
                        WARP_GETSTOPREASONBYWARPERLOT inst = new WARP_GETSTOPREASONBYWARPERLOT();

                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.WARPERLOT = dbResult.WARPERLOT;
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

        #region เพิ่มใหม่ WARP_GETINPROCESSLOTBYHEADNO ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<WARP_GETINPROCESSLOTBYHEADNO> WARP_GETINPROCESSLOTBYHEADNO(string P_WARPHEADNO)
        {
            List<WARP_GETINPROCESSLOTBYHEADNO> results = null;

            if (!HasConnection())
                return results;

            WARP_GETINPROCESSLOTBYHEADNOParameter dbPara = new WARP_GETINPROCESSLOTBYHEADNOParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;

            List<WARP_GETINPROCESSLOTBYHEADNOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETINPROCESSLOTBYHEADNO(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_GETINPROCESSLOTBYHEADNO>();
                    foreach (WARP_GETINPROCESSLOTBYHEADNOResult dbResult in dbResults)
                    {
                        WARP_GETINPROCESSLOTBYHEADNO inst = new WARP_GETINPROCESSLOTBYHEADNO();

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

        #region เพิ่มใหม่ WARP_GETCREELSETUPSTATUS ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<WARP_GETCREELSETUPSTATUS> WARP_GETCREELSETUPSTATUS(string P_MCNO, string P_SIDE)
        {
            List<WARP_GETCREELSETUPSTATUS> results = null;

            if (!HasConnection())
                return results;

            WARP_GETCREELSETUPSTATUSParameter dbPara = new WARP_GETCREELSETUPSTATUSParameter();
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_SIDE = P_SIDE;

            List<WARP_GETCREELSETUPSTATUSResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETCREELSETUPSTATUS(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_GETCREELSETUPSTATUS>();
                    foreach (WARP_GETCREELSETUPSTATUSResult dbResult in dbResults)
                    {
                        WARP_GETCREELSETUPSTATUS inst = new WARP_GETCREELSETUPSTATUS();

                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.WARPMC = dbResult.WARPMC;
                        inst.SIDE = dbResult.SIDE;
                        inst.ACTUALCH = dbResult.ACTUALCH;
                        inst.WTYPE = dbResult.WTYPE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CONDITIONSTART = dbResult.CONDITIONSTART;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.STATUS = dbResult.STATUS;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.REEDNO = dbResult.REEDNO;
                        inst.EDITBY = dbResult.EDITBY;
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

        #region เพิ่มใหม่ GETCREELSETUPSTATUS ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<WARP_GETCREELSETUPSTATUS> GETCREELSETUPSTATUS()
        {
            List<WARP_GETCREELSETUPSTATUS> results = null;

            if (!HasConnection())
                return results;

            WARP_GETCREELSETUPSTATUSParameter dbPara = new WARP_GETCREELSETUPSTATUSParameter();

            List<WARP_GETCREELSETUPSTATUSResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETCREELSETUPSTATUS(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_GETCREELSETUPSTATUS>();
                    foreach (WARP_GETCREELSETUPSTATUSResult dbResult in dbResults)
                    {
                        WARP_GETCREELSETUPSTATUS inst = new WARP_GETCREELSETUPSTATUS();

                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.WARPMC = dbResult.WARPMC;
                        inst.SIDE = dbResult.SIDE;
                        inst.ACTUALCH = dbResult.ACTUALCH;
                        inst.WTYPE = dbResult.WTYPE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CONDITIONSTART = dbResult.CONDITIONSTART;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.ENDDATE = dbResult.ENDDATE;

                        inst.STATUS = dbResult.STATUS;

                        if (inst.STATUS == "S")
                            inst.strSTATUS = "Processing";
                        else if (inst.STATUS == "C")
                            inst.strSTATUS = "Conditioning";
                        else
                            inst.strSTATUS = inst.STATUS;

                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.REEDNO = dbResult.REEDNO;
                        inst.EDITBY = dbResult.EDITBY;
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

        #region เพิ่มใหม่ WARP_GETCREELSETUPDETAIL ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <returns></returns>
        public List<WARP_GETCREELSETUPDETAIL> WARP_GETCREELSETUPDETAIL(string P_WARPHEADNO)
        {
            List<WARP_GETCREELSETUPDETAIL> results = null;

            if (!HasConnection())
                return results;

            WARP_GETCREELSETUPDETAILParameter dbPara = new WARP_GETCREELSETUPDETAILParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            
            List<WARP_GETCREELSETUPDETAILResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETCREELSETUPDETAIL(dbPara);
                if (null != dbResults)
                {
                    decimal RECEIVECH = 0;
                    decimal USEDCH = 0;
                    decimal REJECTCH = 0;

                    results = new List<WARP_GETCREELSETUPDETAIL>();
                    foreach (WARP_GETCREELSETUPDETAILResult dbResult in dbResults)
                    {
                        WARP_GETCREELSETUPDETAIL inst = new WARP_GETCREELSETUPDETAIL();

                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.RECEIVECH = dbResult.RECEIVECH;
                        inst.USEDCH = dbResult.USEDCH;
                        inst.REJECTCH = dbResult.REJECTCH;
                        inst.PREJECT = dbResult.PREJECT;
                        inst.ITM_YARN = dbResult.ITM_YARN;
                        inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                        inst.PUSED = dbResult.PUSED;

                        RECEIVECH = 0;
                        USEDCH = 0;
                        REJECTCH = 0;

                        if (dbResult.RECEIVECH != null)
                        {
                            RECEIVECH = dbResult.RECEIVECH.Value;
                            inst.RECEIVECH = RECEIVECH;
                        }
                        else
                            inst.RECEIVECH = 0;

                        if (dbResult.USEDCH != null)
                        {
                            USEDCH = dbResult.USEDCH.Value;
                            inst.USEDCH = USEDCH;
                        }
                        else
                            inst.USEDCH = 0;

                        if (dbResult.REJECTCH != null)
                        {
                            REJECTCH = dbResult.REJECTCH.Value;
                            inst.REJECTCH = REJECTCH;
                        }
                        else
                            inst.REJECTCH = 0;

                        inst.NoCH = (RECEIVECH - dbResult.USEDCH - dbResult.REJECTCH);

                        if (inst.PUSED != null)
                            inst.Use = inst.PUSED;
                        else
                            inst.Use = (dbResult.USEDCH - dbResult.PREJECT);

                        inst.Reject = dbResult.PREJECT;
                        inst.Remain = (inst.NoCH - inst.Use - inst.Reject);


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

        #region เพิ่มใหม่ WARP_SEARCHWARPRECORD ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <param name="P_WARPMC"></param>
        /// <param name="P_ITMPREPARE"></param>
        /// <param name="P_STARTDATE"></param>
        /// <returns></returns>
        public List<WARP_SEARCHWARPRECORD> WARP_SEARCHWARPRECORD(string P_WARPHEADNO, string P_WARPMC, string P_ITMPREPARE, string P_STARTDATE)
        {
            List<WARP_SEARCHWARPRECORD> results = null;

            if (!HasConnection())
                return results;

            WARP_SEARCHWARPRECORDParameter dbPara = new WARP_SEARCHWARPRECORDParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPMC = P_WARPMC;
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_STARTDATE = P_STARTDATE;

            List<WARP_SEARCHWARPRECORDResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_SEARCHWARPRECORD(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_SEARCHWARPRECORD>();
                    foreach (WARP_SEARCHWARPRECORDResult dbResult in dbResults)
                    {
                        WARP_SEARCHWARPRECORD inst = new WARP_SEARCHWARPRECORD();

                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.WARPMC = dbResult.WARPMC;
                        inst.SIDE = dbResult.SIDE;
                        inst.ACTUALCH = dbResult.ACTUALCH;
                        inst.WTYPE = dbResult.WTYPE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CONDITIONSTART = dbResult.CONDITIONSTART;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.STATUS = dbResult.STATUS;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.REEDNO = dbResult.REEDNO;
                        inst.EDITBY = dbResult.EDITBY;
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

        #region เพิ่มใหม่ WarpingRecordHead ใช้ในการ Load Warping
      
        public WarpingRecordHead WarpingRecordHead(string P_WARPHEADNO, string P_SIDE,string P_WARPMC, string P_ITM_PREPARE ,
            string P_WTYPE, string P_CONDITIONBY, string P_REEDNO, DateTime? P_CONDITIONSTART)
        {
            WarpingRecordHead results = new WarpingRecordHead();

            try
            {
                results.WARPHEADNO = P_WARPHEADNO;
                results.SIDE = P_SIDE;
                results.WARPMC = P_WARPMC;
                results.ITM_PREPARE = P_ITM_PREPARE;

                if (!string.IsNullOrEmpty(P_WTYPE))
                {
                    if (P_WTYPE == "N")
                        results.WTYPE = "No Wax";
                    else if (P_WTYPE == "Y")
                        results.WTYPE = "Waxing";
                }

                results.CONDITIONBY = P_CONDITIONBY;
                results.REEDNO = P_REEDNO;
                results.CONDITIONSTART = P_CONDITIONSTART;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ WARP_GETWARPERROLLDETAIL ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPERROLL"></param>
        /// <returns></returns>
        public List<WARP_GETWARPERROLLDETAIL> WARP_GETWARPERROLLDETAIL(string P_WARPERROLL)
        {
            List<WARP_GETWARPERROLLDETAIL> results = null;

            if (!HasConnection())
                return results;

            WARP_GETWARPERROLLDETAILParameter dbPara = new WARP_GETWARPERROLLDETAILParameter();
            dbPara.P_WARPERROLL = P_WARPERROLL;

            List<WARP_GETWARPERROLLDETAILResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_GETWARPERROLLDETAIL(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_GETWARPERROLLDETAIL>();
                    foreach (WARP_GETWARPERROLLDETAILResult dbResult in dbResults)
                    {
                        WARP_GETWARPERROLLDETAIL inst = new WARP_GETWARPERROLLDETAIL();

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
                        inst.REMARK = dbResult.REMARK;
                        inst.TENSION_IT = dbResult.TENSION_IT;
                        inst.TENSION_TAKEUP = dbResult.TENSION_TAKEUP;
                        inst.MC_COUNT_L = dbResult.MC_COUNT_L;
                        inst.MC_COUNT_S = dbResult.MC_COUNT_S;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.EDITBY = dbResult.EDITBY;

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

        #region เพิ่มใหม่ WARP_CHECKPALLET ใช้ในการ Load Warping
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <returns></returns>
        public List<WARP_CHECKPALLET> WARP_CHECKPALLET(string P_PALLETNO)
        {
            List<WARP_CHECKPALLET> results = null;

            if (!HasConnection())
                return results;

            WARP_CHECKPALLETParameter dbPara = new WARP_CHECKPALLETParameter();
            dbPara.P_PALLETNO = P_PALLETNO;

            List<WARP_CHECKPALLETResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_CHECKPALLET(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_CHECKPALLET>();
                    foreach (WARP_CHECKPALLETResult dbResult in dbResults)
                    {
                        WARP_CHECKPALLET inst = new WARP_CHECKPALLET();

                        inst.ITM_YARN = dbResult.ITM_YARN;
                        inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                        inst.PALLETNO = dbResult.PALLETNO;
                        inst.RECEIVEWEIGHT = dbResult.RECEIVEWEIGHT;
                        inst.RECEIVECH = dbResult.RECEIVECH;
                        inst.USEDWEIGHT = dbResult.USEDWEIGHT;
                        inst.USEDCH = dbResult.USEDCH;
                        inst.VERIFY = dbResult.VERIFY;
                        inst.REJECTID = dbResult.REJECTID;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.RETURNFLAG = dbResult.RETURNFLAG;
                        inst.REJECTCH = dbResult.REJECTCH;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.REMARK = dbResult.REMARK;
                        inst.CLEARDATE = dbResult.CLEARDATE;
                        inst.KGPERCH = dbResult.KGPERCH;

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

        //เพิ่มใหม่ 25/08/17
        #region เพิ่มใหม่ Warp_warplist  ใช้ในการ Load warplist
       /// <summary>
       /// 
       /// </summary>
       /// <param name="P_WARPHEADNO"></param>
       /// <param name="P_WARPMC"></param>
       /// <param name="P_ITMPREPARE"></param>
       /// <param name="P_STARTDATE"></param>
       /// <param name="P_ENDDATE"></param>
       /// <returns></returns>
        public List<WARP_WARPLIST> WARP_WARPLIST(string P_WARPHEADNO, string P_WARPMC, string P_ITMPREPARE, string P_STARTDATE, string P_ENDDATE)
        {
            List<WARP_WARPLIST> results = null;

            if (!HasConnection())
                return results;

            WARP_WARPLISTParameter dbPara = new WARP_WARPLISTParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPMC = P_WARPMC;
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_ENDDATE = P_ENDDATE;

            List<WARP_WARPLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.WARP_WARPLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<WARP_WARPLIST>();
                    foreach (WARP_WARPLISTResult dbResult in dbResults)
                    {
                        WARP_WARPLIST inst = new WARP_WARPLIST();

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
                        inst.REMARK = dbResult.REMARK;
                        inst.TENSION_IT = dbResult.TENSION_IT;
                        inst.TENSION_TAKEUP = dbResult.TENSION_TAKEUP;
                        inst.MC_COUNT_L = dbResult.MC_COUNT_L;
                        inst.MC_COUNT_S = dbResult.MC_COUNT_S;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.ITM_YARN = dbResult.ITM_YARN;

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

        #region WARP_GETWARPERROLLREMARK

        public string WARP_GETWARPERROLLREMARK(string P_WARPLOT)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_WARPLOT))
                return result;

            if (!HasConnection())
                return result;

            WARP_GETWARPERROLLREMARKParameter dbPara = new WARP_GETWARPERROLLREMARKParameter();
            dbPara.P_WARPLOT = P_WARPLOT;


            WARP_GETWARPERROLLREMARKResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_GETWARPERROLLREMARK(dbPara);

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

        #region WARP_RECEIVEPALLET

        public bool WARP_RECEIVEPALLET(string P_ITMYARN, DateTime? P_RECEIVEDATE, string P_PALLETNO, decimal? P_WEIGHT, decimal? P_CH
            ,string P_VERIFY ,string P_REJECTID ,string P_OPERATOR )
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_PALLETNO))
                return result;
            if (string.IsNullOrWhiteSpace(P_ITMYARN))
                return result;

            if (!HasConnection())
                return result;

            WARP_RECEIVEPALLETParameter dbPara = new WARP_RECEIVEPALLETParameter();
            dbPara.P_ITMYARN = P_ITMYARN;
            dbPara.P_RECEIVEDATE = P_RECEIVEDATE;
            dbPara.P_PALLETNO = P_PALLETNO;
            dbPara.P_WEIGHT = P_WEIGHT;
            dbPara.P_CH = P_CH;
            dbPara.P_VERIFY = P_VERIFY;
            dbPara.P_REJECTID = P_REJECTID;
            dbPara.P_OPERATOR = P_OPERATOR;

            WARP_RECEIVEPALLETResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_RECEIVEPALLET(dbPara);

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

        #region WARP_UPDATEPALLET

        public bool WARP_UPDATEPALLET(DateTime? P_RECEIVEDATE, string P_PALLETNO,
        decimal? P_USEDCH, decimal? P_REJECTCH, decimal? P_REMAINCH, string P_WARPHEADNO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_PALLETNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_UPDATEPALLETParameter dbPara = new WARP_UPDATEPALLETParameter();
            dbPara.P_RECEIVEDATE = P_RECEIVEDATE;
            dbPara.P_PALLETNO = P_PALLETNO;
            dbPara.P_USEDCH = P_USEDCH;
            dbPara.P_REJECTCH = P_REJECTCH;
            dbPara.P_REMAINCH = P_REMAINCH;
            dbPara.P_WARPHEADNO = P_WARPHEADNO;

            WARP_UPDATEPALLETResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_UPDATEPALLET(dbPara);

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

        #region WARP_INSERTSETTINGHEAD

        public string WARP_INSERTSETTINGHEAD(string P_ITMPREPARE, string P_PRODUCTID, string P_MCNO, string P_SIDE, decimal? P_ACTUALCH, string P_WTYPE, string P_OPERATOR, string P_WARPERHEADNO, string P_REEDNO)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMPREPARE))
                return result;

            if (!HasConnection())
                return result;

            WARP_INSERTSETTINGHEADParameter dbPara = new WARP_INSERTSETTINGHEADParameter();
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_PRODUCTID = P_PRODUCTID;
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_SIDE = P_SIDE;
            dbPara.P_ACTUALCH = P_ACTUALCH;
            dbPara.P_WTYPE = P_WTYPE;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_WARPERHEADNO = P_WARPERHEADNO;
            dbPara.P_REEDNO = P_REEDNO;

            WARP_INSERTSETTINGHEADResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_INSERTSETTINGHEAD(dbPara);

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

        #region WARP_INSERTSETTINGDETAIL

        public bool WARP_INSERTSETTINGDETAIL(string P_WARPHEADNO, string P_PALLETNO, decimal? P_USED, decimal? P_REJECTCH)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_INSERTSETTINGDETAILParameter dbPara = new WARP_INSERTSETTINGDETAILParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_PALLETNO = P_PALLETNO;
            dbPara.P_USED = P_USED;
            dbPara.P_REJECTCH = P_REJECTCH;
         
            WARP_INSERTSETTINGDETAILResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_INSERTSETTINGDETAIL(dbPara);

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

        #region WARP_UPDATESETTINGHEAD_MCStatus

        public bool WARP_UPDATESETTINGHEAD_MCStatus(string P_WARPHEADNO, DateTime? P_STARTDATE,
        string P_CONDITONBY, string P_STATUS)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_UPDATESETTINGHEADParameter dbPara = new WARP_UPDATESETTINGHEADParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_CONDITONBY = P_CONDITONBY;
            dbPara.P_STATUS = P_STATUS;

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

        #region WARP_UPDATESETTINGHEAD
       
        public bool WARP_UPDATESETTINGHEAD(string P_WARPHEADNO, DateTime? P_ENDDATE,
        string P_STATUS, string P_FINISHBY)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_UPDATESETTINGHEADParameter dbPara = new WARP_UPDATESETTINGHEADParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_ENDDATE = P_ENDDATE;
            dbPara.P_STATUS = P_STATUS;
            dbPara.P_FINISHBY = P_FINISHBY;

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

        #region WARP_INSERTWARPINGPROCESS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <param name="P_WARPMC"></param>
        /// <param name="P_BEAMNO"></param>
        /// <param name="P_SIDE"></param>
        /// <param name="P_STARTDATE"></param>
        /// <param name="P_STARTBY"></param>
        /// <returns></returns>
        public WARP_INSERTWARPINGPROCESS WARP_INSERTWARPINGPROCESS(string P_WARPHEADNO, string P_WARPMC, string P_BEAMNO, string P_SIDE, DateTime? P_STARTDATE, string P_STARTBY)
        {
            WARP_INSERTWARPINGPROCESS results = null;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return results;

            if (!HasConnection())
                return results;

            WARP_INSERTWARPINGPROCESSParameter dbPara = new WARP_INSERTWARPINGPROCESSParameter();

            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPMC = P_WARPMC;
            dbPara.P_BEAMNO = P_BEAMNO;
            dbPara.P_SIDE = P_SIDE;
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_STARTBY = P_STARTBY;

            WARP_INSERTWARPINGPROCESSResult dbResults = null;

            try
            {
                dbResults =
                    DatabaseManager.Instance.WARP_INSERTWARPINGPROCESS(dbPara);

                results = new WARP_INSERTWARPINGPROCESS();

                if (null != dbResults)
                {
                    results.R_WRAPLOT = dbResults.R_WRAPLOT;
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

        #region WARP_UPDATEWARPINGPROCESS
       /// <summary>
       /// 
       /// </summary>
       /// <param name="P_WARPHEADNO"></param>
       /// <param name="P_WARPLOT"></param>
       /// <param name="P_LENGTH"></param>
       /// <param name="P_ENDDATE"></param>
       /// <param name="P_SPEED"></param>
       /// <param name="P_HARDL"></param>
       /// <param name="P_HARDN"></param>
       /// <param name="P_HARDR"></param>
       /// <param name="P_TENSION"></param>
       /// <param name="P_DOFFBY"></param>
       /// <returns></returns>
        public bool WARP_UPDATEWARPINGPROCESS(string P_WARPHEADNO, string P_WARPLOT, decimal? P_LENGTH, DateTime? P_ENDDATE, decimal? P_SPEED,
            decimal? P_HARDL, decimal? P_HARDN, decimal? P_HARDR, decimal? P_TENSION,
            string P_DOFFBY, decimal? P_TENSION_IT, decimal? P_TENSION_TAKE, decimal? P_MCL, decimal? P_MCS)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_UPDATEWARPINGPROCESSParameter dbPara = new WARP_UPDATEWARPINGPROCESSParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPLOT = P_WARPLOT;
            dbPara.P_LENGTH = P_LENGTH;
            dbPara.P_ENDDATE = P_ENDDATE;
            dbPara.P_SPEED = P_SPEED;
            dbPara.P_HARDL = P_HARDL;
            dbPara.P_HARDN = P_HARDN;
            dbPara.P_HARDR = P_HARDR;
            dbPara.P_TENSION = P_TENSION;
            dbPara.P_DOFFBY = P_DOFFBY;
            dbPara.P_TENSION_IT = P_TENSION_IT;
            dbPara.P_TENSION_TAKE = P_TENSION_TAKE;
            dbPara.P_MCL = P_MCL;
            dbPara.P_MCS = P_MCS;

            WARP_UPDATEWARPINGPROCESSResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_UPDATEWARPINGPROCESS(dbPara);

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

        #region  WARP_UPDATEWARPINGPROCESS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <param name="P_WARPLOT"></param>
        /// <param name="P_LENGTH"></param>
        /// <param name="P_SPEED"></param>
        /// <param name="P_HARDL"></param>
        /// <param name="P_HARDN"></param>
        /// <param name="P_HARDR"></param>
        /// <param name="P_TENSION"></param>
        /// <param name="P_TENSION_IT"></param>
        /// <param name="P_TENSION_TAKE"></param>
        /// <param name="P_MCL"></param>
        /// <param name="P_MCS"></param>
        /// <param name="P_OPERATOR"></param>
        /// <param name="P_BEAMNO"></param>
        /// <returns></returns>
        public bool WARP_UPDATEWARPINGPROCESS(string P_WARPHEADNO, string P_WARPLOT
            , decimal? P_LENGTH, decimal? P_SPEED
            , decimal? P_HARDL, decimal? P_HARDN, decimal? P_HARDR
            , decimal? P_TENSION, decimal? P_TENSION_IT, decimal? P_TENSION_TAKE
            , decimal? P_MCL, decimal? P_MCS, string P_OPERATOR, string P_BEAMNO)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_UPDATEWARPINGPROCESSParameter dbPara = new WARP_UPDATEWARPINGPROCESSParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPLOT = P_WARPLOT;
            dbPara.P_LENGTH = P_LENGTH;
            dbPara.P_SPEED = P_SPEED;
            dbPara.P_HARDL = P_HARDL;
            dbPara.P_HARDN = P_HARDN;
            dbPara.P_HARDR = P_HARDR;
            dbPara.P_TENSION = P_TENSION;
            dbPara.P_TENSION_IT = P_TENSION_IT;
            dbPara.P_TENSION_TAKE = P_TENSION_TAKE;
            dbPara.P_MCL = P_MCL;
            dbPara.P_MCS = P_MCS;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_BEAMNO = P_BEAMNO;

            WARP_UPDATEWARPINGPROCESSResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_UPDATEWARPINGPROCESS(dbPara);

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

        #region WARP_INSERTWARPMCSTOP

        public string WARP_INSERTWARPMCSTOP(string P_WARPHEADNO, string P_WARPLOT, string P_REASON, decimal? P_LENGTH, string P_OTHER, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_INSERTWARPMCSTOPParameter dbPara = new WARP_INSERTWARPMCSTOPParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPLOT = P_WARPLOT;
            dbPara.P_REASON = P_REASON;
            dbPara.P_LENGTH = P_LENGTH;
            dbPara.P_OTHER = P_OTHER;
            dbPara.P_OPERATOR = P_OPERATOR;

            WARP_INSERTWARPMCSTOPResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_INSERTWARPMCSTOP(dbPara);

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

        #region WARP_CLEARPALLET
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PALLETNO"></param>
        /// <param name="P_RECEIVEDATE"></param>
        /// <param name="P_OPERATOR"></param>
        /// <param name="P_REMARK"></param>
        /// <returns></returns>
        public bool WARP_CLEARPALLET(string P_PALLETNO, DateTime? P_RECEIVEDATE, string P_OPERATOR, string P_REMARK)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_PALLETNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_CLEARPALLETParameter dbPara = new WARP_CLEARPALLETParameter();
            dbPara.P_PALLETNO = P_PALLETNO;
            dbPara.P_RECEIVEDATE = P_RECEIVEDATE;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_REMARK = P_REMARK;
         
            WARP_CLEARPALLETResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_CLEARPALLET(dbPara);

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

        #region WARP_UPDATEWARPINGPROCESS_REMARK

        public bool WARP_UPDATEWARPINGPROCESS_REMARK(string P_WARPHEADNO, string P_WARPLOT, string P_REMARK)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_UPDATEWARPINGPROCESSParameter dbPara = new WARP_UPDATEWARPINGPROCESSParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPLOT = P_WARPLOT;
            dbPara.P_REMARK = P_REMARK;

            WARP_UPDATEWARPINGPROCESSResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_UPDATEWARPINGPROCESS(dbPara);

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

        #region WARP_EDITWARPERMCSETUP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <param name="P_WARPMC"></param>
        /// <param name="P_SIDE"></param>
        /// <param name="P_NEWWARPMC"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public string WARP_EDITWARPERMCSETUP(string P_WARPHEADNO, string P_WARPMC, string P_SIDE, string P_NEWWARPMC, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return result;

            if (string.IsNullOrWhiteSpace(P_NEWWARPMC))
                return result;

            if (!HasConnection())
                return result;

            WARP_EDITWARPERMCSETUPParameter dbPara = new WARP_EDITWARPERMCSETUPParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPMC = P_WARPMC;
            dbPara.P_SIDE = P_SIDE;
            dbPara.P_NEWWARPMC = P_NEWWARPMC;
            dbPara.P_OPERATOR = P_OPERATOR;

            WARP_EDITWARPERMCSETUPResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_EDITWARPERMCSETUP(dbPara);

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

        #region WARP_CANCELCREELSETUP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <param name="P_WARPMC"></param>
        /// <param name="P_SIDE"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public string WARP_CANCELCREELSETUP(string P_WARPHEADNO, string P_WARPMC, string P_SIDE, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
                return result;

            if (!HasConnection())
                return result;

            WARP_CANCELCREELSETUPParameter dbPara = new WARP_CANCELCREELSETUPParameter();
            dbPara.P_WARPHEADNO = P_WARPHEADNO;
            dbPara.P_WARPMC = P_WARPMC;
            dbPara.P_SIDE = P_SIDE;
            dbPara.P_OPERATOR = P_OPERATOR;

            WARP_CANCELCREELSETUPResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.WARP_CANCELCREELSETUP(dbPara);

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








