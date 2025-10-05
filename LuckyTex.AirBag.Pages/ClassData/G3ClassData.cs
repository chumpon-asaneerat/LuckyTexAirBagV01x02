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
    public class G3ClassData
    {
        #region G3 ClassData
        private static G3ClassData _instance = null;

        public static G3ClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(G3ClassData))
                    {
                        _instance = new G3ClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListG3_GOLABEL

        public class ListG3_GOLABEL
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListG3_GOLABEL()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListG3_GOLABEL()
            {
                // default constructor
            }

            public ListG3_GOLABEL(DateTime? _ENTRYDATE, string _ITM_YARN, string _PALLETNO, string _YARNTYPE, decimal? _WEIGHTQTY, decimal? _CONECH,
                string _VERIFY, decimal? _REMAINQTY, string _RECEIVEBY, DateTime? _RECEIVEDATE, string _FINISHFLAG, DateTime? _UPDATEDATE, string _PALLETTYPE,
                string _ITM400, string _UM, string _PACKAING, string _CLEAN, string _TEARING, string _FALLDOWN, string _CERTIFICATION, string _INVOICE,
                string _IDENTIFYAREA, string _AMOUNTPALLET, string _OTHER, string _ACTION, DateTime? _MOVEMENTDATE, string _LOTNO, string _TRACENO, string _From)
            {
                #region ListG3_GOLABEL

                ENTRYDATE = _ENTRYDATE;
                ITM_YARN = _ITM_YARN;
                PALLETNO = _PALLETNO;
                YARNTYPE = _YARNTYPE;
                WEIGHTQTY = _WEIGHTQTY;
                CONECH = _CONECH;
                VERIFY = _VERIFY;
                REMAINQTY = _REMAINQTY;
                RECEIVEBY = _RECEIVEBY;
                RECEIVEDATE = _RECEIVEDATE;
                FINISHFLAG = _FINISHFLAG;
                UPDATEDATE = _UPDATEDATE;
                PALLETTYPE = _PALLETTYPE;
                ITM400 = _ITM400;
                UM = _UM;
                PACKAING = _PACKAING;
                CLEAN = _CLEAN;
                TEARING = _TEARING;
                FALLDOWN = _FALLDOWN;
                CERTIFICATION = _CERTIFICATION;
                INVOICE = _INVOICE;
                IDENTIFYAREA = _IDENTIFYAREA;
                AMOUNTPALLET = _AMOUNTPALLET;
                OTHER = _OTHER;
                ACTION = _ACTION;
                MOVEMENTDATE = _MOVEMENTDATE;
                LOTNO = _LOTNO;
                TRACENO = _TRACENO;
                From = _From;

                #endregion
            }

            #region G3_GOLABEL

            public DateTime? ENTRYDATE { get; set; }
            public string ITM_YARN { get; set; }
            public string PALLETNO { get; set; }
            public string YARNTYPE { get; set; }
            public decimal? WEIGHTQTY { get; set; }
            public decimal? CONECH { get; set; }
            public string VERIFY { get; set; }
            public decimal? REMAINQTY { get; set; }
            public string RECEIVEBY { get; set; }
            public DateTime? RECEIVEDATE { get; set; }
            public string FINISHFLAG { get; set; }
            public DateTime? UPDATEDATE { get; set; }
            public string PALLETTYPE { get; set; }
            public string ITM400 { get; set; }
            public string UM { get; set; }
            public string PACKAING { get; set; }
            public string CLEAN { get; set; }
            public string TEARING { get; set; }
            public string FALLDOWN { get; set; }
            public string CERTIFICATION { get; set; }
            public string INVOICE { get; set; }
            public string IDENTIFYAREA { get; set; }
            public string AMOUNTPALLET { get; set; }
            public string OTHER { get; set; }
            public string ACTION { get; set; }
            public DateTime? MOVEMENTDATE { get; set; }
            public string LOTNO { get; set; }
            public string TRACENO { get; set; }

            //เพิ่ม 20/01/17
            public string From { get; set; }

            // For Barcode
            public byte[] TRACENOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.TRACENO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.TRACENO,
                            400, 60);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] PALLETNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.PALLETNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.PALLETNO,
                            400, 160);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            #endregion
        }
        #endregion

        #region ListG3_GETREQUESTNODETAIL

        public class ListG3_GETREQUESTNODETAIL
        {
            
            public ListG3_GETREQUESTNODETAIL()
            {
                // default constructor
            }

            public ListG3_GETREQUESTNODETAIL(DateTime? _ISSUEDATE,string _PALLETNO,
                string _TRACENO,decimal? _WEIGHT,decimal? _CH, string _ISSUEBY,
                string _ISSUETO,string _REQUESTNO,string _PALLETTYPE,string _ITM_YARN,
                string _LOTNO,string _YARNTYPE,string _ITM400,DateTime? _ENTRYDATE,
                string _PACKAING,string _CLEAN,string _FALLDOWN,string _TEARING,
                bool _ChkPACKAING,bool _ChkCLEAN,bool _ChkFALLDOWN,bool _ChkTEARING)
            {
                #region ListG3_GETREQUESTNODETAIL

                ISSUEDATE = _ISSUEDATE;
                PALLETNO = _PALLETNO;
                TRACENO = _TRACENO;
                WEIGHT = _WEIGHT;
                CH = _CH;
                ISSUEBY = _ISSUEBY;
                ISSUETO = _ISSUETO;
                REQUESTNO = _REQUESTNO;
                PALLETTYPE = _PALLETTYPE;
                ITM_YARN = _ITM_YARN;
                LOTNO = _LOTNO;
                YARNTYPE = _YARNTYPE;
                ITM400 = _ITM400;
                ENTRYDATE = _ENTRYDATE;
                PACKAING = _PACKAING;
                CLEAN = _CLEAN;
                FALLDOWN = _FALLDOWN;
                TEARING = _TEARING;

                ChkPACKAING = _ChkPACKAING;
                ChkCLEAN = _ChkCLEAN;
                ChkFALLDOWN = _ChkFALLDOWN;
                ChkTEARING = _ChkTEARING;

                #endregion
            }

            #region G3_GETREQUESTNODETAIL

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

            public System.Boolean ChkPACKAING { get; set; }
            public System.Boolean ChkCLEAN { get; set; }
            public System.Boolean ChkFALLDOWN { get; set; }
            public System.Boolean ChkTEARING { get; set; }

            #endregion
        }
        #endregion
    }
}
