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
    #region ProcessCondition Data Service

    /// <summary>
    /// The data service for Packing process.
    /// </summary>
    public class ProcessConditionDataService : BaseDataService
    {
        #region Singelton

        private static ProcessConditionDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static ProcessConditionDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ProcessConditionDataService))
                    {
                        _instance = new ProcessConditionDataService();
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
        private ProcessConditionDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~ProcessConditionDataService()
        {
        }

        #endregion

        #region Public Methods

        #region Create new Session

        public ProcessConditionSession GetSession(LogInResult loginResult)
        {
            ProcessConditionSession result = new ProcessConditionSession();
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

        // ใช้สำหรับ Load ข้อมูล FINISHING SCOURINGCONDITION
        #region Load finishing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itm_code"></param>
        /// <param name="ScouringNo"></param>
        /// <returns></returns>
        public List<FINISHING_GETSCOURINGCONDITIONData> GetFINISHING_GETSCOURINGCONDITION(string itm_code, string ScouringNo)
        {
            List<FINISHING_GETSCOURINGCONDITIONData> results = CoatingDataService.Instance
                .GetFINISHING_GETSCOURINGCONDITIONDataList(itm_code, ScouringNo);

            return results;
        }

        #endregion

        #region CONDITION_FINISHINGSCOURING
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_SCOURINGNO"></param>
        /// <param name="P_CHEM"></param>
        /// <param name="P_CHEM_MARGIN"></param>
        /// <param name="P_WASH1"></param>
        /// <param name="P_WASH1_MARGIN"></param>
        /// <param name="P_WASH2"></param>
        /// <param name="P_WASH2_MARGIN"></param>
        /// <param name="P_HOTFLUE"></param>
        /// <param name="P_HOTFLUE_MARGIN"></param>
        /// <param name="P_ROOMTEMP"></param>
        /// <param name="P_ROOMTEMP_MARGIN"></param>
        /// <param name="P_SPEED"></param>
        /// <param name="P_SPEED_MARGIN"></param>
        /// <param name="P_MAINFRAME"></param>
        /// <param name="P_MAINFRAME_MARGIN"></param>
        /// <param name="P_WIDTHBE"></param>
        /// <param name="P_WIDTHBE_MARGIN"></param>
        /// <param name="P_WIDTHAF"></param>
        /// <param name="P_WIDTHAF_MARGIN"></param>
        /// <param name="P_PIN2PIN"></param>
        /// <param name="P_PIN2PIN_MARGIN"></param>
        /// <param name="P_HUMIDITYMAX"></param>
        /// <param name="P_HUMIDITYMIN"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public string CONDITION_FINISHINGSCOURING(string P_ITEMCODE,string P_SCOURINGNO,
        decimal? P_CHEM, decimal? P_CHEM_MARGIN, decimal? P_WASH1,decimal? P_WASH1_MARGIN,
        decimal? P_WASH2,decimal? P_WASH2_MARGIN,decimal? P_HOTFLUE, decimal? P_HOTFLUE_MARGIN,
        decimal? P_ROOMTEMP, decimal? P_ROOMTEMP_MARGIN, decimal? P_SPEED, decimal? P_SPEED_MARGIN,
        decimal? P_MAINFRAME, decimal? P_MAINFRAME_MARGIN,decimal? P_WIDTHBE, decimal? P_WIDTHBE_MARGIN,
        decimal? P_WIDTHAF, decimal? P_WIDTHAF_MARGIN, decimal? P_PIN2PIN, decimal? P_PIN2PIN_MARGIN,
        decimal? P_HUMIDITYMAX, decimal? P_HUMIDITYMIN, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITEMCODE))
                return result;

            if (string.IsNullOrWhiteSpace(P_SCOURINGNO))
                return result;

            if (!HasConnection())
                return result;

            CONDITION_FINISHINGSCOURINGParameter dbPara = new CONDITION_FINISHINGSCOURINGParameter();
            dbPara.P_ITEMCODE = P_ITEMCODE;
            dbPara.P_SCOURINGNO = P_SCOURINGNO;
            dbPara.P_CHEM = P_CHEM;
            dbPara.P_CHEM_MARGIN = P_CHEM_MARGIN;
            dbPara.P_WASH1 = P_WASH1;
            dbPara.P_WASH1_MARGIN = P_WASH1_MARGIN;
            dbPara.P_WASH2 = P_WASH2;
            dbPara.P_WASH2_MARGIN = P_WASH2_MARGIN;
            dbPara.P_HOTFLUE = P_HOTFLUE;
            dbPara.P_HOTFLUE_MARGIN = P_HOTFLUE_MARGIN;
            dbPara.P_ROOMTEMP = P_ROOMTEMP;
            dbPara.P_ROOMTEMP_MARGIN = P_ROOMTEMP_MARGIN;
            dbPara.P_SPEED = P_SPEED;
            dbPara.P_SPEED_MARGIN = P_SPEED_MARGIN;
            dbPara.P_MAINFRAME = P_MAINFRAME;
            dbPara.P_MAINFRAME_MARGIN = P_MAINFRAME_MARGIN;
            dbPara.P_WIDTHBE = P_WIDTHBE;
            dbPara.P_WIDTHBE_MARGIN = P_WIDTHBE_MARGIN;
            dbPara.P_WIDTHAF = P_WIDTHAF;
            dbPara.P_WIDTHAF_MARGIN = P_WIDTHAF_MARGIN;
            dbPara.P_PIN2PIN = P_PIN2PIN;
            dbPara.P_PIN2PIN_MARGIN = P_PIN2PIN_MARGIN;
            dbPara.P_HUMIDITYMAX = P_HUMIDITYMAX;
            dbPara.P_HUMIDITYMIN = P_HUMIDITYMIN;
            dbPara.P_OPERATOR = P_OPERATOR;

            CONDITION_FINISHINGSCOURINGResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.CONDITION_FINISHINGSCOURING(dbPara);

                result = dbResult.RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region CONDITION_FINISHINGDRYER
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_WIDTH_BE_HEAT_MAX"></param>
        /// <param name="P_WIDTH_BE_HEAT_MIN"></param>
        /// <param name="P_ACCPRESURE"></param>
        /// <param name="P_ASSTENSION"></param>
        /// <param name="P_ACCARIDENSER"></param>
        /// <param name="P_CHIFROT"></param>
        /// <param name="P_CHIREAR"></param>
        /// <param name="P_DRYERTEMP"></param>
        /// <param name="P_DRYERTEMP_MARGIN"></param>
        /// <param name="P_SPEED"></param>
        /// <param name="P_SPEED_MARGIN"></param>
        /// <param name="P_STEAMPRESURE"></param>
        /// <param name="P_DRYERUPCIRCUFAN"></param>
        /// <param name="P_EXHAUSTFAN"></param>
        /// <param name="P_WIDTH_AF_HEAT"></param>
        /// <param name="P_WIDTH_AF_HEAT_MARGIN"></param>
        /// <param name="P_HUMIDITYMAX"></param>
        /// <param name="P_HUMIDITYMIN"></param>
        /// <param name="P_OPERATOR"></param>
        /// <param name="ScouringNo"></param>
        /// <returns></returns>
        public string CONDITION_FINISHINGDRYER(string P_ITEMCODE,decimal? P_WIDTH_BE_HEAT_MAX,decimal? P_WIDTH_BE_HEAT_MIN,
            decimal? P_ACCPRESURE,decimal? P_ASSTENSION,decimal? P_ACCARIDENSER,decimal? P_CHIFROT, decimal? P_CHIREAR, decimal? P_DRYERTEMP,
            decimal? P_DRYERTEMP_MARGIN,decimal? P_SPEED,decimal? P_SPEED_MARGIN, decimal? P_STEAMPRESURE, decimal? P_DRYERUPCIRCUFAN,decimal? P_EXHAUSTFAN,
            decimal? P_WIDTH_AF_HEAT, decimal? P_WIDTH_AF_HEAT_MARGIN, decimal? P_HUMIDITYMAX, decimal? P_HUMIDITYMIN, string P_OPERATOR, string P_MC)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITEMCODE))
                return result;

            if (!HasConnection())
                return result;

            CONDITION_FINISHINGDRYERParameter dbPara = new CONDITION_FINISHINGDRYERParameter();
            dbPara.P_ITEMCODE = P_ITEMCODE;
            dbPara.P_WIDTH_BE_HEAT_MAX = P_WIDTH_BE_HEAT_MAX;
            dbPara.P_WIDTH_BE_HEAT_MIN = P_WIDTH_BE_HEAT_MIN;
            dbPara.P_ACCPRESURE = P_ACCPRESURE;
            dbPara.P_ASSTENSION = P_ASSTENSION;
            dbPara.P_ACCARIDENSER = P_ACCARIDENSER;
            dbPara.P_CHIFROT = P_CHIFROT;
            dbPara.P_CHIREAR = P_CHIREAR;
            dbPara.P_DRYERTEMP = P_DRYERTEMP;
            dbPara.P_DRYERTEMP_MARGIN = P_DRYERTEMP_MARGIN;
            dbPara.P_SPEED = P_SPEED;
            dbPara.P_SPEED_MARGIN = P_SPEED_MARGIN;
            dbPara.P_STEAMPRESURE = P_STEAMPRESURE;
            dbPara.P_DRYERUPCIRCUFAN = P_DRYERUPCIRCUFAN;
            dbPara.P_EXHAUSTFAN = P_EXHAUSTFAN;
            dbPara.P_WIDTH_AF_HEAT = P_WIDTH_AF_HEAT;
            dbPara.P_WIDTH_AF_HEAT_MARGIN = P_WIDTH_AF_HEAT_MARGIN;
            dbPara.P_HUMIDITYMAX = P_HUMIDITYMAX;
            dbPara.P_HUMIDITYMIN = P_HUMIDITYMIN;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_MC = P_MC;

            CONDITION_FINISHINGDRYERResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.CONDITION_FINISHINGDRYER(dbPara);

                result = dbResult.RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region CONDITION_FINISHINGCOATING
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_COATNO"></param>
        /// <param name="P_CHEM"></param>
        /// <param name="P_CHEM_MARGIN"></param>
        /// <param name="P_WASH1"></param>
        /// <param name="P_WASH1_MARGIN"></param>
        /// <param name="P_WASH2"></param>
        /// <param name="P_WASH2_MARGIN"></param>
        /// <param name="P_HOTFLUE"></param>
        /// <param name="P_HOTFLUE_MARGIN"></param>
        /// <param name="P_BE_COAT_MAX"></param>
        /// <param name="P_BE_COAT_MIN"></param>
        /// <param name="P_ROOMTEMP"></param>
        /// <param name="P_ROOMTEMP_MARGIN"></param>
        /// <param name="P_FANRPM"></param>
        /// <param name="P_FANRPM_MARGIN"></param>
        /// <param name="P_EXFAN_FRONT_BACK"></param>
        /// <param name="P_EXFAN_MARGIN"></param>
        /// <param name="P_EXFAN_MIDDLE"></param>
        /// <param name="P_ANGLEKNIFE"></param>
        /// <param name="P_BLADENO"></param>
        /// <param name="P_BLADEDIRECTION"></param>
        /// <param name="P_PATHLINE"></param>
        /// <param name="P_FEEDIN_MAX"></param>
        /// <param name="P_FEEDIN_MIN"></param>
        /// <param name="P_TENSION_UP"></param>
        /// <param name="P_TENSION_UP_MARGIN"></param>
        /// <param name="P_TENSION_DOWN"></param>
        /// <param name="P_TENSION_DOWN_MARGIN"></param>
        /// <param name="P_FRAME_FORN"></param>
        /// <param name="P_FRAME_TENTER"></param>
        /// <param name="P_OVERFEED"></param>
        /// <param name="P_SPEED"></param>
        /// <param name="P_SPEED_MARGIN"></param>
        /// <param name="P_WIDTHCOAT"></param>
        /// <param name="P_WIDTHCOATALL_MAX"></param>
        /// <param name="P_WIDTHCOATALL_MIN"></param>
        /// <param name="P_COATINGWEIGTH_MAX"></param>
        /// <param name="P_COATINGWEIGTH_MIN"></param>
        /// <param name="P_RATIONSILICONE"></param>
        /// <param name="P_HUMIDITYMAX"></param>
        /// <param name="P_HUMIDITYMIN"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public string CONDITION_FINISHINGCOATING(string P_ITEMCODE, string P_COATNO,
        decimal? P_CHEM, decimal? P_CHEM_MARGIN, decimal? P_WASH1, decimal? P_WASH1_MARGIN,
        decimal? P_WASH2, decimal? P_WASH2_MARGIN, decimal? P_HOTFLUE, decimal? P_HOTFLUE_MARGIN,
        decimal? P_BE_COAT_MAX, decimal? P_BE_COAT_MIN, decimal? P_ROOMTEMP, decimal? P_ROOMTEMP_MARGIN,
        decimal? P_FANRPM, decimal? P_FANRPM_MARGIN, decimal? P_EXFAN_FRONT_BACK, decimal? P_EXFAN_MARGIN,
        decimal? P_EXFAN_MIDDLE, decimal? P_ANGLEKNIFE, string P_BLADENO, string P_BLADEDIRECTION,
        string P_PATHLINE, decimal? P_FEEDIN_MAX, decimal? P_FEEDIN_MIN, decimal? P_TENSION_UP,
        decimal? P_TENSION_UP_MARGIN, decimal? P_TENSION_DOWN, decimal? P_TENSION_DOWN_MARGIN, decimal? P_FRAME_FORN,
        decimal? P_FRAME_TENTER, string P_OVERFEED, decimal? P_SPEED, decimal? P_SPEED_MARGIN,
        decimal? P_WIDTHCOAT, decimal? P_WIDTHCOATALL_MAX, decimal? P_WIDTHCOATALL_MIN, decimal? P_COATINGWEIGTH_MAX,
        decimal? P_COATINGWEIGTH_MIN, string P_RATIONSILICONE, decimal? P_HUMIDITYMAX, decimal? P_HUMIDITYMIN, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITEMCODE))
                return result;

            if (string.IsNullOrWhiteSpace(P_COATNO))
                return result;

            if (!HasConnection())
                return result;

            CONDITION_FINISHINGCOATINGParameter dbPara = new CONDITION_FINISHINGCOATINGParameter();
            dbPara.P_ITEMCODE = P_ITEMCODE;
            dbPara.P_COATNO = P_COATNO;
            dbPara.P_CHEM = P_CHEM;
            dbPara.P_CHEM_MARGIN = P_CHEM_MARGIN;
            dbPara.P_WASH1 = P_WASH1;
            dbPara.P_WASH1_MARGIN = P_WASH1_MARGIN;
            dbPara.P_WASH2 = P_WASH2;
            dbPara.P_WASH2_MARGIN = P_WASH2_MARGIN;
            dbPara.P_HOTFLUE = P_HOTFLUE;
            dbPara.P_HOTFLUE_MARGIN = P_HOTFLUE_MARGIN;
            dbPara.P_BE_COAT_MAX = P_BE_COAT_MAX;
            dbPara.P_BE_COAT_MIN = P_BE_COAT_MIN;
            dbPara.P_ROOMTEMP = P_ROOMTEMP;
            dbPara.P_ROOMTEMP_MARGIN = P_ROOMTEMP_MARGIN;
            dbPara.P_FANRPM = P_FANRPM;
            dbPara.P_FANRPM_MARGIN = P_FANRPM_MARGIN;
            dbPara.P_EXFAN_FRONT_BACK = P_EXFAN_FRONT_BACK;
            dbPara.P_EXFAN_MARGIN = P_EXFAN_MARGIN;
            dbPara.P_EXFAN_MIDDLE = P_EXFAN_MIDDLE;
            dbPara.P_ANGLEKNIFE = P_ANGLEKNIFE;
            dbPara.P_BLADENO = P_BLADENO;
            dbPara.P_BLADEDIRECTION = P_BLADEDIRECTION;
            dbPara.P_PATHLINE = P_PATHLINE;
            dbPara.P_FEEDIN_MAX = P_FEEDIN_MAX;
            dbPara.P_FEEDIN_MIN = P_FEEDIN_MIN;
            dbPara.P_TENSION_UP = P_TENSION_UP;
            dbPara.P_TENSION_UP_MARGIN = P_TENSION_UP_MARGIN;
            dbPara.P_TENSION_DOWN = P_TENSION_DOWN;
            dbPara.P_TENSION_DOWN_MARGIN = P_TENSION_DOWN_MARGIN;
            dbPara.P_FRAME_FORN = P_FRAME_FORN;
            dbPara.P_FRAME_TENTER = P_FRAME_TENTER;
            dbPara.P_OVERFEED = P_OVERFEED;
            dbPara.P_SPEED = P_SPEED;
            dbPara.P_SPEED_MARGIN = P_SPEED_MARGIN;
            dbPara.P_WIDTHCOAT = P_WIDTHCOAT;
            dbPara.P_WIDTHCOATALL_MAX = P_WIDTHCOATALL_MAX;
            dbPara.P_WIDTHCOATALL_MIN = P_WIDTHCOATALL_MIN;
            dbPara.P_COATINGWEIGTH_MAX = P_COATINGWEIGTH_MAX;
            dbPara.P_COATINGWEIGTH_MIN = P_COATINGWEIGTH_MIN;
            dbPara.P_RATIONSILICONE = P_RATIONSILICONE;
            dbPara.P_HUMIDITYMAX = P_HUMIDITYMAX;
            dbPara.P_HUMIDITYMIN = P_HUMIDITYMIN;
            dbPara.P_OPERATOR = P_OPERATOR;

            CONDITION_FINISHINGCOATINGResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.CONDITION_FINISHINGCOATING(dbPara);

                result = dbResult.RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region CONDITION_DRAWING
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMPREPARE"></param>
        /// <param name="P_NOYARN"></param>
        /// <param name="P_REEDTYPE"></param>
        /// <param name="P_NODENT"></param>
        /// <param name="P_PITCH"></param>
        /// <param name="P_AIRSPACE"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public string CONDITION_DRAWING(string P_ITMPREPARE, decimal? P_NOYARN, decimal? P_REEDTYPE,
            decimal? P_NODENT, decimal? P_PITCH, decimal? P_AIRSPACE, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMPREPARE))
                return result;

            if (!HasConnection())
                return result;

            CONDITION_DRAWINGParameter dbPara = new CONDITION_DRAWINGParameter();
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_NOYARN = P_NOYARN;
            dbPara.P_REEDTYPE = P_REEDTYPE;
            dbPara.P_NODENT = P_NODENT;
            dbPara.P_PITCH = P_PITCH;
            dbPara.P_AIRSPACE = P_AIRSPACE;
            dbPara.P_OPERATOR = P_OPERATOR;

            CONDITION_DRAWINGResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.CONDITION_DRAWING(dbPara);

                result = dbResult.RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region CONDITION_BEAMING
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMPREPARE"></param>
        /// <param name="P_NOWARPBEAM"></param>
        /// <param name="P_TOTALYARN"></param>
        /// <param name="P_TOTALKEBA"></param>
        /// <param name="P_BEAMLENGTH"></param>
        /// <param name="P_HARDNESS_MAX"></param>
        /// <param name="P_HARDNESS_MIN"></param>
        /// <param name="P_BEAMWIDTH_MAX"></param>
        /// <param name="P_BEAMWIDTH_MIN"></param>
        /// <param name="P_SPEED_MAX"></param>
        /// <param name="P_SPEED_MIN"></param>
        /// <param name="P_YARNTENSION_MAX"></param>
        /// <param name="P_YARNTENSION_MIN"></param>
        /// <param name="P_WINDTENSION_MAX"></param>
        /// <param name="P_WINDTENSION_MIN"></param>
        /// <param name="P_COMBTYPE"></param>
        /// <param name="P_COMBPITCH"></param>
        /// <param name="P_TOTALBEAM"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public string CONDITION_BEAMING(string P_ITMPREPARE,decimal? P_NOWARPBEAM,decimal? P_TOTALYARN,decimal? P_TOTALKEBA,decimal? P_BEAMLENGTH,
                decimal? P_HARDNESS_MAX,decimal? P_HARDNESS_MIN,decimal? P_BEAMWIDTH_MAX,decimal? P_BEAMWIDTH_MIN,decimal? P_SPEED_MAX,
                decimal? P_SPEED_MIN,decimal? P_YARNTENSION_MAX,decimal? P_YARNTENSION_MIN,decimal? P_WINDTENSION_MAX,decimal? P_WINDTENSION_MIN,
                string P_COMBTYPE,decimal? P_COMBPITCH,decimal? P_TOTALBEAM, string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMPREPARE))
                return result;

            if (!HasConnection())
                return result;

            CONDITION_BEAMINGParameter dbPara = new CONDITION_BEAMINGParameter();
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_NOWARPBEAM = P_NOWARPBEAM;
            dbPara.P_TOTALYARN = P_TOTALYARN;
            dbPara.P_TOTALKEBA = P_TOTALKEBA;
            dbPara.P_BEAMLENGTH = P_BEAMLENGTH;
            dbPara.P_HARDNESS_MAX = P_HARDNESS_MAX;
            dbPara.P_HARDNESS_MIN = P_HARDNESS_MIN;
            dbPara.P_BEAMWIDTH_MAX = P_BEAMWIDTH_MAX;
            dbPara.P_BEAMWIDTH_MIN = P_BEAMWIDTH_MIN;
            dbPara.P_SPEED_MAX = P_SPEED_MAX;
            dbPara.P_SPEED_MIN = P_SPEED_MIN;
            dbPara.P_YARNTENSION_MAX = P_YARNTENSION_MAX;
            dbPara.P_YARNTENSION_MIN = P_YARNTENSION_MIN;
            dbPara.P_WINDTENSION_MAX = P_WINDTENSION_MAX;
            dbPara.P_WINDTENSION_MIN = P_WINDTENSION_MIN;
            dbPara.P_COMBTYPE = P_COMBTYPE;
            dbPara.P_COMBPITCH = P_COMBPITCH;
            dbPara.P_TOTALBEAM = P_TOTALBEAM;

            dbPara.P_OPERATOR = P_OPERATOR;

            CONDITION_BEAMINGResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.CONDITION_BEAMING(dbPara);

                result = dbResult.RESULT;
            }
            catch (Exception ex)
            {
                ex.Err();
                result = ex.Message.ToString();
            }

            return result;
        }

        #endregion

        #region CONDITION_WARPING
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMPREPARE"></param>
        /// <param name="P_MCNO"></param>
        /// <param name="P_ITMYARN"></param>
        /// <param name="P_WARPERENDS"></param>
        /// <param name="P_MAXLENGTH"></param>
        /// <param name="P_MINLENGTH"></param>
        /// <param name="P_WAXING"></param>
        /// <param name="P_COMBTYPE"></param>
        /// <param name="P_COMBPITCH"></param>
        /// <param name="P_KEBAYARN"></param>
        /// <param name="P_NOWARPBEAM"></param>
        /// <param name="P_HARDNESS_MAX"></param>
        /// <param name="P_HARDNESS_MIN"></param>
        /// <param name="P_SPEED"></param>
        /// <param name="P_SPEED_MARGIN"></param>
        /// <param name="P_YARNTENSION"></param>
        /// <param name="P_YARNTENSION_MARGIN"></param>
        /// <param name="P_WINDTENSION"></param>
        /// <param name="P_WINDTENSION_MARGIN"></param>
        /// <param name="P_OPERATOR"></param>
        /// <returns></returns>
        public string CONDITION_WARPING(string P_ITMPREPARE,string P_MCNO,string P_ITMYARN, decimal? P_WARPERENDS,decimal? P_MAXLENGTH,
        decimal? P_MINLENGTH,string P_WAXING,string P_COMBTYPE,decimal? P_COMBPITCH,decimal? P_KEBAYARN,decimal? P_NOWARPBEAM,
        decimal? P_HARDNESS_MAX,decimal? P_HARDNESS_MIN, decimal? P_SPEED,decimal? P_SPEED_MARGIN,decimal? P_YARNTENSION,
        decimal? P_YARNTENSION_MARGIN,decimal? P_WINDTENSION,decimal? P_WINDTENSION_MARGIN,string P_OPERATOR)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMPREPARE))
                return result;

            if (string.IsNullOrWhiteSpace(P_MCNO))
                return result;

            if (!HasConnection())
                return result;

            CONDITION_WARPINGParameter dbPara = new CONDITION_WARPINGParameter();
            dbPara.P_ITMPREPARE = P_ITMPREPARE;
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_ITMYARN = P_ITMYARN;
            dbPara.P_WARPERENDS = P_WARPERENDS;
            dbPara.P_MAXLENGTH = P_MAXLENGTH;
            dbPara.P_MINLENGTH = P_MINLENGTH;
            dbPara.P_WAXING = P_WAXING;
            dbPara.P_COMBTYPE = P_COMBTYPE;
            dbPara.P_COMBPITCH = P_COMBPITCH;
            dbPara.P_KEBAYARN = P_KEBAYARN;
            dbPara.P_NOWARPBEAM = P_NOWARPBEAM;
            dbPara.P_HARDNESS_MAX = P_HARDNESS_MAX;
            dbPara.P_HARDNESS_MIN = P_HARDNESS_MIN;
            dbPara.P_SPEED = P_SPEED;
            dbPara.P_SPEED_MARGIN = P_SPEED_MARGIN;
            dbPara.P_YARNTENSION = P_YARNTENSION;
            dbPara.P_YARNTENSION_MARGIN = P_YARNTENSION_MARGIN;
            dbPara.P_WINDTENSION = P_WINDTENSION;
            dbPara.P_WINDTENSION_MARGIN = P_WINDTENSION_MARGIN;
            dbPara.P_OPERATOR = P_OPERATOR;

            CONDITION_WARPINGResult dbResult = null;

            try
            {
                dbResult =
                    DatabaseManager.Instance.CONDITION_WARPING(dbPara);

                result = dbResult.RESULT;
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