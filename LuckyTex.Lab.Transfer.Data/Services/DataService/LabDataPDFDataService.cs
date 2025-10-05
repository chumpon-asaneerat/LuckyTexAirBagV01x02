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
    #region LabDataPDF Data Service

    /// <summary>
    /// The data service for Packing process.
    /// </summary>
    public class LabDataPDFDataService : BaseDataService
    {
        #region Singelton

        private static LabDataPDFDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static LabDataPDFDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(LabDataPDFDataService))
                    {
                        _instance = new LabDataPDFDataService();
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
        private LabDataPDFDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~LabDataPDFDataService()
        {
        }

        #endregion

        #region Public Methods

        #region Create new Session

        public LabDataPDFSession GetSession(LogInResult loginResult)
        {
            LabDataPDFSession result = new LabDataPDFSession();
            result.Init(loginResult);
            return result;
        }

        #endregion

        //-- Lab --//

        #region LAB_INSERTUPDATETENSILE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WEAVINGLOG"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_OPERATOR"></param>
        /// <param name="P_TESTDATE"></param>
        /// <param name="P_TESTTIME"></param>
        /// <param name="P_YARN"></param>
        /// <param name="P_TENSILE1"></param>
        /// <param name="P_TENSILE2"></param>
        /// <param name="P_TENSILE3"></param>
        /// <param name="P_ELONG1"></param>
        /// <param name="P_ELONG2"></param>
        /// <param name="P_ELONG3"></param>
        /// <param name="P_UPLOADDATE"></param>
        /// <param name="P_UPLOADBY"></param>
        /// <returns></returns>
        public string LAB_INSERTUPDATETENSILE(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, string P_OPERATOR
            , string P_TESTDATE, string P_TESTTIME, string P_YARN
            , decimal? P_TENSILE1, decimal? P_TENSILE2, decimal? P_TENSILE3
            , decimal? P_ELONG1, decimal? P_ELONG2, decimal? P_ELONG3, DateTime? P_UPLOADDATE, string P_UPLOADBY)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_WEAVINGLOG) && string.IsNullOrWhiteSpace(P_FINISHINGLOT))
                return "ITMCODE & WEAVINGLOG & FINISHINGLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_INSERTUPDATETENSILEParameter dbPara = new LAB_INSERTUPDATETENSILEParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOG = P_WEAVINGLOG;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_TESTDATE = P_TESTDATE;
            dbPara.P_TESTTIME = P_TESTTIME;
            dbPara.P_YARN = P_YARN;
            dbPara.P_TENSILE1 = P_TENSILE1;
            dbPara.P_TENSILE2 = P_TENSILE2;
            dbPara.P_TENSILE3 = P_TENSILE3;
            dbPara.P_ELONG1 = P_ELONG1;
            dbPara.P_ELONG2 = P_ELONG2;
            dbPara.P_ELONG3 = P_ELONG3;
            dbPara.P_UPLOADDATE = P_UPLOADDATE;
            dbPara.P_UPLOADBY = P_UPLOADBY;

            LAB_INSERTUPDATETENSILEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_INSERTUPDATETENSILE(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region LAB_INSERTUPDATETEAR
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WEAVINGLOG"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_OPERATOR"></param>
        /// <param name="P_YARN"></param>
        /// <param name="P_TEAR1"></param>
        /// <param name="P_TEAR2"></param>
        /// <param name="P_TEAR3"></param>
        /// <param name="P_UPLOADBY"></param>
        /// <returns></returns>
        public string LAB_INSERTUPDATETEAR(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, string P_OPERATOR
            , string P_YARN, decimal? P_TEAR1, decimal? P_TEAR2, decimal? P_TEAR3, DateTime? P_UPLOADDATE, string P_UPLOADBY)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_WEAVINGLOG) && string.IsNullOrWhiteSpace(P_FINISHINGLOT))
                return "ITMCODE & WEAVINGLOG & FINISHINGLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_INSERTUPDATETEARParameter dbPara = new LAB_INSERTUPDATETEARParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOG = P_WEAVINGLOG;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_YARN = P_YARN;
            dbPara.P_TEAR1 = P_TEAR1;
            dbPara.P_TEAR2 = P_TEAR2;
            dbPara.P_TEAR3 = P_TEAR3;
            dbPara.P_UPLOADDATE = P_UPLOADDATE;
            dbPara.P_UPLOADBY = P_UPLOADBY;

            LAB_INSERTUPDATETEARResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_INSERTUPDATETEAR(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region LAB_INSERTUPDATEITEMSPEC
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WIDTH_NO"></param>
        /// <param name="P_WIDTH"></param>
        /// <param name="P_USEWIDTH_NO"></param>
        /// <param name="P_USEWIDTH"></param>
        /// <param name="P_USEWIDTH_TOR"></param>
        /// <param name="P_WIDTHSILICONE_NO"></param>
        /// <param name="P_WIDTHSILICONE"></param>
        /// <param name="P_WIDTHSILICONE_TOR"></param>
        /// <param name="P_NUMTHREADS_W_NO"></param>
        /// <param name="P_NUMTHREADS_W"></param>
        /// <param name="P_NUMTHREADS_W_TOR"></param>
        /// <param name="P_NUMTHREADS_F_NO"></param>
        /// <param name="P_NUMTHREADS_F"></param>
        /// <param name="P_NUMTHREADS_F_TOR"></param>
        /// <param name="P_TOTALWEIGHT_NO"></param>
        /// <param name="P_TOTALWEIGHT"></param>
        /// <param name="P_TOTALWEIGHT_TOR"></param>
        /// <param name="P_UNCOATEDWEIGHT_NO"></param>
        /// <param name="P_UNCOATEDWEIGHT"></param>
        /// <param name="P_UNCOATEDWEIGHT_TOR"></param>
        /// <param name="P_COATWEIGHT_NO"></param>
        /// <param name="P_COATWEIGHT"></param>
        /// <param name="P_COATWEIGHT_TOR"></param>
        /// <param name="P_THICKNESS_NO"></param>
        /// <param name="P_THICKNESS"></param>
        /// <param name="P_THICKNESS_TOR"></param>
        /// <param name="P_MAXFORCE_W_NO"></param>
        /// <param name="P_MAXFORCE_W"></param>
        /// <param name="P_MAXFORCE_W_TOR"></param>
        /// <param name="P_MAXFORCE_F_NO"></param>
        /// <param name="P_MAXFORCE_F"></param>
        /// <param name="P_MAXFORCE_F_TOR"></param>
        /// <param name="P_ELOGATION_W_NO"></param>
        /// <param name="P_ELOGATION_W"></param>
        /// <param name="P_ELOGATION_W_TOR"></param>
        /// <param name="P_ELOGATION_F_NO"></param>
        /// <param name="P_ELOGATION_F"></param>
        /// <param name="P_ELOGATION_F_TOR"></param>
        /// <param name="P_FLAMMABILITY_W_NO"></param>
        /// <param name="P_FLAMMABILITY_W"></param>
        /// <param name="P_FLAMMABILITY_W_TOR"></param>
        /// <param name="P_FLAMMABILITY_F_NO"></param>
        /// <param name="P_FLAMMABILITY_F"></param>
        /// <param name="P_FLAMMABILITY_F_TOR"></param>
        /// <param name="P_EDGECOMB_W_NO"></param>
        /// <param name="P_EDGECOMB_W"></param>
        /// <param name="P_EDGECOMB_W_TOR"></param>
        /// <param name="P_EDGECOMB_F_NO"></param>
        /// <param name="P_EDGECOMB_F"></param>
        /// <param name="P_EDGECOMB_F_TOR"></param>
        /// <param name="P_STIFFNESS_W_NO"></param>
        /// <param name="P_STIFFNESS_W"></param>
        /// <param name="P_STIFFNESS_W_TOR"></param>
        /// <param name="P_STIFFNESS_F_NO"></param>
        /// <param name="P_STIFFNESS_F"></param>
        /// <param name="P_STIFFNESS_F_TOR"></param>
        /// <param name="P_TEAR_W_NO"></param>
        /// <param name="P_TEAR_W"></param>
        /// <param name="P_TEAR_W_TOR"></param>
        /// <param name="P_TEAR_F_NO"></param>
        /// <param name="P_TEAR_F"></param>
        /// <param name="P_TEAR_F_TOR"></param>
        /// <param name="P_STATIC_AIR_NO"></param>
        /// <param name="P_STATIC_AIR"></param>
        /// <param name="P_STATIC_AIR_TOR"></param>
        /// <param name="P_DYNAMIC_AIR_NO"></param>
        /// <param name="P_DYNAMIC_AIR"></param>
        /// <param name="P_DYNAMIC_AIR_TOR"></param>
        /// <param name="P_EXPONENT_NO"></param>
        /// <param name="P_EXPONENT"></param>
        /// <param name="P_EXPONENT_TOR"></param>
        /// <param name="P_DIMENSCHANGE_W_NO"></param>
        /// <param name="P_DIMENSCHANGE_W"></param>
        /// <param name="P_DIMENSCHANGE_W_TOR"></param>
        /// <param name="P_DIMENSCHANGE_F_NO"></param>
        /// <param name="P_DIMENSCHANGE_F"></param>
        /// <param name="P_DIMENSCHANGE_F_TOR"></param>
        /// <param name="P_FLEXABRASION_W_NO"></param>
        /// <param name="P_FLEXABRASION_W"></param>
        /// <param name="P_FLEXABRASION_W_TOR"></param>
        /// <param name="P_FLEXABRASION_F_NO"></param>
        /// <param name="P_FLEXABRASION_F"></param>
        /// <param name="P_FLEXABRASION_F_TOR"></param>
        /// <param name="P_BOW_NO"></param>
        /// <param name="P_BOW"></param>
        /// <param name="P_BOW_TOR"></param>
        /// <param name="P_SKEW_NO"></param>
        /// <param name="P_SKEW"></param>
        /// <param name="P_SKEW_TOR"></param>
        /// <param name="P_BENDING_W_NO"></param>
        /// <param name="P_BENDING_W"></param>
        /// <param name="P_BENDING_W_TOR"></param>
        /// <param name="P_BENDING_F_NO"></param>
        /// <param name="P_BENDING_F"></param>
        /// <param name="P_BENDING_F_TOR"></param>
        /// <param name="P_FLEX_SCOTT_W_NO"></param>
        /// <param name="P_FLEX_SCOTT_W"></param>
        /// <param name="P_FLEX_SCOTT_W_TOR"></param>
        /// <param name="P_FLEX_SCOTT_F_NO"></param>
        /// <param name="P_FLEX_SCOTT_F"></param>
        /// <param name="P_FLEX_SCOTT_F_TOR"></param>
        /// <returns></returns>
        public string LAB_INSERTUPDATEITEMSPEC(string P_ITMCODE, decimal? P_WIDTH_NO, decimal? P_WIDTH,
        decimal? P_USEWIDTH_NO, decimal? P_USEWIDTH, string P_USEWIDTH_TOR,
        decimal? P_WIDTHSILICONE_NO, decimal? P_WIDTHSILICONE, string P_WIDTHSILICONE_TOR,
        decimal? P_NUMTHREADS_W_NO, decimal? P_NUMTHREADS_W, decimal? P_NUMTHREADS_W_TOR,
        decimal? P_NUMTHREADS_F_NO, decimal? P_NUMTHREADS_F, decimal? P_NUMTHREADS_F_TOR,
        decimal? P_TOTALWEIGHT_NO, decimal? P_TOTALWEIGHT, decimal? P_TOTALWEIGHT_TOR,
        decimal? P_UNCOATEDWEIGHT_NO, decimal? P_UNCOATEDWEIGHT, decimal? P_UNCOATEDWEIGHT_TOR,
        decimal? P_COATWEIGHT_NO, decimal? P_COATWEIGHT, decimal? P_COATWEIGHT_TOR,
        decimal? P_THICKNESS_NO, decimal? P_THICKNESS, decimal? P_THICKNESS_TOR,
        decimal? P_MAXFORCE_W_NO, decimal? P_MAXFORCE_W, string P_MAXFORCE_W_TOR,
        decimal? P_MAXFORCE_F_NO, decimal? P_MAXFORCE_F, string P_MAXFORCE_F_TOR,
        decimal? P_ELOGATION_W_NO, decimal? P_ELOGATION_W, string P_ELOGATION_W_TOR,
        decimal? P_ELOGATION_F_NO, decimal? P_ELOGATION_F, string P_ELOGATION_F_TOR,
        decimal? P_FLAMMABILITY_W_NO, decimal? P_FLAMMABILITY_W, string P_FLAMMABILITY_W_TOR,
        decimal? P_FLAMMABILITY_F_NO, decimal? P_FLAMMABILITY_F, string P_FLAMMABILITY_F_TOR,
        decimal? P_EDGECOMB_W_NO, decimal? P_EDGECOMB_W, string P_EDGECOMB_W_TOR,
        decimal? P_EDGECOMB_F_NO, decimal? P_EDGECOMB_F, string P_EDGECOMB_F_TOR,
        decimal? P_STIFFNESS_W_NO, decimal? P_STIFFNESS_W, string P_STIFFNESS_W_TOR,
        decimal? P_STIFFNESS_F_NO, decimal? P_STIFFNESS_F, string P_STIFFNESS_F_TOR,
        decimal? P_TEAR_W_NO, decimal? P_TEAR_W, string P_TEAR_W_TOR,
        decimal? P_TEAR_F_NO, decimal? P_TEAR_F, string P_TEAR_F_TOR,
        decimal? P_STATIC_AIR_NO, decimal? P_STATIC_AIR, string P_STATIC_AIR_TOR,
        decimal? P_DYNAMIC_AIR_NO, decimal? P_DYNAMIC_AIR, decimal? P_DYNAMIC_AIR_TOR,
        decimal? P_EXPONENT_NO, decimal? P_EXPONENT, decimal? P_EXPONENT_TOR,
        decimal? P_DIMENSCHANGE_W_NO, decimal? P_DIMENSCHANGE_W, string P_DIMENSCHANGE_W_TOR,
        decimal? P_DIMENSCHANGE_F_NO, decimal? P_DIMENSCHANGE_F, string P_DIMENSCHANGE_F_TOR,
        decimal? P_FLEXABRASION_W_NO, decimal? P_FLEXABRASION_W, string P_FLEXABRASION_W_TOR,
        decimal? P_FLEXABRASION_F_NO, decimal? P_FLEXABRASION_F, string P_FLEXABRASION_F_TOR,
        decimal? P_BOW_NO, decimal? P_BOW, string P_BOW_TOR,
        decimal? P_SKEW_NO, decimal? P_SKEW, string P_SKEW_TOR,
        decimal? P_BENDING_W_NO, decimal? P_BENDING_W,string P_BENDING_W_TOR,
        decimal? P_BENDING_F_NO,decimal? P_BENDING_F,string P_BENDING_F_TOR,
        decimal? P_FLEX_SCOTT_W_NO,decimal? P_FLEX_SCOTT_W,string P_FLEX_SCOTT_W_TOR,
        decimal? P_FLEX_SCOTT_F_NO,decimal? P_FLEX_SCOTT_F,string P_FLEX_SCOTT_F_TOR   )
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE))
                return "ITMCODE isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_INSERTUPDATEITEMSPECParameter dbPara = new LAB_INSERTUPDATEITEMSPECParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WIDTH_NO = P_WIDTH_NO;
            dbPara.P_WIDTH = P_WIDTH;
            dbPara.P_USEWIDTH_NO = P_USEWIDTH_NO;
            dbPara.P_USEWIDTH = P_USEWIDTH;
            dbPara.P_USEWIDTH_TOR = P_USEWIDTH_TOR;
            dbPara.P_WIDTHSILICONE_NO = P_WIDTHSILICONE_NO;
            dbPara.P_WIDTHSILICONE = P_WIDTHSILICONE;
            dbPara.P_WIDTHSILICONE_TOR = P_WIDTHSILICONE_TOR;
            dbPara.P_NUMTHREADS_W_NO = P_NUMTHREADS_W_NO;
            dbPara.P_NUMTHREADS_W = P_NUMTHREADS_W;
            dbPara.P_NUMTHREADS_W_TOR = P_NUMTHREADS_W_TOR;
            dbPara.P_NUMTHREADS_F_NO = P_NUMTHREADS_F_NO;
            dbPara.P_NUMTHREADS_F = P_NUMTHREADS_F;
            dbPara.P_NUMTHREADS_F_TOR = P_NUMTHREADS_F_TOR;
            dbPara.P_TOTALWEIGHT_NO = P_TOTALWEIGHT_NO;
            dbPara.P_TOTALWEIGHT = P_TOTALWEIGHT;
            dbPara.P_TOTALWEIGHT_TOR = P_TOTALWEIGHT_TOR;
            dbPara.P_UNCOATEDWEIGHT_NO = P_UNCOATEDWEIGHT_NO;
            dbPara.P_UNCOATEDWEIGHT = P_UNCOATEDWEIGHT;
            dbPara.P_UNCOATEDWEIGHT_TOR = P_UNCOATEDWEIGHT_TOR;
            dbPara.P_COATWEIGHT_NO = P_COATWEIGHT_NO;
            dbPara.P_COATWEIGHT = P_COATWEIGHT;
            dbPara.P_COATWEIGHT_TOR = P_COATWEIGHT_TOR;
            dbPara.P_THICKNESS_NO = P_THICKNESS_NO;
            dbPara.P_THICKNESS = P_THICKNESS;
            dbPara.P_THICKNESS_TOR = P_THICKNESS_TOR;
            dbPara.P_MAXFORCE_W_NO = P_MAXFORCE_W_NO;
            dbPara.P_MAXFORCE_W = P_MAXFORCE_W;
            dbPara.P_MAXFORCE_W_TOR = P_MAXFORCE_W_TOR;
            dbPara.P_MAXFORCE_F_NO = P_MAXFORCE_F_NO;
            dbPara.P_MAXFORCE_F = P_MAXFORCE_F;
            dbPara.P_MAXFORCE_F_TOR = P_MAXFORCE_F_TOR;
            dbPara.P_ELOGATION_W_NO = P_ELOGATION_W_NO;
            dbPara.P_ELOGATION_W = P_ELOGATION_W;
            dbPara.P_ELOGATION_W_TOR = P_ELOGATION_W_TOR;
            dbPara.P_ELOGATION_F_NO = P_ELOGATION_F_NO;
            dbPara.P_ELOGATION_F = P_ELOGATION_F;
            dbPara.P_ELOGATION_F_TOR = P_ELOGATION_F_TOR;
            dbPara.P_FLAMMABILITY_W_NO = P_FLAMMABILITY_W_NO;
            dbPara.P_FLAMMABILITY_W = P_FLAMMABILITY_W;
            dbPara.P_FLAMMABILITY_W_TOR = P_FLAMMABILITY_W_TOR;
            dbPara.P_FLAMMABILITY_F_NO = P_FLAMMABILITY_F_NO;
            dbPara.P_FLAMMABILITY_F = P_FLAMMABILITY_F;
            dbPara.P_FLAMMABILITY_F_TOR = P_FLAMMABILITY_F_TOR;
            dbPara.P_EDGECOMB_W_NO = P_EDGECOMB_W_NO;
            dbPara.P_EDGECOMB_W = P_EDGECOMB_W;
            dbPara.P_EDGECOMB_W_TOR = P_EDGECOMB_W_TOR;
            dbPara.P_EDGECOMB_F_NO = P_EDGECOMB_F_NO;
            dbPara.P_EDGECOMB_F = P_EDGECOMB_F;
            dbPara.P_EDGECOMB_F_TOR = P_EDGECOMB_F_TOR;
            dbPara.P_STIFFNESS_W_NO = P_STIFFNESS_W_NO;
            dbPara.P_STIFFNESS_W = P_STIFFNESS_W;
            dbPara.P_STIFFNESS_W_TOR = P_STIFFNESS_W_TOR;
            dbPara.P_STIFFNESS_F_NO = P_STIFFNESS_F_NO;
            dbPara.P_STIFFNESS_F = P_STIFFNESS_F;
            dbPara.P_STIFFNESS_F_TOR = P_STIFFNESS_F_TOR;
            dbPara.P_TEAR_W_NO = P_TEAR_W_NO;
            dbPara.P_TEAR_W = P_TEAR_W;
            dbPara.P_TEAR_W_TOR = P_TEAR_W_TOR;
            dbPara.P_TEAR_F_NO = P_TEAR_F_NO;
            dbPara.P_TEAR_F = P_TEAR_F;
            dbPara.P_TEAR_F_TOR = P_TEAR_F_TOR;
            dbPara.P_STATIC_AIR_NO = P_STATIC_AIR_NO;
            dbPara.P_STATIC_AIR = P_STATIC_AIR;
            dbPara.P_STATIC_AIR_TOR = P_STATIC_AIR_TOR;
            dbPara.P_DYNAMIC_AIR_NO = P_DYNAMIC_AIR_NO;
            dbPara.P_DYNAMIC_AIR = P_DYNAMIC_AIR;
            dbPara.P_DYNAMIC_AIR_TOR = P_DYNAMIC_AIR_TOR;
            dbPara.P_EXPONENT_NO = P_EXPONENT_NO;
            dbPara.P_EXPONENT = P_EXPONENT;
            dbPara.P_EXPONENT_TOR = P_EXPONENT_TOR;
            dbPara.P_DIMENSCHANGE_W_NO = P_DIMENSCHANGE_W_NO;
            dbPara.P_DIMENSCHANGE_W = P_DIMENSCHANGE_W;
            dbPara.P_DIMENSCHANGE_W_TOR = P_DIMENSCHANGE_W_TOR;
            dbPara.P_DIMENSCHANGE_F_NO = P_DIMENSCHANGE_F_NO;
            dbPara.P_DIMENSCHANGE_F = P_DIMENSCHANGE_F;
            dbPara.P_DIMENSCHANGE_F_TOR = P_DIMENSCHANGE_F_TOR;
            dbPara.P_FLEXABRASION_W_NO = P_FLEXABRASION_W_NO;
            dbPara.P_FLEXABRASION_W = P_FLEXABRASION_W;
            dbPara.P_FLEXABRASION_W_TOR = P_FLEXABRASION_W_TOR;
            dbPara.P_FLEXABRASION_F_NO = P_FLEXABRASION_F_NO;
            dbPara.P_FLEXABRASION_F = P_FLEXABRASION_F;
            dbPara.P_FLEXABRASION_F_TOR = P_FLEXABRASION_F_TOR;
            dbPara.P_BOW_NO = P_BOW_NO;
            dbPara.P_BOW = P_BOW;
            dbPara.P_BOW_TOR = P_BOW_TOR;
            dbPara.P_SKEW_NO = P_SKEW_NO;
            dbPara.P_SKEW = P_SKEW;
            dbPara.P_SKEW_TOR = P_SKEW_TOR;

            //Update 07/07/18
            dbPara.P_BENDING_W_NO = P_BENDING_W_NO;
            dbPara.P_BENDING_W = P_BENDING_W;
            dbPara.P_BENDING_W_TOR = P_BENDING_W_TOR;
            dbPara.P_BENDING_F_NO = P_BENDING_F_NO;
            dbPara.P_BENDING_F = P_BENDING_F;
            dbPara.P_BENDING_F_TOR = P_BENDING_F_TOR;
            dbPara.P_FLEX_SCOTT_W_NO = P_FLEX_SCOTT_W_NO;
            dbPara.P_FLEX_SCOTT_W = P_FLEX_SCOTT_W;
            dbPara.P_FLEX_SCOTT_W_TOR = P_FLEX_SCOTT_W_TOR;
            dbPara.P_FLEX_SCOTT_F_NO = P_FLEX_SCOTT_F_NO;
            dbPara.P_FLEX_SCOTT_F = P_FLEX_SCOTT_F;
            dbPara.P_FLEX_SCOTT_F_TOR = P_FLEX_SCOTT_F_TOR;

            LAB_INSERTUPDATEITEMSPECResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_INSERTUPDATEITEMSPEC(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region LAB_INSERTUPDATEEDGECOMB
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WEAVINGLOG"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_OPERATOR"></param>
        /// <param name="P_TESTDATE"></param>
        /// <param name="P_TESTTIME"></param>
        /// <param name="P_YARN"></param>
        /// <param name="P_EDGECOMB1"></param>
        /// <param name="P_EDGECOMB2"></param>
        /// <param name="P_EDGECOMB3"></param>
        /// <param name="P_UPLOADBY"></param>
        /// <returns></returns>
        public string LAB_INSERTUPDATEEDGECOMB(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, string P_OPERATOR
            , string P_TESTDATE, string P_TESTTIME, string P_YARN
            , decimal? P_EDGECOMB1, decimal? P_EDGECOMB2, decimal? P_EDGECOMB3, DateTime? P_UPLOADDATE, string P_UPLOADBY)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_WEAVINGLOG) && string.IsNullOrWhiteSpace(P_FINISHINGLOT))
                return "ITMCODE & WEAVINGLOG & FINISHINGLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_INSERTUPDATEEDGECOMBParameter dbPara = new LAB_INSERTUPDATEEDGECOMBParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOG = P_WEAVINGLOG;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_TESTDATE = P_TESTDATE;
            dbPara.P_TESTTIME = P_TESTTIME;
            dbPara.P_YARN = P_YARN;
            dbPara.P_EDGECOMB1 = P_EDGECOMB1;
            dbPara.P_EDGECOMB2 = P_EDGECOMB2;
            dbPara.P_EDGECOMB3 = P_EDGECOMB3;
            dbPara.P_UPLOADDATE = P_UPLOADDATE;
            dbPara.P_UPLOADBY = P_UPLOADBY;

            LAB_INSERTUPDATEEDGECOMBResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_INSERTUPDATEEDGECOMB(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region LAB_INSERTPRODUCTIONP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WEAVINGLOG"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_ENTRYDATE"></param>
        /// <param name="P_ENTRYBY"></param>
        /// <param name="P_WIDTH"></param>
        /// <param name="P_USEWIDTH1"></param>
        /// <param name="P_USEWIDTH2"></param>
        /// <param name="P_USEWIDTH3"></param>
        /// <param name="P_WIDTHSILICONE1"></param>
        /// <param name="P_WIDTHSILICONE2"></param>
        /// <param name="P_WIDTHSILICONE3"></param>
        /// <param name="P_NUMTHREADS_W1"></param>
        /// <param name="P_NUMTHREADS_W2"></param>
        /// <param name="P_NUMTHREADS_W3"></param>
        /// <param name="P_NUMTHREADS_F1"></param>
        /// <param name="P_NUMTHREADS_F2"></param>
        /// <param name="P_NUMTHREADS_F3"></param>
        /// <param name="P_TOTALWEIGHT1"></param>
        /// <param name="P_TOTALWEIGHT2"></param>
        /// <param name="P_TOTALWEIGHT3"></param>
        /// <param name="P_TOTALWEIGHT4"></param>
        /// <param name="P_TOTALWEIGHT5"></param>
        /// <param name="P_TOTALWEIGHT6"></param>
        /// <param name="P_UNCOATEDWEIGHT1"></param>
        /// <param name="P_UNCOATEDWEIGHT2"></param>
        /// <param name="P_UNCOATEDWEIGHT3"></param>
        /// <param name="P_UNCOATEDWEIGHT4"></param>
        /// <param name="P_UNCOATEDWEIGHT5"></param>
        /// <param name="P_UNCOATEDWEIGHT6"></param>
        /// <param name="P_COATWEIGHT1"></param>
        /// <param name="P_COATWEIGHT2"></param>
        /// <param name="P_COATWEIGHT3"></param>
        /// <param name="P_COATWEIGHT4"></param>
        /// <param name="P_COATWEIGHT5"></param>
        /// <param name="P_COATWEIGHT6"></param>
        /// <param name="P_THICKNESS1"></param>
        /// <param name="P_THICKNESS2"></param>
        /// <param name="P_THICKNESS3"></param>
        /// <param name="P_MAXFORCE_W1"></param>
        /// <param name="P_MAXFORCE_W2"></param>
        /// <param name="P_MAXFORCE_W3"></param>
        /// <param name="P_MAXFORCE_F1"></param>
        /// <param name="P_MAXFORCE_F2"></param>
        /// <param name="P_MAXFORCE_F3"></param>
        /// <param name="P_ELOGATION_W1"></param>
        /// <param name="P_ELOGATION_W2"></param>
        /// <param name="P_ELOGATION_W3"></param>
        /// <param name="P_ELOGATION_F1"></param>
        /// <param name="P_ELOGATION_F2"></param>
        /// <param name="P_ELOGATION_F3"></param>
        /// <param name="P_FLAMMABILITY_W"></param>
        /// <param name="P_FLAMMABILITY_F"></param>
        /// <param name="P_EDGECOMB_W1"></param>
        /// <param name="P_EDGECOMB_W2"></param>
        /// <param name="P_EDGECOMB_W3"></param>
        /// <param name="P_EDGECOMB_F1"></param>
        /// <param name="P_EDGECOMB_F2"></param>
        /// <param name="P_EDGECOMB_F3"></param>
        /// <param name="P_STIFFNESS_W1"></param>
        /// <param name="P_STIFFNESS_W2"></param>
        /// <param name="P_STIFFNESS_W3"></param>
        /// <param name="P_STIFFNESS_F1"></param>
        /// <param name="P_STIFFNESS_F2"></param>
        /// <param name="P_STIFFNESS_F3"></param>
        /// <param name="P_TEAR_W1"></param>
        /// <param name="P_TEAR_W2"></param>
        /// <param name="P_TEAR_W3"></param>
        /// <param name="P_TEAR_F1"></param>
        /// <param name="P_TEAR_F2"></param>
        /// <param name="P_TEAR_F3"></param>
        /// <param name="P_STATIC_AIR1"></param>
        /// <param name="P_STATIC_AIR2"></param>
        /// <param name="P_STATIC_AIR3"></param>
        /// <param name="P_STATIC_AIR4"></param>
        /// <param name="P_STATIC_AIR5"></param>
        /// <param name="P_STATIC_AIR6"></param>
        /// <param name="P_DYNAMIC_AIR1"></param>
        /// <param name="P_DYNAMIC_AIR2"></param>
        /// <param name="P_DYNAMIC_AIR3"></param>
        /// <param name="P_EXPONENT1"></param>
        /// <param name="P_EXPONENT2"></param>
        /// <param name="P_EXPONENT3"></param>
        /// <param name="P_DIMENSCHANGE_W1"></param>
        /// <param name="P_DIMENSCHANGE_W2"></param>
        /// <param name="P_DIMENSCHANGE_W3"></param>
        /// <param name="P_DIMENSCHANGE_F1"></param>
        /// <param name="P_DIMENSCHANGE_F2"></param>
        /// <param name="P_DIMENSCHANGE_F3"></param>
        /// <param name="P_FLEXABRASION_W1"></param>
        /// <param name="P_FLEXABRASION_W2"></param>
        /// <param name="P_FLEXABRASION_W3"></param>
        /// <param name="P_FLEXABRASION_F1"></param>
        /// <param name="P_FLEXABRASION_F2"></param>
        /// <param name="P_FLEXABRASION_F3"></param>
        /// <param name="P_STATUS"></param>
        /// <param name="P_REMARK"></param>
        /// <param name="P_APPROVEBY"></param>
        /// <param name="P_APPROVEDATE"></param>
        /// <param name="P_BOW1"></param>
        /// <param name="P_BOW2"></param>
        /// <param name="P_BOW3"></param>
        /// <param name="P_SKEW1"></param>
        /// <param name="P_SKEW2"></param>
        /// <param name="P_SKEW3"></param>
        /// <param name="P_BENDING_W1"></param>
        /// <param name="P_BENDING_W2"></param>
        /// <param name="P_BENDING_W3"></param>
        /// <param name="P_BENDING_F1"></param>
        /// <param name="P_BENDING_F2"></param>
        /// <param name="P_BENDING_F3"></param>
        /// <param name="P_FLEX_SCOTT_W1"></param>
        /// <param name="P_FLEX_SCOTT_W2"></param>
        /// <param name="P_FLEX_SCOTT_W3"></param>
        /// <param name="P_FLEX_SCOTT_F1"></param>
        /// <param name="P_FLEX_SCOTT_F2"></param>
        /// <param name="P_FLEX_SCOTT_F3"></param>
        /// <returns></returns>
        public string LAB_INSERTPRODUCTIONP(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, DateTime? P_ENTRYDATE, string P_ENTRYBY,
            decimal? P_WIDTH, decimal? P_USEWIDTH1, decimal? P_USEWIDTH2, decimal? P_USEWIDTH3, decimal? P_WIDTHSILICONE1, decimal? P_WIDTHSILICONE2, decimal? P_WIDTHSILICONE3,
            decimal? P_NUMTHREADS_W1, decimal? P_NUMTHREADS_W2, decimal? P_NUMTHREADS_W3, decimal? P_NUMTHREADS_F1, decimal? P_NUMTHREADS_F2, decimal? P_NUMTHREADS_F3,
            decimal? P_TOTALWEIGHT1, decimal? P_TOTALWEIGHT2, decimal? P_TOTALWEIGHT3, decimal? P_TOTALWEIGHT4, decimal? P_TOTALWEIGHT5, decimal? P_TOTALWEIGHT6,
            decimal? P_UNCOATEDWEIGHT1, decimal? P_UNCOATEDWEIGHT2, decimal? P_UNCOATEDWEIGHT3, decimal? P_UNCOATEDWEIGHT4, decimal? P_UNCOATEDWEIGHT5, decimal? P_UNCOATEDWEIGHT6,
            decimal? P_COATWEIGHT1, decimal? P_COATWEIGHT2, decimal? P_COATWEIGHT3, decimal? P_COATWEIGHT4, decimal? P_COATWEIGHT5, decimal? P_COATWEIGHT6,
            decimal? P_THICKNESS1, decimal? P_THICKNESS2, decimal? P_THICKNESS3, 
            decimal? P_MAXFORCE_W1, decimal? P_MAXFORCE_W2, decimal? P_MAXFORCE_W3,
            decimal? P_MAXFORCE_W4, decimal? P_MAXFORCE_W5, decimal? P_MAXFORCE_W6,
            decimal? P_MAXFORCE_F1, decimal? P_MAXFORCE_F2, decimal? P_MAXFORCE_F3,
            decimal? P_MAXFORCE_F4, decimal? P_MAXFORCE_F5, decimal? P_MAXFORCE_F6,
            decimal? P_ELOGATION_W1, decimal? P_ELOGATION_W2, decimal? P_ELOGATION_W3,
            decimal? P_ELOGATION_W4, decimal? P_ELOGATION_W5, decimal? P_ELOGATION_W6,
            decimal? P_ELOGATION_F1, decimal? P_ELOGATION_F2, decimal? P_ELOGATION_F3,
            decimal? P_ELOGATION_F4, decimal? P_ELOGATION_F5, decimal? P_ELOGATION_F6,
            decimal? P_FLAMMABILITY_W, decimal? P_FLAMMABILITY_W2, decimal? P_FLAMMABILITY_W3, decimal? P_FLAMMABILITY_W4, decimal? P_FLAMMABILITY_W5,
            decimal? P_FLAMMABILITY_F, decimal? P_FLAMMABILITY_F2, decimal? P_FLAMMABILITY_F3, decimal? P_FLAMMABILITY_F4, decimal? P_FLAMMABILITY_F5,
            decimal? P_EDGECOMB_W1, decimal? P_EDGECOMB_W2, decimal? P_EDGECOMB_W3, decimal? P_EDGECOMB_F1, decimal? P_EDGECOMB_F2, decimal? P_EDGECOMB_F3,
            decimal? P_STIFFNESS_W1, decimal? P_STIFFNESS_W2, decimal? P_STIFFNESS_W3, decimal? P_STIFFNESS_F1, decimal? P_STIFFNESS_F2, decimal? P_STIFFNESS_F3,
            decimal? P_TEAR_W1, decimal? P_TEAR_W2, decimal? P_TEAR_W3, decimal? P_TEAR_F1, decimal? P_TEAR_F2, decimal? P_TEAR_F3,
            decimal? P_STATIC_AIR1, decimal? P_STATIC_AIR2, decimal? P_STATIC_AIR3, decimal? P_STATIC_AIR4, decimal? P_STATIC_AIR5, decimal? P_STATIC_AIR6,
            decimal? P_DYNAMIC_AIR1, decimal? P_DYNAMIC_AIR2, decimal? P_DYNAMIC_AIR3,
            decimal? P_EXPONENT1, decimal? P_EXPONENT2, decimal? P_EXPONENT3, decimal? P_DIMENSCHANGE_W1, decimal? P_DIMENSCHANGE_W2, decimal? P_DIMENSCHANGE_W3,
            decimal? P_DIMENSCHANGE_F1, decimal? P_DIMENSCHANGE_F2, decimal? P_DIMENSCHANGE_F3, decimal? P_FLEXABRASION_W1, decimal? P_FLEXABRASION_W2, decimal? P_FLEXABRASION_W3,
            decimal? P_FLEXABRASION_F1, decimal? P_FLEXABRASION_F2, decimal? P_FLEXABRASION_F3, string P_STATUS, string P_REMARK, string P_APPROVEBY, DateTime? P_APPROVEDATE,
            decimal? P_BOW1, decimal? P_BOW2, decimal? P_BOW3, decimal? P_SKEW1, decimal? P_SKEW2, decimal? P_SKEW3,
            decimal? P_BENDING_W1, decimal? P_BENDING_W2, decimal? P_BENDING_W3, decimal? P_BENDING_F1, decimal? P_BENDING_F2, decimal? P_BENDING_F3,
            decimal? P_FLEX_SCOTT_W1, decimal? P_FLEX_SCOTT_W2, decimal? P_FLEX_SCOTT_W3, decimal? P_FLEX_SCOTT_F1, decimal? P_FLEX_SCOTT_F2, decimal? P_FLEX_SCOTT_F3)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_WEAVINGLOG) && string.IsNullOrWhiteSpace(P_FINISHINGLOT))
                return "ITMCODE & WEAVINGLOG & FINISHINGLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_INSERTPRODUCTIONParameter dbPara = new LAB_INSERTPRODUCTIONParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOG = P_WEAVINGLOG;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_ENTRYDATE = P_ENTRYDATE;
            dbPara.P_ENTRYBY = P_ENTRYBY;
            dbPara.P_WIDTH = P_WIDTH;
            dbPara.P_USEWIDTH1 = P_USEWIDTH1;
            dbPara.P_USEWIDTH2 = P_USEWIDTH2;
            dbPara.P_USEWIDTH3 = P_USEWIDTH3;
            dbPara.P_WIDTHSILICONE1 = P_WIDTHSILICONE1;
            dbPara.P_WIDTHSILICONE2 = P_WIDTHSILICONE2;
            dbPara.P_WIDTHSILICONE3 = P_WIDTHSILICONE3;
            dbPara.P_NUMTHREADS_W1 = P_NUMTHREADS_W1;
            dbPara.P_NUMTHREADS_W2 = P_NUMTHREADS_W2;
            dbPara.P_NUMTHREADS_W3 = P_NUMTHREADS_W3;
            dbPara.P_NUMTHREADS_F1 = P_NUMTHREADS_F1;
            dbPara.P_NUMTHREADS_F2 = P_NUMTHREADS_F2;
            dbPara.P_NUMTHREADS_F3 = P_NUMTHREADS_F3;
            dbPara.P_TOTALWEIGHT1 = P_TOTALWEIGHT1;
            dbPara.P_TOTALWEIGHT2 = P_TOTALWEIGHT2;
            dbPara.P_TOTALWEIGHT3 = P_TOTALWEIGHT3;
            dbPara.P_TOTALWEIGHT4 = P_TOTALWEIGHT4;
            dbPara.P_TOTALWEIGHT5 = P_TOTALWEIGHT5;
            dbPara.P_TOTALWEIGHT6 = P_TOTALWEIGHT6;
            dbPara.P_UNCOATEDWEIGHT1 = P_UNCOATEDWEIGHT1;
            dbPara.P_UNCOATEDWEIGHT2 = P_UNCOATEDWEIGHT2;
            dbPara.P_UNCOATEDWEIGHT3 = P_UNCOATEDWEIGHT3;
            dbPara.P_UNCOATEDWEIGHT4 = P_UNCOATEDWEIGHT4;
            dbPara.P_UNCOATEDWEIGHT5 = P_UNCOATEDWEIGHT5;
            dbPara.P_UNCOATEDWEIGHT6 = P_UNCOATEDWEIGHT6;
            dbPara.P_COATWEIGHT1 = P_COATWEIGHT1;
            dbPara.P_COATWEIGHT2 = P_COATWEIGHT2;
            dbPara.P_COATWEIGHT3 = P_COATWEIGHT3;
            dbPara.P_COATWEIGHT4 = P_COATWEIGHT4;
            dbPara.P_COATWEIGHT5 = P_COATWEIGHT5;
            dbPara.P_COATWEIGHT6 = P_COATWEIGHT6;
            dbPara.P_THICKNESS1 = P_THICKNESS1;
            dbPara.P_THICKNESS2 = P_THICKNESS2;
            dbPara.P_THICKNESS3 = P_THICKNESS3;

            dbPara.P_MAXFORCE_W1 = P_MAXFORCE_W1;
            dbPara.P_MAXFORCE_W2 = P_MAXFORCE_W2;
            dbPara.P_MAXFORCE_W3 = P_MAXFORCE_W3;

            dbPara.P_MAXFORCE_W4 = P_MAXFORCE_W4;
            dbPara.P_MAXFORCE_W5 = P_MAXFORCE_W5;
            dbPara.P_MAXFORCE_W6 = P_MAXFORCE_W6;

            dbPara.P_MAXFORCE_F1 = P_MAXFORCE_F1;
            dbPara.P_MAXFORCE_F2 = P_MAXFORCE_F2;
            dbPara.P_MAXFORCE_F3 = P_MAXFORCE_F3;

            dbPara.P_MAXFORCE_F4 = P_MAXFORCE_F4;
            dbPara.P_MAXFORCE_F5 = P_MAXFORCE_F5;
            dbPara.P_MAXFORCE_F6 = P_MAXFORCE_F6;

            dbPara.P_ELOGATION_W1 = P_ELOGATION_W1;
            dbPara.P_ELOGATION_W2 = P_ELOGATION_W2;
            dbPara.P_ELOGATION_W3 = P_ELOGATION_W3;

            dbPara.P_ELOGATION_W4 = P_ELOGATION_W4;
            dbPara.P_ELOGATION_W5 = P_ELOGATION_W5;
            dbPara.P_ELOGATION_W6 = P_ELOGATION_W6;

            dbPara.P_ELOGATION_F1 = P_ELOGATION_F1;
            dbPara.P_ELOGATION_F2 = P_ELOGATION_F2;
            dbPara.P_ELOGATION_F3 = P_ELOGATION_F3;

            dbPara.P_ELOGATION_F4 = P_ELOGATION_F4;
            dbPara.P_ELOGATION_F5 = P_ELOGATION_F5;
            dbPara.P_ELOGATION_F6 = P_ELOGATION_F6;

            dbPara.P_FLAMMABILITY_W = P_FLAMMABILITY_W;
            dbPara.P_FLAMMABILITY_W2 = P_FLAMMABILITY_W2;
            dbPara.P_FLAMMABILITY_W3 = P_FLAMMABILITY_W3;
            dbPara.P_FLAMMABILITY_W4 = P_FLAMMABILITY_W4;
            dbPara.P_FLAMMABILITY_W5 = P_FLAMMABILITY_W5;

            dbPara.P_FLAMMABILITY_F = P_FLAMMABILITY_F;
            dbPara.P_FLAMMABILITY_F2 = P_FLAMMABILITY_F2;
            dbPara.P_FLAMMABILITY_F3 = P_FLAMMABILITY_F3;
            dbPara.P_FLAMMABILITY_F4 = P_FLAMMABILITY_F4;
            dbPara.P_FLAMMABILITY_F5 = P_FLAMMABILITY_F5;

            dbPara.P_EDGECOMB_W1 = P_EDGECOMB_W1;
            dbPara.P_EDGECOMB_W2 = P_EDGECOMB_W2;
            dbPara.P_EDGECOMB_W3 = P_EDGECOMB_W3;
            dbPara.P_EDGECOMB_F1 = P_EDGECOMB_F1;
            dbPara.P_EDGECOMB_F2 = P_EDGECOMB_F2;
            dbPara.P_EDGECOMB_F3 = P_EDGECOMB_F3;
            dbPara.P_STIFFNESS_W1 = P_STIFFNESS_W1;
            dbPara.P_STIFFNESS_W2 = P_STIFFNESS_W2;
            dbPara.P_STIFFNESS_W3 = P_STIFFNESS_W3;
            dbPara.P_STIFFNESS_F1 = P_STIFFNESS_F1;
            dbPara.P_STIFFNESS_F2 = P_STIFFNESS_F2;
            dbPara.P_STIFFNESS_F3 = P_STIFFNESS_F3;
            dbPara.P_TEAR_W1 = P_TEAR_W1;
            dbPara.P_TEAR_W2 = P_TEAR_W2;
            dbPara.P_TEAR_W3 = P_TEAR_W3;
            dbPara.P_TEAR_F1 = P_TEAR_F1;
            dbPara.P_TEAR_F2 = P_TEAR_F2;
            dbPara.P_TEAR_F3 = P_TEAR_F3;
            dbPara.P_STATIC_AIR1 = P_STATIC_AIR1;
            dbPara.P_STATIC_AIR2 = P_STATIC_AIR2;
            dbPara.P_STATIC_AIR3 = P_STATIC_AIR3;

            dbPara.P_STATIC_AIR4 = P_STATIC_AIR4;
            dbPara.P_STATIC_AIR5 = P_STATIC_AIR5;
            dbPara.P_STATIC_AIR6 = P_STATIC_AIR6;

            dbPara.P_DYNAMIC_AIR1 = P_DYNAMIC_AIR1;
            dbPara.P_DYNAMIC_AIR2 = P_DYNAMIC_AIR2;
            dbPara.P_DYNAMIC_AIR3 = P_DYNAMIC_AIR3;
            dbPara.P_EXPONENT1 = P_EXPONENT1;
            dbPara.P_EXPONENT2 = P_EXPONENT2;
            dbPara.P_EXPONENT3 = P_EXPONENT3;
            dbPara.P_DIMENSCHANGE_W1 = P_DIMENSCHANGE_W1;
            dbPara.P_DIMENSCHANGE_W2 = P_DIMENSCHANGE_W2;
            dbPara.P_DIMENSCHANGE_W3 = P_DIMENSCHANGE_W3;
            dbPara.P_DIMENSCHANGE_F1 = P_DIMENSCHANGE_F1;
            dbPara.P_DIMENSCHANGE_F2 = P_DIMENSCHANGE_F2;
            dbPara.P_DIMENSCHANGE_F3 = P_DIMENSCHANGE_F3;
            dbPara.P_FLEXABRASION_W1 = P_FLEXABRASION_W1;
            dbPara.P_FLEXABRASION_W2 = P_FLEXABRASION_W2;
            dbPara.P_FLEXABRASION_W3 = P_FLEXABRASION_W3;
            dbPara.P_FLEXABRASION_F1 = P_FLEXABRASION_F1;
            dbPara.P_FLEXABRASION_F2 = P_FLEXABRASION_F2;
            dbPara.P_FLEXABRASION_F3 = P_FLEXABRASION_F3;
            
            dbPara.P_STATUS = P_STATUS;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_APPROVEBY = P_APPROVEBY;
            dbPara.P_APPROVEDATE = P_APPROVEDATE;

            //Update 07/06/18
            dbPara.P_BOW1 = P_BOW1;
            dbPara.P_BOW2 = P_BOW2;
            dbPara.P_BOW3 = P_BOW3;
            dbPara.P_SKEW1 = P_SKEW1;
            dbPara.P_SKEW2 = P_SKEW2;
            dbPara.P_SKEW3 = P_SKEW3;
            dbPara.P_BENDING_W1 = P_BENDING_W1;
            dbPara.P_BENDING_W2 = P_BENDING_W2;
            dbPara.P_BENDING_W3 = P_BENDING_W3;
            dbPara.P_BENDING_F1 = P_BENDING_F1;
            dbPara.P_BENDING_F2 = P_BENDING_F2;
            dbPara.P_BENDING_F3 = P_BENDING_F3;
            dbPara.P_FLEX_SCOTT_W1 = P_FLEX_SCOTT_W1;
            dbPara.P_FLEX_SCOTT_W2 = P_FLEX_SCOTT_W2;
            dbPara.P_FLEX_SCOTT_W3 = P_FLEX_SCOTT_W3;
            dbPara.P_FLEX_SCOTT_F1 = P_FLEX_SCOTT_F1;
            dbPara.P_FLEX_SCOTT_F2 = P_FLEX_SCOTT_F2;
            dbPara.P_FLEX_SCOTT_F3 = P_FLEX_SCOTT_F3;

            LAB_INSERTPRODUCTIONResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_INSERTPRODUCTION(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        //--------//

        #region เพิ่มใหม่ LAB_GETPDFDATA ใช้ในการ Load LAB_GETPDFDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WEAVINGLOG"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <returns></returns>
        public List<LAB_GETPDFDATA> LAB_GETPDFDATA(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT)
        {
            List<LAB_GETPDFDATA> results = null;

            if (!HasConnection())
                return results;

            LAB_GETPDFDATAParameter dbPara = new LAB_GETPDFDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOG = P_WEAVINGLOG;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            
            List<LAB_GETPDFDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETPDFDATA(dbPara);
                if (null != dbResults)
                {
                    int? chkave = 0;
                    decimal? ave1 = 0;
                    decimal? ave2 = 0;
                    decimal? ave3 = 0;

                    decimal? ave4 = 0;
                    decimal? ave5 = 0;
                    decimal? ave6 = 0;

                    results = new List<LAB_GETPDFDATA>();
                    foreach (LAB_GETPDFDATAResult dbResult in dbResults)
                    {
                        LAB_GETPDFDATA inst = new LAB_GETPDFDATA();

                        inst.PROPERTY = dbResult.PROPERTY;
                        inst.YARNTYPE = dbResult.YARNTYPE;
                        inst.N1 = dbResult.N1;
                        inst.N2 = dbResult.N2;
                        inst.N3 = dbResult.N3;

                        //New 30/08/21
                        inst.N4 = dbResult.N4;
                        inst.N5 = dbResult.N5;
                        inst.N6 = dbResult.N6;
                        //---------------------//

                        chkave = 0;
                        ave1 = 0;
                        ave2 = 0;
                        ave3 = 0;

                        ave4 = 0;
                        ave5 = 0;
                        ave6 = 0;

                        if (inst.N1 != null && inst.N1 > 0)
                        {
                            ave1 = inst.N1;
                            chkave++;
                        }
                        if (inst.N2 != null && inst.N2 > 0)
                        {
                            ave2 = inst.N2;
                            chkave++;
                        }
                        if (inst.N3 != null && inst.N3 > 0)
                        {
                            ave3 = inst.N3;
                            chkave++;
                        }


                        if (inst.N4 != null && inst.N4 > 0)
                        {
                            ave4 = inst.N4;
                            chkave++;
                        }
                        if (inst.N5 != null && inst.N5 > 0)
                        {
                            ave5 = inst.N5;
                            chkave++;
                        }
                        if (inst.N6 != null && inst.N6 > 0)
                        {
                            ave6 = inst.N6;
                            chkave++;
                        }

                        if (ave4 > 0 && ave5 > 0 && ave6 > 0)
                        {
                            inst.strAVE = ("( " + ave1.ToString() + " + " + ave2.ToString() + " + " + ave3.ToString()
                                + " + " + ave4.ToString() + " + " + ave5.ToString() + " + " + ave6.ToString() + " ) / " + chkave.ToString());
                            inst.AVE = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / chkave).Value, 2);
                        }
                        else
                        {
                            inst.strAVE = ("( " + ave1.ToString() + " + " + ave2.ToString() + " + " + ave3.ToString() + " ) / " + chkave.ToString());
                            inst.AVE = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3) / chkave).Value, 2);
                        }

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

        #region เพิ่มใหม่ LAB_GETITEMTESTSPECIFICATION ใช้ในการ Load LAB_GETITEMTESTSPECIFICATION
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <returns></returns>
        public List<LAB_GETITEMTESTSPECIFICATION> LAB_GETITEMTESTSPECIFICATION(string P_ITMCODE)
        {
            List<LAB_GETITEMTESTSPECIFICATION> results = null;

            if (!HasConnection())
                return results;

            LAB_GETITEMTESTSPECIFICATIONParameter dbPara = new LAB_GETITEMTESTSPECIFICATIONParameter();
            dbPara.P_ITMCODE = P_ITMCODE;

            List<LAB_GETITEMTESTSPECIFICATIONResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETITEMTESTSPECIFICATION(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETITEMTESTSPECIFICATION>();
                    foreach (LAB_GETITEMTESTSPECIFICATIONResult dbResult in dbResults)
                    {
                        LAB_GETITEMTESTSPECIFICATION inst = new LAB_GETITEMTESTSPECIFICATION();

                        inst.ITM_CODE = dbResult.ITM_CODE;

                        inst.WIDTH = dbResult.WIDTH;

                        if (inst.WIDTH != null)
                            inst.WIDTH_Spe = inst.WIDTH.Value.ToString("#,##0.##");

                        inst.USABLE_WIDTH = dbResult.USABLE_WIDTH;
                        inst.USABLE_WIDTH_TOR = dbResult.USABLE_WIDTH_TOR;

                        if (inst.USABLE_WIDTH != null && !string.IsNullOrEmpty(inst.USABLE_WIDTH_TOR))
                            inst.USABLE_Spe = inst.USABLE_WIDTH_TOR + " " + inst.USABLE_WIDTH.Value.ToString("#,##0.##");

                        inst.NUMTHREADS_W = dbResult.NUMTHREADS_W;
                        inst.NUMTHREADS_W_TOR = dbResult.NUMTHREADS_W_TOR;

                        if (inst.NUMTHREADS_W != null && inst.NUMTHREADS_W_TOR != null)
                            inst.NUMTHREADS_W_Spe = inst.NUMTHREADS_W.Value.ToString("#,##0.##") + " +/- " + inst.NUMTHREADS_W_TOR.Value.ToString("#,##0.##");

                        inst.NUMTHREADS_F = dbResult.NUMTHREADS_F;
                        inst.NUMTHREADS_F_TOR = dbResult.NUMTHREADS_F_TOR;

                        if (inst.NUMTHREADS_F != null && inst.NUMTHREADS_F_TOR != null)
                            inst.NUMTHREADS_F_Spe = inst.NUMTHREADS_F.Value.ToString("#,##0.##") + " +/- " + inst.NUMTHREADS_F_TOR.Value.ToString("#,##0.##");

                        inst.WIDTH_SILICONE = dbResult.WIDTH_SILICONE;
                        inst.WIDTH_SILICONE_TOR = dbResult.WIDTH_SILICONE_TOR;

                        if (inst.WIDTH_SILICONE != null && !string.IsNullOrEmpty(inst.WIDTH_SILICONE_TOR))
                            inst.WIDTH_SILICONE_Spe = inst.WIDTH_SILICONE_TOR + " " + inst.WIDTH_SILICONE.Value.ToString("#,##0.##");

                        inst.TOTALWEIGHT = dbResult.TOTALWEIGHT;
                        inst.TOTALWEIGHT_TOR = dbResult.TOTALWEIGHT_TOR;

                        if (inst.TOTALWEIGHT != null && inst.TOTALWEIGHT_TOR != null)
                            inst.TOTALWEIGHT_Spe = inst.TOTALWEIGHT.Value.ToString("#,##0.##") + " +/- " + inst.TOTALWEIGHT_TOR.Value.ToString("#,##0.##");

                        inst.UNCOATEDWEIGHT = dbResult.UNCOATEDWEIGHT;
                        inst.UNCOATEDWEIGHT_TOR = dbResult.UNCOATEDWEIGHT_TOR;

                        if (inst.UNCOATEDWEIGHT != null && inst.UNCOATEDWEIGHT_TOR != null)
                            inst.UNCOATEDWEIGHT_Spe = inst.UNCOATEDWEIGHT.Value.ToString("#,##0.##") + " +/- " + inst.UNCOATEDWEIGHT_TOR.Value.ToString("#,##0.##");

                        inst.COATINGWEIGHT = dbResult.COATINGWEIGHT;
                        inst.COATINGWEIGHT_TOR = dbResult.COATINGWEIGHT_TOR;

                        if (inst.COATINGWEIGHT != null && inst.COATINGWEIGHT_TOR != null)
                            inst.COATINGWEIGHT_Spe = inst.COATINGWEIGHT.Value.ToString("#,##0.##") + " +/- " + inst.COATINGWEIGHT_TOR.Value.ToString("#,##0.##");

                        inst.THICKNESS = dbResult.THICKNESS;
                        inst.THICKNESS_TOR = dbResult.THICKNESS_TOR;

                        //Update 28/10/20
                        if (inst.THICKNESS != null && inst.THICKNESS_TOR != null)
                        {
                            inst.THICKNESS_Spe = inst.THICKNESS.Value.ToString("#,##0.##") + " +/- " + inst.THICKNESS_TOR.Value.ToString("#,##0.##");
                        }
                        else
                        {
                            if (inst.THICKNESS != null)
                                inst.THICKNESS_Spe = inst.THICKNESS.Value.ToString("#,##0.00");
                        }

                        inst.MAXFORCE_W = dbResult.MAXFORCE_W;
                        inst.MAXFORCE_W_TOR = dbResult.MAXFORCE_W_TOR;

                        if (inst.MAXFORCE_W != null && !string.IsNullOrEmpty(inst.MAXFORCE_W_TOR))
                            inst.MAXFORCE_W_Spe = inst.MAXFORCE_W_TOR + " " + inst.MAXFORCE_W.Value.ToString("#,##0.##");

                        inst.MAXFORCE_F = dbResult.MAXFORCE_F;
                        inst.MAXFORCE_F_TOR = dbResult.MAXFORCE_F_TOR;

                        if (inst.MAXFORCE_F != null && !string.IsNullOrEmpty(inst.MAXFORCE_F_TOR))
                            inst.MAXFORCE_F_Spe = inst.MAXFORCE_F_TOR + " " + inst.MAXFORCE_F.Value.ToString("#,##0.##");

                        // ปรับ 07/07/18
                        inst.ELONGATIONFORCE_W = dbResult.ELONGATIONFORCE_W;
                        inst.ELONGATIONFORCE_W_TOR = dbResult.ELONGATIONFORCE_W_TOR;

                        if (inst.ELONGATIONFORCE_W != null && !string.IsNullOrEmpty(inst.ELONGATIONFORCE_W_TOR))
                        {
                            if (inst.ELONGATIONFORCE_W_TOR.Contains("MAX"))
                                inst.ELONGATIONFORCE_W_Spe = inst.DIMENSCHANGE_W_TOR + " " + inst.ELONGATIONFORCE_W.Value.ToString("#,##0.##");
                            else if (inst.ELONGATIONFORCE_W_TOR.Contains("MIN"))
                                inst.ELONGATIONFORCE_W_Spe = inst.ELONGATIONFORCE_W_TOR + " " + inst.ELONGATIONFORCE_W.Value.ToString("#,##0.##");
                            else
                                inst.ELONGATIONFORCE_W_Spe = inst.ELONGATIONFORCE_W.Value.ToString("#,##0.##") + " +/- " + inst.ELONGATIONFORCE_W_TOR;
                        }

                        //if (inst.ELONGATIONFORCE_W != null && !string.IsNullOrEmpty(inst.ELONGATIONFORCE_W_TOR))
                        //    inst.ELONGATIONFORCE_W_Spe = inst.ELONGATIONFORCE_W_TOR + " " + inst.ELONGATIONFORCE_W.Value.ToString("#,##0.##");

                        // ปรับ 07/07/18
                        inst.ELONGATIONFORCE_F = dbResult.ELONGATIONFORCE_F;
                        inst.ELONGATIONFORCE_F_TOR = dbResult.ELONGATIONFORCE_F_TOR;

                        if (inst.ELONGATIONFORCE_F != null && !string.IsNullOrEmpty(inst.ELONGATIONFORCE_F_TOR))
                        {
                            if (inst.ELONGATIONFORCE_F_TOR.Contains("MAX"))
                                inst.ELONGATIONFORCE_F_Spe = inst.DIMENSCHANGE_F_TOR + " " + inst.ELONGATIONFORCE_F.Value.ToString("#,##0.##");
                            else if (inst.ELONGATIONFORCE_F_TOR.Contains("MIN"))
                                inst.ELONGATIONFORCE_F_Spe = inst.ELONGATIONFORCE_F_TOR + " " + inst.ELONGATIONFORCE_F.Value.ToString("#,##0.##");
                            else
                                inst.ELONGATIONFORCE_F_Spe = inst.ELONGATIONFORCE_F.Value.ToString("#,##0.##") + " +/- " + inst.ELONGATIONFORCE_F_TOR;
                        }

                        //if (inst.ELONGATIONFORCE_F != null && !string.IsNullOrEmpty(inst.ELONGATIONFORCE_F_TOR))
                        //    inst.ELONGATIONFORCE_F_Spe = inst.ELONGATIONFORCE_F_TOR + " " + inst.ELONGATIONFORCE_F.Value.ToString("#,##0.##");

                        inst.FLAMMABILITY_W = dbResult.FLAMMABILITY_W;
                        inst.FLAMMABILITY_W_TOR = dbResult.FLAMMABILITY_W_TOR;

                        if (inst.FLAMMABILITY_W != null && !string.IsNullOrEmpty(inst.FLAMMABILITY_W_TOR))
                            inst.FLAMMABILITY_W_Spe = inst.FLAMMABILITY_W_TOR + " " + inst.FLAMMABILITY_W.Value.ToString("#,##0.##");

                        inst.FLAMMABILITY_F = dbResult.FLAMMABILITY_F;
                        inst.FLAMMABILITY_F_TOR = dbResult.FLAMMABILITY_F_TOR;

                        if (inst.FLAMMABILITY_F != null && !string.IsNullOrEmpty(inst.FLAMMABILITY_F_TOR))
                            inst.FLAMMABILITY_F_Spe = inst.FLAMMABILITY_F_TOR + " " + inst.FLAMMABILITY_F.Value.ToString("#,##0.##");

                        inst.EDGECOMB_W = dbResult.EDGECOMB_W;
                        inst.EDGECOMB_W_TOR = dbResult.EDGECOMB_W_TOR;

                        if (inst.EDGECOMB_W != null && !string.IsNullOrEmpty(inst.EDGECOMB_W_TOR))
                            inst.EDGECOMB_W_Spe = inst.EDGECOMB_W_TOR + " " + inst.EDGECOMB_W.Value.ToString("#,##0.##");

                        inst.EDGECOMB_F = dbResult.EDGECOMB_F;
                        inst.EDGECOMB_F_TOR = dbResult.EDGECOMB_F_TOR;

                        if (inst.EDGECOMB_F != null && !string.IsNullOrEmpty(inst.EDGECOMB_F_TOR))
                            inst.EDGECOMB_F_Spe = inst.EDGECOMB_F_TOR + " " + inst.EDGECOMB_F.Value.ToString("#,##0.##");

                        inst.STIFFNESS_W = dbResult.STIFFNESS_W;
                        inst.STIFFNESS_W_TOR = dbResult.STIFFNESS_W_TOR;

                        if (inst.STIFFNESS_W != null && !string.IsNullOrEmpty(inst.STIFFNESS_W_TOR))
                            inst.STIFFNESS_W_Spe = inst.STIFFNESS_W_TOR + " " + inst.STIFFNESS_W.Value.ToString("#,##0.##");

                        inst.STIFFNESS_F = dbResult.STIFFNESS_F;
                        inst.STIFFNESS_F_TOR = dbResult.STIFFNESS_F_TOR;

                        if (inst.STIFFNESS_F != null && !string.IsNullOrEmpty(inst.STIFFNESS_F_TOR))
                            inst.STIFFNESS_F_Spe = inst.STIFFNESS_F_TOR + " " + inst.STIFFNESS_F.Value.ToString("#,##0.##");

                        inst.TEAR_W = dbResult.TEAR_W;
                        inst.TEAR_W_TOR = dbResult.TEAR_W_TOR;

                        if (inst.TEAR_W != null && !string.IsNullOrEmpty(inst.TEAR_W_TOR))
                            inst.TEAR_W_Spe = inst.TEAR_W_TOR + " " + inst.TEAR_W.Value.ToString("#,##0.##");

                        inst.TEAR_F = dbResult.TEAR_F;
                        inst.TEAR_F_TOR = dbResult.TEAR_F_TOR;

                        if (inst.TEAR_F != null && !string.IsNullOrEmpty(inst.TEAR_F_TOR))
                            inst.TEAR_F_Spe = inst.TEAR_F_TOR + " " + inst.TEAR_F.Value.ToString("#,##0.##");

                        inst.STATIC_AIR = dbResult.STATIC_AIR;
                        inst.STATIC_AIR_TOR = dbResult.STATIC_AIR_TOR;

                        if (inst.STATIC_AIR != null && !string.IsNullOrEmpty(inst.STATIC_AIR_TOR))
                            inst.STATIC_AIR_Spe = inst.STATIC_AIR_TOR + " " + inst.STATIC_AIR.Value.ToString("#,##0.##");

                        inst.DYNAMIC_AIR = dbResult.DYNAMIC_AIR;
                        inst.DYNAMIC_AIR_TOR = dbResult.DYNAMIC_AIR_TOR;

                        if (inst.DYNAMIC_AIR != null && inst.DYNAMIC_AIR_TOR != null)
                            inst.DYNAMIC_AIR_Spe = inst.DYNAMIC_AIR.Value.ToString("#,##0.##") + " +/- " + inst.DYNAMIC_AIR_TOR.Value.ToString("#,##0.##");

                        inst.EXPONENT = dbResult.EXPONENT;
                        inst.EXPONENT_TOR = dbResult.EXPONENT_TOR;

                        if (inst.EXPONENT != null && inst.EXPONENT_TOR != null)
                            inst.EXPONENT_Spe = inst.EXPONENT.Value.ToString("#,##0.##") + " +/- " + inst.EXPONENT_TOR.Value.ToString("#,##0.##");

                        inst.DIMENSCHANGE_W = dbResult.DIMENSCHANGE_W;
                        inst.DIMENSCHANGE_W_TOR = dbResult.DIMENSCHANGE_W_TOR;

                        // ปรับ 09/06/18
                        if (inst.DIMENSCHANGE_W != null && !string.IsNullOrEmpty(inst.DIMENSCHANGE_W_TOR))
                        {
                            if (inst.DIMENSCHANGE_W_TOR.Contains("MAX"))
                                inst.DIMENSCHANGE_W_Spe = inst.DIMENSCHANGE_W_TOR + " " + inst.DIMENSCHANGE_W.Value.ToString("#,##0.##");
                            else if (inst.DIMENSCHANGE_W_TOR.Contains("MIN"))
                                inst.DIMENSCHANGE_W_Spe = inst.DIMENSCHANGE_W_TOR + " " + inst.DIMENSCHANGE_W.Value.ToString("#,##0.##");
                            else
                                inst.DIMENSCHANGE_W_Spe = inst.DIMENSCHANGE_W.Value.ToString("#,##0.##") + " +/- " + inst.DIMENSCHANGE_W_TOR;
                        }

                        inst.DIMENSCHANGE_F = dbResult.DIMENSCHANGE_F;
                        inst.DIMENSCHANGE_F_TOR = dbResult.DIMENSCHANGE_F_TOR;

                        // ปรับ 09/06/18
                        if (inst.DIMENSCHANGE_F != null && !string.IsNullOrEmpty(inst.DIMENSCHANGE_F_TOR))
                        {
                            if (inst.DIMENSCHANGE_F_TOR.Contains("MAX"))
                                inst.DIMENSCHANGE_F_Spe = inst.DIMENSCHANGE_F_TOR + " " + inst.DIMENSCHANGE_F.Value.ToString("#,##0.##");
                            else if (inst.DIMENSCHANGE_F_TOR.Contains("MIN"))
                                inst.DIMENSCHANGE_F_Spe = inst.DIMENSCHANGE_F_TOR + " " + inst.DIMENSCHANGE_F.Value.ToString("#,##0.##");
                            else
                                inst.DIMENSCHANGE_F_Spe = inst.DIMENSCHANGE_F.Value.ToString("#,##0.##") + " +/- " + inst.DIMENSCHANGE_F_TOR;
                        }
                           

                        inst.FLEXABRASION_W = dbResult.FLEXABRASION_W;
                        inst.FLEXABRASION_W_TOR = dbResult.FLEXABRASION_W_TOR;

                        if (inst.FLEXABRASION_W != null && !string.IsNullOrEmpty(inst.FLEXABRASION_W_TOR))
                            inst.FLEXABRASION_W_Spe = inst.FLEXABRASION_W_TOR + " " + inst.FLEXABRASION_W.Value.ToString("#,##0.##");

                        inst.FLEXABRASION_F = dbResult.FLEXABRASION_F;
                        inst.FLEXABRASION_F_TOR = dbResult.FLEXABRASION_F_TOR;

                        if (inst.FLEXABRASION_F != null && !string.IsNullOrEmpty(inst.FLEXABRASION_F_TOR))
                            inst.FLEXABRASION_F_Spe = inst.FLEXABRASION_F_TOR + " " + inst.FLEXABRASION_F.Value.ToString("#,##0.##");

                        //Update 07/07/18

                        inst.BOW = dbResult.BOW;
                        inst.BOW_TOR = dbResult.BOW_TOR;

                        if (inst.BOW != null && !string.IsNullOrEmpty(inst.BOW_TOR))
                        {
                            if (inst.BOW_TOR.Contains("MAX"))
                                inst.BOW_Spe = inst.BOW_TOR + " " + inst.BOW.Value.ToString("#,##0.##");
                            else if (inst.BOW_TOR.Contains("MIN"))
                                inst.BOW_Spe = inst.BOW_TOR + " " + inst.BOW.Value.ToString("#,##0.##");
                            else
                                inst.BOW_Spe = inst.BOW.Value.ToString("#,##0.##") + " +/- " + inst.BOW_TOR;
                        }

                        inst.SKEW = dbResult.SKEW;
                        inst.SKEW_TOR = dbResult.SKEW_TOR;

                        if (inst.SKEW != null && !string.IsNullOrEmpty(inst.SKEW_TOR))
                        {
                            if (inst.SKEW_TOR.Contains("MAX"))
                                inst.SKEW_Spe = inst.SKEW_TOR + " " + inst.SKEW.Value.ToString("#,##0.##");
                            else if (inst.SKEW_TOR.Contains("MIN"))
                                inst.SKEW_Spe = inst.SKEW_TOR + " " + inst.SKEW.Value.ToString("#,##0.##");
                            else
                                inst.SKEW_Spe = inst.SKEW.Value.ToString("#,##0.##") + " +/- " + inst.BOW_TOR;
                        }

                        inst.BENDING_W = dbResult.BENDING_W;
                        inst.BENDING_W_TOR = dbResult.BENDING_W_TOR;

                        if (inst.BENDING_W != null && !string.IsNullOrEmpty(inst.BENDING_W_TOR))
                        {
                            if (inst.BENDING_W_TOR.Contains("MAX"))
                                inst.BENDING_W_Spe = inst.BENDING_W_TOR + " " + inst.BENDING_W.Value.ToString("#,##0.##");
                            else if (inst.BENDING_W_TOR.Contains("MIN"))
                                inst.BENDING_W_Spe = inst.BENDING_W_TOR + " " + inst.BENDING_W.Value.ToString("#,##0.##");
                            else
                                inst.BENDING_W_Spe = inst.BENDING_W.Value.ToString("#,##0.##") + " +/- " + inst.BENDING_W_TOR;
                        }

                        inst.BENDING_F = dbResult.BENDING_F;
                        inst.BENDING_F_TOR = dbResult.BENDING_F_TOR;

                        if (inst.BENDING_F != null && !string.IsNullOrEmpty(inst.BENDING_F_TOR))
                        {
                            if (inst.BENDING_F_TOR.Contains("MAX"))
                                inst.BENDING_F_Spe = inst.BENDING_F_TOR + " " + inst.BENDING_F.Value.ToString("#,##0.##");
                            else if (inst.BENDING_F_TOR.Contains("MIN"))
                                inst.BENDING_F_Spe = inst.BENDING_F_TOR + " " + inst.BENDING_F.Value.ToString("#,##0.##");
                            else
                                inst.BENDING_F_Spe = inst.BENDING_F.Value.ToString("#,##0.##") + " +/- " + inst.BENDING_F_TOR;
                        }

                        inst.FLEX_SCOTT_W = dbResult.FLEX_SCOTT_W;
                        inst.FLEX_SCOTT_W_TOR = dbResult.FLEX_SCOTT_W_TOR;

                        if (inst.FLEX_SCOTT_W != null && !string.IsNullOrEmpty(inst.FLEX_SCOTT_W_TOR))
                        {
                            if (inst.FLEX_SCOTT_W_TOR.Contains("MAX"))
                                inst.FLEX_SCOTT_W_Spe = inst.FLEX_SCOTT_W_TOR + " " + inst.FLEX_SCOTT_W.Value.ToString("#,##0.##");
                            else if (inst.FLEX_SCOTT_W_TOR.Contains("MIN"))
                                inst.FLEX_SCOTT_W_Spe = inst.FLEX_SCOTT_W_TOR + " " + inst.FLEX_SCOTT_W.Value.ToString("#,##0.##");
                            else
                                inst.FLEX_SCOTT_W_Spe = inst.FLEX_SCOTT_W.Value.ToString("#,##0.##") + " +/- " + inst.FLEX_SCOTT_W_TOR;
                        }

                        inst.FLEX_SCOTT_F = dbResult.FLEX_SCOTT_F;
                        inst.FLEX_SCOTT_F_TOR = dbResult.FLEX_SCOTT_F_TOR;

                        if (inst.FLEX_SCOTT_F != null && !string.IsNullOrEmpty(inst.FLEX_SCOTT_F_TOR))
                        {
                            if (inst.FLEX_SCOTT_F_TOR.Contains("MAX"))
                                inst.FLEX_SCOTT_F_Spe = inst.FLEX_SCOTT_F_TOR + " " + inst.FLEX_SCOTT_F.Value.ToString("#,##0.##");
                            else if (inst.FLEX_SCOTT_F_TOR.Contains("MIN"))
                                inst.FLEX_SCOTT_F_Spe = inst.FLEX_SCOTT_F_TOR + " " + inst.FLEX_SCOTT_F.Value.ToString("#,##0.##");
                            else
                                inst.FLEX_SCOTT_F_Spe = inst.FLEX_SCOTT_F.Value.ToString("#,##0.##") + " +/- " + inst.FLEX_SCOTT_F_TOR;
                        }

                        //New 7/9/22
                        inst.USABLE_WIDTH_LCL = dbResult.USABLE_WIDTH_LCL;
                        inst.USABLE_WIDTH_UCL = dbResult.USABLE_WIDTH_UCL;
                        inst.TOTALWEIGHT_LCL = dbResult.TOTALWEIGHT_LCL;
                        inst.TOTALWEIGHT_UCL = dbResult.TOTALWEIGHT_UCL;
                        inst.NUMTHREADS_W_LCL = dbResult.NUMTHREADS_W_LCL;
                        inst.NUMTHREADS_W_UCL = dbResult.NUMTHREADS_W_UCL;
                        inst.NUMTHREADS_F_LCL = dbResult.NUMTHREADS_F_LCL;
                        inst.NUMTHREADS_F_UCL = dbResult.NUMTHREADS_F_UCL;
                        inst.MAXFORCE_W_LCL = dbResult.MAXFORCE_W_LCL;
                        inst.MAXFORCE_W_UCL = dbResult.MAXFORCE_W_UCL;
                        inst.MAXFORCE_F_LCL = dbResult.MAXFORCE_F_LCL;
                        inst.MAXFORCE_F_UCL = dbResult.MAXFORCE_F_UCL;
                        inst.ELONGATIONFORCE_W_LCL = dbResult.ELONGATIONFORCE_W_LCL;
                        inst.ELONGATIONFORCE_W_UCL = dbResult.ELONGATIONFORCE_W_UCL;
                        inst.ELONGATIONFORCE_F_LCL = dbResult.ELONGATIONFORCE_F_LCL;
                        inst.ELONGATIONFORCE_F_UCL = dbResult.ELONGATIONFORCE_F_UCL;
                        inst.EDGECOMB_W_LCL = dbResult.EDGECOMB_W_LCL;
                        inst.EDGECOMB_W_UCL = dbResult.EDGECOMB_W_UCL;
                        inst.EDGECOMB_F_LCL = dbResult.EDGECOMB_F_LCL;
                        inst.EDGECOMB_F_UCL = dbResult.EDGECOMB_F_UCL;
                        inst.TEAR_W_LCL = dbResult.TEAR_W_LCL;
                        inst.TEAR_W_UCL = dbResult.TEAR_W_UCL;
                        inst.TEAR_F_LCL = dbResult.TEAR_F_LCL;
                        inst.TEAR_F_UCL = dbResult.TEAR_F_UCL;
                        inst.STATIC_AIR_LCL = dbResult.STATIC_AIR_LCL;
                        inst.STATIC_AIR_UCL = dbResult.STATIC_AIR_UCL;
                        inst.DYNAMIC_AIR_LCL = dbResult.DYNAMIC_AIR_LCL;
                        inst.DYNAMIC_AIR_UCL = dbResult.DYNAMIC_AIR_UCL;
                        inst.EXPONENT_LCL = dbResult.EXPONENT_LCL;
                        inst.EXPONENT_UCL = dbResult.EXPONENT_UCL;

                        //เพิ่ม 11/1/65
                        inst.CUSTOMERID = dbResult.CUSTOMERID;

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

        #region เพิ่มใหม่ LAB_GETITEMTESTPROPERTY ใช้ในการ Load LAB_GETITEMTESTPROPERTY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <returns></returns>
        public List<LAB_GETITEMTESTPROPERTY> LAB_GETITEMTESTPROPERTY(string P_ITMCODE)
        {
            List<LAB_GETITEMTESTPROPERTY> results = null;

            if (!HasConnection())
                return results;

            LAB_GETITEMTESTPROPERTYParameter dbPara = new LAB_GETITEMTESTPROPERTYParameter();
            dbPara.P_ITMCODE = P_ITMCODE;

            List<LAB_GETITEMTESTPROPERTYResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETITEMTESTPROPERTY(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETITEMTESTPROPERTY>();
                    foreach (LAB_GETITEMTESTPROPERTYResult dbResult in dbResults)
                    {
                        LAB_GETITEMTESTPROPERTY inst = new LAB_GETITEMTESTPROPERTY();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.USABLE_WIDTH = dbResult.USABLE_WIDTH;
                        inst.WIDTH_SILICONE = dbResult.WIDTH_SILICONE;
                        inst.NUMTHREADS_W = dbResult.NUMTHREADS_W;
                        inst.NUMTHREADS_F = dbResult.NUMTHREADS_F;
                        inst.TOTALWEIGHT = dbResult.TOTALWEIGHT;
                        inst.UNCOATEDWEIGHT = dbResult.UNCOATEDWEIGHT;
                        inst.COATINGWEIGHT = dbResult.COATINGWEIGHT;
                        inst.THICKNESS = dbResult.THICKNESS;
                        inst.MAXFORCE_W = dbResult.MAXFORCE_W;
                        inst.MAXFORCE_F = dbResult.MAXFORCE_F;
                        inst.ELONGATIONFORCE_W = dbResult.ELONGATIONFORCE_W;
                        inst.ELONGATIONFORCE_F = dbResult.ELONGATIONFORCE_F;
                        inst.FLAMMABILITY_W = dbResult.FLAMMABILITY_W;
                        inst.FLAMMABILITY_F = dbResult.FLAMMABILITY_F;
                        inst.EDGECOMB_W = dbResult.EDGECOMB_W;
                        inst.EDGECOMB_F = dbResult.EDGECOMB_F;
                        inst.STIFFNESS_W = dbResult.STIFFNESS_W;
                        inst.STIFFNESS_F = dbResult.STIFFNESS_F;
                        inst.TEAR_W = dbResult.TEAR_W;
                        inst.TEAR_F = dbResult.TEAR_F;
                        inst.STATIC_AIR = dbResult.STATIC_AIR;
                        inst.DYNAMIC_AIR = dbResult.DYNAMIC_AIR;
                        inst.EXPONENT = dbResult.EXPONENT;
                        inst.DIMENSCHANGE_W = dbResult.DIMENSCHANGE_W;
                        inst.DIMENSCHANGE_F = dbResult.DIMENSCHANGE_F;
                        inst.FLEXABRASION_W = dbResult.FLEXABRASION_W;
                        inst.FLEXABRASION_F = dbResult.FLEXABRASION_F;
                        inst.BOW = dbResult.BOW;
                        inst.SKEW = dbResult.SKEW;

                        //Update 07/07/18
                        inst.BENDING_W = dbResult.BENDING_W;
                        inst.BENDING_F = dbResult.BENDING_F;
                        inst.FLEX_SCOTT_W = dbResult.FLEX_SCOTT_W;
                        inst.FLEX_SCOTT_F = dbResult.FLEX_SCOTT_F;

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


        // -- Update 15/06/18 -- //

        #region MC_GETLOOMLIST
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MC_GETLOOMLIST> MC_GETLOOMLIST()
        {
            List<MC_GETLOOMLIST> results = new List<MC_GETLOOMLIST>();

            if (!HasConnection())
                return results;

            MC_GETLOOMLISTParameter dbPara = new MC_GETLOOMLISTParameter();

            List<MC_GETLOOMLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.MC_GETLOOMLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<MC_GETLOOMLIST>();

                    MC_GETLOOMLIST inst = new MC_GETLOOMLIST();

                    inst.MCNAME = "All";
                    results.Add(inst);

                    foreach (MC_GETLOOMLISTResult dbResult in dbResults)
                    {
                        inst = new MC_GETLOOMLIST();

                        inst.MCNAME = dbResult.MCNAME;
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

        #region เพิ่มใหม่ LAB_SEARCHLABENTRYPRODUCTION ใช้ในการ Load LAB_SEARCHLABENTRYPRODUCTION
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_ENTRYSTARTDATE"></param>
        /// <param name="P_ENTRYENDDATE"></param>
        /// <param name="P_LOOM"></param>
        /// <param name="P_FINISHPROCESS"></param>
        /// <returns></returns>
        public List<LAB_SEARCHLABENTRYPRODUCTION> LAB_SEARCHLABENTRYPRODUCTION(string P_ITMCODE, string P_ENTRYSTARTDATE, string P_ENTRYENDDATE, string P_LOOM, string P_FINISHPROCESS)
        {
            List<LAB_SEARCHLABENTRYPRODUCTION> results = null;

            if (!HasConnection())
                return results;

            LAB_SEARCHLABENTRYPRODUCTIONParameter dbPara = new LAB_SEARCHLABENTRYPRODUCTIONParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_ENTRYSTARTDATE = P_ENTRYSTARTDATE;
            dbPara.P_ENTRYENDDATE = P_ENTRYENDDATE;
            dbPara.P_LOOM = P_LOOM;
            dbPara.P_FINISHPROCESS = P_FINISHPROCESS;

            List<LAB_SEARCHLABENTRYPRODUCTIONResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_SEARCHLABENTRYPRODUCTION(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_SEARCHLABENTRYPRODUCTION>();
                    foreach (LAB_SEARCHLABENTRYPRODUCTIONResult dbResult in dbResults)
                    {
                        LAB_SEARCHLABENTRYPRODUCTION inst = new LAB_SEARCHLABENTRYPRODUCTION();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ENTRYDATE = dbResult.ENTRYDATE;
                        inst.ENTEYBY = dbResult.ENTEYBY;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.USABLE_WIDTH1 = dbResult.USABLE_WIDTH1;
                        inst.USABLE_WIDTH2 = dbResult.USABLE_WIDTH2;
                        inst.USABLE_WIDTH3 = dbResult.USABLE_WIDTH3;
                        inst.WIDTH_SILICONE1 = dbResult.WIDTH_SILICONE1;
                        inst.WIDTH_SILICONE2 = dbResult.WIDTH_SILICONE2;
                        inst.WIDTH_SILICONE3 = dbResult.WIDTH_SILICONE3;
                        inst.NUMTHREADS_W1 = dbResult.NUMTHREADS_W1;
                        inst.NUMTHREADS_W2 = dbResult.NUMTHREADS_W2;
                        inst.NUMTHREADS_W3 = dbResult.NUMTHREADS_W3;
                        inst.NUMTHREADS_F1 = dbResult.NUMTHREADS_F1;
                        inst.NUMTHREADS_F2 = dbResult.NUMTHREADS_F2;
                        inst.NUMTHREADS_F3 = dbResult.NUMTHREADS_F3;
                        inst.TOTALWEIGHT1 = dbResult.TOTALWEIGHT1;
                        inst.TOTALWEIGHT2 = dbResult.TOTALWEIGHT2;
                        inst.TOTALWEIGHT3 = dbResult.TOTALWEIGHT3;
                        inst.TOTALWEIGHT4 = dbResult.TOTALWEIGHT4;
                        inst.TOTALWEIGHT5 = dbResult.TOTALWEIGHT5;
                        inst.TOTALWEIGHT6 = dbResult.TOTALWEIGHT6;
                        inst.UNCOATEDWEIGHT1 = dbResult.UNCOATEDWEIGHT1;
                        inst.UNCOATEDWEIGHT2 = dbResult.UNCOATEDWEIGHT2;
                        inst.UNCOATEDWEIGHT3 = dbResult.UNCOATEDWEIGHT3;
                        inst.UNCOATEDWEIGHT4 = dbResult.UNCOATEDWEIGHT4;
                        inst.UNCOATEDWEIGHT5 = dbResult.UNCOATEDWEIGHT5;
                        inst.UNCOATEDWEIGHT6 = dbResult.UNCOATEDWEIGHT6;
                        inst.COATINGWEIGHT1 = dbResult.COATINGWEIGHT1;
                        inst.COATINGWEIGHT2 = dbResult.COATINGWEIGHT2;
                        inst.COATINGWEIGHT3 = dbResult.COATINGWEIGHT3;
                        inst.COATINGWEIGHT4 = dbResult.COATINGWEIGHT4;
                        inst.COATINGWEIGHT5 = dbResult.COATINGWEIGHT5;
                        inst.COATINGWEIGHT6 = dbResult.COATINGWEIGHT6;
                        inst.THICKNESS1 = dbResult.THICKNESS1;
                        inst.THICKNESS2 = dbResult.THICKNESS2;
                        inst.THICKNESS3 = dbResult.THICKNESS3;
                        inst.MAXFORCE_W1 = dbResult.MAXFORCE_W1;
                        inst.MAXFORCE_W2 = dbResult.MAXFORCE_W2;
                        inst.MAXFORCE_W3 = dbResult.MAXFORCE_W3;
                        inst.MAXFORCE_F1 = dbResult.MAXFORCE_F1;
                        inst.MAXFORCE_F2 = dbResult.MAXFORCE_F2;
                        inst.MAXFORCE_F3 = dbResult.MAXFORCE_F3;
                        inst.ELONGATIONFORCE_W1 = dbResult.ELONGATIONFORCE_W1;
                        inst.ELONGATIONFORCE_W2 = dbResult.ELONGATIONFORCE_W2;
                        inst.ELONGATIONFORCE_W3 = dbResult.ELONGATIONFORCE_W3;
                        inst.ELONGATIONFORCE_F1 = dbResult.ELONGATIONFORCE_F1;
                        inst.ELONGATIONFORCE_F2 = dbResult.ELONGATIONFORCE_F2;
                        inst.ELONGATIONFORCE_F3 = dbResult.ELONGATIONFORCE_F3;

                        inst.FLAMMABILITY_W = dbResult.FLAMMABILITY_W;
                        inst.FLAMMABILITY_W2 = dbResult.FLAMMABILITY_W2;
                        inst.FLAMMABILITY_W3 = dbResult.FLAMMABILITY_W3;
                        inst.FLAMMABILITY_W4 = dbResult.FLAMMABILITY_W4;
                        inst.FLAMMABILITY_W5 = dbResult.FLAMMABILITY_W5;

                        inst.FLAMMABILITY_F = dbResult.FLAMMABILITY_F;
                        inst.FLAMMABILITY_F2 = dbResult.FLAMMABILITY_F2;
                        inst.FLAMMABILITY_F3 = dbResult.FLAMMABILITY_F3;
                        inst.FLAMMABILITY_F4 = dbResult.FLAMMABILITY_F4;
                        inst.FLAMMABILITY_F5 = dbResult.FLAMMABILITY_F5;

                        inst.EDGECOMB_W1 = dbResult.EDGECOMB_W1;
                        inst.EDGECOMB_W2 = dbResult.EDGECOMB_W2;
                        inst.EDGECOMB_W3 = dbResult.EDGECOMB_W3;
                        inst.EDGECOMB_F1 = dbResult.EDGECOMB_F1;
                        inst.EDGECOMB_F2 = dbResult.EDGECOMB_F2;
                        inst.EDGECOMB_F3 = dbResult.EDGECOMB_F3;
                        inst.STIFFNESS_W1 = dbResult.STIFFNESS_W1;
                        inst.STIFFNESS_W2 = dbResult.STIFFNESS_W2;
                        inst.STIFFNESS_W3 = dbResult.STIFFNESS_W3;
                        inst.STIFFNESS_F1 = dbResult.STIFFNESS_F1;
                        inst.STIFFNESS_F2 = dbResult.STIFFNESS_F2;
                        inst.STIFFNESS_F3 = dbResult.STIFFNESS_F3;
                        inst.TEAR_W1 = dbResult.TEAR_W1;
                        inst.TEAR_W2 = dbResult.TEAR_W2;
                        inst.TEAR_W3 = dbResult.TEAR_W3;
                        inst.TEAR_F1 = dbResult.TEAR_F1;
                        inst.TEAR_F2 = dbResult.TEAR_F2;
                        inst.TEAR_F3 = dbResult.TEAR_F3;
                        inst.STATIC_AIR1 = dbResult.STATIC_AIR1;
                        inst.STATIC_AIR2 = dbResult.STATIC_AIR2;
                        inst.STATIC_AIR3 = dbResult.STATIC_AIR3;

                        inst.STATIC_AIR4 = dbResult.STATIC_AIR4;
                        inst.STATIC_AIR5 = dbResult.STATIC_AIR5;
                        inst.STATIC_AIR6 = dbResult.STATIC_AIR6;

                        inst.DYNAMIC_AIR1 = dbResult.DYNAMIC_AIR1;
                        inst.DYNAMIC_AIR2 = dbResult.DYNAMIC_AIR2;
                        inst.DYNAMIC_AIR3 = dbResult.DYNAMIC_AIR3;
                        inst.EXPONENT1 = dbResult.EXPONENT1;
                        inst.EXPONENT2 = dbResult.EXPONENT2;
                        inst.EXPONENT3 = dbResult.EXPONENT3;
                        inst.DIMENSCHANGE_W1 = dbResult.DIMENSCHANGE_W1;
                        inst.DIMENSCHANGE_W2 = dbResult.DIMENSCHANGE_W2;
                        inst.DIMENSCHANGE_W3 = dbResult.DIMENSCHANGE_W3;
                        inst.DIMENSCHANGE_F1 = dbResult.DIMENSCHANGE_F1;
                        inst.DIMENSCHANGE_F2 = dbResult.DIMENSCHANGE_F2;
                        inst.DIMENSCHANGE_F3 = dbResult.DIMENSCHANGE_F3;
                        inst.FLEXABRASION_W1 = dbResult.FLEXABRASION_W1;
                        inst.FLEXABRASION_W2 = dbResult.FLEXABRASION_W2;
                        inst.FLEXABRASION_W3 = dbResult.FLEXABRASION_W3;
                        inst.FLEXABRASION_F1 = dbResult.FLEXABRASION_F1;
                        inst.FLEXABRASION_F2 = dbResult.FLEXABRASION_F2;
                        inst.FLEXABRASION_F3 = dbResult.FLEXABRASION_F3;
                        
                        inst.STATUS = dbResult.STATUS;
                        inst.REMARK = dbResult.REMARK;
                        inst.APPROVEBY = dbResult.APPROVEBY;
                        inst.APPROVEDATE = dbResult.APPROVEDATE;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.FINISHLENGTH = dbResult.FINISHLENGTH;
                        inst.FINISHINGPROCESS = dbResult.FINISHINGPROCESS;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.FINISHINGMC = dbResult.FINISHINGMC;

                        // Update 06/07/18
                        inst.BOW1 = dbResult.BOW1;
                        inst.BOW2 = dbResult.BOW2;
                        inst.BOW3 = dbResult.BOW3;
                        inst.SKEW1 = dbResult.SKEW1;
                        inst.SKEW2 = dbResult.SKEW2;
                        inst.SKEW3 = dbResult.SKEW3;
                        inst.BENDING_W1 = dbResult.BENDING_W1;
                        inst.BENDING_W2 = dbResult.BENDING_W2;
                        inst.BENDING_W3 = dbResult.BENDING_W3;
                        inst.BENDING_F1 = dbResult.BENDING_F1;
                        inst.BENDING_F2 = dbResult.BENDING_F2;
                        inst.BENDING_F3 = dbResult.BENDING_F3;
                        inst.FLEX_SCOTT_W1 = dbResult.FLEX_SCOTT_W1;
                        inst.FLEX_SCOTT_W2 = dbResult.FLEX_SCOTT_W2;
                        inst.FLEX_SCOTT_W3 = dbResult.FLEX_SCOTT_W3;
                        inst.FLEX_SCOTT_F1 = dbResult.FLEX_SCOTT_F1;
                        inst.FLEX_SCOTT_F2 = dbResult.FLEX_SCOTT_F2;
                        inst.FLEX_SCOTT_F3 = dbResult.FLEX_SCOTT_F3;
                        inst.ITEMLOT = dbResult.ITEMLOT;

                        // Update 31/08/20
                        inst.BATCHNO = dbResult.BATCHNO;
                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.PARTNO = dbResult.PARTNO;

                        // Update 26/10/20
                        inst.FILENAME = dbResult.FILENAME;
                        inst.UPLOADDATE = dbResult.UPLOADDATE;
                        inst.UPLOADBY = dbResult.UPLOADBY;

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

        #region เพิ่มใหม่ LAB_GETLABDETAIL ใช้ในการ Load LAB_GETLABDETAIL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WEAVINGLOG"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_ENTRYDATE"></param>
        /// <returns></returns>
        public List<LAB_GETLABDETAIL> LAB_GETLABDETAIL(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, DateTime? P_ENTRYDATE)
        {
            List<LAB_GETLABDETAIL> results = null;

            if (!HasConnection())
                return results;

            LAB_GETLABDETAILParameter dbPara = new LAB_GETLABDETAILParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOG = P_WEAVINGLOG;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_ENTRYDATE = P_ENTRYDATE;

            List<LAB_GETLABDETAILResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETLABDETAIL(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETLABDETAIL>();
                    foreach (LAB_GETLABDETAILResult dbResult in dbResults)
                    {
                        LAB_GETLABDETAIL inst = new LAB_GETLABDETAIL();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ENTRYDATE = dbResult.ENTRYDATE;
                        inst.ENTEYBY = dbResult.ENTEYBY;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.USABLE_WIDTH1 = dbResult.USABLE_WIDTH1;
                        inst.USABLE_WIDTH2 = dbResult.USABLE_WIDTH2;
                        inst.USABLE_WIDTH3 = dbResult.USABLE_WIDTH3;
                        inst.WIDTH_SILICONE1 = dbResult.WIDTH_SILICONE1;
                        inst.WIDTH_SILICONE2 = dbResult.WIDTH_SILICONE2;
                        inst.WIDTH_SILICONE3 = dbResult.WIDTH_SILICONE3;
                        inst.NUMTHREADS_W1 = dbResult.NUMTHREADS_W1;
                        inst.NUMTHREADS_W2 = dbResult.NUMTHREADS_W2;
                        inst.NUMTHREADS_W3 = dbResult.NUMTHREADS_W3;
                        inst.NUMTHREADS_F1 = dbResult.NUMTHREADS_F1;
                        inst.NUMTHREADS_F2 = dbResult.NUMTHREADS_F2;
                        inst.NUMTHREADS_F3 = dbResult.NUMTHREADS_F3;
                        inst.TOTALWEIGHT1 = dbResult.TOTALWEIGHT1;
                        inst.TOTALWEIGHT2 = dbResult.TOTALWEIGHT2;
                        inst.TOTALWEIGHT3 = dbResult.TOTALWEIGHT3;
                        inst.TOTALWEIGHT4 = dbResult.TOTALWEIGHT4;
                        inst.TOTALWEIGHT5 = dbResult.TOTALWEIGHT5;
                        inst.TOTALWEIGHT6 = dbResult.TOTALWEIGHT6;
                        inst.UNCOATEDWEIGHT1 = dbResult.UNCOATEDWEIGHT1;
                        inst.UNCOATEDWEIGHT2 = dbResult.UNCOATEDWEIGHT2;
                        inst.UNCOATEDWEIGHT3 = dbResult.UNCOATEDWEIGHT3;
                        inst.UNCOATEDWEIGHT4 = dbResult.UNCOATEDWEIGHT4;
                        inst.UNCOATEDWEIGHT5 = dbResult.UNCOATEDWEIGHT5;
                        inst.UNCOATEDWEIGHT6 = dbResult.UNCOATEDWEIGHT6;
                        inst.COATINGWEIGHT1 = dbResult.COATINGWEIGHT1;
                        inst.COATINGWEIGHT2 = dbResult.COATINGWEIGHT2;
                        inst.COATINGWEIGHT3 = dbResult.COATINGWEIGHT3;
                        inst.COATINGWEIGHT4 = dbResult.COATINGWEIGHT4;
                        inst.COATINGWEIGHT5 = dbResult.COATINGWEIGHT5;
                        inst.COATINGWEIGHT6 = dbResult.COATINGWEIGHT6;
                        inst.THICKNESS1 = dbResult.THICKNESS1;
                        inst.THICKNESS2 = dbResult.THICKNESS2;
                        inst.THICKNESS3 = dbResult.THICKNESS3;
                        inst.MAXFORCE_W1 = dbResult.MAXFORCE_W1;
                        inst.MAXFORCE_W2 = dbResult.MAXFORCE_W2;
                        inst.MAXFORCE_W3 = dbResult.MAXFORCE_W3;
                        inst.MAXFORCE_F1 = dbResult.MAXFORCE_F1;
                        inst.MAXFORCE_F2 = dbResult.MAXFORCE_F2;
                        inst.MAXFORCE_F3 = dbResult.MAXFORCE_F3;
                        inst.ELONGATIONFORCE_W1 = dbResult.ELONGATIONFORCE_W1;
                        inst.ELONGATIONFORCE_W2 = dbResult.ELONGATIONFORCE_W2;
                        inst.ELONGATIONFORCE_W3 = dbResult.ELONGATIONFORCE_W3;
                        inst.ELONGATIONFORCE_F1 = dbResult.ELONGATIONFORCE_F1;
                        inst.ELONGATIONFORCE_F2 = dbResult.ELONGATIONFORCE_F2;
                        inst.ELONGATIONFORCE_F3 = dbResult.ELONGATIONFORCE_F3;

                        inst.FLAMMABILITY_W = dbResult.FLAMMABILITY_W;
                        inst.FLAMMABILITY_W2 = dbResult.FLAMMABILITY_W2;
                        inst.FLAMMABILITY_W3 = dbResult.FLAMMABILITY_W3;
                        inst.FLAMMABILITY_W4 = dbResult.FLAMMABILITY_W4;
                        inst.FLAMMABILITY_W5 = dbResult.FLAMMABILITY_W5;

                        inst.FLAMMABILITY_F = dbResult.FLAMMABILITY_F;
                        inst.FLAMMABILITY_F2 = dbResult.FLAMMABILITY_F2;
                        inst.FLAMMABILITY_F3 = dbResult.FLAMMABILITY_F3;
                        inst.FLAMMABILITY_F4 = dbResult.FLAMMABILITY_F4;
                        inst.FLAMMABILITY_F5 = dbResult.FLAMMABILITY_F5;

                        inst.EDGECOMB_W1 = dbResult.EDGECOMB_W1;
                        inst.EDGECOMB_W2 = dbResult.EDGECOMB_W2;
                        inst.EDGECOMB_W3 = dbResult.EDGECOMB_W3;
                        inst.EDGECOMB_F1 = dbResult.EDGECOMB_F1;
                        inst.EDGECOMB_F2 = dbResult.EDGECOMB_F2;
                        inst.EDGECOMB_F3 = dbResult.EDGECOMB_F3;
                        inst.STIFFNESS_W1 = dbResult.STIFFNESS_W1;
                        inst.STIFFNESS_W2 = dbResult.STIFFNESS_W2;
                        inst.STIFFNESS_W3 = dbResult.STIFFNESS_W3;
                        inst.STIFFNESS_F1 = dbResult.STIFFNESS_F1;
                        inst.STIFFNESS_F2 = dbResult.STIFFNESS_F2;
                        inst.STIFFNESS_F3 = dbResult.STIFFNESS_F3;
                        inst.TEAR_W1 = dbResult.TEAR_W1;
                        inst.TEAR_W2 = dbResult.TEAR_W2;
                        inst.TEAR_W3 = dbResult.TEAR_W3;
                        inst.TEAR_F1 = dbResult.TEAR_F1;
                        inst.TEAR_F2 = dbResult.TEAR_F2;
                        inst.TEAR_F3 = dbResult.TEAR_F3;
                        inst.STATIC_AIR1 = dbResult.STATIC_AIR1;
                        inst.STATIC_AIR2 = dbResult.STATIC_AIR2;
                        inst.STATIC_AIR3 = dbResult.STATIC_AIR3;

                        inst.STATIC_AIR4 = dbResult.STATIC_AIR4;
                        inst.STATIC_AIR5 = dbResult.STATIC_AIR5;
                        inst.STATIC_AIR6 = dbResult.STATIC_AIR6;

                        inst.DYNAMIC_AIR1 = dbResult.DYNAMIC_AIR1;
                        inst.DYNAMIC_AIR2 = dbResult.DYNAMIC_AIR2;
                        inst.DYNAMIC_AIR3 = dbResult.DYNAMIC_AIR3;
                        inst.EXPONENT1 = dbResult.EXPONENT1;
                        inst.EXPONENT2 = dbResult.EXPONENT2;
                        inst.EXPONENT3 = dbResult.EXPONENT3;
                        inst.DIMENSCHANGE_W1 = dbResult.DIMENSCHANGE_W1;
                        inst.DIMENSCHANGE_W2 = dbResult.DIMENSCHANGE_W2;
                        inst.DIMENSCHANGE_W3 = dbResult.DIMENSCHANGE_W3;
                        inst.DIMENSCHANGE_F1 = dbResult.DIMENSCHANGE_F1;
                        inst.DIMENSCHANGE_F2 = dbResult.DIMENSCHANGE_F2;
                        inst.DIMENSCHANGE_F3 = dbResult.DIMENSCHANGE_F3;
                        inst.FLEXABRASION_W1 = dbResult.FLEXABRASION_W1;
                        inst.FLEXABRASION_W2 = dbResult.FLEXABRASION_W2;
                        inst.FLEXABRASION_W3 = dbResult.FLEXABRASION_W3;
                        inst.FLEXABRASION_F1 = dbResult.FLEXABRASION_F1;
                        inst.FLEXABRASION_F2 = dbResult.FLEXABRASION_F2;
                        inst.FLEXABRASION_F3 = dbResult.FLEXABRASION_F3;

                        inst.STATUS = dbResult.STATUS;
                        inst.REMARK = dbResult.REMARK;
                        inst.APPROVEBY = dbResult.APPROVEBY;
                        inst.APPROVEDATE = dbResult.APPROVEDATE;
                        inst.CREATEDATE = dbResult.CREATEDATE;

                        // Update 06/07/18
                        inst.BOW1 = dbResult.BOW1;
                        inst.BOW2 = dbResult.BOW2;
                        inst.BOW3 = dbResult.BOW3;
                        inst.SKEW1 = dbResult.SKEW1;
                        inst.SKEW2 = dbResult.SKEW2;
                        inst.SKEW3 = dbResult.SKEW3;
                        inst.BENDING_W1 = dbResult.BENDING_W1;
                        inst.BENDING_W2 = dbResult.BENDING_W2;
                        inst.BENDING_W3 = dbResult.BENDING_W3;
                        inst.BENDING_F1 = dbResult.BENDING_F1;
                        inst.BENDING_F2 = dbResult.BENDING_F2;
                        inst.BENDING_F3 = dbResult.BENDING_F3;
                        inst.FLEX_SCOTT_W1 = dbResult.FLEX_SCOTT_W1;
                        inst.FLEX_SCOTT_W2 = dbResult.FLEX_SCOTT_W2;
                        inst.FLEX_SCOTT_W3 = dbResult.FLEX_SCOTT_W3;
                        inst.FLEX_SCOTT_F1 = dbResult.FLEX_SCOTT_F1;
                        inst.FLEX_SCOTT_F2 = dbResult.FLEX_SCOTT_F2;
                        inst.FLEX_SCOTT_F3 = dbResult.FLEX_SCOTT_F3;

                        inst.FILENAME = dbResult.FILENAME;
                        inst.UPLOADDATE = dbResult.UPLOADDATE;
                        inst.UPLOADBY = dbResult.UPLOADBY;

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

        #region LAB_APPROVELABDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WEAVINGLOG"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_ENTRYDATE"></param>
        /// <param name="P_STATUS"></param>
        /// <param name="P_REMARK"></param>
        /// <param name="P_APPROVEBY"></param>
        /// <param name="P_APPROVEDATE"></param>
        /// <returns></returns>
        public string LAB_APPROVELABDATA(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, DateTime? P_ENTRYDATE, string P_STATUS, string P_REMARK, string P_APPROVEBY, DateTime? P_APPROVEDATE)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_WEAVINGLOG) && string.IsNullOrWhiteSpace(P_FINISHINGLOT))
                return "ITMCODE & WEAVINGLOG & FINISHINGLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_APPROVELABDATAParameter dbPara = new LAB_APPROVELABDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOG = P_WEAVINGLOG;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_ENTRYDATE = P_ENTRYDATE;
            dbPara.P_STATUS = P_STATUS;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_APPROVEBY = P_APPROVEBY;
            dbPara.P_APPROVEDATE = P_APPROVEDATE;

            LAB_APPROVELABDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_APPROVELABDATA(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region เพิ่มใหม่ ITM_GETITEMCODELIST ใช้ในการ Load Item Code

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

                    ITM_GETITEMCODELIST inst = new ITM_GETITEMCODELIST();

                    inst.ITM_CODE = "All";
                    results.Add(inst);

                    foreach (ITM_GETITEMCODELISTResult dbResult in dbResults)
                    {
                        inst = new ITM_GETITEMCODELIST();

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

        #region เพิ่มใหม่ ITM_GETITEMCODELIST ใช้ในการ Load Item Code ไม่มี All

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ITM_GETITEMCODELIST> GETITEMCODELIST()
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

                    ITM_GETITEMCODELIST inst = new ITM_GETITEMCODELIST();

                    foreach (ITM_GETITEMCODELISTResult dbResult in dbResults)
                    {
                        inst = new ITM_GETITEMCODELIST();

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

        #region เพิ่มใหม่ LAB_FinishingProcess ใช้ในการ Load Process

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<LAB_FinishingProcess> LAB_FinishingProcess()
        {
            List<LAB_FinishingProcess> results = null;

            if (!HasConnection())
                return results;
            try
            {
                results = new List<LAB_FinishingProcess>();

                LAB_FinishingProcess inst = new LAB_FinishingProcess();
                inst.PROCESS = "All";
                results.Add(inst);

                inst = new LAB_FinishingProcess();
                inst.PROCESS = "Coating";
                results.Add(inst);

                inst = new LAB_FinishingProcess();
                inst.PROCESS = "Scouring";
                results.Add(inst);

                inst = new LAB_FinishingProcess();
                inst.PROCESS = "Dryer";
                results.Add(inst);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ LAB_Allowance ใช้ในการ Load Allowance

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<LAB_AllowanceProcess> LAB_AllowanceProcess()
        {
            List<LAB_AllowanceProcess> results = null;

            if (!HasConnection())
                return results;
            try
            {
                results = new List<LAB_AllowanceProcess>();

                LAB_AllowanceProcess inst = new LAB_AllowanceProcess();
                inst.Allowance = "Number";
                results.Add(inst);

                inst = new LAB_AllowanceProcess();
                inst.Allowance = "MIN.";
                results.Add(inst);

                inst = new LAB_AllowanceProcess();
                inst.Allowance = "MAX.";
                results.Add(inst);

                inst = new LAB_AllowanceProcess();
                inst.Allowance = "";
                results.Add(inst);

            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region ShowLabDataEntryWindow
        public LabDataEntryInfo ShowLabDataEntryWindow(string Operator, string positionLevel, string itemCode, string weavingLot, string finishingLot, DateTime? entryDate, string status)
        {
            LabDataEntryInfo result = null;
            LuckyTex.Windows.LabDataEntryWindow window = new LuckyTex.Windows.LabDataEntryWindow();
            window.Setup(Operator, positionLevel
                , itemCode, weavingLot, finishingLot, entryDate, status);

            if (window.ShowDialog() == true)
            {
                result = new LabDataEntryInfo();
                result.ChkApprove = window.ChkApprove;
            }
            return result;
        }
        #endregion

        // -- Update 01/07/18 -- //

        #region LAB_INSERTSAMPLEDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_ENTRYDATE"></param>
        /// <param name="P_ENTRYBY"></param>
        /// <param name="P_YARN"></param>
        /// <param name="P_METHOD"></param>
        /// <param name="P_NO"></param>
        /// <param name="P_VALUE"></param>
        /// <returns></returns>
        public string LAB_INSERTSAMPLEDATA(string P_ITMCODE, string P_PRODUCTIONLOT, string P_FINISHINGLOT, DateTime? P_ENTRYDATE, string P_ENTRYBY, string P_YARN, string P_METHOD, decimal? P_NO, decimal? P_VALUE)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_PRODUCTIONLOT) && string.IsNullOrWhiteSpace(P_FINISHINGLOT))
                return "ITMCODE & PRODUCTIONLOT & FINISHINGLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_INSERTSAMPLEDATAParameter dbPara = new LAB_INSERTSAMPLEDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_ENTRYDATE = P_ENTRYDATE;
            dbPara.P_ENTRYBY = P_ENTRYBY;

            if (!string.IsNullOrEmpty(P_YARN))
                dbPara.P_YARN = P_YARN;
            else
                dbPara.P_YARN = " ";

            dbPara.P_METHOD = P_METHOD;
            dbPara.P_NO = P_NO;
            dbPara.P_VALUE = P_VALUE;

            LAB_INSERTSAMPLEDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_INSERTSAMPLEDATA(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region เพิ่มใหม่ LAB_GETSAMPLEDATABYMETHOD ใช้ในการ Load LAB_GETSAMPLEDATABYMETHOD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_METHOD"></param>
        /// <returns></returns>
        public List<LAB_GETSAMPLEDATABYMETHOD> LAB_GETSAMPLEDATABYMETHOD(string P_ITMCODE, string P_PRODUCTIONLOT, string P_FINISHINGLOT, string P_METHOD)
        {
            List<LAB_GETSAMPLEDATABYMETHOD> results = null;

            if (!HasConnection())
                return results;

            LAB_GETSAMPLEDATABYMETHODParameter dbPara = new LAB_GETSAMPLEDATABYMETHODParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_METHOD = P_METHOD;

            List<LAB_GETSAMPLEDATABYMETHODResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETSAMPLEDATABYMETHOD(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETSAMPLEDATABYMETHOD>();
                    foreach (LAB_GETSAMPLEDATABYMETHODResult dbResult in dbResults)
                    {
                        LAB_GETSAMPLEDATABYMETHOD inst = new LAB_GETSAMPLEDATABYMETHOD();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.PRODUCTIONLOT = dbResult.PRODUCTIONLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ENTRYDATE = dbResult.ENTRYDATE;
                        inst.ENTRYBY = dbResult.ENTRYBY;
                        inst.METHOD = dbResult.METHOD;
                        inst.YARN = dbResult.YARN;
                        inst.NO = dbResult.NO;
                        inst.VALUE = dbResult.VALUE;
                        inst.CREATEDDATE = dbResult.CREATEDDATE;

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

        #region LAB_GETSAMPLEDATABYMETHOD_Warp
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_METHOD"></param>
        /// <returns></returns>
        public List<LAB_GETSAMPLEDATABYMETHOD> LAB_GETSAMPLEDATABYMETHOD_Warp(string P_ITMCODE, string P_PRODUCTIONLOT, string P_FINISHINGLOT, string P_METHOD)
        {
            List<LAB_GETSAMPLEDATABYMETHOD> results = null;

            if (!HasConnection())
                return results;

            LAB_GETSAMPLEDATABYMETHODParameter dbPara = new LAB_GETSAMPLEDATABYMETHODParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_METHOD = P_METHOD;

            List<LAB_GETSAMPLEDATABYMETHODResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETSAMPLEDATABYMETHOD(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETSAMPLEDATABYMETHOD>();
                    foreach (LAB_GETSAMPLEDATABYMETHODResult dbResult in dbResults)
                    {
                        if (!string.IsNullOrEmpty(dbResult.YARN))
                        {
                            if (dbResult.YARN == "Warp")
                            {
                                LAB_GETSAMPLEDATABYMETHOD inst = new LAB_GETSAMPLEDATABYMETHOD();

                                inst.ITM_CODE = dbResult.ITM_CODE;
                                inst.PRODUCTIONLOT = dbResult.PRODUCTIONLOT;
                                inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                                inst.ENTRYDATE = dbResult.ENTRYDATE;
                                inst.ENTRYBY = dbResult.ENTRYBY;
                                inst.METHOD = dbResult.METHOD;
                                inst.YARN = dbResult.YARN;
                                inst.NO = dbResult.NO;
                                inst.VALUE = dbResult.VALUE;
                                inst.CREATEDDATE = dbResult.CREATEDDATE;

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

        #region LAB_GETSAMPLEDATABYMETHOD_Weft
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_METHOD"></param>
        /// <returns></returns>
        public List<LAB_GETSAMPLEDATABYMETHOD> LAB_GETSAMPLEDATABYMETHOD_Weft(string P_ITMCODE, string P_PRODUCTIONLOT, string P_FINISHINGLOT, string P_METHOD)
        {
            List<LAB_GETSAMPLEDATABYMETHOD> results = null;

            if (!HasConnection())
                return results;

            LAB_GETSAMPLEDATABYMETHODParameter dbPara = new LAB_GETSAMPLEDATABYMETHODParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_METHOD = P_METHOD;

            List<LAB_GETSAMPLEDATABYMETHODResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETSAMPLEDATABYMETHOD(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETSAMPLEDATABYMETHOD>();
                    foreach (LAB_GETSAMPLEDATABYMETHODResult dbResult in dbResults)
                    {
                        if (!string.IsNullOrEmpty(dbResult.YARN))
                        {
                            if (dbResult.YARN == "Weft")
                            {
                                LAB_GETSAMPLEDATABYMETHOD inst = new LAB_GETSAMPLEDATABYMETHOD();

                                inst.ITM_CODE = dbResult.ITM_CODE;
                                inst.PRODUCTIONLOT = dbResult.PRODUCTIONLOT;
                                inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                                inst.ENTRYDATE = dbResult.ENTRYDATE;
                                inst.ENTRYBY = dbResult.ENTRYBY;
                                inst.METHOD = dbResult.METHOD;
                                inst.YARN = dbResult.YARN;
                                inst.NO = dbResult.NO;
                                inst.VALUE = dbResult.VALUE;
                                inst.CREATEDDATE = dbResult.CREATEDDATE;

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

        #region LAB_GETNOSAMPLEBYMETHOD
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <returns></returns>
        public List<LAB_GETNOSAMPLEBYMETHOD> LAB_GETNOSAMPLEBYMETHOD(string P_ITMCODE, string P_PRODUCTIONLOT, string P_FINISHINGLOT)
        {
            List<LAB_GETNOSAMPLEBYMETHOD> results = null;

            if (!HasConnection())
                return results;

            LAB_GETNOSAMPLEBYMETHODParameter dbPara = new LAB_GETNOSAMPLEBYMETHODParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;

            List<LAB_GETNOSAMPLEBYMETHODResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETNOSAMPLEBYMETHOD(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETNOSAMPLEBYMETHOD>();
                    foreach (LAB_GETNOSAMPLEBYMETHODResult dbResult in dbResults)
                    {
                        LAB_GETNOSAMPLEBYMETHOD inst = new LAB_GETNOSAMPLEBYMETHOD();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.PRODUCTIONLOT = dbResult.PRODUCTIONLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.METHOD = dbResult.METHOD;
                        inst.ENTRYDATE = dbResult.ENTRYDATE;
                        inst.N = dbResult.N;

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

        // -- Update 20/09/18 -- //

        #region LAB_SEARCHLABSAMPLEDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_ENTRYSTARTDATE"></param>
        /// <param name="P_ENTRYENDDATE"></param>
        /// <returns></returns>
        public List<LAB_SEARCHLABSAMPLEDATA> LAB_SEARCHLABSAMPLEDATA(string P_ITMCODE, string P_ENTRYSTARTDATE, string P_ENTRYENDDATE)
        {
            List<LAB_SEARCHLABSAMPLEDATA> results = null;

            if (!HasConnection())
                return results;

            LAB_SEARCHLABSAMPLEDATAParameter dbPara = new LAB_SEARCHLABSAMPLEDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_ENTRYSTARTDATE = P_ENTRYSTARTDATE;
            dbPara.P_ENTRYENDDATE = P_ENTRYENDDATE;

            List<LAB_SEARCHLABSAMPLEDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_SEARCHLABSAMPLEDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_SEARCHLABSAMPLEDATA>();
                    foreach (LAB_SEARCHLABSAMPLEDATAResult dbResult in dbResults)
                    {
                        LAB_SEARCHLABSAMPLEDATA inst = new LAB_SEARCHLABSAMPLEDATA();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.PRODUCTIONLOT = dbResult.PRODUCTIONLOT;
                        inst.ENTRYDATE = dbResult.ENTRYDATE;

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

        #region LAB_SAVEPLCDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_TOTALWEIGHT1"></param>
        /// <param name="P_TOTALWEIGHT2"></param>
        /// <param name="P_TOTALWEIGHT3"></param>
        /// <param name="P_TOTALWEIGHT4"></param>
        /// <param name="P_TOTALWEIGHT5"></param>
        /// <param name="P_TOTALWEIGHT6"></param>
        /// <param name="P_UNCOATEDWEIGHT1"></param>
        /// <param name="P_UNCOATEDWEIGHT2"></param>
        /// <param name="P_UNCOATEDWEIGHT3"></param>
        /// <param name="P_UNCOATEDWEIGHT4"></param>
        /// <param name="P_UNCOATEDWEIGHT5"></param>
        /// <param name="P_UNCOATEDWEIGHT6"></param>
        /// <param name="P_COATWEIGHT1"></param>
        /// <param name="P_COATWEIGHT2"></param>
        /// <param name="P_COATWEIGHT3"></param>
        /// <param name="P_COATWEIGHT4"></param>
        /// <param name="P_COATWEIGHT5"></param>
        /// <param name="P_COATWEIGHT6"></param>
        /// <param name="P_STIFFNESS_W1"></param>
        /// <param name="P_STIFFNESS_W2"></param>
        /// <param name="P_STIFFNESS_W3"></param>
        /// <param name="P_STIFFNESS_F1"></param>
        /// <param name="P_STIFFNESS_F2"></param>
        /// <param name="P_STIFFNESS_F3"></param>
        /// <param name="P_STATIC_AIR1"></param>
        /// <param name="P_STATIC_AIR2"></param>
        /// <param name="P_STATIC_AIR3"></param>
        /// <param name="P_STATIC_AIR4"></param>
        /// <param name="P_STATIC_AIR5"></param>
        /// <param name="P_STATIC_AIR6"></param>
        /// <param name="P_WEIGHTDATE"></param>
        /// <param name="P_WEIGHTBY"></param>
        /// <param name="P_STIFFNESSDATE"></param>
        /// <param name="P_STIFFNESSBY"></param>
        /// <param name="P_STATICAIRDATE"></param>
        /// <param name="P_STATICAIRBY"></param>
        /// <returns></returns>
        public string LAB_SAVEPLCDATA(string P_ITMCODE, string P_PRODUCTIONLOT,
        decimal? P_TOTALWEIGHT1, decimal? P_TOTALWEIGHT2, decimal? P_TOTALWEIGHT3, decimal? P_TOTALWEIGHT4, decimal? P_TOTALWEIGHT5, decimal? P_TOTALWEIGHT6,
        decimal? P_UNCOATEDWEIGHT1, decimal? P_UNCOATEDWEIGHT2, decimal? P_UNCOATEDWEIGHT3, decimal? P_UNCOATEDWEIGHT4, decimal? P_UNCOATEDWEIGHT5, decimal? P_UNCOATEDWEIGHT6,
        decimal? P_COATWEIGHT1, decimal? P_COATWEIGHT2, decimal? P_COATWEIGHT3, decimal? P_COATWEIGHT4, decimal? P_COATWEIGHT5, decimal? P_COATWEIGHT6,
        decimal? P_STIFFNESS_W1, decimal? P_STIFFNESS_W2, decimal? P_STIFFNESS_W3, decimal? P_STIFFNESS_F1, decimal? P_STIFFNESS_F2, decimal? P_STIFFNESS_F3,
        decimal? P_STATIC_AIR1, decimal? P_STATIC_AIR2, decimal? P_STATIC_AIR3, decimal? P_STATIC_AIR4, decimal? P_STATIC_AIR5, decimal? P_STATIC_AIR6,
        decimal? P_DYNAMIC_AIR1, decimal? P_DYNAMIC_AIR2, decimal? P_DYNAMIC_AIR3, decimal? P_EXPONENT1, decimal? P_EXPONENT2, decimal? P_EXPONENT3,
        DateTime? P_WEIGHTDATE, string P_WEIGHTBY, DateTime? P_STIFFNESSDATE, string P_STIFFNESSBY, DateTime? P_STATICAIRDATE, string P_STATICAIRBY,
        DateTime? P_DYNAMICDATE, string P_DYNAMICBY)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_PRODUCTIONLOT))
                return "ITMCODE & PRODUCTIONLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_SAVEPLCDATAParameter dbPara = new LAB_SAVEPLCDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;

            dbPara.P_TOTALWEIGHT1 = P_TOTALWEIGHT1;
            dbPara.P_TOTALWEIGHT2 = P_TOTALWEIGHT2;
            dbPara.P_TOTALWEIGHT3 = P_TOTALWEIGHT3;
            dbPara.P_TOTALWEIGHT4 = P_TOTALWEIGHT4;
            dbPara.P_TOTALWEIGHT5 = P_TOTALWEIGHT5;
            dbPara.P_TOTALWEIGHT6 = P_TOTALWEIGHT6;
            dbPara.P_UNCOATEDWEIGHT1 = P_UNCOATEDWEIGHT1;
            dbPara.P_UNCOATEDWEIGHT2 = P_UNCOATEDWEIGHT2;
            dbPara.P_UNCOATEDWEIGHT3 = P_UNCOATEDWEIGHT3;
            dbPara.P_UNCOATEDWEIGHT4 = P_UNCOATEDWEIGHT4;
            dbPara.P_UNCOATEDWEIGHT5 = P_UNCOATEDWEIGHT5;
            dbPara.P_UNCOATEDWEIGHT6 = P_UNCOATEDWEIGHT6;
            dbPara.P_COATWEIGHT1 = P_COATWEIGHT1;
            dbPara.P_COATWEIGHT2 = P_COATWEIGHT2;
            dbPara.P_COATWEIGHT3 = P_COATWEIGHT3;
            dbPara.P_COATWEIGHT4 = P_COATWEIGHT4;
            dbPara.P_COATWEIGHT5 = P_COATWEIGHT5;
            dbPara.P_COATWEIGHT6 = P_COATWEIGHT6;
            dbPara.P_STIFFNESS_W1 = P_STIFFNESS_W1;
            dbPara.P_STIFFNESS_W2 = P_STIFFNESS_W2;
            dbPara.P_STIFFNESS_W3 = P_STIFFNESS_W3;
            dbPara.P_STIFFNESS_F1 = P_STIFFNESS_F1;
            dbPara.P_STIFFNESS_F2 = P_STIFFNESS_F2;
            dbPara.P_STIFFNESS_F3 = P_STIFFNESS_F3;
            dbPara.P_STATIC_AIR1 = P_STATIC_AIR1;
            dbPara.P_STATIC_AIR2 = P_STATIC_AIR2;
            dbPara.P_STATIC_AIR3 = P_STATIC_AIR3;
            dbPara.P_STATIC_AIR4 = P_STATIC_AIR4;
            dbPara.P_STATIC_AIR5 = P_STATIC_AIR5;
            dbPara.P_STATIC_AIR6 = P_STATIC_AIR6;

            //เพิ่ม 25/11/18
            dbPara.P_DYNAMIC_AIR1 = P_DYNAMIC_AIR1;
            dbPara.P_DYNAMIC_AIR2 = P_DYNAMIC_AIR2;
            dbPara.P_DYNAMIC_AIR3 = P_DYNAMIC_AIR3;
            dbPara.P_EXPONENT1 = P_EXPONENT1;
            dbPara.P_EXPONENT2 = P_EXPONENT2;
            dbPara.P_EXPONENT3 = P_EXPONENT3;

            dbPara.P_WEIGHTDATE = P_WEIGHTDATE;
            dbPara.P_WEIGHTBY = P_WEIGHTBY;
            dbPara.P_STIFFNESSDATE = P_STIFFNESSDATE;
            dbPara.P_STIFFNESSBY = P_STIFFNESSBY;
            dbPara.P_STATICAIRDATE = P_STATICAIRDATE;
            dbPara.P_STATICAIRBY = P_STATICAIRBY;

            //เพิ่ม 25/11/18
            dbPara.P_DYNAMICDATE = P_DYNAMICDATE;
            dbPara.P_DYNAMICBY = P_DYNAMICBY;

            LAB_SAVEPLCDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_SAVEPLCDATA(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region LAB_GETPLCDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <returns></returns>
        public List<LAB_GETPLCDATA> LAB_GETPLCDATA(string P_ITMCODE, string P_PRODUCTIONLOT)
        {
            List<LAB_GETPLCDATA> results = null;

            if (!HasConnection())
                return results;

            LAB_GETPLCDATAParameter dbPara = new LAB_GETPLCDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;

            List<LAB_GETPLCDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETPLCDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETPLCDATA>();
                    foreach (LAB_GETPLCDATAResult dbResult in dbResults)
                    {
                        LAB_GETPLCDATA inst = new LAB_GETPLCDATA();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;

                        inst.TOTALWEIGHT1 = dbResult.TOTALWEIGHT1;
                        inst.TOTALWEIGHT2 = dbResult.TOTALWEIGHT2;
                        inst.TOTALWEIGHT3 = dbResult.TOTALWEIGHT3;
                        inst.TOTALWEIGHT4 = dbResult.TOTALWEIGHT4;
                        inst.TOTALWEIGHT5 = dbResult.TOTALWEIGHT5;
                        inst.TOTALWEIGHT6 = dbResult.TOTALWEIGHT6;
                        inst.UNCOATEDWEIGHT1 = dbResult.UNCOATEDWEIGHT1;
                        inst.UNCOATEDWEIGHT2 = dbResult.UNCOATEDWEIGHT2;
                        inst.UNCOATEDWEIGHT3 = dbResult.UNCOATEDWEIGHT3;
                        inst.UNCOATEDWEIGHT4 = dbResult.UNCOATEDWEIGHT4;
                        inst.UNCOATEDWEIGHT5 = dbResult.UNCOATEDWEIGHT5;
                        inst.UNCOATEDWEIGHT6 = dbResult.UNCOATEDWEIGHT6;
                        inst.COATINGWEIGHT1 = dbResult.COATINGWEIGHT1;
                        inst.COATINGWEIGHT2 = dbResult.COATINGWEIGHT2;
                        inst.COATINGWEIGHT3 = dbResult.COATINGWEIGHT3;
                        inst.COATINGWEIGHT4 = dbResult.COATINGWEIGHT4;
                        inst.COATINGWEIGHT5 = dbResult.COATINGWEIGHT5;
                        inst.COATINGWEIGHT6 = dbResult.COATINGWEIGHT6;
                        inst.STIFFNESS_W1 = dbResult.STIFFNESS_W1;
                        inst.STIFFNESS_W2 = dbResult.STIFFNESS_W2;
                        inst.STIFFNESS_W3 = dbResult.STIFFNESS_W3;
                        inst.STIFFNESS_F1 = dbResult.STIFFNESS_F1;
                        inst.STIFFNESS_F2 = dbResult.STIFFNESS_F2;
                        inst.STIFFNESS_F3 = dbResult.STIFFNESS_F3;
                        inst.STATIC_AIR1 = dbResult.STATIC_AIR1;
                        inst.STATIC_AIR2 = dbResult.STATIC_AIR2;
                        inst.STATIC_AIR3 = dbResult.STATIC_AIR3;
                        inst.STATIC_AIR4 = dbResult.STATIC_AIR4;
                        inst.STATIC_AIR5 = dbResult.STATIC_AIR5;
                        inst.STATIC_AIR6 = dbResult.STATIC_AIR6;
                        inst.WEIGHTDATE = dbResult.WEIGHTDATE;
                        inst.WEIGHT_BY = dbResult.WEIGHT_BY;
                        inst.STIFFNESSDATE = dbResult.STIFFNESSDATE;
                        inst.STIFFNESS_BY = dbResult.STIFFNESS_BY;
                        inst.STATICAIRDATE = dbResult.STATICAIRDATE;
                        inst.STATICAIR_BY = dbResult.STATICAIR_BY;

                        //เพิ่ม 25/11/18
                        inst.EXPONENT1 = dbResult.EXPONENT1;
                        inst.EXPONENT2 = dbResult.EXPONENT2;
                        inst.EXPONENT3 = dbResult.EXPONENT3;
                        inst.DYNAMIC_AIR1 = dbResult.DYNAMIC_AIR1;
                        inst.DYNAMIC_AIR2 = dbResult.DYNAMIC_AIR2;
                        inst.DYNAMIC_AIR3 = dbResult.DYNAMIC_AIR3;
                        inst.DYNAMICDATE = dbResult.DYNAMICDATE;
                        inst.DYNAMIC_BY = dbResult.DYNAMIC_BY;

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

        //----------------------//

        #region ShowEntrySampleTestData1Window
        public EntrySampleTestDataInfo ShowEntrySampleTestData1Window(string Operator, string itemCode, string weavingLot, string finishingLot, DateTime? entryDate, string method,int? no)
        {
            EntrySampleTestDataInfo result = null;
            LuckyTex.Windows.EntrySampleTestData1Window window = new LuckyTex.Windows.EntrySampleTestData1Window();
            window.Setup(Operator, itemCode, weavingLot, finishingLot, entryDate, method, no);

            if (window.ShowDialog() == true)
            {
                result = new EntrySampleTestDataInfo();
                result.ChkSave = window.ChkSave;
            }
            return result;
        }
        #endregion

        #region ShowEntrySampleTestData2Window
        public EntrySampleTestDataInfo ShowEntrySampleTestData2Window(string Operator, string itemCode, string weavingLot, string finishingLot, DateTime? entryDate, string method, int? no)
        {
            EntrySampleTestDataInfo result = null;
            LuckyTex.Windows.EntrySampleTestData2Window window = new LuckyTex.Windows.EntrySampleTestData2Window();
            window.Setup(Operator, itemCode, weavingLot, finishingLot, entryDate, method, no);

            if (window.ShowDialog() == true)
            {
                result = new EntrySampleTestDataInfo();
                result.ChkSave = window.ChkSave;
            }
            return result;
        }
        #endregion

        //----------------------//

        // -- Update 31/08/20 -- //

        #region LAB_GETREPORTINFO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_CUSTOMERID"></param>
        /// <returns></returns>
        public List<LAB_GETREPORTINFO> LAB_GETREPORTINFO(string P_ITMCODE, string P_CUSTOMERID)
        {
            List<LAB_GETREPORTINFO> results = null;

            if (!HasConnection())
                return results;

            LAB_GETREPORTINFOParameter dbPara = new LAB_GETREPORTINFOParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_CUSTOMERID = P_CUSTOMERID;

            List<LAB_GETREPORTINFOResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETREPORTINFO(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETREPORTINFO>();
                    foreach (LAB_GETREPORTINFOResult dbResult in dbResults)
                    {
                        LAB_GETREPORTINFO inst = new LAB_GETREPORTINFO();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.REPORT_ID = dbResult.REPORT_ID;
                        inst.REVESION = dbResult.REVESION;
                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.YARNTYPE = dbResult.YARNTYPE;
                        inst.WEIGHT = dbResult.WEIGHT;
                        inst.COATWEIGHT = dbResult.COATWEIGHT;
                        inst.NUMTHREADS = dbResult.NUMTHREADS;
                        inst.USABLE_WIDTH = dbResult.USABLE_WIDTH;
                        inst.THICKNESS = dbResult.THICKNESS;
                        inst.MAXFORCE = dbResult.MAXFORCE;
                        inst.ELONGATIONFORCE = dbResult.ELONGATIONFORCE;
                        inst.FLAMMABILITY = dbResult.FLAMMABILITY;
                        inst.EDGECOMB = dbResult.EDGECOMB;
                        inst.STIFFNESS = dbResult.STIFFNESS;
                        inst.TEAR = dbResult.TEAR;
                        inst.STATIC_AIR = dbResult.STATIC_AIR;
                        inst.DYNAMIC_AIR = dbResult.DYNAMIC_AIR;
                        inst.EXPONENT = dbResult.EXPONENT;
                        inst.DIMENSCHANGE = dbResult.DIMENSCHANGE;
                        inst.FLEXABRASION = dbResult.FLEXABRASION;
                        inst.EFFECTIVE_DATE = dbResult.EFFECTIVE_DATE;

                        inst.BOW = dbResult.BOW;
                        inst.SKEW = dbResult.SKEW;
                        inst.FLEX_SCOTT = dbResult.FLEX_SCOTT;
                        inst.BENDING = dbResult.BENDING;

                        inst.REPORT_NAME = dbResult.REPORT_NAME;

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

        //----------------------//

        // 01/10/20

        #region LAB_GETREPORTIDLIST
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<LAB_GETREPORTIDLIST> LAB_GETREPORTIDLIST()
        {
            List<LAB_GETREPORTIDLIST> results = null;

            if (!HasConnection())
                return results;

            LAB_GETREPORTIDLISTParameter dbPara = new LAB_GETREPORTIDLISTParameter();

            List<LAB_GETREPORTIDLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETREPORTIDLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETREPORTIDLIST>();
                    foreach (LAB_GETREPORTIDLISTResult dbResult in dbResults)
                    {
                        LAB_GETREPORTIDLIST inst = new LAB_GETREPORTIDLIST();

                        inst.REPORT_ID = dbResult.REPORT_ID;
                        inst.REPORT_NAME = dbResult.REPORT_NAME;
                        inst.EFFECTIVE_DATE = dbResult.EFFECTIVE_DATE;
                        inst.USE_FLAG = dbResult.USE_FLAG;

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

        #region LAB_INSERTUPDATEREPORTINFO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_CUSTOMERID"></param>
        /// <param name="P_REPORTID"></param>
        /// <param name="P_REVERSION"></param>
        /// <param name="P_YARNTYPE"></param>
        /// <param name="P_WEIGHT"></param>
        /// <param name="P_COATWEIGHT"></param>
        /// <param name="P_NUMTHREADS"></param>
        /// <param name="P_USEWIDTH"></param>
        /// <param name="P_THICKNESS"></param>
        /// <param name="P_MAXFORCE"></param>
        /// <param name="P_ELOGATION"></param>
        /// <param name="P_FLAMMABILITY"></param>
        /// <param name="P_EDGECOMB"></param>
        /// <param name="P_STIFFNESS"></param>
        /// <param name="P_TEAR"></param>
        /// <param name="P_STATIC_AIR"></param>
        /// <param name="P_DYNAMIC_AIR"></param>
        /// <param name="P_EXPONENT"></param>
        /// <param name="P_DIMENSCHANGE"></param>
        /// <param name="P_FLEXABRASION"></param>
        /// <returns></returns>
        public string LAB_INSERTUPDATEREPORTINFO(string P_ITMCODE, string P_CUSTOMERID, string P_REPORTID, string P_REVERSION, string P_YARNTYPE, string P_WEIGHT, string P_COATWEIGHT,
                    string P_NUMTHREADS, string P_USEWIDTH, string P_THICKNESS, string P_MAXFORCE, string P_ELOGATION, string P_FLAMMABILITY, string P_EDGECOMB, string P_STIFFNESS, string P_TEAR,
                    string P_STATIC_AIR, string P_DYNAMIC_AIR, string P_EXPONENT, string P_DIMENSCHANGE, string P_FLEXABRASION,
                    string P_BOW, string P_SKEW, string P_FLEX_SCOTT, string P_BENDING)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_REPORTID))
                return result;

            if (!HasConnection())
                return result;

            LAB_INSERTUPDATEREPORTINFOParameter dbPara = new LAB_INSERTUPDATEREPORTINFOParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_CUSTOMERID = P_CUSTOMERID;
            dbPara.P_REPORTID = P_REPORTID;
            dbPara.P_REVERSION = P_REVERSION;
            dbPara.P_YARNTYPE = P_YARNTYPE;
            dbPara.P_WEIGHT = P_WEIGHT;
            dbPara.P_COATWEIGHT = P_COATWEIGHT;
            dbPara.P_NUMTHREADS = P_NUMTHREADS;
            dbPara.P_USEWIDTH = P_USEWIDTH;
            dbPara.P_THICKNESS = P_THICKNESS;
            dbPara.P_MAXFORCE = P_MAXFORCE;
            dbPara.P_ELOGATION = P_ELOGATION;
            dbPara.P_FLAMMABILITY = P_FLAMMABILITY;
            dbPara.P_EDGECOMB = P_EDGECOMB;
            dbPara.P_STIFFNESS = P_STIFFNESS;
            dbPara.P_TEAR = P_TEAR;
            dbPara.P_STATIC_AIR = P_STATIC_AIR;
            dbPara.P_DYNAMIC_AIR = P_DYNAMIC_AIR;
            dbPara.P_EXPONENT = P_EXPONENT;
            dbPara.P_DIMENSCHANGE = P_DIMENSCHANGE;
            dbPara.P_FLEXABRASION = P_FLEXABRASION;

            dbPara.P_BOW = P_BOW;
            dbPara.P_SKEW = P_SKEW;
            dbPara.P_FLEX_SCOTT = P_FLEX_SCOTT;

            dbPara.P_BENDING = P_BENDING;

            LAB_INSERTUPDATEREPORTINFOResult dbResult = null;

            try
            {
                dbResult = DatabaseManager.Instance.LAB_INSERTUPDATEREPORTINFO(dbPara);

                if (dbResult != null)
                {
                    if (!string.IsNullOrEmpty(dbResult.P_RETURN))
                        result = (dbResult.P_RETURN);
                    else
                        result = "0";
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

        // -- Update 26/10/20 -- //

        #region เพิ่มใหม่ LAB_SEARCHAPPROVELAB 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_ENTRYSTARTDATE"></param>
        /// <param name="P_ENTRYENDDATE"></param>
        /// <param name="P_LOOM"></param>
        /// <param name="P_FINISHPROCESS"></param>
        /// <returns></returns>
        public List<LAB_SEARCHAPPROVELAB> LAB_SEARCHAPPROVELAB(string P_ITMCODE, string P_ENTRYSTARTDATE, string P_ENTRYENDDATE, string P_LOOM, string P_FINISHPROCESS)
        {
            List<LAB_SEARCHAPPROVELAB> results = null;

            if (!HasConnection())
                return results;

            LAB_SEARCHAPPROVELABParameter dbPara = new LAB_SEARCHAPPROVELABParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_ENTRYSTARTDATE = P_ENTRYSTARTDATE;
            dbPara.P_ENTRYENDDATE = P_ENTRYENDDATE;
            dbPara.P_LOOM = P_LOOM;
            dbPara.P_FINISHPROCESS = P_FINISHPROCESS;

            List<LAB_SEARCHAPPROVELABResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_SEARCHAPPROVELAB(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_SEARCHAPPROVELAB>();
                    foreach (LAB_SEARCHAPPROVELABResult dbResult in dbResults)
                    {
                        LAB_SEARCHAPPROVELAB inst = new LAB_SEARCHAPPROVELAB();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ENTRYDATE = dbResult.ENTRYDATE;
                        inst.ENTEYBY = dbResult.ENTEYBY;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.USABLE_WIDTH1 = dbResult.USABLE_WIDTH1;
                        inst.USABLE_WIDTH2 = dbResult.USABLE_WIDTH2;
                        inst.USABLE_WIDTH3 = dbResult.USABLE_WIDTH3;
                        inst.WIDTH_SILICONE1 = dbResult.WIDTH_SILICONE1;
                        inst.WIDTH_SILICONE2 = dbResult.WIDTH_SILICONE2;
                        inst.WIDTH_SILICONE3 = dbResult.WIDTH_SILICONE3;
                        inst.NUMTHREADS_W1 = dbResult.NUMTHREADS_W1;
                        inst.NUMTHREADS_W2 = dbResult.NUMTHREADS_W2;
                        inst.NUMTHREADS_W3 = dbResult.NUMTHREADS_W3;
                        inst.NUMTHREADS_F1 = dbResult.NUMTHREADS_F1;
                        inst.NUMTHREADS_F2 = dbResult.NUMTHREADS_F2;
                        inst.NUMTHREADS_F3 = dbResult.NUMTHREADS_F3;
                        inst.TOTALWEIGHT1 = dbResult.TOTALWEIGHT1;
                        inst.TOTALWEIGHT2 = dbResult.TOTALWEIGHT2;
                        inst.TOTALWEIGHT3 = dbResult.TOTALWEIGHT3;
                        inst.TOTALWEIGHT4 = dbResult.TOTALWEIGHT4;
                        inst.TOTALWEIGHT5 = dbResult.TOTALWEIGHT5;
                        inst.TOTALWEIGHT6 = dbResult.TOTALWEIGHT6;
                        inst.UNCOATEDWEIGHT1 = dbResult.UNCOATEDWEIGHT1;
                        inst.UNCOATEDWEIGHT2 = dbResult.UNCOATEDWEIGHT2;
                        inst.UNCOATEDWEIGHT3 = dbResult.UNCOATEDWEIGHT3;
                        inst.UNCOATEDWEIGHT4 = dbResult.UNCOATEDWEIGHT4;
                        inst.UNCOATEDWEIGHT5 = dbResult.UNCOATEDWEIGHT5;
                        inst.UNCOATEDWEIGHT6 = dbResult.UNCOATEDWEIGHT6;
                        inst.COATINGWEIGHT1 = dbResult.COATINGWEIGHT1;
                        inst.COATINGWEIGHT2 = dbResult.COATINGWEIGHT2;
                        inst.COATINGWEIGHT3 = dbResult.COATINGWEIGHT3;
                        inst.COATINGWEIGHT4 = dbResult.COATINGWEIGHT4;
                        inst.COATINGWEIGHT5 = dbResult.COATINGWEIGHT5;
                        inst.COATINGWEIGHT6 = dbResult.COATINGWEIGHT6;
                        inst.THICKNESS1 = dbResult.THICKNESS1;
                        inst.THICKNESS2 = dbResult.THICKNESS2;
                        inst.THICKNESS3 = dbResult.THICKNESS3;
                        inst.MAXFORCE_W1 = dbResult.MAXFORCE_W1;
                        inst.MAXFORCE_W2 = dbResult.MAXFORCE_W2;
                        inst.MAXFORCE_W3 = dbResult.MAXFORCE_W3;
                        inst.MAXFORCE_F1 = dbResult.MAXFORCE_F1;
                        inst.MAXFORCE_F2 = dbResult.MAXFORCE_F2;
                        inst.MAXFORCE_F3 = dbResult.MAXFORCE_F3;
                        inst.ELONGATIONFORCE_W1 = dbResult.ELONGATIONFORCE_W1;
                        inst.ELONGATIONFORCE_W2 = dbResult.ELONGATIONFORCE_W2;
                        inst.ELONGATIONFORCE_W3 = dbResult.ELONGATIONFORCE_W3;
                        inst.ELONGATIONFORCE_F1 = dbResult.ELONGATIONFORCE_F1;
                        inst.ELONGATIONFORCE_F2 = dbResult.ELONGATIONFORCE_F2;
                        inst.ELONGATIONFORCE_F3 = dbResult.ELONGATIONFORCE_F3;

                        inst.FLAMMABILITY_W = dbResult.FLAMMABILITY_W;
                        inst.FLAMMABILITY_W2 = dbResult.FLAMMABILITY_W2;
                        inst.FLAMMABILITY_W3 = dbResult.FLAMMABILITY_W3;
                        inst.FLAMMABILITY_W4 = dbResult.FLAMMABILITY_W4;
                        inst.FLAMMABILITY_W5 = dbResult.FLAMMABILITY_W5;

                        inst.FLAMMABILITY_F = dbResult.FLAMMABILITY_F;
                        inst.FLAMMABILITY_F2 = dbResult.FLAMMABILITY_F2;
                        inst.FLAMMABILITY_F3 = dbResult.FLAMMABILITY_F3;
                        inst.FLAMMABILITY_F4 = dbResult.FLAMMABILITY_F4;
                        inst.FLAMMABILITY_F5 = dbResult.FLAMMABILITY_F5;

                        inst.EDGECOMB_W1 = dbResult.EDGECOMB_W1;
                        inst.EDGECOMB_W2 = dbResult.EDGECOMB_W2;
                        inst.EDGECOMB_W3 = dbResult.EDGECOMB_W3;
                        inst.EDGECOMB_F1 = dbResult.EDGECOMB_F1;
                        inst.EDGECOMB_F2 = dbResult.EDGECOMB_F2;
                        inst.EDGECOMB_F3 = dbResult.EDGECOMB_F3;
                        inst.STIFFNESS_W1 = dbResult.STIFFNESS_W1;
                        inst.STIFFNESS_W2 = dbResult.STIFFNESS_W2;
                        inst.STIFFNESS_W3 = dbResult.STIFFNESS_W3;
                        inst.STIFFNESS_F1 = dbResult.STIFFNESS_F1;
                        inst.STIFFNESS_F2 = dbResult.STIFFNESS_F2;
                        inst.STIFFNESS_F3 = dbResult.STIFFNESS_F3;
                        inst.TEAR_W1 = dbResult.TEAR_W1;
                        inst.TEAR_W2 = dbResult.TEAR_W2;
                        inst.TEAR_W3 = dbResult.TEAR_W3;
                        inst.TEAR_F1 = dbResult.TEAR_F1;
                        inst.TEAR_F2 = dbResult.TEAR_F2;
                        inst.TEAR_F3 = dbResult.TEAR_F3;
                        inst.STATIC_AIR1 = dbResult.STATIC_AIR1;
                        inst.STATIC_AIR2 = dbResult.STATIC_AIR2;
                        inst.STATIC_AIR3 = dbResult.STATIC_AIR3;
                        inst.STATIC_AIR4 = dbResult.STATIC_AIR4;
                        inst.STATIC_AIR5 = dbResult.STATIC_AIR5;
                        inst.STATIC_AIR6 = dbResult.STATIC_AIR6;
                        inst.DYNAMIC_AIR1 = dbResult.DYNAMIC_AIR1;
                        inst.DYNAMIC_AIR2 = dbResult.DYNAMIC_AIR2;
                        inst.DYNAMIC_AIR3 = dbResult.DYNAMIC_AIR3;
                        inst.EXPONENT1 = dbResult.EXPONENT1;
                        inst.EXPONENT2 = dbResult.EXPONENT2;
                        inst.EXPONENT3 = dbResult.EXPONENT3;
                        inst.DIMENSCHANGE_W1 = dbResult.DIMENSCHANGE_W1;
                        inst.DIMENSCHANGE_W2 = dbResult.DIMENSCHANGE_W2;
                        inst.DIMENSCHANGE_W3 = dbResult.DIMENSCHANGE_W3;
                        inst.DIMENSCHANGE_F1 = dbResult.DIMENSCHANGE_F1;
                        inst.DIMENSCHANGE_F2 = dbResult.DIMENSCHANGE_F2;
                        inst.DIMENSCHANGE_F3 = dbResult.DIMENSCHANGE_F3;
                        inst.FLEXABRASION_W1 = dbResult.FLEXABRASION_W1;
                        inst.FLEXABRASION_W2 = dbResult.FLEXABRASION_W2;
                        inst.FLEXABRASION_W3 = dbResult.FLEXABRASION_W3;
                        inst.FLEXABRASION_F1 = dbResult.FLEXABRASION_F1;
                        inst.FLEXABRASION_F2 = dbResult.FLEXABRASION_F2;
                        inst.FLEXABRASION_F3 = dbResult.FLEXABRASION_F3;
                        inst.STATUS = dbResult.STATUS;
                        inst.REMARK = dbResult.REMARK;
                        inst.APPROVEBY = dbResult.APPROVEBY;
                        inst.APPROVEDATE = dbResult.APPROVEDATE;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.FINISHLENGTH = dbResult.FINISHLENGTH;
                        inst.FINISHINGPROCESS = dbResult.FINISHINGPROCESS;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.FINISHINGMC = dbResult.FINISHINGMC;
                        inst.BOW1 = dbResult.BOW1;
                        inst.BOW2 = dbResult.BOW2;
                        inst.BOW3 = dbResult.BOW3;
                        inst.SKEW1 = dbResult.SKEW1;
                        inst.SKEW2 = dbResult.SKEW2;
                        inst.SKEW3 = dbResult.SKEW3;
                        inst.BENDING_W1 = dbResult.BENDING_W1;
                        inst.BENDING_W2 = dbResult.BENDING_W2;
                        inst.BENDING_W3 = dbResult.BENDING_W3;
                        inst.BENDING_F1 = dbResult.BENDING_F1;
                        inst.BENDING_F2 = dbResult.BENDING_F2;
                        inst.BENDING_F3 = dbResult.BENDING_F3;
                        inst.FLEX_SCOTT_W1 = dbResult.FLEX_SCOTT_W1;
                        inst.FLEX_SCOTT_W2 = dbResult.FLEX_SCOTT_W2;
                        inst.FLEX_SCOTT_W3 = dbResult.FLEX_SCOTT_W3;
                        inst.FLEX_SCOTT_F1 = dbResult.FLEX_SCOTT_F1;
                        inst.FLEX_SCOTT_F2 = dbResult.FLEX_SCOTT_F2;
                        inst.FLEX_SCOTT_F3 = dbResult.FLEX_SCOTT_F3;
                        inst.ITEMLOT = dbResult.ITEMLOT;
                        inst.BATCHNO = dbResult.BATCHNO;
                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.PARTNO = dbResult.PARTNO;
                        inst.FILENAME = dbResult.FILENAME;
                        inst.UPLOADDATE = dbResult.UPLOADDATE;
                        inst.UPLOADBY = dbResult.UPLOADBY;

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

        #region LAB_UPLOADREPORT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WEAVINGLOG"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_ENTRYDATE"></param>
        /// <param name="P_FILENAME"></param>
        /// <param name="P_UPLOADDATE"></param>
        /// <returns></returns>
        public string LAB_UPLOADREPORT(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, DateTime? P_ENTRYDATE, string P_FILENAME,DateTime? P_UPLOADDATE,string P_UPLOADBY)
        {
            string result = string.Empty;

            if (!HasConnection())
                return "Can't connect";

            LAB_UPLOADREPORTParameter dbPara = new LAB_UPLOADREPORTParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOG = P_WEAVINGLOG;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_ENTRYDATE = P_ENTRYDATE;
            dbPara.P_FILENAME = P_FILENAME;
            dbPara.P_UPLOADDATE = P_UPLOADDATE;
            dbPara.P_UPLOADBY = P_UPLOADBY;

            LAB_UPLOADREPORTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_UPLOADREPORT(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        // -- Update 28/10/20 -- //
        #region LAB_GETWEAVINGLOTLIST
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_ITEMCODE"></param>
        /// <param name="P_PROCESS"></param>
        /// <returns></returns>
        public List<LAB_GETWEAVINGLOTLIST> LAB_GETWEAVINGLOTLIST(string P_FINISHINGLOT, string P_ITEMCODE, string P_PROCESS)
        {
            List<LAB_GETWEAVINGLOTLIST> results = null;

            if (!HasConnection())
                return results;

            LAB_GETWEAVINGLOTLISTParameter dbPara = new LAB_GETWEAVINGLOTLISTParameter();
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_ITEMCODE = P_ITEMCODE;
            dbPara.P_PROCESS = P_PROCESS;

            List<LAB_GETWEAVINGLOTLISTResult> dbResults = null;
            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETWEAVINGLOTLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETWEAVINGLOTLIST>();
                    foreach (LAB_GETWEAVINGLOTLISTResult dbResult in dbResults)
                    {
                        LAB_GETWEAVINGLOTLIST inst = new LAB_GETWEAVINGLOTLIST();

                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;

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


        // -- Update 18/01/21 -- //

        #region LAB_SAVEAPPROVEPRODUCTION
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_WEAVINGLOG"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_ENTRYDATE"></param>
        /// <param name="P_WIDTH"></param>
        /// <param name="P_USEWIDTH1"></param>
        /// <param name="P_USEWIDTH2"></param>
        /// <param name="P_USEWIDTH3"></param>
        /// <param name="P_WIDTHSILICONE1"></param>
        /// <param name="P_WIDTHSILICONE2"></param>
        /// <param name="P_WIDTHSILICONE3"></param>
        /// <param name="P_NUMTHREADS_W1"></param>
        /// <param name="P_NUMTHREADS_W2"></param>
        /// <param name="P_NUMTHREADS_W3"></param>
        /// <param name="P_NUMTHREADS_F1"></param>
        /// <param name="P_NUMTHREADS_F2"></param>
        /// <param name="P_NUMTHREADS_F3"></param>
        /// <param name="P_TOTALWEIGHT1"></param>
        /// <param name="P_TOTALWEIGHT2"></param>
        /// <param name="P_TOTALWEIGHT3"></param>
        /// <param name="P_TOTALWEIGHT4"></param>
        /// <param name="P_TOTALWEIGHT5"></param>
        /// <param name="P_TOTALWEIGHT6"></param>
        /// <param name="P_UNCOATEDWEIGHT1"></param>
        /// <param name="P_UNCOATEDWEIGHT2"></param>
        /// <param name="P_UNCOATEDWEIGHT3"></param>
        /// <param name="P_UNCOATEDWEIGHT4"></param>
        /// <param name="P_UNCOATEDWEIGHT5"></param>
        /// <param name="P_UNCOATEDWEIGHT6"></param>
        /// <param name="P_COATWEIGHT1"></param>
        /// <param name="P_COATWEIGHT2"></param>
        /// <param name="P_COATWEIGHT3"></param>
        /// <param name="P_COATWEIGHT4"></param>
        /// <param name="P_COATWEIGHT5"></param>
        /// <param name="P_COATWEIGHT6"></param>
        /// <param name="P_THICKNESS1"></param>
        /// <param name="P_THICKNESS2"></param>
        /// <param name="P_THICKNESS3"></param>
        /// <param name="P_MAXFORCE_W1"></param>
        /// <param name="P_MAXFORCE_W2"></param>
        /// <param name="P_MAXFORCE_W3"></param>
        /// <param name="P_MAXFORCE_F1"></param>
        /// <param name="P_MAXFORCE_F2"></param>
        /// <param name="P_MAXFORCE_F3"></param>
        /// <param name="P_ELOGATION_W1"></param>
        /// <param name="P_ELOGATION_W2"></param>
        /// <param name="P_ELOGATION_W3"></param>
        /// <param name="P_ELOGATION_F1"></param>
        /// <param name="P_ELOGATION_F2"></param>
        /// <param name="P_ELOGATION_F3"></param>
        /// <param name="P_FLAMMABILITY_W"></param>
        /// <param name="P_FLAMMABILITY_W2"></param>
        /// <param name="P_FLAMMABILITY_W3"></param>
        /// <param name="P_FLAMMABILITY_W4"></param>
        /// <param name="P_FLAMMABILITY_W5"></param>
        /// <param name="P_FLAMMABILITY_F"></param>
        /// <param name="P_FLAMMABILITY_F2"></param>
        /// <param name="P_FLAMMABILITY_F3"></param>
        /// <param name="P_FLAMMABILITY_F4"></param>
        /// <param name="P_FLAMMABILITY_F5"></param>
        /// <param name="P_EDGECOMB_W1"></param>
        /// <param name="P_EDGECOMB_W2"></param>
        /// <param name="P_EDGECOMB_W3"></param>
        /// <param name="P_EDGECOMB_F1"></param>
        /// <param name="P_EDGECOMB_F2"></param>
        /// <param name="P_EDGECOMB_F3"></param>
        /// <param name="P_STIFFNESS_W1"></param>
        /// <param name="P_STIFFNESS_W2"></param>
        /// <param name="P_STIFFNESS_W3"></param>
        /// <param name="P_STIFFNESS_F1"></param>
        /// <param name="P_STIFFNESS_F2"></param>
        /// <param name="P_STIFFNESS_F3"></param>
        /// <param name="P_TEAR_W1"></param>
        /// <param name="P_TEAR_W2"></param>
        /// <param name="P_TEAR_W3"></param>
        /// <param name="P_TEAR_F1"></param>
        /// <param name="P_TEAR_F2"></param>
        /// <param name="P_TEAR_F3"></param>
        /// <param name="P_STATIC_AIR1"></param>
        /// <param name="P_STATIC_AIR2"></param>
        /// <param name="P_STATIC_AIR3"></param>
        /// <param name="P_STATIC_AIR4"></param>
        /// <param name="P_STATIC_AIR5"></param>
        /// <param name="P_STATIC_AIR6"></param>
        /// <param name="P_DYNAMIC_AIR1"></param>
        /// <param name="P_DYNAMIC_AIR2"></param>
        /// <param name="P_DYNAMIC_AIR3"></param>
        /// <param name="P_EXPONENT1"></param>
        /// <param name="P_EXPONENT2"></param>
        /// <param name="P_EXPONENT3"></param>
        /// <param name="P_DIMENSCHANGE_W1"></param>
        /// <param name="P_DIMENSCHANGE_W2"></param>
        /// <param name="P_DIMENSCHANGE_W3"></param>
        /// <param name="P_DIMENSCHANGE_F1"></param>
        /// <param name="P_DIMENSCHANGE_F2"></param>
        /// <param name="P_DIMENSCHANGE_F3"></param>
        /// <param name="P_FLEXABRASION_W1"></param>
        /// <param name="P_FLEXABRASION_W2"></param>
        /// <param name="P_FLEXABRASION_W3"></param>
        /// <param name="P_FLEXABRASION_F1"></param>
        /// <param name="P_FLEXABRASION_F2"></param>
        /// <param name="P_FLEXABRASION_F3"></param>
        /// <param name="P_STATUS"></param>
        /// <param name="P_REMARK"></param>
        /// <param name="P_APPROVEBY"></param>
        /// <param name="P_APPROVEDATE"></param>
        /// <param name="P_BOW1"></param>
        /// <param name="P_BOW2"></param>
        /// <param name="P_BOW3"></param>
        /// <param name="P_SKEW1"></param>
        /// <param name="P_SKEW2"></param>
        /// <param name="P_SKEW3"></param>
        /// <param name="P_BENDING_W1"></param>
        /// <param name="P_BENDING_W2"></param>
        /// <param name="P_BENDING_W3"></param>
        /// <param name="P_BENDING_F1"></param>
        /// <param name="P_BENDING_F2"></param>
        /// <param name="P_BENDING_F3"></param>
        /// <param name="P_FLEX_SCOTT_W1"></param>
        /// <param name="P_FLEX_SCOTT_W2"></param>
        /// <param name="P_FLEX_SCOTT_W3"></param>
        /// <param name="P_FLEX_SCOTT_F1"></param>
        /// <param name="P_FLEX_SCOTT_F2"></param>
        /// <param name="P_FLEX_SCOTT_F3"></param>
        /// <returns></returns>
        public string LAB_SAVEAPPROVEPRODUCTION(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, DateTime? P_ENTRYDATE,
            decimal? P_WIDTH, decimal? P_USEWIDTH1, decimal? P_USEWIDTH2, decimal? P_USEWIDTH3, decimal? P_WIDTHSILICONE1, decimal? P_WIDTHSILICONE2, decimal? P_WIDTHSILICONE3,
            decimal? P_NUMTHREADS_W1, decimal? P_NUMTHREADS_W2, decimal? P_NUMTHREADS_W3, decimal? P_NUMTHREADS_F1, decimal? P_NUMTHREADS_F2, decimal? P_NUMTHREADS_F3,
            decimal? P_TOTALWEIGHT1, decimal? P_TOTALWEIGHT2, decimal? P_TOTALWEIGHT3, decimal? P_TOTALWEIGHT4, decimal? P_TOTALWEIGHT5, decimal? P_TOTALWEIGHT6,
            decimal? P_UNCOATEDWEIGHT1, decimal? P_UNCOATEDWEIGHT2, decimal? P_UNCOATEDWEIGHT3, decimal? P_UNCOATEDWEIGHT4, decimal? P_UNCOATEDWEIGHT5, decimal? P_UNCOATEDWEIGHT6,
            decimal? P_COATWEIGHT1, decimal? P_COATWEIGHT2, decimal? P_COATWEIGHT3, decimal? P_COATWEIGHT4, decimal? P_COATWEIGHT5, decimal? P_COATWEIGHT6,
            decimal? P_THICKNESS1, decimal? P_THICKNESS2, decimal? P_THICKNESS3, 
            decimal? P_MAXFORCE_W1, decimal? P_MAXFORCE_W2, decimal? P_MAXFORCE_W3,
            decimal? P_MAXFORCE_W4, decimal? P_MAXFORCE_W5, decimal? P_MAXFORCE_W6,
            decimal? P_MAXFORCE_F1, decimal? P_MAXFORCE_F2, decimal? P_MAXFORCE_F3,
            decimal? P_MAXFORCE_F4, decimal? P_MAXFORCE_F5, decimal? P_MAXFORCE_F6, 
            decimal? P_ELOGATION_W1, decimal? P_ELOGATION_W2, decimal? P_ELOGATION_W3,
            decimal? P_ELOGATION_W4, decimal? P_ELOGATION_W5, decimal? P_ELOGATION_W6,
            decimal? P_ELOGATION_F1, decimal? P_ELOGATION_F2, decimal? P_ELOGATION_F3,
            decimal? P_ELOGATION_F4, decimal? P_ELOGATION_F5, decimal? P_ELOGATION_F6,
            decimal? P_FLAMMABILITY_W, decimal? P_FLAMMABILITY_W2, decimal? P_FLAMMABILITY_W3, decimal? P_FLAMMABILITY_W4, decimal? P_FLAMMABILITY_W5,
            decimal? P_FLAMMABILITY_F, decimal? P_FLAMMABILITY_F2, decimal? P_FLAMMABILITY_F3, decimal? P_FLAMMABILITY_F4, decimal? P_FLAMMABILITY_F5,
            decimal? P_EDGECOMB_W1, decimal? P_EDGECOMB_W2, decimal? P_EDGECOMB_W3, decimal? P_EDGECOMB_F1, decimal? P_EDGECOMB_F2, decimal? P_EDGECOMB_F3,
            decimal? P_STIFFNESS_W1, decimal? P_STIFFNESS_W2, decimal? P_STIFFNESS_W3, decimal? P_STIFFNESS_F1, decimal? P_STIFFNESS_F2, decimal? P_STIFFNESS_F3,
            decimal? P_TEAR_W1, decimal? P_TEAR_W2, decimal? P_TEAR_W3, decimal? P_TEAR_F1, decimal? P_TEAR_F2, decimal? P_TEAR_F3,
            decimal? P_STATIC_AIR1, decimal? P_STATIC_AIR2, decimal? P_STATIC_AIR3, decimal? P_STATIC_AIR4, decimal? P_STATIC_AIR5, decimal? P_STATIC_AIR6,
            decimal? P_DYNAMIC_AIR1, decimal? P_DYNAMIC_AIR2, decimal? P_DYNAMIC_AIR3,
            decimal? P_EXPONENT1, decimal? P_EXPONENT2, decimal? P_EXPONENT3, 
            decimal? P_DIMENSCHANGE_W1, decimal? P_DIMENSCHANGE_W2, decimal? P_DIMENSCHANGE_W3,decimal? P_DIMENSCHANGE_F1, decimal? P_DIMENSCHANGE_F2, decimal? P_DIMENSCHANGE_F3, 
            decimal? P_FLEXABRASION_W1, decimal? P_FLEXABRASION_W2, decimal? P_FLEXABRASION_W3,decimal? P_FLEXABRASION_F1, decimal? P_FLEXABRASION_F2, decimal? P_FLEXABRASION_F3, 
            decimal? P_BOW1, decimal? P_BOW2, decimal? P_BOW3, decimal? P_SKEW1, decimal? P_SKEW2, decimal? P_SKEW3,
            decimal? P_BENDING_W1, decimal? P_BENDING_W2, decimal? P_BENDING_W3, decimal? P_BENDING_F1, decimal? P_BENDING_F2, decimal? P_BENDING_F3,
            decimal? P_FLEX_SCOTT_W1, decimal? P_FLEX_SCOTT_W2, decimal? P_FLEX_SCOTT_W3, decimal? P_FLEX_SCOTT_F1, decimal? P_FLEX_SCOTT_F2, decimal? P_FLEX_SCOTT_F3,
            string P_STATUS, string P_REMARK, string P_APPROVEBY, DateTime? P_APPROVEDATE)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_WEAVINGLOG) && string.IsNullOrWhiteSpace(P_FINISHINGLOT))
                return "ITMCODE & WEAVINGLOG & FINISHINGLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_SAVEAPPROVEPRODUCTIONParameter dbPara = new LAB_SAVEAPPROVEPRODUCTIONParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_WEAVINGLOG = P_WEAVINGLOG;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_ENTRYDATE = P_ENTRYDATE;
            dbPara.P_WIDTH = P_WIDTH;
            dbPara.P_USEWIDTH1 = P_USEWIDTH1;
            dbPara.P_USEWIDTH2 = P_USEWIDTH2;
            dbPara.P_USEWIDTH3 = P_USEWIDTH3;
            dbPara.P_WIDTHSILICONE1 = P_WIDTHSILICONE1;
            dbPara.P_WIDTHSILICONE2 = P_WIDTHSILICONE2;
            dbPara.P_WIDTHSILICONE3 = P_WIDTHSILICONE3;
            dbPara.P_NUMTHREADS_W1 = P_NUMTHREADS_W1;
            dbPara.P_NUMTHREADS_W2 = P_NUMTHREADS_W2;
            dbPara.P_NUMTHREADS_W3 = P_NUMTHREADS_W3;
            dbPara.P_NUMTHREADS_F1 = P_NUMTHREADS_F1;
            dbPara.P_NUMTHREADS_F2 = P_NUMTHREADS_F2;
            dbPara.P_NUMTHREADS_F3 = P_NUMTHREADS_F3;
            dbPara.P_TOTALWEIGHT1 = P_TOTALWEIGHT1;
            dbPara.P_TOTALWEIGHT2 = P_TOTALWEIGHT2;
            dbPara.P_TOTALWEIGHT3 = P_TOTALWEIGHT3;
            dbPara.P_TOTALWEIGHT4 = P_TOTALWEIGHT4;
            dbPara.P_TOTALWEIGHT5 = P_TOTALWEIGHT5;
            dbPara.P_TOTALWEIGHT6 = P_TOTALWEIGHT6;
            dbPara.P_UNCOATEDWEIGHT1 = P_UNCOATEDWEIGHT1;
            dbPara.P_UNCOATEDWEIGHT2 = P_UNCOATEDWEIGHT2;
            dbPara.P_UNCOATEDWEIGHT3 = P_UNCOATEDWEIGHT3;
            dbPara.P_UNCOATEDWEIGHT4 = P_UNCOATEDWEIGHT4;
            dbPara.P_UNCOATEDWEIGHT5 = P_UNCOATEDWEIGHT5;
            dbPara.P_UNCOATEDWEIGHT6 = P_UNCOATEDWEIGHT6;
            dbPara.P_COATWEIGHT1 = P_COATWEIGHT1;
            dbPara.P_COATWEIGHT2 = P_COATWEIGHT2;
            dbPara.P_COATWEIGHT3 = P_COATWEIGHT3;
            dbPara.P_COATWEIGHT4 = P_COATWEIGHT4;
            dbPara.P_COATWEIGHT5 = P_COATWEIGHT5;
            dbPara.P_COATWEIGHT6 = P_COATWEIGHT6;
            dbPara.P_THICKNESS1 = P_THICKNESS1;
            dbPara.P_THICKNESS2 = P_THICKNESS2;
            dbPara.P_THICKNESS3 = P_THICKNESS3;

            dbPara.P_MAXFORCE_W1 = P_MAXFORCE_W1;
            dbPara.P_MAXFORCE_W2 = P_MAXFORCE_W2;
            dbPara.P_MAXFORCE_W3 = P_MAXFORCE_W3;

            dbPara.P_MAXFORCE_W4 = P_MAXFORCE_W4;
            dbPara.P_MAXFORCE_W5 = P_MAXFORCE_W5;
            dbPara.P_MAXFORCE_W6 = P_MAXFORCE_W6;

            dbPara.P_MAXFORCE_F1 = P_MAXFORCE_F1;
            dbPara.P_MAXFORCE_F2 = P_MAXFORCE_F2;
            dbPara.P_MAXFORCE_F3 = P_MAXFORCE_F3;

            dbPara.P_MAXFORCE_F4 = P_MAXFORCE_F4;
            dbPara.P_MAXFORCE_F5 = P_MAXFORCE_F5;
            dbPara.P_MAXFORCE_F6 = P_MAXFORCE_F6;

            dbPara.P_ELOGATION_W1 = P_ELOGATION_W1;
            dbPara.P_ELOGATION_W2 = P_ELOGATION_W2;
            dbPara.P_ELOGATION_W3 = P_ELOGATION_W3;

            dbPara.P_ELOGATION_W4 = P_ELOGATION_W4;
            dbPara.P_ELOGATION_W5 = P_ELOGATION_W5;
            dbPara.P_ELOGATION_W6 = P_ELOGATION_W6;

            dbPara.P_ELOGATION_F1 = P_ELOGATION_F1;
            dbPara.P_ELOGATION_F2 = P_ELOGATION_F2;
            dbPara.P_ELOGATION_F3 = P_ELOGATION_F3;

            dbPara.P_ELOGATION_F4 = P_ELOGATION_F4;
            dbPara.P_ELOGATION_F5 = P_ELOGATION_F5;
            dbPara.P_ELOGATION_F6 = P_ELOGATION_F6;

            dbPara.P_FLAMMABILITY_W = P_FLAMMABILITY_W;
            dbPara.P_FLAMMABILITY_W2 = P_FLAMMABILITY_W2;
            dbPara.P_FLAMMABILITY_W3 = P_FLAMMABILITY_W3;
            dbPara.P_FLAMMABILITY_W4 = P_FLAMMABILITY_W4;
            dbPara.P_FLAMMABILITY_W5 = P_FLAMMABILITY_W5;
            dbPara.P_FLAMMABILITY_F = P_FLAMMABILITY_F;
            dbPara.P_FLAMMABILITY_F2 = P_FLAMMABILITY_F2;
            dbPara.P_FLAMMABILITY_F3 = P_FLAMMABILITY_F3;
            dbPara.P_FLAMMABILITY_F4 = P_FLAMMABILITY_F4;
            dbPara.P_FLAMMABILITY_F5 = P_FLAMMABILITY_F5;
            dbPara.P_EDGECOMB_W1 = P_EDGECOMB_W1;
            dbPara.P_EDGECOMB_W2 = P_EDGECOMB_W2;
            dbPara.P_EDGECOMB_W3 = P_EDGECOMB_W3;
            dbPara.P_EDGECOMB_F1 = P_EDGECOMB_F1;
            dbPara.P_EDGECOMB_F2 = P_EDGECOMB_F2;
            dbPara.P_EDGECOMB_F3 = P_EDGECOMB_F3;
            dbPara.P_STIFFNESS_W1 = P_STIFFNESS_W1;
            dbPara.P_STIFFNESS_W2 = P_STIFFNESS_W2;
            dbPara.P_STIFFNESS_W3 = P_STIFFNESS_W3;
            dbPara.P_STIFFNESS_F1 = P_STIFFNESS_F1;
            dbPara.P_STIFFNESS_F2 = P_STIFFNESS_F2;
            dbPara.P_STIFFNESS_F3 = P_STIFFNESS_F3;
            dbPara.P_TEAR_W1 = P_TEAR_W1;
            dbPara.P_TEAR_W2 = P_TEAR_W2;
            dbPara.P_TEAR_W3 = P_TEAR_W3;
            dbPara.P_TEAR_F1 = P_TEAR_F1;
            dbPara.P_TEAR_F2 = P_TEAR_F2;
            dbPara.P_TEAR_F3 = P_TEAR_F3;
            dbPara.P_STATIC_AIR1 = P_STATIC_AIR1;
            dbPara.P_STATIC_AIR2 = P_STATIC_AIR2;
            dbPara.P_STATIC_AIR3 = P_STATIC_AIR3;
            dbPara.P_STATIC_AIR4 = P_STATIC_AIR4;
            dbPara.P_STATIC_AIR5 = P_STATIC_AIR5;
            dbPara.P_STATIC_AIR6 = P_STATIC_AIR6;
            dbPara.P_DYNAMIC_AIR1 = P_DYNAMIC_AIR1;
            dbPara.P_DYNAMIC_AIR2 = P_DYNAMIC_AIR2;
            dbPara.P_DYNAMIC_AIR3 = P_DYNAMIC_AIR3;
            dbPara.P_EXPONENT1 = P_EXPONENT1;
            dbPara.P_EXPONENT2 = P_EXPONENT2;
            dbPara.P_EXPONENT3 = P_EXPONENT3;
            dbPara.P_DIMENSCHANGE_W1 = P_DIMENSCHANGE_W1;
            dbPara.P_DIMENSCHANGE_W2 = P_DIMENSCHANGE_W2;
            dbPara.P_DIMENSCHANGE_W3 = P_DIMENSCHANGE_W3;
            dbPara.P_DIMENSCHANGE_F1 = P_DIMENSCHANGE_F1;
            dbPara.P_DIMENSCHANGE_F2 = P_DIMENSCHANGE_F2;
            dbPara.P_DIMENSCHANGE_F3 = P_DIMENSCHANGE_F3;
            dbPara.P_FLEXABRASION_W1 = P_FLEXABRASION_W1;
            dbPara.P_FLEXABRASION_W2 = P_FLEXABRASION_W2;
            dbPara.P_FLEXABRASION_W3 = P_FLEXABRASION_W3;
            dbPara.P_FLEXABRASION_F1 = P_FLEXABRASION_F1;
            dbPara.P_FLEXABRASION_F2 = P_FLEXABRASION_F2;
            dbPara.P_FLEXABRASION_F3 = P_FLEXABRASION_F3;
            dbPara.P_BOW1 = P_BOW1;
            dbPara.P_BOW2 = P_BOW2;
            dbPara.P_BOW3 = P_BOW3;
            dbPara.P_SKEW1 = P_SKEW1;
            dbPara.P_SKEW2 = P_SKEW2;
            dbPara.P_SKEW3 = P_SKEW3;
            dbPara.P_BENDING_W1 = P_BENDING_W1;
            dbPara.P_BENDING_W2 = P_BENDING_W2;
            dbPara.P_BENDING_W3 = P_BENDING_W3;
            dbPara.P_BENDING_F1 = P_BENDING_F1;
            dbPara.P_BENDING_F2 = P_BENDING_F2;
            dbPara.P_BENDING_F3 = P_BENDING_F3;
            dbPara.P_FLEX_SCOTT_W1 = P_FLEX_SCOTT_W1;
            dbPara.P_FLEX_SCOTT_W2 = P_FLEX_SCOTT_W2;
            dbPara.P_FLEX_SCOTT_W3 = P_FLEX_SCOTT_W3;
            dbPara.P_FLEX_SCOTT_F1 = P_FLEX_SCOTT_F1;
            dbPara.P_FLEX_SCOTT_F2 = P_FLEX_SCOTT_F2;
            dbPara.P_FLEX_SCOTT_F3 = P_FLEX_SCOTT_F3;

            dbPara.P_STATUS = P_STATUS;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_APPROVEBY = P_APPROVEBY;
            dbPara.P_APPROVEDATE = P_APPROVEDATE;
  
            LAB_SAVEAPPROVEPRODUCTIONResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_SAVEAPPROVEPRODUCTION(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        // New 1/9/22

        #region LAB_SAVEREPLCWEIGHT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_TYPE"></param>
        /// <param name="P_WEIGHT1"></param>
        /// <param name="P_WEIGHT2"></param>
        /// <param name="P_WEIGHT3"></param>
        /// <param name="P_WEIGHT4"></param>
        /// <param name="P_WEIGHT5"></param>
        /// <param name="P_WEIGHT6"></param>
        /// <param name="P_WEIGHTDATE"></param>
        /// <param name="P_WEIGHTBY"></param>
        /// <returns></returns>
        public string LAB_SAVEREPLCWEIGHT(string P_ITMCODE, string P_PRODUCTIONLOT,string P_TYPE,
            decimal? P_WEIGHT1, decimal? P_WEIGHT2, decimal? P_WEIGHT3, decimal? P_WEIGHT4, decimal? P_WEIGHT5, decimal? P_WEIGHT6,
            DateTime? P_WEIGHTDATE, string P_WEIGHTBY)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_PRODUCTIONLOT))
                return "ITMCODE & PRODUCTIONLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_SAVEREPLCWEIGHTParameter dbPara = new LAB_SAVEREPLCWEIGHTParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;
            dbPara.P_TYPE = P_TYPE;
            dbPara.P_WEIGHT1 = P_WEIGHT1;
            dbPara.P_WEIGHT2 = P_WEIGHT2;
            dbPara.P_WEIGHT3 = P_WEIGHT3;
            dbPara.P_WEIGHT4 = P_WEIGHT4;
            dbPara.P_WEIGHT5 = P_WEIGHT5;
            dbPara.P_WEIGHT6 = P_WEIGHT6;

            dbPara.P_WEIGHTDATE = P_WEIGHTDATE;
            dbPara.P_WEIGHTBY = P_WEIGHTBY;

            LAB_SAVEREPLCWEIGHTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_SAVEREPLCWEIGHT(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region LAB_SAVEREPLCSTATICAIR
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_STATIC_AIR1"></param>
        /// <param name="P_STATIC_AIR2"></param>
        /// <param name="P_STATIC_AIR3"></param>
        /// <param name="P_STATIC_AIR4"></param>
        /// <param name="P_STATIC_AIR5"></param>
        /// <param name="P_STATIC_AIR6"></param>
        /// <param name="P_STATICAIRDATE"></param>
        /// <param name="P_STATICAIRBY"></param>
        /// <returns></returns>
        public string LAB_SAVEREPLCSTATICAIR(string P_ITMCODE, string P_PRODUCTIONLOT,
            decimal? P_STATIC_AIR1, decimal? P_STATIC_AIR2, decimal? P_STATIC_AIR3, decimal? P_STATIC_AIR4, decimal? P_STATIC_AIR5, decimal? P_STATIC_AIR6,
            DateTime? P_STATICAIRDATE, string P_STATICAIRBY)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_PRODUCTIONLOT))
                return "ITMCODE & PRODUCTIONLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_SAVEREPLCSTATICAIRParameter dbPara = new LAB_SAVEREPLCSTATICAIRParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;

            dbPara.P_STATIC_AIR1 = P_STATIC_AIR1;
            dbPara.P_STATIC_AIR2 = P_STATIC_AIR2;
            dbPara.P_STATIC_AIR3 = P_STATIC_AIR3;
            dbPara.P_STATIC_AIR4 = P_STATIC_AIR4;
            dbPara.P_STATIC_AIR5 = P_STATIC_AIR5;
            dbPara.P_STATIC_AIR6 = P_STATIC_AIR6;

            dbPara.P_STATICAIRDATE = P_STATICAIRDATE;
            dbPara.P_STATICAIRBY = P_STATICAIRBY;

            LAB_SAVEREPLCSTATICAIRResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_SAVEREPLCSTATICAIR(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region LAB_SAVEREPLCDYNAMICAIR
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_DYNAMIC_AIR1"></param>
        /// <param name="P_DYNAMIC_AIR2"></param>
        /// <param name="P_DYNAMIC_AIR3"></param>
        /// <param name="P_EXPONENT1"></param>
        /// <param name="P_EXPONENT2"></param>
        /// <param name="P_EXPONENT3"></param>
        /// <param name="P_DYNAMICDATE"></param>
        /// <param name="P_DYNAMICBY"></param>
        /// <returns></returns>
        public string LAB_SAVEREPLCDYNAMICAIR(string P_ITMCODE, string P_PRODUCTIONLOT,
        decimal? P_DYNAMIC_AIR1, decimal? P_DYNAMIC_AIR2, decimal? P_DYNAMIC_AIR3, decimal? P_EXPONENT1, decimal? P_EXPONENT2, decimal? P_EXPONENT3,
        DateTime? P_DYNAMICDATE, string P_DYNAMICBY)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_PRODUCTIONLOT))
                return "ITMCODE & PRODUCTIONLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_SAVEREPLCDYNAMICAIRParameter dbPara = new LAB_SAVEREPLCDYNAMICAIRParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;

            dbPara.P_DYNAMIC_AIR1 = P_DYNAMIC_AIR1;
            dbPara.P_DYNAMIC_AIR2 = P_DYNAMIC_AIR2;
            dbPara.P_DYNAMIC_AIR3 = P_DYNAMIC_AIR3;
            dbPara.P_EXPONENT1 = P_EXPONENT1;
            dbPara.P_EXPONENT2 = P_EXPONENT2;
            dbPara.P_EXPONENT3 = P_EXPONENT3;

            dbPara.P_DYNAMICDATE = P_DYNAMICDATE;
            dbPara.P_DYNAMICBY = P_DYNAMICBY;

            LAB_SAVEREPLCDYNAMICAIRResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_SAVEREPLCDYNAMICAIR(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region LAB_SAVEREPLCSTIFFNESS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_STIFFNESS_W1"></param>
        /// <param name="P_STIFFNESS_W2"></param>
        /// <param name="P_STIFFNESS_W3"></param>
        /// <param name="P_STIFFNESS_F1"></param>
        /// <param name="P_STIFFNESS_F2"></param>
        /// <param name="P_STIFFNESS_F3"></param>
        /// <param name="P_STIFFNESSDATE"></param>
        /// <param name="P_STIFFNESSBY"></param>
        /// <returns></returns>
        public string LAB_SAVEREPLCSTIFFNESS(string P_ITMCODE, string P_PRODUCTIONLOT,
        decimal? P_STIFFNESS_W1, decimal? P_STIFFNESS_W2, decimal? P_STIFFNESS_W3, decimal? P_STIFFNESS_F1, decimal? P_STIFFNESS_F2, decimal? P_STIFFNESS_F3,
        DateTime? P_STIFFNESSDATE, string P_STIFFNESSBY)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_ITMCODE) && string.IsNullOrWhiteSpace(P_PRODUCTIONLOT))
                return "ITMCODE & PRODUCTIONLOT isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_SAVEREPLCSTIFFNESSParameter dbPara = new LAB_SAVEREPLCSTIFFNESSParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;
        
            dbPara.P_STIFFNESS_W1 = P_STIFFNESS_W1;
            dbPara.P_STIFFNESS_W2 = P_STIFFNESS_W2;
            dbPara.P_STIFFNESS_W3 = P_STIFFNESS_W3;
            dbPara.P_STIFFNESS_F1 = P_STIFFNESS_F1;
            dbPara.P_STIFFNESS_F2 = P_STIFFNESS_F2;
            dbPara.P_STIFFNESS_F3 = P_STIFFNESS_F3;

            dbPara.P_STIFFNESSDATE = P_STIFFNESSDATE;
            dbPara.P_STIFFNESSBY = P_STIFFNESSBY;

            LAB_SAVEREPLCSTIFFNESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.LAB_SAVEREPLCSTIFFNESS(dbPara);

                if (dbResult != null)
                    result = dbResult.P_RETURN;
                else
                    result = "0";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #region LAB_GETWEIGHTDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <param name="P_TYPE"></param>
        /// <returns></returns>
        public List<LAB_GETWEIGHTDATA> LAB_GETWEIGHTDATA(string P_ITMCODE, string P_PRODUCTIONLOT, string P_TYPE)
        {
            List<LAB_GETWEIGHTDATA> results = null;

            if (!HasConnection())
                return results;

            LAB_GETWEIGHTDATAParameter dbPara = new LAB_GETWEIGHTDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;
            dbPara.P_TYPE = P_TYPE;

            List<LAB_GETWEIGHTDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETWEIGHTDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETWEIGHTDATA>();
                    foreach (LAB_GETWEIGHTDATAResult dbResult in dbResults)
                    {
                        LAB_GETWEIGHTDATA inst = new LAB_GETWEIGHTDATA();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;

                        inst.WEIGHT1 = dbResult.WEIGHT1;
                        inst.WEIGHT2 = dbResult.WEIGHT2;
                        inst.WEIGHT3 = dbResult.WEIGHT3;
                        inst.WEIGHT4 = dbResult.WEIGHT4;
                        inst.WEIGHT5 = dbResult.WEIGHT5;
                        inst.WEIGHT6 = dbResult.WEIGHT6;

                        inst.WEIGHTDATE = dbResult.WEIGHTDATE;
                        inst.WEIGHT_BY = dbResult.WEIGHT_BY;

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

        #region LAB_GETSTIFFNESSDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <returns></returns>
        public List<LAB_GETSTIFFNESSDATA> LAB_GETSTIFFNESSDATA(string P_ITMCODE, string P_PRODUCTIONLOT)
        {
            List<LAB_GETSTIFFNESSDATA> results = null;

            if (!HasConnection())
                return results;

            LAB_GETSTIFFNESSDATAParameter dbPara = new LAB_GETSTIFFNESSDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;

            List<LAB_GETSTIFFNESSDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETSTIFFNESSDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETSTIFFNESSDATA>();
                    foreach (LAB_GETSTIFFNESSDATAResult dbResult in dbResults)
                    {
                        LAB_GETSTIFFNESSDATA inst = new LAB_GETSTIFFNESSDATA();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;

                        inst.STIFFNESS_W1 = dbResult.STIFFNESS_W1;
                        inst.STIFFNESS_W2 = dbResult.STIFFNESS_W2;
                        inst.STIFFNESS_W3 = dbResult.STIFFNESS_W3;
                        inst.STIFFNESS_F1 = dbResult.STIFFNESS_F1;
                        inst.STIFFNESS_F2 = dbResult.STIFFNESS_F2;
                        inst.STIFFNESS_F3 = dbResult.STIFFNESS_F3;
                     
                        inst.STIFFNESSDATE = dbResult.STIFFNESSDATE;
                        inst.STIFFNESS_BY = dbResult.STIFFNESS_BY;
                       
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

        #region LAB_GETSTATICAIRDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <returns></returns>
        public List<LAB_GETSTATICAIRDATA> LAB_GETSTATICAIRDATA(string P_ITMCODE, string P_PRODUCTIONLOT)
        {
            List<LAB_GETSTATICAIRDATA> results = null;

            if (!HasConnection())
                return results;

            LAB_GETSTATICAIRDATAParameter dbPara = new LAB_GETSTATICAIRDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;

            List<LAB_GETSTATICAIRDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETSTATICAIRDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETSTATICAIRDATA>();
                    foreach (LAB_GETSTATICAIRDATAResult dbResult in dbResults)
                    {
                        LAB_GETSTATICAIRDATA inst = new LAB_GETSTATICAIRDATA();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;

                        inst.STATIC_AIR1 = dbResult.STATIC_AIR1;
                        inst.STATIC_AIR2 = dbResult.STATIC_AIR2;
                        inst.STATIC_AIR3 = dbResult.STATIC_AIR3;
                        inst.STATIC_AIR4 = dbResult.STATIC_AIR4;
                        inst.STATIC_AIR5 = dbResult.STATIC_AIR5;
                        inst.STATIC_AIR6 = dbResult.STATIC_AIR6;

                        inst.STATICAIRDATE = dbResult.STATICAIRDATE;
                        inst.STATICAIR_BY = dbResult.STATICAIR_BY;

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

        #region LAB_GETDYNAMICAIRDATA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <returns></returns>
        public List<LAB_GETDYNAMICAIRDATA> LAB_GETDYNAMICAIRDATA(string P_ITMCODE, string P_PRODUCTIONLOT)
        {
            List<LAB_GETDYNAMICAIRDATA> results = null;

            if (!HasConnection())
                return results;

            LAB_GETDYNAMICAIRDATAParameter dbPara = new LAB_GETDYNAMICAIRDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;

            List<LAB_GETDYNAMICAIRDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETDYNAMICAIRDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<LAB_GETDYNAMICAIRDATA>();
                    foreach (LAB_GETDYNAMICAIRDATAResult dbResult in dbResults)
                    {
                        LAB_GETDYNAMICAIRDATA inst = new LAB_GETDYNAMICAIRDATA();

                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;

                        inst.EXPONENT1 = dbResult.EXPONENT1;
                        inst.EXPONENT2 = dbResult.EXPONENT2;
                        inst.EXPONENT3 = dbResult.EXPONENT3;
                        inst.DYNAMIC_AIR1 = dbResult.DYNAMIC_AIR1;
                        inst.DYNAMIC_AIR2 = dbResult.DYNAMIC_AIR2;
                        inst.DYNAMIC_AIR3 = dbResult.DYNAMIC_AIR3;

                        inst.DYNAMICDATE = dbResult.DYNAMICDATE;
                        inst.DYNAMIC_BY = dbResult.DYNAMIC_BY;

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

        #region CheckRowWeight
        public bool CheckRowWeight(string P_ITMCODE, string P_PRODUCTIONLOT, string P_TYPE)
        {
            bool results = false;

            if (!HasConnection())
                return results;

            LAB_GETWEIGHTDATAParameter dbPara = new LAB_GETWEIGHTDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;
            dbPara.P_TYPE = P_TYPE;

            List<LAB_GETWEIGHTDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETWEIGHTDATA(dbPara);
                if (null != dbResults)
                {
                    int? chkRow = 0;

                    foreach (LAB_GETWEIGHTDATAResult dbResult in dbResults)
                    {
                        if (dbResult.WEIGHT1 != null || dbResult.WEIGHT2 != null || dbResult.WEIGHT3 != null
                                     || dbResult.WEIGHT4 != null || dbResult.WEIGHT5 != null || dbResult.WEIGHT6 != null)
                        {
                            chkRow++;
                        }

                        if (chkRow > 1)
                            break;
                    }

                    if (chkRow > 1)
                        results = true;
                    else
                        results = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;

        }
        #endregion

        #region CheckRowStiffness
        public bool CheckRowStiffness(string P_ITMCODE, string P_PRODUCTIONLOT)
        {
            bool results = false;

            if (!HasConnection())
                return results;

            LAB_GETSTIFFNESSDATAParameter dbPara = new LAB_GETSTIFFNESSDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;

            List<LAB_GETSTIFFNESSDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETSTIFFNESSDATA(dbPara);
                if (null != dbResults)
                {
                    int? chkRow = 0;

                    foreach (LAB_GETSTIFFNESSDATAResult dbResult in dbResults)
                    {
                        if (dbResult.STIFFNESS_W1 != null || dbResult.STIFFNESS_W2 != null || dbResult.STIFFNESS_W3 != null
                                     || dbResult.STIFFNESS_F1 != null || dbResult.STIFFNESS_F2 != null || dbResult.STIFFNESS_F3 != null)
                        {
                            chkRow++;
                        }

                        if (chkRow > 1)
                            break;
                    }

                    if (chkRow > 1)
                        results = true;
                    else
                        results = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;

        }
        #endregion

        #region CheckRowStaticAir
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <returns></returns>
        public bool CheckRowStaticAir(string P_ITMCODE, string P_PRODUCTIONLOT)
        {
            bool results = false;

            if (!HasConnection())
                return results;

            LAB_GETSTATICAIRDATAParameter dbPara = new LAB_GETSTATICAIRDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;

            List<LAB_GETSTATICAIRDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETSTATICAIRDATA(dbPara);
                if (null != dbResults)
                {
                    int? chkRow = 0;

                    foreach (LAB_GETSTATICAIRDATAResult dbResult in dbResults)
                    {
                        if (dbResult.STATIC_AIR1 != null || dbResult.STATIC_AIR2 != null || dbResult.STATIC_AIR3 != null
                                     || dbResult.STATIC_AIR4 != null || dbResult.STATIC_AIR5 != null || dbResult.STATIC_AIR6 != null)
                        {
                            chkRow++;
                        }

                        if (chkRow > 1)
                            break;
                    }

                    if (chkRow > 1)
                        results = true;
                    else
                        results = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region CheckRowDynamicAir
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_PRODUCTIONLOT"></param>
        /// <returns></returns>
        public bool CheckRowDynamicAir(string P_ITMCODE, string P_PRODUCTIONLOT)
        {
            bool results = false;

            if (!HasConnection())
                return results;

            LAB_GETDYNAMICAIRDATAParameter dbPara = new LAB_GETDYNAMICAIRDATAParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_PRODUCTIONLOT = P_PRODUCTIONLOT;

            List<LAB_GETDYNAMICAIRDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.LAB_GETDYNAMICAIRDATA(dbPara);
                if (null != dbResults)
                {
                    int? chkRow = 0;

                    foreach (LAB_GETDYNAMICAIRDATAResult dbResult in dbResults)
                    {
                        if (dbResult.EXPONENT1 != null || dbResult.EXPONENT2 != null || dbResult.EXPONENT3 != null
                                     || dbResult.DYNAMIC_AIR1 != null || dbResult.DYNAMIC_AIR2 != null || dbResult.DYNAMIC_AIR3 != null)
                        {
                            chkRow++;
                        }

                        if (chkRow > 1)
                            break;
                    }

                    if (chkRow > 1)
                        results = true;
                    else
                        results = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion


        // New 9/1/23

        #region LAB_RETESTRECORD_INSERTUPDATE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITM_CODE"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_FINISHINGLOT"></param>
        /// <param name="P_PROPERTY"></param>
        /// <param name="P_YARNTYPE"></param>
        /// <param name="P_TESTNO"></param>
        /// <param name="P_N1"></param>
        /// <param name="P_N2"></param>
        /// <param name="P_N3"></param>
        /// <param name="P_N4"></param>
        /// <param name="P_N5"></param>
        /// <param name="P_OPERATORID"></param>
        /// <returns></returns>
        public string LAB_RETESTRECORD_INSERTUPDATE(string P_ITM_CODE, string P_WEAVINGLOT, string P_FINISHINGLOT,string P_PROPERTY, string P_YARNTYPE,string P_TESTNO,
            decimal? P_N1, decimal? P_N2, decimal? P_N3, decimal? P_N4, decimal? P_N5, string P_OPERATORID)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(P_ITM_CODE))
                return "Item Code isn't null";

            if (string.IsNullOrEmpty(P_WEAVINGLOT))
                return "Production Lot isn't null";

            if (string.IsNullOrEmpty(P_FINISHINGLOT))
                return "Finishing Lot isn't null";

            if (!HasConnection())
                return "Can't connect";

            LAB_RETESTRECORD_INSERTUPDATEParameter dbPara = new LAB_RETESTRECORD_INSERTUPDATEParameter();
            dbPara.P_ITM_CODE = P_ITM_CODE;
            dbPara.P_WEAVINGLOT = P_WEAVINGLOT;
            dbPara.P_FINISHINGLOT = P_FINISHINGLOT;
            dbPara.P_PROPERTY = P_PROPERTY;
            dbPara.P_YARNTYPE = P_YARNTYPE;
            dbPara.P_TESTNO = P_TESTNO;
            dbPara.P_N1 = P_N1;
            dbPara.P_N2 = P_N2;
            dbPara.P_N3 = P_N3;
            dbPara.P_N4 = P_N4;
            dbPara.P_N5 = P_N5;
            dbPara.P_OPERATORID = P_OPERATORID;

            LAB_RETESTRECORD_INSERTUPDATEResult dbResult = null;

            try
            {
                dbResult = DatabaseManager.Instance.LAB_RETESTRECORD_INSERTUPDATE(dbPara);

                if (dbResult != null)
                    result = dbResult.R_RESULT;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                result = ex.Message.ToString();
            }

            return result;
        }
        #endregion

        #endregion

    }

    #endregion
}
