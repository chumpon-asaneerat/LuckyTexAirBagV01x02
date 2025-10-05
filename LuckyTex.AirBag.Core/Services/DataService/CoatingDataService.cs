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
    #region Coating Data Service

    /// <summary>
    /// The data service for Coating data.
    /// </summary>
    public class CoatingDataService : BaseDataService
    {
        #region Singelton

        private static CoatingDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static CoatingDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(CoatingDataService))
                    {
                        _instance = new CoatingDataService();
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
        private CoatingDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~CoatingDataService()
        {
        }

        #endregion

        #region Public Methods

        #region เพิ่มใหม่ FINISHING_CHECKITEMWEAVINGList ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_CHECKITEMWEAVINGData> FINISHING_CHECKITEMWEAVINGList(string itm_code, string itm_weaving)
        {
            List<FINISHING_CHECKITEMWEAVINGData> results = null;

            if (!HasConnection())
                return results;

            FINISHING_CHECKITEMWEAVINGParameter dbPara = new FINISHING_CHECKITEMWEAVINGParameter();
            dbPara.P_ITMCODE = itm_code;
            dbPara.P_ITMWEAVING = itm_weaving;

            List<FINISHING_CHECKITEMWEAVINGResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_CHECKITEMWEAVING(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_CHECKITEMWEAVINGData>();
                    foreach (FINISHING_CHECKITEMWEAVINGResult dbResult in dbResults)
                    {
                        FINISHING_CHECKITEMWEAVINGData inst = new FINISHING_CHECKITEMWEAVINGData();

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

        #region เพิ่มใหม่ GetWeavingingDataList ใช้ในการ Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<GETWEAVINGINGDATA> GetWeavingingDataList(string WEAVINGLOT)
        {
            List<GETWEAVINGINGDATA> results = null;

            if (!HasConnection())
                return results;

            GETWEAVINGINGDATAParameter dbPara = new GETWEAVINGINGDATAParameter();
            dbPara.P_WEAVINGLOT = WEAVINGLOT;


            List<GETWEAVINGINGDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.GETWEAVINGINGDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<GETWEAVINGINGDATA>();
                    foreach (GETWEAVINGINGDATAResult dbResult in dbResults)
                    {
                        GETWEAVINGINGDATA inst = new GETWEAVINGINGDATA();

                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.LENGTH = dbResult.LENGTH;

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

        #region COATING

        #region เพิ่มใหม่ GetFINISHING_GETCOATINGCONDITIONDataList ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETCOATINGCONDITIONData> GetFINISHING_GETCOATINGCONDITIONDataList(string itm_code, string CoatNo)
        {
            List<FINISHING_GETCOATINGCONDITIONData> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETCOATINGCONDITIONParameter dbPara = new FINISHING_GETCOATINGCONDITIONParameter();
            dbPara.P_ITEMCODE = itm_code;
            dbPara.P_COATNO = CoatNo;

            List<FINISHING_GETCOATINGCONDITIONResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETCOATINGCONDITION(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETCOATINGCONDITIONData>();
                    foreach (FINISHING_GETCOATINGCONDITIONResult dbResult in dbResults)
                    {
                        FINISHING_GETCOATINGCONDITIONData inst = new FINISHING_GETCOATINGCONDITIONData();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.SATURATOR_CHEM = dbResult.SATURATOR_CHEM;
                        inst.SATURATOR_CHEM_MARGIN = dbResult.SATURATOR_CHEM_MARGIN;
                        inst.WASHING1 = dbResult.WASHING1;
                        inst.WASHING1_MARGIN = dbResult.WASHING1_MARGIN;
                        inst.WASHING2 = dbResult.WASHING2;
                        inst.WASHING2_MARGIN = dbResult.WASHING2_MARGIN;
                        inst.HOTFLUE = dbResult.HOTFLUE;
                        inst.HOTFLUE_MARGIN = dbResult.HOTFLUE_MARGIN;
                        inst.BE_COATWIDTHMAX = dbResult.BE_COATWIDTHMAX;
                        inst.BE_COATWIDTHMIN = dbResult.BE_COATWIDTHMIN;
                        inst.ROOMTEMP = dbResult.ROOMTEMP;
                        inst.ROOMTEMP_MARGIN = dbResult.ROOMTEMP_MARGIN;
                        inst.FANRPM = dbResult.FANRPM;
                        inst.FANRPM_MARGIN = dbResult.FANRPM_MARGIN;
                        inst.EXFAN_FRONT_BACK = dbResult.EXFAN_FRONT_BACK;
                        inst.EXFAN_MARGIN = dbResult.EXFAN_MARGIN;
                        inst.ANGLEKNIFE = dbResult.ANGLEKNIFE;
                        inst.BLADENO = dbResult.BLADENO;
                        inst.BLADEDIRECTION = dbResult.BLADEDIRECTION;
                        inst.PATHLINE = dbResult.PATHLINE;
                        inst.FEEDIN_MAX = dbResult.FEEDIN_MAX;
                        inst.TENSION_UP = dbResult.TENSION_UP;
                        inst.TENSION_DOWN = dbResult.TENSION_DOWN;
                        inst.TENSION_DOWN_MARGIN = dbResult.TENSION_DOWN_MARGIN;
                        inst.FRAMEWIDTH_FORN = dbResult.FRAMEWIDTH_FORN;
                        inst.FRAMEWIDTH_TENTER = dbResult.FRAMEWIDTH_TENTER;
                        inst.OVERFEED = dbResult.OVERFEED;
                        inst.SPEED = dbResult.SPEED;
                        inst.SPEED_MARGIN = dbResult.SPEED_MARGIN;
                        inst.WIDTHCOAT = dbResult.WIDTHCOAT;
                        inst.WIDTHCOATALL_MAX = dbResult.WIDTHCOATALL_MAX;
                        inst.WIDTHCOATALL_MIN = dbResult.WIDTHCOATALL_MIN;
                        inst.COATINGWEIGTH_MAX = dbResult.COATINGWEIGTH_MAX;
                        inst.COATINGWEIGTH_MIN = dbResult.COATINGWEIGTH_MIN;
                        inst.EXFAN_MIDDLE = dbResult.EXFAN_MIDDLE;
                        inst.RATIOSILICONE = dbResult.RATIOSILICONE;
                        inst.COATNO = dbResult.COATNO;
                        inst.FEEDIN_MIN = dbResult.FEEDIN_MIN;
                        inst.TENSION_UP_MARGIN = dbResult.TENSION_UP_MARGIN;

                        // เพิ่มใหม่
                        inst.HUMIDITY_MIN = dbResult.HUMIDITY_MIN;
                        inst.HUMIDITY_MAX = dbResult.HUMIDITY_MAX;

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

        #region เพิ่มใหม่ FINISHING_GETCOATINGCONDITION ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETCOATINGDATA> FINISHING_GETCOATINGCONDITIONList(string mcno, string flag)
        {
            List<FINISHING_GETCOATINGDATA> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETCOATINGDATAParameter dbPara = new FINISHING_GETCOATINGDATAParameter();
            dbPara.P_MCNO = mcno;
            dbPara.P_FLAG = flag;

            List<FINISHING_GETCOATINGDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETCOATINGDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETCOATINGDATA>();
                    foreach (FINISHING_GETCOATINGDATAResult dbResult in dbResults)
                    {
                        FINISHING_GETCOATINGDATA inst = new FINISHING_GETCOATINGDATA();

                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;
                        inst.MCNO = dbResult.MCNO;
                        inst.STATUSFLAG = dbResult.STATUSFLAG;
                        inst.SATURATOR_CHEM_PV = dbResult.SATURATOR_CHEM_PV;
                        inst.SATURATOR_CHEM_SP = dbResult.SATURATOR_CHEM_SP;
                        inst.WASHING1_PV = dbResult.WASHING1_PV;
                        inst.WASHING1_SP = dbResult.WASHING1_SP;
                        inst.WASHING2_PV = dbResult.WASHING2_PV;
                        inst.WASHING2_SP = dbResult.WASHING2_SP;
                        inst.HOTFLUE_PV = dbResult.HOTFLUE_PV;
                        inst.HOTFLUE_SP = dbResult.HOTFLUE_SP;
                        inst.BE_COATWIDTH = dbResult.BE_COATWIDTH;
                        inst.TEMP1_PV = dbResult.TEMP1_PV;
                        inst.TEMP1_SP = dbResult.TEMP1_SP;
                        inst.TEMP2_PV = dbResult.TEMP2_PV;
                        inst.TEMP2_SP = dbResult.TEMP2_SP;
                        inst.TEMP3_PV = dbResult.TEMP3_PV;
                        inst.TEMP3_SP = dbResult.TEMP3_SP;
                        inst.TEMP4_PV = dbResult.TEMP4_PV;
                        inst.TEMP4_SP = dbResult.TEMP4_SP;
                        inst.TEMP5_PV = dbResult.TEMP5_PV;
                        inst.TEMP5_SP = dbResult.TEMP5_SP;
                        inst.TEMP6_PV = dbResult.TEMP6_PV;
                        inst.TEMP6_SP = dbResult.TEMP6_SP;
                        inst.TEMP7_PV = dbResult.TEMP7_PV;
                        inst.TEMP7_SP = dbResult.TEMP7_SP;
                        inst.TEMP8_PV = dbResult.TEMP8_PV;
                        inst.TEMP8_SP = dbResult.TEMP8_SP;
                        inst.TEMP9_PV = dbResult.TEMP9_PV;
                        inst.TEMP9_SP = dbResult.TEMP9_SP;
                        inst.TEMP10_PV = dbResult.TEMP10_PV;
                        inst.TEMP10_SP = dbResult.TEMP10_SP;
                        inst.FANRPM = dbResult.FANRPM;
                        inst.EXFAN_FRONT_BACK = dbResult.EXFAN_FRONT_BACK;
                        inst.EXFAN_MIDDLE = dbResult.EXFAN_MIDDLE;
                        inst.ANGLEKNIFE = dbResult.ANGLEKNIFE;
                        inst.BLADENO = dbResult.BLADENO;
                        inst.BLADEDIRECTION = dbResult.BLADEDIRECTION;
                        inst.CYLINDER_TENSIONUP = dbResult.CYLINDER_TENSIONUP;
                        inst.OPOLE_TENSIONDOWN = dbResult.OPOLE_TENSIONDOWN;
                        inst.FRAMEWIDTH_FORN = dbResult.FRAMEWIDTH_FORN;
                        inst.FRAMEWIDTH_TENTER = dbResult.FRAMEWIDTH_TENTER;
                        inst.PATHLINE = dbResult.PATHLINE;
                        inst.FEEDIN = dbResult.FEEDIN;
                        inst.OVERFEED = dbResult.OVERFEED;
                        inst.SPEED_PV = dbResult.SPEED_PV;
                        inst.SPEED_SP = dbResult.SPEED_SP;
                        inst.WIDTHCOAT = dbResult.WIDTHCOAT;
                        inst.WIDTHCOATALL = dbResult.WIDTHCOATALL;
                        inst.SILICONE_A = dbResult.SILICONE_A;
                        inst.SILICONE_B = dbResult.SILICONE_B;
                        inst.COATINGWEIGTH_L = dbResult.COATINGWEIGTH_L;
                        inst.COATINGWEIGTH_C = dbResult.COATINGWEIGTH_C;
                        inst.COATINGWEIGTH_R = dbResult.COATINGWEIGTH_R;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.CONDITIONDATE = dbResult.CONDITIONDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.SAMPLINGID = dbResult.SAMPLINGID;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.WEAVINGLENGTH = dbResult.WEAVINGLENGTH;
                        inst.REMARK = dbResult.REMARK;

                        // เพิ่ม
                        inst.HUMIDITY_AF = dbResult.HUMIDITY_AF;
                        inst.HUMIDITY_BF = dbResult.HUMIDITY_BF;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

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

        #region เพิ่มใหม่ FINISHING_COATINGDATABYLOT ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_COATINGDATABYLOT> FINISHING_COATINGDATABYLOT(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_COATINGDATABYLOT> results = null;

            if (!HasConnection())
                return results;

            FINISHING_COATINGDATABYLOTParameter dbPara = new FINISHING_COATINGDATABYLOTParameter();
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<FINISHING_COATINGDATABYLOTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_COATINGDATABYLOT(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_COATINGDATABYLOT>();
                    foreach (FINISHING_COATINGDATABYLOTResult dbResult in dbResults)
                    {
                        FINISHING_COATINGDATABYLOT inst = new FINISHING_COATINGDATABYLOT();

                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;
                        inst.MCNO = dbResult.MCNO;
                        inst.STATUSFLAG = dbResult.STATUSFLAG;
                        inst.SATURATOR_CHEM_PV = dbResult.SATURATOR_CHEM_PV;
                        inst.SATURATOR_CHEM_SP = dbResult.SATURATOR_CHEM_SP;
                        inst.WASHING1_PV = dbResult.WASHING1_PV;
                        inst.WASHING1_SP = dbResult.WASHING1_SP;
                        inst.WASHING2_PV = dbResult.WASHING2_PV;
                        inst.WASHING2_SP = dbResult.WASHING2_SP;
                        inst.HOTFLUE_PV = dbResult.HOTFLUE_PV;
                        inst.HOTFLUE_SP = dbResult.HOTFLUE_SP;
                        inst.BE_COATWIDTH = dbResult.BE_COATWIDTH;
                        inst.TEMP1_PV = dbResult.TEMP1_PV;
                        inst.TEMP1_SP = dbResult.TEMP1_SP;
                        inst.TEMP2_PV = dbResult.TEMP2_PV;
                        inst.TEMP2_SP = dbResult.TEMP2_SP;
                        inst.TEMP3_PV = dbResult.TEMP3_PV;
                        inst.TEMP3_SP = dbResult.TEMP3_SP;
                        inst.TEMP4_PV = dbResult.TEMP4_PV;
                        inst.TEMP4_SP = dbResult.TEMP4_SP;
                        inst.TEMP5_PV = dbResult.TEMP5_PV;
                        inst.TEMP5_SP = dbResult.TEMP5_SP;
                        inst.TEMP6_PV = dbResult.TEMP6_PV;
                        inst.TEMP6_SP = dbResult.TEMP6_SP;
                        inst.TEMP7_PV = dbResult.TEMP7_PV;
                        inst.TEMP7_SP = dbResult.TEMP7_SP;
                        inst.TEMP8_PV = dbResult.TEMP8_PV;
                        inst.TEMP8_SP = dbResult.TEMP8_SP;
                        inst.TEMP9_PV = dbResult.TEMP9_PV;
                        inst.TEMP9_SP = dbResult.TEMP9_SP;
                        inst.TEMP10_PV = dbResult.TEMP10_PV;
                        inst.TEMP10_SP = dbResult.TEMP10_SP;
                        inst.FANRPM = dbResult.FANRPM;
                        inst.EXFAN_FRONT_BACK = dbResult.EXFAN_FRONT_BACK;
                        inst.EXFAN_MIDDLE = dbResult.EXFAN_MIDDLE;
                        inst.ANGLEKNIFE = dbResult.ANGLEKNIFE;
                        inst.BLADENO = dbResult.BLADENO;
                        inst.BLADEDIRECTION = dbResult.BLADEDIRECTION;
                        inst.CYLINDER_TENSIONUP = dbResult.CYLINDER_TENSIONUP;
                        inst.OPOLE_TENSIONDOWN = dbResult.OPOLE_TENSIONDOWN;
                        inst.FRAMEWIDTH_FORN = dbResult.FRAMEWIDTH_FORN;
                        inst.FRAMEWIDTH_TENTER = dbResult.FRAMEWIDTH_TENTER;
                        inst.PATHLINE = dbResult.PATHLINE;
                        inst.FEEDIN = dbResult.FEEDIN;
                        inst.OVERFEED = dbResult.OVERFEED;
                        inst.SPEED_PV = dbResult.SPEED_PV;
                        inst.SPEED_SP = dbResult.SPEED_SP;
                        inst.WIDTHCOAT = dbResult.WIDTHCOAT;
                        inst.WIDTHCOATALL = dbResult.WIDTHCOATALL;
                        inst.SILICONE_A = dbResult.SILICONE_A;
                        inst.SILICONE_B = dbResult.SILICONE_B;
                        inst.COATINGWEIGTH_L = dbResult.COATINGWEIGTH_L;
                        inst.COATINGWEIGTH_C = dbResult.COATINGWEIGTH_C;
                        inst.COATINGWEIGTH_R = dbResult.COATINGWEIGTH_R;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.CONDITIONDATE = dbResult.CONDITIONDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.SAMPLINGID = dbResult.SAMPLINGID;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.REMARK = dbResult.REMARK;

                        // เพิ่ม
                        inst.HUMIDITY_BF = dbResult.HUMIDITY_BF;
                        inst.HUMIDITY_AF = dbResult.HUMIDITY_AF;
                        inst.REPROCESS = dbResult.REPROCESS;
                        inst.WEAVLENGTH = dbResult.WEAVLENGTH;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;
                        inst.TEMP1_MIN = dbResult.TEMP1_MIN;
                        inst.TEMP1_MAX = dbResult.TEMP1_MAX;
                        inst.TEMP2_MIN = dbResult.TEMP2_MIN;
                        inst.TEMP2_MAX = dbResult.TEMP2_MAX;
                        inst.TEMP3_MIN = dbResult.TEMP3_MIN;
                        inst.TEMP3_MAX = dbResult.TEMP3_MAX;
                        inst.TEMP4_MIN = dbResult.TEMP4_MIN;
                        inst.TEMP4_MAX = dbResult.TEMP4_MAX;
                        inst.TEMP5_MIN = dbResult.TEMP5_MIN;
                        inst.TEMP5_MAX = dbResult.TEMP5_MAX;
                        inst.TEMP6_MIN = dbResult.TEMP6_MIN;
                        inst.TEMP6_MAX = dbResult.TEMP6_MAX;
                        inst.TEMP7_MIN = dbResult.TEMP7_MIN;
                        inst.TEMP7_MAX = dbResult.TEMP7_MAX;
                        inst.TEMP8_MIN = dbResult.TEMP8_MIN;
                        inst.TEMP8_MAX = dbResult.TEMP8_MAX;
                        inst.TEMP9_MIN = dbResult.TEMP9_MIN;
                        inst.TEMP9_MAX = dbResult.TEMP9_MAX;
                        inst.TEMP10_MIN = dbResult.TEMP10_MIN;
                        inst.TEMP10_MAX = dbResult.TEMP10_MAX;
                        inst.SAT_CHEM_MIN = dbResult.SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = dbResult.SAT_CHEM_MAX;
                        inst.WASHING1_MIN = dbResult.WASHING1_MIN;
                        inst.WASHING1_MAX = dbResult.WASHING1_MAX;
                        inst.WASHING2_MIN = dbResult.WASHING2_MIN;
                        inst.WASHING2_MAX = dbResult.WASHING2_MAX;
                        inst.HOTFLUE_MIN = dbResult.HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = dbResult.HOTFLUE_MAX;
                        inst.SPEED_MIN = dbResult.SPEED_MIN;
                        inst.SPEED_MAX = dbResult.SPEED_MAX;
                        inst.TENSIONUP_MIN = dbResult.TENSIONUP_MIN;
                        inst.TENSIONUP_MAX = dbResult.TENSIONUP_MAX;
                        inst.TENSIONDOWN_MIN = dbResult.TENSIONDOWN_MIN;
                        inst.TENSIONDOWN_MAX = dbResult.TENSIONDOWN_MAX;

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

        #region เพิ่มใหม่ FINISHING_COATINGPLCDATA ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_COATINGPLCDATA> FINISHING_COATINGPLCDATA(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_COATINGPLCDATA> results = null;

            if (!HasConnection())
                return results;

            FINISHING_COATINGPLCDATAParameter dbPara = new FINISHING_COATINGPLCDATAParameter();
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<FINISHING_COATINGPLCDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_COATINGPLCDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_COATINGPLCDATA>();
                    foreach (FINISHING_COATINGPLCDATAResult dbResult in dbResults)
                    {
                        FINISHING_COATINGPLCDATA inst = new FINISHING_COATINGPLCDATA();

                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.TEMP1_MIN = dbResult.TEMP1_MIN;
                        inst.TEMP1_MAX = dbResult.TEMP1_MAX;
                        inst.TEMP1 = dbResult.TEMP1;
                        inst.TEMP2_MIN = dbResult.TEMP2_MIN;
                        inst.TEMP2_MAX = dbResult.TEMP2_MAX;
                        inst.TEMP2 = dbResult.TEMP2;
                        inst.TEMP3_MIN = dbResult.TEMP3_MIN;
                        inst.TEMP3_MAX = dbResult.TEMP3_MAX;
                        inst.TEMP3 = dbResult.TEMP3;
                        inst.TEMP4_MIN = dbResult.TEMP4_MIN;
                        inst.TEMP4_MAX = dbResult.TEMP4_MAX;
                        inst.TEMP4 = dbResult.TEMP4;
                        inst.TEMP5_MIN = dbResult.TEMP5_MIN;
                        inst.TEMP5_MAX = dbResult.TEMP5_MAX;
                        inst.TEMP5 = dbResult.TEMP5;
                        inst.TEMP6_MIN = dbResult.TEMP6_MIN;
                        inst.TEMP6_MAX = dbResult.TEMP6_MAX;
                        inst.TEMP6 = dbResult.TEMP6;
                        inst.TEMP7_MIN = dbResult.TEMP7_MIN;
                        inst.TEMP7_MAX = dbResult.TEMP7_MAX;
                        inst.TEMP7 = dbResult.TEMP7;
                        inst.TEMP8_MIN = dbResult.TEMP8_MIN;
                        inst.TEMP8_MAX = dbResult.TEMP8_MAX;
                        inst.TEMP8 = dbResult.TEMP8;
                        inst.TEMP9_MIN = dbResult.TEMP9_MIN;
                        inst.TEMP9_MAX = dbResult.TEMP9_MAX;
                        inst.TEMP9 = dbResult.TEMP9;
                        inst.TEMP10_MIN = dbResult.TEMP10_MIN;
                        inst.TEMP10_MAX = dbResult.TEMP10_MAX;
                        inst.TEMP10 = dbResult.TEMP10;

                        inst.SPEED_MIN = dbResult.SPEED_MIN;
                        inst.SPEED_MAX = dbResult.SPEED_MAX;
                        inst.SPEED = dbResult.SPEED;
                        inst.SAT_MIN = dbResult.SAT_MIN;
                        inst.SAT_MAX = dbResult.SAT_MAX;
                        inst.SAT = dbResult.SAT;
                        inst.HOTF_MIN = dbResult.HOTF_MIN;
                        inst.HOTF_MAX = dbResult.HOTF_MAX;
                        inst.HOTF = dbResult.HOTF;
                        inst.WASH1_MIN = dbResult.WASH1_MIN;
                        inst.WASH1_MAX = dbResult.WASH1_MAX;
                        inst.WASH1 = dbResult.WASH1;
                        inst.WASH2_MIN = dbResult.WASH2_MIN;
                        inst.WASH2_MAX = dbResult.WASH2_MAX;
                        inst.WASH2 = dbResult.WASH2;

                        inst.TENUP_MIN = dbResult.TENUP_MIN;
                        inst.TENUP_MAX = dbResult.TENUP_MAX;
                        inst.TENUP = dbResult.TENUP;
                        inst.TENDOWN_MIN = dbResult.TENDOWN_MIN;
                        inst.TENDOWN_MAX = dbResult.TENDOWN_MAX;
                        inst.TENDOWN = dbResult.TENDOWN;

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

        // เปลี่ยน ค่า Return =  string result 
        #region เพิ่ม FINISHING_INSERTCOATING

        public string FINISHING_INSERTCOATING(string weavlot, string itmCode, string finishcustomer, string PRODUCTTYPEID, string operatorid, string MCNO, string flag
            , decimal? SATURATOR_PV, decimal? SATURATOR_SP, decimal? WASHING1_PV, decimal? WASHING1_SP
            , decimal? WASHING2_PV, decimal? WASHING2_SP, decimal? HOTFLUE_PV, decimal? HOTFLUE_SP
            , decimal? TEMP1_PV, decimal? TEMP1_SP, decimal? TEMP2_PV, decimal? TEMP2_SP
            , decimal? TEMP3_PV, decimal? TEMP3_SP, decimal? TEMP4_PV, decimal? TEMP4_SP
            , decimal? TEMP5_PV, decimal? TEMP5_SP, decimal? TEMP6_PV, decimal? TEMP6_SP
            , decimal? TEMP7_PV, decimal? TEMP7_SP, decimal? TEMP8_PV, decimal? TEMP8_SP
            , decimal? TEMP9_PV, decimal? TEMP9_SP, decimal? TEMP10_PV, decimal? TEMP10_SP
            , decimal? SPEED_PV, decimal? SPEED_SP, decimal? BECOATWIDTH, decimal? FANRPM
            , decimal? EXFAN_FRONT_BACK, decimal? EXFAN_MIDDLE, decimal? ANGLEKNIFE
            , string BLADENO , string BLADEDIRECTION , decimal? TENSIONUP
            , decimal? TENSIONDOWN, decimal? FORN, decimal? TENTER, decimal? PATHLINE
            , decimal? FEEDIN, decimal? OVERFEED, decimal? WIDTHCOAT, decimal? WIDTHCOATALL
            , string SILICONEA , string SILICONEB , decimal? CWL , decimal? CWC , decimal? CWR
            , decimal? HUMID_AF, decimal? HUMID_BF
            , string REPROCESS, decimal? WEAVLENGTH, string OPERATOR_GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(weavlot))
                return result;
            if (string.IsNullOrWhiteSpace(itmCode))
                return result;
            if (string.IsNullOrWhiteSpace(finishcustomer))
                return result;
            if (string.IsNullOrWhiteSpace(PRODUCTTYPEID))
                return result;
            if (string.IsNullOrWhiteSpace(operatorid))
                return result;
            if (string.IsNullOrWhiteSpace(MCNO))
                return result;
            if (string.IsNullOrWhiteSpace(flag))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_INSERTCOATINGParameter dbPara = new FINISHING_INSERTCOATINGParameter();

            dbPara.P_WEAVLOT = weavlot;
            dbPara.P_ITMCODE = itmCode;
            dbPara.P_FINISHCUSTOMER = finishcustomer;
            dbPara.P_PRODUCTTYPEID = PRODUCTTYPEID;
            dbPara.P_OPERATORID = operatorid;
            dbPara.P_MCNO = MCNO;
            dbPara.P_FLAG = flag;
            dbPara.P_SATURATOR_PV = SATURATOR_PV;
            dbPara.P_SATURATOR_SP = SATURATOR_SP;
            dbPara.P_WASHING1_PV = WASHING1_PV;
            dbPara.P_WASHING1_SP = WASHING1_SP;
            dbPara.P_WASHING2_PV = WASHING2_PV;
            dbPara.P_WASHING2_SP = WASHING2_SP;
            dbPara.P_HOTFLUE_PV = HOTFLUE_PV;
            dbPara.P_HOTFLUE_SP = HOTFLUE_SP;
            dbPara.P_TEMP1_PV = TEMP1_PV;
            dbPara.P_TEMP1_SP = TEMP1_SP;
            dbPara.P_TEMP2_PV = TEMP2_PV;
            dbPara.P_TEMP2_SP = TEMP2_SP;
            dbPara.P_TEMP3_PV = TEMP3_PV;
            dbPara.P_TEMP3_SP = TEMP3_SP;
            dbPara.P_TEMP4_PV = TEMP4_PV;
            dbPara.P_TEMP4_SP = TEMP4_SP;
            dbPara.P_TEMP5_PV = TEMP5_PV;
            dbPara.P_TEMP5_SP = TEMP5_SP;
            dbPara.P_TEMP6_PV = TEMP6_PV;
            dbPara.P_TEMP6_SP = TEMP6_SP;
            dbPara.P_TEMP7_PV = TEMP7_PV;
            dbPara.P_TEMP7_SP = TEMP7_SP;
            dbPara.P_TEMP8_PV = TEMP8_PV;
            dbPara.P_TEMP8_SP = TEMP8_SP;
            dbPara.P_TEMP9_PV = TEMP9_PV;
            dbPara.P_TEMP9_SP = TEMP9_SP;
            dbPara.P_TEMP10_PV = TEMP10_PV;
            dbPara.P_TEMP10_SP = TEMP10_SP;
            dbPara.P_SPEED_PV = SPEED_PV;
            dbPara.P_SPEED_SP = SPEED_SP;

            dbPara.P_BECOATWIDTH = BECOATWIDTH;
            dbPara.P_FANRPM = FANRPM;
            dbPara.P_EXFAN_FRONT_BACK = EXFAN_FRONT_BACK;
            dbPara.P_EXFAN_MIDDLE = EXFAN_MIDDLE;
            dbPara.P_ANGLEKNIFE = ANGLEKNIFE;
            dbPara.P_BLADENO = BLADENO;
            dbPara.P_BLADEDIRECTION = BLADEDIRECTION;
            dbPara.P_TENSIONUP = TENSIONUP;
            dbPara.P_TENSIONDOWN = TENSIONDOWN;

            dbPara.P_FORN = FORN;
            dbPara.P_TENTER = TENTER;
            dbPara.P_PATHLINE = PATHLINE;
            dbPara.P_FEEDIN = FEEDIN;
            dbPara.P_OVERFEED = OVERFEED;
            dbPara.P_WIDTHCOAT = WIDTHCOAT;
            dbPara.P_WIDTHCOATALL = WIDTHCOATALL;
            dbPara.P_SILICONEA = SILICONEA;
            dbPara.P_SILICONEB = SILICONEB;
            dbPara.P_CWL = CWL;
            dbPara.P_CWC = CWC;
            dbPara.P_CWR = CWR;

            // เพิ่ม
            dbPara.P_HUMID_AF = HUMID_AF;
            dbPara.P_HUMID_BF = HUMID_BF;

            // เพิ่ม REPROCESS
            dbPara.P_REPROCESS = REPROCESS;
            dbPara.P_WEAVLENGTH = WEAVLENGTH;

            dbPara.P_GROUP = OPERATOR_GROUP;

            FINISHING_INSERTCOATINGResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_INSERTCOATING(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region เพิ่ม FINISHING_UPDATECOATING Processing

        public string FINISHING_UPDATECOATINGProcessing(string FINISHLOT, string operatorid, string flag
            , decimal? SATURATOR_PV, decimal? SATURATOR_SP, decimal? WASHING1_PV, decimal? WASHING1_SP
            , decimal? WASHING2_PV, decimal? WASHING2_SP, decimal? HOTFLUE_PV, decimal? HOTFLUE_SP
            , decimal? TEMP1_PV, decimal? TEMP1_SP, decimal? TEMP2_PV, decimal? TEMP2_SP
            , decimal? TEMP3_PV, decimal? TEMP3_SP, decimal? TEMP4_PV, decimal? TEMP4_SP
            , decimal? TEMP5_PV, decimal? TEMP5_SP, decimal? TEMP6_PV, decimal? TEMP6_SP
            , decimal? TEMP7_PV, decimal? TEMP7_SP, decimal? TEMP8_PV, decimal? TEMP8_SP
            , decimal? TEMP9_PV, decimal? TEMP9_SP, decimal? TEMP10_PV, decimal? TEMP10_SP
            , decimal? SPEED_PV, decimal? SPEED_SP, decimal? BECOATWIDTH, decimal? FANRPM
            , decimal? EXFAN_FRONT_BACK, decimal? EXFAN_MIDDLE, decimal? ANGLEKNIFE
            , string BLADENO, string BLADEDIRECTION, decimal? TENSIONUP
            , decimal? TENSIONDOWN, decimal? FORN, decimal? TENTER, decimal? PATHLINE
            , decimal? FEEDIN, decimal? OVERFEED, decimal? WIDTHCOAT, decimal? WIDTHCOATALL
            , string SILICONEA, string SILICONEB, decimal? CWL, decimal? CWC, decimal? CWR
            , string ITMCODE, string WEAVINGLOT, string CUSTOMER, DateTime? STARTDATE
            , decimal? HUMID_AF, decimal? HUMID_BF, string OPERATOR_GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return result;
            if (string.IsNullOrWhiteSpace(operatorid))
                return result;
            if (string.IsNullOrWhiteSpace(flag))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_UPDATECOATINGParameter dbPara = new FINISHING_UPDATECOATINGParameter();
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_FLAG = flag;
            dbPara.P_CONDITIONBY = operatorid;
            dbPara.P_CONDITONDATE = DateTime.Now;

            dbPara.P_SATURATOR_PV = SATURATOR_PV;
            dbPara.P_SATURATOR_SP = SATURATOR_SP;
            dbPara.P_WASHING1_PV = WASHING1_PV;
            dbPara.P_WASHING1_SP = WASHING1_SP;
            dbPara.P_WASHING2_PV = WASHING2_PV;
            dbPara.P_WASHING2_SP = WASHING2_SP;
            dbPara.P_HOTFLUE_PV = HOTFLUE_PV;
            dbPara.P_HOTFLUE_SP = HOTFLUE_SP;
            dbPara.P_TEMP1_PV = TEMP1_PV;
            dbPara.P_TEMP1_SP = TEMP1_SP;
            dbPara.P_TEMP2_PV = TEMP2_PV;
            dbPara.P_TEMP2_SP = TEMP2_SP;
            dbPara.P_TEMP3_PV = TEMP3_PV;
            dbPara.P_TEMP3_SP = TEMP3_SP;
            dbPara.P_TEMP4_PV = TEMP4_PV;
            dbPara.P_TEMP4_SP = TEMP4_SP;
            dbPara.P_TEMP5_PV = TEMP5_PV;
            dbPara.P_TEMP5_SP = TEMP5_SP;
            dbPara.P_TEMP6_PV = TEMP6_PV;
            dbPara.P_TEMP6_SP = TEMP6_SP;
            dbPara.P_TEMP7_PV = TEMP7_PV;
            dbPara.P_TEMP7_SP = TEMP7_SP;
            dbPara.P_TEMP8_PV = TEMP8_PV;
            dbPara.P_TEMP8_SP = TEMP8_SP;
            dbPara.P_TEMP9_PV = TEMP9_PV;
            dbPara.P_TEMP9_SP = TEMP9_SP;
            dbPara.P_TEMP10_PV = TEMP10_PV;
            dbPara.P_TEMP10_SP = TEMP10_SP;
            dbPara.P_SPEED_PV = SPEED_PV;
            dbPara.P_SPEED_SP = SPEED_SP;
            dbPara.P_BECOATWIDTH = BECOATWIDTH;
            dbPara.P_FANRPM = FANRPM;
            dbPara.P_EXFAN_FRONT_BACK = EXFAN_FRONT_BACK;
            dbPara.P_EXFAN_MIDDLE = EXFAN_MIDDLE;
            
            dbPara.P_ANGLEKNIFE = ANGLEKNIFE;
            dbPara.P_BLADENO = BLADENO;
            dbPara.P_BLADEDIRECTION = BLADEDIRECTION;
            dbPara.P_TENSIONUP = TENSIONUP;
            dbPara.P_TENSIONDOWN = TENSIONDOWN;
            dbPara.P_FORN = FORN;
            dbPara.P_TENTER = TENTER;
            dbPara.P_PATHLINE = PATHLINE;
            dbPara.P_FEEDIN = FEEDIN;
            dbPara.P_OVERFEED = OVERFEED;
            dbPara.P_WIDTHCOAT = WIDTHCOAT;
            dbPara.P_WIDTHCOATALL = WIDTHCOATALL;
            dbPara.P_SILICONEA = SILICONEA;
            dbPara.P_SILICONEB = SILICONEB;
            dbPara.P_CWL = CWL;
            dbPara.P_CWC = CWC;
            dbPara.P_CWR = CWR;

            dbPara.P_ITMCODE = ITMCODE;
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_CUSTOMER = CUSTOMER;
            dbPara.P_STARTDATE = STARTDATE;

            // เพิ่ม
            dbPara.P_HUMID_AF = HUMID_AF;
            dbPara.P_HUMID_BF = HUMID_BF;

            dbPara.P_GROUP = OPERATOR_GROUP;

            FINISHING_UPDATECOATINGResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATECOATING(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region เพิ่ม FINISHING_UPDATECOATINGDATA Finishing

        public string FINISHING_UPDATECOATINGFinishing(string FINISHLOT, string operatorid, string flag
            , decimal? SATURATOR_PV, decimal? SATURATOR_SP, decimal? WASHING1_PV, decimal? WASHING1_SP
            , decimal? WASHING2_PV, decimal? WASHING2_SP, decimal? HOTFLUE_PV, decimal? HOTFLUE_SP
            , decimal? TEMP1_PV, decimal? TEMP1_SP, decimal? TEMP2_PV, decimal? TEMP2_SP
            , decimal? TEMP3_PV, decimal? TEMP3_SP, decimal? TEMP4_PV, decimal? TEMP4_SP
            , decimal? TEMP5_PV, decimal? TEMP5_SP, decimal? TEMP6_PV, decimal? TEMP6_SP
            , decimal? TEMP7_PV, decimal? TEMP7_SP, decimal? TEMP8_PV, decimal? TEMP8_SP
            , decimal? TEMP9_PV, decimal? TEMP9_SP, decimal? TEMP10_PV, decimal? TEMP10_SP
            , decimal? SPEED_PV, decimal? SPEED_SP, decimal? BECOATWIDTH, decimal? FANRPM
            , decimal? EXFAN_FRONT_BACK, decimal? EXFAN_MIDDLE, decimal? ANGLEKNIFE
            , string BLADENO, string BLADEDIRECTION, decimal? TENSIONUP
            , decimal? TENSIONDOWN, decimal? FORN, decimal? TENTER, decimal? PATHLINE
            , decimal? FEEDIN, decimal? OVERFEED, decimal? WIDTHCOAT, decimal? WIDTHCOATALL
            , string SILICONEA, string SILICONEB, decimal? CWL, decimal? CWC, decimal? CWR
            , string ITMCODE, string WEAVINGLOT, string CUSTOMER, DateTime? STARTDATE
            , decimal? HUMID_AF, decimal? HUMID_BF
            , decimal? LENGTH1, decimal? LENGTH2, decimal? LENGTH3, decimal? LENGTH4, decimal? LENGTH5, decimal? LENGTH6, decimal? LENGTH7, string OPERATOR_GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return result;
            if (string.IsNullOrWhiteSpace(operatorid))
                return result;
            if (string.IsNullOrWhiteSpace(flag))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_UPDATECOATINGParameter dbPara = new FINISHING_UPDATECOATINGParameter();
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_FLAG = flag;
            dbPara.P_FINISHBY = operatorid;
            dbPara.P_ENDDATE = DateTime.Now;

            dbPara.P_LENGTH1 = LENGTH1;
            dbPara.P_LENGTH2 = LENGTH2;
            dbPara.P_LENGTH3 = LENGTH3;
            dbPara.P_LENGTH4 = LENGTH4;
            dbPara.P_LENGTH5 = LENGTH5;
            dbPara.P_LENGTH6 = LENGTH6;
            dbPara.P_LENGTH7 = LENGTH7;

            dbPara.P_SATURATOR_PV = SATURATOR_PV;
            dbPara.P_SATURATOR_SP = SATURATOR_SP;
            dbPara.P_WASHING1_PV = WASHING1_PV;
            dbPara.P_WASHING1_SP = WASHING1_SP;
            dbPara.P_WASHING2_PV = WASHING2_PV;
            dbPara.P_WASHING2_SP = WASHING2_SP;
            dbPara.P_HOTFLUE_PV = HOTFLUE_PV;
            dbPara.P_HOTFLUE_SP = HOTFLUE_SP;
            dbPara.P_TEMP1_PV = TEMP1_PV;
            dbPara.P_TEMP1_SP = TEMP1_SP;
            dbPara.P_TEMP2_PV = TEMP2_PV;
            dbPara.P_TEMP2_SP = TEMP2_SP;
            dbPara.P_TEMP3_PV = TEMP3_PV;
            dbPara.P_TEMP3_SP = TEMP3_SP;
            dbPara.P_TEMP4_PV = TEMP4_PV;
            dbPara.P_TEMP4_SP = TEMP4_SP;
            dbPara.P_TEMP5_PV = TEMP5_PV;
            dbPara.P_TEMP5_SP = TEMP5_SP;
            dbPara.P_TEMP6_PV = TEMP6_PV;
            dbPara.P_TEMP6_SP = TEMP6_SP;
            dbPara.P_TEMP7_PV = TEMP7_PV;
            dbPara.P_TEMP7_SP = TEMP7_SP;
            dbPara.P_TEMP8_PV = TEMP8_PV;
            dbPara.P_TEMP8_SP = TEMP8_SP;
            dbPara.P_TEMP9_PV = TEMP9_PV;
            dbPara.P_TEMP9_SP = TEMP9_SP;
            dbPara.P_TEMP10_PV = TEMP10_PV;
            dbPara.P_TEMP10_SP = TEMP10_SP;
            dbPara.P_SPEED_PV = SPEED_PV;
            dbPara.P_SPEED_SP = SPEED_SP;
            dbPara.P_BECOATWIDTH = BECOATWIDTH;
            dbPara.P_FANRPM = FANRPM;
            dbPara.P_EXFAN_FRONT_BACK = EXFAN_FRONT_BACK;
            dbPara.P_EXFAN_MIDDLE = EXFAN_MIDDLE;

            dbPara.P_ANGLEKNIFE = ANGLEKNIFE;
            dbPara.P_BLADENO = BLADENO;
            dbPara.P_BLADEDIRECTION = BLADEDIRECTION;
            dbPara.P_TENSIONUP = TENSIONUP;
            dbPara.P_TENSIONDOWN = TENSIONDOWN;
            dbPara.P_FORN = FORN;
            dbPara.P_TENTER = TENTER;
            dbPara.P_PATHLINE = PATHLINE;
            dbPara.P_FEEDIN = FEEDIN;
            dbPara.P_OVERFEED = OVERFEED;
            dbPara.P_WIDTHCOAT = WIDTHCOAT;
            dbPara.P_WIDTHCOATALL = WIDTHCOATALL;
            dbPara.P_SILICONEA = SILICONEA;
            dbPara.P_SILICONEB = SILICONEB;
            dbPara.P_CWL = CWL;
            dbPara.P_CWC = CWC;
            dbPara.P_CWR = CWR;

            dbPara.P_ITMCODE = ITMCODE;
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_CUSTOMER = CUSTOMER;
            dbPara.P_STARTDATE = STARTDATE;

            // เพิ่ม
            dbPara.P_HUMID_AF = HUMID_AF;
            dbPara.P_HUMID_BF = HUMID_BF;

            dbPara.P_GROUP = OPERATOR_GROUP;

            FINISHING_UPDATECOATINGResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATECOATING(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region เพิ่ม FINISHING_UPDATECOATINGDATA Finishing

        public string FINISHING_UPDATECOATINGDATAFinishing(string FINISHLOT, string FLAG,
            decimal? SAT, decimal? SAT_MIN, decimal? SAT_MAX,
            decimal? WASHING1, decimal? WASHING1_MIN, decimal? WASHING1_MAX,
            decimal? WASHING2, decimal? WASHING2_MIN, decimal? WASHING2_MAX,
            decimal? HOTFLUE, decimal? HOTFLUE_MIN, decimal? HOTFLUE_MAX,
            decimal? TEMP1, decimal? TEMP1_MIN, decimal? TEMP1_MAX,
            decimal? TEMP2, decimal? TEMP2_MIN, decimal? TEMP2_MAX,
            decimal? TEMP3, decimal? TEMP3_MIN, decimal? TEMP3_MAX,
            decimal? TEMP4, decimal? TEMP4_MIN, decimal? TEMP4_MAX,
            decimal? TEMP5, decimal? TEMP5_MIN, decimal? TEMP5_MAX,
            decimal? TEMP6, decimal? TEMP6_MIN, decimal? TEMP6_MAX,
            decimal? TEMP7, decimal? TEMP7_MIN, decimal? TEMP7_MAX,
            decimal? TEMP8, decimal? TEMP8_MIN, decimal? TEMP8_MAX,
            decimal? TEMP9, decimal? TEMP9_MIN, decimal? TEMP9_MAX,
            decimal? TEMP10, decimal? TEMP10_MIN, decimal? TEMP10_MAX,
            decimal? SPEED, decimal? SPEED_MIN, decimal? SPEED_MAX,
            decimal? TENSIONUP, decimal? TENSIONUP_MIN, decimal? TENSIONUP_MAX,
            decimal? TENSIONDOWN, decimal? TENSIONDOWN_MIN, decimal? TENSIONDOWN_MAX,
            decimal? LENGTH1, decimal? LENGTH2, decimal? LENGTH3, decimal? LENGTH4,
            decimal? LENGTH5, decimal? LENGTH6, decimal? LENGTH7,
            string ITMCODE, string WEAVINGLOT, string CUSTOMER,
            DateTime? STARTDATE, decimal? BECOATWIDTH, decimal? FANRPM,
            decimal? EXFAN_FRONT_BACK, decimal? EXFAN_MIDDLE, decimal? ANGLEKNIFE,
            string BLADENO, string BLADEDIRECTION, decimal? FORN, decimal? TENTER,
            decimal? PATHLINE, decimal? FEEDIN, decimal? OVERFEED, decimal? WIDTHCOAT, decimal? WIDTHCOATALL,
            string SILICONEA, string SILICONEB, decimal? CWL, decimal? CWC, decimal? CWR,
            string CONDITIONBY, string FINISHBY, DateTime? ENDDATE, DateTime? CONDITONDATE,
            string REMARK, decimal? HUMID_BF, decimal? HUMID_AF, string GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return result;
            if (string.IsNullOrWhiteSpace(FINISHBY))
                return result;
            if (string.IsNullOrWhiteSpace(FLAG))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_UPDATECOATINGDATAParameter dbPara = new FINISHING_UPDATECOATINGDATAParameter();

            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_FLAG = FLAG;
            dbPara.P_SAT = SAT;
            dbPara.P_SAT_MIN = SAT_MIN;
            dbPara.P_SAT_MAX = SAT_MAX;
            dbPara.P_WASHING1 = WASHING1;
            dbPara.P_WASHING1_MIN = WASHING1_MIN;
            dbPara.P_WASHING1_MAX = WASHING1_MAX;
            dbPara.P_WASHING2 = WASHING2;
            dbPara.P_WASHING2_MIN = WASHING2_MIN;
            dbPara.P_WASHING2_MAX = WASHING2_MAX;
            dbPara.P_HOTFLUE = HOTFLUE;
            dbPara.P_HOTFLUE_MIN = HOTFLUE_MIN;
            dbPara.P_HOTFLUE_MAX = HOTFLUE_MAX;
            dbPara.P_TEMP1 = TEMP1;
            dbPara.P_TEMP1_MIN = TEMP1_MIN;
            dbPara.P_TEMP1_MAX = TEMP1_MAX;
            dbPara.P_TEMP2 = TEMP2;
            dbPara.P_TEMP2_MIN = TEMP2_MIN;
            dbPara.P_TEMP2_MAX = TEMP2_MAX;
            dbPara.P_TEMP3 = TEMP3;
            dbPara.P_TEMP3_MIN = TEMP3_MIN;
            dbPara.P_TEMP3_MAX = TEMP3_MAX;
            dbPara.P_TEMP4 = TEMP4;
            dbPara.P_TEMP4_MIN = TEMP4_MIN;
            dbPara.P_TEMP4_MAX = TEMP4_MAX;
            dbPara.P_TEMP5 = TEMP5;
            dbPara.P_TEMP5_MIN = TEMP5_MIN;
            dbPara.P_TEMP5_MAX = TEMP5_MAX;
            dbPara.P_TEMP6 = TEMP6;
            dbPara.P_TEMP6_MIN = TEMP6_MIN;
            dbPara.P_TEMP6_MAX = TEMP6_MAX;
            dbPara.P_TEMP7 = TEMP7;
            dbPara.P_TEMP7_MIN = TEMP7_MIN;
            dbPara.P_TEMP7_MAX = TEMP7_MAX;
            dbPara.P_TEMP8 = TEMP8;
            dbPara.P_TEMP8_MIN = TEMP8_MIN;
            dbPara.P_TEMP8_MAX = TEMP8_MAX;
            dbPara.P_TEMP9 = TEMP9;
            dbPara.P_TEMP9_MIN = TEMP9_MIN;
            dbPara.P_TEMP9_MAX = TEMP9_MAX;
            dbPara.P_TEMP10 = TEMP10;
            dbPara.P_TEMP10_MIN = TEMP10_MIN;
            dbPara.P_TEMP10_MAX = TEMP10_MAX;
            dbPara.P_SPEED = SPEED;
            dbPara.P_SPEED_MIN = SPEED_MIN;
            dbPara.P_SPEED_MAX = SPEED_MAX;
            dbPara.P_TENSIONUP = TENSIONUP;
            dbPara.P_TENSIONUP_MIN = TENSIONUP_MIN;
            dbPara.P_TENSIONUP_MAX = TENSIONUP_MAX;
            dbPara.P_TENSIONDOWN = TENSIONDOWN;
            dbPara.P_TENSIONDOWN_MIN = TENSIONDOWN_MIN;
            dbPara.P_TENSIONDOWN_MAX = TENSIONDOWN_MAX;
            dbPara.P_LENGTH1 = LENGTH1;
            dbPara.P_LENGTH2 = LENGTH2;
            dbPara.P_LENGTH3 = LENGTH3;
            dbPara.P_LENGTH4 = LENGTH4;
            dbPara.P_LENGTH5 = LENGTH5;
            dbPara.P_LENGTH6 = LENGTH6;
            dbPara.P_LENGTH7 = LENGTH7;
            dbPara.P_ITMCODE = ITMCODE;
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_CUSTOMER = CUSTOMER;
            dbPara.P_STARTDATE = STARTDATE;
            dbPara.P_BECOATWIDTH = BECOATWIDTH;
            dbPara.P_FANRPM = FANRPM;
            dbPara.P_EXFAN_FRONT_BACK = EXFAN_FRONT_BACK;
            dbPara.P_EXFAN_MIDDLE = EXFAN_MIDDLE;
            dbPara.P_ANGLEKNIFE = ANGLEKNIFE;
            dbPara.P_BLADENO = BLADENO;
            dbPara.P_BLADEDIRECTION = BLADEDIRECTION;
            dbPara.P_FORN = FORN;
            dbPara.P_TENTER = TENTER;
            dbPara.P_PATHLINE = PATHLINE;
            dbPara.P_FEEDIN = FEEDIN;
            dbPara.P_OVERFEED = OVERFEED;
            dbPara.P_WIDTHCOAT = WIDTHCOAT;
            dbPara.P_WIDTHCOATALL = WIDTHCOATALL;
            dbPara.P_SILICONEA = SILICONEA;
            dbPara.P_SILICONEB = SILICONEB;
            dbPara.P_CWL = CWL;
            dbPara.P_CWC = CWC;
            dbPara.P_CWR = CWR;
            dbPara.P_CONDITIONBY = CONDITIONBY;
            dbPara.P_FINISHBY = FINISHBY;
            dbPara.P_ENDDATE = ENDDATE;
            dbPara.P_CONDITONDATE = CONDITONDATE;
            dbPara.P_REMARK = REMARK;
            dbPara.P_HUMID_BF = HUMID_BF;
            dbPara.P_HUMID_AF = HUMID_AF;
            dbPara.P_GROUP = GROUP;


            FINISHING_UPDATECOATINGDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATECOATINGDATA(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region เพิ่มใหม่ FINISHING_GETCOATINGREPORT ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETCOATINGREPORTDATA> FINISHING_GETCOATINGREPORTList(string WEAVINGLOT, string FINLOT)
        {
            List<FINISHING_GETCOATINGREPORTDATA> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETCOATINGREPORTParameter dbPara = new FINISHING_GETCOATINGREPORTParameter();
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_FINLOT = FINLOT;

            List<FINISHING_GETCOATINGREPORTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETCOATINGREPORT(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETCOATINGREPORTDATA>();
                    foreach (FINISHING_GETCOATINGREPORTResult dbResult in dbResults)
                    {
                        FINISHING_GETCOATINGREPORTDATA inst = new FINISHING_GETCOATINGREPORTDATA();

                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;
                        inst.MCNO = dbResult.MCNO;
                        inst.STATUSFLAG = dbResult.STATUSFLAG;
                        inst.SATURATOR_CHEM_PV = dbResult.SATURATOR_CHEM_PV;
                        inst.SATURATOR_CHEM_SP = dbResult.SATURATOR_CHEM_SP;
                        inst.WASHING1_PV = dbResult.WASHING1_PV;
                        inst.WASHING1_SP = dbResult.WASHING1_SP;
                        inst.WASHING2_PV = dbResult.WASHING2_PV;
                        inst.WASHING2_SP = dbResult.WASHING2_SP;
                        inst.HOTFLUE_PV = dbResult.HOTFLUE_PV;
                        inst.HOTFLUE_SP = dbResult.HOTFLUE_SP;
                        inst.BE_COATWIDTH = dbResult.BE_COATWIDTH;
                        inst.TEMP1_PV = dbResult.TEMP1_PV;
                        inst.TEMP1_SP = dbResult.TEMP1_SP;
                        inst.TEMP2_PV = dbResult.TEMP2_PV;
                        inst.TEMP2_SP = dbResult.TEMP2_SP;
                        inst.TEMP3_PV = dbResult.TEMP3_PV;
                        inst.TEMP3_SP = dbResult.TEMP3_SP;
                        inst.TEMP4_PV = dbResult.TEMP4_PV;
                        inst.TEMP4_SP = dbResult.TEMP4_SP;
                        inst.TEMP5_PV = dbResult.TEMP5_PV;
                        inst.TEMP5_SP = dbResult.TEMP5_SP;
                        inst.TEMP6_PV = dbResult.TEMP6_PV;
                        inst.TEMP6_SP = dbResult.TEMP6_SP;
                        inst.TEMP7_PV = dbResult.TEMP7_PV;
                        inst.TEMP7_SP = dbResult.TEMP7_SP;
                        inst.TEMP8_PV = dbResult.TEMP8_PV;
                        inst.TEMP8_SP = dbResult.TEMP8_SP;
                        inst.TEMP9_PV = dbResult.TEMP9_PV;
                        inst.TEMP9_SP = dbResult.TEMP9_SP;
                        inst.TEMP10_PV = dbResult.TEMP10_PV;
                        inst.TEMP10_SP = dbResult.TEMP10_SP;
                        inst.FANRPM = dbResult.FANRPM;
                        inst.EXFAN_FRONT_BACK = dbResult.EXFAN_FRONT_BACK;
                        inst.EXFAN_MIDDLE = dbResult.EXFAN_MIDDLE;
                        inst.ANGLEKNIFE = dbResult.ANGLEKNIFE;
                        inst.BLADENO = dbResult.BLADENO;
                        inst.BLADEDIRECTION = dbResult.BLADEDIRECTION;
                        inst.CYLINDER_TENSIONUP = dbResult.CYLINDER_TENSIONUP;
                        inst.OPOLE_TENSIONDOWN = dbResult.OPOLE_TENSIONDOWN;
                        inst.FRAMEWIDTH_FORN = dbResult.FRAMEWIDTH_FORN;
                        inst.FRAMEWIDTH_TENTER = dbResult.FRAMEWIDTH_TENTER;
                        inst.PATHLINE = dbResult.PATHLINE;
                        inst.FEEDIN = dbResult.FEEDIN;
                        inst.OVERFEED = dbResult.OVERFEED;
                        inst.SPEED_PV = dbResult.SPEED_PV;
                        inst.SPEED_SP = dbResult.SPEED_SP;
                        inst.WIDTHCOAT = dbResult.WIDTHCOAT;
                        inst.WIDTHCOATALL = dbResult.WIDTHCOATALL;
                        inst.SILICONE_A = dbResult.SILICONE_A;
                        inst.SILICONE_B = dbResult.SILICONE_B;
                        inst.COATINGWEIGTH_L = dbResult.COATINGWEIGTH_L;
                        inst.COATINGWEIGTH_C = dbResult.COATINGWEIGTH_C;
                        inst.COATINGWEIGTH_R = dbResult.COATINGWEIGTH_R;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.CONDITIONDATE = dbResult.CONDITIONDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.SAMPLINGID = dbResult.SAMPLINGID;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.PARTNO = dbResult.PARTNO;
                        inst.FINISHLENGTH = dbResult.FINISHLENGTH;
                        inst.INPUTLENGTH = dbResult.INPUTLENGTH;
                        inst.REMARK = dbResult.REMARK;

                        // เพิ่ม
                        inst.HUMIDITY_AF = dbResult.HUMIDITY_AF;
                        inst.HUMIDITY_BF = dbResult.HUMIDITY_BF;

                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

                        inst.REPROCESS = dbResult.REPROCESS;
                        inst.TEMP1_MIN = dbResult.TEMP1_MIN;
                        inst.TEMP1_MAX = dbResult.TEMP1_MAX;
                        inst.TEMP2_MIN = dbResult.TEMP2_MIN;
                        inst.TEMP2_MAX = dbResult.TEMP2_MAX;
                        inst.TEMP3_MIN = dbResult.TEMP3_MIN;
                        inst.TEMP3_MAX = dbResult.TEMP3_MAX;
                        inst.TEMP4_MIN = dbResult.TEMP4_MIN;
                        inst.TEMP4_MAX = dbResult.TEMP4_MAX;
                        inst.TEMP5_MIN = dbResult.TEMP5_MIN;
                        inst.TEMP5_MAX = dbResult.TEMP5_MAX;
                        inst.TEMP6_MIN = dbResult.TEMP6_MIN;
                        inst.TEMP6_MAX = dbResult.TEMP6_MAX;
                        inst.TEMP7_MIN = dbResult.TEMP7_MIN;
                        inst.TEMP7_MAX = dbResult.TEMP7_MAX;
                        inst.TEMP8_MIN = dbResult.TEMP8_MIN;
                        inst.TEMP8_MAX = dbResult.TEMP8_MAX;
                        inst.TEMP9_MIN = dbResult.TEMP9_MIN;
                        inst.TEMP9_MAX = dbResult.TEMP9_MAX;
                        inst.TEMP10_MIN = dbResult.TEMP10_MIN;
                        inst.TEMP10_MAX = dbResult.TEMP10_MAX;
                        inst.SAT_CHEM_MIN = dbResult.SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = dbResult.SAT_CHEM_MAX;
                        inst.WASHING1_MIN = dbResult.WASHING1_MIN;
                        inst.WASHING1_MAX = dbResult.WASHING1_MAX;
                        inst.WASHING2_MIN = dbResult.WASHING2_MIN;
                        inst.WASHING2_MAX = dbResult.WASHING2_MAX;
                        inst.HOTFLUE_MIN = dbResult.HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = dbResult.HOTFLUE_MAX;
                        inst.SPEED_MIN = dbResult.SPEED_MIN;
                        inst.SPEED_MAX = dbResult.SPEED_MAX;
                        inst.TENSIONUP_MIN = dbResult.TENSIONUP_MIN;
                        inst.TENSIONUP_MAX = dbResult.TENSIONUP_MAX;
                        inst.TENSIONDOWN_MIN = dbResult.TENSIONDOWN_MIN;
                        inst.TENSIONDOWN_MAX = dbResult.TENSIONDOWN_MAX;

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

        #region เพิ่มใหม่ FINISHING_GETCoatingMCNO ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string FINISHING_GETCoatingMCNO(string WEAVINGLOT, string FINLOT)
        {
            string results = string.Empty;

            if (!HasConnection())
                return results;

            FINISHING_GETCOATINGREPORTParameter dbPara = new FINISHING_GETCOATINGREPORTParameter();
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_FINLOT = FINLOT;

            List<FINISHING_GETCOATINGREPORTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETCOATINGREPORT(dbPara);
                if (null != dbResults)
                {
                    results = dbResults[0].MCNO; 
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region AddCoatingRemark
        public void AddCoatingRemark(string FINISHLOT, string ItemCode, string remark)
        {
            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return;

            if (!HasConnection())
                return;

            FINISHING_UPDATECOATINGParameter dbPara = new FINISHING_UPDATECOATINGParameter();
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_ITMCODE = ItemCode;

            dbPara.P_REMARK = remark;

            FINISHING_UPDATECOATINGResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATECOATING(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #endregion

        #region SCOURING

        #region เพิ่มใหม่ GetFINISHING_GETSCOURINGCONDITIONDataList ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETSCOURINGCONDITIONData> GetFINISHING_GETSCOURINGCONDITIONDataList(string itm_code, string ScouringNo)
        {
            List<FINISHING_GETSCOURINGCONDITIONData> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETSCOURINGCONDITIONParameter dbPara = new FINISHING_GETSCOURINGCONDITIONParameter();
            dbPara.P_ITEMCODE = itm_code;
            dbPara.P_SCOURINGNO = ScouringNo;

            List<FINISHING_GETSCOURINGCONDITIONResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETSCOURINGCONDITION(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETSCOURINGCONDITIONData>();
                    foreach (FINISHING_GETSCOURINGCONDITIONResult dbResult in dbResults)
                    {
                        FINISHING_GETSCOURINGCONDITIONData inst = new FINISHING_GETSCOURINGCONDITIONData();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.SATURATOR_CHEM = dbResult.SATURATOR_CHEM;
                        inst.SATURATOR_CHEM_MARGIN = dbResult.SATURATOR_CHEM_MARGIN;
                        inst.WASHING1 = dbResult.WASHING1;
                        inst.WASHING1_MARGIN = dbResult.WASHING1_MARGIN;
                        inst.WASHING2 = dbResult.WASHING2;
                        inst.WASHING2_MARGIN = dbResult.WASHING2_MARGIN;
                        inst.HOTFLUE = dbResult.HOTFLUE;
                        inst.HOTFLUE_MARGIN = dbResult.HOTFLUE_MARGIN;
                        inst.ROOMTEMP = dbResult.ROOMTEMP;
                        inst.ROOMTEMP_MARGIN = dbResult.ROOMTEMP_MARGIN;
                        inst.SPEED = dbResult.SPEED;
                        inst.SPEED_MARGIN = dbResult.SPEED_MARGIN;
                        inst.MAINFRAMEWIDTH = dbResult.MAINFRAMEWIDTH;
                        inst.MAINFRAMEWIDTH_MARGIN = dbResult.MAINFRAMEWIDTH_MARGIN;
                        inst.WIDTH_BE = dbResult.WIDTH_BE;
                        inst.WIDTH_BE_MARGIN = dbResult.WIDTH_BE_MARGIN;
                        inst.WIDTH_AF = dbResult.WIDTH_AF;
                        inst.WIDTH_AF_MARGIN = dbResult.WIDTH_AF_MARGIN;
                        inst.DENSITY_AF = dbResult.DENSITY_AF;
                        inst.DENSITY_MARGIN = dbResult.DENSITY_MARGIN;
                        inst.SCOURINGNO = dbResult.SCOURINGNO;

                        inst.NIPCHEMICAL = dbResult.NIPCHEMICAL;
                        inst.NIPROLLWASHER1 = dbResult.NIPROLLWASHER1;
                        inst.NIPROLLWASHER2 = dbResult.NIPROLLWASHER2;

                        inst.PIN2PIN = dbResult.PIN2PIN;
                        inst.PIN2PIN_MARGIN = dbResult.PIN2PIN_MARGIN;

                        // เพิ่มใหม่
                        inst.HUMIDITY_MIN = dbResult.HUMIDITY_MIN;
                        inst.HUMIDITY_MAX = dbResult.HUMIDITY_MAX;

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

        #region เพิ่มใหม่ FINISHING_GETSCOURINGDATA ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETSCOURINGDATA> FINISHING_GETSCOURINGDATAList(string mcno, string flag)
        {
            List<FINISHING_GETSCOURINGDATA> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETSCOURINGDATAParameter dbPara = new FINISHING_GETSCOURINGDATAParameter();
            dbPara.P_MCNO = mcno;
            dbPara.P_FLAG = flag;

            List<FINISHING_GETSCOURINGDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETSCOURINGDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETSCOURINGDATA>();
                    foreach (FINISHING_GETSCOURINGDATAResult dbResult in dbResults)
                    {
                        FINISHING_GETSCOURINGDATA inst = new FINISHING_GETSCOURINGDATA();

                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;
                        inst.MCNO = dbResult.MCNO;
                        inst.STATUSFLAG = dbResult.STATUSFLAG;
                        inst.SATURATOR_CHEM_PV = dbResult.SATURATOR_CHEM_PV;
                        inst.SATURATOR_CHEM_SP = dbResult.SATURATOR_CHEM_SP;
                        inst.WASHING1_PV = dbResult.WASHING1_PV;
                        inst.WASHING1_SP = dbResult.WASHING1_SP;
                        inst.WASHING2_PV = dbResult.WASHING2_PV;
                        inst.WASHING2_SP = dbResult.WASHING2_SP;
                        inst.HOTFLUE_PV = dbResult.HOTFLUE_PV;
                        inst.HOTFLUE_SP = dbResult.HOTFLUE_SP;
                        inst.TEMP1_PV = dbResult.TEMP1_PV;
                        inst.TEMP1_SP = dbResult.TEMP1_SP;
                        inst.TEMP2_PV = dbResult.TEMP2_PV;
                        inst.TEMP2_SP = dbResult.TEMP2_SP;
                        inst.TEMP3_PV = dbResult.TEMP3_PV;
                        inst.TEMP3_SP = dbResult.TEMP3_SP;
                        inst.TEMP4_PV = dbResult.TEMP4_PV;
                        inst.TEMP4_SP = dbResult.TEMP4_SP;
                        inst.TEMP5_PV = dbResult.TEMP5_PV;
                        inst.TEMP5_SP = dbResult.TEMP5_SP;
                        inst.TEMP6_PV = dbResult.TEMP6_PV;
                        inst.TEMP6_SP = dbResult.TEMP6_SP;
                        inst.TEMP7_PV = dbResult.TEMP7_PV;
                        inst.TEMP7_SP = dbResult.TEMP7_SP;
                        inst.TEMP8_PV = dbResult.TEMP8_PV;
                        inst.TEMP8_SP = dbResult.TEMP8_SP;

                        inst.TEMP9_PV = dbResult.TEMP9_PV;
                        inst.TEMP9_SP = dbResult.TEMP9_SP;
                        inst.TEMP10_PV = dbResult.TEMP10_PV;
                        inst.TEMP10_SP = dbResult.TEMP10_SP;

                        inst.SPEED_PV = dbResult.SPEED_PV;
                        inst.SPEED_SP = dbResult.SPEED_SP;
                        inst.MAINFRAMEWIDTH = dbResult.MAINFRAMEWIDTH;
                        inst.WIDTH_BE = dbResult.WIDTH_BE;
                        inst.WIDTH_AF = dbResult.WIDTH_AF;
                        inst.PIN2PIN = dbResult.PIN2PIN;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.CONDITIONDATE = dbResult.CONDITIONDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.SAMPLINGID = dbResult.SAMPLINGID;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.WEAVINGLENGTH = dbResult.WEAVINGLENGTH;
                        inst.REMARK = dbResult.REMARK;

                        // เพิ่ม
                        inst.HUMIDITY_AF = dbResult.HUMIDITY_AF;
                        inst.HUMIDITY_BF = dbResult.HUMIDITY_BF;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

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

        #region เพิ่มใหม่ GetFINISHING_SCOURINGDATABYLOT ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_SCOURINGDATABYLOT> GetFINISHING_SCOURINGDATABYLOT(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_SCOURINGDATABYLOT> results = null;

            if (!HasConnection())
                return results;

            FINISHING_SCOURINGDATABYLOTParameter dbPara = new FINISHING_SCOURINGDATABYLOTParameter();
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<FINISHING_SCOURINGDATABYLOTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_SCOURINGDATABYLOT(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_SCOURINGDATABYLOT>();
                    foreach (FINISHING_SCOURINGDATABYLOTResult dbResult in dbResults)
                    {
                        FINISHING_SCOURINGDATABYLOT inst = new FINISHING_SCOURINGDATABYLOT();

                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.MCNO = dbResult.MCNO;
                        inst.STATUSFLAG = dbResult.STATUSFLAG;
                        inst.SATURATOR_CHEM_PV = dbResult.SATURATOR_CHEM_PV;
                        inst.SATURATOR_CHEM_SP = dbResult.SATURATOR_CHEM_SP;
                        inst.WASHING1_PV = dbResult.WASHING1_PV;
                        inst.WASHING1_SP = dbResult.WASHING1_SP;
                        inst.WASHING2_PV = dbResult.WASHING2_PV;
                        inst.WASHING2_SP = dbResult.WASHING2_SP;
                        inst.HOTFLUE_PV = dbResult.HOTFLUE_PV;
                        inst.HOTFLUE_SP = dbResult.HOTFLUE_SP;
                        inst.TEMP1_PV = dbResult.TEMP1_PV;
                        inst.TEMP1_SP = dbResult.TEMP1_SP;
                        inst.TEMP2_PV = dbResult.TEMP2_PV;
                        inst.TEMP2_SP = dbResult.TEMP2_SP;
                        inst.TEMP3_PV = dbResult.TEMP3_PV;
                        inst.TEMP3_SP = dbResult.TEMP3_SP;
                        inst.TEMP4_PV = dbResult.TEMP4_PV;
                        inst.TEMP4_SP = dbResult.TEMP4_SP;
                        inst.TEMP5_PV = dbResult.TEMP5_PV;
                        inst.TEMP5_SP = dbResult.TEMP5_SP;
                        inst.TEMP6_PV = dbResult.TEMP6_PV;
                        inst.TEMP6_SP = dbResult.TEMP6_SP;
                        inst.TEMP7_PV = dbResult.TEMP7_PV;
                        inst.TEMP7_SP = dbResult.TEMP7_SP;
                        inst.TEMP8_PV = dbResult.TEMP8_PV;
                        inst.TEMP8_SP = dbResult.TEMP8_SP;

                        inst.TEMP9_PV = dbResult.TEMP9_PV;
                        inst.TEMP9_SP = dbResult.TEMP9_SP;
                        inst.TEMP10_PV = dbResult.TEMP10_PV;
                        inst.TEMP10_SP = dbResult.TEMP10_SP;

                        inst.SPEED_PV = dbResult.SPEED_PV;
                        inst.SPEED_SP = dbResult.SPEED_SP;
                        inst.MAINFRAMEWIDTH = dbResult.MAINFRAMEWIDTH;
                        inst.WIDTH_BE = dbResult.WIDTH_BE;
                        inst.WIDTH_AF = dbResult.WIDTH_AF;
                        inst.PIN2PIN = dbResult.PIN2PIN;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.CONDITIONDATE = dbResult.CONDITIONDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.SAMPLINGID = dbResult.SAMPLINGID;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;
                        inst.REMARK = dbResult.REMARK;
                        inst.HUMIDITY_BF = dbResult.HUMIDITY_BF;
                        inst.HUMIDITY_AF = dbResult.HUMIDITY_AF;
                        inst.REPROCESS = dbResult.REPROCESS;
                        inst.WEAVLENGTH = dbResult.WEAVLENGTH;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

                        inst.TEMP1_MIN = dbResult.TEMP1_MIN;
                        inst.TEMP1_MAX = dbResult.TEMP1_MAX;
                        inst.TEMP2_MIN = dbResult.TEMP2_MIN;
                        inst.TEMP2_MAX = dbResult.TEMP2_MAX;
                        inst.TEMP3_MIN = dbResult.TEMP3_MIN;
                        inst.TEMP3_MAX = dbResult.TEMP3_MAX;
                        inst.TEMP4_MIN = dbResult.TEMP4_MIN;
                        inst.TEMP4_MAX = dbResult.TEMP4_MAX;
                        inst.TEMP5_MIN = dbResult.TEMP5_MIN;
                        inst.TEMP5_MAX = dbResult.TEMP5_MAX;
                        inst.TEMP6_MIN = dbResult.TEMP6_MIN;
                        inst.TEMP6_MAX = dbResult.TEMP6_MAX;
                        inst.TEMP7_MIN = dbResult.TEMP7_MIN;
                        inst.TEMP7_MAX = dbResult.TEMP7_MAX;
                        inst.TEMP8_MIN = dbResult.TEMP8_MIN;
                        inst.TEMP8_MAX = dbResult.TEMP8_MAX;
                        inst.SAT_CHEM_MIN = dbResult.SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = dbResult.SAT_CHEM_MAX;
                        inst.WASHING1_MIN = dbResult.WASHING1_MIN;
                        inst.WASHING1_MAX = dbResult.WASHING1_MAX;
                        inst.WASHING2_MIN = dbResult.WASHING2_MIN;
                        inst.WASHING2_MAX = dbResult.WASHING2_MAX;
                        inst.HOTFLUE_MIN = dbResult.HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = dbResult.HOTFLUE_MAX;
                        inst.SPEED_MIN = dbResult.SPEED_MIN;
                        inst.SPEED_MAX = dbResult.SPEED_MAX;
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

        #region เพิ่มใหม่ FINISHING_SCOURINGPLCDATA ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_SCOURINGPLCDATA> FINISHING_SCOURINGPLCDATA(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_SCOURINGPLCDATA> results = null;

            if (!HasConnection())
                return results;

            FINISHING_SCOURINGPLCDATAParameter dbPara = new FINISHING_SCOURINGPLCDATAParameter();
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<FINISHING_SCOURINGPLCDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_SCOURINGPLCDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_SCOURINGPLCDATA>();
                    foreach (FINISHING_SCOURINGPLCDATAResult dbResult in dbResults)
                    {
                        FINISHING_SCOURINGPLCDATA inst = new FINISHING_SCOURINGPLCDATA();

                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.TEMP1_MIN = dbResult.TEMP1_MIN;
                        inst.TEMP1_MAX = dbResult.TEMP1_MAX;
                        inst.TEMP1 = dbResult.TEMP1;
                        inst.TEMP2_MIN = dbResult.TEMP2_MIN;
                        inst.TEMP2_MAX = dbResult.TEMP2_MAX;
                        inst.TEMP2 = dbResult.TEMP2;
                        inst.TEMP3_MIN = dbResult.TEMP3_MIN;
                        inst.TEMP3_MAX = dbResult.TEMP3_MAX;
                        inst.TEMP3 = dbResult.TEMP3;
                        inst.TEMP4_MIN = dbResult.TEMP4_MIN;
                        inst.TEMP4_MAX = dbResult.TEMP4_MAX;
                        inst.TEMP4 = dbResult.TEMP4;
                        inst.TEMP5_MIN = dbResult.TEMP5_MIN;
                        inst.TEMP5_MAX = dbResult.TEMP5_MAX;
                        inst.TEMP5 = dbResult.TEMP5;
                        inst.TEMP6_MIN = dbResult.TEMP6_MIN;
                        inst.TEMP6_MAX = dbResult.TEMP6_MAX;
                        inst.TEMP6 = dbResult.TEMP6;
                        inst.TEMP7_MIN = dbResult.TEMP7_MIN;
                        inst.TEMP7_MAX = dbResult.TEMP7_MAX;
                        inst.TEMP7 = dbResult.TEMP7;
                        inst.TEMP8_MIN = dbResult.TEMP8_MIN;
                        inst.TEMP8_MAX = dbResult.TEMP8_MAX;
                        inst.TEMP8 = dbResult.TEMP8;

                        inst.TEMP9 = dbResult.TEMP9;
                        inst.TEMP10 = dbResult.TEMP10;

                        inst.SPEED_MIN = dbResult.SPEED_MIN;
                        inst.SPEED_MAX = dbResult.SPEED_MAX;
                        inst.SPEED = dbResult.SPEED;
                        inst.SAT_MIN = dbResult.SAT_MIN;
                        inst.SAT_MAX = dbResult.SAT_MAX;
                        inst.SAT = dbResult.SAT;
                        inst.HOTF_MIN = dbResult.HOTF_MIN;
                        inst.HOTF_MAX = dbResult.HOTF_MAX;
                        inst.HOTF = dbResult.HOTF;
                        inst.WASH1_MIN = dbResult.WASH1_MIN;
                        inst.WASH1_MAX = dbResult.WASH1_MAX;
                        inst.WASH1 = dbResult.WASH1;
                        inst.WASH2_MIN = dbResult.WASH2_MIN;
                        inst.WASH2_MAX = dbResult.WASH2_MAX;
                        inst.WASH2 = dbResult.WASH2;

                        inst.TEMP9 = dbResult.TEMP9;
                        inst.TEMP10 = dbResult.TEMP10;

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

        // เปลี่ยน ค่า Return =  string result 
        #region เพิ่ม FINISHING_INSERTSCOURING

        public string FINISHING_INSERTSCOURING(string weavlot, string itmCode, string finishcustomer, string PRODUCTTYPEID, string operatorid, string MCNO, string flag
            , decimal? SATURATOR_PV, decimal? SATURATOR_SP, decimal? WASHING1_PV, decimal? WASHING1_SP
            , decimal? WASHING2_PV, decimal? WASHING2_SP, decimal? HOTFLUE_PV, decimal? HOTFLUE_SP
            , decimal? TEMP1_PV, decimal? TEMP1_SP, decimal? TEMP2_PV, decimal? TEMP2_SP
            , decimal? TEMP3_PV, decimal? TEMP3_SP, decimal? TEMP4_PV, decimal? TEMP4_SP
            , decimal? TEMP5_PV, decimal? TEMP5_SP, decimal? TEMP6_PV, decimal? TEMP6_SP
            , decimal? TEMP7_PV, decimal? TEMP7_SP, decimal? TEMP8_PV, decimal? TEMP8_SP
            , decimal? TEMP9_PV, decimal? TEMP9_SP, decimal? TEMP10_PV, decimal? TEMP10_SP
            , decimal? SPEED_PV, decimal? SPEED_SP, decimal? MAINFRAMEWIDTH, decimal? WIDTH_BE
            , decimal? WIDTH_AF, decimal? PIN2PIN
            , decimal? HUMID_AF, decimal? HUMID_BF
            , string REPROCESS, decimal? WEAVLENGTH, string OPERATOR_GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(weavlot))
                return result;
            if (string.IsNullOrWhiteSpace(itmCode))
                return result;
            if (string.IsNullOrWhiteSpace(finishcustomer))
                return result;
            if (string.IsNullOrWhiteSpace(PRODUCTTYPEID))
                return result;
            if (string.IsNullOrWhiteSpace(operatorid))
                return result;
            if (string.IsNullOrWhiteSpace(MCNO))
                return result;
            if (string.IsNullOrWhiteSpace(flag))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_INSERTSCOURINGParameter dbPara = new FINISHING_INSERTSCOURINGParameter();
            dbPara.P_WEAVLOT = weavlot;
            dbPara.P_ITMCODE = itmCode;
            dbPara.P_FINISHCUSTOMER = finishcustomer;
            dbPara.P_PRODUCTTYPEID = PRODUCTTYPEID;
            dbPara.P_OPERATORID = operatorid;
            dbPara.P_MCNO = MCNO;
            dbPara.P_FLAG = flag;
            dbPara.P_SATURATOR_PV = SATURATOR_PV;
            dbPara.P_SATURATOR_SP = SATURATOR_SP;
            dbPara.P_WASHING1_PV = WASHING1_PV;
            dbPara.P_WASHING1_SP = WASHING1_SP;
            dbPara.P_WASHING2_PV = WASHING2_PV;
            dbPara.P_WASHING2_SP = WASHING2_SP;
            dbPara.P_HOTFLUE_PV = HOTFLUE_PV;
            dbPara.P_HOTFLUE_SP = HOTFLUE_SP;
            dbPara.P_TEMP1_PV = TEMP1_PV;
            dbPara.P_TEMP1_SP = TEMP1_SP;
            dbPara.P_TEMP2_PV = TEMP2_PV;
            dbPara.P_TEMP2_SP = TEMP2_SP;
            dbPara.P_TEMP3_PV = TEMP3_PV;
            dbPara.P_TEMP3_SP = TEMP3_SP;
            dbPara.P_TEMP4_PV = TEMP4_PV;
            dbPara.P_TEMP4_SP = TEMP4_SP;
            dbPara.P_TEMP5_PV = TEMP5_PV;
            dbPara.P_TEMP5_SP = TEMP5_SP;
            dbPara.P_TEMP6_PV = TEMP6_PV;
            dbPara.P_TEMP6_SP = TEMP6_SP;
            dbPara.P_TEMP7_PV = TEMP7_PV;
            dbPara.P_TEMP7_SP = TEMP7_SP;
            dbPara.P_TEMP8_PV = TEMP8_PV;
            dbPara.P_TEMP8_SP = TEMP8_SP;

            dbPara.P_TEMP9_PV = TEMP9_PV;
            dbPara.P_TEMP9_SP = TEMP9_SP;
            dbPara.P_TEMP10_PV = TEMP10_PV;
            dbPara.P_TEMP10_SP = TEMP10_SP;

            dbPara.P_SPEED_PV = SPEED_PV;
            dbPara.P_SPEED_SP = SPEED_SP;
            dbPara.P_MAINFRAMEWIDTH = MAINFRAMEWIDTH;
            dbPara.P_WIDTH_BE = WIDTH_BE;
            dbPara.P_WIDTH_AF = WIDTH_AF;
            dbPara.P_PIN2PIN = PIN2PIN;

            // เพิ่ม
            dbPara.P_HUMID_AF = HUMID_AF;
            dbPara.P_HUMID_BF = HUMID_BF;

            // เพิ่ม REPROCESS
            dbPara.P_REPROCESS = REPROCESS;
            dbPara.P_WEAVLENGTH = WEAVLENGTH;
            dbPara.P_GROUP = OPERATOR_GROUP;

            FINISHING_INSERTSCOURINGResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_INSERTSCOURING(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region เพิ่ม FINISHING_UPDATESCOURING Processing

        public string FINISHING_UPDATESCOURINGProcessing(string FINISHLOT, string operatorid, string flag
            , decimal? SATURATOR_PV, decimal? SATURATOR_SP, decimal? WASHING1_PV, decimal? WASHING1_SP
            , decimal? WASHING2_PV, decimal? WASHING2_SP, decimal? HOTFLUE_PV, decimal? HOTFLUE_SP
            , decimal? TEMP1_PV, decimal? TEMP1_SP, decimal? TEMP2_PV, decimal? TEMP2_SP
            , decimal? TEMP3_PV, decimal? TEMP3_SP, decimal? TEMP4_PV, decimal? TEMP4_SP
            , decimal? TEMP5_PV, decimal? TEMP5_SP, decimal? TEMP6_PV, decimal? TEMP6_SP
            , decimal? TEMP7_PV, decimal? TEMP7_SP, decimal? TEMP8_PV, decimal? TEMP8_SP
            , decimal? TEMP9_PV, decimal? TEMP9_SP, decimal? TEMP10_PV, decimal? TEMP10_SP
            , decimal? SPEED_PV, decimal? SPEED_SP, decimal? MAINFRAMEWIDTH, decimal? WIDTH_BE
            , decimal? WIDTH_AF, decimal? PIN2PIN
            , string ITMCODE, string WEAVINGLOT, string CUSTOMER, DateTime? STARTDATE
            , decimal? HUMID_AF, decimal? HUMID_BF, string OPERATOR_GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return result;
            if (string.IsNullOrWhiteSpace(operatorid))
                return result;
            if (string.IsNullOrWhiteSpace(flag))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_UPDATESCOURINGParameter dbPara = new FINISHING_UPDATESCOURINGParameter();
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_FLAG = flag;
            dbPara.P_CONDITIONBY = operatorid;
            dbPara.P_CONDITONDATE = DateTime.Now;

            dbPara.P_SATURATOR_PV = SATURATOR_PV;
            dbPara.P_SATURATOR_SP = SATURATOR_SP;
            dbPara.P_WASHING1_PV = WASHING1_PV;
            dbPara.P_WASHING1_SP = WASHING1_SP;
            dbPara.P_WASHING2_PV = WASHING2_PV;
            dbPara.P_WASHING2_SP = WASHING2_SP;
            dbPara.P_HOTFLUE_PV = HOTFLUE_PV;
            dbPara.P_HOTFLUE_SP = HOTFLUE_SP;
            dbPara.P_TEMP1_PV = TEMP1_PV;
            dbPara.P_TEMP1_SP = TEMP1_SP;
            dbPara.P_TEMP2_PV = TEMP2_PV;
            dbPara.P_TEMP2_SP = TEMP2_SP;
            dbPara.P_TEMP3_PV = TEMP3_PV;
            dbPara.P_TEMP3_SP = TEMP3_SP;
            dbPara.P_TEMP4_PV = TEMP4_PV;
            dbPara.P_TEMP4_SP = TEMP4_SP;
            dbPara.P_TEMP5_PV = TEMP5_PV;
            dbPara.P_TEMP5_SP = TEMP5_SP;
            dbPara.P_TEMP6_PV = TEMP6_PV;
            dbPara.P_TEMP6_SP = TEMP6_SP;
            dbPara.P_TEMP7_PV = TEMP7_PV;
            dbPara.P_TEMP7_SP = TEMP7_SP;
            dbPara.P_TEMP8_PV = TEMP8_PV;
            dbPara.P_TEMP8_SP = TEMP8_SP;

            dbPara.P_TEMP9_PV = TEMP9_PV;
            dbPara.P_TEMP9_SP = TEMP9_SP;
            dbPara.P_TEMP10_PV = TEMP10_PV;
            dbPara.P_TEMP10_SP = TEMP10_SP;

            dbPara.P_SPEED_PV = SPEED_PV;
            dbPara.P_SPEED_SP = SPEED_SP;
            dbPara.P_MAINFRAMEWIDTH = MAINFRAMEWIDTH;
            dbPara.P_WIDTH_BE = WIDTH_BE;
            dbPara.P_WIDTH_AF = WIDTH_AF;
            dbPara.P_PIN2PIN = PIN2PIN;

            dbPara.P_ITMCODE = ITMCODE;
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_CUSTOMER = CUSTOMER;
            dbPara.P_STARTDATE = STARTDATE;

            //dbPara.P_FINISHBY = null;
            //dbPara.P_ENDDATE = null;
            //dbPara.P_LENGTH1 = null;
            //dbPara.P_LENGTH2 = null;
            //dbPara.P_LENGTH3 = null;
            //dbPara.P_LENGTH4 = null;
            //dbPara.P_LENGTH5 = null;

            // เพิ่ม
            dbPara.P_HUMID_AF = HUMID_AF;
            dbPara.P_HUMID_BF = HUMID_BF;
            dbPara.P_GROUP = OPERATOR_GROUP;

            FINISHING_UPDATESCOURINGResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATESCOURING(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region เพิ่ม FINISHING_UPDATESCOURING Finishing

        public string FINISHING_UPDATESCOURINGFinishing(string FINISHLOT, string operatorid, string flag
            , decimal? SATURATOR_PV, decimal? SATURATOR_SP, decimal? WASHING1_PV, decimal? WASHING1_SP
            , decimal? WASHING2_PV, decimal? WASHING2_SP, decimal? HOTFLUE_PV, decimal? HOTFLUE_SP
            , decimal? TEMP1_PV, decimal? TEMP1_SP, decimal? TEMP2_PV, decimal? TEMP2_SP
            , decimal? TEMP3_PV, decimal? TEMP3_SP, decimal? TEMP4_PV, decimal? TEMP4_SP
            , decimal? TEMP5_PV, decimal? TEMP5_SP, decimal? TEMP6_PV, decimal? TEMP6_SP
            , decimal? TEMP7_PV, decimal? TEMP7_SP, decimal? TEMP8_PV, decimal? TEMP8_SP
            , decimal? TEMP9_PV, decimal? TEMP9_SP, decimal? TEMP10_PV, decimal? TEMP10_SP
            , decimal? SPEED_PV, decimal? SPEED_SP, decimal? MAINFRAMEWIDTH, decimal? WIDTH_BE
            , decimal? WIDTH_AF, decimal? PIN2PIN
            , string ITMCODE, string WEAVINGLOT, string CUSTOMER, DateTime? STARTDATE
            , decimal? HUMID_AF, decimal? HUMID_BF
            , decimal? LENGTH1, decimal? LENGTH2, decimal? LENGTH3, decimal? LENGTH4, decimal? LENGTH5, decimal? LENGTH6, decimal? LENGTH7, string OPERATOR_GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return result;
            if (string.IsNullOrWhiteSpace(operatorid))
                return result;
            if (string.IsNullOrWhiteSpace(flag))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_UPDATESCOURINGParameter dbPara = new FINISHING_UPDATESCOURINGParameter();
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_FLAG = flag;
            dbPara.P_FINISHBY = operatorid;
            dbPara.P_ENDDATE = DateTime.Now;

            dbPara.P_LENGTH1 = LENGTH1;
            dbPara.P_LENGTH2 = LENGTH2;
            dbPara.P_LENGTH3 = LENGTH3;
            dbPara.P_LENGTH4 = LENGTH4;
            dbPara.P_LENGTH5 = LENGTH5;
            dbPara.P_LENGTH6 = LENGTH6;
            dbPara.P_LENGTH7 = LENGTH7;

            dbPara.P_SATURATOR_PV = SATURATOR_PV;
            dbPara.P_SATURATOR_SP = SATURATOR_SP;
            dbPara.P_WASHING1_PV = WASHING1_PV;
            dbPara.P_WASHING1_SP = WASHING1_SP;
            dbPara.P_WASHING2_PV = WASHING2_PV;
            dbPara.P_WASHING2_SP = WASHING2_SP;
            dbPara.P_HOTFLUE_PV = HOTFLUE_PV;
            dbPara.P_HOTFLUE_SP = HOTFLUE_SP;
            dbPara.P_TEMP1_PV = TEMP1_PV;
            dbPara.P_TEMP1_SP = TEMP1_SP;
            dbPara.P_TEMP2_PV = TEMP2_PV;
            dbPara.P_TEMP2_SP = TEMP2_SP;
            dbPara.P_TEMP3_PV = TEMP3_PV;
            dbPara.P_TEMP3_SP = TEMP3_SP;
            dbPara.P_TEMP4_PV = TEMP4_PV;
            dbPara.P_TEMP4_SP = TEMP4_SP;
            dbPara.P_TEMP5_PV = TEMP5_PV;
            dbPara.P_TEMP5_SP = TEMP5_SP;
            dbPara.P_TEMP6_PV = TEMP6_PV;
            dbPara.P_TEMP6_SP = TEMP6_SP;
            dbPara.P_TEMP7_PV = TEMP7_PV;
            dbPara.P_TEMP7_SP = TEMP7_SP;
            dbPara.P_TEMP8_PV = TEMP8_PV;
            dbPara.P_TEMP8_SP = TEMP8_SP;

            dbPara.P_TEMP9_PV = TEMP9_PV;
            dbPara.P_TEMP9_SP = TEMP9_SP;
            dbPara.P_TEMP10_PV = TEMP10_PV;
            dbPara.P_TEMP10_SP = TEMP10_SP;

            dbPara.P_SPEED_PV = SPEED_PV;
            dbPara.P_SPEED_SP = SPEED_SP;
            dbPara.P_MAINFRAMEWIDTH = MAINFRAMEWIDTH;
            dbPara.P_WIDTH_BE = WIDTH_BE;
            dbPara.P_WIDTH_AF = WIDTH_AF;
            dbPara.P_PIN2PIN = PIN2PIN;

            dbPara.P_ITMCODE = ITMCODE;
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_CUSTOMER = CUSTOMER;
            dbPara.P_STARTDATE = STARTDATE;

            //dbPara.P_CONDITIONBY = "";
            //dbPara.P_CONDITONDATE = null;

            // เพิ่ม
            dbPara.P_HUMID_AF = HUMID_AF;
            dbPara.P_HUMID_BF = HUMID_BF;
            dbPara.P_GROUP = OPERATOR_GROUP;

            FINISHING_UPDATESCOURINGResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATESCOURING(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region เพิ่ม FINISHING_UPDATESCOURINGDATA Finishing

        public string FINISHING_UPDATESCOURINGDATA(string P_FINISHLOT, string P_FLAG,
                    decimal? P_SAT,decimal? P_SAT_MIN,decimal? P_SAT_MAX,
                    decimal? P_WASHING1,decimal? P_WASHING1_MIN,decimal? P_WASHING1_MAX,
                    decimal? P_WASHING2,decimal? P_WASHING2_MIN,decimal? P_WASHING2_MAX,
                    decimal? P_HOTFLUE,decimal? P_HOTFLUE_MIN,decimal? P_HOTFLUE_MAX,
                    decimal? P_TEMP1,decimal? P_TEMP1_MIN,decimal? P_TEMP1_MAX,
                    decimal? P_TEMP2,decimal? P_TEMP2_MIN,decimal? P_TEMP2_MAX,
                    decimal? P_TEMP3,decimal? P_TEMP3_MIN,decimal? P_TEMP3_MAX,
                    decimal? P_TEMP4,decimal? P_TEMP4_MIN,decimal? P_TEMP4_MAX,
                    decimal? P_TEMP5,decimal? P_TEMP5_MIN,decimal? P_TEMP5_MAX,
                    decimal? P_TEMP6,decimal? P_TEMP6_MIN,decimal? P_TEMP6_MAX,
                    decimal? P_TEMP7,decimal? P_TEMP7_MIN,decimal? P_TEMP7_MAX,
                    decimal? P_TEMP8,decimal? P_TEMP8_MIN,decimal? P_TEMP8_MAX,
                    decimal? P_SPEED,decimal? P_SPEED_MIN, decimal? P_SPEED_MAX,
                    decimal? P_MAINFRAMEWIDTH, decimal? P_WIDTH_BE,decimal? P_WIDTH_AF,
                    decimal? P_PIN2PIN, string P_FINISHBY,DateTime? P_ENDDATE,
                    decimal? P_LENGTH1,decimal? P_LENGTH2, decimal? P_LENGTH3,decimal? P_LENGTH4,
                    decimal? P_LENGTH5,decimal? P_LENGTH6,decimal? P_LENGTH7,
                    string P_ITMCODE,string P_WEAVINGLOT,string P_CUSTOMER, DateTime? P_STARTDATE,
                    string P_REMARK,decimal? P_HUMID_BF,decimal? P_HUMID_AF,string P_GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_FINISHLOT))
                return result;
            if (string.IsNullOrWhiteSpace(P_WEAVINGLOT))
                return result;
            if (string.IsNullOrWhiteSpace(P_FLAG))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_UPDATESCOURINGDATAParameter dbPara = new FINISHING_UPDATESCOURINGDATAParameter();
            dbPara.P_FINISHLOT = P_FINISHLOT;
            dbPara.P_FLAG = P_FLAG;
            dbPara.P_SAT = P_SAT;
            dbPara.P_SAT_MIN = P_SAT_MIN;
            dbPara.P_SAT_MAX = P_SAT_MAX;
            dbPara.P_WASHING1 = P_WASHING1;
            dbPara.P_WASHING1_MIN = P_WASHING1_MIN;
            dbPara.P_WASHING1_MAX = P_WASHING1_MAX;
            dbPara.P_WASHING2 = P_WASHING2;
            dbPara.P_WASHING2_MIN = P_WASHING2_MIN;
            dbPara.P_WASHING2_MAX = P_WASHING2_MAX;
            dbPara.P_HOTFLUE = P_HOTFLUE;
            dbPara.P_HOTFLUE_MIN = P_HOTFLUE_MIN;
            dbPara.P_HOTFLUE_MAX = P_HOTFLUE_MAX;
            dbPara.P_TEMP1 = P_TEMP1;
            dbPara.P_TEMP1_MIN = P_TEMP1_MIN;
            dbPara.P_TEMP1_MAX = P_TEMP1_MAX;
            dbPara.P_TEMP2 = P_TEMP2;
            dbPara.P_TEMP2_MIN = P_TEMP2_MIN;
            dbPara.P_TEMP2_MAX = P_TEMP2_MAX;
            dbPara.P_TEMP3 = P_TEMP3;
            dbPara.P_TEMP3_MIN = P_TEMP3_MIN;
            dbPara.P_TEMP3_MAX = P_TEMP3_MAX;
            dbPara.P_TEMP4 = P_TEMP4;
            dbPara.P_TEMP4_MIN = P_TEMP4_MIN;
            dbPara.P_TEMP4_MAX = P_TEMP4_MAX;
            dbPara.P_TEMP5 = P_TEMP5;
            dbPara.P_TEMP5_MIN = P_TEMP5_MIN;
            dbPara.P_TEMP5_MAX = P_TEMP5_MAX;
            dbPara.P_TEMP6 = P_TEMP6;
            dbPara.P_TEMP6_MIN = P_TEMP6_MIN;
            dbPara.P_TEMP6_MAX = P_TEMP6_MAX;
            dbPara.P_TEMP7 = P_TEMP7;
            dbPara.P_TEMP7_MIN = P_TEMP7_MIN;
            dbPara.P_TEMP7_MAX = P_TEMP7_MAX;
            dbPara.P_TEMP8 = P_TEMP8;
            dbPara.P_TEMP8_MIN = P_TEMP8_MIN;
            dbPara.P_TEMP8_MAX = P_TEMP8_MAX;
            dbPara.P_SPEED = P_SPEED;
            dbPara.P_SPEED_MIN = P_SPEED_MIN;
            dbPara.P_SPEED_MAX = P_SPEED_MAX;
            dbPara.P_MAINFRAMEWIDTH = P_MAINFRAMEWIDTH;
            dbPara.P_WIDTH_BE = P_WIDTH_BE;
            dbPara.P_WIDTH_AF = P_WIDTH_AF;
            dbPara.P_PIN2PIN = P_PIN2PIN;
            dbPara.P_FINISHBY = P_FINISHBY;
            dbPara.P_ENDDATE = P_ENDDATE;
            dbPara.P_LENGTH1 = P_LENGTH1;
            dbPara.P_LENGTH2 = P_LENGTH2;
            dbPara.P_LENGTH3 = P_LENGTH3;
            dbPara.P_LENGTH4 = P_LENGTH4;
            dbPara.P_LENGTH5 = P_LENGTH5;
            dbPara.P_LENGTH6 = P_LENGTH6;
            dbPara.P_LENGTH7 = P_LENGTH7;
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;
            dbPara.P_CUSTOMER = P_CUSTOMER;
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_HUMID_BF = P_HUMID_BF;
            dbPara.P_HUMID_AF = P_HUMID_AF;
            dbPara.P_GROUP = P_GROUP;

            FINISHING_UPDATESCOURINGDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATESCOURINGDATA(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region เพิ่มใหม่ FINISHING_GETSCOURINGREPORT ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETSCOURINGREPORTDATA> FINISHING_GETSCOURINGREPORTList(string WEAVINGLOT, string FINLOT)
        {
            List<FINISHING_GETSCOURINGREPORTDATA> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETSCOURINGREPORTParameter dbPara = new FINISHING_GETSCOURINGREPORTParameter();
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_FINLOT = FINLOT;

            List<FINISHING_GETSCOURINGREPORTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETSCOURINGREPORT(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETSCOURINGREPORTDATA>();
                    foreach (FINISHING_GETSCOURINGREPORTResult dbResult in dbResults)
                    {
                        FINISHING_GETSCOURINGREPORTDATA inst = new FINISHING_GETSCOURINGREPORTDATA();

                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;
                        inst.MCNO = dbResult.MCNO;
                        inst.STATUSFLAG = dbResult.STATUSFLAG;
                        inst.SATURATOR_CHEM_PV = dbResult.SATURATOR_CHEM_PV;
                        inst.SATURATOR_CHEM_SP = dbResult.SATURATOR_CHEM_SP;
                        inst.WASHING1_PV = dbResult.WASHING1_PV;
                        inst.WASHING1_SP = dbResult.WASHING1_SP;
                        inst.WASHING2_PV = dbResult.WASHING2_PV;
                        inst.WASHING2_SP = dbResult.WASHING2_SP;
                        inst.HOTFLUE_PV = dbResult.HOTFLUE_PV;
                        inst.HOTFLUE_SP = dbResult.HOTFLUE_SP;
                        inst.TEMP1_PV = dbResult.TEMP1_PV;
                        inst.TEMP1_SP = dbResult.TEMP1_SP;
                        inst.TEMP2_PV = dbResult.TEMP2_PV;
                        inst.TEMP2_SP = dbResult.TEMP2_SP;
                        inst.TEMP3_PV = dbResult.TEMP3_PV;
                        inst.TEMP3_SP = dbResult.TEMP3_SP;
                        inst.TEMP4_PV = dbResult.TEMP4_PV;
                        inst.TEMP4_SP = dbResult.TEMP4_SP;
                        inst.TEMP5_PV = dbResult.TEMP5_PV;
                        inst.TEMP5_SP = dbResult.TEMP5_SP;
                        inst.TEMP6_PV = dbResult.TEMP6_PV;
                        inst.TEMP6_SP = dbResult.TEMP6_SP;
                        inst.TEMP7_PV = dbResult.TEMP7_PV;
                        inst.TEMP7_SP = dbResult.TEMP7_SP;
                        inst.TEMP8_PV = dbResult.TEMP8_PV;
                        inst.TEMP8_SP = dbResult.TEMP8_SP;

                        inst.TEMP9_PV = dbResult.TEMP9_PV;
                        inst.TEMP9_SP = dbResult.TEMP9_SP;
                        inst.TEMP10_PV = dbResult.TEMP10_PV;
                        inst.TEMP10_SP = dbResult.TEMP10_SP;

                        inst.SPEED_PV = dbResult.SPEED_PV;
                        inst.SPEED_SP = dbResult.SPEED_SP;
                        inst.MAINFRAMEWIDTH = dbResult.MAINFRAMEWIDTH;
                        inst.WIDTH_BE = dbResult.WIDTH_BE;
                        inst.WIDTH_AF = dbResult.WIDTH_AF;
                        inst.PIN2PIN = dbResult.PIN2PIN;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.CONDITIONDATE = dbResult.CONDITIONDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.SAMPLINGID = dbResult.SAMPLINGID;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.PARTNO = dbResult.PARTNO;
                        inst.FINISHLENGTH = dbResult.FINISHLENGTH;
                        inst.INPUTLENGTH = dbResult.INPUTLENGTH;
                        inst.REMARK = dbResult.REMARK;

                        // เพิ่ม
                        inst.HUMIDITY_AF = dbResult.HUMIDITY_AF;
                        inst.HUMIDITY_BF = dbResult.HUMIDITY_BF;

                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

                        inst.REPROCESS = dbResult.REPROCESS;
                        inst.TEMP1_MIN = dbResult.TEMP1_MIN;
                        inst.TEMP1_MAX = dbResult.TEMP1_MAX;
                        inst.TEMP2_MIN = dbResult.TEMP2_MIN;
                        inst.TEMP2_MAX = dbResult.TEMP2_MAX;
                        inst.TEMP3_MIN = dbResult.TEMP3_MIN;
                        inst.TEMP3_MAX = dbResult.TEMP3_MAX;
                        inst.TEMP4_MIN = dbResult.TEMP4_MIN;
                        inst.TEMP4_MAX = dbResult.TEMP4_MAX;
                        inst.TEMP5_MIN = dbResult.TEMP5_MIN;
                        inst.TEMP5_MAX = dbResult.TEMP5_MAX;
                        inst.TEMP6_MIN = dbResult.TEMP6_MIN;
                        inst.TEMP6_MAX = dbResult.TEMP6_MAX;
                        inst.TEMP7_MIN = dbResult.TEMP7_MIN;
                        inst.TEMP7_MAX = dbResult.TEMP7_MAX;
                        inst.TEMP8_MIN = dbResult.TEMP8_MIN;
                        inst.TEMP8_MAX = dbResult.TEMP8_MAX;

                        inst.TEMP9_MIN = dbResult.TEMP9_PV;
                        inst.TEMP9_MAX = dbResult.TEMP9_SP;
                        inst.TEMP10_MIN = dbResult.TEMP10_PV;
                        inst.TEMP10_MAX = dbResult.TEMP10_SP;

                        inst.SAT_CHEM_MIN = dbResult.SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = dbResult.SAT_CHEM_MAX;
                        inst.WASHING1_MIN = dbResult.WASHING1_MIN;
                        inst.WASHING1_MAX = dbResult.WASHING1_MAX;
                        inst.WASHING2_MIN = dbResult.WASHING2_MIN;
                        inst.WASHING2_MAX = dbResult.WASHING2_MAX;
                        inst.HOTFLUE_MIN = dbResult.HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = dbResult.HOTFLUE_MAX;
                        inst.SPEED_MIN = dbResult.SPEED_MIN;
                        inst.SPEED_MAX = dbResult.SPEED_MAX;

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

        #region เพิ่มใหม่ FINISHING_GETScouringMCNO ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string FINISHING_GETScouringMCNO(string WEAVINGLOT, string FINLOT)
        {
            string results = string.Empty;

            if (!HasConnection())
                return results;

            FINISHING_GETSCOURINGREPORTParameter dbPara = new FINISHING_GETSCOURINGREPORTParameter();
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_FINLOT = FINLOT;

            List<FINISHING_GETSCOURINGREPORTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETSCOURINGREPORT(dbPara);
                if (null != dbResults)
                {
                    results = dbResults[0].MCNO;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region AddScouringRemark
        public void AddScouringRemark(string FINISHLOT, string ItemCode, string remark)
        {
            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return;

            if (!HasConnection())
                return;

            FINISHING_UPDATESCOURINGParameter dbPara = new FINISHING_UPDATESCOURINGParameter();
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_ITMCODE = ItemCode;

            dbPara.P_REMARK = remark;

            FINISHING_UPDATESCOURINGResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATESCOURING(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #endregion

        #region DRYER

        #region เพิ่มใหม่ GFINISHING_GETDRYERCONDITIONDataList ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETDRYERCONDITIONData> GetFINISHING_GETDRYERCONDITIONDataList(string itm_code,string mcNo)
        {
            List<FINISHING_GETDRYERCONDITIONData> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETDRYERCONDITIONParameter dbPara = new FINISHING_GETDRYERCONDITIONParameter();
            dbPara.P_ITEMCODE = itm_code;
            dbPara.P_MCNO = mcNo;

            List<FINISHING_GETDRYERCONDITIONResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETDRYERCONDITION(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETDRYERCONDITIONData>();
                    foreach (FINISHING_GETDRYERCONDITIONResult dbResult in dbResults)
                    {
                        FINISHING_GETDRYERCONDITIONData inst = new FINISHING_GETDRYERCONDITIONData();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WIDTH_BE_HEAT_MAX = dbResult.WIDTH_BE_HEAT_MAX;
                        inst.WIDTH_BE_HEAT_MIN = dbResult.WIDTH_BE_HEAT_MIN;
                        inst.ACCPRESURE = dbResult.ACCPRESURE;
                        inst.ASSTENSION = dbResult.ASSTENSION;
                        inst.ACCARIDENSER = dbResult.ACCARIDENSER;
                        inst.CHIFROT = dbResult.CHIFROT;
                        inst.CHIREAR = dbResult.CHIREAR;
                        inst.DRYERTEMP1 = dbResult.DRYERTEMP1;
                        inst.DRYERTEMP1_MARGIN = dbResult.DRYERTEMP1_MARGIN;
                        inst.SPEED = dbResult.SPEED;
                        inst.SPEED_MARGIN = dbResult.SPEED_MARGIN;
                        inst.STEAMPRESSURE = dbResult.STEAMPRESSURE;
                        inst.DRYERUPCIRCUFAN = dbResult.DRYERUPCIRCUFAN;
                        inst.DRYERDOWNCIRCUFAN = dbResult.DRYERDOWNCIRCUFAN;
                        inst.EXHAUSTFAN = dbResult.EXHAUSTFAN;
                        inst.WIDTH_AF_HEAT = dbResult.WIDTH_AF_HEAT;
                        inst.WIDTH_AF_HEAT_MARGIN = dbResult.WIDTH_AF_HEAT_MARGIN;

                        // เพิ่มใหม่
                        inst.HUMIDITY_MIN = dbResult.HUMIDITY_MIN;
                        inst.HUMIDITY_MAX = dbResult.HUMIDITY_MAX;
                        inst.MCNO = dbResult.MCNO;

                        // เพิ่มใหม่
                        inst.SATURATOR_CHEM = dbResult.SATURATOR_CHEM;
                        inst.SATURATOR_CHEM_MARGIN = dbResult.SATURATOR_CHEM_MARGIN;
                        inst.WASHING1 = dbResult.WASHING1;
                        inst.WASHING1_MARGIN = dbResult.WASHING1_MARGIN;
                        inst.WASHING2 = dbResult.WASHING2;
                        inst.WASHING2_MARGIN = dbResult.WASHING2_MARGIN;

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

        #region เพิ่มใหม่ FINISHING_GETDRYERDATA ใช้ในการ Load FINISHING DRYER

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETDRYERDATA> FINISHING_GETDRYERDATAList(string flag, string mcNo)
        {
            List<FINISHING_GETDRYERDATA> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETDRYERDATAParameter dbPara = new FINISHING_GETDRYERDATAParameter();
            dbPara.P_FLAG = flag;
            dbPara.P_MCNO = mcNo;

            List<FINISHING_GETDRYERDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETDRYERDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETDRYERDATA>();
                    foreach (FINISHING_GETDRYERDATAResult dbResult in dbResults)
                    {
                        FINISHING_GETDRYERDATA inst = new FINISHING_GETDRYERDATA();

                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;

                        inst.MCNO = dbResult.MCNO;
                        inst.STATUSFLAG = dbResult.STATUSFLAG;

                        inst.WIDTH_BE_HEAT = dbResult.WIDTH_BE_HEAT;
                        inst.ACCPRESURE = dbResult.ACCPRESURE;
                        inst.ASSTENSION = dbResult.ASSTENSION;
                        inst.ACCARIDENSER = dbResult.ACCARIDENSER;
                        inst.CHIFROT = dbResult.CHIFROT;
                        inst.CHIREAR = dbResult.CHIREAR;
                        inst.DRYERTEMP1_PV = dbResult.DRYERTEMP1_PV;
                        inst.DRYERTEMP1_SP = dbResult.DRYERTEMP1_SP;

                        inst.SPEED_PV = dbResult.SPEED_PV;
                        inst.SPEED_SP = dbResult.SPEED_SP;
                        
                        inst.STEAMPRESSURE = dbResult.STEAMPRESSURE;
                        inst.DRYERCIRCUFAN = dbResult.DRYERCIRCUFAN;
                        inst.EXHAUSTFAN = dbResult.EXHAUSTFAN;
                        inst.WIDTH_AF_HEAT = dbResult.WIDTH_AF_HEAT;

                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.CONDITIONDATE = dbResult.CONDITIONDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.SAMPLINGID = dbResult.SAMPLINGID;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.WEAVINGLENGTH = dbResult.WEAVINGLENGTH;
                        inst.REMARK = dbResult.REMARK;

                        // เพิ่ม
                        inst.HUMIDITY_AF = dbResult.HUMIDITY_AF;
                        inst.HUMIDITY_BF = dbResult.HUMIDITY_BF;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

                        //เพิ่ม 12/05/60
                        inst.SATURATOR_PV = dbResult.SATURATOR_CHEM_PV;
                        inst.SATURATOR_SP = dbResult.SATURATOR_CHEM_SP;
                        inst.WASHING1_PV = dbResult.WASHING1_PV;
                        inst.WASHING1_SP = dbResult.WASHING1_SP;
                        inst.WASHING2_PV = dbResult.WASHING2_PV;
                        inst.WASHING2_SP = dbResult.WASHING2_SP;

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

        #region เพิ่มใหม่ GetFINISHING_DRYERDATABYLOT ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_DRYERDATABYLOT> GetFINISHING_DRYERDATABYLOT(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_DRYERDATABYLOT> results = null;

            if (!HasConnection())
                return results;

            FINISHING_DRYERDATABYLOTParameter dbPara = new FINISHING_DRYERDATABYLOTParameter();
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<FINISHING_DRYERDATABYLOTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_DRYERDATABYLOT(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_DRYERDATABYLOT>();
                    foreach (FINISHING_DRYERDATABYLOTResult dbResult in dbResults)
                    {
                        FINISHING_DRYERDATABYLOT inst = new FINISHING_DRYERDATABYLOT();

                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.MCNO = dbResult.MCNO;
                        inst.STATUSFLAG = dbResult.STATUSFLAG;

                        inst.WIDTH_BE_HEAT = dbResult.WIDTH_BE_HEAT;
                        inst.ACCPRESURE = dbResult.ACCPRESURE;
                        inst.ASSTENSION = dbResult.ASSTENSION;
                        inst.ACCARIDENSER = dbResult.ACCARIDENSER;
                        inst.CHIFROT = dbResult.CHIFROT;
                        inst.CHIREAR = dbResult.CHIREAR;
                        inst.DRYERTEMP1_PV = dbResult.DRYERTEMP1_PV;
                        inst.DRYERTEMP1_SP = dbResult.DRYERTEMP1_SP;

                        inst.STEAMPRESSURE = dbResult.STEAMPRESSURE;
                        inst.DRYERCIRCUFAN = dbResult.DRYERCIRCUFAN;
                        inst.EXHAUSTFAN = dbResult.EXHAUSTFAN;
                        inst.WIDTH_AF_HEAT = dbResult.WIDTH_AF_HEAT;

                        inst.SPEED_PV = dbResult.SPEED_PV;
                        inst.SPEED_SP = dbResult.SPEED_SP;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.CONDITIONDATE = dbResult.CONDITIONDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.SAMPLINGID = dbResult.SAMPLINGID;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;
                        inst.REMARK = dbResult.REMARK;
                        inst.HUMIDITY_BF = dbResult.HUMIDITY_BF;
                        inst.HUMIDITY_AF = dbResult.HUMIDITY_AF;
                        inst.REPROCESS = dbResult.REPROCESS;
                        inst.WEAVLENGTH = dbResult.WEAVLENGTH;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;
                        inst.HOTFLUE_MIN = dbResult.HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = dbResult.HOTFLUE_MAX;
                        inst.SPEED_MIN = dbResult.SPEED_MIN;
                        inst.SPEED_MAX = dbResult.SPEED_MAX;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;

                        //inst.SAT_MIN = dbResult.SAT_MIN;
                        //inst.SAT_MAX = dbResult.SAT_MAX;
                        //inst.SAT = dbResult.SAT;
                        //inst.WASH1_MIN = dbResult.WASH1_MIN;
                        //inst.WASH1_MAX = dbResult.WASH1_MAX;
                        //inst.WASH1 = dbResult.WASH1;
                        //inst.WASH2_MIN = dbResult.WASH2_MIN;
                        //inst.WASH2_MAX = dbResult.WASH2_MAX;
                        //inst.WASH2 = dbResult.WASH2;

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

        #region เพิ่มใหม่ FINISHING_DRYERPLCDATA ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_DRYERPLCDATA> FINISHING_DRYERPLCDATA(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_DRYERPLCDATA> results = null;

            if (!HasConnection())
                return results;

            FINISHING_DRYERPLCDATAParameter dbPara = new FINISHING_DRYERPLCDATAParameter();
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;

            List<FINISHING_DRYERPLCDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_DRYERPLCDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_DRYERPLCDATA>();
                    foreach (FINISHING_DRYERPLCDATAResult dbResult in dbResults)
                    {
                        FINISHING_DRYERPLCDATA inst = new FINISHING_DRYERPLCDATA();

                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.SPEED_MIN = dbResult.SPEED_MIN;
                        inst.SPEED_MAX = dbResult.SPEED_MAX;
                        inst.SPEED = dbResult.SPEED;
                        inst.HOTF_MIN = dbResult.HOTF_MIN;
                        inst.HOTF_MAX = dbResult.HOTF_MAX;
                        inst.HOTF = dbResult.HOTF;

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

        // เปลี่ยน ค่า Return =  string result 
        #region เพิ่ม FINISHING_INSERTDRYER

        public string FINISHING_INSERTDRYER(string weavlot, string itmCode, string finishcustomer, string PRODUCTTYPEID, string operatorid, string MCNO, string flag
            , decimal? ACCARIDENSER, decimal? ACCPRESURE, decimal? ASSTENSION, decimal? CHIFROT
            , decimal? CHIREAR, decimal? DRYCIRCUFAN, decimal? EXHAUSTFAN
            , decimal? HOTFLUE_PV, decimal? HOTFLUE_SP, decimal? SPEED_PV, decimal? SPEED_SP
            , decimal? STEAMPRESURE, decimal? WIDTHAFHEAT, decimal? WIDTHBEHEAT
            , decimal? HUMID_AF, decimal? HUMID_BF
            , string REPROCESS, decimal? WEAVLENGTH, string OPERATOR_GROUP
            , decimal? SATURATOR_PV, decimal? SATURATOR_SP, decimal? WASHING1_PV, decimal? WASHING1_SP, decimal? WASHING2_PV, decimal? WASHING2_SP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(weavlot))
                return result;
            if (string.IsNullOrWhiteSpace(itmCode))
                return result;
            if (string.IsNullOrWhiteSpace(finishcustomer))
                return result;
            if (string.IsNullOrWhiteSpace(PRODUCTTYPEID))
                return result;
            if (string.IsNullOrWhiteSpace(operatorid))
                return result;
            if (string.IsNullOrWhiteSpace(MCNO))
                return result;
            if (string.IsNullOrWhiteSpace(flag))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_INSERTDRYERParameter dbPara = new FINISHING_INSERTDRYERParameter();
            dbPara.P_WEAVLOT = weavlot;
            dbPara.P_ITMCODE = itmCode;
            dbPara.P_FINISHCUSTOMER = finishcustomer;
            dbPara.P_PRODUCTTYPEID = PRODUCTTYPEID;
            dbPara.P_OPERATORID = operatorid;
            dbPara.P_MCNO = MCNO;
            dbPara.P_FLAG = flag;
            dbPara.P_ACCARIDENSER = ACCARIDENSER;
            dbPara.P_ACCPRESURE = ACCPRESURE;
            dbPara.P_ASSTENSION = ASSTENSION;
            dbPara.P_CHIFROT = CHIFROT;
            dbPara.P_CHIREAR = CHIREAR;
            dbPara.P_DRYCIRCUFAN = DRYCIRCUFAN;
            dbPara.P_EXHAUSTFAN = EXHAUSTFAN;
            dbPara.P_HOTFLUE_PV = HOTFLUE_PV;
            dbPara.P_HOTFLUE_SP = HOTFLUE_SP;
            dbPara.P_SPEED_PV = SPEED_PV;
            dbPara.P_SPEED_SP = SPEED_SP;
            dbPara.P_STEAMPRESURE = STEAMPRESURE;
            dbPara.P_WIDTHAFHEAT = WIDTHAFHEAT;
            dbPara.P_WIDTHBEHEAT = WIDTHBEHEAT;

            // เพิ่ม
            dbPara.P_HUMID_AF = HUMID_AF;
            dbPara.P_HUMID_BF = HUMID_BF;

            // เพิ่ม REPROCESS
            dbPara.P_REPROCESS = REPROCESS;
            dbPara.P_WEAVLENGTH = WEAVLENGTH;
            dbPara.P_GROUP = OPERATOR_GROUP;

            //เพิ่ม 12/05/60
            dbPara.P_SATURATOR_PV = SATURATOR_PV;
            dbPara.P_SATURATOR_SP = SATURATOR_SP;
            dbPara.P_WASHING1_PV = WASHING1_PV;
            dbPara.P_WASHING1_SP = WASHING1_SP;
            dbPara.P_WASHING2_PV = WASHING2_PV;
            dbPara.P_WASHING2_SP = WASHING2_SP;

            FINISHING_INSERTDRYERResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_INSERTDRYER(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = "";
            }

            return result;
        }

        #endregion

        #region เพิ่ม FINISHING_UPDATEDRYERProcessing Processing

        public string FINISHING_UPDATEDRYERProcessing(string FINISHLOT, string operatorid, string flag
            , decimal? ACCARIDENSER, decimal? ACCPRESURE, decimal? ASSTENSION, decimal? CHIFROT
            , decimal? CHIREAR, decimal? DRYCIRCUFAN, decimal? EXHAUSTFAN
            , decimal? HOTFLUE_PV, decimal? HOTFLUE_SP, decimal? SPEED_PV, decimal? SPEED_SP
            , decimal? STEAMPRESURE, decimal? WIDTHAFHEAT, decimal? WIDTHBEHEAT
            , string ITMCODE, string WEAVINGLOT, string CUSTOMER, DateTime? STARTDATE
            , decimal? HUMID_AF, decimal? HUMID_BF, string OPERATOR_GROUP
            , decimal? SATURATOR_PV, decimal? SATURATOR_SP, decimal? WASHING1_PV, decimal? WASHING1_SP, decimal? WASHING2_PV, decimal? WASHING2_SP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return result;
            if (string.IsNullOrWhiteSpace(operatorid))
                return result;
            if (string.IsNullOrWhiteSpace(flag))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_UPDATEDRYERParameter dbPara = new FINISHING_UPDATEDRYERParameter();
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_FLAG = flag;
            dbPara.P_CONDITIONBY = operatorid;
            dbPara.P_CONDITONDATE = DateTime.Now;

            dbPara.P_ACCARIDENSER = ACCARIDENSER;
            dbPara.P_ACCPRESURE = ACCPRESURE;
            dbPara.P_ASSTENSION = ASSTENSION;
            dbPara.P_CHIFROT = CHIFROT;
            dbPara.P_CHIREAR = CHIREAR;
            dbPara.P_DRYCIRCUFAN = DRYCIRCUFAN;
            dbPara.P_EXHAUSTFAN = EXHAUSTFAN;

            dbPara.P_HOTFLUE_PV = HOTFLUE_PV;
            dbPara.P_HOTFLUE_SP = HOTFLUE_SP;
            dbPara.P_SPEED_PV = SPEED_PV;
            dbPara.P_SPEED_SP = SPEED_SP;
            dbPara.P_STEAMPRESURE = STEAMPRESURE;

            dbPara.P_WIDTHAFHEAT = WIDTHAFHEAT;
            dbPara.P_WIDTHBEHEAT = WIDTHBEHEAT;

            dbPara.P_ITMCODE = ITMCODE;
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_CUSTOMER = CUSTOMER;
            dbPara.P_STARTDATE = STARTDATE;

            // เพิ่ม
            dbPara.P_HUMID_AF = HUMID_AF;
            dbPara.P_HUMID_BF = HUMID_BF;
            dbPara.P_GROUP = OPERATOR_GROUP;

            // เพิ่ม 12/05/60
            dbPara.P_SATURATOR_PV = SATURATOR_PV;
            dbPara.P_SATURATOR_SP = SATURATOR_SP;
            dbPara.P_WASHING1_PV = WASHING1_PV;
            dbPara.P_WASHING1_SP = WASHING1_SP;
            dbPara.P_WASHING2_PV = WASHING2_PV;
            dbPara.P_WASHING2_SP = WASHING2_SP;

            FINISHING_UPDATEDRYERResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATEDRYER(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = "";
            }

            return result;
        }

        #endregion

        #region เพิ่ม FINISHING_UPDATEDRYER Finishing

        public string FINISHING_UPDATEDRYERFinishing(string FINISHLOT, string operatorid, string flag
            , decimal? ACCARIDENSER, decimal? ACCPRESURE, decimal? ASSTENSION, decimal? CHIFROT
            , decimal? CHIREAR, decimal? DRYCIRCUFAN, decimal? EXHAUSTFAN
            , decimal? HOTFLUE_PV, decimal? HOTFLUE_SP, decimal? SPEED_PV, decimal? SPEED_SP
            , decimal? STEAMPRESURE, decimal? WIDTHAFHEAT, decimal? WIDTHBEHEAT
            , string ITMCODE, string WEAVINGLOT, string CUSTOMER, DateTime? STARTDATE
            , decimal? HUMID_AF, decimal? HUMID_BF
            , decimal? LENGTH1, decimal? LENGTH2, decimal? LENGTH3, decimal? LENGTH4, decimal? LENGTH5, decimal? LENGTH6, decimal? LENGTH7, string OPERATOR_GROUP
            , decimal? SATURATOR_PV, decimal? SATURATOR_SP, decimal? WASHING1_PV, decimal? WASHING1_SP, decimal? WASHING2_PV, decimal? WASHING2_SP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return result;
            if (string.IsNullOrWhiteSpace(operatorid))
                return result;
            if (string.IsNullOrWhiteSpace(flag))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_UPDATEDRYERParameter dbPara = new FINISHING_UPDATEDRYERParameter();
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_FLAG = flag;
            dbPara.P_FINISHBY = operatorid;
            dbPara.P_ENDDATE = DateTime.Now;

            dbPara.P_LENGTH1 = LENGTH1;
            dbPara.P_LENGTH2 = LENGTH2;
            dbPara.P_LENGTH3 = LENGTH3;
            dbPara.P_LENGTH4 = LENGTH4;
            dbPara.P_LENGTH5 = LENGTH5;
            dbPara.P_LENGTH6 = LENGTH6;
            dbPara.P_LENGTH7 = LENGTH7;

            dbPara.P_ACCARIDENSER = ACCARIDENSER;
            dbPara.P_ACCPRESURE = ACCPRESURE;
            dbPara.P_ASSTENSION = ASSTENSION;
            dbPara.P_CHIFROT = CHIFROT;
            dbPara.P_CHIREAR = CHIREAR;
            dbPara.P_DRYCIRCUFAN = DRYCIRCUFAN;
            dbPara.P_EXHAUSTFAN = EXHAUSTFAN;

            dbPara.P_HOTFLUE_PV = HOTFLUE_PV;
            dbPara.P_HOTFLUE_SP = HOTFLUE_SP;
            dbPara.P_SPEED_PV = SPEED_PV;
            dbPara.P_SPEED_SP = SPEED_SP;
            dbPara.P_STEAMPRESURE = STEAMPRESURE;

            dbPara.P_WIDTHAFHEAT = WIDTHAFHEAT;
            dbPara.P_WIDTHBEHEAT = WIDTHBEHEAT;

            dbPara.P_ITMCODE = ITMCODE;
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_CUSTOMER = CUSTOMER;
            dbPara.P_STARTDATE = STARTDATE;

            //dbPara.P_CONDITIONBY = "";
            //dbPara.P_CONDITONDATE = null;

            // เพิ่ม
            dbPara.P_HUMID_AF = HUMID_AF;
            dbPara.P_HUMID_BF = HUMID_BF;
            dbPara.P_GROUP = OPERATOR_GROUP;

            // เพิ่ม 12/05/60
            dbPara.P_SATURATOR_PV = SATURATOR_PV;
            dbPara.P_SATURATOR_SP = SATURATOR_SP;
            dbPara.P_WASHING1_PV = WASHING1_PV;
            dbPara.P_WASHING1_SP = WASHING1_SP;
            dbPara.P_WASHING2_PV = WASHING2_PV;
            dbPara.P_WASHING2_SP = WASHING2_SP;

            FINISHING_UPDATEDRYERResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATEDRYER(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = "";
            }

            return result;
        }

        #endregion

        #region เพิ่ม FINISHING_UPDATEDRYERDATA Finishing

        public string FINISHING_UPDATEDRYERDATA(string P_FINISHLOT, string P_FLAG,
            decimal? P_HOTFLUE, decimal? P_HOTFLUE_MIN, decimal? P_HOTFLUE_MAX,
            decimal? P_SPEED, decimal? P_SPEED_MIN, decimal? P_SPEED_MAX,
            decimal? P_WIDTHBEHEAT, decimal? P_ACCPRESURE, decimal? P_ASSTENSION, decimal? P_ACCARIDENSER,
            decimal? P_CHIFROT, decimal? P_CHIREAR, decimal? P_STEAMPRESURE, decimal? P_DRYCIRCUFAN,
            decimal? P_EXHAUSTFAN, decimal? P_WIDTHAFHEAT, string P_CONDITIONBY, string P_FINISHBY,
            DateTime? P_ENDDATE, DateTime? P_CONDITONDATE,
            decimal? P_LENGTH1, decimal? P_LENGTH2, decimal? P_LENGTH3, decimal? P_LENGTH4,
            decimal? P_LENGTH5, decimal? P_LENGTH6, decimal? P_LENGTH7,
            string P_ITMCODE, string P_WEAVINGLOT, string P_CUSTOMER, DateTime? P_STARTDATE,
            string P_REMARK,decimal? P_HUMID_BF,decimal? P_HUMID_AF,string P_GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_FINISHLOT))
                return result;
            if (string.IsNullOrWhiteSpace(P_WEAVINGLOT))
                return result;
            if (string.IsNullOrWhiteSpace(P_FLAG))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_UPDATEDRYERDATAParameter dbPara = new FINISHING_UPDATEDRYERDATAParameter();
            dbPara.P_FINISHLOT = P_FINISHLOT;
            dbPara.P_FLAG = P_FLAG;
            dbPara.P_HOTFLUE = P_HOTFLUE;
            dbPara.P_HOTFLUE_MIN = P_HOTFLUE_MIN;
            dbPara.P_HOTFLUE_MAX = P_HOTFLUE_MAX;

            dbPara.P_SPEED = P_SPEED;
            dbPara.P_SPEED_MIN = P_SPEED_MIN;
            dbPara.P_SPEED_MAX = P_SPEED_MAX;

            dbPara.P_WIDTHBEHEAT = P_WIDTHBEHEAT;
            dbPara.P_ACCPRESURE = P_ACCPRESURE;
            dbPara.P_ASSTENSION = P_ASSTENSION;
            dbPara.P_ACCARIDENSER = P_ACCARIDENSER;
            dbPara.P_CHIFROT = P_CHIFROT;
            dbPara.P_CHIREAR = P_CHIREAR;
            dbPara.P_STEAMPRESURE = P_STEAMPRESURE;
            dbPara.P_DRYCIRCUFAN = P_DRYCIRCUFAN;
            dbPara.P_EXHAUSTFAN = P_EXHAUSTFAN;
            dbPara.P_WIDTHAFHEAT = P_WIDTHAFHEAT;
            dbPara.P_CONDITIONBY = P_CONDITIONBY;

            dbPara.P_FINISHBY = P_FINISHBY;
            dbPara.P_ENDDATE = P_ENDDATE;
            dbPara.P_CONDITONDATE = P_CONDITONDATE;
            dbPara.P_LENGTH1 = P_LENGTH1;
            dbPara.P_LENGTH2 = P_LENGTH2;
            dbPara.P_LENGTH3 = P_LENGTH3;
            dbPara.P_LENGTH4 = P_LENGTH4;
            dbPara.P_LENGTH5 = P_LENGTH5;
            dbPara.P_LENGTH6 = P_LENGTH6;
            dbPara.P_LENGTH7 = P_LENGTH7;
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;
            dbPara.P_CUSTOMER = P_CUSTOMER;
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_HUMID_BF = P_HUMID_BF;
            dbPara.P_HUMID_AF = P_HUMID_AF;
            dbPara.P_GROUP = P_GROUP;

            FINISHING_UPDATEDRYERDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATEDRYERDATA(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.RESULT;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region เพิ่มใหม่ FINISHING_GETDRYERREPORT ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETDRYERREPORTDATA> FINISHING_GETDRYERREPORTList(string WEAVINGLOT, string FINLOT)
        {
            List<FINISHING_GETDRYERREPORTDATA> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETDRYERREPORTParameter dbPara = new FINISHING_GETDRYERREPORTParameter();
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_FINLOT = FINLOT;

            List<FINISHING_GETDRYERREPORTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETDRYERREPORT(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETDRYERREPORTDATA>();
                    foreach (FINISHING_GETDRYERREPORTResult dbResult in dbResults)
                    {
                        FINISHING_GETDRYERREPORTDATA inst = new FINISHING_GETDRYERREPORTDATA();

                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;
                        inst.MCNO = dbResult.MCNO;
                        inst.STATUSFLAG = dbResult.STATUSFLAG;

                        inst.WIDTH_BE_HEAT = dbResult.WIDTH_BE_HEAT;
                        inst.ACCPRESURE = dbResult.ACCPRESURE;
                        inst.ASSTENSION = dbResult.ASSTENSION;
                        inst.ACCARIDENSER = dbResult.ACCARIDENSER;
                        inst.CHIFROT = dbResult.CHIFROT;
                        inst.CHIREAR = dbResult.CHIREAR;
                        inst.DRYERTEMP1_PV = dbResult.DRYERTEMP1_PV;
                        inst.DRYERTEMP1_SP = dbResult.DRYERTEMP1_SP;
                        inst.SPEED_PV = dbResult.SPEED_PV;
                        inst.SPEED_SP = dbResult.SPEED_SP;
                        inst.STEAMPRESSURE = dbResult.STEAMPRESSURE;
                        inst.DRYERCIRCUFAN = dbResult.DRYERCIRCUFAN;
                        inst.EXHAUSTFAN = dbResult.EXHAUSTFAN;
                        inst.WIDTH_AF_HEAT = dbResult.WIDTH_AF_HEAT;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;

                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.CONDITIONDATE = dbResult.CONDITIONDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.SAMPLINGID = dbResult.SAMPLINGID;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.PARTNO = dbResult.PARTNO;
                        inst.FINISHLENGTH = dbResult.FINISHLENGTH;
                        inst.INPUTLENGTH = dbResult.INPUTLENGTH;
                        inst.REMARK = dbResult.REMARK;

                        // เพิ่ม
                        inst.HUMIDITY_AF = dbResult.HUMIDITY_AF;
                        inst.HUMIDITY_BF = dbResult.HUMIDITY_BF;

                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

                        inst.HOTFLUE_MIN = dbResult.HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = dbResult.HOTFLUE_MAX;
                        inst.SPEED_MIN = dbResult.SPEED_MIN;
                        inst.SPEED_MAX = dbResult.SPEED_MAX;

                        // เพิ่ม 12/05/60
                        inst.SATURATOR_PV = dbResult.SATURATOR_CHEM_PV;
                        inst.SATURATOR_SP = dbResult.SATURATOR_CHEM_SP;
                        inst.WASHING1_PV = dbResult.WASHING1_PV;
                        inst.WASHING1_SP = dbResult.WASHING1_SP;
                        inst.WASHING2_PV = dbResult.WASHING2_PV;
                        inst.WASHING2_SP = dbResult.WASHING2_SP;

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

        #region เพิ่มใหม่ FINISHING_GETDryerMCNO ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string FINISHING_GETDryerMCNO(string WEAVINGLOT, string FINLOT)
        {
            string results = string.Empty;

            if (!HasConnection())
                return results;

            FINISHING_GETDRYERREPORTParameter dbPara = new FINISHING_GETDRYERREPORTParameter();
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_FINLOT = FINLOT;

            List<FINISHING_GETDRYERREPORTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETDRYERREPORT(dbPara);
                if (null != dbResults)
                {
                    results = dbResults[0].MCNO;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region AddDryerRemark
        public void AddDryerRemark(string FINISHLOT, string ItemCode, string remark)
        {
            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return;

            if (!HasConnection())
                return;

            FINISHING_UPDATEDRYERParameter dbPara = new FINISHING_UPDATEDRYERParameter();
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_ITMCODE = ItemCode;

            dbPara.P_REMARK = remark;

            FINISHING_UPDATEDRYERResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_UPDATEDRYER(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #endregion

        #region SAMPLING

        #region FINISHING_SAMPLINGDATA
        public bool FINISHING_SAMPLINGDATA(string P_WEAVLOT, string P_FINISHLOT, string P_ITMCODE, string P_FINISHCUSTOMER, string P_PRODUCTTYPEID
            , string P_OPERATORID, decimal? P_WIDTH, decimal? P_LENGTH, string P_REMARK)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(P_WEAVLOT))
                return result;

            if (string.IsNullOrWhiteSpace(P_FINISHLOT))
                return result;

            if (string.IsNullOrWhiteSpace(P_ITMCODE))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_SAMPLINGDATAParameter dbPara = new FINISHING_SAMPLINGDATAParameter();

            dbPara.P_WEAVLOT = P_WEAVLOT;
            dbPara.P_FINISHLOT = P_FINISHLOT;
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_FINISHCUSTOMER = P_FINISHCUSTOMER;
            dbPara.P_PRODUCTTYPEID = P_PRODUCTTYPEID;
            dbPara.P_OPERATORID = P_OPERATORID;
            dbPara.P_WIDTH = P_WIDTH;
            dbPara.P_LENGTH = P_LENGTH;
            dbPara.P_REMARK = P_REMARK;
            
            FINISHING_SAMPLINGDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_SAMPLINGDATA(dbPara);

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

        #region เพิ่มใหม่ FINISHING_GETSAMPLINGSHEET ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETSAMPLINGSHEET> FINISHING_GETSAMPLINGSHEETList(string WEAVINGLOT, string FINLOT)
        {
            List<FINISHING_GETSAMPLINGSHEET> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETSAMPLINGSHEETParameter dbPara = new FINISHING_GETSAMPLINGSHEETParameter();
            dbPara.P_WEAVINGLOT = WEAVINGLOT;
            dbPara.P_FINLOT = FINLOT;

            List<FINISHING_GETSAMPLINGSHEETResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETSAMPLINGSHEET(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETSAMPLINGSHEET>();
                    foreach (FINISHING_GETSAMPLINGSHEETResult dbResult in dbResults)
                    {
                        FINISHING_GETSAMPLINGSHEET inst = new FINISHING_GETSAMPLINGSHEET();

                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.PRODUCTID = dbResult.PRODUCTID;
                        inst.SAMPLING_WIDTH = dbResult.SAMPLING_WIDTH;
                        inst.SAMPLING_LENGTH = dbResult.SAMPLING_LENGTH;
                        inst.PROCESS = dbResult.PROCESS;
                        inst.REMARK = dbResult.REMARK;
                        inst.FABRICTYPE = dbResult.FABRICTYPE;
                        inst.PRODUCTNAME = dbResult.PRODUCTNAME;
                        inst.RETESTFLAG = dbResult.RETESTFLAG;

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

        //เพิ่ม Load Data show in Grid main menu
        #region เพิ่มใหม่ FINISHING_INPROCESSLIST ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_INPROCESSLIST> FINISHING_INPROCESSLIST()
        {
            List<FINISHING_INPROCESSLIST> results = null;

            if (!HasConnection())
                return results;

            FINISHING_INPROCESSLISTParameter dbPara = new FINISHING_INPROCESSLISTParameter();


            List<FINISHING_INPROCESSLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_INPROCESSLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_INPROCESSLIST>();
                    foreach (FINISHING_INPROCESSLISTResult dbResult in dbResults)
                    {
                        FINISHING_INPROCESSLIST inst = new FINISHING_INPROCESSLIST();

                        inst.MCNAME = dbResult.MCNAME;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.STATUS = dbResult.STATUS;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

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

        #region FINISHING_CANCEL
        public bool FINISHING_CANCEL(string WEAVLOT, string FINISHLOT, string PROCESS, string OPERATOR)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(WEAVLOT))
                return result;

            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return result;

            if (string.IsNullOrWhiteSpace(PROCESS))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_CANCELParameter dbPara = new FINISHING_CANCELParameter();

            dbPara.P_WEAVLOT = WEAVLOT;
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_PROCESS = PROCESS;
            dbPara.P_OPERATOR = OPERATOR;
            

            FINISHING_CANCELResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_CANCEL(dbPara);

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

        #region FINISHING_EDITLENGTH
        public bool FINISHING_EDITLENGTH(string WEAVLOT, string FINISHLOT, string PROCESS, string OPERATOR ,
              decimal? LENGTH1 ,decimal? LENGTH2 ,decimal? LENGTH3 ,decimal? LENGTH4 , decimal? LENGTH5 ,decimal? LENGTH6 ,decimal? LENGTH7 , decimal? TOTALLENGTH )
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(WEAVLOT))
                return result;

            if (string.IsNullOrWhiteSpace(FINISHLOT))
                return result;

            if (string.IsNullOrWhiteSpace(PROCESS))
                return result;

            if (!HasConnection())
                return result;

            FINISHING_EDITLENGTHParameter dbPara = new FINISHING_EDITLENGTHParameter();

            dbPara.P_WEAVLOT = WEAVLOT;
            dbPara.P_FINISHLOT = FINISHLOT;
            dbPara.P_PROCESS = PROCESS;
            dbPara.P_OPERATOR = OPERATOR;

            dbPara.P_LENGTH1 = LENGTH1;
            dbPara.P_LENGTH2 = LENGTH2;
            dbPara.P_LENGTH3 = LENGTH3;
            dbPara.P_LENGTH4 = LENGTH4;
            dbPara.P_LENGTH5 = LENGTH5;
            dbPara.P_LENGTH6 = LENGTH6;
            dbPara.P_LENGTH7 = LENGTH7;
            dbPara.P_TOTALLENGTH = TOTALLENGTH;

            FINISHING_EDITLENGTHResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.FINISHING_EDITLENGTH(dbPara);

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




