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
    public class WeavingClassData
    {
        #region Weaving ClassData
        private static WeavingClassData _instance = null;

        public static WeavingClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(WeavingClassData))
                    {
                        _instance = new WeavingClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListWEAVINGINGDATA

        public class ListWEAVINGINGDATA
        {
             private static BarcodeLib.Barcode BarcodeGenerator = null;

             static ListWEAVINGINGDATA()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListWEAVINGINGDATA()
            {
                // default constructor
            }

            public ListWEAVINGINGDATA(int rowNo, string _WEAVINGLOT, string _ITM_WEAVING, decimal? _LENGTH, string _LOOMNO, DateTime? _WEAVINGDATE
            , string _SHIFT, string _REMARK, DateTime? _CREATEDATE, decimal? _WIDTH, string _PREPAREBY, string _WEAVINGNO
                , string _BEAMLOT, decimal? _DOFFNO, decimal? _DENSITY_WARP, decimal? _TENSION, DateTime? _STARTDATE, string _DOFFBY, decimal? _SPEED,
             decimal? _WASTE, decimal? _DENSITY_WEFT, string _DELETEFLAG, string _DELETEBY, DateTime? _DELETEDATE)
            {
                #region ListWEAVINGINGDATA

                RowNo = rowNo;
                WEAVINGLOT = _WEAVINGLOT;
                ITM_WEAVING = _ITM_WEAVING;
                LENGTH = _LENGTH;
                LOOMNO = _LOOMNO;
                WEAVINGDATE = _WEAVINGDATE;
                SHIFT = _SHIFT;
                REMARK = _REMARK;
                CREATEDATE = _CREATEDATE;
                WIDTH = _WIDTH;
                PREPAREBY = _PREPAREBY;
                WEAVINGNO = _WEAVINGNO;

                BEAMLOT = _BEAMLOT;
                DOFFNO = _DOFFNO;
                DENSITY_WARP = _DENSITY_WARP;
                TENSION = _TENSION;
                STARTDATE = _STARTDATE;
                DOFFBY = _DOFFBY;
                SPEED = _SPEED;
                WASTE = _WASTE;
                DENSITY_WEFT = _DENSITY_WEFT;
                DELETEFLAG = _DELETEFLAG;
                DELETEBY = _DELETEBY;
                DELETEDATE = _DELETEDATE;

                #endregion
            }

            #region Pack_PalletSheet 
            
            public int RowNo { get; set; }
            public string WEAVINGLOT { get; set; }
            public string ITM_WEAVING { get; set; }
            public decimal? LENGTH { get; set; }
            public string LOOMNO { get; set; }
            public DateTime? WEAVINGDATE { get; set; }
            public string SHIFT { get; set; }
            public string REMARK { get; set; }
            public DateTime? CREATEDATE { get; set; }
            public decimal? WIDTH { get; set; }
            public string PREPAREBY { get; set; }
            public string WEAVINGNO { get; set; }

            public string BEAMLOT { get; set; }
            public decimal? DOFFNO { get; set; }
            public decimal? DENSITY_WARP { get; set; }
            public decimal? TENSION { get; set; }
            public DateTime? STARTDATE { get; set; }
            public string DOFFBY { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? WASTE { get; set; }
            public decimal? DENSITY_WEFT { get; set; }
            public string DELETEFLAG { get; set; }
            public string DELETEBY { get; set; }
            public DateTime? DELETEDATE { get; set; }

            // For Barcode
            public byte[] ITM_WEAVINGImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.ITM_WEAVING))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.ITM_WEAVING,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] WEAVINGLOTImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.WEAVINGLOT))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.WEAVINGLOT,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] LENGTHImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.LENGTH.Value.ToString()))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.LENGTH.Value.ToString(),
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

        #region ListWeavingSampling

        public class ListWeavingSampling
        {
             private static BarcodeLib.Barcode BarcodeGenerator = null;

             static ListWeavingSampling()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListWeavingSampling()
            {
                // default constructor
            }

            public ListWeavingSampling(string _BEAMERROLL,
                    string _LOOMNO,string _ITM_WEAVING,DateTime? _SETTINGDATE,string _BARNO,decimal? _SPIRAL_L, decimal? _SPIRAL_R,
                    decimal? _STSAMPLING,decimal? _RECUTSAMPLING,string _STSAMPLINGBY,string _RECUTBY,DateTime? _STDATE,DateTime? _RECUTDATE,string _REMARK
                , string _BEAMMC, string _WARPMC, string _BEAMERNO)
            {
                #region ListWeavingSampling

                BEAMERROLL = _BEAMERROLL;
                LOOMNO = _LOOMNO;
                ITM_WEAVING = _ITM_WEAVING;
                SETTINGDATE = _SETTINGDATE;
                LOOMNO = _LOOMNO;
                BARNO = _BARNO;
                SPIRAL_L = _SPIRAL_L;
                SPIRAL_R = _SPIRAL_R;
                STSAMPLING = _STSAMPLING;
                RECUTSAMPLING = _RECUTSAMPLING;
                STSAMPLINGBY = _STSAMPLINGBY;
                RECUTBY = _RECUTBY;
                STDATE = _STDATE;
                RECUTDATE = _RECUTDATE;
                REMARK = _REMARK;
                BEAMMC = _BEAMMC;
                WARPMC = _WARPMC;
                BEAMERNO = _BEAMERNO;

                #endregion
            }

            #region ListWeavingSampling

            public string BEAMERROLL { get; set; }
            public string LOOMNO { get; set; }
            public string ITM_WEAVING { get; set; }
            public DateTime? SETTINGDATE { get; set; }
            public string BARNO { get; set; }
            public decimal? SPIRAL_L { get; set; }
            public decimal? SPIRAL_R { get; set; }
            public decimal? STSAMPLING { get; set; }
            public decimal? RECUTSAMPLING { get; set; }
            public string STSAMPLINGBY { get; set; }
            public string RECUTBY { get; set; }
            public DateTime? STDATE { get; set; }
            public DateTime? RECUTDATE { get; set; }
            public string REMARK { get; set; }

            public string BEAMMC { get; set; }
            public string WARPMC { get; set; }
            public string BEAMERNO { get; set; }

            public byte[] BEAMERROLLImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.BEAMERROLL))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.BEAMERROLL,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] LOOMNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.LOOMNO.ToString()))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.LOOMNO.ToString(),
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

        #region ListWEAV_SEARCHPRODUCTION

        public class ListWEAV_SEARCHPRODUCTION
        {

            public ListWEAV_SEARCHPRODUCTION()
            {
                // default constructor
            }

            public ListWEAV_SEARCHPRODUCTION(string _BEAMLOT,string _MC, string _REEDNO2, string _WEFTYARN,
                    string _TEMPLETYPE,string _BARNO, DateTime? _STARTDATE, DateTime? _FINISHDATE,string _FINISHFLAG,
                    string _SETTINGBY, DateTime? _EDITDATE,string _EDITBY, string _ITM_WEAVING,string _PRODUCTTYPEID,
                    decimal? _WIDTH, decimal? _BEAMLENGTH, decimal? _SPEED, string _BEAMERNO)
            {
                #region ListWEAVINGINGDATA

                BEAMLOT = _BEAMLOT;
                MC = _MC;
                REEDNO2 = _REEDNO2;
                WEFTYARN = _WEFTYARN;
                TEMPLETYPE = _TEMPLETYPE;
                BARNO = _BARNO;
                STARTDATE = _STARTDATE;
                FINISHDATE = _FINISHDATE;
                FINISHFLAG = _FINISHFLAG;
                SETTINGBY = _SETTINGBY;
                EDITDATE = _EDITDATE;
                EDITBY = _EDITBY;

                ITM_WEAVING = _ITM_WEAVING;
                PRODUCTTYPEID = _PRODUCTTYPEID;
                WIDTH = _WIDTH;
                BEAMLENGTH = _BEAMLENGTH;
                SPEED = _SPEED;
                BEAMERNO = _BEAMERNO;

                #endregion
            }

            #region ListWEAV_SEARCHPRODUCTION

            public string BEAMLOT { get; set; }
            public string MC { get; set; }
            public string REEDNO2 { get; set; }
            public string WEFTYARN { get; set; }
            public string TEMPLETYPE { get; set; }
            public string BARNO { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? FINISHDATE { get; set; }
            public string FINISHFLAG { get; set; }
            public string SETTINGBY { get; set; }
            public DateTime? EDITDATE { get; set; }
            public string EDITBY { get; set; }
            public string ITM_WEAVING { get; set; }
            public string PRODUCTTYPEID { get; set; }
            public decimal? WIDTH { get; set; }
            public decimal? BEAMLENGTH { get; set; }
            public decimal? SPEED { get; set; }
            public string BEAMERNO { get; set; }

            #endregion
        }
        #endregion

        #region ListWEAV_GETWEAVELISTBYBEAMROLL

        public class ListWEAV_GETWEAVELISTBYBEAMROLL
        {

            public ListWEAV_GETWEAVELISTBYBEAMROLL()
            {
                // default constructor
            }

            public ListWEAV_GETWEAVELISTBYBEAMROLL(string _WEAVINGLOT ,string _ITM_WEAVING ,decimal? _LENGTH ,string _LOOMNO ,
                    DateTime? _WEAVINGDATE ,string _SHIFT ,string _REMARK ,DateTime? _CREATEDATE , decimal? _WIDTH ,
                    string _PREPAREBY ,string _WEAVINGNO ,string _BEAMLOT ,decimal? _DOFFNO ,    decimal? _TENSION ,
                    DateTime? _STARTDATE , string _DOFFBY , decimal? _SPEED ,decimal? _WASTE ,decimal? _DENSITY_WARP ,
                    decimal? _DENSITY_WEFT , string _DELETEFLAG , string _DELETEBY , DateTime? _DELETEDATE ,string _DeleteHistory )
            {
                #region ListWEAVINGINGDATA

                WEAVINGLOT = _WEAVINGLOT;
                ITM_WEAVING = _ITM_WEAVING;
                LENGTH = _LENGTH;
                LOOMNO = _LOOMNO;
                WEAVINGDATE = _WEAVINGDATE;
                SHIFT = _SHIFT;
                REMARK = _REMARK;
                CREATEDATE = _CREATEDATE;
                WIDTH = _WIDTH;
                PREPAREBY = _PREPAREBY;
                WEAVINGNO = _WEAVINGNO;
                BEAMLOT = _BEAMLOT;
                DOFFNO = _DOFFNO;
                TENSION = _TENSION;
                STARTDATE = _STARTDATE;
                DOFFBY = _DOFFBY;
                SPEED = _SPEED;
                WASTE = _WASTE;
                DENSITY_WARP = _DENSITY_WARP;
                DENSITY_WEFT = _DENSITY_WEFT;
                DELETEFLAG = _DELETEFLAG;
                DELETEBY = _DELETEBY;
                DELETEDATE = _DELETEDATE;
                DeleteHistory = _DeleteHistory;

                #endregion
            }

            #region WEAV_GETWEAVELISTBYBEAMROLL

            public string WEAVINGLOT { get; set; }
            public string ITM_WEAVING { get; set; }
            public decimal? LENGTH { get; set; }
            public string LOOMNO { get; set; }
            public DateTime? WEAVINGDATE { get; set; }
            public string SHIFT { get; set; }
            public string REMARK { get; set; }
            public DateTime? CREATEDATE { get; set; }
            public decimal? WIDTH { get; set; }
            public string PREPAREBY { get; set; }
            public string WEAVINGNO { get; set; }
            public string BEAMLOT { get; set; }
            public decimal? DOFFNO { get; set; }

            public decimal? TENSION { get; set; }
            public DateTime? STARTDATE { get; set; }
            public string DOFFBY { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? WASTE { get; set; }

            public decimal? DENSITY_WARP { get; set; }
            public decimal? DENSITY_WEFT { get; set; }

            public string DELETEFLAG { get; set; }
            public string DELETEBY { get; set; }
            public DateTime? DELETEDATE { get; set; }

            public string DeleteHistory { get; set; }

            #endregion
        }
        #endregion

        #region ListWEAV_GETMCSTOPBYLOT

        public class ListWEAV_GETMCSTOPBYLOT
        {

            public ListWEAV_GETMCSTOPBYLOT()
            {
                // default constructor
            }

            public ListWEAV_GETMCSTOPBYLOT(string _WEAVINGLOT,
                     string _DEFECTCODE,decimal? _DEFECTPOSITION,string _CREATEBY,DateTime? _CREATEDATE,string _REMARK,
                     string _LOOMNO,string _BEAMERROLL,decimal? _DOFFNO,decimal? _DEFECTLENGTH,string _DESCRIPTION,
                     DateTime? _WEAVSTARTDATE,DateTime? _WEAVFINISHDATE,string _ITM_WEAVING,decimal? _WIDTH,decimal? _LENGTH)
            {
                #region ListWEAV_GETMCSTOPBYLOT

                WEAVINGLOT = _WEAVINGLOT;
                DEFECTCODE = _DEFECTCODE;
                DEFECTPOSITION = _DEFECTPOSITION;
                CREATEBY = _CREATEBY;
                CREATEDATE = _CREATEDATE;
                REMARK = _REMARK;
                LOOMNO = _LOOMNO;
                BEAMERROLL = _BEAMERROLL;
                DOFFNO = _DOFFNO;
                DEFECTLENGTH = _DEFECTLENGTH;
                DESCRIPTION = _DESCRIPTION;
                WEAVSTARTDATE = _WEAVSTARTDATE;
                WEAVFINISHDATE = _WEAVFINISHDATE;
                ITM_WEAVING = _ITM_WEAVING;
                WIDTH = _WIDTH;
                LENGTH = _LENGTH;

                #endregion
            }

            #region WEAV_GETMCSTOPBYLOT

            public string WEAVINGLOT { get; set; }
            public string DEFECTCODE { get; set; }
            public decimal? DEFECTPOSITION { get; set; }
            public string CREATEBY { get; set; }
            public DateTime? CREATEDATE { get; set; }
            public string REMARK { get; set; }
            public string LOOMNO { get; set; }
            public string BEAMERROLL { get; set; }
            public decimal? DOFFNO { get; set; }
            public decimal? DEFECTLENGTH { get; set; }
            public string DESCRIPTION { get; set; }
            public DateTime? WEAVSTARTDATE { get; set; }
            public DateTime? WEAVFINISHDATE { get; set; }
            public string ITM_WEAVING { get; set; }
            public decimal? WIDTH { get; set; }
            public decimal? LENGTH { get; set; }

            #endregion
        }
        #endregion

        #region ListWEAV_SHIPMENTREPORT

        public class ListWEAV_SHIPMENTREPORT
        {

            public ListWEAV_SHIPMENTREPORT()
            {
                // default constructor
            }

            public ListWEAV_SHIPMENTREPORT(int _No,
                     string _ITM_WEAVING, decimal? _PIECES, decimal? _METERS)
            {
                #region ListWEAV_SHIPMENTREPORT

                No = _No;
                ITM_WEAVING = _ITM_WEAVING;
                PIECES = _PIECES;
                METERS = _METERS;

                #endregion
            }

            #region WEAV_SHIPMENTREPORT

            public int No { get; set; }
            public string ITM_WEAVING { get; set; }
            public decimal? PIECES { get; set; }
            public decimal? METERS { get; set; }
          

            #endregion
        }
        #endregion
    }
}
