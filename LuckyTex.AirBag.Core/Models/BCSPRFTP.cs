#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLib;

using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using LuckyTex.Services;
using System.Windows.Media;

#endregion



namespace LuckyTex.Models
{
    #region BCSPRFTPResult

    /// <summary>
    /// BCSPRFTP
    /// </summary>
    public class BCSPRFTPResult
    {
        #region Public Properties

        public string ANNUL { get; set; }
        public string FLAGS { get; set; }
        public string RECTY { get; set; }
        public string CDSTO { get; set; }
        public string USRNM { get; set; }
        public int? DTTRA { get; set; }
        public int? DTINP { get; set; }
        public string CDEL0 { get; set; }
        public string CDCON { get; set; }
        public decimal? BLELE { get; set; }
        public string CDUM0 { get; set; }
        public string CDKE1 { get; set; }
        public string CDKE2 { get; set; }
        public string CDKE3 { get; set; }
        public string CDKE4 { get; set; }
        public string CDKE5 { get; set; }
        public string CDLOT { get; set; }
        public string CDTRA { get; set; }
        public string REFER { get; set; }
        public string LOCAT { get; set; }
        public string CDQUA { get; set; }
        public string QUACA { get; set; }
        public decimal? TECU1 { get; set; }
        public decimal? TECU2 { get; set; }
        public decimal? TECU3 { get; set; }
        public decimal? TECU4 { get; set; }
        public string TECU5 { get; set; }
        public string TECU6 { get; set; }
        public string COMM0 { get; set; }
        public Int64? DTORA { get; set; }

        #endregion
    }

    #endregion

    #region BCSPRFTP_D365Result

    /// <summary>
    /// BCSPRFTP_D365Result
    /// </summary>
    public class BCSPRFTP_D365Result
    {
        #region Public Properties
        public string IFNAME { get; set; }
        public string ANNUL { get; set; }
        public string FLAGS { get; set; }
        public string RECTY { get; set; }
        public string CDSTO { get; set; }
        public string USRNM { get; set; }
        public decimal? DTTRA { get; set; }
        public decimal? DTINP { get; set; }
        public string CDEL0 { get; set; }
        public string CDCON { get; set; }
        public decimal? BLELE { get; set; }
        public string CDUM0 { get; set; }
        public string CDKE1 { get; set; }
        public string CDKE2 { get; set; }
        public string CDKE3 { get; set; }
        public string CDKE4 { get; set; }
        public string CDKE5 { get; set; }
        public string CDLOT { get; set; }
        public string CDTRA { get; set; }
        public string REFER { get; set; }
        public string LOCAT { get; set; }
        public string CDQUA { get; set; }
        public string QUACA { get; set; }
        public decimal? TECU1 { get; set; }
        public decimal? TECU2 { get; set; }
        public decimal? TECU3 { get; set; }
        public decimal? TECU4 { get; set; }
        public string TECU5 { get; set; }
        public string TECU6 { get; set; }
        public string COMM0 { get; set; }
        public decimal? DTORA { get; set; }

        #endregion
    }

    #endregion

    #region FG_SEARCHDATASEND400

    public class FG_SEARCHDATASEND400
    {
        public System.Boolean SelectData { get; set; }
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

        // เพิ่มใหม่ 10/03/16
        public System.Decimal? GROSSLENGTH { get; set; }
        public System.Decimal? ORDERNO { get; set; }
        //-------------------------------------//

        public System.String RETEST { get; set; }
        public System.String PRODUCTTYPE { get; set; }
        public System.String ROLLNO { get; set; }
        public System.String CUSTOMERITEM { get; set; }

        // เพิ่มใหม่ 11/03/16
        public System.String FINISHFLAG { get; set; }
        //-------------------------------------//
    }

    #endregion

    #region AS400 Session

    /// <summary>
    /// AS400Session
    /// </summary>
    [Serializable]
    public class AS400Session
    {
        #region Internal Variables

        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AS400Session()
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

        #region Public Proeprties

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
}
