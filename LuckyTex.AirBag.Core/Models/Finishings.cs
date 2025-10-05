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
    #region Finishing Information

    /// <summary>
    /// Finishing Information
    /// </summary>
    public class FinishingInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Finishing Lot No.
        /// ITEMLOT
        /// </summary>
        public string FinishingLotNo { get; set; }
        /// <summary>
        /// CUSTOMERID
        /// </summary>
        public string CUSTOMERID { get; set; }
        /// <summary>
        /// Gets or sets Item Code (or Grey Code).
        /// ITEMCODE
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Gets or sets Overall Length. This value is the length from finishing process.
        /// FINISHLENGTH
        /// </summary>
        public decimal OverallLength { get; set; }
        /// <summary>
        /// Gets or sets the rest of length on current inspection.
        /// </summary>
        public decimal ActualLength { get; set; }
        /// <summary>
        /// Gets or sets Total Lot that already finished Inspection process.
        /// </summary>
        public decimal TotalIns { get; set; }


        public string PARTNO { get; set; }
        public string FINISHLOT { get; set; }
        public string FINISHINGPROCESS { get; set; }
        public string REPROCESS { get; set; }
        public string SND_BARCODE { get; set; }
        public string ITM_WEAVING { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ CUS_GETLIST
    #region CUS_GETLISTData

    /// <summary>
    /// The CUS_GETLISTData class.
    /// </summary>
    public class CUS_GETLISTData
    {
        #region Public Propeties

        public System.String CUSTOMERID { get; set; }
        public System.String CUSTOMERNAME { get; set; }
        public System.String METHODID { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String CREATEBY { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ CUS_GETITEMGOODBYCUSTOMER
    #region CUS_GETITEMGOODBYCUSTOMERData

    /// <summary>
    /// The CUS_CUS_GETITEMGOODBYCUSTOMERData class.
    /// </summary>
    public class CUS_GETITEMGOODBYCUSTOMERData
    {
        #region Public Propeties

        public System.String CUSTOMERID { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String PARTNO { get; set; }
        public System.String FABRIC { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.String DENSITY_W { get; set; }
        public System.String DENSITY_F { get; set; }
        public System.String WIDTH_ALL { get; set; }
        public System.String WIDTH_PIN { get; set; }
        public System.String WIDTH_COAT { get; set; }
        public System.String TRIM_L { get; set; }
        public System.String TRIM_R { get; set; }
        public System.String FLOPPY_L { get; set; }
        public System.String FLOPPY_R { get; set; }
        public System.String HARDNESS_L { get; set; }
        public System.String HARDNESS_C { get; set; }
        public System.String HARDNESS_R { get; set; }
        public System.String UNWINDER { get; set; }
        public System.String WINDER { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FinishingCustomer
    #region FinishingCustomerData

    /// <summary>
    /// The FinishingCustomerData class.
    /// </summary>
    public class FinishingCustomerData
    {
        #region Public Propeties

        public System.String FINISHINGCUSTOMER { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETITEMGOOD
    #region FINISHING_GETITEMGOODData

    /// <summary>
    /// The FINISHING_GETITEMGOODData class.
    /// </summary>
    public class FINISHING_GETITEMGOODData
    {
        #region Public Propeties

        public System.String CUSTOMERID { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String PARTNO { get; set; }
        public System.String FABRIC { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.String DENSITY_W { get; set; }
        public System.String DENSITY_F { get; set; }
        public System.String WIDTH_ALL { get; set; }
        public System.String WIDTH_PIN { get; set; }
        public System.String WIDTH_COAT { get; set; }
        public System.String TRIM_L { get; set; }
        public System.String TRIM_R { get; set; }
        public System.String FLOPPY_L { get; set; }
        public System.String FLOPPY_R { get; set; }
        public System.String HARDNESS_L { get; set; }
        public System.String HARDNESS_C { get; set; }
        public System.String HARDNESS_R { get; set; }
        public System.String UNWINDER { get; set; }
        public System.String WINDER { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETCOATINGCONDITION
    #region FINISHING_GETCOATINGCONDITIONData

    /// <summary>
    /// The FINISHING_GETCOATINGCONDITIONData class.
    /// </summary>
    public class FINISHING_GETCOATINGCONDITIONData
    {
        #region Public Propeties

        public System.String ITM_CODE { get; set; }
        public System.Decimal? SATURATOR_CHEM { get; set; }
        public System.Decimal? SATURATOR_CHEM_MARGIN { get; set; }
        public System.Decimal? WASHING1 { get; set; }
        public System.Decimal? WASHING1_MARGIN { get; set; }
        public System.Decimal? WASHING2 { get; set; }
        public System.Decimal? WASHING2_MARGIN { get; set; }
        public System.Decimal? HOTFLUE { get; set; }
        public System.Decimal? HOTFLUE_MARGIN { get; set; }
        public System.Decimal? BE_COATWIDTHMAX { get; set; }
        public System.Decimal? BE_COATWIDTHMIN { get; set; }
        public System.Decimal? ROOMTEMP { get; set; }
        public System.Decimal? ROOMTEMP_MARGIN { get; set; }
        public System.Decimal? FANRPM { get; set; }
        public System.Decimal? FANRPM_MARGIN { get; set; }
        public System.Decimal? EXFAN_FRONT_BACK { get; set; }
        public System.Decimal? EXFAN_MARGIN { get; set; }
        public System.Decimal? ANGLEKNIFE { get; set; }
        public System.String BLADENO { get; set; }
        public System.String BLADEDIRECTION { get; set; }
        public System.String PATHLINE { get; set; }
        public System.Decimal? FEEDIN_MAX { get; set; }
        public System.Decimal? TENSION_UP { get; set; }
        public System.Decimal? TENSION_DOWN { get; set; }
        public System.Decimal? TENSION_DOWN_MARGIN { get; set; }
        public System.Decimal? FRAMEWIDTH_FORN { get; set; }
        public System.Decimal? FRAMEWIDTH_TENTER { get; set; }
        public System.String OVERFEED { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? SPEED_MARGIN { get; set; }
        public System.Decimal? WIDTHCOAT { get; set; }
        public System.Decimal? WIDTHCOATALL_MAX { get; set; }
        public System.Decimal? WIDTHCOATALL_MIN { get; set; }
        public System.Decimal? COATINGWEIGTH_MAX { get; set; }
        public System.Decimal? COATINGWEIGTH_MIN { get; set; }
        public System.Decimal? EXFAN_MIDDLE { get; set; }
        public System.String RATIOSILICONE { get; set; }
        public System.String COATNO { get; set; }
        public System.Decimal? FEEDIN_MIN { get; set; }
        public System.Decimal? TENSION_UP_MARGIN { get; set; }
        public System.Decimal? HUMIDITY_MAX { get; set; }
        public System.Decimal? HUMIDITY_MIN { get; set; }

        #endregion
    }

    #endregion

    // FINISHING_COATINGDATABYLOT
    #region FINISHING_COATINGDATABYLOT

    public class FINISHING_COATINGDATABYLOT
    {
        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.String MCNO { get; set; }
        public System.String STATUSFLAG { get; set; }
        public System.Decimal? SATURATOR_CHEM_PV { get; set; }
        public System.Decimal? SATURATOR_CHEM_SP { get; set; }
        public System.Decimal? WASHING1_PV { get; set; }
        public System.Decimal? WASHING1_SP { get; set; }
        public System.Decimal? WASHING2_PV { get; set; }
        public System.Decimal? WASHING2_SP { get; set; }
        public System.Decimal? HOTFLUE_PV { get; set; }
        public System.Decimal? HOTFLUE_SP { get; set; }
        public System.Decimal? BE_COATWIDTH { get; set; }
        public System.Decimal? TEMP1_PV { get; set; }
        public System.Decimal? TEMP1_SP { get; set; }
        public System.Decimal? TEMP2_PV { get; set; }
        public System.Decimal? TEMP2_SP { get; set; }
        public System.Decimal? TEMP3_PV { get; set; }
        public System.Decimal? TEMP3_SP { get; set; }
        public System.Decimal? TEMP4_PV { get; set; }
        public System.Decimal? TEMP4_SP { get; set; }
        public System.Decimal? TEMP5_PV { get; set; }
        public System.Decimal? TEMP5_SP { get; set; }
        public System.Decimal? TEMP6_PV { get; set; }
        public System.Decimal? TEMP6_SP { get; set; }
        public System.Decimal? TEMP7_PV { get; set; }
        public System.Decimal? TEMP7_SP { get; set; }
        public System.Decimal? TEMP8_PV { get; set; }
        public System.Decimal? TEMP8_SP { get; set; }
        public System.Decimal? TEMP9_PV { get; set; }
        public System.Decimal? TEMP9_SP { get; set; }
        public System.Decimal? TEMP10_PV { get; set; }
        public System.Decimal? TEMP10_SP { get; set; }
        public System.Decimal? FANRPM { get; set; }
        public System.Decimal? EXFAN_FRONT_BACK { get; set; }
        public System.Decimal? EXFAN_MIDDLE { get; set; }
        public System.Decimal? ANGLEKNIFE { get; set; }
        public System.String BLADENO { get; set; }
        public System.String BLADEDIRECTION { get; set; }
        public System.Decimal? CYLINDER_TENSIONUP { get; set; }
        public System.Decimal? OPOLE_TENSIONDOWN { get; set; }
        public System.Decimal? FRAMEWIDTH_FORN { get; set; }
        public System.Decimal? FRAMEWIDTH_TENTER { get; set; }
        public System.Decimal? PATHLINE { get; set; }
        public System.Decimal? FEEDIN { get; set; }
        public System.Decimal? OVERFEED { get; set; }
        public System.Decimal? SPEED_PV { get; set; }
        public System.Decimal? SPEED_SP { get; set; }
        public System.Decimal? WIDTHCOAT { get; set; }
        public System.Decimal? WIDTHCOATALL { get; set; }
        public System.String SILICONE_A { get; set; }
        public System.String SILICONE_B { get; set; }
        public System.Decimal? COATINGWEIGTH_L { get; set; }
        public System.Decimal? COATINGWEIGTH_C { get; set; }
        public System.Decimal? COATINGWEIGTH_R { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? CONDITIONDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String SAMPLINGID { get; set; }
        public System.String STARTBY { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.String REMARK { get; set; }
        public System.Decimal? HUMIDITY_BF { get; set; }
        public System.Decimal? HUMIDITY_AF { get; set; }
        public System.String REPROCESS { get; set; }
        public System.Decimal? WEAVLENGTH { get; set; }
        public System.String OPERATOR_GROUP { get; set; }
        public System.Decimal? TEMP1_MIN { get; set; }
        public System.Decimal? TEMP1_MAX { get; set; }
        public System.Decimal? TEMP2_MIN { get; set; }
        public System.Decimal? TEMP2_MAX { get; set; }
        public System.Decimal? TEMP3_MIN { get; set; }
        public System.Decimal? TEMP3_MAX { get; set; }
        public System.Decimal? TEMP4_MIN { get; set; }
        public System.Decimal? TEMP4_MAX { get; set; }
        public System.Decimal? TEMP5_MIN { get; set; }
        public System.Decimal? TEMP5_MAX { get; set; }
        public System.Decimal? TEMP6_MIN { get; set; }
        public System.Decimal? TEMP6_MAX { get; set; }
        public System.Decimal? TEMP7_MIN { get; set; }
        public System.Decimal? TEMP7_MAX { get; set; }
        public System.Decimal? TEMP8_MIN { get; set; }
        public System.Decimal? TEMP8_MAX { get; set; }
        public System.Decimal? TEMP9_MIN { get; set; }
        public System.Decimal? TEMP9_MAX { get; set; }
        public System.Decimal? TEMP10_MIN { get; set; }
        public System.Decimal? TEMP10_MAX { get; set; }
        public System.Decimal? SAT_CHEM_MIN { get; set; }
        public System.Decimal? SAT_CHEM_MAX { get; set; }
        public System.Decimal? WASHING1_MIN { get; set; }
        public System.Decimal? WASHING1_MAX { get; set; }
        public System.Decimal? WASHING2_MIN { get; set; }
        public System.Decimal? WASHING2_MAX { get; set; }
        public System.Decimal? HOTFLUE_MIN { get; set; }
        public System.Decimal? HOTFLUE_MAX { get; set; }
        public System.Decimal? SPEED_MIN { get; set; }
        public System.Decimal? SPEED_MAX { get; set; }
        public System.Decimal? TENSIONUP_MIN { get; set; }
        public System.Decimal? TENSIONUP_MAX { get; set; }
        public System.Decimal? TENSIONDOWN_MIN { get; set; }
        public System.Decimal? TENSIONDOWN_MAX { get; set; }
        public System.String ITM_WEAVING { get; set; }
    }

    #endregion

    #region FINISHING_COATINGPLCDATA

    public class FINISHING_COATINGPLCDATA
    {
        public System.DateTime? STARTDATE { get; set; }
        public System.Decimal? TEMP1_MIN { get; set; }
        public System.Decimal? TEMP1_MAX { get; set; }
        public System.Decimal? TEMP1 { get; set; }
        public System.Decimal? TEMP2_MIN { get; set; }
        public System.Decimal? TEMP2_MAX { get; set; }
        public System.Decimal? TEMP2 { get; set; }
        public System.Decimal? TEMP3_MIN { get; set; }
        public System.Decimal? TEMP3_MAX { get; set; }
        public System.Decimal? TEMP3 { get; set; }
        public System.Decimal? TEMP4_MIN { get; set; }
        public System.Decimal? TEMP4_MAX { get; set; }
        public System.Decimal? TEMP4 { get; set; }
        public System.Decimal? TEMP5_MIN { get; set; }
        public System.Decimal? TEMP5_MAX { get; set; }
        public System.Decimal? TEMP5 { get; set; }
        public System.Decimal? TEMP6_MIN { get; set; }
        public System.Decimal? TEMP6_MAX { get; set; }
        public System.Decimal? TEMP6 { get; set; }
        public System.Decimal? TEMP7_MIN { get; set; }
        public System.Decimal? TEMP7_MAX { get; set; }
        public System.Decimal? TEMP7 { get; set; }
        public System.Decimal? TEMP8_MIN { get; set; }
        public System.Decimal? TEMP8_MAX { get; set; }
        public System.Decimal? TEMP8 { get; set; }
        public System.Decimal? TEMP9_MIN { get; set; }
        public System.Decimal? TEMP9_MAX { get; set; }
        public System.Decimal? TEMP9 { get; set; }
        public System.Decimal? TEMP10_MIN { get; set; }
        public System.Decimal? TEMP10_MAX { get; set; }
        public System.Decimal? TEMP10 { get; set; }
        public System.Decimal? SPEED_MIN { get; set; }
        public System.Decimal? SPEED_MAX { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? SAT_MIN { get; set; }
        public System.Decimal? SAT_MAX { get; set; }
        public System.Decimal? SAT { get; set; }
        public System.Decimal? HOTF_MIN { get; set; }
        public System.Decimal? HOTF_MAX { get; set; }
        public System.Decimal? HOTF { get; set; }
        public System.Decimal? WASH1_MIN { get; set; }
        public System.Decimal? WASH1_MAX { get; set; }
        public System.Decimal? WASH1 { get; set; }
        public System.Decimal? WASH2_MIN { get; set; }
        public System.Decimal? WASH2_MAX { get; set; }
        public System.Decimal? WASH2 { get; set; }
        public System.Decimal? TENUP_MIN { get; set; }
        public System.Decimal? TENUP_MAX { get; set; }
        public System.Decimal? TENUP { get; set; }
        public System.Decimal? TENDOWN_MIN { get; set; }
        public System.Decimal? TENDOWN_MAX { get; set; }
        public System.Decimal? TENDOWN { get; set; }
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETSCOURINGCONDITION
    #region FINISHING_GETSCOURINGCONDITIONData

    /// <summary>
    /// The FINISHING_GETSCOURINGCONDITIONData class.
    /// </summary>
    public class FINISHING_GETSCOURINGCONDITIONData
    {
        #region Public Propeties

        public System.String ITM_CODE { get; set; }
        public System.Decimal? SATURATOR_CHEM { get; set; }
        public System.Decimal? SATURATOR_CHEM_MARGIN { get; set; }
        public System.Decimal? WASHING1 { get; set; }
        public System.Decimal? WASHING1_MARGIN { get; set; }
        public System.Decimal? WASHING2 { get; set; }
        public System.Decimal? WASHING2_MARGIN { get; set; }
        public System.Decimal? HOTFLUE { get; set; }
        public System.Decimal? HOTFLUE_MARGIN { get; set; }
        public System.Decimal? ROOMTEMP { get; set; }
        public System.Decimal? ROOMTEMP_MARGIN { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? SPEED_MARGIN { get; set; }
        public System.Decimal? MAINFRAMEWIDTH { get; set; }
        public System.Decimal? MAINFRAMEWIDTH_MARGIN { get; set; }
        public System.Decimal? WIDTH_BE { get; set; }
        public System.Decimal? WIDTH_BE_MARGIN { get; set; }
        public System.Decimal? WIDTH_AF { get; set; }
        public System.Decimal? WIDTH_AF_MARGIN { get; set; }
        public System.String DENSITY_AF { get; set; }
        public System.Decimal? DENSITY_MARGIN { get; set; }
        public System.String SCOURINGNO { get; set; }
        public System.Decimal? NIPCHEMICAL { get; set; }
        public System.Decimal? NIPROLLWASHER1 { get; set; }
        public System.Decimal? NIPROLLWASHER2 { get; set; }
        public System.Decimal? PIN2PIN { get; set; }
        public System.Decimal? PIN2PIN_MARGIN { get; set; }
        public System.Decimal? HUMIDITY_MAX { get; set; }
        public System.Decimal? HUMIDITY_MIN { get; set; }

        #endregion
    }

    #endregion

    #region FINISHING_SCOURINGDATABYLOT

    public class FINISHING_SCOURINGDATABYLOT
    {
        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.String MCNO { get; set; }
        public System.String STATUSFLAG { get; set; }
        public System.Decimal? SATURATOR_CHEM_PV { get; set; }
        public System.Decimal? SATURATOR_CHEM_SP { get; set; }
        public System.Decimal? WASHING1_PV { get; set; }
        public System.Decimal? WASHING1_SP { get; set; }
        public System.Decimal? WASHING2_PV { get; set; }
        public System.Decimal? WASHING2_SP { get; set; }
        public System.Decimal? HOTFLUE_PV { get; set; }
        public System.Decimal? HOTFLUE_SP { get; set; }
        public System.Decimal? TEMP1_PV { get; set; }
        public System.Decimal? TEMP1_SP { get; set; }
        public System.Decimal? TEMP2_PV { get; set; }
        public System.Decimal? TEMP2_SP { get; set; }
        public System.Decimal? TEMP3_PV { get; set; }
        public System.Decimal? TEMP3_SP { get; set; }
        public System.Decimal? TEMP4_PV { get; set; }
        public System.Decimal? TEMP4_SP { get; set; }
        public System.Decimal? TEMP5_PV { get; set; }
        public System.Decimal? TEMP5_SP { get; set; }
        public System.Decimal? TEMP6_PV { get; set; }
        public System.Decimal? TEMP6_SP { get; set; }
        public System.Decimal? TEMP7_PV { get; set; }
        public System.Decimal? TEMP7_SP { get; set; }
        public System.Decimal? TEMP8_PV { get; set; }
        public System.Decimal? TEMP8_SP { get; set; }

        public System.Decimal? TEMP9_PV { get; set; }
        public System.Decimal? TEMP9_SP { get; set; }
        public System.Decimal? TEMP10_PV { get; set; }
        public System.Decimal? TEMP10_SP { get; set; }

        public System.Decimal? SPEED_PV { get; set; }
        public System.Decimal? SPEED_SP { get; set; }
        public System.Decimal? MAINFRAMEWIDTH { get; set; }
        public System.Decimal? WIDTH_BE { get; set; }
        public System.Decimal? WIDTH_AF { get; set; }
        public System.Decimal? PIN2PIN { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? CONDITIONDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String SAMPLINGID { get; set; }
        public System.String STARTBY { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.String REMARK { get; set; }
        public System.Decimal? HUMIDITY_BF { get; set; }
        public System.Decimal? HUMIDITY_AF { get; set; }
        public System.String REPROCESS { get; set; }
        public System.Decimal? WEAVLENGTH { get; set; }
        public System.String OPERATOR_GROUP { get; set; }
        public System.Decimal? TEMP1_MIN { get; set; }
        public System.Decimal? TEMP1_MAX { get; set; }
        public System.Decimal? TEMP2_MIN { get; set; }
        public System.Decimal? TEMP2_MAX { get; set; }
        public System.Decimal? TEMP3_MIN { get; set; }
        public System.Decimal? TEMP3_MAX { get; set; }
        public System.Decimal? TEMP4_MIN { get; set; }
        public System.Decimal? TEMP4_MAX { get; set; }
        public System.Decimal? TEMP5_MIN { get; set; }
        public System.Decimal? TEMP5_MAX { get; set; }
        public System.Decimal? TEMP6_MIN { get; set; }
        public System.Decimal? TEMP6_MAX { get; set; }
        public System.Decimal? TEMP7_MIN { get; set; }
        public System.Decimal? TEMP7_MAX { get; set; }
        public System.Decimal? TEMP8_MIN { get; set; }
        public System.Decimal? TEMP8_MAX { get; set; }
        public System.Decimal? SAT_CHEM_MIN { get; set; }
        public System.Decimal? SAT_CHEM_MAX { get; set; }
        public System.Decimal? WASHING1_MIN { get; set; }
        public System.Decimal? WASHING1_MAX { get; set; }
        public System.Decimal? WASHING2_MIN { get; set; }
        public System.Decimal? WASHING2_MAX { get; set; }
        public System.Decimal? HOTFLUE_MIN { get; set; }
        public System.Decimal? HOTFLUE_MAX { get; set; }
        public System.Decimal? SPEED_MIN { get; set; }
        public System.Decimal? SPEED_MAX { get; set; }
        public System.String ITM_WEAVING { get; set; }
    }

    #endregion

    #region FINISHING_SCOURINGPLCDATA

    public class FINISHING_SCOURINGPLCDATA
    {
        public System.DateTime? STARTDATE { get; set; }
        public System.Decimal? TEMP1_MIN { get; set; }
        public System.Decimal? TEMP1_MAX { get; set; }
        public System.Decimal? TEMP1 { get; set; }
        public System.Decimal? TEMP2_MIN { get; set; }
        public System.Decimal? TEMP2_MAX { get; set; }
        public System.Decimal? TEMP2 { get; set; }
        public System.Decimal? TEMP3_MIN { get; set; }
        public System.Decimal? TEMP3_MAX { get; set; }
        public System.Decimal? TEMP3 { get; set; }
        public System.Decimal? TEMP4_MIN { get; set; }
        public System.Decimal? TEMP4_MAX { get; set; }
        public System.Decimal? TEMP4 { get; set; }
        public System.Decimal? TEMP5_MIN { get; set; }
        public System.Decimal? TEMP5_MAX { get; set; }
        public System.Decimal? TEMP5 { get; set; }
        public System.Decimal? TEMP6_MIN { get; set; }
        public System.Decimal? TEMP6_MAX { get; set; }
        public System.Decimal? TEMP6 { get; set; }
        public System.Decimal? TEMP7_MIN { get; set; }
        public System.Decimal? TEMP7_MAX { get; set; }
        public System.Decimal? TEMP7 { get; set; }
        public System.Decimal? TEMP8_MIN { get; set; }
        public System.Decimal? TEMP8_MAX { get; set; }
        public System.Decimal? TEMP8 { get; set; }
        public System.Decimal? SPEED_MIN { get; set; }
        public System.Decimal? SPEED_MAX { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? SAT_MIN { get; set; }
        public System.Decimal? SAT_MAX { get; set; }
        public System.Decimal? SAT { get; set; }
        public System.Decimal? HOTF_MIN { get; set; }
        public System.Decimal? HOTF_MAX { get; set; }
        public System.Decimal? HOTF { get; set; }
        public System.Decimal? WASH1_MIN { get; set; }
        public System.Decimal? WASH1_MAX { get; set; }
        public System.Decimal? WASH1 { get; set; }
        public System.Decimal? WASH2_MIN { get; set; }
        public System.Decimal? WASH2_MAX { get; set; }
        public System.Decimal? WASH2 { get; set; }

        public System.Decimal? TEMP9 { get; set; }
        public System.Decimal? TEMP10 { get; set; }
    }

    #endregion

    // เพิ่มใหม่ ใช้ตอนที่ Finish
    #region FINISHING_UPDATESCOURINGDATA

    public class FINISHING_UPDATESCOURINGDATA
    {
        public System.String P_FINISHLOT { get; set; }
        public System.String P_FLAG { get; set; }
        public System.Decimal? P_SAT { get; set; }
        public System.Decimal? P_SAT_MIN { get; set; }
        public System.Decimal? P_SAT_MAX { get; set; }
        public System.Decimal? P_WASHING1 { get; set; }
        public System.Decimal? P_WASHING1_MIN { get; set; }
        public System.Decimal? P_WASHING1_MAX { get; set; }
        public System.Decimal? P_WASHING2 { get; set; }
        public System.Decimal? P_WASHING2_MIN { get; set; }
        public System.Decimal? P_WASHING2_MAX { get; set; }
        public System.Decimal? P_HOTFLUE { get; set; }
        public System.Decimal? P_HOTFLUE_MIN { get; set; }
        public System.Decimal? P_HOTFLUE_MAX { get; set; }
        public System.Decimal? P_TEMP1 { get; set; }
        public System.Decimal? P_TEMP1_MIN { get; set; }
        public System.Decimal? P_TEMP1_MAX { get; set; }
        public System.Decimal? P_TEMP2 { get; set; }
        public System.Decimal? P_TEMP2_MIN { get; set; }
        public System.Decimal? P_TEMP2_MAX { get; set; }
        public System.Decimal? P_TEMP3 { get; set; }
        public System.Decimal? P_TEMP3_MIN { get; set; }
        public System.Decimal? P_TEMP3_MAX { get; set; }
        public System.Decimal? P_TEMP4 { get; set; }
        public System.Decimal? P_TEMP4_MIN { get; set; }
        public System.Decimal? P_TEMP4_MAX { get; set; }
        public System.Decimal? P_TEMP5 { get; set; }
        public System.Decimal? P_TEMP5_MIN { get; set; }
        public System.Decimal? P_TEMP5_MAX { get; set; }
        public System.Decimal? P_TEMP6 { get; set; }
        public System.Decimal? P_TEMP6_MIN { get; set; }
        public System.Decimal? P_TEMP6_MAX { get; set; }
        public System.Decimal? P_TEMP7 { get; set; }
        public System.Decimal? P_TEMP7_MIN { get; set; }
        public System.Decimal? P_TEMP7_MAX { get; set; }
        public System.Decimal? P_TEMP8 { get; set; }
        public System.Decimal? P_TEMP8_MIN { get; set; }
        public System.Decimal? P_TEMP8_MAX { get; set; }
        public System.Decimal? P_SPEED { get; set; }
        public System.Decimal? P_SPEED_MIN { get; set; }
        public System.Decimal? P_SPEED_MAX { get; set; }
        public System.Decimal? P_MAINFRAMEWIDTH { get; set; }
        public System.Decimal? P_WIDTH_BE { get; set; }
        public System.Decimal? P_WIDTH_AF { get; set; }
        public System.Decimal? P_PIN2PIN { get; set; }
        public System.String P_FINISHBY { get; set; }
        public System.DateTime? P_ENDDATE { get; set; }
        public System.Decimal? P_LENGTH1 { get; set; }
        public System.Decimal? P_LENGTH2 { get; set; }
        public System.Decimal? P_LENGTH3 { get; set; }
        public System.Decimal? P_LENGTH4 { get; set; }
        public System.Decimal? P_LENGTH5 { get; set; }
        public System.Decimal? P_LENGTH6 { get; set; }
        public System.Decimal? P_LENGTH7 { get; set; }
        public System.String P_ITMCODE { get; set; }
        public System.String P_WEAVINGLOT { get; set; }
        public System.String P_CUSTOMER { get; set; }
        public System.DateTime? P_STARTDATE { get; set; }
        public System.String P_REMARK { get; set; }
        public System.Decimal? P_HUMID_BF { get; set; }
        public System.Decimal? P_HUMID_AF { get; set; }
        public System.String P_GROUP { get; set; }
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETDRYERCONDITION
    #region FINISHING_GETDRYERCONDITIONData

    /// <summary>
    /// The FINISHING_GETDRYERCONDITIONData class.
    /// </summary>
    public class FINISHING_GETDRYERCONDITIONData
    {
        #region Public Propeties

        public System.String ITM_CODE { get; set; }
        public System.Decimal? WIDTH_BE_HEAT_MAX { get; set; }
        public System.Decimal? WIDTH_BE_HEAT_MIN { get; set; }
        public System.Decimal? ACCPRESURE { get; set; }
        public System.Decimal? ASSTENSION { get; set; }
        public System.Decimal? ACCARIDENSER { get; set; }
        public System.Decimal? CHIFROT { get; set; }
        public System.Decimal? CHIREAR { get; set; }
        public System.Decimal? DRYERTEMP1 { get; set; }
        public System.Decimal? DRYERTEMP1_MARGIN { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? SPEED_MARGIN { get; set; }
        public System.Decimal? STEAMPRESSURE { get; set; }
        public System.Decimal? DRYERUPCIRCUFAN { get; set; }
        public System.Decimal? DRYERDOWNCIRCUFAN { get; set; }
        public System.Decimal? EXHAUSTFAN { get; set; }
        public System.Decimal? WIDTH_AF_HEAT { get; set; }
        public System.Decimal? WIDTH_AF_HEAT_MARGIN { get; set; }
        public System.Decimal? HUMIDITY_MAX { get; set; }
        public System.Decimal? HUMIDITY_MIN { get; set; }

        public System.String MCNO { get; set; }

        public System.Decimal? SATURATOR_CHEM { get; set; }
        public System.Decimal? SATURATOR_CHEM_MARGIN { get; set; }
        public System.Decimal? WASHING1 { get; set; }
        public System.Decimal? WASHING1_MARGIN { get; set; }
        public System.Decimal? WASHING2 { get; set; }
        public System.Decimal? WASHING2_MARGIN { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_CHECKITEMWEAVING
    #region FINISHING_CHECKITEMWEAVINGData

    /// <summary>
    /// The FINISHING_CHECKITEMWEAVINGData class.
    /// </summary>
    public class FINISHING_CHECKITEMWEAVINGData
    {
        #region Public Propeties

        public System.String ITM_CODE { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.String ITM_YARN { get; set; }
        public System.Decimal? ITM_WIDTH { get; set; }
        public System.String ITM_PROC1 { get; set; }
        public System.String ITM_PROC2 { get; set; }
        public System.String ITM_PROC3 { get; set; }
        public System.String ITM_PROC4 { get; set; }
        public System.String ITM_PROC5 { get; set; }
        public System.String ITM_PROC6 { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String CREATEBY { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.String EDITBY { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.Decimal? COREWEIGHT { get; set; }
        public System.Decimal? FULLWEIGHT { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ GETWEAVINGINGDATA
    #region GETWEAVINGINGDATA

    /// <summary>
    /// The GETWEAVINGINGDATA class.
    /// </summary>
    public class GETWEAVINGINGDATA
    {
        #region Public Propeties

        public System.String WEAVINGLOT { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.String LOOMNO { get; set; }
        public System.DateTime? WEAVINGDATE { get; set; }
        public System.String SHIFT { get; set; }
        public System.String REMARK { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.String PREPAREBY { get; set; }
        public System.String WEAVINGNO { get; set; }

       
        public System.String BEAMLOT { get; set; }
        public System.Decimal? DOFFNO { get; set; }
        public System.Decimal? DENSITY_WARP { get; set; }
        public System.Decimal? TENSION { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.String DOFFBY { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? WASTE { get; set; }
        public System.Decimal? DENSITY_WEFT { get; set; }
        public System.String DELETEFLAG { get; set; }
        public System.String DELETEBY { get; set; }
        public System.DateTime? DELETEDATE { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETCOATINGDATA
    #region FINISHING_GETCOATINGDATA

    /// <summary>
    /// The FINISHING_GETCOATINGDATA class.
    /// </summary>
    public class FINISHING_GETCOATINGDATA
    {
        #region Public Propeties

        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.String MCNO { get; set; }
        public System.String STATUSFLAG { get; set; }
        public System.Decimal? SATURATOR_CHEM_PV { get; set; }
        public System.Decimal? SATURATOR_CHEM_SP { get; set; }
        public System.Decimal? WASHING1_PV { get; set; }
        public System.Decimal? WASHING1_SP { get; set; }
        public System.Decimal? WASHING2_PV { get; set; }
        public System.Decimal? WASHING2_SP { get; set; }
        public System.Decimal? HOTFLUE_PV { get; set; }
        public System.Decimal? HOTFLUE_SP { get; set; }
        public System.Decimal? BE_COATWIDTH { get; set; }
        public System.Decimal? TEMP1_PV { get; set; }
        public System.Decimal? TEMP1_SP { get; set; }
        public System.Decimal? TEMP2_PV { get; set; }
        public System.Decimal? TEMP2_SP { get; set; }
        public System.Decimal? TEMP3_PV { get; set; }
        public System.Decimal? TEMP3_SP { get; set; }
        public System.Decimal? TEMP4_PV { get; set; }
        public System.Decimal? TEMP4_SP { get; set; }
        public System.Decimal? TEMP5_PV { get; set; }
        public System.Decimal? TEMP5_SP { get; set; }
        public System.Decimal? TEMP6_PV { get; set; }
        public System.Decimal? TEMP6_SP { get; set; }
        public System.Decimal? TEMP7_PV { get; set; }
        public System.Decimal? TEMP7_SP { get; set; }
        public System.Decimal? TEMP8_PV { get; set; }
        public System.Decimal? TEMP8_SP { get; set; }
        public System.Decimal? TEMP9_PV { get; set; }
        public System.Decimal? TEMP9_SP { get; set; }
        public System.Decimal? TEMP10_PV { get; set; }
        public System.Decimal? TEMP10_SP { get; set; }
        public System.Decimal? FANRPM { get; set; }
        public System.Decimal? EXFAN_FRONT_BACK { get; set; }
        public System.Decimal? EXFAN_MIDDLE { get; set; }
        public System.Decimal? ANGLEKNIFE { get; set; }
        public System.String BLADENO { get; set; }
        public System.String BLADEDIRECTION { get; set; }
        public System.Decimal? CYLINDER_TENSIONUP { get; set; }
        public System.Decimal? OPOLE_TENSIONDOWN { get; set; }
        public System.Decimal? FRAMEWIDTH_FORN { get; set; }
        public System.Decimal? FRAMEWIDTH_TENTER { get; set; }
        public System.Decimal? PATHLINE { get; set; }
        public System.Decimal? FEEDIN { get; set; }
        public System.Decimal? OVERFEED { get; set; }
        public System.Decimal? SPEED_PV { get; set; }
        public System.Decimal? SPEED_SP { get; set; }
        public System.Decimal? WIDTHCOAT { get; set; }
        public System.Decimal? WIDTHCOATALL { get; set; }
        public System.String SILICONE_A { get; set; }
        public System.String SILICONE_B { get; set; }
        public System.Decimal? COATINGWEIGTH_L { get; set; }
        public System.Decimal? COATINGWEIGTH_C { get; set; }
        public System.Decimal? COATINGWEIGTH_R { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? CONDITIONDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String SAMPLINGID { get; set; }
        public System.String STARTBY { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? WEAVINGLENGTH { get; set; }
        public System.String REMARK { get; set; }

        public System.Decimal? HUMIDITY_BF { get; set; }
        public System.Decimal? HUMIDITY_AF { get; set; }
        public System.String OPERATOR_GROUP { get; set; }
        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETCOATINGREPORTDATA
    #region FINISHING_GETCOATINGREPORTDATA

    /// <summary>
    /// The FINISHING_GETCOATINGREPORTDATA class.
    /// </summary>
    public class FINISHING_GETCOATINGREPORTDATA
    {
        #region Public Propeties

        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.String MCNO { get; set; }
        public System.String STATUSFLAG { get; set; }
        public System.Decimal? SATURATOR_CHEM_PV { get; set; }
        public System.Decimal? SATURATOR_CHEM_SP { get; set; }
        public System.Decimal? WASHING1_PV { get; set; }
        public System.Decimal? WASHING1_SP { get; set; }
        public System.Decimal? WASHING2_PV { get; set; }
        public System.Decimal? WASHING2_SP { get; set; }
        public System.Decimal? HOTFLUE_PV { get; set; }
        public System.Decimal? HOTFLUE_SP { get; set; }
        public System.Decimal? BE_COATWIDTH { get; set; }
        public System.Decimal? TEMP1_PV { get; set; }
        public System.Decimal? TEMP1_SP { get; set; }
        public System.Decimal? TEMP2_PV { get; set; }
        public System.Decimal? TEMP2_SP { get; set; }
        public System.Decimal? TEMP3_PV { get; set; }
        public System.Decimal? TEMP3_SP { get; set; }
        public System.Decimal? TEMP4_PV { get; set; }
        public System.Decimal? TEMP4_SP { get; set; }
        public System.Decimal? TEMP5_PV { get; set; }
        public System.Decimal? TEMP5_SP { get; set; }
        public System.Decimal? TEMP6_PV { get; set; }
        public System.Decimal? TEMP6_SP { get; set; }
        public System.Decimal? TEMP7_PV { get; set; }
        public System.Decimal? TEMP7_SP { get; set; }
        public System.Decimal? TEMP8_PV { get; set; }
        public System.Decimal? TEMP8_SP { get; set; }
        public System.Decimal? TEMP9_PV { get; set; }
        public System.Decimal? TEMP9_SP { get; set; }
        public System.Decimal? TEMP10_PV { get; set; }
        public System.Decimal? TEMP10_SP { get; set; }
        public System.Decimal? FANRPM { get; set; }
        public System.Decimal? EXFAN_FRONT_BACK { get; set; }
        public System.Decimal? EXFAN_MIDDLE { get; set; }
        public System.Decimal? ANGLEKNIFE { get; set; }
        public System.String BLADENO { get; set; }
        public System.String BLADEDIRECTION { get; set; }
        public System.Decimal? CYLINDER_TENSIONUP { get; set; }
        public System.Decimal? OPOLE_TENSIONDOWN { get; set; }
        public System.Decimal? FRAMEWIDTH_FORN { get; set; }
        public System.Decimal? FRAMEWIDTH_TENTER { get; set; }
        public System.Decimal? PATHLINE { get; set; }
        public System.Decimal? FEEDIN { get; set; }
        public System.Decimal? OVERFEED { get; set; }
        public System.Decimal? SPEED_PV { get; set; }
        public System.Decimal? SPEED_SP { get; set; }
        public System.Decimal? WIDTHCOAT { get; set; }
        public System.Decimal? WIDTHCOATALL { get; set; }
        public System.String SILICONE_A { get; set; }
        public System.String SILICONE_B { get; set; }
        public System.Decimal? COATINGWEIGTH_L { get; set; }
        public System.Decimal? COATINGWEIGTH_C { get; set; }
        public System.Decimal? COATINGWEIGTH_R { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? CONDITIONDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String SAMPLINGID { get; set; }
        public System.String STARTBY { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.String PARTNO { get; set; }
        public System.Decimal? FINISHLENGTH { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? INPUTLENGTH { get; set; }

        public System.String REMARK { get; set; }
        public System.Decimal? HUMIDITY_BF { get; set; }
        public System.Decimal? HUMIDITY_AF { get; set; }

        public System.String OPERATOR_GROUP { get; set; }

        public System.String REPROCESS { get; set; }
        public System.Decimal? TEMP1_MIN { get; set; }
        public System.Decimal? TEMP1_MAX { get; set; }
        public System.Decimal? TEMP2_MIN { get; set; }
        public System.Decimal? TEMP2_MAX { get; set; }
        public System.Decimal? TEMP3_MIN { get; set; }
        public System.Decimal? TEMP3_MAX { get; set; }
        public System.Decimal? TEMP4_MIN { get; set; }
        public System.Decimal? TEMP4_MAX { get; set; }
        public System.Decimal? TEMP5_MIN { get; set; }
        public System.Decimal? TEMP5_MAX { get; set; }
        public System.Decimal? TEMP6_MIN { get; set; }
        public System.Decimal? TEMP6_MAX { get; set; }
        public System.Decimal? TEMP7_MIN { get; set; }
        public System.Decimal? TEMP7_MAX { get; set; }
        public System.Decimal? TEMP8_MIN { get; set; }
        public System.Decimal? TEMP8_MAX { get; set; }
        public System.Decimal? TEMP9_MIN { get; set; }
        public System.Decimal? TEMP9_MAX { get; set; }
        public System.Decimal? TEMP10_MIN { get; set; }
        public System.Decimal? TEMP10_MAX { get; set; }
        public System.Decimal? SAT_CHEM_MIN { get; set; }
        public System.Decimal? SAT_CHEM_MAX { get; set; }
        public System.Decimal? WASHING1_MIN { get; set; }
        public System.Decimal? WASHING1_MAX { get; set; }
        public System.Decimal? WASHING2_MIN { get; set; }
        public System.Decimal? WASHING2_MAX { get; set; }
        public System.Decimal? HOTFLUE_MIN { get; set; }
        public System.Decimal? HOTFLUE_MAX { get; set; }
        public System.Decimal? SPEED_MIN { get; set; }
        public System.Decimal? SPEED_MAX { get; set; }
        public System.Decimal? TENSIONUP_MIN { get; set; }
        public System.Decimal? TENSIONUP_MAX { get; set; }
        public System.Decimal? TENSIONDOWN_MIN { get; set; }
        public System.Decimal? TENSIONDOWN_MAX { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETSCOURINGDATA
    #region FINISHING_GETSCOURINGDATA

    /// <summary>
    /// The FINISHING_GETSCOURINGDATA class.
    /// </summary>
    public class FINISHING_GETSCOURINGDATA
    {
        #region Public Propeties

        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.String MCNO { get; set; }
        public System.String STATUSFLAG { get; set; }
        public System.Decimal? SATURATOR_CHEM_PV { get; set; }
        public System.Decimal? SATURATOR_CHEM_SP { get; set; }
        public System.Decimal? WASHING1_PV { get; set; }
        public System.Decimal? WASHING1_SP { get; set; }
        public System.Decimal? WASHING2_PV { get; set; }
        public System.Decimal? WASHING2_SP { get; set; }
        public System.Decimal? HOTFLUE_PV { get; set; }
        public System.Decimal? HOTFLUE_SP { get; set; }
        public System.Decimal? TEMP1_PV { get; set; }
        public System.Decimal? TEMP1_SP { get; set; }
        public System.Decimal? TEMP2_PV { get; set; }
        public System.Decimal? TEMP2_SP { get; set; }
        public System.Decimal? TEMP3_PV { get; set; }
        public System.Decimal? TEMP3_SP { get; set; }
        public System.Decimal? TEMP4_PV { get; set; }
        public System.Decimal? TEMP4_SP { get; set; }
        public System.Decimal? TEMP5_PV { get; set; }
        public System.Decimal? TEMP5_SP { get; set; }
        public System.Decimal? TEMP6_PV { get; set; }
        public System.Decimal? TEMP6_SP { get; set; }
        public System.Decimal? TEMP7_PV { get; set; }
        public System.Decimal? TEMP7_SP { get; set; }
        public System.Decimal? TEMP8_PV { get; set; }
        public System.Decimal? TEMP8_SP { get; set; }

        public System.Decimal? TEMP9_PV { get; set; }
        public System.Decimal? TEMP9_SP { get; set; }
        public System.Decimal? TEMP10_PV { get; set; }
        public System.Decimal? TEMP10_SP { get; set; }

        public System.Decimal? SPEED_PV { get; set; }
        public System.Decimal? SPEED_SP { get; set; }
        public System.Decimal? MAINFRAMEWIDTH { get; set; }
        public System.Decimal? WIDTH_BE { get; set; }
        public System.Decimal? WIDTH_AF { get; set; }
        public System.Decimal? PIN2PIN { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? CONDITIONDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String SAMPLINGID { get; set; }
        public System.String STARTBY { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? WEAVINGLENGTH { get; set; }
        public System.String REMARK { get; set; }

        public System.Decimal? HUMIDITY_BF { get; set; }
        public System.Decimal? HUMIDITY_AF { get; set; }
        public System.String OPERATOR_GROUP { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETSCOURINGREPORTDATA
    #region FINISHING_GETSCOURINGREPORTDATA

    /// <summary>
    /// The FINISHING_GETSCOURINGREPORTDATA class.
    /// </summary>
    public class FINISHING_GETSCOURINGREPORTDATA
    {
        #region Public Propeties

        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.String MCNO { get; set; }
        public System.String STATUSFLAG { get; set; }
        public System.Decimal? SATURATOR_CHEM_PV { get; set; }
        public System.Decimal? SATURATOR_CHEM_SP { get; set; }
        public System.Decimal? WASHING1_PV { get; set; }
        public System.Decimal? WASHING1_SP { get; set; }
        public System.Decimal? WASHING2_PV { get; set; }
        public System.Decimal? WASHING2_SP { get; set; }
        public System.Decimal? HOTFLUE_PV { get; set; }
        public System.Decimal? HOTFLUE_SP { get; set; }
        public System.Decimal? TEMP1_PV { get; set; }
        public System.Decimal? TEMP1_SP { get; set; }
        public System.Decimal? TEMP2_PV { get; set; }
        public System.Decimal? TEMP2_SP { get; set; }
        public System.Decimal? TEMP3_PV { get; set; }
        public System.Decimal? TEMP3_SP { get; set; }
        public System.Decimal? TEMP4_PV { get; set; }
        public System.Decimal? TEMP4_SP { get; set; }
        public System.Decimal? TEMP5_PV { get; set; }
        public System.Decimal? TEMP5_SP { get; set; }
        public System.Decimal? TEMP6_PV { get; set; }
        public System.Decimal? TEMP6_SP { get; set; }
        public System.Decimal? TEMP7_PV { get; set; }
        public System.Decimal? TEMP7_SP { get; set; }
        public System.Decimal? TEMP8_PV { get; set; }
        public System.Decimal? TEMP8_SP { get; set; }

        public System.Decimal? TEMP9_PV { get; set; }
        public System.Decimal? TEMP9_SP { get; set; }
        public System.Decimal? TEMP10_PV { get; set; }
        public System.Decimal? TEMP10_SP { get; set; }

        public System.Decimal? SPEED_PV { get; set; }
        public System.Decimal? SPEED_SP { get; set; }
        public System.Decimal? MAINFRAMEWIDTH { get; set; }
        public System.Decimal? WIDTH_BE { get; set; }
        public System.Decimal? WIDTH_AF { get; set; }
        public System.Decimal? PIN2PIN { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? CONDITIONDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String SAMPLINGID { get; set; }
        public System.String STARTBY { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.String PARTNO { get; set; }
        public System.Decimal? FINISHLENGTH { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? INPUTLENGTH { get; set; }

        public System.String REMARK { get; set; }
        public System.Decimal? HUMIDITY_BF { get; set; }
        public System.Decimal? HUMIDITY_AF { get; set; }

        public System.String OPERATOR_GROUP { get; set; }

        public System.String REPROCESS { get; set; }
        public System.Decimal? TEMP1_MIN { get; set; }
        public System.Decimal? TEMP1_MAX { get; set; }
        public System.Decimal? TEMP2_MIN { get; set; }
        public System.Decimal? TEMP2_MAX { get; set; }
        public System.Decimal? TEMP3_MIN { get; set; }
        public System.Decimal? TEMP3_MAX { get; set; }
        public System.Decimal? TEMP4_MIN { get; set; }
        public System.Decimal? TEMP4_MAX { get; set; }
        public System.Decimal? TEMP5_MIN { get; set; }
        public System.Decimal? TEMP5_MAX { get; set; }
        public System.Decimal? TEMP6_MIN { get; set; }
        public System.Decimal? TEMP6_MAX { get; set; }
        public System.Decimal? TEMP7_MIN { get; set; }
        public System.Decimal? TEMP7_MAX { get; set; }
        public System.Decimal? TEMP8_MIN { get; set; }
        public System.Decimal? TEMP8_MAX { get; set; }

        public System.Decimal? TEMP9_MIN { get; set; }
        public System.Decimal? TEMP9_MAX { get; set; }
        public System.Decimal? TEMP10_MIN { get; set; }
        public System.Decimal? TEMP10_MAX { get; set; }

        public System.Decimal? SAT_CHEM_MIN { get; set; }
        public System.Decimal? SAT_CHEM_MAX { get; set; }
        public System.Decimal? WASHING1_MIN { get; set; }
        public System.Decimal? WASHING1_MAX { get; set; }
        public System.Decimal? WASHING2_MIN { get; set; }
        public System.Decimal? WASHING2_MAX { get; set; }
        public System.Decimal? HOTFLUE_MIN { get; set; }
        public System.Decimal? HOTFLUE_MAX { get; set; }
        public System.Decimal? SPEED_MIN { get; set; }
        public System.Decimal? SPEED_MAX { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETDRYERDATA
    #region FINISHING_GETDRYERDATA

    /// <summary>
    /// The FINISHING_GETDRYERDATA class.
    /// </summary>
    public class FINISHING_GETDRYERDATA
    {
        #region Public Propeties

        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.String MCNO { get; set; }
        public System.String STATUSFLAG { get; set; }
        public System.Decimal? WIDTH_BE_HEAT { get; set; }
        public System.Decimal? ACCPRESURE { get; set; }
        public System.Decimal? ASSTENSION { get; set; }
        public System.Decimal? ACCARIDENSER { get; set; }
        public System.Decimal? CHIFROT { get; set; }
        public System.Decimal? CHIREAR { get; set; }
        public System.Decimal? DRYERTEMP1_PV { get; set; }
        public System.Decimal? DRYERTEMP1_SP { get; set; }
        public System.Decimal? SPEED_PV { get; set; }
        public System.Decimal? SPEED_SP { get; set; }
        public System.Decimal? STEAMPRESSURE { get; set; }
        public System.Decimal? DRYERCIRCUFAN { get; set; }
        public System.Decimal? EXHAUSTFAN { get; set; }
        public System.Decimal? WIDTH_AF_HEAT { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? CONDITIONDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String SAMPLINGID { get; set; }
        public System.String STARTBY { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? WEAVINGLENGTH { get; set; }
        public System.String REMARK { get; set; }

        public System.Decimal? HUMIDITY_BF { get; set; }
        public System.Decimal? HUMIDITY_AF { get; set; }
        public System.String OPERATOR_GROUP { get; set; }

        public System.Decimal? SATURATOR_PV { get; set; }
        public System.Decimal? SATURATOR_SP { get; set; }
        public System.Decimal? WASHING1_PV { get; set; }
        public System.Decimal? WASHING1_SP { get; set; }
        public System.Decimal? WASHING2_PV { get; set; }
        public System.Decimal? WASHING2_SP { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETDRYERREPORTDATA
    #region FINISHING_GETDRYERREPORTDATA

    /// <summary>
    /// The FINISHING_GETDRYERREPORTDATA class.
    /// </summary>
    public class FINISHING_GETDRYERREPORTDATA
    {
        #region Public Propeties

        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.String MCNO { get; set; }
        public System.String STATUSFLAG { get; set; }
        public System.Decimal? WIDTH_BE_HEAT { get; set; }
        public System.Decimal? ACCPRESURE { get; set; }
        public System.Decimal? ASSTENSION { get; set; }
        public System.Decimal? ACCARIDENSER { get; set; }
        public System.Decimal? CHIFROT { get; set; }
        public System.Decimal? CHIREAR { get; set; }
        public System.Decimal? DRYERTEMP1_PV { get; set; }
        public System.Decimal? DRYERTEMP1_SP { get; set; }
        public System.Decimal? SPEED_PV { get; set; }
        public System.Decimal? SPEED_SP { get; set; }
        public System.Decimal? STEAMPRESSURE { get; set; }
        public System.Decimal? DRYERCIRCUFAN { get; set; }
        public System.Decimal? EXHAUSTFAN { get; set; }
        public System.Decimal? WIDTH_AF_HEAT { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? CONDITIONDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String SAMPLINGID { get; set; }
        public System.String STARTBY { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.String PARTNO { get; set; }
        public System.Decimal? FINISHLENGTH { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? INPUTLENGTH { get; set; }

        public System.String REMARK { get; set; }
        public System.Decimal? HUMIDITY_BF { get; set; }
        public System.Decimal? HUMIDITY_AF { get; set; }

        public System.String OPERATOR_GROUP { get; set; }

        public System.Decimal? HOTFLUE_MIN { get; set; }
        public System.Decimal? HOTFLUE_MAX { get; set; }
        public System.Decimal? SPEED_MIN { get; set; }
        public System.Decimal? SPEED_MAX { get; set; }

        public System.Decimal? SATURATOR_PV { get; set; }
        public System.Decimal? SATURATOR_SP { get; set; }
        public System.Decimal? WASHING1_PV { get; set; }
        public System.Decimal? WASHING1_SP { get; set; }
        public System.Decimal? WASHING2_PV { get; set; }
        public System.Decimal? WASHING2_SP { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_DRYERDATABYLOT
    #region FINISHING_DRYERDATABYLOT 

    public class FINISHING_DRYERDATABYLOT
    {
        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.String MCNO { get; set; }
        public System.String STATUSFLAG { get; set; }
        public System.Decimal? WIDTH_BE_HEAT { get; set; }
        public System.Decimal? ACCPRESURE { get; set; }
        public System.Decimal? ASSTENSION { get; set; }
        public System.Decimal? ACCARIDENSER { get; set; }
        public System.Decimal? CHIFROT { get; set; }
        public System.Decimal? CHIREAR { get; set; }
        public System.Decimal? DRYERTEMP1_PV { get; set; }
        public System.Decimal? DRYERTEMP1_SP { get; set; }
        public System.Decimal? SPEED_PV { get; set; }
        public System.Decimal? SPEED_SP { get; set; }
        public System.Decimal? STEAMPRESSURE { get; set; }
        public System.Decimal? DRYERCIRCUFAN { get; set; }
        public System.Decimal? EXHAUSTFAN { get; set; }
        public System.Decimal? WIDTH_AF_HEAT { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? CONDITIONDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String SAMPLINGID { get; set; }
        public System.String STARTBY { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.String REMARK { get; set; }
        public System.Decimal? HUMIDITY_BF { get; set; }
        public System.Decimal? HUMIDITY_AF { get; set; }
        public System.String REPROCESS { get; set; }
        public System.Decimal? WEAVLENGTH { get; set; }
        public System.String OPERATOR_GROUP { get; set; }
        public System.Decimal? HOTFLUE_MIN { get; set; }
        public System.Decimal? HOTFLUE_MAX { get; set; }
        public System.Decimal? SPEED_MIN { get; set; }
        public System.Decimal? SPEED_MAX { get; set; }
        public System.String ITM_WEAVING { get; set; }


    }

    #endregion

    #region FINISHING_DRYERPLCDATA

    public class FINISHING_DRYERPLCDATA
    {
        public System.DateTime? STARTDATE { get; set; }
        public System.Decimal? HOTF_MIN { get; set; }
        public System.Decimal? HOTF_MAX { get; set; }
        public System.Decimal? HOTF { get; set; }
        public System.Decimal? SPEED_MIN { get; set; }
        public System.Decimal? SPEED_MAX { get; set; }
        public System.Decimal? SPEED { get; set; }
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ GETCURRENTINSDATA
    #region GETCURRENTINSDATA 

    public class GETCURRENTINSDATA
    {
        public System.Decimal? ACTUALLENGTH { get; set; }
        public System.Decimal? TOTALINS { get; set; }
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ GETSAMPLINGSHEET
    #region FINISHING_GETSAMPLINGSHEET 

    public class FINISHING_GETSAMPLINGSHEET
    {
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String CREATEBY { get; set; }
        public System.String PRODUCTID { get; set; }
        public System.Decimal? SAMPLING_WIDTH { get; set; }
        public System.Decimal? SAMPLING_LENGTH { get; set; }
        public System.String PROCESS { get; set; }
        public System.String REMARK { get; set; }
        public System.String FABRICTYPE { get; set; }
        public System.String RETESTFLAG { get; set; }
        public System.String PRODUCTNAME { get; set; }
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_INPROCESSLIST
    #region FINISHING_INPROCESSLIST

    public class FINISHING_INPROCESSLIST
    {
        public System.String MCNAME { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.String STATUS { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.String STARTBY { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.String OPERATOR_GROUP { get; set; }
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ MACHINE
    #region GETMACHINELISTBYPROCESSID

    public class GETMACHINELISTBYPROCESSID
    {
        public System.String PROCESSDESCRIPTION { get; set; }
        public System.String MACHINEID { get; set; }
        public System.String MCNAME { get; set; }
    }

    #endregion

    #region FINISHING_SEARCHFINISHRECORD

    public class FINISHING_SEARCHFINISHRECORD
    {
        public System.Int32 RowNo { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.String ITM_CODE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String STARTBY { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.String MCNO { get; set; }
        public System.String MC { get; set; }
        public System.Decimal? WEAVLENGTH { get; set; }
        public System.Decimal? WIDTH_BE { get; set; }
        public System.Decimal? WIDTH_AF { get; set; }
        public System.String OPERATOR_GROUP { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.Decimal? TOTALLENGTH { get; set; }

        public System.String FinishingLength { get; set; }

        //New 17/07/19
        public System.String PRODUCTIONTYPE { get; set; }
    }

    #endregion

    #region Finishing Session

    /// <summary>
    /// Finishing Session.
    /// </summary>
    [Serializable]
    public class FinishingSession
    {
        #region Internal Variables

        private FinishingMCItem _machine = null;
        private LogInResult _currUser = null;

        private string _operator = string.Empty;

        private string _finishingId = string.Empty; // PK.
        private string _finishingLotNo = string.Empty;
        private string _itemCode = string.Empty;
        private string _customer = string.Empty;
        private string _WEAVINGLOT = string.Empty;

        private string _MCNO = string.Empty;
        private string _PRODUCTTYPEID = string.Empty;
        private string _flag = string.Empty;
        private string _FINISHLOT = string.Empty;

        private decimal? _SATURATOR_PV = null;
        private decimal? _SATURATOR_SP = null;
        private decimal? _WASHING1_PV = null;
        private decimal? _WASHING1_SP = null;
        private decimal? _WASHING2_PV = null;
        private decimal? _WASHING2_SP = null;
        private decimal? _HOTFLUE_PV = null;
        private decimal? _HOTFLUE_SP = null;
        private decimal? _TEMP1_PV = null;
        private decimal? _TEMP1_SP = null;
        private decimal? _TEMP2_PV = null;
        private decimal? _TEMP2_SP = null;
        private decimal? _TEMP3_PV = null;
        private decimal? _TEMP3_SP = null;
        private decimal? _TEMP4_PV = null;
        private decimal? _TEMP4_SP = null;
        private decimal? _TEMP5_PV = null;
        private decimal? _TEMP5_SP = null;
        private decimal? _TEMP6_PV = null;
        private decimal? _TEMP6_SP = null;
        private decimal? _TEMP7_PV = null;
        private decimal? _TEMP7_SP = null;
        private decimal? _TEMP8_PV = null;
        private decimal? _TEMP8_SP = null;
        private decimal? _TEMP9_PV = null;
        private decimal? _TEMP9_SP = null;
        private decimal? _TEMP10_PV = null;
        private decimal? _TEMP10_SP = null;

        private decimal? _SPEED_PV = null;
        private decimal? _SPEED_SP = null;
        private decimal? _MAINFRAMEWIDTH = null;
        private decimal? _WIDTH_BE = null;
        private decimal? _WIDTH_AF = null;
        private decimal? _PIN2PIN = null;
        private decimal? _LENGTH1 = null;
        private decimal? _LENGTH2 = null;
        private decimal? _LENGTH3 = null;
        private decimal? _LENGTH4 = null;
        private decimal? _LENGTH5 = null;
        private decimal? _LENGTH6 = null;
        private decimal? _LENGTH7 = null;

        private decimal? _ACCARIDENSER = null;
        private decimal? _ACCPRESURE = null;
        private decimal? _ASSTENSION = null;
        private decimal? _CHIFROT = null;
        private decimal? _CHIREAR = null;
        private decimal? _DRYCIRCUFAN = null;
        private decimal? _EXHAUSTFAN = null;

        private decimal? _STEAMPRESURE = null;
        private decimal? _WIDTHAFHEAT = null;
        private decimal? _WIDTHBEHEAT = null;
        private DateTime? _STARTDATE = null;

        private decimal? _BE_COATWIDTH = null;
        private decimal? _FANRPM = null;
        private decimal? _EXFAN_FRONT_BACK = null;
        private decimal? _EXFAN_MIDDLE = null;
        private decimal? _ANGLEKNIFE = null;
        private string _BLADENO = string.Empty;
        private string _BLADEDIRECTION = string.Empty;
        private decimal? _CYLINDER_TENSIONUP = null;
        private decimal? _OPOLE_TENSIONDOWN = null;
        //private decimal? _FRAMEWIDTH_FORN = null;
        //private decimal? _FRAMEWIDTH_TENTER = null;
        private decimal? _PATHLINE = null;
        private decimal? _FEEDIN = null;
        private decimal? _OVERFEED = null;

        private decimal? _WIDTHCOAT = null;
        private decimal? _WIDTHCOATALL = null;
        private decimal? _COATINGWEIGTH_L = null;
        private decimal? _COATINGWEIGTH_C = null;
        private decimal? _COATINGWEIGTH_R = null;

        private decimal? _TENSIONUP = null;

        private decimal? _TENSIONDOWN = null;


        private decimal? _FORN = null;
        private decimal? _TENTER = null;
        private string _SILICONE_A = string.Empty;
        private string _SILICONE_B = string.Empty;
        private decimal? _CWL = null;
        private decimal? _CWC = null;
        private decimal? _CWR = null;

        public decimal? _HUMIDITY_BF = null;
        public decimal? _HUMIDITY_AF = null;

        public string _REPROCESS = string.Empty;
        public decimal? _WEAVLENGTH = null;
        public string _OPERATOR_GROUP = string.Empty;

        private decimal? _SAT = null;
        private decimal? _SAT_MIN = null;
        private decimal? _SAT_MAX = null;
        private decimal? _WASHING1 = null;
        private decimal? _WASHING1_MIN = null;
        private decimal? _WASHING1_MAX = null;
        private decimal? _WASHING2 = null;
        private decimal? _WASHING2_MIN = null;
        private decimal? _WASHING2_MAX = null;
        private decimal? _HOTFLUE = null;
        private decimal? _HOTFLUE_MIN = null;
        private decimal? _HOTFLUE_MAX = null;
        private decimal? _TEMP1 = null;
        private decimal? _TEMP1_MIN = null;
        private decimal? _TEMP1_MAX = null;
        private decimal? _TEMP2 = null;
        private decimal? _TEMP2_MIN = null;
        private decimal? _TEMP2_MAX = null;
        private decimal? _TEMP3 = null;
        private decimal? _TEMP3_MIN = null;
        private decimal? _TEMP3_MAX = null;
        private decimal? _TEMP4 = null;
        private decimal? _TEMP4_MIN = null;
        private decimal? _TEMP4_MAX = null;
        private decimal? _TEMP5 = null;
        private decimal? _TEMP5_MIN = null;
        private decimal? _TEMP5_MAX = null;
        private decimal? _TEMP6 = null;
        private decimal? _TEMP6_MIN = null;
        private decimal? _TEMP6_MAX = null;
        private decimal? _TEMP7 = null;
        private decimal? _TEMP7_MIN = null;
        private decimal? _TEMP7_MAX = null;
        private decimal? _TEMP8 = null;
        private decimal? _TEMP8_MIN = null;
        private decimal? _TEMP8_MAX = null;
        private decimal? _TEMP9 = null;
        private decimal? _TEMP9_MIN = null;
        private decimal? _TEMP9_MAX = null;
        private decimal? _TEMP10 = null;
        private decimal? _TEMP10_MIN = null;
        private decimal? _TEMP10_MAX = null;

        private decimal? _SPEED = null;
        private decimal? _SPEED_MIN = null;
        private decimal? _SPEED_MAX = null;
        private DateTime? _ENDDATE = null;
        private string _REMARK = string.Empty;


        private decimal? _TENUP_MIN = null;
        private decimal? _TENUP_MAX = null;

        private decimal? _TENDOWN_MIN = null;
        private decimal? _TENDOWN_MAX = null;
        

        private string _CONDITIONBY = string.Empty;
        private DateTime? _CONDITONDATE = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FinishingSession()
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

        #region Public Methods

        #region Init

        /// <summary>
        /// Init current Session.
        /// </summary>
        /// <param name="machine">The selected machine.</param>
        /// <param name="currUser">The current user that operate machine.</param>
        public void Init(FinishingMCItem machine, LogInResult currUser)
        {
            _machine = machine;
            _currUser = currUser;

            //this.New(); // Reset all data.
            // Load master datasource
            //LoadDefectCodes();
        }

        #endregion

        #region Main Operations

        public void Resume(string finLotNo, string itemCode, string insLotNo,
            string customer,
            string insId)
        {
            _finishingLotNo = finLotNo;
            _itemCode = itemCode;
            _WEAVINGLOT = insLotNo;
            _customer = customer;
            _finishingId = insId;


            if (!string.IsNullOrWhiteSpace(_finishingLotNo) &&
                !string.IsNullOrWhiteSpace(_itemCode) &&
                !string.IsNullOrWhiteSpace(_WEAVINGLOT))
            {

                // Raise event.
                RaiseStateChanged();
            }
        }

        #endregion

        // ใช้สำหรับ Load ข้อมูลใส่ ComboBox
        #region Load Combo

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CUS_GETLISTData> GetCUS_GETLIST()
        {
            List<CUS_GETLISTData> results = MasterDataService.Instance
                .GetCUS_GETLISTDataList();

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusID"></param>
        /// <returns></returns>
        public List<CUS_GETITEMGOODBYCUSTOMERData> GetCUS_GETITEMGOODBYCUSTOMER(string cusID)
        {
            List<CUS_GETITEMGOODBYCUSTOMERData> results = MasterDataService.Instance
                .GetCUS_GETITEMGOODBYCUSTOMERDataList(cusID);

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FinishingCustomerData> Finishing_GetCustomerList()
        {
            List<FinishingCustomerData> results = MasterDataService.Instance
                .GetFinishingCustomerDataList();

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusID"></param>
        /// <returns></returns>
        public List<FINISHING_GETITEMGOODData> GetFINISHING_GETITEMGOOD(string cusID)
        {
            List<FINISHING_GETITEMGOODData> results = MasterDataService.Instance
                .GetFINISHING_GETITEMGOODDataList(cusID);

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_PROCESSID"></param>
        /// <returns></returns>
        public List<GETMACHINELISTBYPROCESSID> GetGETMACHINELISTBYPROCESSID(string P_PROCESSID)
        {
            List<GETMACHINELISTBYPROCESSID> results = FinishingDataService.Instance
                .GETMACHINELISTBYPROCESSID(P_PROCESSID);

            return results;
        }

        public List<ITM_GETITEMCODELIST> GetItemCodeData()
        {
            List<ITM_GETITEMCODELIST> results = FinishingDataService.Instance
                .ITM_GETITEMCODELIST();

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

        public List<FINISHING_SCOURINGDATABYLOT> GetFINISHING_SCOURINGDATABYLOT(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_SCOURINGDATABYLOT> results = CoatingDataService.Instance
                .GetFINISHING_SCOURINGDATABYLOT(P_MCNO, P_WEAVINGLOT);

            return results;
        }

        public List<FINISHING_DRYERPLCDATA> GetFINISHING_DRYERPLCDATA(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_DRYERPLCDATA> results = CoatingDataService.Instance
                .FINISHING_DRYERPLCDATA(P_MCNO, P_WEAVINGLOT);

            return results;
        }

        // ใช้สำหรับ Load ข้อมูล FINISHING DRYERCONDITION
        #region Load finishing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itm_code"></param>
        /// <returns></returns>
        public List<FINISHING_GETDRYERCONDITIONData> GetFINISHING_GETDRYERCONDITION(string itm_code, string mcno)
        {
            List<FINISHING_GETDRYERCONDITIONData> results = CoatingDataService.Instance
                .GetFINISHING_GETDRYERCONDITIONDataList(itm_code, mcno);

            return results;
        }

        #endregion

        // ใช้สำหรับ Load ข้อมูล FINISHING COATINGCONDITION
        #region Load finishing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itm_code"></param>
        /// <param name="CoatNo"></param>
        /// <returns></returns>
        public List<FINISHING_GETCOATINGCONDITIONData> GetFINISHING_GETCOATINGCONDITION(string itm_code, string CoatNo)
        {
            List<FINISHING_GETCOATINGCONDITIONData> results = CoatingDataService.Instance
                .GetFINISHING_GETCOATINGCONDITIONDataList(itm_code, CoatNo);

            return results;
        }

        #endregion

        // ใช้สำหรับ Load ข้อมูล FINISHING CHECKITEMWEAVING
        #region Load finishing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itm_code"></param>
        /// <param name="itm_weaving"></param>
        /// <returns></returns>
        public List<FINISHING_CHECKITEMWEAVINGData> GetFINISHING_CHECKITEMWEAVING(string itm_code, string itm_weaving)
        {
            List<FINISHING_CHECKITEMWEAVINGData> results = CoatingDataService.Instance
                .FINISHING_CHECKITEMWEAVINGList(itm_code, itm_weaving);

            return results;
        }

        #endregion

        // ใช้สำหรับ Load ข้อมูล WeavingingData
        #region Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WEAVINGLOT"></param>
        /// <returns></returns>
        public List<GETWEAVINGINGDATA> GetWeavingingDataList(string WEAVINGLOT)
        {
            List<GETWEAVINGINGDATA> results = CoatingDataService.Instance
                .GetWeavingingDataList(WEAVINGLOT);

            return results;
        }

        #endregion

        // ใช้สำหรับ Load ข้อมูล Finish Record
        #region Load finishing

        public List<FINISHING_SEARCHFINISHRECORD> GetFINISHING_SEARCHFINISHRECORD(string P_DATE, string P_MCNO)
        {
            List<FINISHING_SEARCHFINISHRECORD> results = FinishingDataService.Instance
                .FINISHING_SEARCHFINISHRECORD(P_DATE, P_MCNO);

            return results;
        }

        #endregion

        #region Load finishing

        public List<FINISHING_SEARCHFINISHRECORD> GetFINISHING_SEARCHFINISHRECORD(string P_DATE, string P_MCNO, string P_ITMCODE)
        {
            List<FINISHING_SEARCHFINISHRECORD> results = FinishingDataService.Instance
                .FINISHING_SEARCHFINISHRECORD(P_DATE, P_MCNO, P_ITMCODE);

            return results;
        }

        #endregion

        #region COATING

        // ใช้สำหรับ Load ข้อมูล FINISHING GET COATINGCONDITION
        #region Load finishing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mcno"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public List<FINISHING_GETCOATINGDATA> GetFINISHING_GETCOATINGCONDITIONDATA(string mcno, string flag)
        {
            List<FINISHING_GETCOATINGDATA> results = CoatingDataService.Instance
                .FINISHING_GETCOATINGCONDITIONList(mcno, flag);

            return results;
        }

        #endregion

        public List<FINISHING_COATINGDATABYLOT> GetFINISHING_COATINGDATABYLOT(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_COATINGDATABYLOT> results = CoatingDataService.Instance
                .FINISHING_COATINGDATABYLOT(P_MCNO, P_WEAVINGLOT);

            return results;
        }

        public List<FINISHING_COATINGPLCDATA> GetFINISHING_COATINGPLCDATA(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_COATINGPLCDATA> results = CoatingDataService.Instance
                .FINISHING_COATINGPLCDATA(P_MCNO, P_WEAVINGLOT);

            return results;
        }

        // ใช้สำหรับ FINISHING_INSERTCOATING
        #region FINISHING_INSERTCOATING

        public string FINISHING_INSERTCOATING()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_INSERTCOATING(_WEAVINGLOT, _itemCode, _customer, _PRODUCTTYPEID, _operator, _MCNO, _flag
            , _SATURATOR_PV, _SATURATOR_SP, _WASHING1_PV, _WASHING1_SP
            , _WASHING2_PV, _WASHING2_SP, _HOTFLUE_PV, _HOTFLUE_SP
            , _TEMP1_PV, _TEMP1_SP, _TEMP2_PV, _TEMP2_SP
            , _TEMP3_PV, _TEMP3_SP, _TEMP4_PV, _TEMP4_SP
            , _TEMP5_PV, _TEMP5_SP, _TEMP6_PV, _TEMP6_SP
            , _TEMP7_PV, _TEMP7_SP, _TEMP8_PV, _TEMP8_SP
            , _TEMP9_PV, _TEMP9_SP, _TEMP10_PV, _TEMP10_SP
            , _SPEED_PV, _SPEED_SP, _BE_COATWIDTH, _FANRPM
            , _EXFAN_FRONT_BACK, _EXFAN_MIDDLE, _ANGLEKNIFE
            , _BLADENO, _BLADEDIRECTION, _TENSIONUP
            , _TENSIONDOWN, _FORN, _TENTER, _PATHLINE
            , _FEEDIN, _OVERFEED, _WIDTHCOAT, _WIDTHCOATALL
            , _SILICONE_A, _SILICONE_B, _CWL, _CWC, _CWR
            , _HUMIDITY_AF, _HUMIDITY_BF
            , _REPROCESS, _WEAVLENGTH, _OPERATOR_GROUP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        // ใช้สำหรับ FINISHING_UPDATECOATING Processing
        #region FINISHING_UPDATECOATING Processing

        public string FINISHING_UPDATECOATINGProcessing()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_UPDATECOATINGProcessing(_FINISHLOT, _operator, _flag
            , _SATURATOR_PV, _SATURATOR_SP, _WASHING1_PV, _WASHING1_SP
            , _WASHING2_PV, _WASHING2_SP, _HOTFLUE_PV, _HOTFLUE_SP
            , _TEMP1_PV, _TEMP1_SP, _TEMP2_PV, _TEMP2_SP
            , _TEMP3_PV, _TEMP3_SP, _TEMP4_PV, _TEMP4_SP
            , _TEMP5_PV, _TEMP5_SP, _TEMP6_PV, _TEMP6_SP
            , _TEMP7_PV, _TEMP7_SP, _TEMP8_PV, _TEMP8_SP
            , _TEMP9_PV, _TEMP9_SP, _TEMP10_PV, _TEMP10_SP
            , _SPEED_PV, _SPEED_SP, _BE_COATWIDTH, _FANRPM
            , _EXFAN_FRONT_BACK, _EXFAN_MIDDLE, _ANGLEKNIFE
            , _BLADENO, _BLADEDIRECTION, _TENSIONUP
            , _TENSIONDOWN, _FORN, _TENTER, _PATHLINE
            , _FEEDIN, _OVERFEED, _WIDTHCOAT, _WIDTHCOATALL
            , _SILICONE_A, _SILICONE_B, _CWL, _CWC, _CWR
            , _itemCode, _WEAVINGLOT, _customer, _STARTDATE
            , _HUMIDITY_AF, _HUMIDITY_BF, _OPERATOR_GROUP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        // ใช้สำหรับ FINISHING_UPDATECOATING Finishing
        #region FINISHING_UPDATECOATING Finishing

        public string FINISHING_UPDATECOATINGFinishing()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_UPDATECOATINGFinishing(_FINISHLOT, _operator, _flag
            , _SATURATOR_PV, _SATURATOR_SP, _WASHING1_PV, _WASHING1_SP
            , _WASHING2_PV, _WASHING2_SP, _HOTFLUE_PV, _HOTFLUE_SP
            , _TEMP1_PV, _TEMP1_SP, _TEMP2_PV, _TEMP2_SP
            , _TEMP3_PV, _TEMP3_SP, _TEMP4_PV, _TEMP4_SP
            , _TEMP5_PV, _TEMP5_SP, _TEMP6_PV, _TEMP6_SP
            , _TEMP7_PV, _TEMP7_SP, _TEMP8_PV, _TEMP8_SP
            , _TEMP9_PV, _TEMP9_SP, _TEMP10_PV, _TEMP10_SP
            , _SPEED_PV, _SPEED_SP, _BE_COATWIDTH, _FANRPM
            , _EXFAN_FRONT_BACK, _EXFAN_MIDDLE, _ANGLEKNIFE
            , _BLADENO, _BLADEDIRECTION, _TENSIONUP
            , _TENSIONDOWN, _FORN, _TENTER, _PATHLINE
            , _FEEDIN, _OVERFEED, _WIDTHCOAT, _WIDTHCOATALL
            , _SILICONE_A, _SILICONE_B, _CWL, _CWC, _CWR
            , _itemCode, _WEAVINGLOT, _customer, _STARTDATE
            , _HUMIDITY_AF, _HUMIDITY_BF
            , _LENGTH1, _LENGTH2, _LENGTH3, _LENGTH4, _LENGTH5, _LENGTH6, _LENGTH7, _OPERATOR_GROUP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        // ใช้สำหรับ FINISHING_UPDATECOATING Finishing
        #region FINISHING_UPDATECOATINGDATA Finishing

        public string FINISHING_UPDATECOATINGDATAFinishing()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_UPDATECOATINGDATAFinishing(_FINISHLOT, _flag,
                    _SAT, _SAT_MIN, _SAT_MAX,
                    _WASHING1, _WASHING1_MIN, _WASHING1_MAX,
                    _WASHING2, _WASHING2_MIN, _WASHING2_MAX,
                    _HOTFLUE, _HOTFLUE_MIN, _HOTFLUE_MAX,
                    _TEMP1, _TEMP1_MIN, _TEMP1_MAX,
                    _TEMP2, _TEMP2_MIN, _TEMP2_MAX,
                    _TEMP3, _TEMP3_MIN, _TEMP3_MAX,
                    _TEMP4, _TEMP4_MIN, _TEMP4_MAX,
                    _TEMP5, _TEMP5_MIN, _TEMP5_MAX,
                    _TEMP6, _TEMP6_MIN, _TEMP6_MAX,
                    _TEMP7, _TEMP7_MIN, _TEMP7_MAX,
                    _TEMP8, _TEMP8_MIN, _TEMP8_MAX,
                    _TEMP9, _TEMP9_MIN, _TEMP9_MAX,
                    _TEMP10, _TEMP10_MIN, _TEMP10_MAX,
                    _SPEED, _SPEED_MIN, _SPEED_MAX,
                    _TENSIONUP, _TENUP_MIN, _TENUP_MAX,
                    _TENSIONDOWN, _TENDOWN_MIN, _TENDOWN_MAX,
                    _LENGTH1, _LENGTH2, _LENGTH3, _LENGTH4,
                    _LENGTH5, _LENGTH6, _LENGTH7,
                    _itemCode, _WEAVINGLOT, _customer,
                    _STARTDATE, _BE_COATWIDTH, _FANRPM,
                    _EXFAN_FRONT_BACK, _EXFAN_MIDDLE, _ANGLEKNIFE,
                    _BLADENO, _BLADEDIRECTION, _FORN, _TENTER, _PATHLINE,
                    _FEEDIN, _OVERFEED, _WIDTHCOAT, _WIDTHCOATALL,
                    _SILICONE_A, _SILICONE_B, _CWL, _CWC, _CWR,
                    _CONDITIONBY, _operator, _ENDDATE, _CONDITONDATE,
                    _REMARK, _HUMIDITY_BF, _HUMIDITY_AF, _OPERATOR_GROUP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        #region GetCoatingRemark
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mcno"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string GetCoatingRemark(string mcno, string flag)
        {
            string result = string.Empty;

            List<FINISHING_GETCOATINGDATA> results =
                CoatingDataService.Instance.FINISHING_GETCOATINGCONDITIONList(mcno, flag);
            if (null != results && results.Count > 0)
            {
                result = results[0].REMARK;
            }

            return result;
        }
        #endregion

        #region AddCoatingRemark
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FINISHLOT"></param>
        /// <param name="ItemCode"></param>
        /// <param name="remark"></param>
        public void AddCoatingRemark(string FINISHLOT, string ItemCode, string remark)
        {
            CoatingDataService.Instance.AddCoatingRemark(
                    FINISHLOT, ItemCode, remark);
        }
        #endregion

        #endregion

        #region SCOURING

        // ใช้สำหรับ Load ข้อมูล FINISHING GETSCOURINGDATA
        #region Load finishing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mcno"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public List<FINISHING_GETSCOURINGDATA> GetFINISHING_GETSCOURINGDATA(string mcno, string flag)
        {
            List<FINISHING_GETSCOURINGDATA> results = CoatingDataService.Instance
                .FINISHING_GETSCOURINGDATAList(mcno, flag);

            return results;
        }

        #endregion

        // ใช้สำหรับ FINISHING_INSERTSCOURING
        #region FINISHING_INSERTSCOURING

        public string FINISHING_INSERTSCOURING()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_INSERTSCOURING(_WEAVINGLOT, _itemCode, _customer, _PRODUCTTYPEID, _operator, _MCNO, _flag
            , _SATURATOR_PV, _SATURATOR_SP, _WASHING1_PV, _WASHING1_SP
            , _WASHING2_PV, _WASHING2_SP, _HOTFLUE_PV, _HOTFLUE_SP
            , _TEMP1_PV, _TEMP1_SP, _TEMP2_PV, _TEMP2_SP
            , _TEMP3_PV, _TEMP3_SP, _TEMP4_PV, _TEMP4_SP
            , _TEMP5_PV, _TEMP5_SP, _TEMP6_PV, _TEMP6_SP
            , _TEMP7_PV, _TEMP7_SP, _TEMP8_PV, _TEMP8_SP
            , _TEMP9_PV, _TEMP9_SP, _TEMP10_PV, _TEMP10_SP
            , _SPEED_PV, _SPEED_SP, _MAINFRAMEWIDTH, _WIDTH_BE
            , _WIDTH_AF, _PIN2PIN
            , _HUMIDITY_AF, _HUMIDITY_BF
            , _REPROCESS, _WEAVLENGTH, _OPERATOR_GROUP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        // ใช้สำหรับ FINISHING_UPDATESCOURING Processing
        #region FINISHING_UPDATESCOURING Processing

        public string FINISHING_UPDATESCOURINGProcessing()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_UPDATESCOURINGProcessing(_FINISHLOT, _operator, _flag
            , _SATURATOR_PV, _SATURATOR_SP, _WASHING1_PV, _WASHING1_SP
            , _WASHING2_PV, _WASHING2_SP, _HOTFLUE_PV, _HOTFLUE_SP
            , _TEMP1_PV, _TEMP1_SP, _TEMP2_PV, _TEMP2_SP
            , _TEMP3_PV, _TEMP3_SP, _TEMP4_PV, _TEMP4_SP
            , _TEMP5_PV, _TEMP5_SP, _TEMP6_PV, _TEMP6_SP
            , _TEMP7_PV, _TEMP7_SP, _TEMP8_PV, _TEMP8_SP
            , _TEMP9_PV, _TEMP9_SP, _TEMP10_PV, _TEMP10_SP
            , _SPEED_PV, _SPEED_SP, _MAINFRAMEWIDTH, _WIDTH_BE
            , _WIDTH_AF, _PIN2PIN
            , _itemCode, _WEAVINGLOT, _customer, _STARTDATE
            , _HUMIDITY_AF, _HUMIDITY_BF, _OPERATOR_GROUP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        // เก่า ใช้กับ Finish
        // ใช้สำหรับ FINISHING_UPDATESCOURING Finishing
        #region FINISHING_UPDATESCOURING Finishing

        public string FINISHING_UPDATESCOURINGFinishing()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_UPDATESCOURINGFinishing(_FINISHLOT, _operator, _flag
            , _SATURATOR_PV, _SATURATOR_SP, _WASHING1_PV, _WASHING1_SP
            , _WASHING2_PV, _WASHING2_SP, _HOTFLUE_PV, _HOTFLUE_SP
            , _TEMP1_PV, _TEMP1_SP, _TEMP2_PV, _TEMP2_SP
            , _TEMP3_PV, _TEMP3_SP, _TEMP4_PV, _TEMP4_SP
            , _TEMP5_PV, _TEMP5_SP, _TEMP6_PV, _TEMP6_SP
            , _TEMP7_PV, _TEMP7_SP, _TEMP8_PV, _TEMP8_SP
            , _TEMP9_PV, _TEMP9_SP, _TEMP10_PV, _TEMP10_SP
            , _SPEED_PV, _SPEED_SP, _MAINFRAMEWIDTH, _WIDTH_BE
            , _WIDTH_AF, _PIN2PIN
            , _itemCode, _WEAVINGLOT, _customer, _STARTDATE
            , _HUMIDITY_AF, _HUMIDITY_BF
            , _LENGTH1, _LENGTH2, _LENGTH3, _LENGTH4, _LENGTH5, _LENGTH6, _LENGTH7, _OPERATOR_GROUP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        // ใช้สำหรับ FINISHING_UPDATESCOURING Finishing
        #region FINISHING_UPDATESCOURINGDATA Finishing

        public string FINISHING_UPDATESCOURINGDATAFinishing()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_UPDATESCOURINGDATA(_FINISHLOT, _flag,
                    _SAT, _SAT_MIN, _SAT_MAX,
                    _WASHING1, _WASHING1_MIN, _WASHING1_MAX,
                    _WASHING2, _WASHING2_MIN, _WASHING2_MAX,
                    _HOTFLUE, _HOTFLUE_MIN, _HOTFLUE_MAX,
                    _TEMP1, _TEMP1_MIN, _TEMP1_MAX,
                    _TEMP2, _TEMP2_MIN, _TEMP2_MAX,
                    _TEMP3, _TEMP3_MIN, _TEMP3_MAX,
                    _TEMP4, _TEMP4_MIN, _TEMP4_MAX,
                    _TEMP5, _TEMP5_MIN, _TEMP5_MAX,
                    _TEMP6, _TEMP6_MIN, _TEMP6_MAX,
                    _TEMP7, _TEMP7_MIN, _TEMP7_MAX,
                    _TEMP8, _TEMP8_MIN, _TEMP8_MAX,
                    _SPEED, _SPEED_MIN, _SPEED_MAX,
                    _MAINFRAMEWIDTH, _WIDTH_BE, _WIDTH_AF,
                    _PIN2PIN, _operator, _ENDDATE,
                    _LENGTH1, _LENGTH2, _LENGTH3, _LENGTH4,
                    _LENGTH5, _LENGTH6, _LENGTH7,
                    _itemCode, _WEAVINGLOT, _customer, _STARTDATE,
                    _REMARK, _HUMIDITY_BF, _HUMIDITY_AF, _OPERATOR_GROUP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        #region GetScouringRemark
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mcno"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string GetScouringRemark(string mcno, string flag)
        {
            string result = string.Empty;

            List<FINISHING_GETSCOURINGDATA> results =
                CoatingDataService.Instance.FINISHING_GETSCOURINGDATAList(mcno, flag);
            if (null != results && results.Count > 0)
            {
                result = results[0].REMARK;
            }

            return result;
        }
        #endregion

        #region AddScouringRemark
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FINISHLOT"></param>
        /// <param name="ItemCode"></param>
        /// <param name="remark"></param>
        public void AddScouringRemark(string FINISHLOT, string ItemCode, string remark)
        {
            CoatingDataService.Instance.AddScouringRemark(
                    FINISHLOT, ItemCode, remark);
        }
        #endregion

        #endregion

        #region DRYER

        // ใช้สำหรับ Load ข้อมูล FINISHING GET DRYER DATA
        #region Load finishing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public List<FINISHING_GETDRYERDATA> GetFINISHING_GETDRYERDATAList(string flag, string mcNo)
        {
            List<FINISHING_GETDRYERDATA> results = CoatingDataService.Instance
                .FINISHING_GETDRYERDATAList(flag, mcNo);

            return results;
        }

        #endregion

        public List<FINISHING_DRYERDATABYLOT> GetFINISHING_DRYERDATABYLOT(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_DRYERDATABYLOT> results = CoatingDataService.Instance
                .GetFINISHING_DRYERDATABYLOT(P_MCNO, P_WEAVINGLOT);

            return results;
        }

        public List<FINISHING_SCOURINGPLCDATA> GetFINISHING_SCOURINGPLCDATA(string P_MCNO, string P_WEAVINGLOT)
        {
            List<FINISHING_SCOURINGPLCDATA> results = CoatingDataService.Instance
                .FINISHING_SCOURINGPLCDATA(P_MCNO, P_WEAVINGLOT);

            return results;
        }

        // ใช้สำหรับ FINISHING_INSERTDRYER
        #region FINISHING_INSERTDRYER

        public string FINISHING_INSERTDRYER()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_INSERTDRYER(_WEAVINGLOT, _itemCode, _customer, _PRODUCTTYPEID, _operator, _MCNO, _flag
            , _ACCARIDENSER, _ACCPRESURE, _ASSTENSION, _CHIFROT
            , _CHIREAR, _DRYCIRCUFAN, _EXHAUSTFAN
            , _HOTFLUE_PV, _HOTFLUE_SP, _SPEED_PV, _SPEED_SP
            , _STEAMPRESURE, _WIDTHAFHEAT, _WIDTHBEHEAT
            , _HUMIDITY_AF, _HUMIDITY_BF
            , _REPROCESS, _WEAVLENGTH, _OPERATOR_GROUP
            ,_SATURATOR_PV ,_SATURATOR_SP,_WASHING1_PV,_WASHING1_SP,_WASHING2_PV,_WASHING2_SP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        // ใช้สำหรับ FINISHING_UPDATEDRYERProcessing Processing
        #region FINISHING_UPDATEDRYERProcessing Processing

        public string FINISHING_UPDATEDRYERProcessing()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_UPDATEDRYERProcessing(_FINISHLOT, _operator, _flag
           , _ACCARIDENSER, _ACCPRESURE, _ASSTENSION, _CHIFROT
            , _CHIREAR, _DRYCIRCUFAN, _EXHAUSTFAN
            , _HOTFLUE_PV, _HOTFLUE_SP, _SPEED_PV, _SPEED_SP
            , _STEAMPRESURE, _WIDTHAFHEAT, _WIDTHBEHEAT
            , _itemCode, _WEAVINGLOT, _customer, _STARTDATE
            , _HUMIDITY_AF, _HUMIDITY_BF, _OPERATOR_GROUP
            , _SATURATOR_PV, _SATURATOR_SP, _WASHING1_PV, _WASHING1_SP, _WASHING2_PV, _WASHING2_SP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        // ใช้สำหรับ FINISHING_UPDATEDRYER Finishing
        #region FINISHING_UPDATEDRYER Finishing

        public string FINISHING_UPDATEDRYERFinishing()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_UPDATEDRYERFinishing(_FINISHLOT, _operator, _flag
            , _ACCARIDENSER, _ACCPRESURE, _ASSTENSION, _CHIFROT
            , _CHIREAR, _DRYCIRCUFAN, _EXHAUSTFAN
            , _HOTFLUE_PV, _HOTFLUE_SP, _SPEED_PV, _SPEED_SP
            , _STEAMPRESURE, _WIDTHAFHEAT, _WIDTHBEHEAT
            , _itemCode, _WEAVINGLOT, _customer, _STARTDATE
            , _HUMIDITY_AF, _HUMIDITY_BF
            , _LENGTH1, _LENGTH2, _LENGTH3, _LENGTH4, _LENGTH5, _LENGTH6, _LENGTH7, _OPERATOR_GROUP
            , _SATURATOR_PV, _SATURATOR_SP, _WASHING1_PV, _WASHING1_SP, _WASHING2_PV, _WASHING2_SP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        #region FINISHING_UPDATEDRYERDATA Finishing

        public string FINISHING_UPDATEDRYERDATAFinishing()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = CoatingDataService.Instance.FINISHING_UPDATEDRYERDATA(_FINISHLOT, _flag,
                    _HOTFLUE, _HOTFLUE_MIN, _HOTFLUE_MAX,
                     _SPEED, _SPEED_MIN, _SPEED_MAX,
                     _WIDTH_BE,_ACCPRESURE,_ASSTENSION,_ACCARIDENSER,
                     _CHIFROT,_CHIREAR,_STEAMPRESURE,_DRYCIRCUFAN,
                     _EXHAUSTFAN, _WIDTHAFHEAT, _CONDITIONBY, _operator,
                     _ENDDATE,_CONDITONDATE,
                     _LENGTH1, _LENGTH2, _LENGTH3, _LENGTH4,
                     _LENGTH5, _LENGTH6, _LENGTH7,
                     _itemCode, _WEAVINGLOT, _customer, _STARTDATE,
                    _REMARK, _HUMIDITY_BF, _HUMIDITY_AF, _OPERATOR_GROUP);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkSave;
        }

        #endregion

        #region GetDryerRemark
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string GetDryerRemark(string flag, string mcNo)
        {
            string result = string.Empty;

            List<FINISHING_GETDRYERDATA> results =
                CoatingDataService.Instance.FINISHING_GETDRYERDATAList(flag, mcNo);
            if (null != results && results.Count > 0)
            {
                result = results[0].REMARK;
            }

            return result;
        }
        #endregion

        #region AddDryerRemark
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FINISHLOT"></param>
        /// <param name="ItemCode"></param>
        /// <param name="remark"></param>
        public void AddDryerRemark(string FINISHLOT, string ItemCode, string remark)
        {
            CoatingDataService.Instance.AddDryerRemark(
                    FINISHLOT, ItemCode, remark);
        }
        #endregion

        #endregion

        #endregion

        #region Public Proeprties

        /// <summary>
        /// Gets or sets machine.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FinishingMCItem Machine
        {
            get { return _machine; }
            set
            {
                _machine = value;
            }
        }
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
        /// <summary>
        /// Gets or sets Item Code (or Grey Code).
        /// </summary>
        [XmlAttribute]
        public string ItemCode
        {
            get { return _itemCode; }
            set
            {
                if (_itemCode != value)
                {
                    _itemCode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets Customer.
        /// </summary>
        [XmlAttribute]
        public string Customer
        {
            get { return _customer; }
            set
            {
                if (_customer != value)
                {
                    _customer = value;
                }
            }
        }

        [XmlAttribute]
        public string MCNO
        {
            get { return _MCNO; }
            set
            {
                if (_MCNO != value)
                {
                    _MCNO = value;
                }
            }
        }
        [XmlAttribute]
        public string WEAVINGLOT
        {
            get { return _WEAVINGLOT; }
            set
            {
                if (_WEAVINGLOT != value)
                {
                    _WEAVINGLOT = value;
                }
            }
        }
        [XmlAttribute]
        public string PRODUCTTYPEID
        {
            get { return _PRODUCTTYPEID; }
            set
            {
                if (_PRODUCTTYPEID != value)
                {
                    _PRODUCTTYPEID = value;
                }
            }
        }
        [XmlAttribute]
        public string Flag
        {
            get { return _flag; }
            set
            {
                if (_flag != value)
                {
                    _flag = value;
                }
            }
        }

        [XmlAttribute]
        public string FINISHLOT
        {
            get { return _FINISHLOT; }
            set
            {
                if (_FINISHLOT != value)
                {
                    _FINISHLOT = value;
                }
            }
        }

        [XmlAttribute]
        public decimal? SATURATOR_PV
        {
            get { return _SATURATOR_PV; }
            set
            {
                if (_SATURATOR_PV != value)
                {
                    _SATURATOR_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? SATURATOR_SP
        {
            get { return _SATURATOR_SP; }
            set
            {
                if (_SATURATOR_SP != value)
                {
                    _SATURATOR_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WASHING1_PV
        {
            get { return _WASHING1_PV; }
            set
            {
                if (_WASHING1_PV != value)
                {
                    _WASHING1_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WASHING1_SP
        {
            get { return _WASHING1_SP; }
            set
            {
                if (_WASHING1_SP != value)
                {
                    _WASHING1_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WASHING2_PV
        {
            get { return _WASHING2_PV; }
            set
            {
                if (_WASHING2_PV != value)
                {
                    _WASHING2_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WASHING2_SP
        {
            get { return _WASHING2_SP; }
            set
            {
                if (_WASHING2_SP != value)
                {
                    _WASHING2_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? HOTFLUE_PV
        {
            get { return _HOTFLUE_PV; }
            set
            {
                if (_HOTFLUE_PV != value)
                {
                    _HOTFLUE_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? HOTFLUE_SP
        {
            get { return _HOTFLUE_SP; }
            set
            {
                if (_HOTFLUE_SP != value)
                {
                    _HOTFLUE_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP1_PV
        {
            get { return _TEMP1_PV; }
            set
            {
                if (_TEMP1_PV != value)
                {
                    _TEMP1_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP1_SP
        {
            get { return _TEMP1_SP; }
            set
            {
                if (_TEMP1_SP != value)
                {
                    _TEMP1_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP2_PV
        {
            get { return _TEMP2_PV; }
            set
            {
                if (_TEMP2_PV != value)
                {
                    _TEMP2_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP2_SP
        {
            get { return _TEMP2_SP; }
            set
            {
                if (_TEMP2_SP != value)
                {
                    _TEMP2_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP3_PV
        {
            get { return _TEMP3_PV; }
            set
            {
                if (_TEMP3_PV != value)
                {
                    _TEMP3_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP3_SP
        {
            get { return _TEMP3_SP; }
            set
            {
                if (_TEMP3_SP != value)
                {
                    _TEMP3_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP4_PV
        {
            get { return _TEMP4_PV; }
            set
            {
                if (_TEMP4_PV != value)
                {
                    _TEMP4_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP4_SP
        {
            get { return _TEMP4_SP; }
            set
            {
                if (_TEMP4_SP != value)
                {
                    _TEMP4_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP5_PV
        {
            get { return _TEMP5_PV; }
            set
            {
                if (_TEMP5_PV != value)
                {
                    _TEMP5_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP5_SP
        {
            get { return _TEMP5_SP; }
            set
            {
                if (_TEMP5_SP != value)
                {
                    _TEMP5_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP6_PV
        {
            get { return _TEMP6_PV; }
            set
            {
                if (_TEMP6_PV != value)
                {
                    _TEMP6_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP6_SP
        {
            get { return _TEMP6_SP; }
            set
            {
                if (_TEMP6_SP != value)
                {
                    _TEMP6_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP7_PV
        {
            get { return _TEMP7_PV; }
            set
            {
                if (_TEMP7_PV != value)
                {
                    _TEMP7_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP7_SP
        {
            get { return _TEMP7_SP; }
            set
            {
                if (_TEMP7_SP != value)
                {
                    _TEMP7_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP8_PV
        {
            get { return _TEMP8_PV; }
            set
            {
                if (_TEMP8_PV != value)
                {
                    _TEMP8_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP8_SP
        {
            get { return _TEMP8_SP; }
            set
            {
                if (_TEMP8_SP != value)
                {
                    _TEMP8_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP9_PV
        {
            get { return _TEMP9_PV; }
            set
            {
                if (_TEMP9_PV != value)
                {
                    _TEMP9_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP9_SP
        {
            get { return _TEMP9_SP; }
            set
            {
                if (_TEMP9_SP != value)
                {
                    _TEMP9_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP10_PV
        {
            get { return _TEMP10_PV; }
            set
            {
                if (_TEMP10_PV != value)
                {
                    _TEMP10_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP10_SP
        {
            get { return _TEMP10_SP; }
            set
            {
                if (_TEMP10_SP != value)
                {
                    _TEMP10_SP = value;
                }
            }
        }

        [XmlAttribute]
        public decimal? SPEED_PV
        {
            get { return _SPEED_PV; }
            set
            {
                if (_SPEED_PV != value)
                {
                    _SPEED_PV = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? SPEED_SP
        {
            get { return _SPEED_SP; }
            set
            {
                if (_SPEED_SP != value)
                {
                    _SPEED_SP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? MAINFRAMEWIDTH
        {
            get { return _MAINFRAMEWIDTH; }
            set
            {
                if (_MAINFRAMEWIDTH != value)
                {
                    _MAINFRAMEWIDTH = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTH_BE
        {
            get { return _WIDTH_BE; }
            set
            {
                if (_WIDTH_BE != value)
                {
                    _WIDTH_BE = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTH_AF
        {
            get { return _WIDTH_AF; }
            set
            {
                if (_WIDTH_AF != value)
                {
                    _WIDTH_AF = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? PIN2PIN
        {
            get { return _PIN2PIN; }
            set
            {
                if (_PIN2PIN != value)
                {
                    _PIN2PIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? LENGTH1
        {
            get { return _LENGTH1; }
            set
            {
                if (_LENGTH1 != value)
                {
                    _LENGTH1 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? LENGTH2
        {
            get { return _LENGTH2; }
            set
            {
                if (_LENGTH2 != value)
                {
                    _LENGTH2 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? LENGTH3
        {
            get { return _LENGTH3; }
            set
            {
                if (_LENGTH3 != value)
                {
                    _LENGTH3 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? LENGTH4
        {
            get { return _LENGTH4; }
            set
            {
                if (_LENGTH4 != value)
                {
                    _LENGTH4 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? LENGTH5
        {
            get { return _LENGTH5; }
            set
            {
                if (_LENGTH5 != value)
                {
                    _LENGTH5 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? LENGTH6
        {
            get { return _LENGTH6; }
            set
            {
                if (_LENGTH6 != value)
                {
                    _LENGTH6 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? LENGTH7
        {
            get { return _LENGTH7; }
            set
            {
                if (_LENGTH7 != value)
                {
                    _LENGTH7 = value;
                }
            }
        }

        [XmlAttribute]
        public decimal? ACCARIDENSER
        {
            get { return _ACCARIDENSER; }
            set
            {
                if (_ACCARIDENSER != value)
                {
                    _ACCARIDENSER = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? ACCPRESURE
        {
            get { return _ACCPRESURE; }
            set
            {
                if (_ACCPRESURE != value)
                {
                    _ACCPRESURE = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? ASSTENSION
        {
            get { return _ASSTENSION; }
            set
            {
                if (_ASSTENSION != value)
                {
                    _ASSTENSION = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? CHIFROT
        {
            get { return _CHIFROT; }
            set
            {
                if (_CHIFROT != value)
                {
                    _CHIFROT = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? CHIREAR
        {
            get { return _CHIREAR; }
            set
            {
                if (_CHIREAR != value)
                {
                    _CHIREAR = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? DRYCIRCUFAN
        {
            get { return _DRYCIRCUFAN; }
            set
            {
                if (_DRYCIRCUFAN != value)
                {
                    _DRYCIRCUFAN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? EXHAUSTFAN
        {
            get { return _EXHAUSTFAN; }
            set
            {
                if (_EXHAUSTFAN != value)
                {
                    _EXHAUSTFAN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? STEAMPRESURE
        {
            get { return _STEAMPRESURE; }
            set
            {
                if (_STEAMPRESURE != value)
                {
                    _STEAMPRESURE = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTHAFHEAT
        {
            get { return _WIDTHAFHEAT; }
            set
            {
                if (_WIDTHAFHEAT != value)
                {
                    _WIDTHAFHEAT = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTHBEHEAT
        {
            get { return _WIDTHBEHEAT; }
            set
            {
                if (_WIDTHBEHEAT != value)
                {
                    _WIDTHBEHEAT = value;
                }
            }
        }
        [XmlAttribute]
        public DateTime? STARTDATE
        {
            get { return _STARTDATE; }
            set
            {
                if (_STARTDATE != value)
                {
                    _STARTDATE = value;
                }
            }
        }

        [XmlAttribute]
        public decimal? BE_COATWIDTH
        {
            get { return _BE_COATWIDTH; }
            set
            {
                if (_BE_COATWIDTH != value)
                {
                    _BE_COATWIDTH = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? FANRPM
        {
            get { return _FANRPM; }
            set
            {
                if (_FANRPM != value)
                {
                    _FANRPM = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? EXFAN_FRONT_BACK
        {
            get { return _EXFAN_FRONT_BACK; }
            set
            {
                if (_EXFAN_FRONT_BACK != value)
                {
                    _EXFAN_FRONT_BACK = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? EXFAN_MIDDLE
        {
            get { return _EXFAN_MIDDLE; }
            set
            {
                if (_EXFAN_MIDDLE != value)
                {
                    _EXFAN_MIDDLE = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? ANGLEKNIFE
        {
            get { return _ANGLEKNIFE; }
            set
            {
                if (_ANGLEKNIFE != value)
                {
                    _ANGLEKNIFE = value;
                }
            }
        }
        [XmlAttribute]
        public string BLADENO
        {
            get { return _BLADENO; }
            set
            {
                if (_BLADENO != value)
                {
                    _BLADENO = value;
                }
            }
        }
        [XmlAttribute]
        public string BLADEDIRECTION
        {
            get { return _BLADEDIRECTION; }
            set
            {
                if (_BLADEDIRECTION != value)
                {
                    _BLADEDIRECTION = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? CYLINDER_TENSIONUP
        {
            get { return _CYLINDER_TENSIONUP; }
            set
            {
                if (_CYLINDER_TENSIONUP != value)
                {
                    _CYLINDER_TENSIONUP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? OPOLE_TENSIONDOWN
        {
            get { return _OPOLE_TENSIONDOWN; }
            set
            {
                if (_OPOLE_TENSIONDOWN != value)
                {
                    _OPOLE_TENSIONDOWN = value;
                }
            }
        }
        //[XmlAttribute]
        //public decimal? FRAMEWIDTH_FORN
        //{
        //    get { return _FRAMEWIDTH_FORN; }
        //    set
        //    {
        //        if (_FRAMEWIDTH_FORN != value)
        //        {
        //            _FRAMEWIDTH_FORN = value;
        //        }
        //    }
        //}
        //[XmlAttribute]
        //public decimal? FRAMEWIDTH_TENTER
        //{
        //    get { return _FRAMEWIDTH_TENTER; }
        //    set
        //    {
        //        if (_FRAMEWIDTH_TENTER != value)
        //        {
        //            _FRAMEWIDTH_TENTER = value;
        //        }
        //    }
        //}
        [XmlAttribute]
        public decimal? PATHLINE
        {
            get { return _PATHLINE; }
            set
            {
                if (_PATHLINE != value)
                {
                    _PATHLINE = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? FEEDIN
        {
            get { return _FEEDIN; }
            set
            {
                if (_FEEDIN != value)
                {
                    _FEEDIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? OVERFEED
        {
            get { return _OVERFEED; }
            set
            {
                if (_OVERFEED != value)
                {
                    _OVERFEED = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTHCOAT
        {
            get { return _WIDTHCOAT; }
            set
            {
                if (_WIDTHCOAT != value)
                {
                    _WIDTHCOAT = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTHCOATALL
        {
            get { return _WIDTHCOATALL; }
            set
            {
                if (_WIDTHCOATALL != value)
                {
                    _WIDTHCOATALL = value;
                }
            }
        }
        [XmlAttribute]
        public string SILICONE_A
        {
            get { return _SILICONE_A; }
            set
            {
                if (_SILICONE_A != value)
                {
                    _SILICONE_A = value;
                }
            }
        }
        [XmlAttribute]
        public string SILICONE_B
        {
            get { return _SILICONE_B; }
            set
            {
                if (_SILICONE_B != value)
                {
                    _SILICONE_B = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? COATINGWEIGTH_L
        {
            get { return _COATINGWEIGTH_L; }
            set
            {
                if (_COATINGWEIGTH_L != value)
                {
                    _COATINGWEIGTH_L = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? COATINGWEIGTH_C
        {
            get { return _COATINGWEIGTH_C; }
            set
            {
                if (_COATINGWEIGTH_C != value)
                {
                    _COATINGWEIGTH_C = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? COATINGWEIGTH_R
        {
            get { return _COATINGWEIGTH_R; }
            set
            {
                if (_COATINGWEIGTH_R != value)
                {
                    _COATINGWEIGTH_R = value;
                }
            }
        }

        [XmlAttribute]
        public decimal? TENSIONUP
        {
            get { return _TENSIONUP; }
            set
            {
                if (_TENSIONUP != value)
                {
                    _TENSIONUP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TENSIONDOWN
        {
            get { return _TENSIONDOWN; }
            set
            {
                if (_TENSIONDOWN != value)
                {
                    _TENSIONDOWN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? FORN
        {
            get { return _FORN; }
            set
            {
                if (_FORN != value)
                {
                    _FORN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TENTER
        {
            get { return _TENTER; }
            set
            {
                if (_TENTER != value)
                {
                    _TENTER = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? CWL
        {
            get { return _CWL; }
            set
            {
                if (_CWL != value)
                {
                    _CWL = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? CWC
        {
            get { return _CWC; }
            set
            {
                if (_CWC != value)
                {
                    _CWC = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? CWR
        {
            get { return _CWR; }
            set
            {
                if (_CWR != value)
                {
                    _CWR = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? HUMIDITY_AF
        {
            get { return _HUMIDITY_AF; }
            set
            {
                if (_HUMIDITY_AF != value)
                {
                    _HUMIDITY_AF = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? HUMIDITY_BF
        {
            get { return _HUMIDITY_BF; }
            set
            {
                if (_HUMIDITY_BF != value)
                {
                    _HUMIDITY_BF = value;
                }
            }
        }

        [XmlAttribute]
        public string REPROCESS
        {
            get { return _REPROCESS; }
            set
            {
                if (_REPROCESS != value)
                {
                    _REPROCESS = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WEAVLENGTH
        {
            get { return _WEAVLENGTH; }
            set
            {
                if (_WEAVLENGTH != value)
                {
                    _WEAVLENGTH = value;
                }
            }
        }

        [XmlAttribute]
        public string OPERATOR_GROUP
        {
            get { return _OPERATOR_GROUP; }
            set
            {
                if (_OPERATOR_GROUP != value)
                {
                    _OPERATOR_GROUP = value;
                }
            }
        }

        [XmlAttribute]
        public decimal? SAT
        {
            get { return _SAT; }
            set
            {
                if (_SAT != value)
                {
                    _SAT = value;
                }
            }
        }

        [XmlAttribute]
        public decimal? SAT_MIN
        {
            get { return _SAT_MIN; }
            set
            {
                if (_SAT_MIN != value)
                {
                    _SAT_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? SAT_MAX
        {
            get { return _SAT_MAX; }
            set
            {
                if (_SAT_MAX != value)
                {
                    _SAT_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WASHING1
        {
            get { return _WASHING1; }
            set
            {
                if (_WASHING1 != value)
                {
                    _WASHING1 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WASHING1_MIN
        {
            get { return _WASHING1_MIN; }
            set
            {
                if (_WASHING1_MIN != value)
                {
                    _WASHING1_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WASHING1_MAX
        {
            get { return _WASHING1_MAX; }
            set
            {
                if (_WASHING1_MAX != value)
                {
                    _WASHING1_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WASHING2
        {
            get { return _WASHING2; }
            set
            {
                if (_WASHING2 != value)
                {
                    _WASHING2 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WASHING2_MIN
        {
            get { return _WASHING2_MIN; }
            set
            {
                if (_WASHING2_MIN != value)
                {
                    _WASHING2_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WASHING2_MAX
        {
            get { return _WASHING2_MAX; }
            set
            {
                if (_WASHING2_MAX != value)
                {
                    _WASHING2_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? HOTFLUE
        {
            get { return _HOTFLUE; }
            set
            {
                if (_HOTFLUE != value)
                {
                    _HOTFLUE = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? HOTFLUE_MIN
        {
            get { return _HOTFLUE_MIN; }
            set
            {
                if (_HOTFLUE_MIN != value)
                {
                    _HOTFLUE_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? HOTFLUE_MAX
        {
            get { return _HOTFLUE_MAX; }
            set
            {
                if (_HOTFLUE_MAX != value)
                {
                    _HOTFLUE_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP1
        {
            get { return _TEMP1; }
            set
            {
                if (_TEMP1 != value)
                {
                    _TEMP1 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP1_MIN
        {
            get { return _TEMP1_MIN; }
            set
            {
                if (_TEMP1_MIN != value)
                {
                    _TEMP1_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP1_MAX
        {
            get { return _TEMP1_MAX; }
            set
            {
                if (_TEMP1_MAX != value)
                {
                    _TEMP1_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP2
        {
            get { return _TEMP2; }
            set
            {
                if (_TEMP2 != value)
                {
                    _TEMP2 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP2_MIN
        {
            get { return _TEMP2_MIN; }
            set
            {
                if (_TEMP2_MIN != value)
                {
                    _TEMP2_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP2_MAX
        {
            get { return _TEMP2_MAX; }
            set
            {
                if (_TEMP2_MAX != value)
                {
                    _TEMP2_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP3
        {
            get { return _TEMP3; }
            set
            {
                if (_TEMP3 != value)
                {
                    _TEMP3 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP3_MIN
        {
            get { return _TEMP3_MIN; }
            set
            {
                if (_TEMP3_MIN != value)
                {
                    _TEMP3_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP3_MAX
        {
            get { return _TEMP3_MAX; }
            set
            {
                if (_TEMP3_MAX != value)
                {
                    _TEMP3_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP4
        {
            get { return _TEMP4; }
            set
            {
                if (_TEMP4 != value)
                {
                    _TEMP4 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP4_MIN
        {
            get { return _TEMP4_MIN; }
            set
            {
                if (_TEMP4_MIN != value)
                {
                    _TEMP4_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP4_MAX
        {
            get { return _TEMP4_MAX; }
            set
            {
                if (_TEMP4_MAX != value)
                {
                    _TEMP4_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP5
        {
            get { return _TEMP5; }
            set
            {
                if (_TEMP5 != value)
                {
                    _TEMP5 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP5_MIN
        {
            get { return _TEMP5_MIN; }
            set
            {
                if (_TEMP5_MIN != value)
                {
                    _TEMP5_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP5_MAX
        {
            get { return _TEMP5_MAX; }
            set
            {
                if (_TEMP5_MAX != value)
                {
                    _TEMP5_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP6
        {
            get { return _TEMP6; }
            set
            {
                if (_TEMP6 != value)
                {
                    _TEMP6 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP6_MIN
        {
            get { return _TEMP6_MIN; }
            set
            {
                if (_TEMP6_MIN != value)
                {
                    _TEMP6_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP6_MAX
        {
            get { return _TEMP6_MAX; }
            set
            {
                if (_TEMP6_MAX != value)
                {
                    _TEMP6_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP7
        {
            get { return _TEMP7; }
            set
            {
                if (_TEMP7 != value)
                {
                    _TEMP7 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP7_MIN
        {
            get { return _TEMP7_MIN; }
            set
            {
                if (_TEMP7_MIN != value)
                {
                    _TEMP7_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP7_MAX
        {
            get { return _TEMP7_MAX; }
            set
            {
                if (_TEMP7_MAX != value)
                {
                    _TEMP7_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP8
        {
            get { return _TEMP8; }
            set
            {
                if (_TEMP8 != value)
                {
                    _TEMP8 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP8_MIN
        {
            get { return _TEMP8_MIN; }
            set
            {
                if (_TEMP8_MIN != value)
                {
                    _TEMP8_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP8_MAX
        {
            get { return _TEMP8_MAX; }
            set
            {
                if (_TEMP8_MAX != value)
                {
                    _TEMP8_MAX = value;
                }
            }
        }


        [XmlAttribute]
        public decimal? TEMP9
        {
            get { return _TEMP9; }
            set
            {
                if (_TEMP9 != value)
                {
                    _TEMP9 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP9_MIN
        {
            get { return _TEMP9_MIN; }
            set
            {
                if (_TEMP9_MIN != value)
                {
                    _TEMP9_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP9_MAX
        {
            get { return _TEMP9_MAX; }
            set
            {
                if (_TEMP9_MAX != value)
                {
                    _TEMP9_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP10
        {
            get { return _TEMP10; }
            set
            {
                if (_TEMP10 != value)
                {
                    _TEMP10 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP10_MIN
        {
            get { return _TEMP10_MIN; }
            set
            {
                if (_TEMP10_MIN != value)
                {
                    _TEMP10_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TEMP10_MAX
        {
            get { return _TEMP10_MAX; }
            set
            {
                if (_TEMP10_MAX != value)
                {
                    _TEMP10_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? SPEED
        {
            get { return _SPEED; }
            set
            {
                if (_SPEED != value)
                {
                    _SPEED = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? SPEED_MIN
        {
            get { return _SPEED_MIN; }
            set
            {
                if (_SPEED_MIN != value)
                {
                    _SPEED_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? SPEED_MAX
        {
            get { return _SPEED_MAX; }
            set
            {
                if (_SPEED_MAX != value)
                {
                    _SPEED_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public DateTime? ENDDATE
        {
            get { return _ENDDATE; }
            set
            {
                if (_ENDDATE != value)
                {
                    _ENDDATE = value;
                }
            }
        }
        [XmlAttribute]
        public string REMARK
        {
            get { return _REMARK; }
            set
            {
                if (_REMARK != value)
                {
                    _REMARK = value;
                }
            }
        }

        [XmlAttribute]
        public decimal? TENUP_MIN
        {
            get { return _TENUP_MIN; }
            set
            {
                if (_TENUP_MIN != value)
                {
                    _TENUP_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TENUP_MAX
        {
            get { return _TENUP_MAX; }
            set
            {
                if (_TENUP_MAX != value)
                {
                    _TENUP_MAX = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TENDOWN_MIN
        {
            get { return _TENDOWN_MIN; }
            set
            {
                if (_TENDOWN_MIN != value)
                {
                    _TENDOWN_MIN = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? TENDOWN_MAX
        {
            get { return _TENDOWN_MAX; }
            set
            {
                if (_TENDOWN_MAX != value)
                {
                    _TENDOWN_MAX = value;
                }
            }
        }

        [XmlAttribute]
        public string CONDITIONBY
        {
            get { return _CONDITIONBY; }
            set
            {
                if (_CONDITIONBY != value)
                {
                    _CONDITIONBY = value;
                }
            }
        }
        [XmlAttribute]
        public DateTime? CONDITONDATE
        {
            get { return _CONDITONDATE; }
            set
            {
                if (_CONDITONDATE != value)
                {
                    _CONDITONDATE = value;
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
}
