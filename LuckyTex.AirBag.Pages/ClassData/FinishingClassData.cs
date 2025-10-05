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
    public class FinishingClassData
    {
        #region Finishing ClassData
        private static FinishingClassData _instance = null;

        public static FinishingClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(FinishingClassData))
                    {
                        _instance = new FinishingClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListScouring

        public class ListScouring
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListScouring()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListScouring()
            {
                // default constructor
            }

            public ListScouring(string itm_Code
                , string weavingLot, string finishingCustomer, DateTime startDate, DateTime endDate, string productTypeID
                , string mcNo, string statusFlag
                , decimal? saturator_Chem_PV, decimal? washing1_PV, decimal? washing2_PV, decimal? hotFlue_PV
                , decimal? temp1_PV, decimal? temp2_PV, decimal? temp3_PV, decimal? temp4_PV
                , decimal? temp5_PV, decimal? temp6_PV, decimal? temp7_PV, decimal? temp8_PV
                , decimal? temp9_PV, decimal? temp10_PV
                , decimal? speed_PV, decimal? mainFrameWidth, decimal? width_BE, decimal? width_AF
                , decimal pin2pin, string itm_WEAVING, string finishingLot
                , decimal? length1, decimal? length2, decimal? length3, decimal? length4
                , decimal? length5, decimal? length6, decimal? length7
                , decimal? inputLength, string finishBy, string partNo
                , decimal? finishLength, string conditionBy, string startBy, string remark
                , decimal? humid_AF, decimal? humid_BF, string operator_group
               , string _REPROCESS ,decimal? _TEMP1_MIN ,decimal? _TEMP1_MAX ,decimal? _TEMP2_MIN ,decimal? _TEMP2_MAX ,
                decimal? _TEMP3_MIN ,decimal? _TEMP3_MAX , decimal? _TEMP4_MIN ,decimal? _TEMP4_MAX ,decimal? _TEMP5_MIN ,decimal? _TEMP5_MAX ,
                decimal? _TEMP6_MIN ,decimal? _TEMP6_MAX ,decimal? _TEMP7_MIN , decimal? _TEMP7_MAX ,decimal? _TEMP8_MIN ,decimal? _TEMP8_MAX ,
                decimal? _TEMP9_MIN, decimal? _TEMP9_MAX, decimal? _TEMP10_MIN, decimal? _TEMP10_MAX,
                decimal? _SAT_CHEM_MIN , decimal? _SAT_CHEM_MAX , decimal? _WASHING1_MIN , decimal? _WASHING1_MAX ,
                decimal? _WASHING2_MIN , decimal? _WASHING2_MAX , decimal? _HOTFLUE_MIN , decimal? _HOTFLUE_MAX ,decimal? _SPEED_MIN ,decimal? _SPEED_MAX)
            {
                #region ListScouring

                ITM_CODE = itm_Code;
                WEAVINGLOT = weavingLot;
                FINISHINGCUSTOMER = finishingCustomer;
                STARTDATE = startDate;
                ENDDATE = endDate;
                PRODUCTTYPEID = productTypeID;
                MCNO = mcNo;
                STATUSFLAG = statusFlag;
                SATURATOR_CHEM_PV = saturator_Chem_PV;
                WASHING1_PV = washing1_PV;
                WASHING2_PV = washing2_PV;
                HOTFLUE_PV = hotFlue_PV;
                TEMP1_PV = temp1_PV;
                TEMP2_PV = temp2_PV;
                TEMP3_PV = temp3_PV;
                TEMP4_PV = temp4_PV;
                TEMP5_PV = temp5_PV;
                TEMP6_PV = temp6_PV;
                TEMP7_PV = temp7_PV;
                TEMP8_PV = temp8_PV;

                TEMP9_PV = temp9_PV;
                TEMP10_PV = temp10_PV;

                SPEED_PV = speed_PV;
                MAINFRAMEWIDTH = mainFrameWidth;
                WIDTH_BE = width_BE;
                WIDTH_AF = width_AF;
                PIN2PIN = pin2pin;
                ITM_WEAVING = itm_WEAVING;
                FINISHINGLOT = finishingLot;
                LENGTH1 = length1;
                LENGTH2 = length2;
                LENGTH3 = length3;
                LENGTH4 = length4;
                LENGTH5 = length5;
                LENGTH6 = length6;
                LENGTH7 = length7;
                INPUTLENGTH = inputLength;
                FINISHBY = finishBy;
                PARTNO = partNo;
                FINISHLENGTH = finishLength;

                CONDITIONBY = conditionBy;
                STARTBY = startBy;
                REMARK = remark;

                HUMID_AF = humid_AF;
                HUMID_BF = humid_BF;

                OPERATOR_GROUP = operator_group;

                REPROCESS = _REPROCESS;
                TEMP1_MIN = _TEMP1_MIN;
                TEMP1_MAX = _TEMP1_MAX;
                TEMP2_MIN = _TEMP2_MIN;
                TEMP2_MAX = _TEMP2_MAX;
                TEMP3_MIN = _TEMP3_MIN;
                TEMP3_MAX = _TEMP3_MAX;
                TEMP4_MIN = _TEMP4_MIN;
                TEMP4_MAX = _TEMP4_MAX;
                TEMP5_MIN = _TEMP5_MIN;
                TEMP5_MAX = _TEMP5_MAX;
                TEMP6_MIN = _TEMP6_MIN;
                TEMP6_MAX = _TEMP6_MAX;
                TEMP7_MIN = _TEMP7_MIN;
                TEMP7_MAX = _TEMP7_MAX;
                TEMP8_MIN = _TEMP8_MIN;
                TEMP8_MAX = _TEMP8_MAX;

                TEMP9_MIN = _TEMP9_MIN;
                TEMP9_MAX = _TEMP9_MAX;
                TEMP10_MIN = _TEMP10_MIN;
                TEMP10_MAX = _TEMP10_MAX;

                SAT_CHEM_MIN = _SAT_CHEM_MIN;
                SAT_CHEM_MAX = _SAT_CHEM_MAX;
                WASHING1_MIN = _WASHING1_MIN;
                WASHING1_MAX = _WASHING1_MAX;
                WASHING2_MIN = _WASHING2_MIN;
                WASHING2_MAX = _WASHING2_MAX;
                HOTFLUE_MIN = _HOTFLUE_MIN;
                HOTFLUE_MAX = _HOTFLUE_MAX;
                SPEED_MIN = _SPEED_MIN;
                SPEED_MAX = _SPEED_MAX;
        
                #endregion
            }

            #region Scouring
          
            public string ITM_CODE { get; set; }
            public string WEAVINGLOT { get; set; }
            public string FINISHINGCUSTOMER { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public string PRODUCTTYPEID { get; set; }
            public string MCNO { get; set; }
            public string STATUSFLAG { get; set; }
            public decimal? SATURATOR_CHEM_PV { get; set; }
            public decimal? WASHING1_PV { get; set; }
            public decimal? WASHING2_PV { get; set; }
            public decimal? HOTFLUE_PV { get; set; }
            public decimal? TEMP1_PV { get; set; }
            public decimal? TEMP2_PV { get; set; }
            public decimal? TEMP3_PV { get; set; }
            public decimal? TEMP4_PV { get; set; }
            public decimal? TEMP5_PV { get; set; }
            public decimal? TEMP6_PV { get; set; }
            public decimal? TEMP7_PV { get; set; }
            public decimal? TEMP8_PV { get; set; }

            public decimal? TEMP9_PV { get; set; }
            public decimal? TEMP10_PV { get; set; }

            public decimal? SPEED_PV { get; set; }
            public decimal? MAINFRAMEWIDTH { get; set; }
            public decimal? WIDTH_BE { get; set; }
            public decimal? WIDTH_AF { get; set; }
            public decimal? PIN2PIN { get; set; }
            public string ITM_WEAVING { get; set; }
            public string FINISHINGLOT { get; set; }

            public decimal? LENGTH1 { get; set; }
            public decimal? LENGTH2 { get; set; }
            public decimal? LENGTH3 { get; set; }
            public decimal? LENGTH4 { get; set; }
            public decimal? LENGTH5 { get; set; }
            public decimal? LENGTH6 { get; set; }
            public decimal? LENGTH7 { get; set; }
            public decimal? INPUTLENGTH { get; set; }
            public string FINISHBY { get; set; }
            public string PARTNO { get; set; }
            public decimal? FINISHLENGTH { get; set; }

            public string CONDITIONBY { get; set; }
            public string STARTBY { get; set; }
            public string REMARK { get; set; }

            public decimal? HUMID_AF { get; set; }
            public decimal? HUMID_BF { get; set; }

            public string OPERATOR_GROUP { get; set; }

            public string REPROCESS { get; set; }
            public decimal? TEMP1_MIN { get; set; }
            public decimal? TEMP1_MAX { get; set; }
            public decimal? TEMP2_MIN { get; set; }
            public decimal? TEMP2_MAX { get; set; }
            public decimal? TEMP3_MIN { get; set; }
            public decimal? TEMP3_MAX { get; set; }
            public decimal? TEMP4_MIN { get; set; }
            public decimal? TEMP4_MAX { get; set; }
            public decimal? TEMP5_MIN { get; set; }
            public decimal? TEMP5_MAX { get; set; }
            public decimal? TEMP6_MIN { get; set; }
            public decimal? TEMP6_MAX { get; set; }
            public decimal? TEMP7_MIN { get; set; }
            public decimal? TEMP7_MAX { get; set; }
            public decimal? TEMP8_MIN { get; set; }
            public decimal? TEMP8_MAX { get; set; }

            public decimal? TEMP9_MIN { get; set; }
            public decimal? TEMP9_MAX { get; set; }
            public decimal? TEMP10_MIN { get; set; }
            public decimal? TEMP10_MAX { get; set; }

            public decimal? SAT_CHEM_MIN { get; set; }
            public decimal? SAT_CHEM_MAX { get; set; }
            public decimal? WASHING1_MIN { get; set; }
            public decimal? WASHING1_MAX { get; set; }
            public decimal? WASHING2_MIN { get; set; }
            public decimal? WASHING2_MAX { get; set; }
            public decimal? HOTFLUE_MIN { get; set; }
            public decimal? HOTFLUE_MAX { get; set; }
            public decimal? SPEED_MIN { get; set; }
            public decimal? SPEED_MAX { get; set; }
        
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

            public byte[] PARTNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.PARTNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.PARTNO,
                            600, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            #endregion
        }

        #endregion

        #region ListScouringCondition

        public class ListScouringCondition
        {
            public ListScouringCondition()
            {
                // default constructor
            }

            public ListScouringCondition(string itm_Code
                , decimal? _SATURATOR_CHEM, decimal? _SATURATOR_CHEM_MARGIN
                , decimal? _WASHING1, decimal? _WASHING1_MARGIN, decimal? _WASHING2, decimal? _WASHING2_MARGIN
                , decimal? _HOTFLUE, decimal? _HOTFLUE_MARGIN, decimal? _ROOMTEMP, decimal? _ROOMTEMP_MARGIN
                , decimal? _SPEED, decimal? _SPEED_MARGIN, decimal? _MAINFRAMEWIDTH, decimal? _MAINFRAMEWIDTH_MARGIN
                , decimal? _WIDTH_BE, decimal? _WIDTH_BE_MARGIN, decimal? _WIDTH_AF, decimal? _WIDTH_AF_MARGIN
                , string _DENSITY_AF, decimal? _DENSITY_MARGIN, string _SCOURINGNO, decimal? _NIPCHEMICAL
                , decimal? _NIPROLLWASHER1, decimal? _NIPROLLWASHER2, decimal? _PIN2PIN, decimal? _PIN2PIN_MARGIN
                , string _SATURATOR_CHEMSpecification, string _WASHING1Specification, string _WASHING2Specification
                , string _HOTFLUESpecification, string _SPEEDSpecification, string _MAINFRAMEWIDTHSpecification
                , string _WIDTH_BESpecification, string _WIDTH_AFSpecification, string _PIN2PINSpecification
                , string _strROOMTEMP, string _HUMIDSpecification)
            {
                #region ListScouringCondition

                ITM_CODE = itm_Code;
                SATURATOR_CHEM = _SATURATOR_CHEM;
                SATURATOR_CHEM_MARGIN = _SATURATOR_CHEM_MARGIN;
                WASHING1 = _WASHING1;
                WASHING1_MARGIN = _WASHING1_MARGIN;
                WASHING2 = _WASHING2;
                WASHING2_MARGIN = _WASHING2_MARGIN;
                HOTFLUE = _HOTFLUE;
                HOTFLUE_MARGIN = _HOTFLUE_MARGIN;
                ROOMTEMP = _ROOMTEMP;
                ROOMTEMP_MARGIN = _ROOMTEMP_MARGIN;
                SPEED = _SPEED;
                SPEED_MARGIN = _SPEED_MARGIN;
                MAINFRAMEWIDTH = _MAINFRAMEWIDTH;
                MAINFRAMEWIDTH_MARGIN = _MAINFRAMEWIDTH_MARGIN;
                WIDTH_BE = _WIDTH_BE;
                WIDTH_BE_MARGIN = _WIDTH_BE_MARGIN;
                WIDTH_AF = _WIDTH_AF;
                WIDTH_AF_MARGIN = _WIDTH_AF_MARGIN;
                DENSITY_AF = _DENSITY_AF;
                DENSITY_MARGIN = _DENSITY_MARGIN;
                SCOURINGNO = _SCOURINGNO;
                NIPCHEMICAL = _NIPCHEMICAL;
                NIPROLLWASHER1 = _NIPROLLWASHER1;
                NIPROLLWASHER2 = _NIPROLLWASHER2;
                PIN2PIN = _PIN2PIN;
                PIN2PIN_MARGIN = _PIN2PIN_MARGIN;

                SATURATOR_CHEMSpecification = _SATURATOR_CHEMSpecification;
                WASHING1Specification = _WASHING1Specification;
                WASHING2Specification = _WASHING2Specification;
                HOTFLUESpecification = _HOTFLUESpecification;
                SPEEDSpecification = _SPEEDSpecification;
                MAINFRAMEWIDTHSpecification = _MAINFRAMEWIDTHSpecification;
                WIDTH_BESpecification = _WIDTH_BESpecification;
                WIDTH_AFSpecification = _WIDTH_AFSpecification;
                PIN2PINSpecification = _PIN2PINSpecification;
                StrROOMTEMP = _strROOMTEMP;
                HUMIDSpecification = _HUMIDSpecification;

                #endregion
            }

            #region Scouring Condition

            public string ITM_CODE { get; set; }
            public decimal? SATURATOR_CHEM { get; set; }
            public decimal? SATURATOR_CHEM_MARGIN { get; set; }
            public decimal? WASHING1 { get; set; }
            public decimal? WASHING1_MARGIN { get; set; }
            public decimal? WASHING2 { get; set; }
            public decimal? WASHING2_MARGIN { get; set; }
            public decimal? HOTFLUE { get; set; }
            public decimal? HOTFLUE_MARGIN { get; set; }
            public decimal? ROOMTEMP { get; set; }
            public decimal? ROOMTEMP_MARGIN { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? SPEED_MARGIN { get; set; }
            public decimal? MAINFRAMEWIDTH { get; set; }
            public decimal? MAINFRAMEWIDTH_MARGIN { get; set; }
            public decimal? WIDTH_BE { get; set; }
            public decimal? WIDTH_BE_MARGIN { get; set; }
            public decimal? WIDTH_AF { get; set; }
            public decimal? WIDTH_AF_MARGIN { get; set; }
            public string DENSITY_AF { get; set; }
            public decimal? DENSITY_MARGIN { get; set; }
            public string SCOURINGNO { get; set; }
            public decimal? NIPCHEMICAL { get; set; }
            public decimal? NIPROLLWASHER1 { get; set; }
            public decimal? NIPROLLWASHER2 { get; set; }
            public decimal? PIN2PIN { get; set; }
            public decimal? PIN2PIN_MARGIN { get; set; }

            public string SATURATOR_CHEMSpecification { get; set; }
            public string WASHING1Specification { get; set; }
            public string WASHING2Specification { get; set; }
            public string HOTFLUESpecification { get; set; }
            public string SPEEDSpecification { get; set; }
            public string MAINFRAMEWIDTHSpecification { get; set; }
            public string WIDTH_BESpecification { get; set; }
            public string WIDTH_AFSpecification { get; set; }
            public string PIN2PINSpecification { get; set; }
            public string StrROOMTEMP { get; set; }
            public string HUMIDSpecification { get; set; }

            #endregion
        }

        #endregion

        #region ListDryer 

        public class ListDryer
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListDryer()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListDryer()
            {
                // default constructor
            }

            public ListDryer(string itm_Code
                , string weavingLot, string finishingCustomer, DateTime startDate, DateTime endDate, string productTypeID
                , string mcNo, string statusFlag
                , decimal? width_BE_HEAT, decimal? accpresure, decimal? asstension, decimal? accaridenser
                , decimal? chifrot, decimal? chirear, decimal? dryerTemp1_PV, decimal? dryerTemp1_SP
                , decimal? speed_PV, decimal? speed_SP, decimal? steamPressure, decimal? dryercircufan
                , decimal exhaustFan, decimal? width_AF_HEAT, string itm_WEAVING, string finishingLot ,string samplingID
                , decimal? length1, decimal? length2, decimal? length3, decimal? length4
                , decimal? length5, decimal? length6, decimal? length7
                , decimal? inputLength, string finishBy, string partNo
                , decimal? finishLength, string conditionBy, string startBy, string remark
                , decimal? humid_AF, decimal? humid_BF, string operator_group
                , decimal? _HOTFLUE_MIN, decimal? _HOTFLUE_MAX, decimal? _SPEED_MIN, decimal? _SPEED_MAX
                , decimal? _SATURATOR_CHEM_PV, decimal? _SATURATOR_CHEM_SP, decimal? _WASHING1_PV, decimal? _WASHING1_SP
                , decimal? _WASHING2_PV, decimal? _WASHING2_SP)
            {
                #region ListDryer

                ITM_CODE = itm_Code;
                WEAVINGLOT = weavingLot;
                FINISHINGCUSTOMER = finishingCustomer;
                STARTDATE = startDate;
                ENDDATE = endDate;
                PRODUCTTYPEID = productTypeID;
                MCNO = mcNo;
                STATUSFLAG = statusFlag;
                WIDTH_BE_HEAT = width_BE_HEAT;
                ACCPRESURE = accpresure;
                ASSTENSION = asstension;
                ACCARIDENSER = accaridenser;
                CHIFROT = chifrot;
                CHIREAR = chirear;
                DRYERTEMP1_PV = dryerTemp1_PV;
                DRYERTEMP1_SP = dryerTemp1_SP;
                SPEED_PV = speed_PV;
                SPEED_SP = speed_SP;
                STEAMPRESSURE = steamPressure;
                DRYERCIRCUFAN = dryercircufan;
                EXHAUSTFAN = exhaustFan;
                WIDTH_AF_HEAT = width_AF_HEAT;
                ITM_WEAVING = itm_WEAVING;
                FINISHINGLOT = finishingLot;
                SAMPLINGID = samplingID;
                LENGTH1 = length1;
                LENGTH2 = length2;
                LENGTH3 = length3;
                LENGTH4 = length4;
                LENGTH5 = length5;
                LENGTH6 = length6;
                LENGTH7 = length7;
                INPUTLENGTH = inputLength;
                FINISHBY = finishBy;
                PARTNO = partNo;
                FINISHLENGTH = finishLength;

                CONDITIONBY = conditionBy;
                STARTBY = startBy;
                REMARK = remark;
                HUMID_AF = humid_AF;
                HUMID_BF = humid_BF;

                OPERATOR_GROUP = operator_group;

                HOTFLUE_MIN = _HOTFLUE_MIN;
                HOTFLUE_MAX = _HOTFLUE_MAX;
                SPEED_MIN = _SPEED_MIN;
                SPEED_MAX = _SPEED_MAX;

                SATURATOR_CHEM_PV = _SATURATOR_CHEM_PV;
                SATURATOR_CHEM_SP = _SATURATOR_CHEM_SP;
                WASHING1_PV = _WASHING1_PV;
                WASHING1_SP = _WASHING1_SP;
                WASHING2_PV = _WASHING2_PV;
                WASHING2_SP = _WASHING2_SP;

                #endregion
            }

            #region Dryer

            public string ITM_CODE { get; set; }
            public string WEAVINGLOT { get; set; }
            public string FINISHINGCUSTOMER { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public string PRODUCTTYPEID { get; set; }
            public string MCNO { get; set; }
            public string STATUSFLAG { get; set; }

            public decimal? WIDTH_BE_HEAT { get; set; }
            public decimal? ACCPRESURE { get; set; }
            public decimal? ASSTENSION { get; set; }
            public decimal? ACCARIDENSER { get; set; }
            public decimal? CHIFROT { get; set; }
            public decimal? CHIREAR { get; set; }
            public decimal? DRYERTEMP1_PV { get; set; }
            public decimal? DRYERTEMP1_SP { get; set; }

            public decimal? SPEED_PV { get; set; }
            public decimal? SPEED_SP { get; set; }
            public decimal? STEAMPRESSURE { get; set; }
            public decimal? DRYERCIRCUFAN { get; set; }
            public decimal? EXHAUSTFAN { get; set; }
            public decimal? WIDTH_AF_HEAT { get; set; }
            public string ITM_WEAVING { get; set; }
            public string FINISHINGLOT { get; set; }
            public string SAMPLINGID { get; set; }

            public decimal? LENGTH1 { get; set; }
            public decimal? LENGTH2 { get; set; }
            public decimal? LENGTH3 { get; set; }
            public decimal? LENGTH4 { get; set; }
            public decimal? LENGTH5 { get; set; }
            public decimal? LENGTH6 { get; set; }
            public decimal? LENGTH7 { get; set; }
            public decimal? INPUTLENGTH { get; set; }
            public string FINISHBY { get; set; }
            public string PARTNO { get; set; }
            public decimal? FINISHLENGTH { get; set; }

            public string CONDITIONBY { get; set; }
            public string STARTBY { get; set; }
            public string REMARK { get; set; }
            public decimal? HUMID_AF { get; set; }
            public decimal? HUMID_BF { get; set; }

            public string OPERATOR_GROUP { get; set; }

            public decimal? HOTFLUE_MIN { get; set; }
            public decimal? HOTFLUE_MAX { get; set; }
            public decimal? SPEED_MIN { get; set; }
            public decimal? SPEED_MAX { get; set; }


            public decimal? SATURATOR_CHEM_PV { get; set; }
            public decimal? SATURATOR_CHEM_SP { get; set; }
            public decimal? WASHING1_PV { get; set; }
            public decimal? WASHING1_SP { get; set; }
            public decimal? WASHING2_PV { get; set; }
            public decimal? WASHING2_SP { get; set; }

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

            public byte[] PARTNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.PARTNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.PARTNO,
                            600, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            #endregion
        }

        #endregion

        #region ListDryerCondition

        public class ListDryerCondition
        {
            public ListDryerCondition()
            {
                // default constructor
            }

            public ListDryerCondition(string itm_Code

                , string _WIDTH_BE_HEAT, string _ACCPRESURE, string _ASSTENSION
                , string _ACCARIDENSER, string _CHIFROT, string _CHIREAR
                , string _DRYERTEMP1, string _SPEED, string _STEAMPRESSURE
                , string _DRYERUPCIRCUFAN, string _EXHAUSTFAN, string _WIDTH_AF_HEAT
                , string _HUMIDSpecification, string _SATURATOR_CHEMSpecification, string _WASHING1Specification, string _WASHING2Specification)
            {
                #region ListDryerCondition

                ITM_CODE = itm_Code;
                WIDTH_BE_HEATSpecification = _WIDTH_BE_HEAT;
                ACCPRESURESpecification = _ACCPRESURE;
                ASSTENSIONSpecification = _ASSTENSION;
                ACCARIDENSERSpecification = _ACCARIDENSER;
                CHIFROTSpecification = _CHIFROT;
                CHIREARSpecification = _CHIREAR;
                DRYERTEMP1Specification = _DRYERTEMP1;
                SPEEDSpecification = _SPEED;
                STEAMPRESSURESpecification = _STEAMPRESSURE;
                DRYERUPCIRCUFANSpecification = _DRYERUPCIRCUFAN;
                EXHAUSTFANSpecification = _EXHAUSTFAN;
                WIDTH_AF_HEATSpecification = _WIDTH_AF_HEAT;
                HUMIDSpecification = _HUMIDSpecification;

                SATURATOR_CHEMSpecification = _SATURATOR_CHEMSpecification;
                WASHING1Specification = _WASHING1Specification;
                WASHING2Specification = _WASHING2Specification;

                #endregion
            }

            #region Dryer Condition

            public string ITM_CODE { get; set; }
            public string WIDTH_BE_HEATSpecification { get; set; }
            public string ACCPRESURESpecification { get; set; }
            public string ASSTENSIONSpecification { get; set; }
            public string ACCARIDENSERSpecification { get; set; }
            public string CHIFROTSpecification { get; set; }
            public string CHIREARSpecification { get; set; }
            public string DRYERTEMP1Specification { get; set; }
            public string SPEEDSpecification { get; set; }
            public string STEAMPRESSURESpecification { get; set; }
            public string DRYERUPCIRCUFANSpecification { get; set; }
            public string EXHAUSTFANSpecification { get; set; }
            public string WIDTH_AF_HEATSpecification { get; set; }
            public string HUMIDSpecification { get; set; }

            public string SATURATOR_CHEMSpecification { get; set; }
            public string WASHING1Specification { get; set; }
            public string WASHING2Specification { get; set; }

            #endregion
        }

        #endregion

        #region ListCoating 

        public class ListCoating
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListCoating()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListCoating()
            {
                // default constructor
            }

            public ListCoating(string itm_Code
                , string weavingLot, string finishingCustomer, DateTime startDate, DateTime endDate, string productTypeID
                , string mcNo, string statusFlag
                , decimal? saturator_Chem_PV, decimal? washing1_PV, decimal? washing2_PV, decimal? hotFlue_PV, decimal? be_coatwidth
                , decimal? temp1_PV, decimal? temp2_PV, decimal? temp3_PV, decimal? temp4_PV
                , decimal? temp5_PV, decimal? temp6_PV, decimal? temp7_PV, decimal? temp8_PV, decimal? temp9_PV, decimal? temp10_PV
                , decimal? _FANRPM, decimal? _EXFAN_FRONT_BACK, decimal? _EXFAN_MIDDLE, decimal? _ANGLEKNIFE, string _BLADENO
                , string _BLADEDIRECTION, decimal? _CYLINDER_TENSIONUP, decimal? _OPOLE_TENSIONDOWN, decimal? _FRAMEWIDTH_FORN
                , decimal? _FRAMEWIDTH_TENTER, decimal? _PATHLINE, decimal? _FEEDIN, decimal? _OVERFEED
                , decimal? speed_PV, decimal? _WIDTHCOAT, decimal? _WIDTHCOATALL, string _SILICONE_A
                , string _SILICONE_B, decimal? _COATINGWEIGTH_L, decimal? _COATINGWEIGTH_C, decimal? _COATINGWEIGTH_R
                , string itm_WEAVING, string finishingLot
                , decimal? length1, decimal? length2, decimal? length3, decimal? length4
                , decimal? length5, decimal? length6, decimal? length7
                , decimal? inputLength, string finishBy, string partNo
                , decimal? finishLength, string samplingid, string conditionBy, string startBy, string remark
                , decimal? humid_AF, decimal? humid_BF, string operator_group
                , string _REPROCESS, decimal? _TEMP1_MIN, decimal? _TEMP1_MAX, decimal? _TEMP2_MIN, decimal? _TEMP2_MAX,
                 decimal? _TEMP3_MIN, decimal? _TEMP3_MAX, decimal? _TEMP4_MIN, decimal? _TEMP4_MAX,
                 decimal? _TEMP5_MIN, decimal? _TEMP5_MAX, decimal? _TEMP6_MIN, decimal? _TEMP6_MAX,
                 decimal? _TEMP7_MIN, decimal? _TEMP7_MAX, decimal? _TEMP8_MIN, decimal? _TEMP8_MAX,
                 decimal? _TEMP9_MIN, decimal? _TEMP9_MAX, decimal? _TEMP10_MIN, decimal? _TEMP10_MAX,
                 decimal? _SAT_CHEM_MIN, decimal? _SAT_CHEM_MAX, decimal? _WASHING1_MIN, decimal? _WASHING1_MAX,
                 decimal? _WASHING2_MIN, decimal? _WASHING2_MAX, decimal? _HOTFLUE_MIN, decimal? _HOTFLUE_MAX,
                 decimal? _SPEED_MIN, decimal? _SPEED_MAX, decimal? _TENSIONUP_MIN, decimal? _TENSIONUP_MAX,
                 decimal? _TENSIONDOWN_MIN, decimal? _TENSIONDOWN_MAX)
            {
                #region ListCoating

                ITM_CODE = itm_Code;
                WEAVINGLOT = weavingLot;
                FINISHINGCUSTOMER = finishingCustomer;
                STARTDATE = startDate;
                ENDDATE = endDate;
                PRODUCTTYPEID = productTypeID;
                MCNO = mcNo;
                STATUSFLAG = statusFlag;
                SATURATOR_CHEM_PV = saturator_Chem_PV;
                WASHING1_PV = washing1_PV;
                WASHING2_PV = washing2_PV;
                HOTFLUE_PV = hotFlue_PV;
                BE_COATWIDTH = be_coatwidth;

                TEMP1_PV = temp1_PV;
                TEMP2_PV = temp2_PV;
                TEMP3_PV = temp3_PV;
                TEMP4_PV = temp4_PV;
                TEMP5_PV = temp5_PV;
                TEMP6_PV = temp6_PV;
                TEMP7_PV = temp7_PV;
                TEMP8_PV = temp8_PV;
                TEMP9_PV = temp9_PV;
                TEMP10_PV = temp10_PV;

                FANRPM = _FANRPM;
                EXFAN_FRONT_BACK = _EXFAN_FRONT_BACK;
                EXFAN_MIDDLE = _EXFAN_MIDDLE;
                ANGLEKNIFE = _ANGLEKNIFE;
                BLADENO = _BLADENO;
                BLADEDIRECTION = _BLADEDIRECTION;
                CYLINDER_TENSIONUP = _CYLINDER_TENSIONUP;
                OPOLE_TENSIONDOWN = _OPOLE_TENSIONDOWN;
                FRAMEWIDTH_FORN = _FRAMEWIDTH_FORN;
                FRAMEWIDTH_TENTER = _FRAMEWIDTH_TENTER;
                PATHLINE = _PATHLINE;
                FEEDIN = _FEEDIN;
                OVERFEED = _OVERFEED;

                SPEED_PV = speed_PV;
                WIDTHCOAT = _WIDTHCOAT;
                WIDTHCOATALL = _WIDTHCOATALL;
                SILICONE_A = _SILICONE_A;
                SILICONE_B = _SILICONE_B;
                ITM_WEAVING = itm_WEAVING;
                FINISHINGLOT = finishingLot;
                COATINGWEIGTH_L = _COATINGWEIGTH_L;
                COATINGWEIGTH_C = _COATINGWEIGTH_C;
                COATINGWEIGTH_R = _COATINGWEIGTH_R;

                LENGTH1 = length1;
                LENGTH2 = length2;
                LENGTH3 = length3;
                LENGTH4 = length4;
                LENGTH5 = length5;
                LENGTH6 = length6;
                LENGTH7 = length7;
                INPUTLENGTH = inputLength;
                FINISHBY = finishBy;
                PARTNO = partNo;
                FINISHLENGTH = finishLength;
                SAMPLINGID = samplingid;

                CONDITIONBY = conditionBy;
                STARTBY = startBy;
                REMARK = remark;
                HUMID_AF = humid_AF;
                HUMID_BF = humid_BF;

                OPERATOR_GROUP = operator_group;

                REPROCESS = _REPROCESS;
                TEMP1_MIN = _TEMP1_MIN;
                TEMP1_MAX = _TEMP1_MAX;
                TEMP2_MIN = _TEMP2_MIN;
                TEMP2_MAX = _TEMP2_MAX;
                TEMP3_MIN = _TEMP3_MIN;
                TEMP3_MAX = _TEMP3_MAX;
                TEMP4_MIN = _TEMP4_MIN;
                TEMP4_MAX = _TEMP4_MAX;
                TEMP5_MIN = _TEMP5_MIN;
                TEMP5_MAX = _TEMP5_MAX;
                TEMP6_MIN = _TEMP6_MIN;
                TEMP6_MAX = _TEMP6_MAX;
                TEMP7_MIN = _TEMP7_MIN;
                TEMP7_MAX = _TEMP7_MAX;
                TEMP8_MIN = _TEMP8_MIN;
                TEMP8_MAX = _TEMP8_MAX;
                TEMP9_MIN = _TEMP9_MIN;
                TEMP9_MAX = _TEMP9_MAX;
                TEMP10_MIN = _TEMP10_MIN;
                TEMP10_MAX = _TEMP10_MAX;
                SAT_CHEM_MIN = _SAT_CHEM_MIN;
                SAT_CHEM_MAX = _SAT_CHEM_MAX;
                WASHING1_MIN = _WASHING1_MIN;
                WASHING1_MAX = _WASHING1_MAX;
                WASHING2_MIN = _WASHING2_MIN;
                WASHING2_MAX = _WASHING2_MAX;
                HOTFLUE_MIN = _HOTFLUE_MIN;
                HOTFLUE_MAX = _HOTFLUE_MAX;
                SPEED_MIN = _SPEED_MIN;
                SPEED_MAX = _SPEED_MAX;
                TENSIONUP_MIN = _TENSIONUP_MIN;
                TENSIONUP_MAX = _TENSIONUP_MAX;
                TENSIONDOWN_MIN = _TENSIONDOWN_MIN;
                TENSIONDOWN_MAX = _TENSIONDOWN_MAX;

                #endregion
            }

            #region Coating

            public string ITM_CODE { get; set; }
            public string WEAVINGLOT { get; set; }
            public string FINISHINGCUSTOMER { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public string PRODUCTTYPEID { get; set; }
            public string MCNO { get; set; }
            public string STATUSFLAG { get; set; }

            public decimal? SATURATOR_CHEM_PV { get; set; }
            public decimal? WASHING1_PV { get; set; }
            public decimal? WASHING2_PV { get; set; }
            public decimal? HOTFLUE_PV { get; set; }
            public decimal? BE_COATWIDTH { get; set; }

            public decimal? TEMP1_PV { get; set; }
            public decimal? TEMP2_PV { get; set; }
            public decimal? TEMP3_PV { get; set; }
            public decimal? TEMP4_PV { get; set; }
            public decimal? TEMP5_PV { get; set; }
            public decimal? TEMP6_PV { get; set; }
            public decimal? TEMP7_PV { get; set; }
            public decimal? TEMP8_PV { get; set; }
            public decimal? TEMP9_PV { get; set; }
            public decimal? TEMP10_PV { get; set; }

            public decimal? FANRPM { get; set; }
            public decimal? EXFAN_FRONT_BACK { get; set; }
            public decimal? EXFAN_MIDDLE { get; set; }
            public decimal? ANGLEKNIFE { get; set; }
            public string BLADENO { get; set; }
            public string BLADEDIRECTION { get; set; }
            public decimal? CYLINDER_TENSIONUP { get; set; }
            public decimal? OPOLE_TENSIONDOWN { get; set; }
            public decimal? FRAMEWIDTH_FORN { get; set; }
            public decimal? FRAMEWIDTH_TENTER { get; set; }
            public decimal? PATHLINE { get; set; }
            public decimal? FEEDIN { get; set; }
            public decimal? OVERFEED { get; set; }

            public decimal? SPEED_PV { get; set; }
            public decimal? WIDTHCOAT { get; set; }
            public decimal? WIDTHCOATALL { get; set; }
            public string SILICONE_A { get; set; }
            public string SILICONE_B { get; set; }
            public string ITM_WEAVING { get; set; }
            public string FINISHINGLOT { get; set; }

            public decimal? COATINGWEIGTH_L { get; set; }
            public decimal? COATINGWEIGTH_C { get; set; }
            public decimal? COATINGWEIGTH_R { get; set; }

            public decimal? LENGTH1 { get; set; }
            public decimal? LENGTH2 { get; set; }
            public decimal? LENGTH3 { get; set; }
            public decimal? LENGTH4 { get; set; }
            public decimal? LENGTH5 { get; set; }
            public decimal? LENGTH6 { get; set; }
            public decimal? LENGTH7 { get; set; }
            public decimal? INPUTLENGTH { get; set; }
            public string FINISHBY { get; set; }
            public string PARTNO { get; set; }
            public decimal? FINISHLENGTH { get; set; }
            public string SAMPLINGID { get; set; }

            public string CONDITIONBY { get; set; }
            public string STARTBY { get; set; }
            public string REMARK { get; set; }
            public decimal? HUMID_AF { get; set; }
            public decimal? HUMID_BF { get; set; }

            public string OPERATOR_GROUP { get; set; }

            public string REPROCESS { get; set; }
            public decimal? TEMP1_MIN { get; set; }
            public decimal? TEMP1_MAX { get; set; }
            public decimal? TEMP2_MIN { get; set; }
            public decimal? TEMP2_MAX { get; set; }
            public decimal? TEMP3_MIN { get; set; }
            public decimal? TEMP3_MAX { get; set; }
            public decimal? TEMP4_MIN { get; set; }
            public decimal? TEMP4_MAX { get; set; }
            public decimal? TEMP5_MIN { get; set; }
            public decimal? TEMP5_MAX { get; set; }
            public decimal? TEMP6_MIN { get; set; }
            public decimal? TEMP6_MAX { get; set; }
            public decimal? TEMP7_MIN { get; set; }
            public decimal? TEMP7_MAX { get; set; }
            public decimal? TEMP8_MIN { get; set; }
            public decimal? TEMP8_MAX { get; set; }
            public decimal? TEMP9_MIN { get; set; }
            public decimal? TEMP9_MAX { get; set; }
            public decimal? TEMP10_MIN { get; set; }
            public decimal? TEMP10_MAX { get; set; }
            public decimal? SAT_CHEM_MIN { get; set; }
            public decimal? SAT_CHEM_MAX { get; set; }
            public decimal? WASHING1_MIN { get; set; }
            public decimal? WASHING1_MAX { get; set; }
            public decimal? WASHING2_MIN { get; set; }
            public decimal? WASHING2_MAX { get; set; }
            public decimal? HOTFLUE_MIN { get; set; }
            public decimal? HOTFLUE_MAX { get; set; }
            public decimal? SPEED_MIN { get; set; }
            public decimal? SPEED_MAX { get; set; }
            public decimal? TENSIONUP_MIN { get; set; }
            public decimal? TENSIONUP_MAX { get; set; }
            public decimal? TENSIONDOWN_MIN { get; set; }
            public decimal? TENSIONDOWN_MAX { get; set; }
            
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

            public byte[] PARTNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.PARTNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.PARTNO,
                            600, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            #endregion
        }

        #endregion

        #region ListCoatingCondition

        public class ListCoatingCondition
        {
            public ListCoatingCondition()
            {
                // default constructor
            }

            public ListCoatingCondition(string _ITM_CODE,decimal? _SATURATOR_CHEM,decimal? _SATURATOR_CHEM_MARGIN,
            decimal? _WASHING1, decimal? _WASHING1_MARGIN, decimal? _WASHING2, decimal? _WASHING2_MARGIN,
            decimal? _HOTFLUE,decimal? _HOTFLUE_MARGIN,decimal? _BE_COATWIDTHMAX,decimal? _BE_COATWIDTHMIN,
            decimal? _ROOMTEMP,decimal? _ROOMTEMP_MARGIN, decimal? _FANRPM, decimal? _FANRPM_MARGIN,
            decimal? _EXFAN_FRONT_BACK,decimal? _EXFAN_MARGIN,decimal? _ANGLEKNIFE,string _BLADENO,
            string _BLADEDIRECTION,string _PATHLINE,decimal? _FEEDIN_MAX,decimal? _TENSION_UP,
            decimal? _TENSION_DOWN, decimal? _TENSION_DOWN_MARGIN,decimal? _FRAMEWIDTH_FORN,decimal? _FRAMEWIDTH_TENTER,
            string _OVERFEED,decimal? _SPEED,decimal? _SPEED_MARGIN,decimal? _WIDTHCOAT,
            decimal? _WIDTHCOATALL_MAX, decimal? _WIDTHCOATALL_MIN, decimal? _COATINGWEIGTH_MAX, decimal? _COATINGWEIGTH_MIN,
            decimal? _EXFAN_MIDDLE, string _RATIOSILICONE,string _COATNO, decimal? _FEEDIN_MIN,decimal? _TENSION_UP_MARGIN,

            string _BE_COATWIDTHSpecification,string _Fan110Specification,string _EXFAN15Specification,string _EXFAN234Specification,
            string _EXFAN1_6Specification,string _EXFAN2_5Specification ,
            string _ANGLEKNIFESpecification,string _FeedInSpecification, string _TENSION_DOWNSpecification,string _TENSION_UPSpecification,
            string _SPEEDSpecification,string _WIDTHCOATSpecification,string _WIDTHCOATALLSpecification,
            string _SATURATOR_CHEMSpecification, string _WASHING1Specification,string _WASHING2Specification,
            string _HOTFLUESpecification,string _ROOMTEMPSpecification, string _COATINGWEIGTHSpecification,
            string _HUMIDSpecification)
            {
                #region ListCoatingCondition

                ITM_CODE = _ITM_CODE;
                SATURATOR_CHEM = _SATURATOR_CHEM;
                SATURATOR_CHEM_MARGIN = _SATURATOR_CHEM_MARGIN;
                WASHING1 = _WASHING1;
                WASHING1_MARGIN = _WASHING1_MARGIN;
                WASHING2 = _WASHING2;
                WASHING2_MARGIN = _WASHING2_MARGIN;
                HOTFLUE = _HOTFLUE;
                HOTFLUE_MARGIN = _HOTFLUE_MARGIN;
                BE_COATWIDTHMAX = _BE_COATWIDTHMAX;
                BE_COATWIDTHMIN = _BE_COATWIDTHMIN;
                ROOMTEMP = _ROOMTEMP;
                ROOMTEMP_MARGIN = _ROOMTEMP_MARGIN;
                FANRPM = _FANRPM;
                FANRPM_MARGIN = _FANRPM_MARGIN;
                EXFAN_FRONT_BACK = _EXFAN_FRONT_BACK;
                EXFAN_MARGIN = _EXFAN_MARGIN;
                ANGLEKNIFE = _ANGLEKNIFE;
                BLADENO = _BLADENO;
                BLADEDIRECTION = _BLADEDIRECTION;
                PATHLINE = _PATHLINE;
                FEEDIN_MAX = _FEEDIN_MAX;
                TENSION_UP = _TENSION_UP;
                TENSION_DOWN = _TENSION_DOWN;
                TENSION_DOWN_MARGIN = _TENSION_DOWN_MARGIN;
                FRAMEWIDTH_FORN = _FRAMEWIDTH_FORN;
                FRAMEWIDTH_TENTER = _FRAMEWIDTH_TENTER;
                OVERFEED = _OVERFEED;
                SPEED = _SPEED;
                SPEED_MARGIN = _SPEED_MARGIN;
                WIDTHCOAT = _WIDTHCOAT;
                WIDTHCOATALL_MAX = _WIDTHCOATALL_MAX;
                WIDTHCOATALL_MIN = _WIDTHCOATALL_MIN;
                COATINGWEIGTH_MAX = _COATINGWEIGTH_MAX;
                COATINGWEIGTH_MIN = _COATINGWEIGTH_MIN;
                EXFAN_MIDDLE = _EXFAN_MIDDLE;
                RATIOSILICONE = _RATIOSILICONE;
                COATNO = _COATNO;
                FEEDIN_MIN = _FEEDIN_MIN;
                TENSION_UP_MARGIN = _TENSION_UP_MARGIN;

                BE_COATWIDTHSpecification = _BE_COATWIDTHSpecification;
                Fan110Specification = _Fan110Specification;
                EXFAN15Specification = _EXFAN15Specification;
                EXFAN234Specification = _EXFAN234Specification;
                EXFAN1_6Specification = _EXFAN1_6Specification;
                EXFAN2_5Specification = _EXFAN2_5Specification;
                ANGLEKNIFESpecification = _ANGLEKNIFESpecification;
                FeedInSpecification = _FeedInSpecification;
                TENSION_DOWNSpecification = _TENSION_DOWNSpecification;
                TENSION_UPSpecification = _TENSION_UPSpecification;
                SPEEDSpecification = _SPEEDSpecification;
                WIDTHCOATSpecification = _WIDTHCOATSpecification;
                WIDTHCOATALLSpecification = _WIDTHCOATALLSpecification;

                SATURATOR_CHEMSpecification = _SATURATOR_CHEMSpecification;
                WASHING1Specification = _WASHING1Specification;
                WASHING2Specification = _WASHING2Specification;
                HOTFLUESpecification = _HOTFLUESpecification;
                ROOMTEMPSpecification = _ROOMTEMPSpecification;
                COATINGWEIGTHSpecification = _COATINGWEIGTHSpecification;
                HUMIDSpecification = _HUMIDSpecification;

                #endregion
            }

            #region Coating Condition

            public string ITM_CODE { get; set; }
            public decimal? SATURATOR_CHEM { get; set; }
            public decimal? SATURATOR_CHEM_MARGIN { get; set; }
            public decimal? WASHING1 { get; set; }
            public decimal? WASHING1_MARGIN { get; set; }
            public decimal? WASHING2 { get; set; }
            public decimal? WASHING2_MARGIN { get; set; }
            public decimal? HOTFLUE { get; set; }
            public decimal? HOTFLUE_MARGIN { get; set; }
            public decimal? BE_COATWIDTHMAX { get; set; }
            public decimal? BE_COATWIDTHMIN { get; set; }
            public decimal? ROOMTEMP { get; set; }
            public decimal? ROOMTEMP_MARGIN { get; set; }
            public decimal? FANRPM { get; set; }
            public decimal? FANRPM_MARGIN { get; set; }
            public decimal? EXFAN_FRONT_BACK { get; set; }
            public decimal? EXFAN_MARGIN { get; set; }
            public decimal? ANGLEKNIFE { get; set; }
            public string BLADENO { get; set; }
            public string BLADEDIRECTION { get; set; }
            public string PATHLINE { get; set; }
            public decimal? FEEDIN_MAX { get; set; }
            public decimal? TENSION_UP { get; set; }
            public decimal? TENSION_DOWN { get; set; }
            public decimal? TENSION_DOWN_MARGIN { get; set; }
            public decimal? FRAMEWIDTH_FORN { get; set; }
            public decimal? FRAMEWIDTH_TENTER { get; set; }
            public string OVERFEED { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? SPEED_MARGIN { get; set; }
            public decimal? WIDTHCOAT { get; set; }
            public decimal? WIDTHCOATALL_MAX { get; set; }
            public decimal? WIDTHCOATALL_MIN { get; set; }
            public decimal? COATINGWEIGTH_MAX { get; set; }
            public decimal? COATINGWEIGTH_MIN { get; set; }
            public decimal? EXFAN_MIDDLE { get; set; }
            public string RATIOSILICONE { get; set; }
            public string COATNO { get; set; }
            public decimal? FEEDIN_MIN { get; set; }
            public decimal? TENSION_UP_MARGIN { get; set; }
            public string BE_COATWIDTHSpecification { get; set; }
            public string Fan110Specification { get; set; }
            public string EXFAN15Specification { get; set; }
            public string EXFAN234Specification { get; set; }
            public string EXFAN1_6Specification { get; set; }
            public string EXFAN2_5Specification { get; set; }

            public string ANGLEKNIFESpecification { get; set; }
            public string FeedInSpecification { get; set; }
            public string TENSION_DOWNSpecification { get; set; }
            public string TENSION_UPSpecification { get; set; }
            public string SPEEDSpecification { get; set; }
            public string WIDTHCOATSpecification { get; set; }
            public string WIDTHCOATALLSpecification { get; set; }
            public string SATURATOR_CHEMSpecification { get; set; }
            public string WASHING1Specification { get; set; }
            public string WASHING2Specification { get; set; }
            public string HOTFLUESpecification { get; set; }
            public string ROOMTEMPSpecification { get; set; }
            public string COATINGWEIGTHSpecification { get; set; }
            public string HUMIDSpecification { get; set; }

            #endregion
        }

        #endregion

        #region ListBarcodePart

        public class ListBarcodePart
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListBarcodePart()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListBarcodePart()
            {
                // default constructor
            }

            public ListBarcodePart(string customerPartNo
                , decimal? quantity, string description, string supplierCode, string strDate
                , string partRev, decimal? nw, decimal? gw, string serial, string batchNo)
            {
                #region ListBarcodePart

                CustomerPartNo = customerPartNo;
                Quantity = quantity;
                Description = description;
                SupplierCode = supplierCode;
                StrDate = strDate;
                PartRev = partRev;
                NW = nw;
                GW = gw;
                Serial = serial;
                BatchNo = batchNo;

                #endregion
            }

            #region BarcodePart

            public string CustomerPartNo { get; set; }
            public decimal? Quantity { get; set; }
            public string Description { get; set; }
            public string SupplierCode { get; set; }
            public string StrDate { get; set; }
            public string PartRev { get; set; }
            public decimal? NW { get; set; }
            public decimal? GW { get; set; }
            public string Serial { get; set; }
            public string BatchNo { get; set; }


            // For Barcode
            public byte[] CustomerPartNoImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.CustomerPartNo))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.CustomerPartNo,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] QuantityImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.Quantity.ToString()))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.Quantity.ToString(),
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] SupplierCodeImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.SupplierCode))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.SupplierCode,
                            550, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] SerialImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.Serial))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.Serial,
                            550, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] BatchNoImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.BatchNo))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.BatchNo,
                            550, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }
            #endregion
        }

        #endregion

        #region ListSampling

        public class ListSampling
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListSampling()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListSampling()
            {
                // default constructor
            }

            public ListSampling(string weavingLot
                , string finishingLot, string itm_Code, DateTime createDate ,string createBy
                ,string productID , decimal? width, decimal? length
                , string process, string remark
                , string fabrictype, string productName)
            {
                #region ListSampling

                WEAVINGLOT = weavingLot;
                FINISHINGLOT = finishingLot;
                ITM_CODE = itm_Code;
                CREATEDATE = createDate;
                CREATEBY = createBy;
                PRODUCTID = productID;
                SAMPLING_WIDTH = width;
                SAMPLING_LENGTH = length;
                PROCESS = process;
                REMARK = remark;
                FABRICTYPE = fabrictype;
                PRODUCTNAME = productName;

                #endregion
            }

            #region Sampling

            public string WEAVINGLOT { get; set; }
            public string FINISHINGLOT { get; set; }
            public string ITM_CODE { get; set; }
            public DateTime? CREATEDATE { get; set; }
            public string CREATEBY { get; set; }
            public string PRODUCTID { get; set; }
            public decimal? SAMPLING_WIDTH { get; set; }
            public decimal? SAMPLING_LENGTH { get; set; }
            public string PROCESS { get; set; }
            public string REMARK { get; set; }
            public string FABRICTYPE { get; set; }
            public string PRODUCTNAME { get; set; }

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

            public byte[] FINISHINGLOTImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.FINISHINGLOT))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.FINISHINGLOT,
                            600, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            #endregion
        }

        #endregion

        #region ListFINISHRECORD

        public class ListFINISHRECORD
        {
           
            public ListFINISHRECORD()
            {
                // default constructor
            }

            public ListFINISHRECORD(int rowNo, string _WEAVINGLOT, string _FINISHINGLOT, string _ITM_CODE, DateTime? _STARTDATE, DateTime? _ENDDATE
                , string _FINISHBY, string _STARTBY, string _CONDITIONBY, string _MCNO, string _MC
                , decimal? _WEAVLENGTH, decimal? _WIDTH_BE, decimal? _WIDTH_AF , string _OPERATOR_GROUP
                , decimal? _LENGTH1, decimal? _LENGTH2, decimal? _LENGTH3, decimal? _LENGTH4, decimal? _LENGTH5, decimal? _LENGTH6, decimal? _LENGTH7, decimal? _TOTALLENGTH, string _FinishingLength, string _PRODUCTIONTYPE)
            {
                #region ListWEAVINGINGDATA

                RowNo = rowNo;
                WEAVINGLOT = _WEAVINGLOT;
                FINISHINGLOT = _FINISHINGLOT;
                ITM_CODE = _ITM_CODE;
                STARTDATE = _STARTDATE;
                ENDDATE = _ENDDATE;
                FINISHBY = _FINISHBY;
                STARTBY = _STARTBY;
                CONDITIONBY = _CONDITIONBY;
                MCNO = _MCNO;
                MC = _MC;
                WEAVLENGTH = _WEAVLENGTH;
                WIDTH_BE = _WIDTH_BE;
                WIDTH_AF = _WIDTH_AF;
                OPERATOR_GROUP = _OPERATOR_GROUP;
                LENGTH1 = _LENGTH1;
                LENGTH2 = _LENGTH2;
                LENGTH3 = _LENGTH3;
                LENGTH4 = _LENGTH4;
                LENGTH5 = _LENGTH5;
                LENGTH6 = _LENGTH6;
                LENGTH7 = _LENGTH7;
                TOTALLENGTH = _TOTALLENGTH;
                FinishingLength = _FinishingLength;

                //New 17/07/19
                PRODUCTIONTYPE = _PRODUCTIONTYPE;

                #endregion
            }

            #region ListFINISHRECORD

            public int RowNo { get; set; }
            public string WEAVINGLOT { get; set; }
            public string FINISHINGLOT { get; set; }
            public string ITM_CODE { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public string FINISHBY { get; set; }
            public string STARTBY { get; set; }
            public string CONDITIONBY { get; set; }
            public string MCNO { get; set; }
            public string MC { get; set; }
            public decimal? WEAVLENGTH { get; set; }
            public decimal? WIDTH_BE { get; set; }
            public decimal? WIDTH_AF { get; set; }
            public string OPERATOR_GROUP { get; set; }
            public decimal? LENGTH1 { get; set; }
            public decimal? LENGTH2 { get; set; }
            public decimal? LENGTH3 { get; set; }
            public decimal? LENGTH4 { get; set; }
            public decimal? LENGTH5 { get; set; }
            public decimal? LENGTH6 { get; set; }
            public decimal? LENGTH7 { get; set; }
            public decimal? TOTALLENGTH { get; set; }
            public string FinishingLength { get; set; }

            //New 17/07/19
            public string PRODUCTIONTYPE { get; set; }

            #endregion
        }
        #endregion
    }
}
