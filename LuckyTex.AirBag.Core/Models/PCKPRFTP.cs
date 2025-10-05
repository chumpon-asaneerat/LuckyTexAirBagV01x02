#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLib;

using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using LuckyTex.Services;
using System.Windows.Media;

using System.Windows.Media.Imaging;

#endregion

namespace LuckyTex.Models
{
    #region PCKPRFTPResult

    /// <summary>
    /// PCKPRFTP
    /// </summary>
    public class PCKPRFTPResult
    {
        #region Public Properties
        //FLAG
        public string ANNUL { get; set; }
        //DIVISION
        public string CDDIV { get; set; }
        //INV. TYPE
        public string INVTY { get; set; }
        //INV. NO.
        public string INVNO { get; set; }
        //ORDER NO.
        public string CDORD { get; set; }
        //RELEASE NO.
        public int? RELNO { get; set; }
        //CUSTOMER TYPE
        public string CUSCD { get; set; }
        //CUSTOMER NAME
        public string CUSNM { get; set; }
        //ARTICLE TYPE
        public string RECTY { get; set; }
        //ITEM CODE
        public string CDKE1 { get; set; }
        //ITEM CODE
        public string CDKE2 { get; set; }
        //CUSTOMER ITEM
        public string CSITM { get; set; }
        //PALLET NO.
        public string CDCON { get; set; }
        //ROLL NO.
        public string CDEL0 { get; set; }
        //GRADE
        public string GRADE { get; set; }
        //LENGTH
        public decimal? PIELN { get; set; }
        //NET WEIGHT
        public decimal? NETWH { get; set; }
        //GROSS WEIGHT
        public decimal? GRSWH { get; set; }
        //GROSS LENGTH
        public decimal? GRSLN { get; set; }
        //PALLET CODE
        public string PALSZ { get; set; }
        //DATE
        public string DTTRA { get; set; }
        //UPDATE TIME
        public string DTORA { get; set; }

        #endregion
    }

    #endregion

    #region PCKPRFTPD365Result

    /// <summary>
    /// PCKPRFTP
    /// </summary>
    public class PCKPRFTPD365Result
    {
        #region Public Properties
        //FLAG
        public string ANNUL { get; set; }
        //DIVISION
        public string CDDIV { get; set; }
        //INV. TYPE
        public string INVTY { get; set; }
        //INV. NO.
        public string INVNO { get; set; }
        //ORDER NO.
        public string CDORD { get; set; }
        //RELEASE NO.
        public int? RELNO { get; set; }
        //CUSTOMER TYPE
        public string CUSCD { get; set; }
        //CUSTOMER NAME
        public string CUSNM { get; set; }
        //ARTICLE TYPE
        public string RECTY { get; set; }
        //ITEM CODE
        public string CDKE1 { get; set; }
        //ITEM CODE
        public string CDKE2 { get; set; }
        //CUSTOMER ITEM
        public string CSITM { get; set; }
        //PALLET NO.
        public string CDCON { get; set; }
        //ROLL NO.
        public string CDEL0 { get; set; }
        //GRADE
        public string GRADE { get; set; }
        //LENGTH
        public decimal? PIELN { get; set; }
        //NET WEIGHT
        public decimal? NETWH { get; set; }
        //GROSS WEIGHT
        public decimal? GRSWH { get; set; }
        //GROSS LENGTH
        public decimal? GRSLN { get; set; }
        //PALLET CODE
        public string PALSZ { get; set; }
        //DATE
        public decimal? DTTRA { get; set; }
        //UPDATE TIME
        public decimal? DTORA { get; set; }
        //UPDATE TIME
        public string DTORASTR { get; set; }

        #endregion
    }

    #endregion

    #region ListPCKPRFTPData
    /// <summary>
    /// 
    /// </summary>
    public class ListPCKPRFTPData
    {
        public ListPCKPRFTPData()
        {
            // default constructor
        }

        public ListPCKPRFTPData(string annul, string cddiv, string invty
            , string invno, string cdord, decimal? relno, string cuscd, string cusnm, string recty, string cdke1, string cdke2
            , string csitm, string cdcon, string cdel0, string grade, decimal? pieln
            , decimal? netwh, decimal? grswh, decimal? grsln, string palsz, Decimal? dttra, Decimal? dtora
            , decimal? runno, decimal? as400No, decimal? cusNo, string insertby, DateTime? insertdate, string editby, DateTime? editdate, decimal? inuse, string scanby, DateTime? scandate, BitmapImage imgScanFlag)
        {
            ANNUL = annul;
            CDDIV = cddiv;
            INVTY = invty;
            INVNO = invno;
            CDORD = cdord;
            RELNO = relno;
            CUSCD = cuscd;
            CUSNM = cusnm;
            RECTY = recty;
            CDKE1 = cdke1;
            CDKE2 = cdke2;
            CSITM = csitm;
            CDCON = cdcon;
            CDEL0 = cdel0;
            GRADE = grade;
            PIELN = pieln;
            NETWH = netwh;
            GRSWH = grswh;
            GRSLN = grsln;
            PALSZ = palsz;
            DTTRA = dttra;
            DTORA = dtora;
            RUNNO = runno;
            AS400NO = as400No;
            CUSNO = cusNo;

            INSERTBY = insertby;
            INSERTDATE = insertdate;
            EDITBY = editby;
            EDITDATE = editdate;
            INUSE = inuse;
            SCANBY = scanby;
            SCANDATE = scandate;

            ImgScanFlag = imgScanFlag;
        }


        //FLAG
        public string ANNUL { get; set; }
        //DIVISION
        public string CDDIV { get; set; }
        //INV. TYPE
        public string INVTY { get; set; }
        //INV. NO.
        public string INVNO { get; set; }
        //ORDER NO.
        public string CDORD { get; set; }
        //RELEASE NO.
        public decimal? RELNO { get; set; }
        //CUSTOMER TYPE
        public string CUSCD { get; set; }
        //CUSTOMER NAME
        public string CUSNM { get; set; }
        //ARTICLE TYPE
        public string RECTY { get; set; }
        //ITEM CODE
        public string CDKE1 { get; set; }
        //ITEM CODE
        public string CDKE2 { get; set; }
        //CUSTOMER ITEM
        public string CSITM { get; set; }
        //PALLET NO.
        public string CDCON { get; set; }
        //ROLL NO.
        public string CDEL0 { get; set; }
        //GRADE
        public string GRADE { get; set; }
        //LENGTH
        public decimal? PIELN { get; set; }
        //NET WEIGHT
        public decimal? NETWH { get; set; }
        //GROSS WEIGHT
        public decimal? GRSWH { get; set; }
        //GROSS LENGTH
        public decimal? GRSLN { get; set; }
        //PALLET CODE
        public string PALSZ { get; set; }
        //DATE
        public Decimal? DTTRA { get; set; }
        //UPDATE TIME
        public Decimal? DTORA { get; set; }

        public decimal? RUNNO { get; set; }
        public decimal? AS400NO { get; set; }
        public decimal? CUSNO { get; set; }

        public string INSERTBY { get; set; }
        public DateTime? INSERTDATE { get; set; }
        public string EDITBY { get; set; }
        public DateTime? EDITDATE { get; set; }

        public decimal? INUSE { get; set; }

        public string SCANBY { get; set; }
        public DateTime? SCANDATE { get; set; }

        public BitmapImage ImgScanFlag { get; set; }

        public decimal? CHKNETWEIGHT { get; set; }
        public decimal? CHKGROSSWEIGHT { get; set; }
        public decimal? CHKNETLENGTH { get; set; }
        public decimal? CHKGROSSLENGTH { get; set; }

        public BitmapImage ImgChkNetWeight { get; set; }
        public BitmapImage ImgChkGrossWeight { get; set; }
        public BitmapImage ImgChkNetLength { get; set; }
        public BitmapImage ImgChkGrossLength { get; set; }
    }
    #endregion

    #region PackingListINVNOResult

    public class PackingListINVNOResult
    {
        #region Public Properties

        public string INVNO { get; set; }

        #endregion
    }

    #endregion

    #region PackingListResult

    public class PackingListResult
    {
        #region Public Properties

        public string FabricNo { get; set; }
        public string RollNo { get; set; }
        public decimal? Qty { get; set; }
        public decimal? NW { get; set; }
        public decimal? GW { get; set; }
        public decimal? CusRunNo { get; set; }

        #endregion
    }

    #endregion
}
