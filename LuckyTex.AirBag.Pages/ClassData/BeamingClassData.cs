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
    public class BeamingClassData
    {
        #region Beaming ClassData
        private static BeamingClassData _instance = null;

        public static BeamingClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(BeamingClassData))
                    {
                        _instance = new BeamingClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListBEAM_TRANFERSLIP

        public class ListBEAM_TRANFERSLIP
        {
             private static BarcodeLib.Barcode BarcodeGenerator = null;

             static ListBEAM_TRANFERSLIP()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListBEAM_TRANFERSLIP()
            {
                // default constructor
            }

            public ListBEAM_TRANFERSLIP(string P_BEAMERNO, string P_BEAMLOT, string P_BEAMNO, DateTime? P_STARTDATE, DateTime? P_ENDDATE
            , decimal? P_LENGTH, decimal? P_SPEED, decimal? P_BEAMSTANDTENSION, decimal? P_WINDINGTENSION
            , decimal? P_HARDNESS_L, decimal? P_HARDNESS_N, decimal? P_HARDNESS_R, decimal? P_INSIDE_WIDTH
            , decimal? P_OUTSIDE_WIDTH, decimal? P_FULL_WIDTH
            , string P_STARTBY, string P_DOFFBY, string P_BEAMMC, string P_FLAG, string P_REMARK, string P_ITM_PREPARE)
            {
                #region ListPacking

                BEAMERNO = P_BEAMERNO;
                BEAMLOT = P_BEAMLOT;
                BEAMNO = P_BEAMNO;

                STARTDATE = P_STARTDATE;
                ENDDATE = P_ENDDATE;
                LENGTH = P_LENGTH;
                SPEED = P_SPEED;
                BEAMSTANDTENSION = P_BEAMSTANDTENSION;
                WINDINGTENSION = P_WINDINGTENSION;
                HARDNESS_L = P_HARDNESS_L;
                HARDNESS_N = P_HARDNESS_N;
                HARDNESS_R = P_HARDNESS_R;
                INSIDE_WIDTH = P_INSIDE_WIDTH;
                OUTSIDE_WIDTH = P_OUTSIDE_WIDTH;
                FULL_WIDTH = P_FULL_WIDTH;
                STARTBY = P_STARTBY;
                DOFFBY = P_DOFFBY;
                BEAMMC = P_BEAMMC;
                FLAG = P_FLAG;
                REMARK = P_REMARK;
                ITM_PREPARE = P_ITM_PREPARE;
                
                #endregion
            }

            #region ListBEAM_TRANFERSLIP

            public string BEAMERNO { get; set; }
            public string BEAMLOT { get; set; }
            public string BEAMNO { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public Decimal? LENGTH { get; set; }
            public Decimal? SPEED { get; set; }
            public Decimal? BEAMSTANDTENSION { get; set; }
            public Decimal? WINDINGTENSION { get; set; }
            public Decimal? HARDNESS_L { get; set; }
            public Decimal? HARDNESS_N { get; set; }
            public Decimal? HARDNESS_R { get; set; }
            public Decimal? INSIDE_WIDTH { get; set; }
            public Decimal? OUTSIDE_WIDTH { get; set; }
            public Decimal? FULL_WIDTH { get; set; }
            public string STARTBY { get; set; }
            public string DOFFBY { get; set; }
            public string BEAMMC { get; set; }
            public string FLAG { get; set; }
            public string REMARK { get; set; }
            public string ITM_PREPARE { get; set; }

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

        #region ListBeamingRecordHead

        public class ListBeamingRecordHead
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListBeamingRecordHead()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListBeamingRecordHead()
            {
                // default constructor
            }

            public ListBeamingRecordHead(string P_BEAMERNO, string P_ITM_PREPARE, string P_MCNO, decimal? P_TOTALYARN, decimal? P_TOTALKEBA, decimal? P_ADJUSTKEBA
                , DateTime? P_STARTDATE, DateTime? P_ENDDATE, string P_REMARK)
            {
                #region ListPacking

                BEAMERNO = P_BEAMERNO;
                ITM_PREPARE = P_ITM_PREPARE;
                MCNO = P_MCNO;
                TOTALYARN = P_TOTALYARN;
                TOTALKEBA = P_TOTALKEBA;
                ADJUSTKEBA = P_ADJUSTKEBA;
                STARTDATE = P_STARTDATE;
                ENDDATE = P_ENDDATE;
                REMARK = P_REMARK;

                #endregion
            }

            #region BeamingRecordHead

            public string BEAMERNO { get; set; }
            public string ITM_PREPARE { get; set; }

            public string MCNO { get; set; }

            public decimal? TOTALYARN { get; set; }
            public decimal? TOTALKEBA { get; set; }
            public decimal? ADJUSTKEBA { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public string REMARK { get; set; }

            // For Barcode
            public byte[] BEAMERNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.BEAMERNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.BEAMERNO,
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

        #region ListBEAM_GETBEAMLOTBYBEAMERNO

        public class ListBEAM_GETBEAMLOTBYBEAMERNO
        {
            public ListBEAM_GETBEAMLOTBYBEAMERNO()
            {
                // default constructor
            }

            public ListBEAM_GETBEAMLOTBYBEAMERNO(string P_BEAMERNO, string P_BEAMLOT, string P_BEAMNO, DateTime? P_STARTDATE, DateTime? P_ENDDATE,
                        decimal? P_LENGTH,decimal? P_SPEED,decimal? P_BEAMSTANDTENSION,decimal? P_WINDINGTENSION,decimal? P_HARDNESS_L,decimal? P_HARDNESS_N,
                        decimal? P_HARDNESS_R,decimal? P_INSIDE_WIDTH, decimal? P_OUTSIDE_WIDTH,decimal? P_FULL_WIDTH,string P_STARTBY,string P_DOFFBY,
                        string P_BEAMMC,string P_FLAG,string P_REMARK,decimal? P_TENSION_ST1,decimal? P_TENSION_ST2,decimal? P_TENSION_ST3,decimal? P_TENSION_ST4,
                        decimal? P_TENSION_ST5,decimal? P_TENSION_ST6,decimal? P_TENSION_ST7,decimal? P_TENSION_ST8,decimal? P_TENSION_ST9,decimal? P_TENSION_ST10,string P_EDITBY,
                        string P_OLDBEAMNO, DateTime? P_EDITDATE, string P_ITM_PREPARE, decimal? P_KEBA, decimal? P_MISSYARN, decimal? P_OTHER)
            {
                #region ListPacking

                BEAMERNO = P_BEAMERNO;
                BEAMLOT = P_BEAMLOT;
                BEAMNO = P_BEAMNO;
                STARTDATE = P_STARTDATE;
                ENDDATE = P_ENDDATE;
                LENGTH = P_LENGTH;
                SPEED = P_SPEED;
                BEAMSTANDTENSION = P_BEAMSTANDTENSION;
                WINDINGTENSION = P_WINDINGTENSION;
                HARDNESS_L = P_HARDNESS_L;
                HARDNESS_N = P_HARDNESS_N;
                HARDNESS_R = P_HARDNESS_R;
                INSIDE_WIDTH = P_INSIDE_WIDTH;
                OUTSIDE_WIDTH = P_OUTSIDE_WIDTH;
                FULL_WIDTH = P_FULL_WIDTH;
                STARTBY = P_STARTBY;
                DOFFBY = P_DOFFBY;
                BEAMMC = P_BEAMMC;
                FLAG = P_FLAG;
                REMARK = P_REMARK;
                TENSION_ST1 = P_TENSION_ST1;
                TENSION_ST2 = P_TENSION_ST2;
                TENSION_ST3 = P_TENSION_ST3;
                TENSION_ST4 = P_TENSION_ST4;
                TENSION_ST5 = P_TENSION_ST5;
                TENSION_ST6 = P_TENSION_ST6;
                TENSION_ST7 = P_TENSION_ST7;
                TENSION_ST8 = P_TENSION_ST8;
                TENSION_ST9 = P_TENSION_ST9;
                TENSION_ST10 = P_TENSION_ST10;
                EDITBY = P_EDITBY;
                OLDBEAMNO = P_OLDBEAMNO;
                EDITDATE = P_EDITDATE;

                ITM_PREPARE = P_ITM_PREPARE;

                KEBA = P_KEBA;
                MISSYARN = P_MISSYARN;
                OTHER = P_OTHER;

                #endregion
            }

            #region BEAM_GETBEAMLOTBYBEAMERNO

            public string BEAMERNO { get; set; }
            public string BEAMLOT { get; set; }
            public string BEAMNO { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public decimal? LENGTH { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? BEAMSTANDTENSION { get; set; }
            public decimal? WINDINGTENSION { get; set; }
            public decimal? HARDNESS_L { get; set; }
            public decimal? HARDNESS_N { get; set; }
            public decimal? HARDNESS_R { get; set; }
            public decimal? INSIDE_WIDTH { get; set; }
            public decimal? OUTSIDE_WIDTH { get; set; }
            public decimal? FULL_WIDTH { get; set; }
            public string STARTBY { get; set; }
            public string DOFFBY { get; set; }
            public string BEAMMC { get; set; }
            public string FLAG { get; set; }
            public string REMARK { get; set; }
            public decimal? TENSION_ST1 { get; set; }
            public decimal? TENSION_ST2 { get; set; }
            public decimal? TENSION_ST3 { get; set; }
            public decimal? TENSION_ST4 { get; set; }
            public decimal? TENSION_ST5 { get; set; }
            public decimal? TENSION_ST6 { get; set; }
            public decimal? TENSION_ST7 { get; set; }
            public decimal? TENSION_ST8 { get; set; }
            public decimal? TENSION_ST9 { get; set; }
            public decimal? TENSION_ST10 { get; set; }
            public string EDITBY { get; set; }
            public string OLDBEAMNO { get; set; }
            public DateTime? EDITDATE { get; set; }
            public string ITM_PREPARE { get; set; }

            public System.Decimal? KEBA { get; set; }
            public System.Decimal? MISSYARN { get; set; }
            public System.Decimal? OTHER { get; set; }

            #endregion
        }
        #endregion

        #region ListBEAM_GETSPECBYCHOPNO

        public class ListBEAM_GETSPECBYCHOPNO
        {
            public ListBEAM_GETSPECBYCHOPNO()
            {
                // default constructor
            }

            public ListBEAM_GETSPECBYCHOPNO(string P_CHOPNO, decimal? P_NOWARPBEAM, decimal? P_TOTALYARN, decimal? P_TOTALKEBA, decimal? P_BEAMLENGTH,
                decimal? P_MAXHARDNESS,decimal? P_MINHARDNESS,decimal? P_MAXBEAMWIDTH, decimal? P_MINBEAMWIDTH, decimal? P_MAXSPEED,decimal? P_MINSPEED,decimal? P_MAXYARNTENSION, 
                decimal? P_MINYARNTENSION,decimal? P_MAXWINDTENSION,decimal? P_MINWINDTENSION,string P_COMBTYPE, decimal? P_COMBPITCH,decimal? P_TOTALBEAM)
            {
                #region ListPacking

                CHOPNO = P_CHOPNO;
                NOWARPBEAM = P_NOWARPBEAM;
                TOTALYARN = P_TOTALYARN;
                TOTALKEBA = P_TOTALKEBA;
                BEAMLENGTH = P_BEAMLENGTH;
                MAXHARDNESS = P_MAXHARDNESS;
                MINHARDNESS = P_MINHARDNESS;
                MAXBEAMWIDTH = P_MAXBEAMWIDTH;
                MINBEAMWIDTH = P_MINBEAMWIDTH;
                MAXSPEED = P_MAXSPEED;
                MINSPEED = P_MINSPEED;
                MAXYARNTENSION = P_MAXYARNTENSION;
                MINYARNTENSION = P_MINYARNTENSION;
                MAXWINDTENSION = P_MAXWINDTENSION;
                MINWINDTENSION = P_MINWINDTENSION;
                COMBTYPE = P_COMBTYPE;
                COMBPITCH = P_COMBPITCH;
                TOTALBEAM = P_TOTALBEAM;
               
                #endregion
            }

            #region BEAM_GETSPECBYCHOPNO

            public string CHOPNO { get; set; }
            public decimal? NOWARPBEAM { get; set; }
            public decimal? TOTALYARN { get; set; }
            public decimal? TOTALKEBA { get; set; }
            public decimal? BEAMLENGTH { get; set; }
            public decimal? MAXHARDNESS { get; set; }
            public decimal? MINHARDNESS { get; set; }
            public decimal? MAXBEAMWIDTH { get; set; }
            public decimal? MINBEAMWIDTH { get; set; }
            public decimal? MAXSPEED { get; set; }
            public decimal? MINSPEED { get; set; }
            public decimal? MAXYARNTENSION { get; set; }
            public decimal? MINYARNTENSION { get; set; }
            public decimal? MAXWINDTENSION { get; set; }
            public decimal? MINWINDTENSION { get; set; }
            public string COMBTYPE { get; set; }
            public decimal? COMBPITCH { get; set; }
            public decimal? TOTALBEAM { get; set; }

            #endregion
        }
        #endregion

        #region ListBEAM_GETWARPROLLBYBEAMERNO

        public class ListBEAM_GETWARPROLLBYBEAMERNO
        {
            public ListBEAM_GETWARPROLLBYBEAMERNO()
            {
                // default constructor
            }

            public ListBEAM_GETWARPROLLBYBEAMERNO(string P_BEAMERNO, string P_WARPHEADNO, string P_WARPERLOT)
            {
                #region ListPacking

                BEAMERNO = P_BEAMERNO;
                WARPHEADNO = P_WARPHEADNO;
                WARPERLOT = P_WARPERLOT;
               
                #endregion
            }

            #region BEAM_GETWARPROLLBYBEAMERNO

            public string BEAMERNO { get; set; }
            public string WARPHEADNO { get; set; }
            public string WARPERLOT { get; set; }

            #endregion
        }
        #endregion

        #region ListBEAM_BEAMLIST

        public class ListBEAM_BEAMLIST
        {
            public ListBEAM_BEAMLIST()
            {
                // default constructor
            }

            public ListBEAM_BEAMLIST(string P_BEAMERNO, string P_BEAMLOT, string P_BEAMNO, DateTime? P_STARTDATE, DateTime? P_ENDDATE,
                decimal? P_LENGTH, decimal? P_SPEED, decimal? P_BEAMSTANDTENSION, decimal? P_WINDINGTENSION, decimal? P_HARDNESS_L, decimal? P_HARDNESS_N, decimal? P_HARDNESS_R,
                decimal? P_INSIDE_WIDTH, decimal? P_OUTSIDE_WIDTH, decimal? P_FULL_WIDTH, string P_STARTBY, string P_DOFFBY, string P_BEAMMC, string P_FLAG, string P_REMARK,
                decimal? P_TENSION_ST1, decimal? P_TENSION_ST2, decimal? P_TENSION_ST3, decimal? P_TENSION_ST4, decimal? P_TENSION_ST5, decimal? P_TENSION_ST6,
                decimal? P_TENSION_ST7, decimal? P_TENSION_ST8, decimal? P_TENSION_ST9, decimal? P_TENSION_ST10, string P_EDITBY, string P_OLDBEAMNO,
                DateTime? P_EDITDATE, string P_ITM_PREPARE, string P_WARPHEADNO, decimal? P_TOTALYARN, decimal? P_TOTALKEBA)
            {
                #region ListPacking

                BEAMERNO = P_BEAMERNO;
                BEAMLOT = P_BEAMLOT;
                BEAMNO = P_BEAMNO;
                STARTDATE = P_STARTDATE;
                ENDDATE = P_ENDDATE;
                LENGTH = P_LENGTH;
                SPEED = P_SPEED;
                BEAMSTANDTENSION = P_BEAMSTANDTENSION;
                WINDINGTENSION = P_WINDINGTENSION;
                HARDNESS_L = P_HARDNESS_L;
                HARDNESS_N = P_HARDNESS_N;
                HARDNESS_R = P_HARDNESS_R;
                INSIDE_WIDTH = P_INSIDE_WIDTH;
                OUTSIDE_WIDTH = P_OUTSIDE_WIDTH;
                FULL_WIDTH = P_FULL_WIDTH;
                STARTBY = P_STARTBY;
                DOFFBY = P_DOFFBY;
                BEAMMC = P_BEAMMC;
                FLAG = P_FLAG;
                REMARK = P_REMARK;
                TENSION_ST1 = P_TENSION_ST1;
                TENSION_ST2 = P_TENSION_ST2;
                TENSION_ST3 = P_TENSION_ST3;
                TENSION_ST4 = P_TENSION_ST4;
                TENSION_ST5 = P_TENSION_ST5;
                TENSION_ST6 = P_TENSION_ST6;
                TENSION_ST7 = P_TENSION_ST7;
                TENSION_ST8 = P_TENSION_ST8;
                TENSION_ST9 = P_TENSION_ST9;
                TENSION_ST10 = P_TENSION_ST10;
                EDITBY = P_EDITBY;
                OLDBEAMNO = P_OLDBEAMNO;
                EDITDATE = P_EDITDATE;
                ITM_PREPARE = P_ITM_PREPARE;
                WARPHEADNO = P_WARPHEADNO;
                TOTALYARN = P_TOTALYARN;
                TOTALKEBA = P_TOTALKEBA;

                #endregion
            }

            #region BEAM_BEAMLIST

            public string BEAMERNO { get; set; }
            public string BEAMLOT { get; set; }
            public string BEAMNO { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public decimal? LENGTH { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? BEAMSTANDTENSION { get; set; }
            public decimal? WINDINGTENSION { get; set; }
            public decimal? HARDNESS_L { get; set; }
            public decimal? HARDNESS_N { get; set; }
            public decimal? HARDNESS_R { get; set; }
            public decimal? INSIDE_WIDTH { get; set; }
            public decimal? OUTSIDE_WIDTH { get; set; }
            public decimal? FULL_WIDTH { get; set; }
            public string STARTBY { get; set; }
            public string DOFFBY { get; set; }
            public string BEAMMC { get; set; }
            public string FLAG { get; set; }
            public string REMARK { get; set; }
            public decimal? TENSION_ST1 { get; set; }
            public decimal? TENSION_ST2 { get; set; }
            public decimal? TENSION_ST3 { get; set; }
            public decimal? TENSION_ST4 { get; set; }
            public decimal? TENSION_ST5 { get; set; }
            public decimal? TENSION_ST6 { get; set; }
            public decimal? TENSION_ST7 { get; set; }
            public decimal? TENSION_ST8 { get; set; }
            public decimal? TENSION_ST9 { get; set; }
            public decimal? TENSION_ST10 { get; set; }
            public string EDITBY { get; set; }
            public string OLDBEAMNO { get; set; }
            public DateTime? EDITDATE { get; set; }
            public string ITM_PREPARE { get; set; }
            public string WARPHEADNO { get; set; }
            public decimal? TOTALYARN { get; set; }
            public decimal? TOTALKEBA { get; set; }

            #endregion
        }
        #endregion
    }
}
