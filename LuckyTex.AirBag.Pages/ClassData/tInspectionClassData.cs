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
    public class tInspectionClassData
    {
        #region tInspection ClassData
        private static tInspectionClassData _instance = null;

        public static tInspectionClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(tInspectionClassData))
                    {
                        _instance = new tInspectionClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListInspection

        public class ListInspection
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListInspection()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                //เปลี่ยน Type 39 เป็น 128
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                //BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE128;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListInspection()
            {
                // default constructor
            }

            public ListInspection(string inspectionLot
                , string ItemCode, string finishingLot, DateTime? startDate, DateTime? endDate,
             decimal? grossLength, decimal? netLength, string customerID, string producttypeID, string grade,
             decimal? grossweight, decimal? netweight, string peinspectionLot, string defectID, string remark, string attachID,
             string testRecordID, string inspectedBy, string mcno, string finishFlag, DateTime? suspendDate, string inspectionID,
             string retest, string preItemCode, string clearBy, string clearRemark, string suspendby, DateTime? startDate1,
             string productName, string mcName, string customerName, string customerType, string defectFileName, string partNo, decimal? grossNetLength, string loadingType,
             string shift_ID, string shift_Remark, decimal? confirmstartLength, string confirmSTDLength)
            {
                #region ListInspection
                INSPECTIONLOT = inspectionLot;

                //Barcode = barcode;
                //Image = image;
                ITEMCODE = ItemCode;
                FINISHINGLOT = finishingLot;
                STARTDATE = startDate;
                ENDDATE = endDate;
                GROSSLENGTH = grossLength;
                NETLENGTH = netLength;
                CUSTOMERID = customerID;
                PRODUCTTYPEID = producttypeID;
                GRADE = grade;
                GROSSWEIGHT = grossweight;
                NETWEIGHT = netweight;
                PEINSPECTIONLOT = peinspectionLot;
                DEFECTID = defectID;
                REMARK = remark;
                ATTACHID = attachID;
                TESTRECORDID = testRecordID;
                INSPECTEDBY = inspectedBy;
                MCNO = mcno;
                FINISHFLAG = finishFlag;
                SUSPENDDATE = suspendDate;
                INSPECTIONID = inspectionID;
                RETEST = retest;
                PREITEMCODE = preItemCode;
                CLEARBY = clearBy;
                CLEARREMARK = clearRemark;
                SUSPENDBY = suspendby;
                STARTDATE1 = startDate1;

                // เพิ่มใหม่
                PRODUCTNAME = productName;
                MCNAME = mcName;
                CUSTOMERNAME = customerName;
                CUSTOMERTYPE = customerType;
                DEFECTFILENAME = defectFileName;
                PARTNO = partNo;

                GrossNetLength = grossNetLength;

                LOADINGTYPE = loadingType;

                SHIFT_ID = shift_ID;
                SHIFT_REMARK = shift_Remark;
                CONFIRMSTARTLENGTH = confirmstartLength;
                CONFIRMSTDLENGTH = confirmSTDLength;

                #endregion
            }

            #region Recipe Head

            public string INSPECTIONLOT { get; set; }
            public string ITEMCODE { get; set; }
            public string FINISHINGLOT { get; set; }
            public DateTime? STARTDATE { get; set; }
            public DateTime? ENDDATE { get; set; }
            public decimal? GROSSLENGTH { get; set; }
            public decimal? NETLENGTH { get; set; }
            public string CUSTOMERID { get; set; }
            public string PRODUCTTYPEID { get; set; }
            public string GRADE { get; set; }
            public decimal? GROSSWEIGHT { get; set; }
            public decimal? NETWEIGHT { get; set; }
            public string PEINSPECTIONLOT { get; set; }
            public string DEFECTID { get; set; }
            public string REMARK { get; set; }
            public string ATTACHID { get; set; }
            public string TESTRECORDID { get; set; }
            public string INSPECTEDBY { get; set; }
            public string MCNO { get; set; }
            public string FINISHFLAG { get; set; }
            public DateTime? SUSPENDDATE { get; set; }
            public string INSPECTIONID { get; set; }
            public string RETEST { get; set; }
            public string PREITEMCODE { get; set; }
            public string CLEARBY { get; set; }
            public string CLEARREMARK { get; set; }
            public string SUSPENDBY { get; set; }
            public DateTime? STARTDATE1 { get; set; }

            // เพิ่มใหม่ 
            public string CUSTOMERTYPE { get; set; }
            public string CUSTOMERNAME { get; set; }
            public string LOADINGTYPE { get; set; }

            public string PRODUCTNAME { get; set; }
            public string MCNAME { get; set; }

            public string DEFECTFILENAME { get; set; }
            public string PARTNO { get; set; }

            public decimal? GrossNetLength { get; set; }

            //เพิ่ม 13/05/16
            public string SHIFT_ID { get; set; }
            public string SHIFT_REMARK { get; set; }

            //New 23/8/22
            public decimal? CONFIRMSTARTLENGTH { get; set; }
            public string CONFIRMSTDLENGTH { get; set; }

            public decimal? RESETSTARTLENGTH { get; set; }

            // For Barcode
            public byte[] INSLotImage
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

            public byte[] GROSSLENGTHImage
            {
                get
                {
                    byte[] results = null;
                    if (GROSSLENGTH.HasValue)
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.GROSSLENGTH.Value.ToString("#.0"),
                            300, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] GrossNetLengthImage
            {
                get
                {
                    byte[] results = null;
                    if (GrossNetLength.HasValue)
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.GrossNetLength.Value.ToString("#.0"),
                            300, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

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

        #region ListInstestreCordList

        public class ListInstestreCordList
        {
            public ListInstestreCordList()
            {
                // default constructor
            }

            public ListInstestreCordList(
             string stdLength, string actualLength, string densityW, string densityF,
             string widthAll, string widthPIN, string widthCoat, string triml, string trimr,
             string floppyL, string floppyR, string unwinderSet, string unwinderActual,
             string winderSet, string winderActual)
            {
                #region ListInstestreCordList

                STDLength = stdLength;
                ActualLength = actualLength;
                DensityW = densityW;
                DensityF = densityF;
                WidthAll = widthAll;
                WidthPin = widthPIN;
                WidthCoat = widthCoat;
                TrimL = triml;
                TrimR = trimr;
                FloppyL = floppyL;
                FloppyR = floppyR;
                UnwinderSet = unwinderSet;
                UnwinderActual = unwinderActual;
                WinderSet = winderSet;
                WinderActual = winderActual;

                #endregion
            }

            #region Recipe Head

            /// <summary>
            /// Gets or sets STD Length.
            /// </summary>
            public string STDLength { get; set; }
            /// <summary>
            /// Gets or sets Actual Length.
            /// </summary>
            public string ActualLength { get; set; }
            /// <summary>
            /// Gets or sets Density W.
            /// </summary>
            public string DensityW { get; set; }
            /// <summary>
            /// Gets or sets Density F.
            /// </summary>
            public string DensityF { get; set; }
            /// <summary>
            /// Gets or sets Width All.
            /// </summary>
            public string WidthAll { get; set; }
            /// <summary>
            /// Gets or sets Width Pin.
            /// </summary>
            public string WidthPin { get; set; }
            /// <summary>
            /// Gets or sets Width Coat.
            /// </summary>
            public string WidthCoat { get; set; }
            /// <summary>
            /// Gets or sets Trim L.
            /// </summary>
            public string TrimL { get; set; }
            /// <summary>
            /// Gets or sets Trim R.
            /// </summary>
            public string TrimR { get; set; }
            /// <summary>
            /// Gets or sets Floppy L.
            /// </summary>
            public string FloppyL { get; set; }
            /// <summary>
            /// Gets or sets Floppy R.
            /// </summary>
            public string FloppyR { get; set; }
            /// <summary>
            /// Gets or sets Unwinder Actual.
            /// </summary>
            public string UnwinderActual { get; set; }
            /// <summary>
            /// Gets or sets Unwinder Set.
            /// </summary>
            public string UnwinderSet { get; set; }
            /// <summary>
            /// Gets or sets Winder Actual.
            /// </summary>
            public string WinderActual { get; set; }
            /// <summary>
            /// Gets or sets Winder Set.
            /// </summary>
            public string WinderSet { get; set; }

            #endregion
        }
        #endregion

        #region ListInsdefectList

        public class ListInsdefectList
        {
            public ListInsdefectList()
            {
                // default constructor
            }

            public ListInsdefectList(
             string no, string length, string length2, string defectLength,
             string defectCode, string description, string position,decimal? defectPoint100)
            {
                #region ListInsdefectList

                No = no;
                Length = length;
                Length2 = length2;
                DefectLength = defectLength;
                DefectCode = defectCode;
                Description = description;
                Position = position;
                DEFECTPOINT100 = defectPoint100;

                #endregion
            }

            #region Public Properties

            /// <summary>
            /// Gets or sets running number (for record display).
            /// </summary>
            public string No { get; set; }
            /// <summary>
            /// Gets or sets defect length.
            /// </summary>
            public string Length { get; set; }

            /// <summary>
            /// Gets or sets defect length2.
            /// </summary>
            public string Length2 { get; set; }
            /// <summary>
            /// Gets or sets DefectLength.
            /// </summary>
            public string DefectLength { get; set; }

            /// <summary>
            /// Gets or sets defect code.
            /// </summary>
            public string DefectCode { get; set; }
            /// <summary>
            /// Gets or sets defect code description.
            /// </summary>
            public string Description { get; set; }
            /// <summary>
            /// Gets or sets defect position.
            /// </summary>
            public string Position { get; set; }

            public decimal? DEFECTPOINT100 { get; set; }

            #endregion
        }
        #endregion

        #region ListINS_ReportSumDefectList

        public class ListINS_ReportSumDefectList
        {
            public ListINS_ReportSumDefectList()
            {
                // default constructor
            }

            public ListINS_ReportSumDefectList(decimal? totalPoint
                , decimal? shortDefect, decimal? longDefect
                , decimal? comLongDefect,decimal? comShortDefect)
            {
                #region ListInsdefectList

                TotalPoint = totalPoint;
                ShortDefect = shortDefect;
                LongDefect = longDefect;
                ComLongDefect = comLongDefect;
                ComShortDefect = comShortDefect;
                #endregion
            }

            #region Public Properties

            /// <summary>
            /// Gets or sets TotalPoint
            /// </summary>
            public decimal? TotalPoint { get; set; }
            /// <summary>
            /// Gets or sets Short Defect.
            /// </summary>
            public decimal? ShortDefect { get; set; }
            /// <summary>
            /// Gets or sets Long Defect.
            /// </summary>
            public decimal? LongDefect { get; set; }
            /// <summary>
            /// Gets or sets COMLONGDEFECT.
            /// </summary>
            public decimal? ComLongDefect { get; set; }
            /// <summary>
            /// Gets or sets COMSHORTDEFECT.
            /// </summary>
            public decimal? ComShortDefect { get; set; }


            #endregion
        }
        #endregion
    }
}
