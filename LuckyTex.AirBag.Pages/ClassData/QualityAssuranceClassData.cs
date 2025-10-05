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
    public class QualityAssuranceClassData
    {
        #region QualityAssurance ClassData
        private static QualityAssuranceClassData _instance = null;

        public static QualityAssuranceClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(QualityAssuranceClassData))
                    {
                        _instance = new QualityAssuranceClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region ListQA_SEARCHCHECKINGDATA

        public class ListQA_SEARCHCHECKINGDATA
        {
            private static BarcodeLib.Barcode BarcodeGenerator = null;

            static ListQA_SEARCHCHECKINGDATA()
            {
                BarcodeGenerator = new BarcodeLib.Barcode();
                BarcodeGenerator.EncodedType = BarcodeLib.TYPE.CODE39;
                BarcodeGenerator.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                BarcodeGenerator.IncludeLabel = false;
                //BarcodeGenerator.Height = 100;
                //BarcodeGenerator.Width = 500;
            }

            public ListQA_SEARCHCHECKINGDATA()
            {
                // default constructor
            }

            public ListQA_SEARCHCHECKINGDATA(int? _RowNo,string _CUSTOMERID, 
                string _LAB_ITMCODE, string _LAB_LOT, string _LAB_BATCHNO,
                string _INS_ITMCODE, string _INS_LOT, string _INS_BATCHNO,
                string _CUS_CODE, string _CHECK_RESULT, DateTime? _CHECKDATE, string _CHECKEDBY,string _CUSTOMERNAME,
                string _DELETEFLAG, string _DELETEBY, DateTime? _DELETEDATE, string _SHIFT, string _REMARK)
            {
                #region ListQA_SEARCHCHECKINGDATA

                RowNo = _RowNo;
                CUSTOMERID = _CUSTOMERID;
                LAB_ITMCODE = _LAB_ITMCODE;
                LAB_LOT = _LAB_LOT;
                LAB_BATCHNO = _LAB_BATCHNO;
                INS_ITMCODE = _INS_ITMCODE;
                INS_LOT = _INS_LOT;
                INS_BATCHNO = _INS_BATCHNO;
                CUS_CODE = _CUS_CODE;
                CHECK_RESULT = _CHECK_RESULT;
                CHECKDATE = _CHECKDATE;
                CHECKEDBY = _CHECKEDBY;
                CUSTOMERNAME = _CUSTOMERNAME;
                DELETEFLAG = _DELETEFLAG;
                DELETEBY = _DELETEBY;
                DELETEDATE = _DELETEDATE;
                SHIFT = _SHIFT;
                REMARK = _REMARK;

                #endregion
            }

            #region QA_SEARCHCHECKINGDATA

            public int? RowNo { get; set; }
            public string CUSTOMERID { get; set; }

            public string LAB_ITMCODE { get; set; }
            public string LAB_LOT { get; set; }
            public string LAB_BATCHNO { get; set; }

            public string INS_ITMCODE { get; set; }
            public string INS_LOT { get; set; }
            public string INS_BATCHNO { get; set; }

            public string CUS_CODE { get; set; }
            public string CHECK_RESULT { get; set; }
            public DateTime? CHECKDATE { get; set; }
            public string CHECKEDBY { get; set; }
            public string CUSTOMERNAME { get; set; }

            public string DELETEFLAG { get; set; }
            public string DELETEBY { get; set; }
            public DateTime? DELETEDATE { get; set; }
            public string SHIFT { get; set; }
            public string REMARK { get; set; }
            
            // For Barcode
            public byte[] ITEMCODEImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.LAB_ITMCODE))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.LAB_ITMCODE,
                            500, 100);

                        results = NLib.Utils.ImageUtils.GetImage(img);
                    }
                    return results;
                }
                set { }
            }

            public byte[] ITEMCODEINSImage
            {
                get
                {
                    byte[] results = null;
                    if (!string.IsNullOrWhiteSpace(this.INS_ITMCODE))
                    {
                        System.Drawing.Image img = BarcodeGenerator.Encode(
                            BarcodeGenerator.EncodedType, this.INS_ITMCODE,
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
