#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace DataControl.ClassData
{
    public class LabClassData
    {
        #region Lab ClassData
        private static LabClassData _instance = null;

        public static LabClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(LabClassData))
                    {
                        _instance = new LabClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListLAB_SEARCHLABENTRYPRODUCTION

        public class ListLAB_SEARCHLABENTRYPRODUCTION
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListLAB_SEARCHLABENTRYPRODUCTION()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                //BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE128;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListLAB_SEARCHLABENTRYPRODUCTION()
            {
                // default constructor
            }

            public ListLAB_SEARCHLABENTRYPRODUCTION(string P_PARTNO, string P_ITM_CODE, string P_ITM_CODE_B, string P_ITM_CODE_H, string P_WEAVINGLOT, string P_FINISHINGLOT, string P_CUSTOMERID, string P_FINISHINGPROCESS
                , string P_ITEMLOT, string P_LOOMNO, string P_FINISHINGMC, string P_BATCHNO, DateTime? P_ENTRYDATE
                , decimal? P_NUMTHREADS_W_AVG, string P_NUMTHREADS_W_JUD, decimal? P_NUMTHREADS_F_AVG, string P_NUMTHREADS_F_JUD
                , decimal? P_USABLE_WIDTH_AVG, string P_USABLE_WIDTH_JUD, decimal? P_WIDTH_SILICONE_AVG, string P_WIDTH_SILICONE_JUD
                , decimal? P_TOTALWEIGHT_AVG, string P_TOTALWEIGHT_JUD, decimal? P_UNCOATEDWEIGHT_AVG, string P_UNCOATEDWEIGHT_JUD, decimal? P_COATINGWEIGHT_AVG, string P_COATINGWEIGHT_JUD
                , decimal? P_MAXFORCE_W_AVG, string P_MAXFORCE_W_JUD, decimal? P_MAXFORCE_F_AVG, string P_MAXFORCE_F_JUD
                , decimal? P_ELONGATIONFORCE_W_AVG, string P_ELONGATIONFORCE_W_JUD, decimal? P_ELONGATIONFORCE_F_AVG, string P_ELONGATIONFORCE_F_JUD
                , decimal? P_FLAMMABILITY_W, string P_FLAMMABILITY_W_JUD
                , decimal? P_FLAMMABILITY_F, string P_FLAMMABILITY_F_JUD
                , decimal? P_EDGECOMB_W_AVG, string P_EDGECOMB_W_JUD, decimal? P_EDGECOMB_F_AVG, string P_EDGECOMB_F_JUD
                , decimal? P_STIFFNESS_W_AVG, string P_STIFFNESS_W_JUD, decimal? P_STIFFNESS_F_AVG, string P_STIFFNESS_F_JUD
                , decimal? P_TEAR_W_AVG, string P_TEAR_W_JUD, decimal? P_TEAR_F_AVG, string P_TEAR_F_JUD
                , decimal? P_STATIC_AIR_AVG, string P_STATIC_AIR_JUD
                , decimal? P_FLEXABRASION_W_AVG, string P_FLEXABRASION_W_JUD, decimal? P_FLEXABRASION_F_AVG, string P_FLEXABRASION_F_JUD
                , decimal? P_DIMENSCHANGE_W_AVG, string P_DIMENSCHANGE_W_JUD, decimal? P_DIMENSCHANGE_F_AVG, string P_DIMENSCHANGE_F_JUD
                , string P_JUDGEMENT

                , decimal? P_NUMTHREADS_W_Min, decimal? P_NUMTHREADS_W_Max
                , decimal? P_NUMTHREADS_F_Min, decimal? P_NUMTHREADS_F_Max
                , decimal? P_USABLE_WIDTH_Min, decimal? P_WIDTH_SILICONE_Min
                , decimal? P_TOTALWEIGHT_Min, decimal? P_TOTALWEIGHT_Max
                , decimal? P_UNCOATEDWEIGHT_Min, decimal? P_UNCOATEDWEIGHT_Max
                , decimal? P_COATINGWEIGHT_Min, decimal? P_COATINGWEIGHT_Max
                , decimal? P_MAXFORCE_W_Min
                , decimal? P_MAXFORCE_F_Min
                , string P_ELONGATIONFORCE_W_TOR, decimal? P_ELONGATIONFORCE_W_Min, decimal? P_ELONGATIONFORCE_W_Max
                , string P_ELONGATIONFORCE_F_TOR, decimal? P_ELONGATIONFORCE_F_Min, decimal? P_ELONGATIONFORCE_F_Max
                , decimal? P_FLAMMABILITY_W_Max
                , decimal? P_FLAMMABILITY_F_Max
                , decimal? P_EDGECOMB_W_Min
                , decimal? P_EDGECOMB_F_Min
                , decimal? P_STIFFNESS_W_Max
                , decimal? P_STIFFNESS_F_Max
                , decimal? P_TEAR_W_Min
                , decimal? P_TEAR_F_Min
                , decimal? P_STATIC_AIR_Max
                , decimal? P_FLEXABRASION_W_Min
                , decimal? P_FLEXABRASION_F_Min
                , string P_DIMENSCHANGE_W_TOR, decimal? P_DIMENSCHANGE_W_Min, decimal? P_DIMENSCHANGE_W_Max
                , string P_DIMENSCHANGE_F_TOR, decimal? P_DIMENSCHANGE_F_Min, decimal? P_DIMENSCHANGE_F_Max
                , string P_NUMTHREADS_W_Spe, string P_NUMTHREADS_F_Spe, string P_USABLE_Spe, string P_WIDTH_SILICONE_Spe, string P_TOTALWEIGHT_Spe, string P_UNCOATEDWEIGHT_Spe, string P_COATINGWEIGHT_Spe
                , string P_MAXFORCE_W_Spe, string P_MAXFORCE_F_Spe, string P_ELONGATIONFORCE_W_Spe, string P_ELONGATIONFORCE_F_Spe, string P_FLAMMABILITY_W_Spe, string P_FLAMMABILITY_F_Spe
                , string P_EDGECOMB_W_Spe, string P_EDGECOMB_F_Spe, string P_STIFFNESS_W_Spe, string P_STIFFNESS_F_Spe, string P_TEAR_W_Spe, string P_TEAR_F_Spe
                , string P_STATIC_AIR_Spe, string P_FLEXABRASION_W_Spe, string P_FLEXABRASION_F_Spe, string P_DIMENSCHANGE_W_Spe, string P_DIMENSCHANGE_F_Spe

                , string P_REPORT_ID, string P_REVESION, string P_WIDTH, string P_USABLE_WIDTH, string P_NUMTHREADS,
                                    string P_WEIGHT, string P_FLAMMABILITY, string P_EDGECOMB, string P_STIFFNESS, string P_TEAR,
                                    string P_STATIC_AIR, string P_FLEXABRASION, string P_DIMENSCHANGE, DateTime? P_EFFECTIVE_DATE,
                                    string P_YARNTYPE, string P_COATWEIGHT, string P_THICKNESS, string P_MAXFORCE, string P_ELONGATIONFORCE, string P_DYNAMIC_AIR, string P_EXPONENT, string P_REPORT_NAME
                , bool? P_ChkHide1, bool? P_ChkHide2, bool? P_ChkHide3, bool? P_Chk2C
                , string P_FILENAME, DateTime? P_UPLOADDATE, string P_UPLOADBY, bool? P_ChkCM, string P_WEAVINGLOTLIST
                , string P_THICKNESS_Spe, string P_BENDING_W_Spe, string P_BENDING_F_Spe, string P_DYNAMIC_AIR_Spe, string P_EXPONENT_Spe, string P_BENDING
                , decimal? P_THICKNESS_AVG, string P_THICKNESS_JUD, decimal? P_BENDING_W_AVG, string P_BENDING_W_JUD, decimal? P_BENDING_F_AVG, string P_BENDING_F_JUD
                , decimal? P_DYNAMIC_AIR_AVG, string P_DYNAMIC_AIR_JUD, decimal? P_EXPONENT_AVG, string P_EXPONENT_JUD
                , bool? P_ChkSTIFFNESS, bool? P_ChkDYNAMIC_AIR, bool? P_ChkEXPONENT, bool? P_ChkDIMENSCHANGE
                , bool? P_ChkFLEX_SCOTT, bool? P_ChkBOW, bool? P_ChkSKEW)
            {
                #region ListLAB_SEARCHLABENTRYPRODUCTION

                PARTNO = P_PARTNO;

                ITM_CODE = P_ITM_CODE;
                ITM_CODE_B = P_ITM_CODE_B;
                ITM_CODE_H = P_ITM_CODE_H;

                WEAVINGLOT = P_WEAVINGLOT;
                FINISHINGLOT = P_FINISHINGLOT;
                CUSTOMERID = P_CUSTOMERID;
                FINISHINGPROCESS = P_FINISHINGPROCESS;
                ITEMLOT = P_ITEMLOT;
                LOOMNO = P_LOOMNO;
                FINISHINGMC = P_FINISHINGMC;
                BATCHNO = P_BATCHNO;
                ENTRYDATE = P_ENTRYDATE;

                NUMTHREADS_W_AVG = P_NUMTHREADS_W_AVG;
                NUMTHREADS_W_JUD = P_NUMTHREADS_W_JUD;

                NUMTHREADS_F_AVG = P_NUMTHREADS_F_AVG;
                NUMTHREADS_F_JUD = P_NUMTHREADS_F_JUD;

                USABLE_WIDTH_AVG = P_USABLE_WIDTH_AVG;
                USABLE_WIDTH_JUD = P_USABLE_WIDTH_JUD;

                WIDTH_SILICONE_AVG = P_WIDTH_SILICONE_AVG;
                WIDTH_SILICONE_JUD = P_WIDTH_SILICONE_JUD;

                TOTALWEIGHT_AVG = P_TOTALWEIGHT_AVG;
                TOTALWEIGHT_JUD = P_TOTALWEIGHT_JUD;

                UNCOATEDWEIGHT_AVG = P_UNCOATEDWEIGHT_AVG;
                UNCOATEDWEIGHT_JUD = P_UNCOATEDWEIGHT_JUD;

                COATINGWEIGHT_AVG = P_COATINGWEIGHT_AVG;
                COATINGWEIGHT_JUD = P_COATINGWEIGHT_JUD;

                MAXFORCE_W_AVG = P_MAXFORCE_W_AVG;
                MAXFORCE_W_JUD = P_MAXFORCE_W_JUD;

                MAXFORCE_F_AVG = P_MAXFORCE_F_AVG;
                MAXFORCE_F_JUD = P_MAXFORCE_F_JUD;

                ELONGATIONFORCE_W_AVG = P_ELONGATIONFORCE_W_AVG;
                ELONGATIONFORCE_W_JUD = P_ELONGATIONFORCE_W_JUD;

                ELONGATIONFORCE_F_AVG = P_ELONGATIONFORCE_F_AVG;
                ELONGATIONFORCE_F_JUD = P_ELONGATIONFORCE_F_JUD;

                FLAMMABILITY_W = P_FLAMMABILITY_W;
                FLAMMABILITY_W_JUD = P_FLAMMABILITY_W_JUD;

                FLAMMABILITY_F = P_FLAMMABILITY_F;
                FLAMMABILITY_F_JUD = P_FLAMMABILITY_F_JUD;

                EDGECOMB_W_AVG = P_EDGECOMB_W_AVG;
                EDGECOMB_W_JUD = P_EDGECOMB_W_JUD;

                EDGECOMB_F_AVG = P_EDGECOMB_F_AVG;
                EDGECOMB_F_JUD = P_EDGECOMB_F_JUD;

                STIFFNESS_W_AVG = P_STIFFNESS_W_AVG;
                STIFFNESS_W_JUD = P_STIFFNESS_W_JUD;

                STIFFNESS_F_AVG = P_STIFFNESS_F_AVG;
                STIFFNESS_F_JUD = P_STIFFNESS_F_JUD;

                TEAR_W_AVG = P_TEAR_W_AVG;
                TEAR_W_JUD = P_TEAR_W_JUD;

                TEAR_F_AVG = P_TEAR_F_AVG;
                TEAR_F_JUD = P_TEAR_F_JUD;

                STATIC_AIR_AVG = P_STATIC_AIR_AVG;
                STATIC_AIR_JUD = P_STATIC_AIR_JUD;

                FLEXABRASION_W_AVG = P_FLEXABRASION_W_AVG;
                FLEXABRASION_W_JUD = P_FLEXABRASION_W_JUD;

                FLEXABRASION_F_AVG = P_FLEXABRASION_F_AVG;
                FLEXABRASION_F_JUD = P_FLEXABRASION_F_JUD;

                DIMENSCHANGE_W_AVG = P_DIMENSCHANGE_W_AVG;
                DIMENSCHANGE_W_JUD = P_DIMENSCHANGE_W_JUD;

                DIMENSCHANGE_F_AVG = P_DIMENSCHANGE_F_AVG;
                DIMENSCHANGE_F_JUD = P_DIMENSCHANGE_F_JUD;

                JUDGEMENT = P_JUDGEMENT;

                #endregion

                #region LAB_GETITEMTESTSPECIFICATION

                NUMTHREADS_W_Min = P_NUMTHREADS_W_Min;
                NUMTHREADS_W_Max = P_NUMTHREADS_W_Max;

                NUMTHREADS_F_Min = P_NUMTHREADS_F_Min;
                NUMTHREADS_F_Max = P_NUMTHREADS_F_Max;

                USABLE_WIDTH_Min = P_USABLE_WIDTH_Min;
                WIDTH_SILICONE_Min = P_WIDTH_SILICONE_Min;

                TOTALWEIGHT_Min = P_TOTALWEIGHT_Min;
                TOTALWEIGHT_Max = P_TOTALWEIGHT_Max;

                UNCOATEDWEIGHT_Min = P_UNCOATEDWEIGHT_Min;
                UNCOATEDWEIGHT_Max = P_UNCOATEDWEIGHT_Max;

                COATINGWEIGHT_Min = P_COATINGWEIGHT_Min;
                COATINGWEIGHT_Max = P_COATINGWEIGHT_Max;

                MAXFORCE_W_Min = P_MAXFORCE_W_Min;
                MAXFORCE_F_Min = P_MAXFORCE_F_Min;

                ELONGATIONFORCE_W_TOR = P_ELONGATIONFORCE_W_TOR;
                ELONGATIONFORCE_W_Min = P_ELONGATIONFORCE_W_Min;
                ELONGATIONFORCE_W_Max = P_ELONGATIONFORCE_W_Max;

                ELONGATIONFORCE_F_TOR = P_ELONGATIONFORCE_F_TOR;
                ELONGATIONFORCE_F_Min = P_ELONGATIONFORCE_F_Min;
                ELONGATIONFORCE_F_Max = P_ELONGATIONFORCE_F_Max;

                FLAMMABILITY_W_Max = P_FLAMMABILITY_W_Max;
                FLAMMABILITY_F_Max = P_FLAMMABILITY_F_Max;

                EDGECOMB_W_Min = P_EDGECOMB_W_Min;
                EDGECOMB_F_Min = P_EDGECOMB_F_Min;

                STIFFNESS_W_Max = P_STIFFNESS_W_Max;
                STIFFNESS_F_Max = P_STIFFNESS_F_Max;

                TEAR_W_Min = P_TEAR_W_Min;
                TEAR_F_Min = P_TEAR_F_Min;

                STATIC_AIR_Max = P_STATIC_AIR_Max;

                FLEXABRASION_W_Min = P_FLEXABRASION_W_Min;
                FLEXABRASION_F_Min = P_FLEXABRASION_F_Min;

                DIMENSCHANGE_W_TOR = P_DIMENSCHANGE_W_TOR;
                DIMENSCHANGE_W_Min = P_DIMENSCHANGE_W_Min;
                DIMENSCHANGE_W_Max = P_DIMENSCHANGE_W_Max;

                DIMENSCHANGE_F_TOR = P_DIMENSCHANGE_F_TOR;
                DIMENSCHANGE_F_Min = P_DIMENSCHANGE_F_Min;
                DIMENSCHANGE_F_Max = P_DIMENSCHANGE_F_Max;

                NUMTHREADS_W_Spe = P_NUMTHREADS_W_Spe;
                NUMTHREADS_F_Spe = P_NUMTHREADS_F_Spe;

                USABLE_Spe = P_USABLE_Spe;
                WIDTH_SILICONE_Spe = P_WIDTH_SILICONE_Spe;

                TOTALWEIGHT_Spe = P_TOTALWEIGHT_Spe;
                UNCOATEDWEIGHT_Spe = P_UNCOATEDWEIGHT_Spe;
                COATINGWEIGHT_Spe = P_COATINGWEIGHT_Spe;
                MAXFORCE_W_Spe = P_MAXFORCE_W_Spe;
                MAXFORCE_F_Spe = P_MAXFORCE_F_Spe;
                ELONGATIONFORCE_W_Spe = P_ELONGATIONFORCE_W_Spe;
                ELONGATIONFORCE_F_Spe = P_ELONGATIONFORCE_F_Spe;
                FLAMMABILITY_W_Spe = P_FLAMMABILITY_W_Spe;
                FLAMMABILITY_F_Spe = P_FLAMMABILITY_F_Spe;

                EDGECOMB_W_Spe = P_EDGECOMB_W_Spe;
                EDGECOMB_F_Spe = P_EDGECOMB_F_Spe;
                STIFFNESS_W_Spe = P_STIFFNESS_W_Spe;
                STIFFNESS_F_Spe = P_STIFFNESS_F_Spe;
                TEAR_W_Spe = P_TEAR_W_Spe;
                TEAR_F_Spe = P_TEAR_F_Spe;
                STATIC_AIR_Spe = P_STATIC_AIR_Spe;
                FLEXABRASION_W_Spe = P_FLEXABRASION_W_Spe;
                FLEXABRASION_F_Spe = P_FLEXABRASION_F_Spe;
                DIMENSCHANGE_W_Spe = P_DIMENSCHANGE_W_Spe;
                DIMENSCHANGE_F_Spe = P_DIMENSCHANGE_F_Spe;

                #endregion

                #region LAB_GETREPORTINFO

                REPORT_ID = P_REPORT_ID;
                REVESION = P_REVESION;
                USABLE_WIDTH = P_USABLE_WIDTH;
                NUMTHREADS = P_NUMTHREADS;
                WEIGHT = P_WEIGHT;
                FLAMMABILITY = P_FLAMMABILITY;
                EDGECOMB = P_EDGECOMB;
                STIFFNESS = P_STIFFNESS;
                TEAR = P_TEAR;
                STATIC_AIR = P_STATIC_AIR;
                FLEXABRASION = P_FLEXABRASION;
                DIMENSCHANGE = P_DIMENSCHANGE;
                EFFECTIVE_DATE = P_EFFECTIVE_DATE;

                // ปรับเพิ่ม
                YARNTYPE = P_YARNTYPE;
                COATWEIGHT = P_COATWEIGHT;
                THICKNESS = P_THICKNESS;
                MAXFORCE = P_MAXFORCE;
                ELONGATIONFORCE = P_ELONGATIONFORCE;
                DYNAMIC_AIR = P_DYNAMIC_AIR;
                EXPONENT = P_EXPONENT;

                REPORT_NAME = P_REPORT_NAME;

                BENDING = P_BENDING;

                #endregion

                ChkHide1 = P_ChkHide1;
                ChkHide2 = P_ChkHide2;
                ChkHide3 = P_ChkHide3;
                Chk2C = P_Chk2C;

                ChkSTIFFNESS = P_ChkSTIFFNESS;
                ChkDYNAMIC_AIR = P_ChkDYNAMIC_AIR;
                ChkEXPONENT = P_ChkEXPONENT;
                ChkDIMENSCHANGE = P_ChkDIMENSCHANGE;

                ChkFLEX_SCOTT = P_ChkFLEX_SCOTT;
                ChkBOW = P_ChkBOW;
                ChkSKEW = P_ChkSKEW;

                FILENAME = P_FILENAME;
                UPLOADDATE = P_UPLOADDATE;
                UPLOADBY = P_UPLOADBY;
                ChkCM = P_ChkCM;
                WEAVINGLOTLIST = P_WEAVINGLOTLIST;

                THICKNESS_Spe = P_THICKNESS_Spe;
                BENDING_W_Spe = P_BENDING_W_Spe;
                BENDING_F_Spe = P_BENDING_F_Spe;
                DYNAMIC_AIR_Spe = P_DYNAMIC_AIR_Spe;
                EXPONENT_Spe = P_EXPONENT_Spe;

                THICKNESS_AVG = P_THICKNESS_AVG;
                THICKNESS_JUD = P_THICKNESS_JUD;
                BENDING_W_AVG = P_BENDING_W_AVG;
                BENDING_W_JUD = P_BENDING_W_JUD;
                BENDING_F_AVG = P_BENDING_F_AVG;
                BENDING_F_JUD = P_BENDING_F_JUD;
                DYNAMIC_AIR_AVG = P_DYNAMIC_AIR_AVG;
                DYNAMIC_AIR_JUD = P_DYNAMIC_AIR_JUD;
                EXPONENT_AVG = P_EXPONENT_AVG;
                EXPONENT_JUD = P_EXPONENT_JUD;
            }

            #region ListLAB_SEARCHLABENTRYPRODUCTION

            public string PARTNO { get; set; }
            public string ITM_CODE { get; set; }
            public string ITM_CODE_B { get; set; }
            public string ITM_CODE_H { get; set; }
            public string WEAVINGLOT { get; set; }
            public string FINISHINGLOT { get; set; }
            public string CUSTOMERID { get; set; }
            public string FINISHINGPROCESS { get; set; }
            public string ITEMLOT { get; set; }
            public string LOOMNO { get; set; }
            public string FINISHINGMC { get; set; }
            public string BATCHNO { get; set; }
            public DateTime? ENTRYDATE { get; set; }

            public Decimal? NUMTHREADS_W_AVG { get; set; }
            public string NUMTHREADS_W_JUD { get; set; }

            public Decimal? NUMTHREADS_F_AVG { get; set; }
            public string NUMTHREADS_F_JUD { get; set; }

            public Decimal? USABLE_WIDTH_AVG { get; set; }
            public string USABLE_WIDTH_JUD { get; set; }

            public Decimal? WIDTH_SILICONE_AVG { get; set; }
            public string WIDTH_SILICONE_JUD { get; set; }

            public Decimal? TOTALWEIGHT_AVG { get; set; }
            public string TOTALWEIGHT_JUD { get; set; }

            public Decimal? UNCOATEDWEIGHT_AVG { get; set; }
            public string UNCOATEDWEIGHT_JUD { get; set; }

            public Decimal? COATINGWEIGHT_AVG { get; set; }
            public string COATINGWEIGHT_JUD { get; set; }

            public Decimal? MAXFORCE_W_AVG { get; set; }
            public string MAXFORCE_W_JUD { get; set; }

            public Decimal? MAXFORCE_F_AVG { get; set; }
            public string MAXFORCE_F_JUD { get; set; }

            public Decimal? ELONGATIONFORCE_W_AVG { get; set; }
            public string ELONGATIONFORCE_W_JUD { get; set; }

            public Decimal? ELONGATIONFORCE_F_AVG { get; set; }
            public string ELONGATIONFORCE_F_JUD { get; set; }

            public Decimal? FLAMMABILITY_W { get; set; }
            public string FLAMMABILITY_W_JUD { get; set; }

            public Decimal? FLAMMABILITY_F { get; set; }
            public string FLAMMABILITY_F_JUD { get; set; }


            public Decimal? EDGECOMB_W_AVG { get; set; }
            public string EDGECOMB_W_JUD { get; set; }

            public Decimal? EDGECOMB_F_AVG { get; set; }
            public string EDGECOMB_F_JUD { get; set; }

            public Decimal? STIFFNESS_W_AVG { get; set; }
            public string STIFFNESS_W_JUD { get; set; }

            public Decimal? STIFFNESS_F_AVG { get; set; }
            public string STIFFNESS_F_JUD { get; set; }

            public Decimal? TEAR_W_AVG { get; set; }
            public string TEAR_W_JUD { get; set; }

            public Decimal? TEAR_F_AVG { get; set; }
            public string TEAR_F_JUD { get; set; }

            public Decimal? STATIC_AIR_AVG { get; set; }
            public string STATIC_AIR_JUD { get; set; }

            public Decimal? FLEXABRASION_W_AVG { get; set; }
            public string FLEXABRASION_W_JUD { get; set; }

            public Decimal? FLEXABRASION_F_AVG { get; set; }
            public string FLEXABRASION_F_JUD { get; set; }

            public Decimal? DIMENSCHANGE_W_AVG { get; set; }
            public string DIMENSCHANGE_W_JUD { get; set; }

            public Decimal? DIMENSCHANGE_F_AVG { get; set; }
            public string DIMENSCHANGE_F_JUD { get; set; }

            public string JUDGEMENT { get; set; }

            public bool? ChkHide1 { get; set; }
            public bool? ChkHide2 { get; set; }
            public bool? ChkHide3 { get; set; }
            public bool? Chk2C { get; set; }

            public string FILENAME { get; set; }
            public DateTime? UPLOADDATE { get; set; }
            public string UPLOADBY { get; set; }
            public bool? ChkCM { get; set; }
            public string WEAVINGLOTLIST { get; set; }

            #endregion

            #region LAB_GETITEMTESTSPECIFICATION

            public decimal? NUMTHREADS_W { get; set; }
            public decimal? NUMTHREADS_W_TOR { get; set; }
            public decimal? NUMTHREADS_W_Min { get; set; }
            public decimal? NUMTHREADS_W_Max { get; set; }

            public decimal? NUMTHREADS_F { get; set; }
            public decimal? NUMTHREADS_F_TOR { get; set; }
            public decimal? NUMTHREADS_F_Min { get; set; }
            public decimal? NUMTHREADS_F_Max { get; set; }

            public decimal? USABLE_WIDTH_Cal { get; set; }
            public string USABLE_WIDTH_TOR { get; set; }
            public decimal? USABLE_WIDTH_Min { get; set; }

            public decimal? WIDTH_SILICONE_Cal { get; set; }
            public string WIDTH_SILICONE_TOR { get; set; }
            public decimal? WIDTH_SILICONE_Min { get; set; }

            public decimal? TOTALWEIGHT { get; set; }
            public decimal? TOTALWEIGHT_TOR { get; set; }
            public decimal? TOTALWEIGHT_Min { get; set; }
            public decimal? TOTALWEIGHT_Max { get; set; }

            public decimal? UNCOATEDWEIGHT { get; set; }
            public decimal? UNCOATEDWEIGHT_TOR { get; set; }
            public decimal? UNCOATEDWEIGHT_Min { get; set; }
            public decimal? UNCOATEDWEIGHT_Max { get; set; }

            public decimal? COATINGWEIGHT { get; set; }
            public decimal? COATINGWEIGHT_TOR { get; set; }
            public decimal? COATINGWEIGHT_Min { get; set; }
            public decimal? COATINGWEIGHT_Max { get; set; }

            public decimal? MAXFORCE_W { get; set; }
            public string MAXFORCE_W_TOR { get; set; }
            public decimal? MAXFORCE_W_Min { get; set; }

            public decimal? MAXFORCE_F { get; set; }
            public string MAXFORCE_F_TOR { get; set; }
            public decimal? MAXFORCE_F_Min { get; set; }

            public decimal? ELONGATIONFORCE_W { get; set; }
            public string ELONGATIONFORCE_W_TOR { get; set; }
            public decimal? ELONGATIONFORCE_W_Min { get; set; }
            public decimal? ELONGATIONFORCE_W_Max { get; set; }

            public decimal? ELONGATIONFORCE_F { get; set; }
            public string ELONGATIONFORCE_F_TOR { get; set; }
            public decimal? ELONGATIONFORCE_F_Min { get; set; }
            public decimal? ELONGATIONFORCE_F_Max { get; set; }

            public decimal? FLAMMABILITY_W_Cal { get; set; }
            public string FLAMMABILITY_W_TOR { get; set; }
            public decimal? FLAMMABILITY_W_Max { get; set; }

            public decimal? FLAMMABILITY_F_Cal { get; set; }
            public string FLAMMABILITY_F_TOR { get; set; }
            public decimal? FLAMMABILITY_F_Max { get; set; }

            public decimal? EDGECOMB_W { get; set; }
            public string EDGECOMB_W_TOR { get; set; }
            public decimal? EDGECOMB_W_Min { get; set; }

            public decimal? EDGECOMB_F { get; set; }
            public string EDGECOMB_F_TOR { get; set; }
            public decimal? EDGECOMB_F_Min { get; set; }

            public decimal? STIFFNESS_W { get; set; }
            public string STIFFNESS_W_TOR { get; set; }
            public decimal? STIFFNESS_W_Max { get; set; }

            public decimal? STIFFNESS_F { get; set; }
            public string STIFFNESS_F_TOR { get; set; }
            public decimal? STIFFNESS_F_Max { get; set; }

            public decimal? TEAR_W { get; set; }
            public string TEAR_W_TOR { get; set; }
            public decimal? TEAR_W_Min { get; set; }

            public decimal? TEAR_F { get; set; }
            public string TEAR_F_TOR { get; set; }
            public decimal? TEAR_F_Min { get; set; }

            public decimal? STATIC_AIR_Cal { get; set; }
            public string STATIC_AIR_TOR { get; set; }
            public decimal? STATIC_AIR_Max { get; set; }

            public decimal? FLEXABRASION_W { get; set; }
            public string FLEXABRASION_W_TOR { get; set; }
            public decimal? FLEXABRASION_W_Min { get; set; }

            public decimal? FLEXABRASION_F { get; set; }
            public string FLEXABRASION_F_TOR { get; set; }
            public decimal? FLEXABRASION_F_Min { get; set; }

            public decimal? DIMENSCHANGE_W { get; set; }
            public string DIMENSCHANGE_W_TOR { get; set; }
            public decimal? DIMENSCHANGE_W_Min { get; set; }
            public decimal? DIMENSCHANGE_W_Max { get; set; }

            public decimal? DIMENSCHANGE_F { get; set; }
            public string DIMENSCHANGE_F_TOR { get; set; }
            public decimal? DIMENSCHANGE_F_Min { get; set; }
            public decimal? DIMENSCHANGE_F_Max { get; set; }

            // ตัวแปรเอาไว้ใส่ค่าที่ได้จาก Database //
            public string NUMTHREADS_W_Spe { get; set; }
            public string NUMTHREADS_F_Spe { get; set; }

            public string USABLE_Spe { get; set; }
            public string WIDTH_SILICONE_Spe { get; set; }

            public string TOTALWEIGHT_Spe { get; set; }
            public string UNCOATEDWEIGHT_Spe { get; set; }
            public string COATINGWEIGHT_Spe { get; set; }
            public string MAXFORCE_W_Spe { get; set; }
            public string MAXFORCE_F_Spe { get; set; }
            public string ELONGATIONFORCE_W_Spe { get; set; }
            public string ELONGATIONFORCE_F_Spe { get; set; }
            public string FLAMMABILITY_W_Spe { get; set; }
            public string FLAMMABILITY_F_Spe { get; set; }

            public string EDGECOMB_W_Spe { get; set; }
            public string EDGECOMB_F_Spe { get; set; }
            public string STIFFNESS_W_Spe { get; set; }
            public string STIFFNESS_F_Spe { get; set; }
            public string TEAR_W_Spe { get; set; }
            public string TEAR_F_Spe { get; set; }
            public string STATIC_AIR_Spe { get; set; }
            public string FLEXABRASION_W_Spe { get; set; }
            public string FLEXABRASION_F_Spe { get; set; }
            public string DIMENSCHANGE_W_Spe { get; set; }
            public string DIMENSCHANGE_F_Spe { get; set; }

            //Update 28/10/20
            public string THICKNESS_Spe { get; set; }
            public string BENDING_W_Spe { get; set; }
            public string BENDING_F_Spe { get; set; }
            public string DYNAMIC_AIR_Spe { get; set; }
            public string EXPONENT_Spe { get; set; }

            public decimal? THICKNESS_AVG { get; set; }
            public string THICKNESS_JUD { get; set; }
            public decimal? BENDING_W_AVG { get; set; }
            public string BENDING_W_JUD { get; set; }
            public decimal? BENDING_F_AVG { get; set; }
            public string BENDING_F_JUD { get; set; }

            public decimal? DYNAMIC_AIR_AVG { get; set; }
            public string DYNAMIC_AIR_JUD { get; set; }
            public decimal? EXPONENT_AVG { get; set; }
            public string EXPONENT_JUD { get; set; }

            #endregion

            #region LAB_GETREPORTINFO

            public string REPORT_ID { get; set; }
            public string REVESION { get; set; }
            public string USABLE_WIDTH { get; set; }
            public string NUMTHREADS { get; set; }
            public string WEIGHT { get; set; }
            public string FLAMMABILITY { get; set; }
            public string EDGECOMB { get; set; }
            public string STIFFNESS { get; set; }
            public string TEAR { get; set; }
            public string STATIC_AIR { get; set; }
            public string FLEXABRASION { get; set; }
            public string DIMENSCHANGE { get; set; }
            public DateTime? EFFECTIVE_DATE { get; set; }


            // ปรับเพิ่ม
            public string YARNTYPE { get; set; }
            public string COATWEIGHT { get; set; }
            public string THICKNESS { get; set; }
            public string MAXFORCE { get; set; }
            public string ELONGATIONFORCE { get; set; }
            public string DYNAMIC_AIR { get; set; }
            public string EXPONENT { get; set; }

            public string REPORT_NAME { get; set; }
            public string BENDING { get; set; }

            #endregion

            #region New 31/08/21
            public Decimal? BOW_AVG { get; set; }
            public string BOW_JUD { get; set; }
            public string BOW_Spe { get; set; }
            public string BOW { get; set; }
            public string BOW_TOR { get; set; }
            public decimal? BOW_Min { get; set; }
            public decimal? BOW_Max { get; set; }

            public Decimal? SKEW_AVG { get; set; }
            public string SKEW_JUD { get; set; }
            public string SKEW_Spe { get; set; }
            public string SKEW { get; set; }
            public string SKEW_TOR { get; set; }
            public decimal? SKEW_Min { get; set; }
            public decimal? SKEW_Max { get; set; }

            public string FLEX_SCOTT { get; set; }
            public Decimal? FLEX_SCOTT_F_AVG { get; set; }
            public string FLEX_SCOTT_F_JUD { get; set; }
            public string FLEX_SCOTT_F_JUD2 { get; set; }
            public string FLEX_SCOTT_F_Spe { get; set; }  
            public string FLEX_SCOTT_F_TOR { get; set; }
            public decimal? FLEX_SCOTT_F_Min { get; set; }
            public decimal? FLEX_SCOTT_F_Max { get; set; }

            public Decimal? FLEX_SCOTT_W_AVG { get; set; }
            public string FLEX_SCOTT_W_JUD { get; set; }
            public string FLEX_SCOTT_W_JUD2 { get; set; }
            public string FLEX_SCOTT_W_Spe { get; set; }
            public string FLEX_SCOTT_W_TOR { get; set; }
            public decimal? FLEX_SCOTT_W_Min { get; set; }
            public decimal? FLEX_SCOTT_W_Max { get; set; }

            public bool? ChkSTIFFNESS { get; set; }
            public bool? ChkDYNAMIC_AIR { get; set; }
            public bool? ChkEXPONENT { get; set; }
            public bool? ChkDIMENSCHANGE { get; set; }

            public bool? ChkFLEX_SCOTT { get; set; }
            public bool? ChkBOW { get; set; }
            public bool? ChkSKEW { get; set; }
            public bool? ChkTHICKNESS { get; set; }

            public string strUSABLE_WIDTH { get; set; }

            #endregion

            #region Barcode
            // For Barcode
            public byte[] PARTNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.PARTNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.PARTNO,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] ITM_CODEImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.ITM_CODE_B))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.ITM_CODE_B,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] WEAVINGLOTImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.WEAVINGLOT))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.WEAVINGLOT,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] BATCHNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.BATCHNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.BATCHNO,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] FINISHINGLOTImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.FINISHINGLOT))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.FINISHINGLOT,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            #endregion
        }
        #endregion

        #region ListLAB_GETREPORTINFO

        public class ListLAB_GETREPORTINFO
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListLAB_GETREPORTINFO()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListLAB_GETREPORTINFO()
            {
                // default constructor
            }

            public ListLAB_GETREPORTINFO(string P_ITM_CODE,string P_REPORT_ID,string P_REVESION,string P_USABLE_WIDTH, string P_NUMTHREADS,
                                    string P_WEIGHT, string P_FLAMMABILITY, string P_EDGECOMB, string P_STIFFNESS, string P_TEAR,
                                    string P_STATIC_AIR,string P_FLEXABRASION,string P_DIMENSCHANGE,string P_CUSTOMERID, DateTime? P_EFFECTIVE_DATE,
                string P_YARNTYPE, string P_COATWEIGHT, string P_THICKNESS, string P_MAXFORCE, string P_ELONGATIONFORCE, string P_DYNAMIC_AIR, string P_EXPONENT, string P_BENDING)
            {
                #region ListLAB_GETREPORTINFO

                ITM_CODE = P_ITM_CODE;
                REPORT_ID = P_REPORT_ID;
                REVESION = P_REVESION;
                USABLE_WIDTH = P_USABLE_WIDTH;
                NUMTHREADS = P_NUMTHREADS;
                WEIGHT = P_WEIGHT;
                FLAMMABILITY = P_FLAMMABILITY;
                EDGECOMB = P_EDGECOMB;
                STIFFNESS = P_STIFFNESS;
                TEAR = P_TEAR;
                STATIC_AIR = P_STATIC_AIR;
                FLEXABRASION = P_FLEXABRASION;
                DIMENSCHANGE = P_DIMENSCHANGE;
                CUSTOMERID = P_CUSTOMERID;
                EFFECTIVE_DATE = P_EFFECTIVE_DATE;

                // ปรับเพิ่ม
                YARNTYPE = P_YARNTYPE;
                COATWEIGHT = P_COATWEIGHT;
                THICKNESS = P_THICKNESS;
                MAXFORCE = P_MAXFORCE;
                ELONGATIONFORCE = P_ELONGATIONFORCE;
                DYNAMIC_AIR = P_DYNAMIC_AIR;
                EXPONENT = P_EXPONENT;
                BENDING = P_BENDING;

                #endregion
            }

            #region ListLAB_GETREPORTINFO

            public string ITM_CODE { get; set; }
            public string REPORT_ID { get; set; }
            public string REVESION { get; set; }
            public string USABLE_WIDTH { get; set; }
            public string NUMTHREADS { get; set; }
            public string WEIGHT { get; set; }
            public string FLAMMABILITY { get; set; }
            public string EDGECOMB { get; set; }
            public string STIFFNESS { get; set; }
            public string TEAR { get; set; }
            public string STATIC_AIR { get; set; }
            public string FLEXABRASION { get; set; }
            public string DIMENSCHANGE { get; set; }
            public string CUSTOMERID { get; set; }
            public DateTime? EFFECTIVE_DATE { get; set; }

            // ปรับเพิ่ม
            public string YARNTYPE { get; set; }
            public string COATWEIGHT { get; set; }
            public string THICKNESS { get; set; }
            public string MAXFORCE { get; set; }
            public string ELONGATIONFORCE { get; set; }
            public string DYNAMIC_AIR { get; set; }
            public string EXPONENT { get; set; }
            public string BENDING { get; set; }


            // For Barcode
            public byte[] ITM_CODEImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.ITM_CODE))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.ITM_CODE,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            #endregion
        }
        #endregion
    }
}
