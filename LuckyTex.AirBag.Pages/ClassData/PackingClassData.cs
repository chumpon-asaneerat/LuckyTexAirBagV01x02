#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using DataMatrix.Net2D;

#endregion

namespace DataControl.ClassData
{
    public class PackingClassData
    {
        #region Packing ClassData
        private static PackingClassData _instance = null;

        public static PackingClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(PackingClassData))
                    {
                        _instance = new PackingClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListPack_PalletSheet

        public class ListPack_PalletSheet
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListPack_PalletSheet()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListPack_PalletSheet()
            {
                // default constructor
            }

            public ListPack_PalletSheet(int rowNo , string _PALLETNO, string _ITEMCODE, string _CUSTOMERTYPE, string _INSPECTIONLOT, string _GRADE
                , decimal? _GROSSWEIGHT, decimal? _NETLENGTH, decimal? _NETWEIGHT, DateTime? _PACKINGDATE, string _PACKINGBY, string _CHECKBY, DateTime? _CHECKINGDATE, string _LOADINGTYPE
                , string _ITM_WEAVING, string _YARNCODE)
            {
                #region ListPacking

                RowNo = rowNo;
                PALLETNO = _PALLETNO;
                ITEMCODE = _ITEMCODE;
                CUSTOMERTYPE = _CUSTOMERTYPE;
                INSPECTIONLOT = _INSPECTIONLOT;
                GRADE = _GRADE;
                GROSSWEIGHT = _GROSSWEIGHT;
                NETLENGTH = _NETLENGTH;
                NETWEIGHT = _NETWEIGHT;
                PACKINGDATE = _PACKINGDATE;
                PACKINGBY = _PACKINGBY;
                CHECKBY = _CHECKBY;
                CHECKINGDATE = _CHECKINGDATE;
                LOADINGTYPE = _LOADINGTYPE;
                ITM_WEAVING = _ITM_WEAVING;
                YARNCODE = _YARNCODE;

                #endregion
            }

            #region Pack_PalletSheet 

            public int RowNo { get; set; }
            public string PALLETNO { get; set; }
            public string ITEMCODE { get; set; }
            public string CUSTOMERTYPE { get; set; }
            public string INSPECTIONLOT { get; set; }
            public string GRADE { get; set; }
            public decimal? GROSSWEIGHT { get; set; }
            public decimal? NETLENGTH { get; set; }
            public decimal? NETWEIGHT { get; set; }
            public DateTime? PACKINGDATE { get; set; }
            public string PACKINGBY { get; set; }
            public string CHECKBY { get; set; }
            public DateTime? CHECKINGDATE { get; set; }
            public string LOADINGTYPE { get; set; }
            public string ITM_WEAVING { get; set; }
            public string YARNCODE { get; set; }

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

            public byte[] PALLETNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.PALLETNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.PALLETNO,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }
            #endregion
        }
        #endregion

        #region ListPACK_PRINTLABEL

        public class ListPACK_PRINTLABEL
        {
             private static BarcodeLib.Barcode BarcodeGenerator = null;

             static ListPACK_PRINTLABEL()
             {
                 BarcodeGenerator = new BarcodeLib.Barcode();
                 BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE128;
                 BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                 BarcodeGenerator.IncludeLabel = false;
                 //BarcodeGenerator.Height = 100;
                 //BarcodeGenerator.Width = 500;
             }

            public ListPACK_PRINTLABEL()
            {
                // default constructor
            }

            public ListPACK_PRINTLABEL(string _INSPECTIONLOT, string _INSPECTIONLOT09, decimal? _QUANTITY, decimal? _GROSSWEIGHT, decimal? _NETWEIGHT, string _GRADE
                , string _ITEMCODE, string _DESCRIPTION, string _SUPPLIERCODE, string _SUPPLIERCODE09, string _BARCODEBACTHNO,string _BARCODEBACTHNO09
                , string _CUSTOMERPARTNO, string _CUSTOMERPARTNO09, string _BATCHNO, string _BATCHNO09, string _PDATE
                , string _CUSTOMERID, string _BarcodeCMPARTNO, string _StrQuantity, string _StrQuantity09, string _FINISHINGPROCESS)
            {
                #region ListPacking

                INSPECTIONLOT = _INSPECTIONLOT;
                INSPECTIONLOT09 = _INSPECTIONLOT09;

                QUANTITY = _QUANTITY;
                GROSSWEIGHT = _GROSSWEIGHT;
                NETWEIGHT = _NETWEIGHT;
                GRADE = _GRADE;
                ITEMCODE = _ITEMCODE;
                DESCRIPTION = _DESCRIPTION;

                SUPPLIERCODE = _SUPPLIERCODE;
                SUPPLIERCODE09 = _SUPPLIERCODE09;

                BARCODEBACTHNO = _BARCODEBACTHNO;
                BARCODEBACTHNO09 = _BARCODEBACTHNO09;

                CUSTOMERPARTNO = _CUSTOMERPARTNO;
                CUSTOMERPARTNO09 = _CUSTOMERPARTNO09;

                BATCHNO = _BATCHNO;
                BATCHNO09 = _BATCHNO09;

                PDATE = _PDATE;
                CUSTOMERID = _CUSTOMERID;
                BarcodeCMPARTNO = _BarcodeCMPARTNO;

                StrQuantity = _StrQuantity;
                StrQuantity09 = _StrQuantity09;

                //INC เพิ่มเอง
                FINISHINGPROCESS = _FINISHINGPROCESS;

                #endregion
            }

            #region PACK_PRINTLABEL

            public string INSPECTIONLOT { get; set; }
            public string INSPECTIONLOT09 { get; set; }

            public decimal? QUANTITY { get; set; }
            public decimal? GROSSWEIGHT { get; set; }
            public decimal? NETWEIGHT { get; set; }
            public string GRADE { get; set; }
            public string ITEMCODE { get; set; }
            public string DESCRIPTION { get; set; }

            public string SUPPLIERCODE { get; set; }
            public string SUPPLIERCODE09 { get; set; }

            public string BARCODEBACTHNO { get; set; }
            public string BARCODEBACTHNO09 { get; set; }

            public string CUSTOMERPARTNO { get; set; }
            public string CUSTOMERPARTNO09 { get; set; }

            public string BATCHNO { get; set; }
            public string BATCHNO09 { get; set; }

            public string PDATE { get; set; }

            public string CUSTOMERID { get; set; }
            public string BarcodeCMPARTNO { get; set; }

            public string StrQuantity { get; set; }
            public string StrQuantity09 { get; set; }

            //INC เพิ่มเอง
            public string FINISHINGPROCESS { get; set; }

            // For Barcode
            public byte[] INSPECTIONLOTImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.INSPECTIONLOT))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.INSPECTIONLOT,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] BARCODEBACTHNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.BARCODEBACTHNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.BARCODEBACTHNO,
                            700, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] SUPPLIERCODEImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.SUPPLIERCODE))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.SUPPLIERCODE,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] CUSTOMERPARTNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.BarcodeCMPARTNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.BarcodeCMPARTNO,
                            700, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] QUANTITYImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.QUANTITY.Value.ToString()))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.QUANTITY.Value.ToString(),
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] StrQuantityImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.StrQuantity))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.StrQuantity,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            #endregion
        }
        #endregion

        #region ListPACK_PRINTLABEL2D

        public class ListPACK_PRINTLABEL2D
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListPACK_PRINTLABEL2D()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE128;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListPACK_PRINTLABEL2D()
            {
                // default constructor
            }

            public ListPACK_PRINTLABEL2D(string _INSPECTIONLOT, string _INSPECTIONLOT09, decimal? _QUANTITY, decimal? _GROSSWEIGHT, decimal? _NETWEIGHT, string _GRADE
                , string _ITEMCODE, string _DESCRIPTION, string _SUPPLIERCODE, string _SUPPLIERCODE09, string _BARCODEBACTHNO, string _BARCODEBACTHNO09, string _CUSTOMERPARTNO
                , string _CUSTOMERPARTNO09, string _BATCHNO, string _BATCHNO09, string _PDATE, string _CUSTOMERID, string _BarcodeCMPARTNO, string _StrQuantity, string _StrQuantity09
                , string _FINISHINGPROCESS, string _DBARCODE, string _BDate, string _CUSPARTNO2D)
            {
                #region ListPacking

                INSPECTIONLOT = _INSPECTIONLOT;
                INSPECTIONLOT09 = _INSPECTIONLOT09;

                QUANTITY = _QUANTITY;
                GROSSWEIGHT = _GROSSWEIGHT;
                NETWEIGHT = _NETWEIGHT;
                GRADE = _GRADE;
                ITEMCODE = _ITEMCODE;
                DESCRIPTION = _DESCRIPTION;

                SUPPLIERCODE = _SUPPLIERCODE;
                SUPPLIERCODE09 = _SUPPLIERCODE09;

                BARCODEBACTHNO = _BARCODEBACTHNO;
                BARCODEBACTHNO09 = _BARCODEBACTHNO09;

                CUSTOMERPARTNO = _CUSTOMERPARTNO;
                CUSTOMERPARTNO09 = _CUSTOMERPARTNO09;

                BATCHNO = _BATCHNO;
                BATCHNO09 = _BATCHNO09;

                PDATE = _PDATE;
                CUSTOMERID = _CUSTOMERID;
                BarcodeCMPARTNO = _BarcodeCMPARTNO;

                StrQuantity = _StrQuantity;
                StrQuantity09 = _StrQuantity09;

                //INC เพิ่มเอง
                FINISHINGPROCESS = _FINISHINGPROCESS;

                DBARCODE = _DBARCODE;
                BDate = _BDate;
                CUSPARTNO2D = _CUSPARTNO2D;

                #endregion
            }

            #region PACK_PRINTLABEL

            public string INSPECTIONLOT { get; set; }
            public string INSPECTIONLOT09 { get; set; }

            public decimal? QUANTITY { get; set; }
            public decimal? GROSSWEIGHT { get; set; }
            public decimal? NETWEIGHT { get; set; }
            public string GRADE { get; set; }
            public string ITEMCODE { get; set; }
            public string DESCRIPTION { get; set; }

            public string SUPPLIERCODE { get; set; }
            public string SUPPLIERCODE09 { get; set; }

            public string BARCODEBACTHNO { get; set; }
            public string BARCODEBACTHNO09 { get; set; }

            public string CUSTOMERPARTNO { get; set; }
            public string CUSTOMERPARTNO09 { get; set; }

            public string BATCHNO { get; set; }
            public string BATCHNO09 { get; set; }

            public string PDATE { get; set; }

            public string CUSTOMERID { get; set; }
            public string BarcodeCMPARTNO { get; set; }

            public string StrQuantity { get; set; }
            public string StrQuantity09 { get; set; }

            public string DBARCODE { get; set; }
            public string BDate { get; set; }
            public string CUSPARTNO2D { get; set; }
            //INC เพิ่มเอง
            public string FINISHINGPROCESS { get; set; }

            // For Barcode
            public byte[] INSPECTIONLOTImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.INSPECTIONLOT))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.INSPECTIONLOT,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] BARCODEBACTHNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.BARCODEBACTHNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.BARCODEBACTHNO,
                            700, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] SUPPLIERCODEImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.SUPPLIERCODE))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.SUPPLIERCODE,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] CUSTOMERPARTNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.BarcodeCMPARTNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.BarcodeCMPARTNO,
                            700, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] QUANTITYImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.QUANTITY.Value.ToString()))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.QUANTITY.Value.ToString(),
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] StrQuantityImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.StrQuantity))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.StrQuantity,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] Barcode2DImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.DBARCODE))
                    {
                        DmtxImageEncoder encoder = new DmtxImageEncoder();
                        DmtxImageEncoderOptions options = new DmtxImageEncoderOptions();
                        options.Scheme = DmtxScheme.DmtxSchemeAscii;
                        options.ModuleSize = 5;
                        options.MarginSize = 1;
                        options.BackColor = System.Drawing.Color.White;
                        options.ForeColor = System.Drawing.Color.Black;
                        Bitmap oB = encoder.EncodeImage(this.DBARCODE, options);

                        results = NLib.Utils.ImageUtils.GetImage(oB);
                    }
                    return results;
                }
                set { }
            }


            #endregion
        }
        #endregion
    }
}
