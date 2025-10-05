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
    #region G3 Data

    /// <summary>
    /// The G3 Lot Data class.
    /// </summary>
    [Serializable]
    public class G3_SEARCHBYPALLETNOSearchData
    {
        #region Public Propeties

        public System.DateTime? ENTRYDATE { get; set; }
        public System.String ITM_YARN { get; set; }
        public System.String PALLETNO { get; set; }
        public System.String YARNTYPE { get; set; }
        public System.Decimal? WEIGHTQTY { get; set; }
        public System.Decimal? CONECH { get; set; }
        public System.String VERIFY { get; set; }
        public System.Decimal? REMAINQTY { get; set; }
        public System.String RECEIVEBY { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.DateTime? UPDATEDATE { get; set; }
        public System.String PALLETTYPE { get; set; }
        public System.String ITM400 { get; set; }
        public System.String UM { get; set; }
        public System.String PACKAING { get; set; }
        public System.String CLEAN { get; set; }
        public System.String TEARING { get; set; }
        public System.String FALLDOWN { get; set; }
        public System.String CERTIFICATION { get; set; }
        public System.String INVOICE { get; set; }
        public System.String IDENTIFYAREA { get; set; }
        public System.String AMOUNTPALLET { get; set; }
        public System.String OTHER { get; set; }
        public System.String ACTION { get; set; }
        public System.DateTime? MOVEMENTDATE { get; set; }
        public System.String LOTNO { get; set; }
        public System.String TRACENO { get; set; }

        #endregion
    }

    #endregion

    #region AS400 Data

    /// <summary>
    /// The G3 Lot Data class.
    /// </summary>
    [Serializable]
    public class AS400G3
    {
        #region Public Propeties

        public System.String CDEL0 { get; set; }

        public System.DateTime? MOVEMENTDATE { get; set; }
        public System.String ITM400 { get; set; }
        public System.String CDKE2 { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String LOTNO { get; set; }
        public System.String PALLETNO { get; set; }
        public System.Decimal? CONECH { get; set; }
        public System.Decimal? WEIGHTQTY { get; set; }
        public System.String TRACENO { get; set; }
        public System.String UM { get; set; }

        #endregion
    }

    #endregion

    #region ListG3_YarnData
    /// <summary>
    /// 
    /// </summary>
    public class ListG3_YarnData
    {
        public ListG3_YarnData()
        {
            // default constructor
        }

        public ListG3_YarnData(string palletNo, string lotorderNo, string verify, bool verifyOK, bool verifyNG
            , string flag, string operatorID, string packaging, string clean, string tearing, string falldown, string certification
            , string invoice, string identifyarea, string amountpallet, string other, string action
            , string itm_Yarn, decimal? weightQty, string yarnType, string traceNo)
        {
            PalletNo = palletNo;
            LotorderNo = lotorderNo;
            Verify = verify;
            VerifyOK = verifyOK;
            VerifyNG = verifyNG;
            //Remainqty = remainqty;
            Flag = flag;
            OperatorID = operatorID;
            Packaging = packaging;
            Clean = clean;
            Tearing = tearing;
            Falldown = falldown;
            Certification = certification;

            Packaging = packaging;
            Clean = clean;
            Tearing = tearing;
            Falldown = falldown;
            Certification = certification;
            Invoice = invoice;
            Identifyarea = identifyarea;
            Amountpallet = amountpallet;
            Other = other;
            Action = action;

            Itm_Yarn = itm_Yarn;
            WeightQty = weightQty;

            YarnType = yarnType;
            TraceNo = traceNo;
        }


        public string PalletNo { get; set; }
        public string LotorderNo { get; set; }
        public string Verify { get; set; }

        public bool VerifyOK { get; set; }
        public bool VerifyNG { get; set; }

        //public decimal? Remainqty { get; set; }
        public string Flag { get; set; }
        public string OperatorID { get; set; }

        public string Packaging { get; set; }
        public string Clean { get; set; }
        public string Tearing { get; set; }
        public string Falldown { get; set; }
        public string Certification { get; set; }
        public string Invoice { get; set; }
        public string Identifyarea { get; set; }
        public string Amountpallet { get; set; }
        public string Other { get; set; }
        public string Action { get; set; }

        public string Itm_Yarn { get; set; }
        public decimal? WeightQty { get; set; }

        public string YarnType { get; set; }
        public string TraceNo { get; set; }
    }
    #endregion

    #region ItemYarnBase

    /// <summary>
    /// MachineBase class.
    /// </summary>
    [Serializable]
    public abstract class ItemYarnBase
    {
        #region Public Properties

        [XmlAttribute]
        public string ITM_YARN { get; set; }

        #endregion
    }

    #endregion

    #region ItemYarnItem

    /// <summary>
    /// The Inspection M/C Item.
    /// </summary>
    [Serializable]
    public class ItemYarnItem : ItemYarnBase
    {
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ G3_SEARCHYARNSTOCKDATA
    #region G3_SEARCHYARNSTOCKData

    /// <summary>
    /// The Inspection Lot Data class.
    /// </summary>
    [Serializable]
    public class G3_SEARCHYARNSTOCKData
    {
        #region Public Propeties

        public System.Boolean SelectData { get; set; }

        public System.Int32 RowNo { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String ITM_YARN { get; set; }
        public System.String PALLETNO { get; set; }
        public System.String YARNTYPE { get; set; }
        public System.Decimal? WEIGHTQTY { get; set; }
        public System.Decimal? CONECH { get; set; }
        public System.String VERIFY { get; set; }
        public System.Decimal? REMAINQTY { get; set; }
        public System.String RECEIVEBY { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.DateTime? UPDATEDATE { get; set; }
        public System.String PALLETTYPE { get; set; }
        public System.String ITM400 { get; set; }
        public System.String UM { get; set; }
        public System.String PACKAING { get; set; }
        public System.String CLEAN { get; set; }
        public System.String TEARING { get; set; }
        public System.String FALLDOWN { get; set; }
        public System.String CERTIFICATION { get; set; }
        public System.String INVOICE { get; set; }
        public System.String IDENTIFYAREA { get; set; }
        public System.String AMOUNTPALLET { get; set; }
        public System.String OTHER { get; set; }
        public System.String ACTION { get; set; }
        public System.DateTime? MOVEMENTDATE { get; set; }
        public System.String LOTNO { get; set; }
        public System.String TRACENO { get; set; }

        //เพิ่ม 08/07/16
        public System.Decimal? KGPERCH { get; set; }

        #endregion
    }

    #endregion

    #region G3_GETREQUESTNODETAI

    public class G3_GETREQUESTNODETAIL
    {
        public System.Int32 RowNo { get; set; }
        public System.DateTime? ISSUEDATE { get; set; }
        public System.String PALLETNO { get; set; }
        public System.String TRACENO { get; set; }
        public System.Decimal? WEIGHT { get; set; }
        public System.Decimal? CH { get; set; }
        public System.String ISSUEBY { get; set; }
        public System.String ISSUETO { get; set; }
        public System.String REQUESTNO { get; set; }
        public System.String PALLETTYPE { get; set; }
        public System.String ITM_YARN { get; set; }
        public System.String LOTNO { get; set; }
        public System.String YARNTYPE { get; set; }
        public System.String ITM400 { get; set; }
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String PACKAING { get; set; }
        public System.String CLEAN { get; set; }
        public System.String FALLDOWN { get; set; }
        public System.String TEARING { get; set; }
        public System.Boolean NewData { get; set; }

        public System.String DELETEFLAG { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.String EDITBY { get; set; }
        public System.String REMARK { get; set; }
        
        //public System.String DELETEFLAG { get; set; }
        //public System.DateTime? EDITDATE { get; set; }
        //public System.String EDITBY { get; set; }
        //public System.String REMARK { get; set; }
    }

    #endregion

    #region G3_GETPALLETDETAIL 

    public class G3_GETPALLETDETAIL
    {
        public System.DateTime? ENTRYDATE { get; set; }
        public System.String ITM_YARN { get; set; }
        public System.String PALLETNO { get; set; }
        public System.String YARNTYPE { get; set; }
        public System.Decimal? WEIGHTQTY { get; set; }
        public System.Decimal? CONECH { get; set; }
        public System.String VERIFY { get; set; }
        public System.Decimal? REMAINQTY { get; set; }
        public System.String RECEIVEBY { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.DateTime? UPDATEDATE { get; set; }
        public System.String PALLETTYPE { get; set; }
        public System.String ITM400 { get; set; }
        public System.String UM { get; set; }
        public System.String PACKAING { get; set; }
        public System.String CLEAN { get; set; }
        public System.String TEARING { get; set; }
        public System.String FALLDOWN { get; set; }
        public System.String CERTIFICATION { get; set; }
        public System.String INVOICE { get; set; }
        public System.String IDENTIFYAREA { get; set; }
        public System.String AMOUNTPALLET { get; set; }
        public System.String OTHER { get; set; }
        public System.String ACTION { get; set; }
        public System.DateTime? MOVEMENTDATE { get; set; }
        public System.String LOTNO { get; set; }
        public System.String TRACENO { get; set; }
        public System.Decimal? KGPERCH { get; set; }
    }

    #endregion

    #region G3_INSERTRETURNYARN

    public class G3_INSERTRETURNYARN
    {
        public System.Int32 RowNo { get; set; }
        public System.String PALLETNO { get; set; }
        public System.String LOTNO { get; set; }
        public System.String ITM400 { get; set; }

        public System.String TRACENO { get; set; }
        public System.String NEWTRACENO { get; set; }
        public System.Decimal? CH { get; set; }
        public System.Decimal? WEIGHT { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String OPERATOR { get; set; }
        public System.String ITEMYARN { get; set; }
        public System.String YARNTYPE { get; set; }
        public System.String RETURNBY { get; set; }
        public System.String GRADE { get; set; }
    }

    #endregion

    #region G3 Session

    /// <summary>
    /// G3 Session.
    /// </summary>
    [Serializable]
    public class G3Session
    {
        #region Internal Variables

        private string _truckno  = string.Empty;
        private string _desc  = string.Empty;
        private string _palletNo  = string.Empty;
        private decimal? _ch = null;
        private decimal? _weight  = null;
        private string _lotorderNo  = string.Empty;
        private string _itmorder  = string.Empty;
        private string _receiveDate = string.Empty;
        private string _um = string.Empty;
        private string _itmYarn  = string.Empty;
        private string _type = string.Empty;

        // ใช้กับ Update
        private string _verify = string.Empty;
        private string _flag = string.Empty;
        private string _operatorID = string.Empty;
        private DateTime? _receiveG3Date = null;
        private string _packaging  = string.Empty;
        private string _clean = string.Empty;
        private string _tearing = string.Empty;
        private string _falldown = string.Empty;
        private string _certification = string.Empty;
        private string _invoice = string.Empty;
        private string _identifyarea = string.Empty;
        private string _amountpallet = string.Empty;
        private string _other = string.Empty;
        private string _action = string.Empty;

        private string _traceNo = string.Empty;
        private string _yarnType = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public G3Session()
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

        #region New

        public void New()
        {
            _truckno = string.Empty;
            _desc = string.Empty;
            _palletNo = string.Empty;
            _ch = null;
            _weight = null;
            _lotorderNo = string.Empty;
            _itmorder = string.Empty;
            _receiveDate = string.Empty;
            _um = string.Empty;
            _itmYarn = string.Empty;
            _type = string.Empty;
        }

        public void NewUpdateG3_yarn()
        {
            _palletNo = string.Empty;
            _lotorderNo = string.Empty;
            _itmYarn = string.Empty;
            _weight = null;
            _verify = string.Empty;
            _flag = string.Empty;
            _operatorID = string.Empty;
            _receiveG3Date = null;
            _packaging = string.Empty;
            _clean = string.Empty;
            _tearing = string.Empty;
            _falldown = string.Empty;
            _certification = string.Empty;
            _invoice = string.Empty;
            _identifyarea = string.Empty;
            _amountpallet = string.Empty;
            _other = string.Empty;
            _action = string.Empty;

            _traceNo = string.Empty;
            _yarnType = string.Empty;
        }

        #endregion

        #region Save

        public bool SaveG3_yarn()
        {
            bool chkSave = true;

            try
            {
                if (G3DataService.Instance.SaveG3_yarn(_truckno, _desc, _palletNo, _ch, _weight, _lotorderNo, _itmorder, _receiveDate, _um, _itmYarn, _type) == true)
                {
                    chkSave = true;
                }
                else
                {
                    chkSave = false;
                }
            }
            catch
            {
                chkSave = false;
            }

            return chkSave;
        }

        public bool G3_RECEIVEYARN()
        {
            bool chkSave = true;

            try
            {
                if (G3DataService.Instance.G3_RECEIVEYARN(_traceNo, _lotorderNo, _verify, _weight, _flag, _operatorID, _receiveG3Date, null, _type, _packaging, _clean, _tearing
                    , _falldown, _certification, _invoice, _identifyarea, _amountpallet, _other, _action) == true)
                {
                    chkSave = true;
                }
                else
                {
                    chkSave = false;
                }
            }
            catch
            {
                chkSave = false;
            }

            return chkSave;
        }

        #endregion

        #region CheckItemCodeData

        public bool CheckItemCodeData(string itmyarn)
        {
            bool chkItemCode = true;

            try
            {
                if (G3DataService.Instance.CheckITMYARN(itmyarn) == true)
                {
                    chkItemCode = true;
                }
                else
                {
                    chkItemCode = false;
                }
            }
            catch
            {
                chkItemCode = false;
            }

            return chkItemCode;
        }

        #endregion

        #endregion

        #region Public Proeprties

        /// <summary>
        /// Gets or sets Truckno.
        /// </summary>
        [XmlAttribute]
        public string TruckNo
        {
            get { return _truckno; }
            set
            {
                if (_truckno != value)
                {
                    _truckno = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Desc
        {
            get { return _desc; }
            set
            {
                if (_desc != value)
                {
                    _desc = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string PalletNo
        {
            get { return _palletNo; }
            set
            {
                if (_palletNo != value)
                {
                    _palletNo = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public decimal? CH
        {
            get { return _ch; }
            set
            {
                if (_ch != value)
                {
                    _ch = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public decimal? Weight
        {
            get { return _weight; }
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string LotorderNo 
        {
            get { return _lotorderNo; }
            set
            {
                if (_lotorderNo != value)
                {
                    _lotorderNo = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Itmorder 
        {
            get { return _itmorder; }
            set
            {
                if (_itmorder != value)
                {
                    _itmorder = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string ReceiveDate
        {
            get { return _receiveDate; }
            set
            {
                if (_receiveDate != value)
                {
                    _receiveDate = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string UM 
        {
            get { return _um; }
            set
            {
                if (_um != value)
                {
                    _um = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string ItmYarn
        {
            get { return _itmYarn; }
            set
            {
                if (_itmYarn != value)
                {
                    _itmYarn = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                }
            }
        }

        // ใช้กับ Update 
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Verify
        {
            get { return _verify; }
            set
            {
                if (_verify != value)
                {
                    _verify = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string OperatorID
        {
            get { return _operatorID; }
            set
            {
                if (_operatorID != value)
                {
                    _operatorID = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public DateTime? ReceiveG3Date
        {
            get { return _receiveG3Date; }
            set
            {
                if (_receiveG3Date != value)
                {
                    _receiveG3Date = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Packaging
        {
            get { return _packaging; }
            set
            {
                if (_packaging != value)
                {
                    _packaging = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Clean
        {
            get { return _clean; }
            set
            {
                if (_clean != value)
                {
                    _clean = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Tearing
        {
            get { return _tearing; }
            set
            {
                if (_tearing != value)
                {
                    _tearing = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Falldown
        {
            get { return _falldown; }
            set
            {
                if (_falldown != value)
                {
                    _falldown = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Certification
        {
            get { return _certification; }
            set
            {
                if (_certification != value)
                {
                    _certification = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Invoice
        {
            get { return _invoice; }
            set
            {
                if (_invoice != value)
                {
                    _invoice = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Identifyarea
        {
            get { return _identifyarea; }
            set
            {
                if (_identifyarea != value)
                {
                    _identifyarea = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Amountpallet
        {
            get { return _amountpallet; }
            set
            {
                if (_amountpallet != value)
                {
                    _amountpallet = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Other
        {
            get { return _other; }
            set
            {
                if (_other != value)
                {
                    _other = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Action
        {
            get { return _action; }
            set
            {
                if (_action != value)
                {
                    _action = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string TraceNo
        {
            get { return _traceNo; }
            set
            {
                if (_traceNo != value)
                {
                    _traceNo = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string YarnType
        {
            get { return _yarnType; }
            set
            {
                if (_yarnType != value)
                {
                    _yarnType = value;
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
