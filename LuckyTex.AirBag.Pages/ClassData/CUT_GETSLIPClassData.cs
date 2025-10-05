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
    public class CUT_GETSLIPClassData
    {
        #region CUT_GETSLIP ClassData
        private static CUT_GETSLIPClassData _instance = null;

        public static CUT_GETSLIPClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(CUT_GETSLIPClassData))
                    {
                        _instance = new CUT_GETSLIPClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListCUT_GETSLIP

        public class ListCUT_GETSLIP
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListCUT_GETSLIP()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListCUT_GETSLIP()
            {
                // default constructor
            }

            public ListCUT_GETSLIP(string _ITEMLOT, DateTime? _STARTDATE, DateTime? _ENDDATE,
                decimal? _WIDTHBARCODE1, decimal? _WIDTHBARCODE2, decimal? _WIDTHBARCODE3, decimal? _WIDTHBARCODE4,
                decimal? _DISTANTBARCODE1, decimal? _DISTANTBARCODE2, decimal? _DISTANTBARCODE3, decimal? _DISTANTBARCODE4,
                decimal? _DISTANTLINE1, decimal? _DISTANTLINE2, decimal? _DISTANTLINE3,
                decimal? _DENSITYWARP, decimal? _DENSITYWEFT, decimal? _SPEED, decimal? _BEFORE_WIDTH, decimal? _AFTER_WIDTH,
                string _BEGINROLL_LINE1, string _BEGINROLL_LINE2, string _BEGINROLL_LINE3, string _BEGINROLL_LINE4,
                string _ENDROLL_LINE1, string _ENDROLL_LINE2, string _ENDROLL_LINE3, string _ENDROLL_LINE4, string _OPERATORID, string _SELVAGE_LEFT, string _SELVAGE_RIGHT,
                string _REMARK, string _PRODUCTTYPEID, string _MCNO, string _FINISHLOT, string _ITEMCODE, string _ITEMLOT1, string _PARTNO, string _MCNAME,string _FINISHINGPROCESS,
                string _BEGINROLL2_LINE1, string _BEGINROLL2_LINE2, string _BEGINROLL2_LINE3, string _BEGINROLL2_LINE4,
                string _ENDROLL2_LINE1, string _ENDROLL2_LINE2, string _ENDROLL2_LINE3, string _ENDROLL2_LINE4, string _SND_BARCODE, string _CUSTOMERID, string _PARTNO2, decimal? _TENSION, string _FINISHLENGTH, string _LENGTHDETAIL, decimal? _AFTER_WIDTH_END)
            {
                #region ListCUT_GETSLIP

                ITEMLOT = _ITEMLOT;
                STARTDATE = _STARTDATE;
                ENDDATE = _ENDDATE;
                WIDTHBARCODE1 = _WIDTHBARCODE1;
                WIDTHBARCODE2 = _WIDTHBARCODE2;
                WIDTHBARCODE3 = _WIDTHBARCODE3;
                WIDTHBARCODE4 = _WIDTHBARCODE4;
                DISTANTBARCODE1 = _DISTANTBARCODE1;
                DISTANTBARCODE2 = _DISTANTBARCODE2;
                DISTANTBARCODE3 = _DISTANTBARCODE3;
                DISTANTBARCODE4 = _DISTANTBARCODE4;
                DISTANTLINE1 = _DISTANTLINE1;
                DISTANTLINE2 = _DISTANTLINE2;
                DISTANTLINE3 = _DISTANTLINE3;
                DENSITYWARP = _DENSITYWARP;
                DENSITYWEFT = _DENSITYWEFT;
                SPEED = _SPEED;
                BEFORE_WIDTH = _BEFORE_WIDTH;
                AFTER_WIDTH = _AFTER_WIDTH;
                BEGINROLL_LINE1 = _BEGINROLL_LINE1;
                BEGINROLL_LINE2 = _BEGINROLL_LINE2;
                BEGINROLL_LINE3 = _BEGINROLL_LINE3;
                BEGINROLL_LINE4 = _BEGINROLL_LINE4;
                ENDROLL_LINE1 = _ENDROLL_LINE1;
                ENDROLL_LINE2 = _ENDROLL_LINE2;
                ENDROLL_LINE3 = _ENDROLL_LINE3;
                ENDROLL_LINE4 = _ENDROLL_LINE4;
                OPERATORID = _OPERATORID;
                SELVAGE_LEFT = _SELVAGE_LEFT;
                SELVAGE_RIGHT = _SELVAGE_RIGHT;
                REMARK = _REMARK;
                PRODUCTTYPEID = _PRODUCTTYPEID;
                MCNO = _MCNO;
                FINISHLOT = _FINISHLOT;
                ITEMCODE = _ITEMCODE;
                ITEMLOT1 = _ITEMLOT1;
                PARTNO = _PARTNO;
                MCNAME = _MCNAME;

                //เพิ่มใหม่ 20/01/16
                FINISHINGPROCESS = _FINISHINGPROCESS;

                BEGINROLL2_LINE1 = _BEGINROLL2_LINE1;
                BEGINROLL2_LINE2 = _BEGINROLL2_LINE2;
                BEGINROLL2_LINE3 = _BEGINROLL2_LINE3;
                BEGINROLL2_LINE4 = _BEGINROLL2_LINE4;
                ENDROLL2_LINE1 = _ENDROLL2_LINE1;
                ENDROLL2_LINE2 = _ENDROLL2_LINE2;
                ENDROLL2_LINE3 = _ENDROLL2_LINE3;
                ENDROLL2_LINE4 = _ENDROLL2_LINE4;
                SND_BARCODE = _SND_BARCODE;

                //เพิ่มใหม่ 10/12/16
                CUSTOMERID = _CUSTOMERID;
                PARTNO2 = _PARTNO2;

                //เพิ่ม 28/06/17
                TENSION = _TENSION;

                //เพิ่มใหม่ 13/07/17
                FINISHLENGTH = _FINISHLENGTH;

                //เพิ่มใหม่ 28/09/17
                LENGTHDETAIL = _LENGTHDETAIL;

                //เพิ่มใหม่ 04/10/17
                AFTER_WIDTH_END = _AFTER_WIDTH_END;

                #endregion
            }

            #region CUT_GETSLIP 

            public string ITEMLOT { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public decimal? WIDTHBARCODE1 { get; set; }
            public decimal? WIDTHBARCODE2 { get; set; }
            public decimal? WIDTHBARCODE3 { get; set; }
            public decimal? WIDTHBARCODE4 { get; set; }
            public decimal? DISTANTBARCODE1 { get; set; }
            public decimal? DISTANTBARCODE2 { get; set; }
            public decimal? DISTANTBARCODE3 { get; set; }
            public decimal? DISTANTBARCODE4 { get; set; }
            public decimal? DISTANTLINE1 { get; set; }
            public decimal? DISTANTLINE2 { get; set; }
            public decimal? DISTANTLINE3 { get; set; }
            public decimal? DENSITYWARP { get; set; }
            public decimal? DENSITYWEFT { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? BEFORE_WIDTH { get; set; }
            public decimal? AFTER_WIDTH { get; set; }
            public string BEGINROLL_LINE1 { get; set; }
            public string BEGINROLL_LINE2 { get; set; }
            public string BEGINROLL_LINE3 { get; set; }
            public string BEGINROLL_LINE4 { get; set; }
            public string ENDROLL_LINE1 { get; set; }
            public string ENDROLL_LINE2 { get; set; }
            public string ENDROLL_LINE3 { get; set; }
            public string ENDROLL_LINE4 { get; set; }
            public string OPERATORID { get; set; }
            public string SELVAGE_LEFT { get; set; }
            public string SELVAGE_RIGHT { get; set; }
            public string REMARK { get; set; }
            public string PRODUCTTYPEID { get; set; }
            public string MCNO { get; set; }
            public string FINISHLOT { get; set; }
            public string ITEMCODE { get; set; }
            public string ITEMLOT1 { get; set; }
            public string PARTNO { get; set; }
            public string MCNAME { get; set; }

            //เพิ่มใหม่ 20/01/16
            public string FINISHINGPROCESS { get; set; }

            //เพิ่มใหม่ 03/05/16
            public string STATUS { get; set; }
            public DateTime? SUSPENDDATE { get; set; }
            public string SUSPENDBY { get; set; }
            public DateTime? CLEARDATE { get; set; }
            public string CLEARREMARK { get; set; }
            public string CLEARBY { get; set; }
            public decimal? LENGTHPRINT { get; set; }
            public DateTime? SUSPENDSTARTDATE { get; set; }

            public string SHOWSELVAGE { get; set; }

            //เพิ่มใหม่ 27/06/16
            public string BEGINROLL2_LINE1 { get; set; }
            public string BEGINROLL2_LINE2 { get; set; }
            public string BEGINROLL2_LINE3 { get; set; }
            public string BEGINROLL2_LINE4 { get; set; }
            public string ENDROLL2_LINE1 { get; set; }
            public string ENDROLL2_LINE2 { get; set; }
            public string ENDROLL2_LINE3 { get; set; }
            public string ENDROLL2_LINE4 { get; set; }
            public string SND_BARCODE { get; set; }

            //เพิ่มใหม่ 10/12/16
            public string CUSTOMERID { get; set; }
            public string PARTNO2 { get; set; }

            //เพิ่ม 28/06/17
            public decimal? TENSION { get; set; }

            //เพิ่มใหม่ 13/07/17
            public string FINISHLENGTH { get; set; }

            //เพิ่มใหม่ 28/09/17
            public string LENGTHDETAIL { get; set; }

            //เพิ่มใหม่ 04/10/17
            public decimal? AFTER_WIDTH_END { get; set; }

            // For Barcode
            public byte[] ITEMCODEImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.ITEMCODE))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.ITEMCODE,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] ITEMLOTImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.ITEMLOT))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.ITEMLOT,
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

        #region ListCUT_GETCONDITIONBYITEMCODE

        public class ListCUT_GETCONDITIONBYITEMCODE
        {
            public ListCUT_GETCONDITIONBYITEMCODE()
            {
                // default constructor
            }

            public ListCUT_GETCONDITIONBYITEMCODE(string _ITM_CODE, decimal? _WIDTHBARCODE_MIN, decimal? _WIDTHBARCODE_MAX,
                        decimal? _DISTANTBARCODE_MIN, decimal? _DISTANTBARCODE_MAX, decimal? _DISTANTLINE_MIN, decimal? _DISTANTLINE_MAX,
                        decimal? _DENSITYWARP_MIN, decimal? _DENSITYWARP_MAX, decimal? _DENSITYWEFT_MIN, decimal? _DENSITYWEFT_MAX,
                        decimal? _SPEED, decimal? _SPEED_MARGIN, decimal? _AFTER_WIDTH,
                        string _strWIDTHBARCODE, string _strDISTANTBARCODE, string _strDISTANTLINE, string _strDENSITYWARP,
                        string _strDENSITYWEFT, string _strSPEED, string _strAFTER)
            {
                #region ListCUT_GETCONDITIONBYITEMCODE

                ITM_CODE = _ITM_CODE;
                WIDTHBARCODE_MIN = _WIDTHBARCODE_MIN;
                WIDTHBARCODE_MAX = _WIDTHBARCODE_MAX;
                DISTANTBARCODE_MIN = _DISTANTBARCODE_MIN;
                DISTANTBARCODE_MAX = _DISTANTBARCODE_MAX;
                DISTANTLINE_MIN = _DISTANTLINE_MIN;
                DISTANTLINE_MAX = _DISTANTLINE_MAX;
                DENSITYWARP_MIN = _DENSITYWARP_MIN;
                DENSITYWARP_MAX = _DENSITYWARP_MAX;
                DENSITYWEFT_MIN = _DENSITYWEFT_MIN;
                DENSITYWEFT_MAX = _DENSITYWEFT_MAX;
                SPEED = _SPEED;
                SPEED_MARGIN = _SPEED_MARGIN;
                AFTER_WIDTH = _AFTER_WIDTH;


                strWIDTHBARCODE = _strWIDTHBARCODE;
                strDISTANTBARCODE = _strDISTANTBARCODE;
                strDISTANTLINE = _strDISTANTLINE;
                strDENSITYWARP = _strDENSITYWARP;
                strDENSITYWEFT = _strDENSITYWEFT;
                strSPEED = _strSPEED;
                strAFTER = _strAFTER;

                #endregion
            }

            #region CUT_GETCONDITIONBYITEMCODE

            public string ITM_CODE { get; set; }
            public decimal? WIDTHBARCODE_MIN { get; set; }
            public decimal? WIDTHBARCODE_MAX { get; set; }
            public decimal? DISTANTBARCODE_MIN { get; set; }
            public decimal? DISTANTBARCODE_MAX { get; set; }
            public decimal? DISTANTLINE_MIN { get; set; }
            public decimal? DISTANTLINE_MAX { get; set; }
            public decimal? DENSITYWARP_MIN { get; set; }
            public decimal? DENSITYWARP_MAX { get; set; }
            public decimal? DENSITYWEFT_MIN { get; set; }
            public decimal? DENSITYWEFT_MAX { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? SPEED_MARGIN { get; set; }
            public decimal? AFTER_WIDTH { get; set; }


            public string strWIDTHBARCODE { get; set; }
            public string strDISTANTBARCODE { get; set; }
            public string strDISTANTLINE { get; set; }
            public string strDENSITYWARP { get; set; }
            public string strDENSITYWEFT { get; set; }
            public string strSPEED { get; set; }
            public string strAFTER { get; set; }


            #endregion
        }
        #endregion

        #region ListCUT_SERACHLIST

        public class ListCUT_SERACHLIST
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListCUT_SERACHLIST()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListCUT_SERACHLIST()
            {
                // default constructor
            }

            public ListCUT_SERACHLIST(int? _No,string _ITEMLOT, DateTime? _STARTDATE, DateTime? _ENDDATE,
                decimal? _WIDTHBARCODE1, decimal? _WIDTHBARCODE2, decimal? _WIDTHBARCODE3, decimal? _WIDTHBARCODE4,
                decimal? _DISTANTBARCODE1, decimal? _DISTANTBARCODE2, decimal? _DISTANTBARCODE3, decimal? _DISTANTBARCODE4,
                decimal? _DISTANTLINE1, decimal? _DISTANTLINE2, decimal? _DISTANTLINE3,
                decimal? _DENSITYWARP, decimal? _DENSITYWEFT, decimal? _SPEED, decimal? _BEFORE_WIDTH, decimal? _AFTER_WIDTH,
                string _BEGINROLL_LINE1, string _BEGINROLL_LINE2, string _BEGINROLL_LINE3, string _BEGINROLL_LINE4,
                string _ENDROLL_LINE1, string _ENDROLL_LINE2, string _ENDROLL_LINE3, string _ENDROLL_LINE4, string _OPERATORID, string _SELVAGE_LEFT, string _SELVAGE_RIGHT,
                string _REMARK, string _PRODUCTTYPEID, string _MCNO, string _ITEMCODE, string _PARTNO, string _MCNAME,
                string _BEGINROLL2_LINE1, string _BEGINROLL2_LINE2, string _BEGINROLL2_LINE3, string _BEGINROLL2_LINE4,
                string _ENDROLL2_LINE1, string _ENDROLL2_LINE2, string _ENDROLL2_LINE3, string _ENDROLL2_LINE4, string _SND_BARCODE, decimal? _TENSION, string _FINISHLOT, decimal? _FINISHLENGTH,
                string _STATUS, DateTime? _SUSPENDDATE, string _SUSPENDBY, DateTime? _CLEARDATE, string _CLEARREMARK, string _CLEARBY, decimal? _LENGTHPRINT, DateTime? _SUSPENDSTARTDATE,
                string _LENGTHDETAIL, string _FINISHLENGTH1)
            {
                #region ListCUT_SERACHLIST

                No = _No;
                ITEMLOT = _ITEMLOT;
                STARTDATE = _STARTDATE;
                ENDDATE = _ENDDATE;
                WIDTHBARCODE1 = _WIDTHBARCODE1;
                WIDTHBARCODE2 = _WIDTHBARCODE2;
                WIDTHBARCODE3 = _WIDTHBARCODE3;
                WIDTHBARCODE4 = _WIDTHBARCODE4;
                DISTANTBARCODE1 = _DISTANTBARCODE1;
                DISTANTBARCODE2 = _DISTANTBARCODE2;
                DISTANTBARCODE3 = _DISTANTBARCODE3;
                DISTANTBARCODE4 = _DISTANTBARCODE4;
                DISTANTLINE1 = _DISTANTLINE1;
                DISTANTLINE2 = _DISTANTLINE2;
                DISTANTLINE3 = _DISTANTLINE3;
                DENSITYWARP = _DENSITYWARP;
                DENSITYWEFT = _DENSITYWEFT;
                SPEED = _SPEED;
                BEFORE_WIDTH = _BEFORE_WIDTH;
                AFTER_WIDTH = _AFTER_WIDTH;
                BEGINROLL_LINE1 = _BEGINROLL_LINE1;
                BEGINROLL_LINE2 = _BEGINROLL_LINE2;
                BEGINROLL_LINE3 = _BEGINROLL_LINE3;
                BEGINROLL_LINE4 = _BEGINROLL_LINE4;
                ENDROLL_LINE1 = _ENDROLL_LINE1;
                ENDROLL_LINE2 = _ENDROLL_LINE2;
                ENDROLL_LINE3 = _ENDROLL_LINE3;
                ENDROLL_LINE4 = _ENDROLL_LINE4;
                OPERATORID = _OPERATORID;
                SELVAGE_LEFT = _SELVAGE_LEFT;
                SELVAGE_RIGHT = _SELVAGE_RIGHT;
                REMARK = _REMARK;
                PRODUCTTYPEID = _PRODUCTTYPEID;
                MCNO = _MCNO;
                ITEMCODE = _ITEMCODE;
                PARTNO = _PARTNO;
                MCNAME = _MCNAME;
                
                BEGINROLL2_LINE1 = _BEGINROLL2_LINE1;
                BEGINROLL2_LINE2 = _BEGINROLL2_LINE2;
                BEGINROLL2_LINE3 = _BEGINROLL2_LINE3;
                BEGINROLL2_LINE4 = _BEGINROLL2_LINE4;
                ENDROLL2_LINE1 = _ENDROLL2_LINE1;
                ENDROLL2_LINE2 = _ENDROLL2_LINE2;
                ENDROLL2_LINE3 = _ENDROLL2_LINE3;
                ENDROLL2_LINE4 = _ENDROLL2_LINE4;
                SND_BARCODE = _SND_BARCODE;
                TENSION = _TENSION;
                FINISHLOT = _FINISHLOT;
                FINISHLENGTH = _FINISHLENGTH;
                STATUS = _STATUS;
                SUSPENDDATE = _SUSPENDDATE;
                SUSPENDBY = _SUSPENDBY;
                CLEARDATE = _CLEARDATE;
                CLEARREMARK = _CLEARREMARK;
                CLEARBY = _CLEARBY;
                LENGTHPRINT = _LENGTHPRINT;
                SUSPENDSTARTDATE = _SUSPENDSTARTDATE;

                LENGTHDETAIL = _LENGTHDETAIL;
                FINISHLENGTH1 = _FINISHLENGTH1;

                #endregion
            }

            #region CUT_SERACHLIST

            public System.Int32? No { get; set; }
            public System.String ITEMLOT { get; set; }
            public System.DateTime? STARTDATE { get; set; }
            public System.DateTime? ENDDATE { get; set; }
            public System.Decimal? WIDTHBARCODE1 { get; set; }
            public System.Decimal? WIDTHBARCODE2 { get; set; }
            public System.Decimal? WIDTHBARCODE3 { get; set; }
            public System.Decimal? WIDTHBARCODE4 { get; set; }
            public System.Decimal? DISTANTBARCODE1 { get; set; }
            public System.Decimal? DISTANTBARCODE2 { get; set; }
            public System.Decimal? DISTANTBARCODE3 { get; set; }
            public System.Decimal? DISTANTBARCODE4 { get; set; }
            public System.Decimal? DISTANTLINE1 { get; set; }
            public System.Decimal? DISTANTLINE2 { get; set; }
            public System.Decimal? DISTANTLINE3 { get; set; }
            public System.Decimal? DENSITYWARP { get; set; }
            public System.Decimal? DENSITYWEFT { get; set; }
            public System.Decimal? SPEED { get; set; }
            public System.Decimal? BEFORE_WIDTH { get; set; }
            public System.Decimal? AFTER_WIDTH { get; set; }
            public System.String BEGINROLL_LINE1 { get; set; }
            public System.String BEGINROLL_LINE2 { get; set; }
            public System.String BEGINROLL_LINE3 { get; set; }
            public System.String BEGINROLL_LINE4 { get; set; }
            public System.String ENDROLL_LINE1 { get; set; }
            public System.String ENDROLL_LINE2 { get; set; }
            public System.String ENDROLL_LINE3 { get; set; }
            public System.String ENDROLL_LINE4 { get; set; }
            public System.String OPERATORID { get; set; }
            public System.String SELVAGE_LEFT { get; set; }
            public System.String SELVAGE_RIGHT { get; set; }
            public System.String REMARK { get; set; }
            public System.String PRODUCTTYPEID { get; set; }
            public System.String MCNO { get; set; }
            public System.String ITEMCODE { get; set; }
            public System.String PARTNO { get; set; }
            public System.String MCNAME { get; set; }

            //เพิ่มใหม่ 27/06/16
            public System.String BEGINROLL2_LINE1 { get; set; }
            public System.String BEGINROLL2_LINE2 { get; set; }
            public System.String BEGINROLL2_LINE3 { get; set; }
            public System.String BEGINROLL2_LINE4 { get; set; }
            public System.String ENDROLL2_LINE1 { get; set; }
            public System.String ENDROLL2_LINE2 { get; set; }
            public System.String ENDROLL2_LINE3 { get; set; }
            public System.String ENDROLL2_LINE4 { get; set; }

            public System.String SND_BARCODE { get; set; }

            //เพิ่มใหม่ 28/06/17
            public System.Decimal? TENSION { get; set; }
            public System.String FINISHLOT { get; set; }
            public System.Decimal? FINISHLENGTH { get; set; }

            public System.String STATUS { get; set; }
            public System.DateTime? SUSPENDDATE { get; set; }
            public System.String SUSPENDBY { get; set; }
            public System.DateTime? CLEARDATE { get; set; }
            public System.String CLEARREMARK { get; set; }
            public System.String CLEARBY { get; set; }
            public System.Decimal? LENGTHPRINT { get; set; }
            public System.DateTime? SUSPENDSTARTDATE { get; set; }

            //เพิ่มใหม่ 25/08/17
            public System.String LENGTHDETAIL { get; set; }
            public System.String FINISHLENGTH1 { get; set; }

            #endregion
        }
        #endregion
    }
}
