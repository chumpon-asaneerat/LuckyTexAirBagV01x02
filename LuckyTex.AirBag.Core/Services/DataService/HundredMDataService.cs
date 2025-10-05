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
    #region HundredMDataService

    /// <summary>
    /// The data service for HundredM process.
    /// </summary>
    public class HundredMDataService : BaseDataService
    {
        #region Singelton

        private static HundredMDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static HundredMDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(HundredMDataService))
                    {
                        _instance = new HundredMDataService();
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
        private HundredMDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~HundredMDataService()
        {
        }

        #endregion

        #region Public Methods

        #region Create new Session

        public HundredMDataSession GetSession(LogInResult loginResult)
        {
            HundredMDataSession result = new HundredMDataSession();
            result.Init(loginResult);
            return result;
        }

        #endregion

        #region เพิ่มใหม่ ITM_GETITEMCODELIST ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ITM_GETITEMCODELIST> ITM_GETITEMCODELIST()
        {
            List<ITM_GETITEMCODELIST> results = null;

            if (!HasConnection())
                return results;

            ITM_GETITEMCODELISTParameter dbPara = new ITM_GETITEMCODELISTParameter();

            List<ITM_GETITEMCODELISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.ITM_GETITEMCODELIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<ITM_GETITEMCODELIST>();
                    foreach (ITM_GETITEMCODELISTResult dbResult in dbResults)
                    {
                        ITM_GETITEMCODELIST inst = new ITM_GETITEMCODELIST();

                        inst.ITM_CODE = dbResult.ITM_CODE;

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

        #region Load GETINSPECTIONLISTTESTBYITMCODE

        public List<GETINSPECTIONLISTTESTBYITMCODE> GETINSPECTIONLISTTESTBYITMCODE(string P_ITMCODE)
        {
            List<GETINSPECTIONLISTTESTBYITMCODE> results = null;

            if (!HasConnection())
                return results;

            GETINSPECTIONLISTTESTBYITMCODEParameter dbPara = new GETINSPECTIONLISTTESTBYITMCODEParameter();
            dbPara.P_ITMCODE = P_ITMCODE;

            List<GETINSPECTIONLISTTESTBYITMCODEResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.GETINSPECTIONLISTTESTBYITMCODE(dbPara);
                if (null != dbResults)
                {
                    results = new List<GETINSPECTIONLISTTESTBYITMCODE>();

                    foreach (GETINSPECTIONLISTTESTBYITMCODEResult dbResult in dbResults)
                    {
                        GETINSPECTIONLISTTESTBYITMCODE inst = new GETINSPECTIONLISTTESTBYITMCODE();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.DENSITY_W = dbResult.DENSITY_W;

                        #region ChkDENSITY_W

                        if (!string.IsNullOrEmpty(inst.DENSITY_W))
                        {
                            if (inst.DENSITY_W == "1")
                                inst.ChkDENSITY_W = true;
                            else
                                inst.ChkDENSITY_W = false;
                        }

                        #endregion

                        inst.DENSITY_F = dbResult.DENSITY_F;

                        #region ChkDENSITY_F

                        if (!string.IsNullOrEmpty(inst.DENSITY_F))
                        {
                            if (inst.DENSITY_F == "1")
                                inst.ChkDENSITY_F = true;
                            else
                                inst.ChkDENSITY_F = false;
                        }

                        #endregion

                        inst.WIDTH_ALL = dbResult.WIDTH_ALL;

                        #region ChkWIDTH_ALL

                        if (!string.IsNullOrEmpty(inst.WIDTH_ALL))
                        {
                            if (inst.WIDTH_ALL == "1")
                                inst.ChkWIDTH_ALL = true;
                            else
                                inst.ChkWIDTH_ALL = false;
                        }

                        #endregion

                        inst.WIDTH_PIN = dbResult.WIDTH_PIN;

                        #region ChkWIDTH_PIN

                        if (!string.IsNullOrEmpty(inst.WIDTH_PIN))
                        {
                            if (inst.WIDTH_PIN == "1")
                                inst.ChkWIDTH_PIN = true;
                            else
                                inst.ChkWIDTH_PIN = false;
                        }

                        #endregion

                        inst.WIDTH_SelvageL = dbResult.WIDTH_SelvageL;
                        #region SelvageL

                        if (!string.IsNullOrEmpty(inst.WIDTH_SelvageL))
                        {
                            if (inst.WIDTH_SelvageL == "1")
                                inst.ChkWIDTH_SelvageL = true;
                            else
                                inst.ChkWIDTH_SelvageL = false;
                        }

                        #endregion


                        inst.WIDTH_SelvageR = dbResult.WIDTH_SelvageR;
                        #region SelvageR

                        if (!string.IsNullOrEmpty(inst.WIDTH_SelvageR))
                        {
                            if (inst.WIDTH_SelvageR == "1")
                                inst.ChkWIDTH_SelvageR = true;
                            else
                                inst.ChkWIDTH_SelvageR = false;
                        }

                        #endregion


                        inst.WIDTH_COAT = dbResult.WIDTH_COAT;
                        #region ChkWIDTH_COAT

                        if (!string.IsNullOrEmpty(inst.WIDTH_COAT))
                        {
                            if (inst.WIDTH_COAT == "1")
                                inst.ChkWIDTH_COAT = true;
                            else
                                inst.ChkWIDTH_COAT = false;
                        }

                        #endregion

                        inst.TRIM_L = dbResult.TRIM_L;

                        #region ChkTRIM_L

                        if (!string.IsNullOrEmpty(inst.TRIM_L))
                        {
                            if (inst.TRIM_L == "1")
                                inst.ChkTRIM_L = true;
                            else
                                inst.ChkTRIM_L = false;
                        }

                        #endregion

                        inst.TRIM_R = dbResult.TRIM_R;

                        #region ChkTRIM_R

                        if (!string.IsNullOrEmpty(inst.TRIM_R))
                        {
                            if (inst.TRIM_R == "1")
                                inst.ChkTRIM_R = true;
                            else
                                inst.ChkTRIM_R = false;
                        }

                        #endregion

                        inst.FLOPPY_L = dbResult.FLOPPY_L;

                        #region ChkFLOPPY_L

                        if (!string.IsNullOrEmpty(inst.FLOPPY_L))
                        {
                            if (inst.FLOPPY_L == "1")
                                inst.ChkFLOPPY_L = true;
                            else
                                inst.ChkFLOPPY_L = false;
                        }

                        #endregion

                        inst.FLOPPY_R = dbResult.FLOPPY_R;

                        #region ChkFLOPPY_R

                        if (!string.IsNullOrEmpty(inst.FLOPPY_R))
                        {
                            if (inst.FLOPPY_R == "1")
                                inst.ChkFLOPPY_R = true;
                            else
                                inst.ChkFLOPPY_R = false;
                        }

                        #endregion

                        inst.HARDNESS_L = dbResult.HARDNESS_L;

                        #region ChkHARDNESS_L

                        if (!string.IsNullOrEmpty(inst.HARDNESS_L))
                        {
                            if (inst.HARDNESS_L == "1")
                                inst.ChkHARDNESS_L = true;
                            else
                                inst.ChkHARDNESS_L = false;
                        }

                        #endregion

                        inst.HARDNESS_C = dbResult.HARDNESS_C;

                        #region ChkHARDNESS_C

                        if (!string.IsNullOrEmpty(inst.HARDNESS_C))
                        {
                            if (inst.HARDNESS_C == "1")
                                inst.ChkHARDNESS_C = true;
                            else
                                inst.ChkHARDNESS_C = false;
                        }

                        #endregion

                        inst.HARDNESS_R = dbResult.HARDNESS_R;

                        #region ChkHARDNESS_R

                        if (!string.IsNullOrEmpty(inst.HARDNESS_R))
                        {
                            if (inst.HARDNESS_R == "1")
                                inst.ChkHARDNESS_R = true;
                            else
                                inst.ChkHARDNESS_R = false;
                        }

                        #endregion

                        inst.UNWINDER = dbResult.UNWINDER;

                        #region ChkUNWINDER

                        if (!string.IsNullOrEmpty(inst.UNWINDER))
                        {
                            if (inst.UNWINDER == "1")
                                inst.ChkUNWINDER = true;
                            else
                                inst.ChkUNWINDER = false;
                        }

                        #endregion

                        inst.WINDER = dbResult.WINDER;

                        #region ChkWINDER

                        if (!string.IsNullOrEmpty(inst.WINDER))
                        {
                            if (inst.WINDER == "1")
                                inst.ChkWINDER = true;
                            else
                                inst.ChkWINDER = false;
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

        #region ITM_UPDATE100MRECORD

        public bool ITM_UPDATE100MRECORD(string P_ITEMCODE,
        string P_DENW, string P_DENF, string P_WIDTHALL, string P_WIDTHPIN, string P_WIDTHCOAT, string P_WIDTHSELVAGEL, string P_WIDTHSELVAGER, string P_TRIML, string P_TRIMR,
        string P_FLOPPYL, string P_FLOPPYR, string P_UNWINDER, string P_WINDER, string P_HARDNESSL, string P_HARDNESSC, string P_HARDNESSR)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_ITEMCODE))
                return result;

            if (!HasConnection())
                return result;

            ITM_UPDATE100MRECORDParameter dbPara = new ITM_UPDATE100MRECORDParameter();
            dbPara.P_ITEMCODE = P_ITEMCODE;
            dbPara.P_DENW = P_DENW;
            dbPara.P_DENF = P_DENF;
            dbPara.P_WIDTHALL = P_WIDTHALL;
            dbPara.P_WIDTHPIN = P_WIDTHPIN;
            dbPara.P_WIDTHCOAT = P_WIDTHCOAT;
            dbPara.P_WIDTHSELVAGEL = P_WIDTHSELVAGEL;
            dbPara.P_WIDTHSELVAGER = P_WIDTHSELVAGER;

            dbPara.P_TRIML = P_TRIML;
            dbPara.P_TRIMR = P_TRIMR;
            dbPara.P_FLOPPYL = P_FLOPPYL;
            dbPara.P_FLOPPYR = P_FLOPPYR;
            dbPara.P_UNWINDER = P_UNWINDER;
            dbPara.P_WINDER = P_WINDER;
            dbPara.P_HARDNESSL = P_HARDNESSL;
            dbPara.P_HARDNESSC = P_HARDNESSC;
            dbPara.P_HARDNESSR = P_HARDNESSR;

            ITM_UPDATE100MRECORDResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.ITM_UPDATE100MRECORD(dbPara);

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

