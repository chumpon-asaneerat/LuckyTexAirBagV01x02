#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#endregion

using NLib;
using LuckyTex.Services;
using System.Windows.Media;

namespace LuckyTex.Models
{
    //-- Lab --//

    #region LAB_INSERTUPDATETENSILE

    public class LAB_INSERTUPDATETENSILE
    {
        public System.String P_ITMCODE { get; set; }
        public System.String P_WEAVINGLOG { get; set; }
        public System.String P_FINISHINGLOT { get; set; }
        public System.String P_OPERATOR { get; set; }
        public System.String P_TESTDATE { get; set; }
        public System.String P_TESTTIME { get; set; }
        public System.String P_YARN { get; set; }
        public System.Decimal? P_TENSILE1 { get; set; }
        public System.Decimal? P_TENSILE2 { get; set; }
        public System.Decimal? P_TENSILE3 { get; set; }
        public System.Decimal? P_ELONG1 { get; set; }
        public System.Decimal? P_ELONG2 { get; set; }
        public System.Decimal? P_ELONG3 { get; set; }
        public System.DateTime? P_UPLOADDATE { get; set; }
        public System.String P_UPLOADBY { get; set; }
    }

    #endregion

    #region LAB_INSERTUPDATETEAR

    public class LAB_INSERTUPDATETEAR
    {
        public System.String P_ITMCODE { get; set; }
        public System.String P_WEAVINGLOG { get; set; }
        public System.String P_FINISHINGLOT { get; set; }
        public System.String P_OPERATOR { get; set; }
        public System.String P_YARN { get; set; }
        public System.Decimal? P_TEAR1 { get; set; }
        public System.Decimal? P_TEAR2 { get; set; }
        public System.Decimal? P_TEAR3 { get; set; }
        public System.DateTime? P_UPLOADDATE { get; set; }
        public System.String P_UPLOADBY { get; set; }
    }

    #endregion

    #region LAB_INSERTUPDATEITEMSPEC

    public class LAB_INSERTUPDATEITEMSPEC
    {
        public System.String P_ITMCODE { get; set; }
        public System.Decimal? P_WIDTH_NO { get; set; }
        public System.Decimal? P_WIDTH { get; set; }
        public System.Decimal? P_USEWIDTH_NO { get; set; }
        public System.Decimal? P_USEWIDTH { get; set; }
        public System.String P_USEWIDTH_TOR { get; set; }
        public System.Decimal? P_WIDTHSILICONE_NO { get; set; }
        public System.Decimal? P_WIDTHSILICONE { get; set; }
        public System.String P_WIDTHSILICONE_TOR { get; set; }
        public System.Decimal? P_NUMTHREADS_W_NO { get; set; }
        public System.Decimal? P_NUMTHREADS_W { get; set; }
        public System.Decimal? P_NUMTHREADS_W_TOR { get; set; }
        public System.Decimal? P_NUMTHREADS_F_NO { get; set; }
        public System.Decimal? P_NUMTHREADS_F { get; set; }
        public System.Decimal? P_NUMTHREADS_F_TOR { get; set; }
        public System.Decimal? P_TOTALWEIGHT_NO { get; set; }
        public System.Decimal? P_TOTALWEIGHT { get; set; }
        public System.Decimal? P_TOTALWEIGHT_TOR { get; set; }
        public System.Decimal? P_UNCOATEDWEIGHT_NO { get; set; }
        public System.Decimal? P_UNCOATEDWEIGHT { get; set; }
        public System.Decimal? P_UNCOATEDWEIGHT_TOR { get; set; }
        public System.Decimal? P_COATWEIGHT_NO { get; set; }
        public System.Decimal? P_COATWEIGHT { get; set; }
        public System.Decimal? P_COATWEIGHT_TOR { get; set; }
        public System.Decimal? P_THICKNESS_NO { get; set; }
        public System.Decimal? P_THICKNESS { get; set; }
        public System.Decimal? P_THICKNESS_TOR { get; set; }
        public System.Decimal? P_MAXFORCE_W_NO { get; set; }
        public System.Decimal? P_MAXFORCE_W { get; set; }
        public System.String P_MAXFORCE_W_TOR { get; set; }
        public System.Decimal? P_MAXFORCE_F_NO { get; set; }
        public System.Decimal? P_MAXFORCE_F { get; set; }
        public System.String P_MAXFORCE_F_TOR { get; set; }
        public System.Decimal? P_ELOGATION_W_NO { get; set; }
        public System.Decimal? P_ELOGATION_W { get; set; }
        public System.String P_ELOGATION_W_TOR { get; set; }
        public System.Decimal? P_ELOGATION_F_NO { get; set; }
        public System.Decimal? P_ELOGATION_F { get; set; }
        public System.String P_ELOGATION_F_TOR { get; set; }
        public System.Decimal? P_FLAMMABILITY_W_NO { get; set; }
        public System.Decimal? P_FLAMMABILITY_W { get; set; }
        public System.String P_FLAMMABILITY_W_TOR { get; set; }
        public System.Decimal? P_FLAMMABILITY_F_NO { get; set; }
        public System.Decimal? P_FLAMMABILITY_F { get; set; }
        public System.String P_FLAMMABILITY_F_TOR { get; set; }
        public System.Decimal? P_EDGECOMB_W_NO { get; set; }
        public System.Decimal? P_EDGECOMB_W { get; set; }
        public System.String P_EDGECOMB_W_TOR { get; set; }
        public System.Decimal? P_EDGECOMB_F_NO { get; set; }
        public System.Decimal? P_EDGECOMB_F { get; set; }
        public System.String P_EDGECOMB_F_TOR { get; set; }
        public System.Decimal? P_STIFFNESS_W_NO { get; set; }
        public System.Decimal? P_STIFFNESS_W { get; set; }
        public System.String P_STIFFNESS_W_TOR { get; set; }
        public System.Decimal? P_STIFFNESS_F_NO { get; set; }
        public System.Decimal? P_STIFFNESS_F { get; set; }
        public System.String P_STIFFNESS_F_TOR { get; set; }
        public System.Decimal? P_TEAR_W_NO { get; set; }
        public System.Decimal? P_TEAR_W { get; set; }
        public System.String P_TEAR_W_TOR { get; set; }
        public System.Decimal? P_TEAR_F_NO { get; set; }
        public System.Decimal? P_TEAR_F { get; set; }
        public System.String P_TEAR_F_TOR { get; set; }
        public System.Decimal? P_STATIC_AIR_NO { get; set; }
        public System.Decimal? P_STATIC_AIR { get; set; }
        public System.String P_STATIC_AIR_TOR { get; set; }
        public System.Decimal? P_DYNAMIC_AIR_NO { get; set; }
        public System.Decimal? P_DYNAMIC_AIR { get; set; }
        public System.Decimal? P_DYNAMIC_AIR_TOR { get; set; }
        public System.Decimal? P_EXPONENT_NO { get; set; }
        public System.Decimal? P_EXPONENT { get; set; }
        public System.Decimal? P_EXPONENT_TOR { get; set; }
        public System.Decimal? P_DIMENSCHANGE_W_NO { get; set; }
        public System.Decimal? P_DIMENSCHANGE_W { get; set; }
        public System.String P_DIMENSCHANGE_W_TOR { get; set; }
        public System.Decimal? P_DIMENSCHANGE_F_NO { get; set; }
        public System.Decimal? P_DIMENSCHANGE_F { get; set; }
        public System.String P_DIMENSCHANGE_F_TOR { get; set; }
        public System.Decimal? P_FLEXABRASION_W_NO { get; set; }
        public System.Decimal? P_FLEXABRASION_W { get; set; }
        public System.String P_FLEXABRASION_W_TOR { get; set; }
        public System.Decimal? P_FLEXABRASION_F_NO { get; set; }
        public System.Decimal? P_FLEXABRASION_F { get; set; }
        public System.String P_FLEXABRASION_F_TOR { get; set; }
        public System.Decimal? P_BOW_NO { get; set; }
        public System.Decimal? P_BOW { get; set; }
        public System.String P_BOW_TOR { get; set; }
        public System.Decimal? P_SKEW_NO { get; set; }
        public System.Decimal? P_SKEW { get; set; }
        public System.String P_SKEW_TOR { get; set; }

        public System.Decimal? P_BENDING_W_NO { get; set; }
        public System.Decimal? P_BENDING_W { get; set; }
        public System.String P_BENDING_W_TOR { get; set; }
        public System.Decimal? P_BENDING_F_NO { get; set; }
        public System.Decimal? P_BENDING_F { get; set; }
        public System.String P_BENDING_F_TOR { get; set; }
        public System.Decimal? P_FLEX_SCOTT_W_NO { get; set; }
        public System.Decimal? P_FLEX_SCOTT_W { get; set; }
        public System.String P_FLEX_SCOTT_W_TOR { get; set; }
        public System.Decimal? P_FLEX_SCOTT_F_NO { get; set; }
        public System.Decimal? P_FLEX_SCOTT_F { get; set; }
        public System.String P_FLEX_SCOTT_F_TOR { get; set; }
    }

    #endregion

    #region LAB_INSERTUPDATEEDGECOMB

    public class LAB_INSERTUPDATEEDGECOMB
    {
        public System.String P_ITMCODE { get; set; }
        public System.String P_WEAVINGLOG { get; set; }
        public System.String P_FINISHINGLOT { get; set; }
        public System.String P_OPERATOR { get; set; }
        public System.String P_TESTDATE { get; set; }
        public System.String P_TESTTIME { get; set; }
        public System.String P_YARN { get; set; }
        public System.Decimal? P_EDGECOMB1 { get; set; }
        public System.Decimal? P_EDGECOMB2 { get; set; }
        public System.Decimal? P_EDGECOMB3 { get; set; }
        public System.DateTime? P_UPLOADDATE { get; set; }
        public System.String P_UPLOADBY { get; set; }
    }

    #endregion

    #region LAB_GETPDFDATA

    public class LAB_GETPDFDATA
    {
        public System.String PROPERTY { get; set; }
        public System.String YARNTYPE { get; set; }
        public System.Decimal? N1 { get; set; }
        public System.Decimal? N2 { get; set; }
        public System.Decimal? N3 { get; set; }

        //New 30/08/21
        public System.Decimal? N4 { get; set; }
        public System.Decimal? N5 { get; set; }
        public System.Decimal? N6 { get; set; }
        //----------//

        public System.Decimal? AVE { get; set; }
        public System.String strAVE { get; set; }
    }

    #endregion

    #region LAB_GETITEMTESTSPECIFICATION
    
    public class LAB_GETITEMTESTSPECIFICATION
    {
        public System.String ITM_CODE { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? USABLE_WIDTH { get; set; }
        public System.String USABLE_WIDTH_TOR { get; set; }
        public System.Decimal? NUMTHREADS_W { get; set; }
        public System.Decimal? NUMTHREADS_W_TOR { get; set; }
        public System.Decimal? NUMTHREADS_F { get; set; }
        public System.Decimal? NUMTHREADS_F_TOR { get; set; }
        public System.Decimal? WIDTH_SILICONE { get; set; }
        public System.String WIDTH_SILICONE_TOR { get; set; }
        public System.Decimal? TOTALWEIGHT { get; set; }
        public System.Decimal? TOTALWEIGHT_TOR { get; set; }
        public System.Decimal? UNCOATEDWEIGHT { get; set; }
        public System.Decimal? UNCOATEDWEIGHT_TOR { get; set; }
        public System.Decimal? COATINGWEIGHT { get; set; }
        public System.Decimal? COATINGWEIGHT_TOR { get; set; }
        public System.Decimal? THICKNESS { get; set; }
        public System.Decimal? THICKNESS_TOR { get; set; }
        public System.Decimal? MAXFORCE_W { get; set; }
        public System.String MAXFORCE_W_TOR { get; set; }
        public System.Decimal? MAXFORCE_F { get; set; }
        public System.String MAXFORCE_F_TOR { get; set; }
        public System.Decimal? ELONGATIONFORCE_W { get; set; }
        public System.String ELONGATIONFORCE_W_TOR { get; set; }
        public System.Decimal? ELONGATIONFORCE_F { get; set; }
        public System.String ELONGATIONFORCE_F_TOR { get; set; }
        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.String FLAMMABILITY_W_TOR { get; set; }
        public System.Decimal? FLAMMABILITY_F { get; set; }
        public System.String FLAMMABILITY_F_TOR { get; set; }
        public System.Decimal? EDGECOMB_W { get; set; }
        public System.String EDGECOMB_W_TOR { get; set; }
        public System.Decimal? EDGECOMB_F { get; set; }
        public System.String EDGECOMB_F_TOR { get; set; }
        public System.Decimal? STIFFNESS_W { get; set; }
        public System.String STIFFNESS_W_TOR { get; set; }
        public System.Decimal? STIFFNESS_F { get; set; }
        public System.String STIFFNESS_F_TOR { get; set; }
        public System.Decimal? TEAR_W { get; set; }
        public System.String TEAR_W_TOR { get; set; }
        public System.Decimal? TEAR_F { get; set; }
        public System.String TEAR_F_TOR { get; set; }
        public System.Decimal? STATIC_AIR { get; set; }
        public System.String STATIC_AIR_TOR { get; set; }
        public System.Decimal? DYNAMIC_AIR { get; set; }
        public System.Decimal? DYNAMIC_AIR_TOR { get; set; }
        public System.Decimal? EXPONENT { get; set; }
        public System.Decimal? EXPONENT_TOR { get; set; }
        public System.Decimal? DIMENSCHANGE_W { get; set; }
        public System.String DIMENSCHANGE_W_TOR { get; set; }
        public System.Decimal? DIMENSCHANGE_F { get; set; }
        public System.String DIMENSCHANGE_F_TOR { get; set; }
        public System.Decimal? FLEXABRASION_W { get; set; }
        public System.String FLEXABRASION_W_TOR { get; set; }
        public System.Decimal? FLEXABRASION_F { get; set; }
        public System.String FLEXABRASION_F_TOR { get; set; }
        public System.Decimal? BOW { get; set; }
        public System.String BOW_TOR { get; set; }
        public System.Decimal? SKEW { get; set; }
        public System.String SKEW_TOR { get; set; }

        //Update 07/07/18
        public System.Decimal? BENDING_W { get; set; }
        public System.String BENDING_W_TOR { get; set; }
        public System.Decimal? BENDING_F { get; set; }
        public System.String BENDING_F_TOR { get; set; }
        public System.Decimal? FLEX_SCOTT_W { get; set; }
        public System.String FLEX_SCOTT_W_TOR { get; set; }
        public System.Decimal? FLEX_SCOTT_F { get; set; }
        public System.String FLEX_SCOTT_F_TOR { get; set; }

        // ตัวแปรเอาไว้ใส่ค่าที่ได้จาก Database //
        public System.String WIDTH_Spe { get; set; }
        public System.String USABLE_Spe { get; set; }
        public System.String NUMTHREADS_W_Spe { get; set; }
        public System.String NUMTHREADS_F_Spe { get; set; }
        public System.String WIDTH_SILICONE_Spe { get; set; }
        public System.String TOTALWEIGHT_Spe { get; set; }
        public System.String UNCOATEDWEIGHT_Spe { get; set; }
        public System.String COATINGWEIGHT_Spe { get; set; }
        public System.String THICKNESS_Spe { get; set; }
        public System.String MAXFORCE_W_Spe { get; set; }
        public System.String MAXFORCE_F_Spe { get; set; }
        public System.String ELONGATIONFORCE_W_Spe { get; set; }
        public System.String ELONGATIONFORCE_F_Spe { get; set; }
        public System.String FLAMMABILITY_W_Spe { get; set; }
        public System.String FLAMMABILITY_F_Spe { get; set; }
        public System.String EDGECOMB_W_Spe { get; set; }
        public System.String EDGECOMB_F_Spe { get; set; }
        public System.String STIFFNESS_W_Spe { get; set; }
        public System.String STIFFNESS_F_Spe { get; set; }
        public System.String TEAR_W_Spe { get; set; }
        public System.String TEAR_F_Spe { get; set; }
        public System.String STATIC_AIR_Spe { get; set; }
        public System.String DYNAMIC_AIR_Spe { get; set; }
        public System.String EXPONENT_Spe { get; set; }
        public System.String DIMENSCHANGE_W_Spe { get; set; }
        public System.String DIMENSCHANGE_F_Spe { get; set; }
        public System.String FLEXABRASION_W_Spe { get; set; }
        public System.String FLEXABRASION_F_Spe { get; set; }
        public System.String BOW_Spe { get; set; }
        public System.String SKEW_Spe { get; set; }

        //Update 07/07/18
        public System.String BENDING_W_Spe { get; set; }
        public System.String BENDING_F_Spe { get; set; }
        public System.String FLEX_SCOTT_W_Spe { get; set; }
        public System.String FLEX_SCOTT_F_Spe { get; set; }

        //New 7/9/22
        public System.Decimal? USABLE_WIDTH_LCL { get; set; }
        public System.Decimal? USABLE_WIDTH_UCL { get; set; }
        public System.Decimal? TOTALWEIGHT_LCL { get; set; }
        public System.Decimal? TOTALWEIGHT_UCL { get; set; }
        public System.Decimal? NUMTHREADS_W_LCL { get; set; }
        public System.Decimal? NUMTHREADS_W_UCL { get; set; }
        public System.Decimal? NUMTHREADS_F_LCL { get; set; }
        public System.Decimal? NUMTHREADS_F_UCL { get; set; }
        public System.Decimal? MAXFORCE_W_LCL { get; set; }
        public System.Decimal? MAXFORCE_W_UCL { get; set; }
        public System.Decimal? MAXFORCE_F_LCL { get; set; }
        public System.Decimal? MAXFORCE_F_UCL { get; set; }
        public System.Decimal? ELONGATIONFORCE_W_LCL { get; set; }
        public System.Decimal? ELONGATIONFORCE_W_UCL { get; set; }
        public System.Decimal? ELONGATIONFORCE_F_LCL { get; set; }
        public System.Decimal? ELONGATIONFORCE_F_UCL { get; set; }
        public System.Decimal? EDGECOMB_W_LCL { get; set; }
        public System.Decimal? EDGECOMB_W_UCL { get; set; }
        public System.Decimal? EDGECOMB_F_LCL { get; set; }
        public System.Decimal? EDGECOMB_F_UCL { get; set; }
        public System.Decimal? TEAR_W_LCL { get; set; }
        public System.Decimal? TEAR_W_UCL { get; set; }
        public System.Decimal? TEAR_F_LCL { get; set; }
        public System.Decimal? TEAR_F_UCL { get; set; }
        public System.Decimal? STATIC_AIR_LCL { get; set; }
        public System.Decimal? STATIC_AIR_UCL { get; set; }
        public System.Decimal? DYNAMIC_AIR_LCL { get; set; }
        public System.Decimal? DYNAMIC_AIR_UCL { get; set; }
        public System.Decimal? EXPONENT_LCL { get; set; }
        public System.Decimal? EXPONENT_UCL { get; set; }

        // เพิ่ม 11/1/65
        public System.String CUSTOMERID { get; set; }
    }

    #endregion

    #region LAB_GETITEMTESTPROPERTY

    public class LAB_GETITEMTESTPROPERTY
    {
        public System.String ITM_CODE { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? USABLE_WIDTH { get; set; }
        public System.Decimal? WIDTH_SILICONE { get; set; }
        public System.Decimal? NUMTHREADS_W { get; set; }
        public System.Decimal? NUMTHREADS_F { get; set; }
        public System.Decimal? TOTALWEIGHT { get; set; }
        public System.Decimal? UNCOATEDWEIGHT { get; set; }
        public System.Decimal? COATINGWEIGHT { get; set; }
        public System.Decimal? THICKNESS { get; set; }
        public System.Decimal? MAXFORCE_W { get; set; }
        public System.Decimal? MAXFORCE_F { get; set; }
        public System.Decimal? ELONGATIONFORCE_W { get; set; }
        public System.Decimal? ELONGATIONFORCE_F { get; set; }
        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.Decimal? FLAMMABILITY_F { get; set; }
        public System.Decimal? EDGECOMB_W { get; set; }
        public System.Decimal? EDGECOMB_F { get; set; }
        public System.Decimal? STIFFNESS_W { get; set; }
        public System.Decimal? STIFFNESS_F { get; set; }
        public System.Decimal? TEAR_W { get; set; }
        public System.Decimal? TEAR_F { get; set; }
        public System.Decimal? STATIC_AIR { get; set; }
        public System.Decimal? DYNAMIC_AIR { get; set; }
        public System.Decimal? EXPONENT { get; set; }
        public System.Decimal? DIMENSCHANGE_W { get; set; }
        public System.Decimal? DIMENSCHANGE_F { get; set; }
        public System.Decimal? FLEXABRASION_W { get; set; }
        public System.Decimal? FLEXABRASION_F { get; set; }
        public System.Decimal? BOW { get; set; }
        public System.Decimal? SKEW { get; set; }
        public System.Decimal? BENDING_W { get; set; }
        public System.Decimal? BENDING_F { get; set; }
        public System.Decimal? FLEX_SCOTT_W { get; set; }
        public System.Decimal? FLEX_SCOTT_F { get; set; }
    }

    #endregion

    #region LAB_INSERTPRODUCTION

    public class LAB_INSERTPRODUCTION
    {
        public System.String P_ITMCODE { get; set; }
        public System.String P_WEAVINGLOG { get; set; }
        public System.String P_FINISHINGLOT { get; set; }
        public System.DateTime? P_ENTRYDATE { get; set; }
        public System.String P_ENTRYBY { get; set; }
        public System.Decimal? P_WIDTH { get; set; }
        public System.Decimal? P_USEWIDTH1 { get; set; }
        public System.Decimal? P_USEWIDTH2 { get; set; }
        public System.Decimal? P_USEWIDTH3 { get; set; }
        public System.Decimal? P_WIDTHSILICONE1 { get; set; }
        public System.Decimal? P_WIDTHSILICONE2 { get; set; }
        public System.Decimal? P_WIDTHSILICONE3 { get; set; }
        public System.Decimal? P_NUMTHREADS_W1 { get; set; }
        public System.Decimal? P_NUMTHREADS_W2 { get; set; }
        public System.Decimal? P_NUMTHREADS_W3 { get; set; }
        public System.Decimal? P_NUMTHREADS_F1 { get; set; }
        public System.Decimal? P_NUMTHREADS_F2 { get; set; }
        public System.Decimal? P_NUMTHREADS_F3 { get; set; }
        public System.Decimal? P_TOTALWEIGHT1 { get; set; }
        public System.Decimal? P_TOTALWEIGHT2 { get; set; }
        public System.Decimal? P_TOTALWEIGHT3 { get; set; }
        public System.Decimal? P_TOTALWEIGHT4 { get; set; }
        public System.Decimal? P_TOTALWEIGHT5 { get; set; }
        public System.Decimal? P_TOTALWEIGHT6 { get; set; }
        public System.Decimal? P_UNCOATEDWEIGHT1 { get; set; }
        public System.Decimal? P_UNCOATEDWEIGHT2 { get; set; }
        public System.Decimal? P_UNCOATEDWEIGHT3 { get; set; }
        public System.Decimal? P_UNCOATEDWEIGHT4 { get; set; }
        public System.Decimal? P_UNCOATEDWEIGHT5 { get; set; }
        public System.Decimal? P_UNCOATEDWEIGHT6 { get; set; }
        public System.Decimal? P_COATWEIGHT1 { get; set; }
        public System.Decimal? P_COATWEIGHT2 { get; set; }
        public System.Decimal? P_COATWEIGHT3 { get; set; }
        public System.Decimal? P_COATWEIGHT4 { get; set; }
        public System.Decimal? P_COATWEIGHT5 { get; set; }
        public System.Decimal? P_COATWEIGHT6 { get; set; }
        public System.Decimal? P_THICKNESS1 { get; set; }
        public System.Decimal? P_THICKNESS2 { get; set; }
        public System.Decimal? P_THICKNESS3 { get; set; }
        public System.Decimal? P_MAXFORCE_W1 { get; set; }
        public System.Decimal? P_MAXFORCE_W2 { get; set; }
        public System.Decimal? P_MAXFORCE_W3 { get; set; }

        public System.Decimal? P_MAXFORCE_W4 { get; set; }
        public System.Decimal? P_MAXFORCE_W5 { get; set; }
        public System.Decimal? P_MAXFORCE_W6 { get; set; }

        public System.Decimal? P_MAXFORCE_F1 { get; set; }
        public System.Decimal? P_MAXFORCE_F2 { get; set; }
        public System.Decimal? P_MAXFORCE_F3 { get; set; }

        public System.Decimal? P_MAXFORCE_F4 { get; set; }
        public System.Decimal? P_MAXFORCE_F5 { get; set; }
        public System.Decimal? P_MAXFORCE_F6 { get; set; }

        public System.Decimal? P_ELOGATION_W1 { get; set; }
        public System.Decimal? P_ELOGATION_W2 { get; set; }
        public System.Decimal? P_ELOGATION_W3 { get; set; }

        public System.Decimal? P_ELOGATION_W4 { get; set; }
        public System.Decimal? P_ELOGATION_W5 { get; set; }
        public System.Decimal? P_ELOGATION_W6 { get; set; }

        public System.Decimal? P_ELOGATION_F1 { get; set; }
        public System.Decimal? P_ELOGATION_F2 { get; set; }
        public System.Decimal? P_ELOGATION_F3 { get; set; }

        public System.Decimal? P_ELOGATION_F4 { get; set; }
        public System.Decimal? P_ELOGATION_F5 { get; set; }
        public System.Decimal? P_ELOGATION_F6 { get; set; }

        public System.Decimal? P_FLAMMABILITY_W { get; set; }
        public System.Decimal? P_FLAMMABILITY_W2 { get; set; }
        public System.Decimal? P_FLAMMABILITY_W3 { get; set; }
        public System.Decimal? P_FLAMMABILITY_W4 { get; set; }
        public System.Decimal? P_FLAMMABILITY_W5 { get; set; }

        public System.Decimal? P_FLAMMABILITY_F { get; set; }
        public System.Decimal? P_FLAMMABILITY_F2 { get; set; }
        public System.Decimal? P_FLAMMABILITY_F3 { get; set; }
        public System.Decimal? P_FLAMMABILITY_F4 { get; set; }
        public System.Decimal? P_FLAMMABILITY_F5 { get; set; }

        public System.Decimal? P_EDGECOMB_W1 { get; set; }
        public System.Decimal? P_EDGECOMB_W2 { get; set; }
        public System.Decimal? P_EDGECOMB_W3 { get; set; }
        public System.Decimal? P_EDGECOMB_F1 { get; set; }
        public System.Decimal? P_EDGECOMB_F2 { get; set; }
        public System.Decimal? P_EDGECOMB_F3 { get; set; }
        public System.Decimal? P_STIFFNESS_W1 { get; set; }
        public System.Decimal? P_STIFFNESS_W2 { get; set; }
        public System.Decimal? P_STIFFNESS_W3 { get; set; }
        public System.Decimal? P_STIFFNESS_F1 { get; set; }
        public System.Decimal? P_STIFFNESS_F2 { get; set; }
        public System.Decimal? P_STIFFNESS_F3 { get; set; }
        public System.Decimal? P_TEAR_W1 { get; set; }
        public System.Decimal? P_TEAR_W2 { get; set; }
        public System.Decimal? P_TEAR_W3 { get; set; }
        public System.Decimal? P_TEAR_F1 { get; set; }
        public System.Decimal? P_TEAR_F2 { get; set; }
        public System.Decimal? P_TEAR_F3 { get; set; }
        public System.Decimal? P_STATIC_AIR1 { get; set; }
        public System.Decimal? P_STATIC_AIR2 { get; set; }
        public System.Decimal? P_STATIC_AIR3 { get; set; }

        public System.Decimal? P_STATIC_AIR4 { get; set; }
        public System.Decimal? P_STATIC_AIR5 { get; set; }
        public System.Decimal? P_STATIC_AIR6 { get; set; }

        public System.Decimal? P_DYNAMIC_AIR1 { get; set; }
        public System.Decimal? P_DYNAMIC_AIR2 { get; set; }
        public System.Decimal? P_DYNAMIC_AIR3 { get; set; }
        public System.Decimal? P_EXPONENT1 { get; set; }
        public System.Decimal? P_EXPONENT2 { get; set; }
        public System.Decimal? P_EXPONENT3 { get; set; }
        public System.Decimal? P_DIMENSCHANGE_W1 { get; set; }
        public System.Decimal? P_DIMENSCHANGE_W2 { get; set; }
        public System.Decimal? P_DIMENSCHANGE_W3 { get; set; }
        public System.Decimal? P_DIMENSCHANGE_F1 { get; set; }
        public System.Decimal? P_DIMENSCHANGE_F2 { get; set; }
        public System.Decimal? P_DIMENSCHANGE_F3 { get; set; }
        public System.Decimal? P_FLEXABRASION_W1 { get; set; }
        public System.Decimal? P_FLEXABRASION_W2 { get; set; }
        public System.Decimal? P_FLEXABRASION_W3 { get; set; }
        public System.Decimal? P_FLEXABRASION_F1 { get; set; }
        public System.Decimal? P_FLEXABRASION_F2 { get; set; }
        public System.Decimal? P_FLEXABRASION_F3 { get; set; }
      
        public System.String P_STATUS { get; set; }
        public System.String P_REMARK { get; set; }
        public System.String P_APPROVEBY { get; set; }
        public System.DateTime? P_APPROVEDATE { get; set; }

        public System.Decimal? P_BOW1 { get; set; }
        public System.Decimal? P_BOW2 { get; set; }
        public System.Decimal? P_BOW3 { get; set; }
        public System.Decimal? P_SKEW1 { get; set; }
        public System.Decimal? P_SKEW2 { get; set; }
        public System.Decimal? P_SKEW3 { get; set; }
        public System.Decimal? P_BENDING_W1 { get; set; }
        public System.Decimal? P_BENDING_W2 { get; set; }
        public System.Decimal? P_BENDING_W3 { get; set; }
        public System.Decimal? P_BENDING_F1 { get; set; }
        public System.Decimal? P_BENDING_F2 { get; set; }
        public System.Decimal? P_BENDING_F3 { get; set; }
        public System.Decimal? P_FLEX_SCOTT_W1 { get; set; }
        public System.Decimal? P_FLEX_SCOTT_W2 { get; set; }
        public System.Decimal? P_FLEX_SCOTT_W3 { get; set; }
        public System.Decimal? P_FLEX_SCOTT_F1 { get; set; }
        public System.Decimal? P_FLEX_SCOTT_F2 { get; set; }
        public System.Decimal? P_FLEX_SCOTT_F3 { get; set; }
    }

    #endregion

    // -- Update 15/06/18 -- //

    #region MC_GETLOOMLIST

    public class MC_GETLOOMLIST
    {
        public System.String MCNAME { get; set; }
    }

    #endregion

    #region LAB_SEARCHLABENTRYPRODUCTION

    public class LAB_SEARCHLABENTRYPRODUCTION
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String ENTEYBY { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? USABLE_WIDTH1 { get; set; }
        public System.Decimal? USABLE_WIDTH2 { get; set; }
        public System.Decimal? USABLE_WIDTH3 { get; set; }
        public System.Decimal? WIDTH_SILICONE1 { get; set; }
        public System.Decimal? WIDTH_SILICONE2 { get; set; }
        public System.Decimal? WIDTH_SILICONE3 { get; set; }
        public System.Decimal? NUMTHREADS_W1 { get; set; }
        public System.Decimal? NUMTHREADS_W2 { get; set; }
        public System.Decimal? NUMTHREADS_W3 { get; set; }
        public System.Decimal? NUMTHREADS_F1 { get; set; }
        public System.Decimal? NUMTHREADS_F2 { get; set; }
        public System.Decimal? NUMTHREADS_F3 { get; set; }
        public System.Decimal? TOTALWEIGHT1 { get; set; }
        public System.Decimal? TOTALWEIGHT2 { get; set; }
        public System.Decimal? TOTALWEIGHT3 { get; set; }
        public System.Decimal? TOTALWEIGHT4 { get; set; }
        public System.Decimal? TOTALWEIGHT5 { get; set; }
        public System.Decimal? TOTALWEIGHT6 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT1 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT2 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT3 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT4 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT5 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT6 { get; set; }
        public System.Decimal? COATINGWEIGHT1 { get; set; }
        public System.Decimal? COATINGWEIGHT2 { get; set; }
        public System.Decimal? COATINGWEIGHT3 { get; set; }
        public System.Decimal? COATINGWEIGHT4 { get; set; }
        public System.Decimal? COATINGWEIGHT5 { get; set; }
        public System.Decimal? COATINGWEIGHT6 { get; set; }
        public System.Decimal? THICKNESS1 { get; set; }
        public System.Decimal? THICKNESS2 { get; set; }
        public System.Decimal? THICKNESS3 { get; set; }
        public System.Decimal? MAXFORCE_W1 { get; set; }
        public System.Decimal? MAXFORCE_W2 { get; set; }
        public System.Decimal? MAXFORCE_W3 { get; set; }
        public System.Decimal? MAXFORCE_F1 { get; set; }
        public System.Decimal? MAXFORCE_F2 { get; set; }
        public System.Decimal? MAXFORCE_F3 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W1 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W2 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W3 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F1 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F2 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F3 { get; set; }

        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.Decimal? FLAMMABILITY_W2 { get; set; }
        public System.Decimal? FLAMMABILITY_W3 { get; set; }
        public System.Decimal? FLAMMABILITY_W4 { get; set; }
        public System.Decimal? FLAMMABILITY_W5 { get; set; }

        public System.Decimal? FLAMMABILITY_F { get; set; }
        public System.Decimal? FLAMMABILITY_F2 { get; set; }
        public System.Decimal? FLAMMABILITY_F3 { get; set; }
        public System.Decimal? FLAMMABILITY_F4 { get; set; }
        public System.Decimal? FLAMMABILITY_F5 { get; set; }

        public System.Decimal? EDGECOMB_W1 { get; set; }
        public System.Decimal? EDGECOMB_W2 { get; set; }
        public System.Decimal? EDGECOMB_W3 { get; set; }
        public System.Decimal? EDGECOMB_F1 { get; set; }
        public System.Decimal? EDGECOMB_F2 { get; set; }
        public System.Decimal? EDGECOMB_F3 { get; set; }
        public System.Decimal? STIFFNESS_W1 { get; set; }
        public System.Decimal? STIFFNESS_W2 { get; set; }
        public System.Decimal? STIFFNESS_W3 { get; set; }
        public System.Decimal? STIFFNESS_F1 { get; set; }
        public System.Decimal? STIFFNESS_F2 { get; set; }
        public System.Decimal? STIFFNESS_F3 { get; set; }
        public System.Decimal? TEAR_W1 { get; set; }
        public System.Decimal? TEAR_W2 { get; set; }
        public System.Decimal? TEAR_W3 { get; set; }
        public System.Decimal? TEAR_F1 { get; set; }
        public System.Decimal? TEAR_F2 { get; set; }
        public System.Decimal? TEAR_F3 { get; set; }
        public System.Decimal? STATIC_AIR1 { get; set; }
        public System.Decimal? STATIC_AIR2 { get; set; }
        public System.Decimal? STATIC_AIR3 { get; set; }

        public System.Decimal? STATIC_AIR4 { get; set; }
        public System.Decimal? STATIC_AIR5 { get; set; }
        public System.Decimal? STATIC_AIR6 { get; set; }

        public System.Decimal? DYNAMIC_AIR1 { get; set; }
        public System.Decimal? DYNAMIC_AIR2 { get; set; }
        public System.Decimal? DYNAMIC_AIR3 { get; set; }
        public System.Decimal? EXPONENT1 { get; set; }
        public System.Decimal? EXPONENT2 { get; set; }
        public System.Decimal? EXPONENT3 { get; set; }
        public System.Decimal? DIMENSCHANGE_W1 { get; set; }
        public System.Decimal? DIMENSCHANGE_W2 { get; set; }
        public System.Decimal? DIMENSCHANGE_W3 { get; set; }
        public System.Decimal? DIMENSCHANGE_F1 { get; set; }
        public System.Decimal? DIMENSCHANGE_F2 { get; set; }
        public System.Decimal? DIMENSCHANGE_F3 { get; set; }
        public System.Decimal? FLEXABRASION_W1 { get; set; }
        public System.Decimal? FLEXABRASION_W2 { get; set; }
        public System.Decimal? FLEXABRASION_W3 { get; set; }
        public System.Decimal? FLEXABRASION_F1 { get; set; }
        public System.Decimal? FLEXABRASION_F2 { get; set; }
        public System.Decimal? FLEXABRASION_F3 { get; set; }
        public System.Decimal? BOW1 { get; set; }
        public System.Decimal? SKEW1 { get; set; }
        public System.String STATUS { get; set; }
        public System.String REMARK { get; set; }
        public System.String APPROVEBY { get; set; }
        public System.DateTime? APPROVEDATE { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.Decimal? BOW2 { get; set; }
        public System.Decimal? BOW3 { get; set; }
        public System.Decimal? SKEW2 { get; set; }
        public System.Decimal? SKEW3 { get; set; }
        public System.Decimal? BENDING_W1 { get; set; }
        public System.Decimal? BENDING_W2 { get; set; }
        public System.Decimal? BENDING_W3 { get; set; }
        public System.Decimal? BENDING_F1 { get; set; }
        public System.Decimal? BENDING_F2 { get; set; }
        public System.Decimal? BENDING_F3 { get; set; }
        public System.Decimal? FLEX_SCOTT_W1 { get; set; }
        public System.Decimal? FLEX_SCOTT_W2 { get; set; }
        public System.Decimal? FLEX_SCOTT_W3 { get; set; }
        public System.Decimal? FLEX_SCOTT_F1 { get; set; }
        public System.Decimal? FLEX_SCOTT_F2 { get; set; }
        public System.Decimal? FLEX_SCOTT_F3 { get; set; }
        public System.Decimal? FINISHLENGTH { get; set; }
        public System.String FINISHINGPROCESS { get; set; }
        public System.String ITEMLOT { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String FINISHINGMC { get; set; }

        //New 31/08/20
        public System.String BATCHNO { get; set; }
        public System.String CUSTOMERID { get; set; }
        public System.String PARTNO { get; set; }

        /// <summary>
        /// Update 26/10/20
        /// </summary>
        public System.String FILENAME { get; set; }
        public System.DateTime? UPLOADDATE { get; set; }
        public System.String UPLOADBY { get; set; }
    }

    #endregion

    #region LAB_GETLABDETAIL

    public class LAB_GETLABDETAIL
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String ENTEYBY { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? USABLE_WIDTH1 { get; set; }
        public System.Decimal? USABLE_WIDTH2 { get; set; }
        public System.Decimal? USABLE_WIDTH3 { get; set; }
        public System.Decimal? WIDTH_SILICONE1 { get; set; }
        public System.Decimal? WIDTH_SILICONE2 { get; set; }
        public System.Decimal? WIDTH_SILICONE3 { get; set; }
        public System.Decimal? NUMTHREADS_W1 { get; set; }
        public System.Decimal? NUMTHREADS_W2 { get; set; }
        public System.Decimal? NUMTHREADS_W3 { get; set; }
        public System.Decimal? NUMTHREADS_F1 { get; set; }
        public System.Decimal? NUMTHREADS_F2 { get; set; }
        public System.Decimal? NUMTHREADS_F3 { get; set; }
        public System.Decimal? TOTALWEIGHT1 { get; set; }
        public System.Decimal? TOTALWEIGHT2 { get; set; }
        public System.Decimal? TOTALWEIGHT3 { get; set; }
        public System.Decimal? TOTALWEIGHT4 { get; set; }
        public System.Decimal? TOTALWEIGHT5 { get; set; }
        public System.Decimal? TOTALWEIGHT6 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT1 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT2 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT3 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT4 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT5 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT6 { get; set; }
        public System.Decimal? COATINGWEIGHT1 { get; set; }
        public System.Decimal? COATINGWEIGHT2 { get; set; }
        public System.Decimal? COATINGWEIGHT3 { get; set; }
        public System.Decimal? COATINGWEIGHT4 { get; set; }
        public System.Decimal? COATINGWEIGHT5 { get; set; }
        public System.Decimal? COATINGWEIGHT6 { get; set; }
        public System.Decimal? THICKNESS1 { get; set; }
        public System.Decimal? THICKNESS2 { get; set; }
        public System.Decimal? THICKNESS3 { get; set; }
        public System.Decimal? MAXFORCE_W1 { get; set; }
        public System.Decimal? MAXFORCE_W2 { get; set; }
        public System.Decimal? MAXFORCE_W3 { get; set; }
        public System.Decimal? MAXFORCE_F1 { get; set; }
        public System.Decimal? MAXFORCE_F2 { get; set; }
        public System.Decimal? MAXFORCE_F3 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W1 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W2 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W3 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F1 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F2 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F3 { get; set; }

        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.Decimal? FLAMMABILITY_W2 { get; set; }
        public System.Decimal? FLAMMABILITY_W3 { get; set; }
        public System.Decimal? FLAMMABILITY_W4 { get; set; }
        public System.Decimal? FLAMMABILITY_W5 { get; set; }

        public System.Decimal? FLAMMABILITY_F { get; set; }
        public System.Decimal? FLAMMABILITY_F2 { get; set; }
        public System.Decimal? FLAMMABILITY_F3 { get; set; }
        public System.Decimal? FLAMMABILITY_F4 { get; set; }
        public System.Decimal? FLAMMABILITY_F5 { get; set; }

        public System.Decimal? EDGECOMB_W1 { get; set; }
        public System.Decimal? EDGECOMB_W2 { get; set; }
        public System.Decimal? EDGECOMB_W3 { get; set; }
        public System.Decimal? EDGECOMB_F1 { get; set; }
        public System.Decimal? EDGECOMB_F2 { get; set; }
        public System.Decimal? EDGECOMB_F3 { get; set; }
        public System.Decimal? STIFFNESS_W1 { get; set; }
        public System.Decimal? STIFFNESS_W2 { get; set; }
        public System.Decimal? STIFFNESS_W3 { get; set; }
        public System.Decimal? STIFFNESS_F1 { get; set; }
        public System.Decimal? STIFFNESS_F2 { get; set; }
        public System.Decimal? STIFFNESS_F3 { get; set; }
        public System.Decimal? TEAR_W1 { get; set; }
        public System.Decimal? TEAR_W2 { get; set; }
        public System.Decimal? TEAR_W3 { get; set; }
        public System.Decimal? TEAR_F1 { get; set; }
        public System.Decimal? TEAR_F2 { get; set; }
        public System.Decimal? TEAR_F3 { get; set; }
        public System.Decimal? STATIC_AIR1 { get; set; }
        public System.Decimal? STATIC_AIR2 { get; set; }
        public System.Decimal? STATIC_AIR3 { get; set; }

        public System.Decimal? STATIC_AIR4 { get; set; }
        public System.Decimal? STATIC_AIR5 { get; set; }
        public System.Decimal? STATIC_AIR6 { get; set; }

        public System.Decimal? DYNAMIC_AIR1 { get; set; }
        public System.Decimal? DYNAMIC_AIR2 { get; set; }
        public System.Decimal? DYNAMIC_AIR3 { get; set; }
        public System.Decimal? EXPONENT1 { get; set; }
        public System.Decimal? EXPONENT2 { get; set; }
        public System.Decimal? EXPONENT3 { get; set; }
        public System.Decimal? DIMENSCHANGE_W1 { get; set; }
        public System.Decimal? DIMENSCHANGE_W2 { get; set; }
        public System.Decimal? DIMENSCHANGE_W3 { get; set; }
        public System.Decimal? DIMENSCHANGE_F1 { get; set; }
        public System.Decimal? DIMENSCHANGE_F2 { get; set; }
        public System.Decimal? DIMENSCHANGE_F3 { get; set; }
        public System.Decimal? FLEXABRASION_W1 { get; set; }
        public System.Decimal? FLEXABRASION_W2 { get; set; }
        public System.Decimal? FLEXABRASION_W3 { get; set; }
        public System.Decimal? FLEXABRASION_F1 { get; set; }
        public System.Decimal? FLEXABRASION_F2 { get; set; }
        public System.Decimal? FLEXABRASION_F3 { get; set; }
        public System.Decimal? BOW1 { get; set; }
        public System.Decimal? SKEW1 { get; set; }
        public System.String STATUS { get; set; }
        public System.String REMARK { get; set; }
        public System.String APPROVEBY { get; set; }
        public System.DateTime? APPROVEDATE { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.Decimal? BOW2 { get; set; }
        public System.Decimal? BOW3 { get; set; }
        public System.Decimal? SKEW2 { get; set; }
        public System.Decimal? SKEW3 { get; set; }
        public System.Decimal? BENDING_W1 { get; set; }
        public System.Decimal? BENDING_W2 { get; set; }
        public System.Decimal? BENDING_W3 { get; set; }
        public System.Decimal? BENDING_F1 { get; set; }
        public System.Decimal? BENDING_F2 { get; set; }
        public System.Decimal? BENDING_F3 { get; set; }
        public System.Decimal? FLEX_SCOTT_W1 { get; set; }
        public System.Decimal? FLEX_SCOTT_W2 { get; set; }
        public System.Decimal? FLEX_SCOTT_W3 { get; set; }
        public System.Decimal? FLEX_SCOTT_F1 { get; set; }
        public System.Decimal? FLEX_SCOTT_F2 { get; set; }
        public System.Decimal? FLEX_SCOTT_F3 { get; set; }

        /// <summary>
        /// Update 26/10/20
        /// </summary>
        public System.String FILENAME { get; set; }
        public System.DateTime? UPLOADDATE { get; set; }
        public System.String UPLOADBY { get; set; }
    }

    #endregion

    #region FinishingProcess

    public class LAB_FinishingProcess
    {
        public System.String PROCESS { get; set; }
    }

    #endregion

    #region Allowance

    public class LAB_AllowanceProcess
    {
        public System.String Allowance { get; set; }
    }

    #endregion
    
    #region LabDataEntryInfo
    public class LabDataEntryInfo
    {
        #region Public Properties

        public bool ChkApprove { get; set; }

        #endregion
    }
    #endregion

    #region EntrySampleTestDataInfo
    public class EntrySampleTestDataInfo
    {
        #region Public Properties

        public bool ChkSave { get; set; }

        #endregion
    }
    #endregion

    // -- Update 20/06/18 -- //

    #region Row 1

    #region LAB_SEARCHLABENTRYPRODUCTION_1ROW

    public class LAB_SEARCHLABENTRYPRODUCTION_1ROW
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String ENTEYBY { get; set; }
        public System.String STATUS { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.Decimal? FINISHLENGTH { get; set; }
        public System.String FINISHINGPROCESS { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String FINISHINGMC { get; set; }

        // Update 06/07/18
        public System.String ITEMLOT { get; set; }
    }

    #endregion

    #region LAB_WIDTH_1ROW

    public class LAB_WIDTH_1ROW
    {
        public System.Decimal? WIDTH { get; set; }
    }

    #endregion

    #region LAB_FLAMMABILITY_1ROW

    public class LAB_FLAMMABILITY_1ROW
    {
        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.Decimal? FLAMMABILITY_F { get; set; }
    }

    #endregion

    #region LAB_REMARK_1ROW

    public class LAB_REMARK_1ROW
    {
        public System.String REMARK { get; set; }
    }

    #endregion

    #region LAB_APPROVE_1ROW

    public class LAB_APPROVE_1ROW
    {
        public System.String APPROVEBY { get; set; }
        public System.DateTime? APPROVEDATE { get; set; }
    }

    #endregion

    #endregion

    #region Row 3

    #region LAB_USABLE_WIDTH_3ROW

    public class LAB_USABLE_WIDTH_3ROW
    {
        public System.Decimal? USABLE_WIDTH { get; set; }
    }

    #endregion

    #region LAB_WIDTH_SILICONE_3ROW

    public class LAB_WIDTH_SILICONE_3ROW
    {
        public System.Decimal? WIDTH_SILICONE { get; set; }
    }

    #endregion

    #region LAB_NUMTHREADS_3ROW

    public class LAB_NUMTHREADS_3ROW
    {
        public System.Decimal? NUMTHREADS_W { get; set; }
        public System.Decimal? NUMTHREADS_F { get; set; }
    }

    #endregion

    #region LAB_THICKNESS_3ROW

    public class LAB_THICKNESS_3ROW
    {
        public System.Decimal? THICKNESS { get; set; }
    }

    #endregion

    #region LAB_MAXFORCE_3ROW

    public class LAB_MAXFORCE_3ROW
    {
        public System.Decimal? MAXFORCE_W { get; set; }
        public System.Decimal? MAXFORCE_F { get; set; }
    }

    #endregion

    #region LAB_ELONGATIONFORCE_3ROW

    public class LAB_ELONGATIONFORCE_3ROW
    {
        public System.Decimal? ELONGATIONFORCE_W { get; set; }
        public System.Decimal? ELONGATIONFORCE_F { get; set; }
    }

    #endregion

    #region LAB_EDGECOMB_3ROW

    public class LAB_EDGECOMB_3ROW
    {
        public System.Decimal? EDGECOMB_W { get; set; }
        public System.Decimal? EDGECOMB_F { get; set; }
    }

    #endregion

    #region LAB_STIFFNESS_3ROW

    public class LAB_STIFFNESS_3ROW
    {
        public System.Decimal? STIFFNESS_W { get; set; }
        public System.Decimal? STIFFNESS_F { get; set; }
    }

    #endregion

    #region LAB_TEAR_3ROW

    public class LAB_TEAR_3ROW
    {
        public System.Decimal? TEAR_W { get; set; }
        public System.Decimal? TEAR_F { get; set; }
    }

    #endregion

    #region LAB_DYNAMIC_AIR_3ROW

    public class LAB_DYNAMIC_AIR_3ROW
    {
        public System.Decimal? DYNAMIC_AIR { get; set; }
    }

    #endregion

    #region LAB_EXPONENT_3ROW

    public class LAB_EXPONENT_3ROW
    {
        public System.Decimal? EXPONENT { get; set; }
    }

    #endregion

    #region LAB_DIMENSCHANGE_3ROW

    public class LAB_DIMENSCHANGE_3ROW
    {
        public System.Decimal? DIMENSCHANGE_W { get; set; }
        public System.Decimal? DIMENSCHANGE_F { get; set; }
    }

    #endregion

    #region LAB_FLEXABRASION_3ROW

    public class LAB_FLEXABRASION_3ROW
    {
        public System.Decimal? FLEXABRASION_W { get; set; }
        public System.Decimal? FLEXABRASION_F { get; set; }
    }

    #endregion

    #region LAB_BOW_3ROW

    public class LAB_BOW_3ROW
    {
        public System.Decimal? BOW { get; set; }
    }

    #endregion

    #region LAB_SKEW_3ROW

    public class LAB_SKEW_3ROW
    {
        public System.Decimal? SKEW { get; set; }
    }

    #endregion

    #region LAB_BENDING_3ROW

    public class LAB_BENDING_3ROW
    {
        public System.Decimal? BENDING_W { get; set; }
        public System.Decimal? BENDING_F { get; set; }
    }

    #endregion

    #region LAB_FLEX_SCOTT_3ROW

    public class LAB_FLEX_SCOTT_3ROW
    {
        public System.Decimal? FLEX_SCOTT_W { get; set; }
        public System.Decimal? FLEX_SCOTT_F { get; set; }
    }

    #endregion

    #endregion

    #region Row 6

    #region LAB_STATIC_AIR_6ROW

    public class LAB_STATIC_AIR_6ROW
    {
        public System.Decimal? STATIC_AIR { get; set; }
    }

    #endregion

    #region LAB_TOTALWEIGHT_6ROW

    public class LAB_TOTALWEIGHT_6ROW
    {
        public System.Decimal? TOTALWEIGHT { get; set; }
    }

    #endregion

    #region LAB_UNCOATEDWEIGHT_6ROW

    public class LAB_UNCOATEDWEIGHT_6ROW
    {
        public System.Decimal? UNCOATEDWEIGHT { get; set; }
    }

    #endregion

    #region LAB_COATINGWEIGHT_6ROW

    public class LAB_COATINGWEIGHT_6ROW
    {
        public System.Decimal? COATINGWEIGHT { get; set; }
    }

    #endregion

    #endregion

    #region LAB_SEARCHLABENTRYPRODUCTION_All

    public class LAB_SEARCHLABENTRYPRODUCTION_All
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String ENTEYBY { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.Decimal? FLAMMABILITY_F { get; set; }

        public System.String STATUS { get; set; }
        public System.String REMARK { get; set; }
        public System.String APPROVEBY { get; set; }
        public System.DateTime? APPROVEDATE { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.Decimal? FINISHLENGTH { get; set; }
        public System.String FINISHINGPROCESS { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String FINISHINGMC { get; set; }

        // Update 06/07/18
        public System.String ITEMLOT { get; set; }

        // ---- 3 Row ---- //
        public System.Decimal? USABLE_WIDTH { get; set; }
        public System.Decimal? WIDTH_SILICONE { get; set; }
        public System.Decimal? NUMTHREADS_W { get; set; }
        public System.Decimal? NUMTHREADS_F { get; set; }
        public System.Decimal? THICKNESS { get; set; }
        public System.Decimal? MAXFORCE_W { get; set; }
        public System.Decimal? MAXFORCE_F { get; set; }
        public System.Decimal? ELONGATIONFORCE_W { get; set; }
        public System.Decimal? ELONGATIONFORCE_F { get; set; }
        public System.Decimal? EDGECOMB_W { get; set; }
        public System.Decimal? EDGECOMB_F { get; set; }
        public System.Decimal? STIFFNESS_W { get; set; }
        public System.Decimal? STIFFNESS_F { get; set; }
        public System.Decimal? TEAR_W { get; set; }
        public System.Decimal? TEAR_F { get; set; }
        public System.Decimal? STATIC_AIR { get; set; }
        public System.Decimal? DYNAMIC_AIR { get; set; }
        public System.Decimal? EXPONENT { get; set; }
        public System.Decimal? DIMENSCHANGE_W { get; set; }
        public System.Decimal? DIMENSCHANGE_F { get; set; }
        public System.Decimal? FLEXABRASION_W { get; set; }
        public System.Decimal? FLEXABRASION_F { get; set; }

        // Update 06/07/18
        public System.Decimal? BOW { get; set; }
        public System.Decimal? SKEW { get; set; }
        public System.Decimal? BENDING_W { get; set; }
        public System.Decimal? BENDING_F { get; set; }
        public System.Decimal? FLEX_SCOTT_W { get; set; }
        public System.Decimal? FLEX_SCOTT_F { get; set; }

        // ---- 6 Row ---- //
        public System.Decimal? TOTALWEIGHT { get; set; }
        public System.Decimal? UNCOATEDWEIGHT { get; set; }
        public System.Decimal? COATINGWEIGHT { get; set; }
    }

    #endregion

    #region LAB_SEARCHLABENTRYPRODUCTION_QDAS

    public class LAB_SEARCHLABENTRYPRODUCTION_QDAS
    {
        
        public System.String WEAVINGLOT { get; set; }
        public System.String LOOMNO { get; set; }
        
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? USABLE_WIDTH { get; set; }
        public System.Decimal? WIDTH_SILICONE { get; set; }
        public System.Decimal? NUMTHREADS_W { get; set; }
        public System.Decimal? NUMTHREADS_F { get; set; }
        public System.Decimal? TOTALWEIGHT { get; set; }
        public System.Decimal? UNCOATEDWEIGHT { get; set; }
        public System.Decimal? COATINGWEIGHT { get; set; }
        public System.Decimal? THICKNESS { get; set; }
        public System.Decimal? MAXFORCE_W { get; set; }
        public System.Decimal? MAXFORCE_F { get; set; }
        public System.Decimal? ELONGATIONFORCE_W { get; set; }
        public System.Decimal? ELONGATIONFORCE_F { get; set; }
        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.Decimal? FLAMMABILITY_F { get; set; }
        public System.Decimal? EDGECOMB_W { get; set; }
        public System.Decimal? EDGECOMB_F { get; set; }
        public System.Decimal? STIFFNESS_W { get; set; }
        public System.Decimal? STIFFNESS_F { get; set; }
        public System.Decimal? TEAR_W { get; set; }
        public System.Decimal? TEAR_F { get; set; }
        public System.Decimal? STATIC_AIR { get; set; }
        public System.Decimal? DYNAMIC_AIR { get; set; }
        public System.Decimal? EXPONENT { get; set; }
        public System.Decimal? DIMENSCHANGE_W { get; set; }
        public System.Decimal? DIMENSCHANGE_F { get; set; }
        public System.Decimal? FLEXABRASION_W { get; set; }
        public System.Decimal? FLEXABRASION_F { get; set; }
        public System.Decimal? BOW { get; set; }
        public System.Decimal? SKEW { get; set; }
        public System.Decimal? BENDING_W { get; set; }
        public System.Decimal? BENDING_F { get; set; }
        public System.Decimal? FLEX_SCOTT_W { get; set; }
        public System.Decimal? FLEX_SCOTT_F { get; set; }

    }

    #endregion

    // -- Update 01/07/18 -- //

    #region LAB_INSERTSAMPLEDATA

    public class LAB_INSERTSAMPLEDATA
    {
        public System.String P_ITMCODE { get; set; }
        public System.String P_PRODUCTIONLOT { get; set; }
        public System.String P_FINISHINGLOT { get; set; }
        public System.DateTime? P_ENTRYDATE { get; set; }
        public System.String P_ENTRYBY { get; set; }
        public System.String P_YARN { get; set; }
        public System.String P_METHOD { get; set; }
        public System.Decimal? P_NO { get; set; }
        public System.Decimal? P_VALUE { get; set; }
        public System.Decimal? P_VALUE_OLD { get; set; }
    }

    #endregion

    #region LAB_GETSAMPLEDATABYMETHOD

    public class LAB_GETSAMPLEDATABYMETHOD
    {
        public System.String ITM_CODE { get; set; }
        public System.String PRODUCTIONLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String ENTRYBY { get; set; }
        public System.String METHOD { get; set; }
        public System.String YARN { get; set; }
        public System.Decimal? NO { get; set; }
        public System.Decimal? VALUE { get; set; }
        public System.DateTime? CREATEDDATE { get; set; }

    }

    #endregion

    #region LAB_GETNOSAMPLEBYMETHOD

    public class LAB_GETNOSAMPLEBYMETHOD
    {
        public System.String ITM_CODE { get; set; }
        public System.String PRODUCTIONLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.String METHOD { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.Decimal? N { get; set; }
    }

    #endregion

    //----------------------//

    // -- Update 20/09/18 -- //

    #region LAB_SEARCHLABSAMPLEDATA

    public class LAB_SEARCHLABSAMPLEDATA
    {
        public System.String PRODUCTIONLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
    }

    #endregion

    #region LAB_GETPLCDATA

    public class LAB_GETPLCDATA
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.Decimal? TOTALWEIGHT1 { get; set; }
        public System.Decimal? TOTALWEIGHT2 { get; set; }
        public System.Decimal? TOTALWEIGHT3 { get; set; }
        public System.Decimal? TOTALWEIGHT4 { get; set; }
        public System.Decimal? TOTALWEIGHT5 { get; set; }
        public System.Decimal? TOTALWEIGHT6 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT1 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT2 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT3 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT4 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT5 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT6 { get; set; }
        public System.Decimal? COATINGWEIGHT1 { get; set; }
        public System.Decimal? COATINGWEIGHT2 { get; set; }
        public System.Decimal? COATINGWEIGHT3 { get; set; }
        public System.Decimal? COATINGWEIGHT4 { get; set; }
        public System.Decimal? COATINGWEIGHT5 { get; set; }
        public System.Decimal? COATINGWEIGHT6 { get; set; }
        public System.Decimal? STIFFNESS_W1 { get; set; }
        public System.Decimal? STIFFNESS_W2 { get; set; }
        public System.Decimal? STIFFNESS_W3 { get; set; }
        public System.Decimal? STIFFNESS_F1 { get; set; }
        public System.Decimal? STIFFNESS_F2 { get; set; }
        public System.Decimal? STIFFNESS_F3 { get; set; }
        public System.Decimal? STATIC_AIR1 { get; set; }
        public System.Decimal? STATIC_AIR2 { get; set; }
        public System.Decimal? STATIC_AIR3 { get; set; }
        public System.Decimal? STATIC_AIR4 { get; set; }
        public System.Decimal? STATIC_AIR5 { get; set; }
        public System.Decimal? STATIC_AIR6 { get; set; }
        public System.DateTime? WEIGHTDATE { get; set; }
        public System.String WEIGHT_BY { get; set; }
        public System.DateTime? STIFFNESSDATE { get; set; }
        public System.String STIFFNESS_BY { get; set; }
        public System.DateTime? STATICAIRDATE { get; set; }
        public System.String STATICAIR_BY { get; set; }

        //เพิ่ม 25/11/18
        public System.Decimal? EXPONENT1 { get; set; }
        public System.Decimal? EXPONENT2 { get; set; }
        public System.Decimal? EXPONENT3 { get; set; }
        public System.Decimal? DYNAMIC_AIR1 { get; set; }
        public System.Decimal? DYNAMIC_AIR2 { get; set; }
        public System.Decimal? DYNAMIC_AIR3 { get; set; }
        public System.DateTime? DYNAMICDATE { get; set; }
        public System.String DYNAMIC_BY { get; set; }
    }

    #endregion

    // New 1/9/22
    #region LAB_GETWEIGHTDATA 

    public class LAB_GETWEIGHTDATA
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.Decimal? WEIGHT1 { get; set; }
        public System.Decimal? WEIGHT2 { get; set; }
        public System.Decimal? WEIGHT3 { get; set; }
        public System.Decimal? WEIGHT4 { get; set; }
        public System.Decimal? WEIGHT5 { get; set; }
        public System.Decimal? WEIGHT6 { get; set; }
        public System.DateTime? WEIGHTDATE { get; set; }
        public System.String WEIGHT_BY { get; set; }
    }

    #endregion

    #region LAB_GETSTIFFNESSDATA

    public class LAB_GETSTIFFNESSDATA
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.Decimal? STIFFNESS_W1 { get; set; }
        public System.Decimal? STIFFNESS_W2 { get; set; }
        public System.Decimal? STIFFNESS_W3 { get; set; }
        public System.Decimal? STIFFNESS_F1 { get; set; }
        public System.Decimal? STIFFNESS_F2 { get; set; }
        public System.Decimal? STIFFNESS_F3 { get; set; }
        public System.DateTime? STIFFNESSDATE { get; set; }
        public System.String STIFFNESS_BY { get; set; }
    }

    #endregion

    #region LAB_GETSTATICAIRDATA

    public class LAB_GETSTATICAIRDATA
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.Decimal? STATIC_AIR1 { get; set; }
        public System.Decimal? STATIC_AIR2 { get; set; }
        public System.Decimal? STATIC_AIR3 { get; set; }
        public System.Decimal? STATIC_AIR4 { get; set; }
        public System.Decimal? STATIC_AIR5 { get; set; }
        public System.Decimal? STATIC_AIR6 { get; set; }
        public System.DateTime? STATICAIRDATE { get; set; }
        public System.String STATICAIR_BY { get; set; }
    }

    #endregion

    #region LAB_GETDYNAMICAIRDATA

    public class LAB_GETDYNAMICAIRDATA
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.Decimal? EXPONENT1 { get; set; }
        public System.Decimal? EXPONENT2 { get; set; }
        public System.Decimal? EXPONENT3 { get; set; }
        public System.Decimal? DYNAMIC_AIR1 { get; set; }
        public System.Decimal? DYNAMIC_AIR2 { get; set; }
        public System.Decimal? DYNAMIC_AIR3 { get; set; }
        public System.DateTime? DYNAMICDATE { get; set; }
        public System.String DYNAMIC_BY { get; set; }
    }

    #endregion

    //----------------------//

    // -- Update 31/08/20 -- //

    #region LAB_GETREPORTINFO

    public class LAB_GETREPORTINFO
    {
        public System.String ITM_CODE { get; set; }
        public System.String REPORT_ID { get; set; }
        public System.String REVESION { get; set; }
        public System.String CUSTOMERID { get; set; }
        public System.String YARNTYPE { get; set; }
        public System.String WEIGHT { get; set; }
        public System.String COATWEIGHT { get; set; }
        public System.String NUMTHREADS { get; set; }
        public System.String USABLE_WIDTH { get; set; }
        public System.String THICKNESS { get; set; }
        public System.String MAXFORCE { get; set; }
        public System.String ELONGATIONFORCE { get; set; }
        public System.String FLAMMABILITY { get; set; }
        public System.String EDGECOMB { get; set; }
        public System.String STIFFNESS { get; set; }
        public System.String TEAR { get; set; }
        public System.String STATIC_AIR { get; set; }
        public System.String DYNAMIC_AIR { get; set; }
        public System.String EXPONENT { get; set; }
        public System.String DIMENSCHANGE { get; set; }
        public System.String FLEXABRASION { get; set; }
        public System.DateTime? EFFECTIVE_DATE { get; set; }

        public System.String REPORT_NAME { get; set; }

        public System.String BOW { get; set; }
        public System.String SKEW { get; set; }
        public System.String FLEX_SCOTT { get; set; }
        public System.String BENDING { get; set; }

    }

    #endregion

    //----------------------//

    #region LAB_ImportExcel1

    public class LAB_ImportExcel1
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? USABLE_WIDTH1 { get; set; }
        public System.Decimal? USABLE_WIDTH2 { get; set; }
        public System.Decimal? USABLE_WIDTH3 { get; set; }
        public System.Decimal? TOTALWEIGHT1 { get; set; }
        public System.Decimal? TOTALWEIGHT2 { get; set; }
        public System.Decimal? TOTALWEIGHT3 { get; set; }
        public System.Decimal? TOTALWEIGHT4 { get; set; }
        public System.Decimal? TOTALWEIGHT5 { get; set; }
        public System.Decimal? TOTALWEIGHT6 { get; set; }

        public System.Decimal? UNCOATEDWEIGHT1 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT2 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT3 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT4 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT5 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT6 { get; set; }

        public System.Decimal? COATWEIGHT1 { get; set; }
        public System.Decimal? COATWEIGHT2 { get; set; }
        public System.Decimal? COATWEIGHT3 { get; set; }
        public System.Decimal? COATWEIGHT4 { get; set; }
        public System.Decimal? COATWEIGHT5 { get; set; }
        public System.Decimal? COATWEIGHT6 { get; set; }

        public System.Decimal? THICKNESS1 { get; set; }
        public System.Decimal? THICKNESS2 { get; set; }
        public System.Decimal? THICKNESS3 { get; set; }
        public System.Decimal? NUMTHREADS_W1 { get; set; }
        public System.Decimal? NUMTHREADS_W2 { get; set; }
        public System.Decimal? NUMTHREADS_W3 { get; set; }
        public System.Decimal? NUMTHREADS_F1 { get; set; }
        public System.Decimal? NUMTHREADS_F2 { get; set; }
        public System.Decimal? NUMTHREADS_F3 { get; set; }
        public System.Decimal? MAXFORCE_W1 { get; set; }
        public System.Decimal? MAXFORCE_W2 { get; set; }
        public System.Decimal? MAXFORCE_W3 { get; set; }

        public System.Decimal? MAXFORCE_W4 { get; set; }
        public System.Decimal? MAXFORCE_W5 { get; set; }
        public System.Decimal? MAXFORCE_W6 { get; set; }

        public System.Decimal? MAXFORCE_F1 { get; set; }
        public System.Decimal? MAXFORCE_F2 { get; set; }
        public System.Decimal? MAXFORCE_F3 { get; set; }

        public System.Decimal? MAXFORCE_F4 { get; set; }
        public System.Decimal? MAXFORCE_F5 { get; set; }
        public System.Decimal? MAXFORCE_F6 { get; set; }

        public System.Decimal? ELONGATIONFORCE_W1 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W2 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W3 { get; set; }

        public System.Decimal? ELONGATIONFORCE_W4 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W5 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W6 { get; set; }

        public System.Decimal? ELONGATIONFORCE_F1 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F2 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F3 { get; set; }

        public System.Decimal? ELONGATIONFORCE_F4 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F5 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F6 { get; set; }

        public System.Decimal? EDGECOMB_W1 { get; set; }
        public System.Decimal? EDGECOMB_W2 { get; set; }
        public System.Decimal? EDGECOMB_W3 { get; set; }
        public System.Decimal? EDGECOMB_F1 { get; set; }
        public System.Decimal? EDGECOMB_F2 { get; set; }
        public System.Decimal? EDGECOMB_F3 { get; set; }
        public System.Decimal? STIFFNESS_W1 { get; set; }
        public System.Decimal? STIFFNESS_W2 { get; set; }
        public System.Decimal? STIFFNESS_W3 { get; set; }
        public System.Decimal? STIFFNESS_F1 { get; set; }
        public System.Decimal? STIFFNESS_F2 { get; set; }
        public System.Decimal? STIFFNESS_F3 { get; set; }
        public System.Decimal? TEAR_W1 { get; set; }
        public System.Decimal? TEAR_W2 { get; set; }
        public System.Decimal? TEAR_W3 { get; set; }
        public System.Decimal? TEAR_F1 { get; set; }
        public System.Decimal? TEAR_F2 { get; set; }
        public System.Decimal? TEAR_F3 { get; set; }
        public System.Decimal? STATIC_AIR1 { get; set; }
        public System.Decimal? STATIC_AIR2 { get; set; }
        public System.Decimal? STATIC_AIR3 { get; set; }
        public System.Decimal? DYNAMIC_AIR1 { get; set; }
        public System.Decimal? DYNAMIC_AIR2 { get; set; }
        public System.Decimal? DYNAMIC_AIR3 { get; set; }
        public System.Decimal? EXPONENT1 { get; set; }
        public System.Decimal? EXPONENT2 { get; set; }
        public System.Decimal? EXPONENT3 { get; set; }

        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.Decimal? FLAMMABILITY_W2 { get; set; }
        public System.Decimal? FLAMMABILITY_W3 { get; set; }
        public System.Decimal? FLAMMABILITY_W4 { get; set; }
        public System.Decimal? FLAMMABILITY_W5 { get; set; }

        public System.Decimal? FLAMMABILITY_F { get; set; }
        public System.Decimal? FLAMMABILITY_F2 { get; set; }
        public System.Decimal? FLAMMABILITY_F3 { get; set; }
        public System.Decimal? FLAMMABILITY_F4 { get; set; }
        public System.Decimal? FLAMMABILITY_F5 { get; set; }

        public System.Decimal? DIMENSCHANGE_W1 { get; set; }
        public System.Decimal? DIMENSCHANGE_W2 { get; set; }
        public System.Decimal? DIMENSCHANGE_W3 { get; set; }
        public System.Decimal? DIMENSCHANGE_F1 { get; set; }
        public System.Decimal? DIMENSCHANGE_F2 { get; set; }
        public System.Decimal? DIMENSCHANGE_F3 { get; set; }

        public System.Decimal? FLEXABRASION_W1 { get; set; }
        public System.Decimal? FLEXABRASION_W2 { get; set; }
        public System.Decimal? FLEXABRASION_W3 { get; set; }
        public System.Decimal? FLEXABRASION_F1 { get; set; }
        public System.Decimal? FLEXABRASION_F2 { get; set; }
        public System.Decimal? FLEXABRASION_F3 { get; set; }

        public System.Decimal? BOW1 { get; set; }
        public System.Decimal? BOW2 { get; set; }
        public System.Decimal? BOW3 { get; set; }
        public System.Decimal? SKEW1 { get; set; }
        public System.Decimal? SKEW2 { get; set; }
        public System.Decimal? SKEW3 { get; set; }
        public System.String ENTEYBY { get; set; }
        public System.String APPROVEBY { get; set; }

    }

    #endregion

    #region Lab Data PDF Session

    [Serializable]
    public class LabDataPDFSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LabDataPDFSession()
            : base()
        {

        }

        #endregion

        #region Private Methods

        #region Event Raiser(s)

        private void RaiseStateChanged()
        {
            if (null != OnStateChanged)
            {
                OnStateChanged.Call(this, EventArgs.Empty);
            }
        }

        #endregion

        #endregion

        // ใช้สำหรับ Load ข้อมูลใส่ ComboBox
        #region Load Combo GetItemCodeData
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusID"></param>
        /// <returns></returns>
        public List<ITM_GETITEMCODELIST> GetItemCodeData()
        {
            List<ITM_GETITEMCODELIST> results = LABDataService.Instance
                .ITM_GETITEMCODELIST();

            return results;
        }

        #endregion

        #region Public Methods

        #region Init

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        public void Init(LogInResult currUser)
        {
            _currUser = currUser;
        }

        #endregion

        #endregion

        #region Public Proeprties

        /// <summary>
        /// Gets or sets current user.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public LogInResult CurrentUser
        {
            get { return _currUser; }
            set
            {
                _currUser = value;
            }
        }
        /// <summary>
        /// Gets or sets Operator (or Operator).
        /// </summary>
        [XmlAttribute]
        public string Operator
        {
            get { return _operator; }
            set
            {
                if (_operator != value)
                {
                    _operator = value;
                }
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// OnStateChanged event.
        /// </summary>
        public event EventHandler OnStateChanged;

        #endregion
    }

    #endregion

    #region CheckList
    public class CheckList
    {
        public int NO { get; set; }
        public string ITMCODE { get; set; }
        public string WEAVINGLOG { get; set; }
        public string FINISHINGLOT { get; set; }
        public string STATUS { get; set; }

        public string ERROR { get; set; }

        public string FILENAME { get; set; }
        public int SUMROW { get; set; }
        public int SUMERR { get; set; }
    }
    #endregion

    // -- Update 26/10/20 -- //

    #region LAB_SEARCHAPPROVELAB

    public class LAB_SEARCHAPPROVELAB
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String ENTEYBY { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? USABLE_WIDTH1 { get; set; }
        public System.Decimal? USABLE_WIDTH2 { get; set; }
        public System.Decimal? USABLE_WIDTH3 { get; set; }
        public System.Decimal? WIDTH_SILICONE1 { get; set; }
        public System.Decimal? WIDTH_SILICONE2 { get; set; }
        public System.Decimal? WIDTH_SILICONE3 { get; set; }
        public System.Decimal? NUMTHREADS_W1 { get; set; }
        public System.Decimal? NUMTHREADS_W2 { get; set; }
        public System.Decimal? NUMTHREADS_W3 { get; set; }
        public System.Decimal? NUMTHREADS_F1 { get; set; }
        public System.Decimal? NUMTHREADS_F2 { get; set; }
        public System.Decimal? NUMTHREADS_F3 { get; set; }
        public System.Decimal? TOTALWEIGHT1 { get; set; }
        public System.Decimal? TOTALWEIGHT2 { get; set; }
        public System.Decimal? TOTALWEIGHT3 { get; set; }
        public System.Decimal? TOTALWEIGHT4 { get; set; }
        public System.Decimal? TOTALWEIGHT5 { get; set; }
        public System.Decimal? TOTALWEIGHT6 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT1 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT2 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT3 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT4 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT5 { get; set; }
        public System.Decimal? UNCOATEDWEIGHT6 { get; set; }
        public System.Decimal? COATINGWEIGHT1 { get; set; }
        public System.Decimal? COATINGWEIGHT2 { get; set; }
        public System.Decimal? COATINGWEIGHT3 { get; set; }
        public System.Decimal? COATINGWEIGHT4 { get; set; }
        public System.Decimal? COATINGWEIGHT5 { get; set; }
        public System.Decimal? COATINGWEIGHT6 { get; set; }
        public System.Decimal? THICKNESS1 { get; set; }
        public System.Decimal? THICKNESS2 { get; set; }
        public System.Decimal? THICKNESS3 { get; set; }
        public System.Decimal? MAXFORCE_W1 { get; set; }
        public System.Decimal? MAXFORCE_W2 { get; set; }
        public System.Decimal? MAXFORCE_W3 { get; set; }
        public System.Decimal? MAXFORCE_F1 { get; set; }
        public System.Decimal? MAXFORCE_F2 { get; set; }
        public System.Decimal? MAXFORCE_F3 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W1 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W2 { get; set; }
        public System.Decimal? ELONGATIONFORCE_W3 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F1 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F2 { get; set; }
        public System.Decimal? ELONGATIONFORCE_F3 { get; set; }

        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.Decimal? FLAMMABILITY_W2 { get; set; }
        public System.Decimal? FLAMMABILITY_W3 { get; set; }
        public System.Decimal? FLAMMABILITY_W4 { get; set; }
        public System.Decimal? FLAMMABILITY_W5 { get; set; }

        public System.Decimal? FLAMMABILITY_F { get; set; }
        public System.Decimal? FLAMMABILITY_F2 { get; set; }
        public System.Decimal? FLAMMABILITY_F3 { get; set; }
        public System.Decimal? FLAMMABILITY_F4 { get; set; }
        public System.Decimal? FLAMMABILITY_F5 { get; set; }

        public System.Decimal? EDGECOMB_W1 { get; set; }
        public System.Decimal? EDGECOMB_W2 { get; set; }
        public System.Decimal? EDGECOMB_W3 { get; set; }
        public System.Decimal? EDGECOMB_F1 { get; set; }
        public System.Decimal? EDGECOMB_F2 { get; set; }
        public System.Decimal? EDGECOMB_F3 { get; set; }
        public System.Decimal? STIFFNESS_W1 { get; set; }
        public System.Decimal? STIFFNESS_W2 { get; set; }
        public System.Decimal? STIFFNESS_W3 { get; set; }
        public System.Decimal? STIFFNESS_F1 { get; set; }
        public System.Decimal? STIFFNESS_F2 { get; set; }
        public System.Decimal? STIFFNESS_F3 { get; set; }
        public System.Decimal? TEAR_W1 { get; set; }
        public System.Decimal? TEAR_W2 { get; set; }
        public System.Decimal? TEAR_W3 { get; set; }
        public System.Decimal? TEAR_F1 { get; set; }
        public System.Decimal? TEAR_F2 { get; set; }
        public System.Decimal? TEAR_F3 { get; set; }
        public System.Decimal? STATIC_AIR1 { get; set; }
        public System.Decimal? STATIC_AIR2 { get; set; }
        public System.Decimal? STATIC_AIR3 { get; set; }
        public System.Decimal? DYNAMIC_AIR1 { get; set; }
        public System.Decimal? DYNAMIC_AIR2 { get; set; }
        public System.Decimal? DYNAMIC_AIR3 { get; set; }
        public System.Decimal? EXPONENT1 { get; set; }
        public System.Decimal? EXPONENT2 { get; set; }
        public System.Decimal? EXPONENT3 { get; set; }
        public System.Decimal? DIMENSCHANGE_W1 { get; set; }
        public System.Decimal? DIMENSCHANGE_W2 { get; set; }
        public System.Decimal? DIMENSCHANGE_W3 { get; set; }
        public System.Decimal? DIMENSCHANGE_F1 { get; set; }
        public System.Decimal? DIMENSCHANGE_F2 { get; set; }
        public System.Decimal? DIMENSCHANGE_F3 { get; set; }
        public System.Decimal? FLEXABRASION_W1 { get; set; }
        public System.Decimal? FLEXABRASION_W2 { get; set; }
        public System.Decimal? FLEXABRASION_W3 { get; set; }
        public System.Decimal? FLEXABRASION_F1 { get; set; }
        public System.Decimal? FLEXABRASION_F2 { get; set; }
        public System.Decimal? FLEXABRASION_F3 { get; set; }
        public System.Decimal? BOW1 { get; set; }
        public System.Decimal? SKEW1 { get; set; }
        public System.String STATUS { get; set; }
        public System.String REMARK { get; set; }
        public System.String APPROVEBY { get; set; }
        public System.DateTime? APPROVEDATE { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.Decimal? BOW2 { get; set; }
        public System.Decimal? BOW3 { get; set; }
        public System.Decimal? SKEW2 { get; set; }
        public System.Decimal? SKEW3 { get; set; }
        public System.Decimal? BENDING_W1 { get; set; }
        public System.Decimal? BENDING_W2 { get; set; }
        public System.Decimal? BENDING_W3 { get; set; }
        public System.Decimal? BENDING_F1 { get; set; }
        public System.Decimal? BENDING_F2 { get; set; }
        public System.Decimal? BENDING_F3 { get; set; }
        public System.Decimal? FLEX_SCOTT_W1 { get; set; }
        public System.Decimal? FLEX_SCOTT_W2 { get; set; }
        public System.Decimal? FLEX_SCOTT_W3 { get; set; }
        public System.Decimal? FLEX_SCOTT_F1 { get; set; }
        public System.Decimal? FLEX_SCOTT_F2 { get; set; }
        public System.Decimal? FLEX_SCOTT_F3 { get; set; }
        public System.Decimal? STATIC_AIR4 { get; set; }
        public System.Decimal? STATIC_AIR5 { get; set; }
        public System.Decimal? STATIC_AIR6 { get; set; }
        public System.String FILENAME { get; set; }
        public System.DateTime? UPLOADDATE { get; set; }
        public System.String UPLOADBY { get; set; }
        public System.Decimal? FINISHLENGTH { get; set; }
        public System.String FINISHINGPROCESS { get; set; }
        public System.String ITEMLOT { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String FINISHINGMC { get; set; }
        public System.String BATCHNO { get; set; }
        public System.String CUSTOMERID { get; set; }
        public System.String PARTNO { get; set; }
    }

    #endregion

    //----------------------//

    #region LAB_GETITEM_LCL_UCL

    public class LAB_GETITEM_LCL_UCL
    {
        //New 7/9/22
        public System.Decimal? USABLE_WIDTH_LCL { get; set; }
        public System.Decimal? USABLE_WIDTH_UCL { get; set; }
        public System.Decimal? TOTALWEIGHT_LCL { get; set; }
        public System.Decimal? TOTALWEIGHT_UCL { get; set; }
        public System.Decimal? NUMTHREADS_W_LCL { get; set; }
        public System.Decimal? NUMTHREADS_W_UCL { get; set; }
        public System.Decimal? NUMTHREADS_F_LCL { get; set; }
        public System.Decimal? NUMTHREADS_F_UCL { get; set; }
        public System.Decimal? MAXFORCE_W_LCL { get; set; }
        public System.Decimal? MAXFORCE_W_UCL { get; set; }
        public System.Decimal? MAXFORCE_F_LCL { get; set; }
        public System.Decimal? MAXFORCE_F_UCL { get; set; }
        public System.Decimal? ELONGATIONFORCE_W_LCL { get; set; }
        public System.Decimal? ELONGATIONFORCE_W_UCL { get; set; }
        public System.Decimal? ELONGATIONFORCE_F_LCL { get; set; }
        public System.Decimal? ELONGATIONFORCE_F_UCL { get; set; }
        public System.Decimal? EDGECOMB_W_LCL { get; set; }
        public System.Decimal? EDGECOMB_W_UCL { get; set; }
        public System.Decimal? EDGECOMB_F_LCL { get; set; }
        public System.Decimal? EDGECOMB_F_UCL { get; set; }
        public System.Decimal? TEAR_W_LCL { get; set; }
        public System.Decimal? TEAR_W_UCL { get; set; }
        public System.Decimal? TEAR_F_LCL { get; set; }
        public System.Decimal? TEAR_F_UCL { get; set; }
        public System.Decimal? STATIC_AIR_LCL { get; set; }
        public System.Decimal? STATIC_AIR_UCL { get; set; }
        public System.Decimal? DYNAMIC_AIR_LCL { get; set; }
        public System.Decimal? DYNAMIC_AIR_UCL { get; set; }
        public System.Decimal? EXPONENT_LCL { get; set; }
        public System.Decimal? EXPONENT_UCL { get; set; }
    }

    #endregion
}
