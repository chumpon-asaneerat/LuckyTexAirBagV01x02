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
    public class DrawingClassData
    {
        #region Drawing ClassData
        private static DrawingClassData _instance = null;

        public static DrawingClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(DrawingClassData))
                    {
                        _instance = new DrawingClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListDRAW_DAILYREPORT

        public class ListDRAW_DAILYREPORT
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListDRAW_DAILYREPORT()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListDRAW_DAILYREPORT()
            {
                // default constructor
            }

            public ListDRAW_DAILYREPORT(int P_No, string P_BEAMLOT, string P_ITM_PREPARE, string P_PRODUCTTYPEID, string P_DRAWINGTYPE,
            DateTime? P_STARTDATE, DateTime? P_ENDATE, string P_REEDNO, string P_HEALDCOLOR, string P_STARTBY, string P_FINISHBY,
            string P_USEFLAG, decimal? P_HEALDNO, string P_OPERATOR_GROUP, decimal? P_TOTALYARN, string P_BEAMNO, decimal? P_LENGTH, string P_BEAMERNO)
            {
                #region ListDRAW_DAILYREPORT

                No = P_No;
                BEAMLOT = P_BEAMLOT;
                ITM_PREPARE = P_ITM_PREPARE;
                PRODUCTTYPEID = P_PRODUCTTYPEID;
                DRAWINGTYPE = P_DRAWINGTYPE;
                STARTDATE = P_STARTDATE;
                ENDATE = P_ENDATE;
                REEDNO = P_REEDNO;
                HEALDCOLOR = P_HEALDCOLOR;
                STARTBY = P_STARTBY;
                FINISHBY = P_FINISHBY;
                USEFLAG = P_USEFLAG;
                HEALDNO = P_HEALDNO;
                OPERATOR_GROUP = P_OPERATOR_GROUP;
                TOTALYARN = P_TOTALYARN;
                BEAMNO = P_BEAMNO;
                LENGTH = P_LENGTH;
                BEAMERNO = P_BEAMERNO;


                #endregion
            }

            #region ListDRAW_DAILYREPORT

            public int No { get; set; }
            public string BEAMLOT { get; set; }
            public string ITM_PREPARE { get; set; }
            public string PRODUCTTYPEID { get; set; }
            public string DRAWINGTYPE { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDATE { get; set; }
            public string REEDNO { get; set; }
            public string HEALDCOLOR { get; set; }
            public string STARTBY { get; set; }
            public string FINISHBY { get; set; }
            public string USEFLAG { get; set; }
            public decimal? HEALDNO { get; set; }
            public string OPERATOR_GROUP { get; set; }
            public decimal? TOTALYARN { get; set; }
            public string BEAMNO { get; set; }
            public decimal? LENGTH { get; set; }
            public string BEAMERNO { get; set; }

            // For Barcode
            public byte[] BEAMLOTImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.BEAMLOT))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.BEAMLOT,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] ITM_PREPAREImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.ITM_PREPARE))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.ITM_PREPARE,
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

        #region ListDRAW_TRANSFERSLIP

        public class ListDRAW_TRANSFERSLIP
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListDRAW_TRANSFERSLIP()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListDRAW_TRANSFERSLIP()
            {
                // default constructor
            }

            public ListDRAW_TRANSFERSLIP(string P_BEAMLOT, string P_ITM_PREPARE, string P_PRODUCTTYPEID, string P_DRAWINGTYPE,
            DateTime? P_STARTDATE, DateTime? P_ENDATE, string P_REEDNO, string P_HEALDCOLOR, string P_STARTBY, string P_FINISHBY,
            string P_USEFLAG, decimal? P_HEALDNO, string P_OPERATOR_GROUP, decimal? P_TOTALYARN, string P_BEAMNO, decimal? P_LENGTH, string P_BEAMERNO)
            {
                #region ListDRAW_TRANSFERSLIP

                BEAMLOT = P_BEAMLOT;
                ITM_PREPARE = P_ITM_PREPARE;
                PRODUCTTYPEID = P_PRODUCTTYPEID;
                DRAWINGTYPE = P_DRAWINGTYPE;
                STARTDATE = P_STARTDATE;
                ENDATE = P_ENDATE;
                REEDNO = P_REEDNO;
                HEALDCOLOR = P_HEALDCOLOR;
                STARTBY = P_STARTBY;
                FINISHBY = P_FINISHBY;
                USEFLAG = P_USEFLAG;
                HEALDNO = P_HEALDNO;
                OPERATOR_GROUP = P_OPERATOR_GROUP;
                TOTALYARN = P_TOTALYARN;
                BEAMNO = P_BEAMNO;
                LENGTH = P_LENGTH;
                BEAMERNO = P_BEAMERNO;


                #endregion
            }

            #region ListDRAW_TRANSFERSLIP

            public string BEAMLOT { get; set; }
            public string ITM_PREPARE { get; set; }
            public string PRODUCTTYPEID { get; set; }
            public string DRAWINGTYPE { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDATE { get; set; }
            public string REEDNO { get; set; }
            public string HEALDCOLOR { get; set; }
            public string STARTBY { get; set; }
            public string FINISHBY { get; set; }
            public string USEFLAG { get; set; }
            public decimal? HEALDNO { get; set; }
            public string OPERATOR_GROUP { get; set; }
            public decimal? TOTALYARN { get; set; }
            public string BEAMNO { get; set; }
            public decimal? LENGTH { get; set; }
            public string BEAMERNO { get; set; }

            // For Barcode
            public byte[] BEAMLOTImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.BEAMLOT))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.BEAMLOT,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] ITM_PREPAREImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.ITM_PREPARE))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.ITM_PREPARE,
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
    }
}
