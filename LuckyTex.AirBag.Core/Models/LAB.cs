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
    #region ITM_GETITEMCODELIST 

    public class ITM_GETITEMCODELIST
    {
        public System.String ITM_CODE { get; set; }
    }

    #endregion

    #region LAB_GETINSPECTIONLIST 

    public class LAB_GETINSPECTIONLIST
    {
        public System.String PALLETNO { get; set; }
        public System.String INSPECTIONLOT { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.Decimal? NETLENGTH { get; set; }
        public System.Decimal? GROSSWEIGHT { get; set; }
        public System.Decimal? NETWEIGHT { get; set; }
        public System.String GRADE { get; set; }
        public System.String CUSTOMERTYPE { get; set; }
        public System.String ISLAB { get; set; }
        public System.DateTime? INSPECTIONDATE { get; set; }
        public System.String LABResult { get; set; }
    }

    #endregion

    #region LAB_GETFINISHINGSAMPLING

    public class LAB_GETFINISHINGSAMPLING
    {
        public System.Int32 RowNo { get; set; }

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

        public System.DateTime? ReceiveDate { get; set; }
    }

    #endregion

    #region LAB_GETWEAVINGSAMPLING

    public class LAB_GETWEAVINGSAMPLING
    {
        public System.Int32 RowNo { get; set; }

        public System.String BEAMERROLL { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.DateTime? SETTINGDATE { get; set; }
        public System.String BARNO { get; set; }
        public System.Decimal? SPIRAL_L { get; set; }
        public System.Decimal? SPIRAL_R { get; set; }
        public System.Decimal? STSAMPLING { get; set; }
        public System.Decimal? RECUTSAMPLING { get; set; }
        public System.String STSAMPLINGBY { get; set; }
        public System.String RECUTBY { get; set; }
        public System.DateTime? STDATE { get; set; }
        public System.DateTime? RECUTDATE { get; set; }
        public System.String REMARK { get; set; }

        public System.DateTime? ReceiveDate { get; set; }
    }

    #endregion

    #region LAB_SEARCHLABGREIGE

    public class LAB_SEARCHLABGREIGE
    {
        public System.String BEAMERROLL { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? TESTNO { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String RECEIVEBY { get; set; }
        public System.String STATUS { get; set; }
        public System.DateTime? CONDITIONINGTIME { get; set; }
        public System.DateTime? TESTDATE { get; set; }
        public System.String TESTRESULT { get; set; }
        public System.String REMARK { get; set; }
        public System.String TESTBY { get; set; }
        public System.String APPROVESTATUS { get; set; }
        public System.String APPROVEBY { get; set; }
        public System.DateTime? SENDDATE { get; set; }
        public System.DateTime? APPROVEDATE { get; set; }
        public System.DateTime? SETTINGDATE { get; set; }

        public System.String Judgment { get; set; }
        public System.String Recut { get; set; }
    }

    #endregion

    #region LAB_SEARCHLABMASSPRO

    public class LAB_SEARCHLABMASSPRO
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String RECEIVEBY { get; set; }
        public System.String STATUS { get; set; }
        public System.DateTime? CONDITIONINGTIME { get; set; }
        public System.DateTime? TESTDATE { get; set; }
        public System.String TESTRESULT { get; set; }
        public System.String REMARK { get; set; }
        public System.String TESTBY { get; set; }
        public System.String APPROVESTATUS { get; set; }
        public System.String APPROVEBY { get; set; }
        public System.DateTime? SENDDATE { get; set; }
        public System.DateTime? APPROVEDATE { get; set; }
        public System.String FABRICTYPE { get; set; }

        public System.String Judgment { get; set; }
    }

    #endregion

    #region LAB_MASSPROSTOCKSTATUS 

    public class LAB_MASSPROSTOCKSTATUS
    {
        public System.String ITM_CODE { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String RECEIVEBY { get; set; }
        public System.String STATUS { get; set; }
        public System.String CONDITIONINGTIME { get; set; }
        public System.DateTime? TESTDATE { get; set; }
        public System.String TESTRESULT { get; set; }
        public System.String REMARK { get; set; }
        public System.String TESTBY { get; set; }
        public System.String APPROVESTATUS { get; set; }
        public System.String APPROVEBY { get; set; }
        public System.DateTime? SENDDATE { get; set; }
        public System.DateTime? APPROVEDATE { get; set; }
        public System.String LABFORM { get; set; }

        public System.String Judgment { get; set; }
    }

    #endregion

    #region LAB_GREIGESTOCKSTATUS

    public class LAB_GREIGESTOCKSTATUS
    {
        public System.String BEAMERROLL { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String RECEIVEBY { get; set; }
        public System.String STATUS { get; set; }
        public System.Decimal? CONDITIONINGTIME { get; set; }
        public System.DateTime? TESTDATE { get; set; }
        public System.String TESTRESULT { get; set; }
        public System.String REMARK { get; set; }
        public System.String TESTBY { get; set; }
        public System.String APPROVESTATUS { get; set; }
        public System.String APPROVEBY { get; set; }
        public System.DateTime? SENDDATE { get; set; }
        public System.DateTime? APPROVEDATE { get; set; }
        public System.Decimal? TESTNO { get; set; }

        public System.String Judgment { get; set; }
    }

    #endregion

    #region LAB_WEAVINGHISTORY

    public class LAB_WEAVINGHISTORY
    {
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
    }

    #endregion

    #region LAB Session

    /// <summary>
    /// Finishing Session.
    /// </summary>
    [Serializable]
    public class LABSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LABSession()
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

        #region Weav_getAllItemWeaving

        public List<WEAV_GETALLITEMWEAVING> Weav_getAllItemWeaving()
        {
            List<WEAV_GETALLITEMWEAVING> results = WeavingDataService.Instance
                    .WEAV_GETALLITEMWEAVING();

            return results;
        }

        #endregion

        #region Weav_getAllItemWeaving

        public List<LAB_WEAVINGHISTORY> LAB_WEAVINGHISTORY(string P_WEAVINGLOT)
        {
            List<LAB_WEAVINGHISTORY> results = LABDataService.Instance
                    .LAB_WEAVINGHISTORY(P_WEAVINGLOT);

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

    // New 31/08/20
    #region LAB_SEARCHLABENTRYPRODUCTION_Rep

    public class LAB_SEARCHLABENTRYPRODUCTION_Rep
    {
        #region LAB_SEARCHLABENTRYPRODUCTION

        public System.String ITM_CODE { get; set; }
        public System.String ITM_CODE_B { get; set; }
        public System.String ITM_CODE_H { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String CUSTOMERID { get; set; }
        public System.String PARTNO { get; set; }
        public System.String FINISHINGPROCESS { get; set; }
        public System.String ITEMLOT { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String FINISHINGMC { get; set; }
        public System.String BATCHNO { get; set; }

        public System.Decimal? NUMTHREADS_W_AVG { get; set; }
        public System.String NUMTHREADS_W_JUD { get; set; }

        public System.Decimal? NUMTHREADS_F_AVG { get; set; }
        public System.String NUMTHREADS_F_JUD { get; set; }

        public System.Decimal? USABLE_WIDTH_AVG { get; set; }
        public System.String USABLE_WIDTH_JUD { get; set; }

        public System.Decimal? WIDTH_SILICONE_AVG { get; set; }
        public System.String WIDTH_SILICONE_JUD { get; set; }

        public System.Decimal? TOTALWEIGHT_AVG { get; set; }
        public System.String TOTALWEIGHT_JUD { get; set; }

        public System.Decimal? UNCOATEDWEIGHT_AVG { get; set; }
        public System.String UNCOATEDWEIGHT_JUD { get; set; }

        public System.Decimal? COATINGWEIGHT_AVG { get; set; }
        public System.String COATINGWEIGHT_JUD { get; set; }

        public System.Decimal? MAXFORCE_W_AVG { get; set; }
        public System.String MAXFORCE_W_JUD { get; set; }

        public System.Decimal? MAXFORCE_F_AVG { get; set; }
        public System.String MAXFORCE_F_JUD { get; set; }

        public System.Decimal? ELONGATIONFORCE_W_AVG { get; set; }
        public System.String ELONGATIONFORCE_W_JUD { get; set; }

        public System.Decimal? ELONGATIONFORCE_F_AVG { get; set; }
        public System.String ELONGATIONFORCE_F_JUD { get; set; }

        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.String FLAMMABILITY_W_JUD { get; set; }
        public System.Decimal? FLAMMABILITY_F { get; set; }
        public System.String FLAMMABILITY_F_JUD { get; set; }

        public System.Decimal? EDGECOMB_W_AVG { get; set; }
        public System.String EDGECOMB_W_JUD { get; set; }

        public System.Decimal? EDGECOMB_F_AVG { get; set; }
        public System.String EDGECOMB_F_JUD { get; set; }

        public System.Decimal? STIFFNESS_W_AVG { get; set; }
        public System.String STIFFNESS_W_JUD { get; set; }

        public System.Decimal? STIFFNESS_F_AVG { get; set; }
        public System.String STIFFNESS_F_JUD { get; set; }

        public System.Decimal? TEAR_W_AVG { get; set; }
        public System.String TEAR_W_JUD { get; set; }

        public System.Decimal? TEAR_F_AVG { get; set; }
        public System.String TEAR_F_JUD { get; set; }


        public System.Decimal? STATIC_AIR_AVG { get; set; }
        public System.String STATIC_AIR_JUD { get; set; }

        public System.Decimal? FLEXABRASION_W_AVG { get; set; }
        public System.String FLEXABRASION_W_JUD { get; set; }
        
        public System.Decimal? FLEXABRASION_F_AVG { get; set; }
        public System.String FLEXABRASION_F_JUD { get; set; }

        public System.Decimal? DIMENSCHANGE_W_AVG { get; set; }
        public System.String DIMENSCHANGE_W_JUD { get; set; }

        public System.Decimal? DIMENSCHANGE_F_AVG { get; set; }
        public System.String DIMENSCHANGE_F_JUD { get; set; }

        public System.String JUDGEMENT { get; set; }

        /// <summary>
        /// Update 26/10/20
        /// </summary>
        public System.String FILENAME { get; set; }
        public System.DateTime? UPLOADDATE { get; set; }
        public System.String UPLOADBY { get; set; }

        #endregion

        #region LAB_GETITEMTESTSPECIFICATION

        //public System.Decimal? NUMTHREADS_W { get; set; }
        //public System.Decimal? NUMTHREADS_W_TOR { get; set; }
        public System.Decimal? NUMTHREADS_W_Min { get; set; }
        public System.Decimal? NUMTHREADS_W_Max { get; set; }

        //public System.Decimal? NUMTHREADS_F { get; set; }
        //public System.Decimal? NUMTHREADS_F_TOR { get; set; }
        public System.Decimal? NUMTHREADS_F_Min { get; set; }
        public System.Decimal? NUMTHREADS_F_Max { get; set; }

        //public System.Decimal? USABLE_WIDTH_Cal { get; set; }
        //public System.String USABLE_WIDTH_TOR { get; set; }
        public System.Decimal? USABLE_WIDTH_Min { get; set; }

        public System.Decimal? WIDTH_SILICONE_Min { get; set; }

        //public System.Decimal? TOTALWEIGHT { get; set; }
        //public System.Decimal? TOTALWEIGHT_TOR { get; set; }
        public System.Decimal? TOTALWEIGHT_Min { get; set; }
        public System.Decimal? TOTALWEIGHT_Max { get; set; }

        //public System.Decimal? UNCOATEDWEIGHT { get; set; }
        //public System.Decimal? UNCOATEDWEIGHT_TOR { get; set; }
        public System.Decimal? UNCOATEDWEIGHT_Min { get; set; }
        public System.Decimal? UNCOATEDWEIGHT_Max { get; set; }

        //public System.Decimal? COATINGWEIGHT { get; set; }
        //public System.Decimal? COATINGWEIGHT_TOR { get; set; }
        public System.Decimal? COATINGWEIGHT_Min { get; set; }
        public System.Decimal? COATINGWEIGHT_Max { get; set; }

        //public System.Decimal? MAXFORCE_W { get; set; }
        //public System.String MAXFORCE_W_TOR { get; set; }
        public System.Decimal? MAXFORCE_W_Min { get; set; }

        //public System.Decimal? MAXFORCE_F { get; set; }
        //public System.String MAXFORCE_F_TOR { get; set; }
        public System.Decimal? MAXFORCE_F_Min { get; set; }

        //public System.Decimal? ELONGATIONFORCE_W { get; set; }
        public System.String ELONGATIONFORCE_W_TOR { get; set; }
        public System.Decimal? ELONGATIONFORCE_W_Min { get; set; }
        public System.Decimal? ELONGATIONFORCE_W_Max { get; set; }

        //public System.Decimal? ELONGATIONFORCE_F { get; set; }
        public System.String ELONGATIONFORCE_F_TOR { get; set; }
        public System.Decimal? ELONGATIONFORCE_F_Min { get; set; }
        public System.Decimal? ELONGATIONFORCE_F_Max { get; set; }

        //public System.Decimal? FLAMMABILITY_W_Cal { get; set; }
        //public System.String FLAMMABILITY_W_TOR { get; set; }
        public System.Decimal? FLAMMABILITY_W_Max { get; set; }

        //public System.Decimal? FLAMMABILITY_F_Cal { get; set; }
        //public System.String FLAMMABILITY_F_TOR { get; set; }
        public System.Decimal? FLAMMABILITY_F_Max { get; set; }

        //public System.Decimal? EDGECOMB_W { get; set; }
        //public System.String EDGECOMB_W_TOR { get; set; }
        public System.Decimal? EDGECOMB_W_Min { get; set; }

        //public System.Decimal? EDGECOMB_F { get; set; }
        //public System.String EDGECOMB_F_TOR { get; set; }
        public System.Decimal? EDGECOMB_F_Min { get; set; }

        //public System.Decimal? STIFFNESS_W { get; set; }
        //public System.String STIFFNESS_W_TOR { get; set; }
        public System.Decimal? STIFFNESS_W_Max { get; set; }

        //public System.Decimal? STIFFNESS_F { get; set; }
        //public System.String STIFFNESS_F_TOR { get; set; }
        public System.Decimal? STIFFNESS_F_Max { get; set; }

        //public System.Decimal? TEAR_W { get; set; }
        //public System.String TEAR_W_TOR { get; set; }
        public System.Decimal? TEAR_W_Min { get; set; }

        //public System.Decimal? TEAR_F { get; set; }
        //public System.String TEAR_F_TOR { get; set; }
        public System.Decimal? TEAR_F_Min { get; set; }

        //public System.Decimal? STATIC_AIR_Cal { get; set; }
        //public System.String STATIC_AIR_TOR { get; set; }
        public System.Decimal? STATIC_AIR_Max { get; set; }

        //public System.Decimal? FLEXABRASION_W { get; set; }
        //public System.String FLEXABRASION_W_TOR { get; set; }
        public System.Decimal? FLEXABRASION_W_Min { get; set; }

        //public System.Decimal? FLEXABRASION_F { get; set; }
        //public System.String FLEXABRASION_F_TOR { get; set; }
        public System.Decimal? FLEXABRASION_F_Min { get; set; }

        //public System.Decimal? DIMENSCHANGE_W { get; set; }
        public System.String DIMENSCHANGE_W_TOR { get; set; }
        public System.Decimal? DIMENSCHANGE_W_Min { get; set; }
        public System.Decimal? DIMENSCHANGE_W_Max { get; set; }

        //public System.Decimal? DIMENSCHANGE_F { get; set; }
        public System.String DIMENSCHANGE_F_TOR { get; set; }
        public System.Decimal? DIMENSCHANGE_F_Min { get; set; }
        public System.Decimal? DIMENSCHANGE_F_Max { get; set; }

        public System.String NUMTHREADS_W_Spe { get; set; }
        public System.String NUMTHREADS_F_Spe { get; set; }

        public System.String USABLE_Spe { get; set; }
        public System.String WIDTH_SILICONE_Spe { get; set; }

        public System.String TOTALWEIGHT_Spe { get; set; }
        public System.String UNCOATEDWEIGHT_Spe { get; set; }
        public System.String COATINGWEIGHT_Spe { get; set; }
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
        public System.String FLEXABRASION_W_Spe { get; set; }
        public System.String FLEXABRASION_F_Spe { get; set; }
        public System.String DIMENSCHANGE_W_Spe { get; set; }
        public System.String DIMENSCHANGE_F_Spe { get; set; }

        //Update 28/10/20
        public System.String THICKNESS_Spe { get; set; }
        public System.Decimal? THICKNESS_Min { get; set; }
        public System.Decimal? THICKNESS_Max { get; set; }
        public System.Decimal? THICKNESS_AVG { get; set; }
        public System.String THICKNESS_JUD { get; set; }
        public System.Decimal? THICKNESS_TOR { get; set; }

        public System.String BENDING_W_Spe { get; set; }
        public System.Decimal? BENDING_W_Min { get; set; }
        public System.Decimal? BENDING_W_Max { get; set; }
        public System.String BENDING_W_TOR { get; set; }
        public System.Decimal? BENDING_W_AVG { get; set; }
        public System.String BENDING_W_JUD { get; set; }

        public System.String BENDING_F_Spe { get; set; }
        public System.Decimal? BENDING_F_Min { get; set; }
        public System.Decimal? BENDING_F_Max { get; set; }
        public System.String BENDING_F_TOR { get; set; }
        public System.Decimal? BENDING_F_AVG { get; set; }
        public System.String BENDING_F_JUD { get; set; }   
        
        public System.String DYNAMIC_AIR_Spe { get; set; }
        public System.Decimal? DYNAMIC_AIR_Min { get; set; }
        public System.Decimal? DYNAMIC_AIR_Max { get; set; }
        public System.Decimal? DYNAMIC_AIR_TOR { get; set; }
        public System.Decimal? DYNAMIC_AIR_AVG { get; set; }
        public System.String DYNAMIC_AIR_JUD { get; set; }

        public System.String EXPONENT_Spe { get; set; }
        public System.Decimal? EXPONENT_Min { get; set; }
        public System.Decimal? EXPONENT_Max { get; set; }
        public System.Decimal? EXPONENT_TOR { get; set; }
        public System.Decimal? EXPONENT_AVG { get; set; }
        public System.String EXPONENT_JUD { get; set; }

        #endregion

        #region LAB_GETREPORTINFO
        // LAB_GETREPORTINFO
        public System.String REPORT_ID { get; set; }
        public System.String REVESION { get; set; }
        public System.String USABLE_WIDTH { get; set; }
        public System.String NUMTHREADS { get; set; }
        public System.String WEIGHT { get; set; }
        public System.String FLAMMABILITY { get; set; }
        public System.String EDGECOMB { get; set; }
        public System.String STIFFNESS { get; set; }
        public System.String TEAR { get; set; }
        public System.String STATIC_AIR { get; set; }
        public System.String FLEXABRASION { get; set; }
        public System.String DIMENSCHANGE { get; set; }
        public System.DateTime? EFFECTIVE_DATE { get; set; }

        // ปรับเพิ่ม
        public System.String YARNTYPE { get; set; }
        public System.String COATWEIGHT { get; set; }
        public System.String THICKNESS { get; set; }
        public System.String MAXFORCE { get; set; }
        public System.String ELONGATIONFORCE { get; set; }
        public System.String DYNAMIC_AIR { get; set; }
        public System.String EXPONENT { get; set; }

        public System.String REPORT_NAME { get; set; }

        public System.String BENDING { get; set; }

        //เพิ่ม 31/08/21
        public System.String BOW { get; set; }
        public System.String BOW_Spe { get; set; }
        public System.Decimal? BOW_Min { get; set; }
        public System.Decimal? BOW_Max { get; set; }
        public System.String BOW_TOR { get; set; }
        public System.Decimal? BOW_AVG { get; set; }
        public System.String BOW_JUD { get; set; }

        public System.Decimal? BOW1 { get; set; }
        public System.Decimal? BOW2 { get; set; }
        public System.Decimal? BOW3 { get; set; }

        public System.String SKEW { get; set; }
        public System.String SKEW_Spe { get; set; }
        public System.Decimal? SKEW_Min { get; set; }
        public System.Decimal? SKEW_Max { get; set; }
        public System.String SKEW_TOR { get; set; }
        public System.Decimal? SKEW_AVG { get; set; }
        public System.String SKEW_JUD { get; set; }
        public System.Decimal? SKEW1 { get; set; }
        public System.Decimal? SKEW2 { get; set; }
        public System.Decimal? SKEW3 { get; set; }

        public System.String FLEX_SCOTT { get; set; }
        public System.String FLEX_SCOTT_W_Spe { get; set; }
        public System.Decimal? FLEX_SCOTT_W_Min { get; set; }
        public System.Decimal? FLEX_SCOTT_W_Max { get; set; }
        public System.String FLEX_SCOTT_W_TOR { get; set; }
        public System.Decimal? FLEX_SCOTT_W_AVG { get; set; }
        public System.String FLEX_SCOTT_W_JUD { get; set; }
        public System.Decimal? FLEX_SCOTT_W1 { get; set; }
        public System.Decimal? FLEX_SCOTT_W2 { get; set; }
        public System.Decimal? FLEX_SCOTT_W3 { get; set; }

        public System.String FLEX_SCOTT_F_Spe { get; set; }
        public System.Decimal? FLEX_SCOTT_F_Min { get; set; }
        public System.Decimal? FLEX_SCOTT_F_Max { get; set; }
        public System.String FLEX_SCOTT_F_TOR { get; set; }
        public System.Decimal? FLEX_SCOTT_F_AVG { get; set; }
        public System.String FLEX_SCOTT_F_JUD { get; set; }
        public System.Decimal? FLEX_SCOTT_F1 { get; set; }
        public System.Decimal? FLEX_SCOTT_F2 { get; set; }
        public System.Decimal? FLEX_SCOTT_F3 { get; set; }

        public System.String FLEX_SCOTT_W_JUD2 { get; set; }
        public System.String FLEX_SCOTT_F_JUD2 { get; set; }
        #endregion
    }

    #endregion

    #region LAB_GETREPORTINFO_Rep

    public class LAB_GETREPORTINFO_Rep
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

        public System.String BOW { get; set; }
        public System.String SKEW { get; set; }
        public System.String FLEX_SCOTT { get; set; }

        public System.String REPORT_NAME { get; set; }

        public System.String BENDING { get; set; }
    }

    #endregion

    #region LAB_GETITEMTESTSPECIFICATION_Rep

    public class LAB_GETITEMTESTSPECIFICATION_Rep
    {
        public System.String ITM_CODE { get; set; }

        public System.Decimal? NUMTHREADS_W { get; set; }
        public System.Decimal? NUMTHREADS_W_TOR { get; set; }
        public System.Decimal? NUMTHREADS_W_Min { get; set; }
        public System.Decimal? NUMTHREADS_W_Max { get; set; }

        public System.Decimal? NUMTHREADS_F { get; set; }
        public System.Decimal? NUMTHREADS_F_TOR { get; set; }
        public System.Decimal? NUMTHREADS_F_Min { get; set; }
        public System.Decimal? NUMTHREADS_F_Max { get; set; }

        public System.Decimal? USABLE_WIDTH { get; set; }
        public System.String USABLE_WIDTH_TOR { get; set; }
        public System.Decimal? USABLE_WIDTH_Min { get; set; }

        public System.Decimal? WIDTH_SILICONE { get; set; }
        public System.String WIDTH_SILICONE_TOR { get; set; }
        public System.Decimal? WIDTH_SILICONE_Min { get; set; }

        public System.Decimal? TOTALWEIGHT { get; set; }
        public System.Decimal? TOTALWEIGHT_TOR { get; set; }
        public System.Decimal? TOTALWEIGHT_Min { get; set; }
        public System.Decimal? TOTALWEIGHT_Max { get; set; }

        public System.Decimal? UNCOATEDWEIGHT { get; set; }
        public System.Decimal? UNCOATEDWEIGHT_TOR { get; set; }
        public System.Decimal? UNCOATEDWEIGHT_Min { get; set; }
        public System.Decimal? UNCOATEDWEIGHT_Max { get; set; }

        public System.Decimal? COATINGWEIGHT { get; set; }
        public System.Decimal? COATINGWEIGHT_TOR { get; set; }
        public System.Decimal? COATINGWEIGHT_Min { get; set; }
        public System.Decimal? COATINGWEIGHT_Max { get; set; }

        public System.Decimal? MAXFORCE_W { get; set; }
        public System.String MAXFORCE_W_TOR { get; set; }
        public System.Decimal? MAXFORCE_W_Min { get; set; }

        public System.Decimal? MAXFORCE_F { get; set; }
        public System.String MAXFORCE_F_TOR { get; set; }
        public System.Decimal? MAXFORCE_F_Min { get; set; }

        public System.Decimal? ELONGATIONFORCE_W { get; set; }
        public System.String ELONGATIONFORCE_W_TOR { get; set; }
        public System.Decimal? ELONGATIONFORCE_W_Min { get; set; }
        public System.Decimal? ELONGATIONFORCE_W_Max { get; set; }

        public System.Decimal? ELONGATIONFORCE_F { get; set; }
        public System.String ELONGATIONFORCE_F_TOR { get; set; }
        public System.Decimal? ELONGATIONFORCE_F_Min { get; set; }
        public System.Decimal? ELONGATIONFORCE_F_Max { get; set; }

        public System.Decimal? FLAMMABILITY_W { get; set; }
        public System.String FLAMMABILITY_W_TOR { get; set; }
        public System.Decimal? FLAMMABILITY_W_Max { get; set; }

        public System.Decimal? FLAMMABILITY_F { get; set; }
        public System.String FLAMMABILITY_F_TOR { get; set; }
        public System.Decimal? FLAMMABILITY_F_Max { get; set; }

        public System.Decimal? EDGECOMB_W { get; set; }
        public System.String EDGECOMB_W_TOR { get; set; }
        public System.Decimal? EDGECOMB_W_Min { get; set; }

        public System.Decimal? EDGECOMB_F { get; set; }
        public System.String EDGECOMB_F_TOR { get; set; }
        public System.Decimal? EDGECOMB_F_Min { get; set; }

        public System.Decimal? STIFFNESS_W { get; set; }
        public System.String STIFFNESS_W_TOR { get; set; }
        public System.Decimal? STIFFNESS_W_Max { get; set; }

        public System.Decimal? STIFFNESS_F { get; set; }
        public System.String STIFFNESS_F_TOR { get; set; }
        public System.Decimal? STIFFNESS_F_Max { get; set; }

        public System.Decimal? TEAR_W { get; set; }
        public System.String TEAR_W_TOR { get; set; }
        public System.Decimal? TEAR_W_Min { get; set; }

        public System.Decimal? TEAR_F { get; set; }
        public System.String TEAR_F_TOR { get; set; }
        public System.Decimal? TEAR_F_Min { get; set; }

        public System.Decimal? STATIC_AIR { get; set; }
        public System.String STATIC_AIR_TOR { get; set; }
        public System.Decimal? STATIC_AIR_Max { get; set; }

        public System.Decimal? FLEXABRASION_W { get; set; }
        public System.String FLEXABRASION_W_TOR { get; set; }
        public System.Decimal? FLEXABRASION_W_Min { get; set; }

        public System.Decimal? FLEXABRASION_F { get; set; }
        public System.String FLEXABRASION_F_TOR { get; set; }
        public System.Decimal? FLEXABRASION_F_Min { get; set; }

        public System.Decimal? DIMENSCHANGE_W { get; set; }
        public System.String DIMENSCHANGE_W_TOR { get; set; }
        public System.Decimal? DIMENSCHANGE_W_Min { get; set; }
        public System.Decimal? DIMENSCHANGE_W_Max { get; set; }

        public System.Decimal? DIMENSCHANGE_F { get; set; }
        public System.String DIMENSCHANGE_F_TOR { get; set; }
        public System.Decimal? DIMENSCHANGE_F_Min { get; set; }
        public System.Decimal? DIMENSCHANGE_F_Max { get; set; }

     
        // ตัวแปรเอาไว้ใส่ค่าที่ได้จาก Database //
        public System.String NUMTHREADS_W_Spe { get; set; }
        public System.String NUMTHREADS_F_Spe { get; set; }

        public System.String USABLE_Spe { get; set; }
        public System.String WIDTH_SILICONE_Spe { get; set; }

        public System.String TOTALWEIGHT_Spe { get; set; }
        public System.String UNCOATEDWEIGHT_Spe { get; set; }
        public System.String COATINGWEIGHT_Spe { get; set; }
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
        public System.String FLEXABRASION_W_Spe { get; set; }
        public System.String FLEXABRASION_F_Spe { get; set; }
        public System.String DIMENSCHANGE_W_Spe { get; set; }
        public System.String DIMENSCHANGE_F_Spe { get; set; }

        // ปรับ 28/10/20
        public System.Decimal? THICKNESS { get; set; }
        public System.Decimal? THICKNESS_TOR { get; set; }
        public System.String THICKNESS_Spe { get; set; }
        public System.Decimal? THICKNESS_Min { get; set; }
        public System.Decimal? THICKNESS_Max { get; set; }

        public System.Decimal? BENDING_W { get; set; }
        public System.String BENDING_W_TOR { get; set; }
        public System.String BENDING_W_Spe { get; set; }
        public System.Decimal? BENDING_W_Min { get; set; }
        public System.Decimal? BENDING_W_Max { get; set; }

        public System.Decimal? BENDING_F { get; set; }
        public System.String BENDING_F_TOR { get; set; }
        public System.String BENDING_F_Spe { get; set; }
        public System.Decimal? BENDING_F_Min { get; set; }
        public System.Decimal? BENDING_F_Max { get; set; }

        public System.Decimal? DYNAMIC_AIR { get; set; }
        public System.Decimal? DYNAMIC_AIR_TOR { get; set; }
        public System.String DYNAMIC_AIR_Spe { get; set; }
        public System.Decimal? DYNAMIC_AIR_Min { get; set; }
        public System.Decimal? DYNAMIC_AIR_Max { get; set; }

        public System.Decimal? EXPONENT { get; set; }
        public System.Decimal? EXPONENT_TOR { get; set; }
        public System.String EXPONENT_Spe { get; set; }
        public System.Decimal? EXPONENT_Min { get; set; }
        public System.Decimal? EXPONENT_Max { get; set; }

        //31/08/21
        public System.Decimal? BOW { get; set; }
        public System.String BOW_TOR { get; set; }
        public System.String BOW_Spe { get; set; }
        public System.Decimal? BOW_Min { get; set; }
        public System.Decimal? BOW_Max { get; set; }

        public System.Decimal? SKEW { get; set; }
        public System.String SKEW_TOR { get; set; }
        public System.String SKEW_Spe { get; set; }
        public System.Decimal? SKEW_Min { get; set; }
        public System.Decimal? SKEW_Max { get; set; }

        public System.Decimal? FLEX_SCOTT_W { get; set; }
        public System.String FLEX_SCOTT_W_TOR { get; set; }
        public System.String FLEX_SCOTT_W_Spe { get; set; }
        public System.Decimal? FLEX_SCOTT_W_Min { get; set; }
        public System.Decimal? FLEX_SCOTT_W_Max { get; set; }

        public System.Decimal? FLEX_SCOTT_F { get; set; }
        public System.String FLEX_SCOTT_F_TOR { get; set; }
        public System.String FLEX_SCOTT_F_Spe { get; set; }
        public System.Decimal? FLEX_SCOTT_F_Min { get; set; }
        public System.Decimal? FLEX_SCOTT_F_Max { get; set; }

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

    #region LAB_GETREPORTIDLIST

    public class LAB_GETREPORTIDLIST
    {
        public System.String REPORT_ID { get; set; }
        public System.String REPORT_NAME { get; set; }
        public System.DateTime? EFFECTIVE_DATE { get; set; }
        public System.String USE_FLAG { get; set; }
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

    #region LAB_GETWEAVINGLOTLIST

    public class LAB_GETWEAVINGLOTLIST
    {
        public System.String WEAVINGLOT { get; set; }
    }

    #endregion

}
