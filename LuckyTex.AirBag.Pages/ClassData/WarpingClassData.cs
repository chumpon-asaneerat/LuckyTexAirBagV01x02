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
    public class WarpingClassData
    {
        #region Warping ClassData
        private static WarpingClassData _instance = null;

        public static WarpingClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(WarpingClassData))
                    {
                        _instance = new WarpingClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListWARP_TRANFERSLIP

        public class ListWARP_TRANFERSLIP
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListWARP_TRANFERSLIP()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListWARP_TRANFERSLIP()
            {
                // default constructor
            }

            public ListWARP_TRANFERSLIP(string P_WARPHEADNO, string P_WARPERLOT, string P_BEAMNO, string P_SIDE, DateTime? P_STARTDATE, DateTime? P_ENDDATE
                , decimal? P_LENGTH, decimal? P_SPEED, decimal? P_HARDNESS_L, decimal? P_HARDNESS_N, decimal? P_HARDNESS_R, decimal? P_TENSION
                , string P_STARTBY, string P_DOFFBY, string P_FLAG, string P_WARPMC, string P_ITM_PREPARE, string P_ITM_YARN
                , string P_REMARK, decimal? P_TENSION_IT, decimal? P_TENSION_TAKEUP, decimal? P_MC_COUNT_L, decimal? P_MC_COUNT_S)
            {
                #region ListWARP_TRANFERSLIP

                WARPHEADNO = P_WARPHEADNO;
                WARPERLOT = P_WARPERLOT;
                BEAMNO = P_BEAMNO;
                SIDE = P_SIDE;
                STARTDATE = P_STARTDATE;
                ENDDATE = P_ENDDATE;
                LENGTH = P_LENGTH;
                SPEED = P_SPEED;
                HARDNESS_L = P_HARDNESS_L;
                HARDNESS_N = P_HARDNESS_N;
                HARDNESS_R = P_HARDNESS_R;
                TENSION = P_TENSION;
                STARTBY = P_STARTBY;
                DOFFBY = P_DOFFBY;
                FLAG = P_FLAG;
                WARPMC = P_WARPMC;
                ITM_PREPARE = P_ITM_PREPARE;
                ITM_YARN = P_ITM_YARN;

                REMARK = P_REMARK;
                TENSION_IT = P_TENSION_IT;
                TENSION_TAKEUP = P_TENSION_TAKEUP;
                MC_COUNT_L = P_MC_COUNT_L;
                MC_COUNT_S = P_MC_COUNT_S;

                #endregion
            }

            #region ListWARP_TRANFERSLIP

            public string WARPHEADNO { get; set; }
            public string WARPERLOT { get; set; }
            public string BEAMNO { get; set; }
            public string SIDE { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public decimal? LENGTH { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? HARDNESS_L { get; set; }
            public decimal? HARDNESS_N { get; set; }
            public decimal? HARDNESS_R { get; set; }
            public decimal? TENSION { get; set; }
            public string STARTBY { get; set; }
            public string DOFFBY { get; set; }
            public string FLAG { get; set; }
            public string WARPMC { get; set; }
            public string ITM_PREPARE { get; set; }
            public string ITM_YARN { get; set; }

            public string REMARK { get; set; }
            public decimal? TENSION_IT { get; set; }
            public decimal? TENSION_TAKEUP { get; set; }
            public decimal? MC_COUNT_L { get; set; }
            public decimal? MC_COUNT_S { get; set; }

            // For Barcode
            public byte[] WARPERLOTImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.WARPERLOT))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.WARPERLOT,
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

        #region ListWarpingRecordHead

        public class ListWarpingRecordHead
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListWarpingRecordHead()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListWarpingRecordHead()
            {
                // default constructor
            }

            public ListWarpingRecordHead(string P_WARPHEADNO, string P_SIDE, string P_WARPMC, string P_ITM_PREPARE
                , string P_WTYPE, string P_CONDITIONBY, string P_REEDNO, DateTime? P_CONDITIONSTART)
            {
                #region ListPacking

                WARPHEADNO = P_WARPHEADNO;
                SIDE = P_SIDE;
                WARPMC = P_WARPMC;
                ITM_PREPARE = P_ITM_PREPARE;
                WTYPE = P_WTYPE;
                CONDITIONBY = P_CONDITIONBY;
                REEDNO = P_REEDNO;
                CONDITIONSTART = P_CONDITIONSTART;

                #endregion
            }

            #region ListWarpingRecordHead

            public string WARPHEADNO { get; set; }
            public string SIDE { get; set; }
            public string WARPMC { get; set; }
            public string ITM_PREPARE { get; set; }

            public string WTYPE { get; set; }
            public string CONDITIONBY { get; set; }
            public string REEDNO { get; set; }
            public DateTime? CONDITIONSTART { get; set; }

            // For Barcode
            public byte[] WARPHEADNOImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.WARPHEADNO))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.WARPHEADNO,
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

        #region ListWARP_GETWARPERLOTBYHEADNO

        public class ListWARP_GETWARPERLOTBYHEADNO
        {
            public ListWARP_GETWARPERLOTBYHEADNO()
            {
                // default constructor
            }

            public ListWARP_GETWARPERLOTBYHEADNO(string P_WARPHEADNO, string P_WARPERLOT, string P_BEAMNO, string P_SIDE, DateTime? P_STARTDATE, DateTime? P_ENDDATE,
                     decimal? P_LENGTH, decimal? P_SPEED, decimal? P_HARDNESS_L, decimal? P_HARDNESS_N, decimal? P_HARDNESS_R, decimal? P_TENSION, string P_STARTBY, string P_DOFFBY, string P_FLAG,
                     string P_WARPMC, string P_REMARK, decimal? P_TENSION_IT, decimal? P_TENSION_TAKEUP, decimal? P_MC_COUNT_L, decimal? P_MC_COUNT_S,
                     DateTime? P_EDITDATE, string P_EDITBY, decimal? P_KEBA, decimal? P_TIGHTEND, decimal? P_MISSYARN, decimal? P_OTHER)
            {
                #region ListPacking

                WARPHEADNO = P_WARPHEADNO;
                WARPERLOT = P_WARPERLOT;
                BEAMNO = P_BEAMNO;
                SIDE = P_SIDE;
                STARTDATE = P_STARTDATE;
                ENDDATE = P_ENDDATE;
                LENGTH = P_LENGTH;
                SPEED = P_SPEED;
                HARDNESS_L = P_HARDNESS_L;
                HARDNESS_N = P_HARDNESS_N;
                HARDNESS_R = P_HARDNESS_R;
                TENSION = P_TENSION;
                STARTBY = P_STARTBY;
                DOFFBY = P_DOFFBY;
                FLAG = P_FLAG;
                WARPMC = P_WARPMC;

                REMARK = P_REMARK;
                TENSION_IT = P_TENSION_IT;
                TENSION_TAKEUP = P_TENSION_TAKEUP;
                MC_COUNT_L = P_MC_COUNT_L;
                MC_COUNT_S = P_MC_COUNT_S;

                EDITDATE = P_EDITDATE;
                EDITBY = P_EDITBY;
                KEBA = P_KEBA;
                TIGHTEND = P_TIGHTEND;
                MISSYARN = P_MISSYARN;
                OTHER = P_OTHER;

                #endregion
            }

            #region WARP_GETWARPERLOTBYHEADNO

            public string WARPHEADNO { get; set; }
            public string WARPERLOT { get; set; }
            public string BEAMNO { get; set; }
            public string SIDE { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public decimal? LENGTH { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? HARDNESS_L { get; set; }
            public decimal? HARDNESS_N { get; set; }
            public decimal? HARDNESS_R { get; set; }
            public decimal? TENSION { get; set; }
            public string STARTBY { get; set; }
            public string DOFFBY { get; set; }
            public string FLAG { get; set; }
            public string WARPMC { get; set; }
            public string REMARK { get; set; }
            public decimal? TENSION_IT { get; set; }
            public decimal? TENSION_TAKEUP { get; set; }
            public decimal? MC_COUNT_L { get; set; }
            public decimal? MC_COUNT_S { get; set; }

            public DateTime? EDITDATE { get; set; }
            public string EDITBY { get; set; }
            public decimal? KEBA { get; set; }
            public decimal? TIGHTEND { get; set; }
            public decimal? MISSYARN { get; set; }
            public decimal? OTHER { get; set; }

            #endregion
        }
        #endregion

        #region ListWARP_GETCREELSETUPDETAIL

        public class ListWARP_GETCREELSETUPDETAIL
        {
            public ListWARP_GETCREELSETUPDETAIL()
            {
                // default constructor
            }

            public ListWARP_GETCREELSETUPDETAIL(string P_PALLETNO
                , decimal? P_RECEIVECH, decimal? P_USEDCH, decimal? P_REJECTCH, decimal? P_PREJECT
                , string P_ITM_YARN, DateTime? P_RECEIVEDATE, decimal? P_PUSED)
            {
                #region ListPacking

                PALLETNO = P_PALLETNO;
                RECEIVECH = P_RECEIVECH;
                USEDCH = P_USEDCH;
                REJECTCH = P_REJECTCH;
                PREJECT = P_PREJECT;


                ITM_YARN = P_ITM_YARN;
                RECEIVEDATE = P_RECEIVEDATE;
                PUSED = P_PUSED;

                #endregion
            }

            #region WARP_GETCREELSETUPDETAIL

            public string PALLETNO { get; set; }
            public decimal? RECEIVECH { get; set; }
            public decimal? USEDCH { get; set; }
            public decimal? REJECTCH { get; set; }
            public decimal? PREJECT { get; set; }
            public string ITM_YARN { get; set; }
            public DateTime? RECEIVEDATE { get; set; }
            public decimal? PUSED { get; set; }

            #endregion
        }
        #endregion

        #region ListWARP_GETSPECBYCHOPNOANDMC

        public class ListWARP_GETSPECBYCHOPNOANDMC
        {
            public ListWARP_GETSPECBYCHOPNOANDMC()
            {
                // default constructor
            }

            public ListWARP_GETSPECBYCHOPNOANDMC(string P_CHOPNO, string P_ITM_YARN, decimal? P_WARPERENDS, decimal? P_MAXLENGTH, decimal? P_MINLENGTH, string P_WAXING, string P_COMBTYPE, string P_COMBPITCH,
                decimal? P_KEBAYARN, decimal? P_NOWARPBEAM, decimal? P_MAXHARDNESS, decimal? P_MINHARDNESS, string P_MCNO, decimal? P_SPEED, decimal? P_SPEED_MARGIN,
                decimal? P_YARN_TENSION, decimal? P_YARN_TENSION_MARGIN, decimal? P_WINDING_TENSION, decimal? P_WINDING_TENSION_MARGIN, decimal? P_NOCH)
            {
                #region ListPacking

                CHOPNO = P_CHOPNO;
                ITM_YARN = P_ITM_YARN;
                WARPERENDS = P_WARPERENDS;
                MAXLENGTH = P_MAXLENGTH;
                MINLENGTH = P_MINLENGTH;
                WAXING = P_WAXING;
                COMBTYPE = P_COMBTYPE;
                COMBPITCH = P_COMBPITCH;
                KEBAYARN = P_KEBAYARN;
                NOWARPBEAM = P_NOWARPBEAM;
                MAXHARDNESS = P_MAXHARDNESS;
                MINHARDNESS = P_MINHARDNESS;
                MCNO = P_MCNO;
                SPEED = P_SPEED;
                SPEED_MARGIN = P_SPEED_MARGIN;
                YARN_TENSION = P_YARN_TENSION;
                YARN_TENSION_MARGIN = P_YARN_TENSION_MARGIN;
                WINDING_TENSION = P_WINDING_TENSION;
                WINDING_TENSION = P_WINDING_TENSION;
                WINDING_TENSION_MARGIN = P_WINDING_TENSION_MARGIN;
                NOCH = P_NOCH;

                #endregion
            }

            #region WARP_GETSPECBYCHOPNOANDMC

            public string CHOPNO { get; set; }
            public string ITM_YARN { get; set; }
            public decimal? WARPERENDS { get; set; }
            public decimal? MAXLENGTH { get; set; }
            public decimal? MINLENGTH { get; set; }
            public string WAXING { get; set; }
            public string COMBTYPE { get; set; }
            public string COMBPITCH { get; set; }
            public decimal? KEBAYARN { get; set; }
            public decimal? NOWARPBEAM { get; set; }
            public decimal? MAXHARDNESS { get; set; }
            public decimal? MINHARDNESS { get; set; }
            public string MCNO { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? SPEED_MARGIN { get; set; }
            public decimal? YARN_TENSION { get; set; }
            public decimal? YARN_TENSION_MARGIN { get; set; }
            public decimal? WINDING_TENSION { get; set; }
            public decimal? WINDING_TENSION_MARGIN { get; set; }
            public decimal? NOCH { get; set; }

            #endregion
        }
        #endregion

        #region ListWARP_WARPLIST

        public class ListWARP_WARPLIST
        {
            public ListWARP_WARPLIST()
            {
                // default constructor
            }

            public ListWARP_WARPLIST(string P_WARPHEADNO, string P_WARPERLOT, string P_BEAMNO, string P_SIDE, DateTime? P_STARTDATE, DateTime? P_ENDDATE,
                decimal? P_LENGTH, decimal? P_SPEED, decimal? P_HARDNESS_L, decimal? P_HARDNESS_N, decimal? P_HARDNESS_R, decimal? P_TENSION,
                string P_STARTBY, string P_DOFFBY, string P_FLAG, string P_WARPMC, string P_REMARK,
                decimal? P_TENSION_IT, decimal? P_TENSION_TAKEUP, decimal? P_MC_COUNT_L, decimal? P_MC_COUNT_S,
                DateTime? P_EDITDATE, string P_EDITBY, string P_ITM_PREPARE, string P_ITM_YARN)
            {
                #region ListPacking

                WARPHEADNO = P_WARPHEADNO;
                WARPERLOT = P_WARPERLOT;
                BEAMNO = P_BEAMNO;
                SIDE = P_SIDE;
                STARTDATE = P_STARTDATE;
                ENDDATE = P_ENDDATE;
                LENGTH = P_LENGTH;
                SPEED = P_SPEED;
                HARDNESS_L = P_HARDNESS_L;
                HARDNESS_N = P_HARDNESS_N;
                HARDNESS_R = P_HARDNESS_N;
                TENSION = P_TENSION;
                STARTBY = P_STARTBY;
                DOFFBY = P_DOFFBY;
                FLAG = P_FLAG;
                WARPMC = P_WARPMC;
                REMARK = P_REMARK;
                TENSION_IT = P_TENSION_IT;
                TENSION_TAKEUP = P_TENSION_TAKEUP;
                MC_COUNT_L = P_MC_COUNT_L;
                MC_COUNT_S = P_MC_COUNT_S;
                EDITDATE = P_EDITDATE;
                EDITBY = P_EDITBY;
                ITM_PREPARE = P_ITM_PREPARE;
                ITM_YARN = P_ITM_YARN;

                #endregion
            }

            #region WARP_WARPLIST

            public string WARPHEADNO { get; set; }
            public string WARPERLOT { get; set; }
            public string BEAMNO { get; set; }
            public string SIDE { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public decimal? LENGTH { get; set; }
            public decimal? SPEED { get; set; }
            public decimal? HARDNESS_L { get; set; }
            public decimal? HARDNESS_N { get; set; }
            public decimal? HARDNESS_R { get; set; }
            public decimal? TENSION { get; set; }
            public string STARTBY { get; set; }
            public string DOFFBY { get; set; }
            public string FLAG { get; set; }
            public string WARPMC { get; set; }
            public string REMARK { get; set; }
            public decimal? TENSION_IT { get; set; }
            public decimal? TENSION_TAKEUP { get; set; }
            public decimal? MC_COUNT_L { get; set; }
            public decimal? MC_COUNT_S { get; set; }
            public DateTime? EDITDATE { get; set; }
            public string EDITBY { get; set; }
            public string ITM_PREPARE { get; set; }
            public string ITM_YARN { get; set; }


            #endregion
        }
        #endregion
    }
}
