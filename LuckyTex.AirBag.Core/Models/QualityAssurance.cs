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
    #region QA_SEARCHCHECKINGDATA 

    public class QA_SEARCHCHECKINGDATA
    {
        public System.Int32? RowNo { get; set; }
        public System.String CUSTOMERID { get; set; }

        public System.String LAB_ITMCODE { get; set; }
        public System.String LAB_LOT { get; set; }
        public System.String LAB_BATCHNO { get; set; }

        public System.String INS_ITMCODE { get; set; }
        public System.String INS_LOT { get; set; }
        public System.String INS_BATCHNO { get; set; }

        public System.String CUS_CODE { get; set; }
        public System.String CHECK_RESULT { get; set; }
        public System.DateTime? CHECKDATE { get; set; }
        public System.String CHECKEDBY { get; set; }
        public System.String CUSTOMERNAME { get; set; }

        public System.String DELETEFLAG { get; set; }
        public System.String DELETEBY { get; set; }
        public System.DateTime? DELETEDATE { get; set; }
        public System.String SHIFT { get; set; }
        public System.String REMARK { get; set; }
    }

    #endregion

    #region ITM_GETITEMBYITEMCODEANDCUSID

    public class ITM_GETITEMBYITEMCODEANDCUSIDDATA
    {
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
        public System.String DESCRIPTION { get; set; }
        public System.String SUPPLIERCODE { get; set; }
        public System.String WIDTH { get; set; }

        // เพิ่ม 17/10/22
        public System.String WIDTH_SELVAGEL { get; set; }
        public System.String WIDTH_SELVAGER { get; set; }
        public System.Decimal? RESETSTARTLENGTH { get; set; }
    }

    #endregion

    #region QualityAssurance Session

    /// <summary>
    /// Finishing Session.
    /// </summary>
    [Serializable]
    public class QualityAssuranceSession
    {
        #region Internal Variables

        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public QualityAssuranceSession()
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

        #region GetCustomerList
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CUS_GETLISTData> GetCustomerList()
        {
            List<CUS_GETLISTData> results = QualityAssuranceService.Instance
                .GetCUS_GETLISTDataList();

            return results;
        }
        #endregion

        #region GetItemCodeData
        public List<ITM_GETITEMCODELIST> GetItemCodeData()
        {
            List<ITM_GETITEMCODELIST> results = QualityAssuranceService.Instance
                .ITM_GETITEMCODELIST();

            return results;
        }
        #endregion

        #endregion

        #region Public Proeprties

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
