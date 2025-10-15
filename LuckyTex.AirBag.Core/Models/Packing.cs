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
    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ FINISHING_GETITEMGOOD
    #region GetItemCodeData

    /// <summary>
    /// The GetItemCodeData class.
    /// </summary>
    public class GetItemCodeData
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

    #region PACK_SEARCHINSPECTIONDATA 

    public class PACK_SEARCHINSPECTIONDATA
    {
        #region Public Propeties

        public System.String INSPECTIONLOT { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? GROSSLENGTH { get; set; }
        public System.Decimal? NETLENGTH { get; set; }
        public System.String CUSTOMERID { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String GRADE { get; set; }
        public System.Decimal? GROSSWEIGHT { get; set; }
        public System.Decimal? NETWEIGHT { get; set; }
        public System.String PEINSPECTIONLOT { get; set; }
        public System.String DEFECTID { get; set; }
        public System.String REMARK { get; set; }
        public System.String ATTACHID { get; set; }
        public System.String TESTRECORDID { get; set; }
        public System.String INSPECTEDBY { get; set; }
        public System.String MCNO { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.DateTime? SUSPENDDATE { get; set; }
        public System.String INSPECTIONID { get; set; }
        public System.String RETEST { get; set; }
        public System.String PREITEMCODE { get; set; }
        public System.String CLEARBY { get; set; }
        public System.String CLEARREMARK { get; set; }
        public System.String SUSPENDBY { get; set; }
        public System.DateTime? STARTDATE1 { get; set; }
        public System.String CUSTOMERTYPE { get; set; }
        public System.String DEFECTFILENAME { get; set; }
        public System.String ISPACKED { get; set; }
        public System.String LOADINGTYPE { get; set; }
        public System.String ITM_GROUP { get; set; }

        public System.String OPERATOR_GROUP { get; set; }
        public System.String DF_CODE { get; set; }
        public System.Decimal? DF_AMOUNT { get; set; }
        public System.Decimal? DF_POINT { get; set; }

        #endregion
    }

    #endregion

    #region PACK_SEARCHPALLETLIST 

    public class PACK_SEARCHPALLETLIST
    {
        public System.String PALLETNO { get; set; }
        public System.DateTime? PACKINGDATE { get; set; }
        public System.String PACKINGBY { get; set; }
        public System.String CHECKBY { get; set; }
        public System.Boolean PalletChecking { get; set; }
        public System.DateTime? CHECKINGDATE { get; set; }
        public System.String REMARK { get; set; }
        public System.String COMPLETELAB { get; set; }
        public System.String TRANSFERAS400 { get; set; }
        public System.String FLAG { get; set; }
    }

    #endregion

    #region PACK_GETPALLETDETAIL 

    public class PACK_GETPALLETDETAIL
    {
        public System.Int32 RowNo { get; set; }

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
        public System.String FLAG { get; set; }
        public System.String LOADINGTYPE { get; set; }
        public System.String STOCK { get; set; }
        public System.Decimal? GROSSLENGTH { get; set; }
        public System.Decimal? ORDERNO { get; set; }

        public System.String ITM_GROUP { get; set; }

        public System.String INSPECTIONLOT_OLD { get; set; }
    }

    #endregion

    #region PACK_PALLETSHEET 

    public class PACK_PALLETSHEET
    {
        public System.String PALLETNO { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.String CUSTOMERTYPE { get; set; }
        public System.String INSPECTIONLOT { get; set; }
        public System.String GRADE { get; set; }
        public System.Decimal? GROSSWEIGHT { get; set; }
        public System.Decimal? NETLENGTH { get; set; }
        public System.Decimal? NETWEIGHT { get; set; }
        public System.DateTime? PACKINGDATE { get; set; }
        public System.String PACKINGBY { get; set; }
        public System.String CHECKBY { get; set; }
        public System.DateTime? CHECKINGDATE { get; set; }

        public System.String LOADINGTYPE { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.String YARNCODE { get; set; }
    }

    #endregion

    #region PACK_PRINTLABEL 

    public class PACK_PRINTLABEL
    {
        public System.String INSPECTIONLOT { get; set; }
        public System.Decimal? QUANTITY { get; set; }
        public System.Decimal? GROSSWEIGHT { get; set; }
        public System.Decimal? NETWEIGHT { get; set; }
        public System.String GRADE { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.String DESCRIPTION { get; set; }
        public System.String SUPPLIERCODE { get; set; }
        public System.String BARCODEBACTHNO { get; set; }
        public System.String CUSTOMERPARTNO { get; set; }
        public System.String BATCHNO { get; set; }
        public System.String PDATE { get; set; }

        public System.String CUSTOMERID { get; set; }
        public System.String BarcodeCMPARTNO { get; set; }

        //INC เพิ่มเอง
        public System.String FINISHINGPROCESS { get; set; }

        public System.String DBARCODE { get; set; }

        public System.String BDate { get; set; }
        public System.String CUSPARTNO2D { get; set; }

        // เพิ่ม 24/06/25
        public System.Decimal? GROSSLENGTH { get; set; }
    }

    #endregion

    #region PalletData

    public class PalletData
    {
        #region Public Propeties
        public System.Int32 RowNo { get; set; }
        public System.String INSPECTIONLOT { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? GROSSLENGTH { get; set; }
        public System.Decimal? NETLENGTH { get; set; }
        public System.String CUSTOMERID { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String GRADE { get; set; }
        public System.Decimal? GROSSWEIGHT { get; set; }
        public System.Decimal? NETWEIGHT { get; set; }
        public System.String PEINSPECTIONLOT { get; set; }
        public System.String DEFECTID { get; set; }
        public System.String REMARK { get; set; }
        public System.String ATTACHID { get; set; }
        public System.String TESTRECORDID { get; set; }
        public System.String INSPECTEDBY { get; set; }
        public System.String MCNO { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.DateTime? SUSPENDDATE { get; set; }
        public System.String INSPECTIONID { get; set; }
        public System.String RETEST { get; set; }
        public System.String PREITEMCODE { get; set; }
        public System.String CLEARBY { get; set; }
        public System.String CLEARREMARK { get; set; }
        public System.String SUSPENDBY { get; set; }
        public System.DateTime? STARTDATE1 { get; set; }
        public System.String CUSTOMERTYPE { get; set; }
        public System.String DEFECTFILENAME { get; set; }
        public System.String ISPACKED { get; set; }
        public System.String LOADINGTYPE { get; set; }
        public System.String ITM_GROUP { get; set; }

        #endregion
    }

    #endregion

    #region Packing Session

    /// <summary>
    /// Finishing Session.
    /// </summary>
    [Serializable]
    public class PackingSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        private string _INSPECTIONLOT = string.Empty;
        private string _ITEMCODE = string.Empty;
        private string _FINISHINGLOT = string.Empty;
        private DateTime? _STARTDATE = null;
        private DateTime? _ENDDATE = null;
        private decimal? _GROSSLENGTH = null;
        private decimal? _NETLENGTH = null;
        private string _CUSTOMERID = string.Empty;
        private string _PRODUCTTYPEID = string.Empty;
        private string _GRADE = string.Empty;
        private decimal? _GROSSWEIGHT = null;
        private decimal? _NETWEIGHT = null;
        private string _PEINSPECTIONLOT = string.Empty;
        private string _DEFECTID = string.Empty;
        private string _REMARK = string.Empty;
        private string _ATTACHID = string.Empty;
        private string _TESTRECORDID = string.Empty;
        private string _INSPECTEDBY = string.Empty;
        private string _MCNO = string.Empty;
        private string _FINISHFLAG = string.Empty;
        private DateTime? _SUSPENDDATE = null;
        private string _INSPECTIONID = string.Empty;
        private string _RETEST = string.Empty;
        private string _PREITEMCODE = string.Empty;
        private string _CLEARBY = string.Empty;
        private string _CLEARREMARK = string.Empty;
        private string _SUSPENDBY = string.Empty;
        private DateTime? _STARTDATE1 = null;
        private string _CUSTOMERTYPE = string.Empty;
        private string _DEFECTFILENAME = string.Empty;
        private string _ISPACKED = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PackingSession()
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
        #region Load Combo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusID"></param>
        /// <returns></returns>
        public List<GetItemCodeData> GetItemCodeData()
        {
            List<GetItemCodeData> results = PackingDataService.Instance
                .GetItemCodeData();

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

        [XmlAttribute]
        public string INSPECTIONLOT
        {
            get { return _INSPECTIONLOT; }
            set
            {
                if (_INSPECTIONLOT != value)
                {
                    _INSPECTIONLOT = value;
                }
            }
        }
        [XmlAttribute]
        public string ITEMCODE
        {
            get { return _ITEMCODE; }
            set
            {
                if (_ITEMCODE != value)
                {
                    _ITEMCODE = value;
                }
            }
        }
        [XmlAttribute]
        public string FINISHINGLOT
        {
            get { return _FINISHINGLOT; }
            set
            {
                if (_FINISHINGLOT != value)
                {
                    _FINISHINGLOT = value;
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
        public decimal? GROSSLENGTH
        {
            get { return _GROSSLENGTH; }
            set
            {
                if (_GROSSLENGTH != value)
                {
                    _GROSSLENGTH = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? NETLENGTH
        {
            get { return _NETLENGTH; }
            set
            {
                if (_NETLENGTH != value)
                {
                    _NETLENGTH = value;
                }
            }
        }
        [XmlAttribute]
        public string CUSTOMERID
        {
            get { return _CUSTOMERID; }
            set
            {
                if (_CUSTOMERID != value)
                {
                    _CUSTOMERID = value;
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
        public string GRADE
        {
            get { return _GRADE; }
            set
            {
                if (_GRADE != value)
                {
                    _GRADE = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? GROSSWEIGHT
        {
            get { return _GROSSWEIGHT; }
            set
            {
                if (_GROSSWEIGHT != value)
                {
                    _GROSSWEIGHT = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? NETWEIGHT
        {
            get { return _NETWEIGHT; }
            set
            {
                if (_NETWEIGHT != value)
                {
                    _NETWEIGHT = value;
                }
            }
        }
        [XmlAttribute]
        public string PEINSPECTIONLOT
        {
            get { return _PEINSPECTIONLOT; }
            set
            {
                if (_PEINSPECTIONLOT != value)
                {
                    _PEINSPECTIONLOT = value;
                }
            }
        }
        [XmlAttribute]
        public string DEFECTID
        {
            get { return _DEFECTID; }
            set
            {
                if (_DEFECTID != value)
                {
                    _DEFECTID = value;
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
        public string ATTACHID
        {
            get { return _ATTACHID; }
            set
            {
                if (_ATTACHID != value)
                {
                    _ATTACHID = value;
                }
            }
        }
        [XmlAttribute]
        public string TESTRECORDID
        {
            get { return _TESTRECORDID; }
            set
            {
                if (_TESTRECORDID != value)
                {
                    _TESTRECORDID = value;
                }
            }
        }
        [XmlAttribute]
        public string INSPECTEDBY
        {
            get { return _INSPECTEDBY; }
            set
            {
                if (_INSPECTEDBY != value)
                {
                    _INSPECTEDBY = value;
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
        public string FINISHFLAG
        {
            get { return _FINISHFLAG; }
            set
            {
                if (_FINISHFLAG != value)
                {
                    _FINISHFLAG = value;
                }
            }
        }
        [XmlAttribute]
        public DateTime? SUSPENDDATE
        {
            get { return _SUSPENDDATE; }
            set
            {
                if (_SUSPENDDATE != value)
                {
                    _SUSPENDDATE = value;
                }
            }
        }
        [XmlAttribute]
        public string INSPECTIONID
        {
            get { return _INSPECTIONID; }
            set
            {
                if (_INSPECTIONID != value)
                {
                    _INSPECTIONID = value;
                }
            }
        }
        [XmlAttribute]
        public string RETEST
        {
            get { return _RETEST; }
            set
            {
                if (_RETEST != value)
                {
                    _RETEST = value;
                }
            }
        }
        [XmlAttribute]
        public string PREITEMCODE
        {
            get { return _PREITEMCODE; }
            set
            {
                if (_PREITEMCODE != value)
                {
                    _PREITEMCODE = value;
                }
            }
        }
        [XmlAttribute]
        public string CLEARBY
        {
            get { return _CLEARBY; }
            set
            {
                if (_CLEARBY != value)
                {
                    _CLEARBY = value;
                }
            }
        }
        [XmlAttribute]
        public string CLEARREMARK
        {
            get { return _CLEARREMARK; }
            set
            {
                if (_CLEARREMARK != value)
                {
                    _CLEARREMARK = value;
                }
            }
        }
        [XmlAttribute]
        public string SUSPENDBY
        {
            get { return _SUSPENDBY; }
            set
            {
                if (_SUSPENDBY != value)
                {
                    _SUSPENDBY = value;
                }
            }
        }
        [XmlAttribute]
        public DateTime? STARTDATE1
        {
            get { return _STARTDATE1; }
            set
            {
                if (_STARTDATE1 != value)
                {
                    _STARTDATE1 = value;
                }
            }
        }
        [XmlAttribute]
        public string CUSTOMERTYPE
        {
            get { return _CUSTOMERTYPE; }
            set
            {
                if (_CUSTOMERTYPE != value)
                {
                    _CUSTOMERTYPE = value;
                }
            }
        }
        [XmlAttribute]
        public string DEFECTFILENAME
        {
            get { return _DEFECTFILENAME; }
            set
            {
                if (_DEFECTFILENAME != value)
                {
                    _DEFECTFILENAME = value;
                }
            }
        }
        [XmlAttribute]
        public string ISPACKED
        {
            get { return _ISPACKED; }
            set
            {
                if (_ISPACKED != value)
                {
                    _ISPACKED = value;
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
