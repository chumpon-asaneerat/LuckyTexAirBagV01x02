#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
#endregion

namespace DataControl.ClassData
{
    public class ConmonReportService
    {
        private static ConmonReportService _instance = null;

        #region ConmonReportService
        public static ConmonReportService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ConmonReportService))
                    {
                        _instance = new ConmonReportService();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Current
        private Stack<ContentControl> _currents = new Stack<ContentControl>();

        private ConmonReportService() { }

        public ContentControl Current
        {
            get
            {
                if (_currents.Count > 0)
                {
                    return _currents.Peek();
                }
                else return null;
            }
            set
            {
                if (null == value)
                    return;

                ContentControl last = null;
                if (_currents.Count > 0)
                {
                    last = _currents.Peek();
                }
                if (last != value)
                {
                    _currents.Push(value);

                    if (null != OnContentChanged)
                    {
                        OnContentChanged(this, EventArgs.Empty);
                    }
                }
            }
        }
        #endregion

        #region ReportName
        private string _reportName = string.Empty;
        public string ReportName
        {

            get { return _reportName; }
            set
            {
                if (_reportName != value)
                {
                    _reportName = value;
                }
            }
        }
        #endregion

        #region CmdString
        private string _cmdString = string.Empty;
        public string CmdString
        {

            get { return _cmdString; }
            set
            {
                if (_cmdString != value)
                {
                    _cmdString = value;
                }
            }
        }
        #endregion

        #region CmdFormDate
        private string _cmdFromDate = string.Empty;
        public string CmdFromDate
        {

            get { return _cmdFromDate; }
            set
            {
                if (_cmdFromDate != value)
                {
                    _cmdFromDate = value;
                }
            }
        }
        #endregion

        #region CmdToDate
        private string _cmdToDate = string.Empty;
        public string CmdToDate
        {

            get { return _cmdToDate; }
            set
            {
                if (_cmdToDate != value)
                {
                    _cmdToDate = value;
                }
            }
        }
        #endregion

        #region CmdStringDate
        private string _cmdStringDate = string.Empty;
        public string CmdStringDate
        {

            get { return _cmdStringDate; }
            set
            {
                if (_cmdStringDate != value)
                {
                    _cmdStringDate = value;
                }
            }
        }
        #endregion

        #region CmdStringStartDate
        private string _cmdStringStartDate = string.Empty;
        public string CmdStringStartDate
        {

            get { return _cmdStringStartDate; }
            set
            {
                if (_cmdStringStartDate != value)
                {
                    _cmdStringStartDate = value;
                }
            }
        }
        #endregion

        #region CmdStringEndDate
        private string _cmdStringEndDate = string.Empty;
        public string CmdStringEndDate
        {

            get { return _cmdStringEndDate; }
            set
            {
                if (_cmdStringEndDate != value)
                {
                    _cmdStringEndDate = value;
                }
            }
        }
        #endregion

        #region CmdChkDataRow
        private bool _cmdChkDataRow = true;
        public bool CmdChkDataRow
        {

            get { return _cmdChkDataRow; }
            set
            {
                if (_cmdChkDataRow != value)
                {
                    _cmdChkDataRow = value;
                }
            }
        }
        #endregion

        #region CmdCountTestData
        private int _cmdCountTestData = 0;
        public int CmdCountTestData
        {

            get { return _cmdCountTestData; }
            set
            {
                if (_cmdCountTestData != value)
                {
                    _cmdCountTestData = value;
                }
            }
        }
        #endregion

        #region WEAVINGLOT
        private string _WEAVINGLOT = string.Empty;
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
        #endregion

        #region WEAVINGDATE
        private string _WEAVINGDATE = string.Empty;
        public string WEAVINGDATE
        {

            get { return _WEAVINGDATE; }
            set
            {
                if (_WEAVINGDATE != value)
                {
                    _WEAVINGDATE = value;
                }
            }
        }
        #endregion

        #region CHINA
        private string _CHINA = string.Empty;
        public string CHINA
        {

            get { return _CHINA; }
            set
            {
                if (_CHINA != value)
                {
                    _CHINA = value;
                }
            }
        }
        #endregion

        #region WARPHEADNO
        private string _WARPHEADNO = string.Empty;
        public string WARPHEADNO
        {

            get { return _WARPHEADNO; }
            set
            {
                if (_WARPHEADNO != value)
                {
                    _WARPHEADNO = value;
                }
            }
        }
        #endregion

        #region SIDE
        private string _SIDE = string.Empty;
        public string SIDE
        {

            get { return _SIDE; }
            set
            {
                if (_SIDE != value)
                {
                    _SIDE = value;
                }
            }
        }
        #endregion

        #region WARPLOT
        private string _WARPLOT = string.Empty;
        public string WARPLOT
        {

            get { return _WARPLOT; }
            set
            {
                if (_WARPLOT != value)
                {
                    _WARPLOT = value;
                }
            }
        }
        #endregion

        #region BEAMLOT
        private string _BEAMLOT = string.Empty;
        public string BEAMLOT
        {

            get { return _BEAMLOT; }
            set
            {
                if (_BEAMLOT != value)
                {
                    _BEAMLOT = value;
                }
            }
        }
        #endregion

        #region BEAMERNO
        private string _BEAMERNO = string.Empty;
        public string BEAMERNO
        {

            get { return _BEAMERNO; }
            set
            {
                if (_BEAMERNO != value)
                {
                    _BEAMERNO = value;
                }
            }
        }
        #endregion

        #region BARNO
        private string _BARNO = string.Empty;
        public string BARNO
        {

            get { return _BARNO; }
            set
            {
                if (_BARNO != value)
                {
                    _BARNO = value;
                }
            }
        }
        #endregion

        #region LOOM
        private string _LOOM = string.Empty;
        public string LOOM
        {

            get { return _LOOM; }
            set
            {
                if (_LOOM != value)
                {
                    _LOOM = value;
                }
            }
        }
        #endregion

        #region INSLOT
        private string _INSLOT = string.Empty;
        public string INSLOT
        {

            get { return _INSLOT; }
            set
            {
                if (_INSLOT != value)
                {
                    _INSLOT = value;
                }
            }
        }
        #endregion
        
        #region FinishingLot
        private string _FinishingLot = string.Empty;
        public string FinishingLot
        {

            get { return _FinishingLot; }
            set
            {
                if (_FinishingLot != value)
                {
                    _FinishingLot = value;
                }
            }
        }
        #endregion

        #region ITM_Code
        private string _itm_code = string.Empty;
        public string ITM_Code
        {
            get { return _itm_code; }
            set
            {
                if (_itm_code != value)
                {
                    _itm_code = value;
                }
            }
        }
        #endregion

        #region ITEMLOT
        private string _ITEMLOT = string.Empty;
        public string ITEMLOT
        {
            get { return _ITEMLOT; }
            set
            {
                if (_ITEMLOT != value)
                {
                    _ITEMLOT = value;
                }
            }
        }
        #endregion

        #region ITMPREPARE
        private string _ITMPREPARE = string.Empty;
        public string ITMPREPARE
        {

            get { return _ITMPREPARE; }
            set
            {
                if (_ITMPREPARE != value)
                {
                    _ITMPREPARE = value;
                }
            }
        }
        #endregion

        #region WTYPE
        private string _WTYPE = string.Empty;
        public string WTYPE 
        {

            get { return _WTYPE; }
            set
            {
                if (_WTYPE != value)
                {
                    _WTYPE = value;
                }
            }
        }
        #endregion

        #region WEFTYARN
        private string _WEFTYARN = string.Empty;
        public string WEFTYARN
        {

            get { return _WEFTYARN; }
            set
            {
                if (_WEFTYARN != value)
                {
                    _WEFTYARN = value;
                }
            }
        }
        #endregion

        #region CONDITIONBY
        private string _CONDITIONBY = string.Empty;
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
        #endregion

        #region REEDNO
        private string _REEDNO = string.Empty;
        public string REEDNO
        {

            get { return _REEDNO; }
            set
            {
                if (_REEDNO != value)
                {
                    _REEDNO = value;
                }
            }
        }
        #endregion

        #region CONDITIONSTART
        private DateTime? _CONDITIONSTART = null;
        public DateTime? CONDITIONSTART
        {

            get { return _CONDITIONSTART; }
            set
            {
                if (_CONDITIONSTART != value)
                {
                    _CONDITIONSTART = value;
                }
            }
        }
        #endregion

        #region PALLETNO
        private string _PALLETNO = string.Empty;
        public string PALLETNO
        {
            get { return _PALLETNO; }
            set
            {
                if (_PALLETNO != value)
                {
                    _PALLETNO = value;
                }
            }
        }
        #endregion

        #region MCNO
        private string _MCNO = string.Empty;
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
        #endregion

        #region P_MC
        private string _MC = string.Empty;
        public string P_MC
        {

            get { return _MC; }
            set
            {
                if (_MC != value)
                {
                    _MC = value;
                }
            }
        }
        #endregion

        #region P_BEAMERROLL
        private string _BEAMERROLL = string.Empty;
        public string P_BEAMERROLL
        {
            get { return _BEAMERROLL; }
            set
            {
                if (_BEAMERROLL != value)
                {
                    _BEAMERROLL = value;
                }
            }
        }
        #endregion

        #region RowCount
        private int _RowCount = 0;
        public int RowCount
        {
            get { return _RowCount; }
            set
            {
                if (_RowCount != value)
                {
                    _RowCount = value;
                }
            }
        }
        #endregion

        #region RowLast
        private int _RowLast = 0;
        public int RowLast
        {
            get { return _RowLast; }
            set
            {
                if (_RowLast != value)
                {
                    _RowLast = value;
                }
            }
        }
        #endregion

        #region chkRowBreak
        private bool _chkRowBreak = true;
        public bool chkRowBreak
        {
            get { return _chkRowBreak; }
            set
            {
                if (_chkRowBreak != value)
                {
                    _chkRowBreak = value;
                }
            }
        }
        #endregion

        #region GRADE
        private string _GRADE = string.Empty;
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
        #endregion

        #region NetLenge
        private decimal? _NetLenge = null;
        public decimal? NetLenge
        {
            get { return _NetLenge; }
            set
            {
                if (_NetLenge != value)
                {
                    _NetLenge = value;
                }
            }
        }
        #endregion

        #region GrossLength
        private decimal? _GrossLength = null;
        public decimal? GrossLength
        {
            get { return _GrossLength; }
            set
            {
                if (_GrossLength != value)
                {
                    _GrossLength = value;
                }
            }
        }
        #endregion

        #region TOTALYARN
        private decimal? _TOTALYARN = null;
        public decimal? TOTALYARN
        {
            get { return _TOTALYARN; }
            set
            {
                if (_TOTALYARN != value)
                {
                    _TOTALYARN = value;
                }
            }
        }
        #endregion

        #region TOTALKEBA
        private decimal? _TOTALKEBA = null;
        public decimal? TOTALKEBA
        {
            get { return _TOTALKEBA; }
            set
            {
                if (_TOTALKEBA != value)
                {
                    _TOTALKEBA = value;
                }
            }
        }
        #endregion

        #region ADJUSTKEBA
        private decimal? _ADJUSTKEBA = null;
        public decimal? ADJUSTKEBA
        {
            get { return _ADJUSTKEBA; }
            set
            {
                if (_ADJUSTKEBA != value)
                {
                    _ADJUSTKEBA = value;
                }
            }
        }
        #endregion

        #region STARTDATE
        private DateTime? _STARTDATE = null;
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
        #endregion

        #region ENDDATE
        private DateTime? _ENDDATE = null;
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
        #endregion

        #region REMARK
        private string _REMARK = string.Empty;
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
        #endregion

        #region Printername
        private string _Printername = string.Empty;
        public string Printername
        {
            get { return _Printername; }
            set
            {
                if (_Printername != value)
                {
                    _Printername = value;
                }
            }
        }
        #endregion

        #region ScouringNo
        private string _scouringNo = string.Empty;
        public string ScouringNo
        {

            get { return _scouringNo; }
            set
            {
                if (_scouringNo != value)
                {
                    _scouringNo = value;
                }
            }
        }
        #endregion

        #region WIDTH
        private decimal? _WIDTH = null;
        public decimal? WIDTH
        {
            get { return _WIDTH; }
            set
            {
                if (_WIDTH != value)
                {
                    _WIDTH = value;
                }
            }
        }
        #endregion

        #region BEAMLENGTH
        private decimal? _BEAMLENGTH = null;
        public decimal? BEAMLENGTH
        {
            get { return _BEAMLENGTH; }
            set
            {
                if (_BEAMLENGTH != value)
                {
                    _BEAMLENGTH = value;
                }
            }
        }
        #endregion

        #region SPEED
        private decimal? _SPEED = null;
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
        #endregion

        #region TRACENO
        private string _TRACENO = string.Empty;
        public string TRACENO
        {
            get { return _TRACENO; }
            set
            {
                if (_TRACENO != value)
                {
                    _TRACENO = value;
                }
            }
        }
        #endregion

        #region LOTNO
        private string _LOTNO = string.Empty;
        public string LOTNO
        {
            get { return _LOTNO; }
            set
            {
                if (_LOTNO != value)
                {
                    _LOTNO = value;
                }
            }
        }
        #endregion

        #region ITM_YARN
        private string _ITM_YARN = string.Empty;
        public string ITM_YARN
        {
            get { return _ITM_YARN; }
            set
            {
                if (_ITM_YARN != value)
                {
                    _ITM_YARN = value;
                }
            }
        }
        #endregion

        #region CONECH
        private decimal? _CONECH = null;
        public decimal? CONECH
        {
            get { return _CONECH; }
            set
            {
                if (_CONECH != value)
                {
                    _CONECH = value;
                }
            }
        }
        #endregion

        #region RECEIVEDATE
        private DateTime? _RECEIVEDATE = null;
        public DateTime? RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set
            {
                if (_RECEIVEDATE != value)
                {
                    _RECEIVEDATE = value;
                }
            }
        }
        #endregion

        #region ENTRYDATE
        private DateTime? _ENTRYDATE = null;
        public DateTime? ENTRYDATE
        {
            get { return _ENTRYDATE; }
            set
            {
                if (_ENTRYDATE != value)
                {
                    _ENTRYDATE = value;
                }
            }
        }
        #endregion

        #region YARNTYPE
        private string _YARNTYPE = string.Empty;
        public string YARNTYPE
        {
            get { return _YARNTYPE; }
            set
            {
                if (_YARNTYPE != value)
                {
                    _YARNTYPE = value;
                }
            }
        }
        #endregion

        #region WEIGHTQTY
        private decimal? _WEIGHTQTY = null;
        public decimal? WEIGHTQTY
        {
            get { return _WEIGHTQTY; }
            set
            {
                if (_WEIGHTQTY != value)
                {
                    _WEIGHTQTY = value;
                }
            }
        }
        #endregion

        #region REQUESTNO

        private string _REQUESTNO = string.Empty;
        public string REQUESTNO
        {
            get { return _REQUESTNO; }
            set
            {
                if (_REQUESTNO != value)
                {
                    _REQUESTNO = value;
                }
            }
        }

        #endregion

        #region OperatorID
        private string _OperatorID = string.Empty;
        public string OperatorID
        {

            get { return _OperatorID; }
            set
            {
                if (_OperatorID != value)
                {
                    _OperatorID = value;
                }
            }
        }
        #endregion

        #region P_CUSID
        private string _CUSID = string.Empty;
        public string P_CUSID
        {
            get { return _CUSID; }
            set
            {
                if (_CUSID != value)
                {
                    _CUSID = value;
                }
            }
        }
        #endregion

        #region P_DATE
        private string _DATE = string.Empty;
        public string P_DATE
        {
            get { return _DATE; }
            set
            {
                if (_DATE != value)
                {
                    _DATE = value;
                }
            }
        }
        #endregion

        #region P_LABITMCODE
        private string _LABITMCODE = string.Empty;
        public string P_LABITMCODE
        {
            get { return _LABITMCODE; }
            set
            {
                if (_LABITMCODE != value)
                {
                    _LABITMCODE = value;
                }
            }
        }
        #endregion

        #region P_RESULT
        private string _RESULT = string.Empty;
        public string P_RESULT
        {
            get { return _RESULT; }
            set
            {
                if (_RESULT != value)
                {
                    _RESULT = value;
                }
            }
        }
        #endregion

        #region P_BEGINDATE
        private string _BEGINDATE = string.Empty;
        public string P_BEGINDATE
        {
            get { return _BEGINDATE; }
            set
            {
                if (_BEGINDATE != value)
                {
                    _BEGINDATE = value;
                }
            }
        }
        #endregion

        #region P_ENDDATE
        private string p_ENDDATE = string.Empty;
        public string P_ENDDATE
        {
            get { return p_ENDDATE; }
            set
            {
                if (p_ENDDATE != value)
                {
                    p_ENDDATE = value;
                }
            }
        }
        #endregion

        #region ENTRYSTARTDATE
        private string _ENTRYSTARTDATE = string.Empty;
        public string ENTRYSTARTDATE
        {

            get { return _ENTRYSTARTDATE; }
            set
            {
                if (_ENTRYSTARTDATE != value)
                {
                    _ENTRYSTARTDATE = value;
                }
            }
        }
        #endregion

        #region ENTRYENDDATE
        private string _ENTRYENDDATE = string.Empty;
        public string ENTRYENDDATE
        {

            get { return _ENTRYENDDATE; }
            set
            {
                if (_ENTRYENDDATE != value)
                {
                    _ENTRYENDDATE = value;
                }
            }
        }
        #endregion

        #region FINISHPROCESS
        private string _FINISHPROCESS = string.Empty;
        public string FINISHPROCESS
        {

            get { return _FINISHPROCESS; }
            set
            {
                if (_FINISHPROCESS != value)
                {
                    _FINISHPROCESS = value;
                }
            }
        }
        #endregion

        #region UseShiftRemark
        private bool _useShiftRemark = true;
        public bool UseShiftRemark
        {

            get { return _useShiftRemark; }
            set
            {
                if (_useShiftRemark != value)
                {
                    _useShiftRemark = value;
                }
            }
        }
        #endregion

        #region Reason
        private string _reason = string.Empty;
        public string Reason
        {

            get { return _reason; }
            set
            {
                if (_reason != value)
                {
                    _reason = value;
                }
            }
        }
        #endregion

        public event EventHandler OnContentChanged;
    }
}
