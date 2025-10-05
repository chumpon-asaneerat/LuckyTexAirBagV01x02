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
    #region WP

    #region ListD365_WP_OULData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_WP_OULData
    {
        public ListD365_WP_OULData()
        {
            // default constructor
        }

        public ListD365_WP_OULData(decimal? _LINENO, DateTime? _OUTPUTDATE, string _ITEMID, decimal? _QTY, string _UNIT, decimal? _GROSSLENGTH, decimal? _NETLENGTH, decimal? _GROSSWEIGHT,
                                    decimal? _NETWEIGHT, string _PALLETNO, string _GRADE, string _LOADINGTYPE, string _SERIALID, decimal? _FINISH, string _MOVEMENTTRANS, string _WAREHOUSE, string _LOCATION)
        {
            LINENO = _LINENO;
            OUTPUTDATE = _OUTPUTDATE;
            ITEMID = _ITEMID;
            QTY = _QTY;
            UNIT = _UNIT;
            GROSSLENGTH = _GROSSLENGTH;
            NETLENGTH = _NETLENGTH;
            GROSSWEIGHT = _GROSSWEIGHT;
            NETWEIGHT = _NETWEIGHT;
            PALLETNO = _PALLETNO;
            GRADE = _GRADE;
            LOADINGTYPE = _LOADINGTYPE;
            SERIALID = _SERIALID;
            FINISH = _FINISH;
            MOVEMENTTRANS = _MOVEMENTTRANS;

            WAREHOUSE = _WAREHOUSE;
            LOCATION = _LOCATION;
        }

        public decimal? LINENO { get; set; }
        public DateTime? OUTPUTDATE { get; set; }
        public string ITEMID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public decimal? GROSSLENGTH { get; set; }
        public decimal? NETLENGTH { get; set; }
        public decimal? GROSSWEIGHT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string PALLETNO { get; set; }
        public string GRADE { get; set; }
        public string LOADINGTYPE { get; set; }
        public string SERIALID { get; set; }
        public decimal? FINISH { get; set; }
        public string MOVEMENTTRANS { get; set; }

        public string WAREHOUSE { get; set; }
        public string LOCATION { get; set; }
    }
    #endregion

    #region D365_WP_OUHData

    public class D365_WP_OUHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_WP_OPLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_WP_OPLData
    {
        public ListD365_WP_OPLData()
        {
            // default constructor
        }

        public ListD365_WP_OPLData(decimal? _LINENO,decimal? _PROCQTY,string _OPRNO,string _OPRID,string _MACHINENO,DateTime? _STARTDATETIME,DateTime? _ENDDATETIME)
        {
            LINENO = _LINENO;
            PROCQTY = _PROCQTY;
            OPRNO = _OPRNO;
            OPRID = _OPRID;
            MACHINENO = _MACHINENO;
            STARTDATETIME = _STARTDATETIME;
            ENDDATETIME = _ENDDATETIME;
        }

        public decimal? LINENO { get; set; }
        public decimal? PROCQTY { get; set; }
        public string OPRNO { get; set; }
        public string OPRID { get; set; }
        public string MACHINENO { get; set; }
        public DateTime? STARTDATETIME { get; set; }
        public DateTime? ENDDATETIME { get; set; }
    }
    #endregion

    #region D365_WP_OPHData

    public class D365_WP_OPHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_WP_ISLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_WP_ISLData
    {
        public ListD365_WP_ISLData()
        {
            // default constructor
        }

        public ListD365_WP_ISLData(decimal? _LINENO,DateTime? _ISSUEDATE,string _ITEMID, string _STYLEID, decimal? _QTY,string _UNIT,string _SERIALID)
        {
            LINENO = _LINENO;
            ISSUEDATE = _ISSUEDATE;
            ITEMID = _ITEMID;
            STYLEID = _STYLEID;
            QTY = _QTY;
            UNIT = _UNIT;
            SERIALID = _SERIALID;
        }

        public decimal? LINENO { get; set; }
        public DateTime? ISSUEDATE { get; set; }
        public string ITEMID { get; set; }
        public string STYLEID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string SERIALID { get; set; }
    }
    #endregion

    #region  D365_WP_ISHData

    public class D365_WP_ISHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_WP_BPOData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_WP_BPOData
    {
        public ListD365_WP_BPOData()
        {
            // default constructor
        }

        public ListD365_WP_BPOData(decimal? _PRODID,string _LOTNO,string _ITEMID,string _LOADINGTYPE,decimal? _QTY, string _UNIT,string _OPERATION)
        {
            PRODID = _PRODID;
            LOTNO = _LOTNO;
            ITEMID = _ITEMID;
            LOADINGTYPE = _LOADINGTYPE;
            QTY = _QTY;
            UNIT = _UNIT;
            OPERATION = _OPERATION;
        }

        public decimal? PRODID { get; set; }
        public string LOTNO { get; set; }
        public string ITEMID { get; set; }
        public string LOADINGTYPE { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string OPERATION { get; set; }
    }
    #endregion

    #endregion

    #region PK

    #region ListD365_PK_TOTALHEADERData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_PK_TOTALHEADERData
    {
        public ListD365_PK_TOTALHEADERData()
        {
            // default constructor
        }

        public ListD365_PK_TOTALHEADERData(string _PALLETNO, string _ITEMCODE, string _LOADINGTYPE)
        {
            PALLETNO = _PALLETNO;
            ITEMCODE = _ITEMCODE;
            LOADINGTYPE = _LOADINGTYPE;
        }


        public string PALLETNO { get; set; }
        public string ITEMCODE { get; set; }
        public string LOADINGTYPE { get; set; }

    }
    #endregion

    #region ListD365_PK_OUL_CData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_PK_OUL_CData
    {
        public ListD365_PK_OUL_CData()
        {
            // default constructor
        }

        public ListD365_PK_OUL_CData(decimal? _LINENO, DateTime? _OUTPUTDATE, string _ITEMID, decimal? _QTY, string _UNIT, decimal? _GROSSLENGTH, decimal? _NETLENGTH, decimal? _GROSSWEIGHT,
                                    decimal? _NETWEIGHT, string _PALLETNO, string _GRADE, string _LOADINGTYPE, string _SERIALID, decimal? _FINISH, string _MOVEMENTTRANS, string _WAREHOUSE, string _LOCATION)
        {
            LINENO = _LINENO;
            OUTPUTDATE = _OUTPUTDATE;
            ITEMID = _ITEMID;
            QTY = _QTY;
            UNIT = _UNIT;
            GROSSLENGTH = _GROSSLENGTH;
            NETLENGTH = _NETLENGTH;
            GROSSWEIGHT = _GROSSWEIGHT;
            NETWEIGHT = _NETWEIGHT;
            PALLETNO = _PALLETNO;
            GRADE = _GRADE;
            LOADINGTYPE = _LOADINGTYPE;
            SERIALID = _SERIALID;
            FINISH = _FINISH;
            MOVEMENTTRANS = _MOVEMENTTRANS;

            WAREHOUSE = _WAREHOUSE;
            LOCATION = _LOCATION;
        }

        public decimal? LINENO { get; set; }
        public DateTime? OUTPUTDATE { get; set; }
        public string ITEMID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public decimal? GROSSLENGTH { get; set; }
        public decimal? NETLENGTH { get; set; }
        public decimal? GROSSWEIGHT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string PALLETNO { get; set; }
        public string GRADE { get; set; }
        public string LOADINGTYPE { get; set; }
        public string SERIALID { get; set; }
        public decimal? FINISH { get; set; }
        public string MOVEMENTTRANS { get; set; }

        public string WAREHOUSE { get; set; }
        public string LOCATION { get; set; }
    }
    #endregion

    #region ListD365_PK_OULData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_PK_OULData
    {
        public ListD365_PK_OULData()
        {
            // default constructor
        }

        public ListD365_PK_OULData(decimal? _LINENO, DateTime? _OUTPUTDATE, string _ITEMID, decimal? _QTY, string _UNIT, decimal? _GROSSLENGTH, decimal? _NETLENGTH, decimal? _GROSSWEIGHT,
                                    decimal? _NETWEIGHT, string _PALLETNO, string _GRADE, string _LOADINGTYPE, string _SERIALID, decimal? _FINISH, string _MOVEMENTTRANS, string _WAREHOUSE, string _LOCATION)
        {
            LINENO = _LINENO;
            OUTPUTDATE = _OUTPUTDATE;
            ITEMID = _ITEMID;
            QTY = _QTY;
            UNIT = _UNIT;
            GROSSLENGTH = _GROSSLENGTH;
            NETLENGTH = _NETLENGTH;
            GROSSWEIGHT = _GROSSWEIGHT;
            NETWEIGHT = _NETWEIGHT;
            PALLETNO = _PALLETNO;
            GRADE = _GRADE;
            LOADINGTYPE = _LOADINGTYPE;
            SERIALID = _SERIALID;
            FINISH = _FINISH;
            MOVEMENTTRANS = _MOVEMENTTRANS;

            WAREHOUSE = _WAREHOUSE;
            LOCATION = _LOCATION;
        }

        public decimal? LINENO { get; set; }
        public DateTime? OUTPUTDATE { get; set; }
        public string ITEMID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public decimal? GROSSLENGTH { get; set; }
        public decimal? NETLENGTH { get; set; }
        public decimal? GROSSWEIGHT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string PALLETNO { get; set; }
        public string GRADE { get; set; }
        public string LOADINGTYPE { get; set; }
        public string SERIALID { get; set; }
        public decimal? FINISH { get; set; }
        public string MOVEMENTTRANS { get; set; }

        public string WAREHOUSE { get; set; }
        public string LOCATION { get; set; }
    }
    #endregion

    #region D365_PK_OUH_CData

    public class D365_PK_OUH_CData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public string ITEMID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region D365_PK_OUHData

    public class D365_PK_OUHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_PK_ISL_CData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_PK_ISL_CData
    {
        public ListD365_PK_ISL_CData()
        {
            // default constructor
        }

        public ListD365_PK_ISL_CData(decimal? _LINENO, DateTime? _ISSUEDATE, string _ITEMID, string _STYLEID, decimal? _QTY, string _UNIT, string _SERIALID)
        {
            LINENO = _LINENO;
            ISSUEDATE = _ISSUEDATE;
            ITEMID = _ITEMID;
            STYLEID = _STYLEID;
            QTY = _QTY;
            UNIT = _UNIT;
            SERIALID = _SERIALID;
        }

        public decimal? LINENO { get; set; }
        public DateTime? ISSUEDATE { get; set; }
        public string ITEMID { get; set; }
        public string STYLEID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string SERIALID { get; set; }
    }
    #endregion

    #region ListD365_PK_ISLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_PK_ISLData
    {
        public ListD365_PK_ISLData()
        {
            // default constructor
        }

        public ListD365_PK_ISLData(decimal? _LINENO, DateTime? _ISSUEDATE, string _ITEMID, string _STYLEID, decimal? _QTY, string _UNIT, string _SERIALID)
        {
            LINENO = _LINENO;
            ISSUEDATE = _ISSUEDATE;
            ITEMID = _ITEMID;
            STYLEID = _STYLEID;
            QTY = _QTY;
            UNIT = _UNIT;
            SERIALID = _SERIALID;
        }

        public decimal? LINENO { get; set; }
        public DateTime? ISSUEDATE { get; set; }
        public string ITEMID { get; set; }
        public string STYLEID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string SERIALID { get; set; }
    }
    #endregion

    #region  D365_PK_ISHData

    public class D365_PK_ISHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_PK_BPO_CData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_PK_BPO_CData
    {
        public ListD365_PK_BPO_CData()
        {
            // default constructor
        }

        public ListD365_PK_BPO_CData(decimal? _PRODID, string _LOTNO, string _ITEMID, string _LOADINGTYPE, decimal? _QTY, string _UNIT, string _OPERATION)
        {
            PRODID = _PRODID;
            LOTNO = _LOTNO;
            ITEMID = _ITEMID;
            LOADINGTYPE = _LOADINGTYPE;
            QTY = _QTY;
            UNIT = _UNIT;
            OPERATION = _OPERATION;
        }

        public decimal? PRODID { get; set; }
        public string LOTNO { get; set; }
        public string ITEMID { get; set; }
        public string LOADINGTYPE { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string OPERATION { get; set; }
    }
    #endregion

    #region ListD365_PK_BPOData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_PK_BPOData
    {
        public ListD365_PK_BPOData()
        {
            // default constructor
        }

        public ListD365_PK_BPOData(decimal? _PRODID, string _LOTNO, string _ITEMID, string _LOADINGTYPE, decimal? _QTY, string _UNIT, string _OPERATION)
        {
            PRODID = _PRODID;
            LOTNO = _LOTNO;
            ITEMID = _ITEMID;
            LOADINGTYPE = _LOADINGTYPE;
            QTY = _QTY;
            UNIT = _UNIT;
            OPERATION = _OPERATION;
        }

        public decimal? PRODID { get; set; }
        public string LOTNO { get; set; }
        public string ITEMID { get; set; }
        public string LOADINGTYPE { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string OPERATION { get; set; }
    }
    #endregion

    #endregion

    #region IN

    #region ListD365_IN_OUL_AUTOData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_IN_OUL_AUTOData
    {
        public ListD365_IN_OUL_AUTOData()
        {
            // default constructor
        }

        public ListD365_IN_OUL_AUTOData(decimal? _LINENO, DateTime? _OUTPUTDATE, string _ITEMID, decimal? _QTY, string _UNIT, decimal? _GROSSLENGTH, decimal? _NETLENGTH, decimal? _GROSSWEIGHT,
                                    decimal? _NETWEIGHT, string _PALLETNO, string _GRADE, string _LOADINGTYPE, string _SERIALID, decimal? _FINISH, string _MOVEMENTTRANS, string _WAREHOUSE, string _LOCATION)
        {
            LINENO = _LINENO;
            OUTPUTDATE = _OUTPUTDATE;
            ITEMID = _ITEMID;
            QTY = _QTY;
            UNIT = _UNIT;
            GROSSLENGTH = _GROSSLENGTH;
            NETLENGTH = _NETLENGTH;
            GROSSWEIGHT = _GROSSWEIGHT;
            NETWEIGHT = _NETWEIGHT;
            PALLETNO = _PALLETNO;
            GRADE = _GRADE;
            LOADINGTYPE = _LOADINGTYPE;
            SERIALID = _SERIALID;
            FINISH = _FINISH;
            MOVEMENTTRANS = _MOVEMENTTRANS;

            WAREHOUSE = _WAREHOUSE;
            LOCATION = _LOCATION;
        }

        public decimal? LINENO { get; set; }
        public DateTime? OUTPUTDATE { get; set; }
        public string ITEMID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public decimal? GROSSLENGTH { get; set; }
        public decimal? NETLENGTH { get; set; }
        public decimal? GROSSWEIGHT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string PALLETNO { get; set; }
        public string GRADE { get; set; }
        public string LOADINGTYPE { get; set; }
        public string SERIALID { get; set; }
        public decimal? FINISH { get; set; }
        public string MOVEMENTTRANS { get; set; }

        public string WAREHOUSE { get; set; }
        public string LOCATION { get; set; }
    }
    #endregion

    #region ListD365_IN_OULData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_IN_OULData
    {
        public ListD365_IN_OULData()
        {
            // default constructor
        }

        public ListD365_IN_OULData(decimal? _LINENO, DateTime? _OUTPUTDATE, string _ITEMID, decimal? _QTY, string _UNIT, decimal? _GROSSLENGTH, decimal? _NETLENGTH, decimal? _GROSSWEIGHT,
                                    decimal? _NETWEIGHT, string _PALLETNO, string _GRADE, string _LOADINGTYPE, string _SERIALID, decimal? _FINISH, string _MOVEMENTTRANS, string _WAREHOUSE, string _LOCATION)
        {
            LINENO = _LINENO;
            OUTPUTDATE = _OUTPUTDATE;
            ITEMID = _ITEMID;
            QTY = _QTY;
            UNIT = _UNIT;
            GROSSLENGTH = _GROSSLENGTH;
            NETLENGTH = _NETLENGTH;
            GROSSWEIGHT = _GROSSWEIGHT;
            NETWEIGHT = _NETWEIGHT;
            PALLETNO = _PALLETNO;
            GRADE = _GRADE;
            LOADINGTYPE = _LOADINGTYPE;
            SERIALID = _SERIALID;
            FINISH = _FINISH;
            MOVEMENTTRANS = _MOVEMENTTRANS;

            WAREHOUSE = _WAREHOUSE;
            LOCATION = _LOCATION;
        }

        public decimal? LINENO { get; set; }
        public DateTime? OUTPUTDATE { get; set; }
        public string ITEMID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public decimal? GROSSLENGTH { get; set; }
        public decimal? NETLENGTH { get; set; }
        public decimal? GROSSWEIGHT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string PALLETNO { get; set; }
        public string GRADE { get; set; }
        public string LOADINGTYPE { get; set; }
        public string SERIALID { get; set; }
        public decimal? FINISH { get; set; }
        public string MOVEMENTTRANS { get; set; }

        public string WAREHOUSE { get; set; }
        public string LOCATION { get; set; }
    }
    #endregion

    #region D365_IN_OUHData

    public class D365_IN_OUHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_IN_OPLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_IN_OPLData
    {
        public ListD365_IN_OPLData()
        {
            // default constructor
        }

        public ListD365_IN_OPLData(decimal? _LINENO, decimal? _PROCQTY, string _OPRNO, string _OPRID, string _MACHINENO, DateTime? _STARTDATETIME, DateTime? _ENDDATETIME)
        {
            LINENO = _LINENO;
            PROCQTY = _PROCQTY;
            OPRNO = _OPRNO;
            OPRID = _OPRID;
            MACHINENO = _MACHINENO;
            STARTDATETIME = _STARTDATETIME;
            ENDDATETIME = _ENDDATETIME;
        }

        public decimal? LINENO { get; set; }
        public decimal? PROCQTY { get; set; }
        public string OPRNO { get; set; }
        public string OPRID { get; set; }
        public string MACHINENO { get; set; }
        public DateTime? STARTDATETIME { get; set; }
        public DateTime? ENDDATETIME { get; set; }
    }
    #endregion

    #region D365_IN_OPHData

    public class D365_IN_OPHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_IN_ISLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_IN_ISLData
    {
        public ListD365_IN_ISLData()
        {
            // default constructor
        }

        public ListD365_IN_ISLData(decimal? _LINENO, DateTime? _ISSUEDATE, string _ITEMID, string _STYLEID, decimal? _QTY, string _UNIT, string _SERIALID)
        {
            LINENO = _LINENO;
            ISSUEDATE = _ISSUEDATE;
            ITEMID = _ITEMID;
            STYLEID = _STYLEID;
            QTY = _QTY;
            UNIT = _UNIT;
            SERIALID = _SERIALID;
        }

        public decimal? LINENO { get; set; }
        public DateTime? ISSUEDATE { get; set; }
        public string ITEMID { get; set; }
        public string STYLEID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string SERIALID { get; set; }
    }
    #endregion

    #region  D365_IN_ISHData

    public class D365_IN_ISHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_IN_BPOData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_IN_BPOData
    {
        public ListD365_IN_BPOData()
        {
            // default constructor
        }

        public ListD365_IN_BPOData(decimal? _PRODID, string _LOTNO, string _ITEMID, string _LOADINGTYPE, decimal? _QTY, string _UNIT, string _OPERATION)
        {
            PRODID = _PRODID;
            LOTNO = _LOTNO;
            ITEMID = _ITEMID;
            LOADINGTYPE = _LOADINGTYPE;
            QTY = _QTY;
            UNIT = _UNIT;
            OPERATION = _OPERATION;
        }

        public decimal? PRODID { get; set; }
        public string LOTNO { get; set; }
        public string ITEMID { get; set; }
        public string LOADINGTYPE { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string OPERATION { get; set; }
    }
    #endregion

    #endregion

    #region GR

    #region ListD365_GR_OULData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_GR_OULData
    {
        public ListD365_GR_OULData()
        {
            // default constructor
        }

        public ListD365_GR_OULData(decimal? _LINENO, DateTime? _OUTPUTDATE, string _ITEMID, decimal? _QTY, string _UNIT, decimal? _GROSSLENGTH, decimal? _NETLENGTH, decimal? _GROSSWEIGHT,
                                    decimal? _NETWEIGHT, string _PALLETNO, string _GRADE, string _LOADINGTYPE, string _SERIALID, decimal? _FINISH, string _MOVEMENTTRANS, string _WAREHOUSE, string _LOCATION)
        {
            LINENO = _LINENO;
            OUTPUTDATE = _OUTPUTDATE;
            ITEMID = _ITEMID;
            QTY = _QTY;
            UNIT = _UNIT;
            GROSSLENGTH = _GROSSLENGTH;
            NETLENGTH = _NETLENGTH;
            GROSSWEIGHT = _GROSSWEIGHT;
            NETWEIGHT = _NETWEIGHT;
            PALLETNO = _PALLETNO;
            GRADE = _GRADE;
            LOADINGTYPE = _LOADINGTYPE;
            SERIALID = _SERIALID;
            FINISH = _FINISH;
            MOVEMENTTRANS = _MOVEMENTTRANS;

            WAREHOUSE = _WAREHOUSE;
            LOCATION = _LOCATION;
        }

        public decimal? LINENO { get; set; }
        public DateTime? OUTPUTDATE { get; set; }
        public string ITEMID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public decimal? GROSSLENGTH { get; set; }
        public decimal? NETLENGTH { get; set; }
        public decimal? GROSSWEIGHT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string PALLETNO { get; set; }
        public string GRADE { get; set; }
        public string LOADINGTYPE { get; set; }
        public string SERIALID { get; set; }
        public decimal? FINISH { get; set; }
        public string MOVEMENTTRANS { get; set; }

        public string WAREHOUSE { get; set; }
        public string LOCATION { get; set; }
    }
    #endregion

    #region D365_GR_OUHData

    public class D365_GR_OUHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_GR_OPLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_GR_OPLData
    {
        public ListD365_GR_OPLData()
        {
            // default constructor
        }

        public ListD365_GR_OPLData(decimal? _LINENO, decimal? _PROCQTY, string _OPRNO, string _OPRID, string _MACHINENO, DateTime? _STARTDATETIME, DateTime? _ENDDATETIME)
        {
            LINENO = _LINENO;
            PROCQTY = _PROCQTY;
            OPRNO = _OPRNO;
            OPRID = _OPRID;
            MACHINENO = _MACHINENO;
            STARTDATETIME = _STARTDATETIME;
            ENDDATETIME = _ENDDATETIME;
        }

        public decimal? LINENO { get; set; }
        public decimal? PROCQTY { get; set; }
        public string OPRNO { get; set; }
        public string OPRID { get; set; }
        public string MACHINENO { get; set; }
        public DateTime? STARTDATETIME { get; set; }
        public DateTime? ENDDATETIME { get; set; }
    }
    #endregion

    #region D365_GR_OPHData

    public class D365_GR_OPHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_GR_ISLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_GR_ISLData
    {
        public ListD365_GR_ISLData()
        {
            // default constructor
        }

        public ListD365_GR_ISLData(decimal? _LINENO, DateTime? _ISSUEDATE, string _ITEMID, string _STYLEID, decimal? _QTY, string _UNIT, string _SERIALID)
        {
            LINENO = _LINENO;
            ISSUEDATE = _ISSUEDATE;
            ITEMID = _ITEMID;
            STYLEID = _STYLEID;
            QTY = _QTY;
            UNIT = _UNIT;
            SERIALID = _SERIALID;
        }

        public decimal? LINENO { get; set; }
        public DateTime? ISSUEDATE { get; set; }
        public string ITEMID { get; set; }
        public string STYLEID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string SERIALID { get; set; }
    }
    #endregion

    #region  D365_GR_ISHData

    public class D365_GR_ISHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_GR_BPOData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_GR_BPOData
    {
        public ListD365_GR_BPOData()
        {
            // default constructor
        }

        public ListD365_GR_BPOData(decimal? _PRODID, string _LOTNO, string _ITEMID, string _LOADINGTYPE, decimal? _QTY, string _UNIT, string _OPERATION)
        {
            PRODID = _PRODID;
            LOTNO = _LOTNO;
            ITEMID = _ITEMID;
            LOADINGTYPE = _LOADINGTYPE;
            QTY = _QTY;
            UNIT = _UNIT;
            OPERATION = _OPERATION;
        }

        public decimal? PRODID { get; set; }
        public string LOTNO { get; set; }
        public string ITEMID { get; set; }
        public string LOADINGTYPE { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string OPERATION { get; set; }
    }
    #endregion

    #endregion

    #region FN

    #region ListD365_FN_OULData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_FN_OULData
    {
        public ListD365_FN_OULData()
        {
            // default constructor
        }

        public ListD365_FN_OULData(decimal? _LINENO, DateTime? _OUTPUTDATE, string _ITEMID, decimal? _QTY, string _UNIT, decimal? _GROSSLENGTH, decimal? _NETLENGTH, decimal? _GROSSWEIGHT,
                                    decimal? _NETWEIGHT, string _PALLETNO, string _GRADE, string _LOADINGTYPE, string _SERIALID, decimal? _FINISH, string _MOVEMENTTRANS,string _WAREHOUSE, string _LOCATION)
        {
            LINENO = _LINENO;
            OUTPUTDATE = _OUTPUTDATE;
            ITEMID = _ITEMID;
            QTY = _QTY;
            UNIT = _UNIT;
            GROSSLENGTH = _GROSSLENGTH;
            NETLENGTH = _NETLENGTH;
            GROSSWEIGHT = _GROSSWEIGHT;
            NETWEIGHT = _NETWEIGHT;
            PALLETNO = _PALLETNO;
            GRADE = _GRADE;
            LOADINGTYPE = _LOADINGTYPE;
            SERIALID = _SERIALID;
            FINISH = _FINISH;
            MOVEMENTTRANS = _MOVEMENTTRANS;

            WAREHOUSE = _WAREHOUSE;
            LOCATION = _LOCATION;
        }

        public decimal? LINENO { get; set; }
        public DateTime? OUTPUTDATE { get; set; }
        public string ITEMID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public decimal? GROSSLENGTH { get; set; }
        public decimal? NETLENGTH { get; set; }
        public decimal? GROSSWEIGHT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string PALLETNO { get; set; }
        public string GRADE { get; set; }
        public string LOADINGTYPE { get; set; }
        public string SERIALID { get; set; }
        public decimal? FINISH { get; set; }
        public string MOVEMENTTRANS { get; set; }

        public string WAREHOUSE { get; set; }
        public string LOCATION { get; set; }
    }
    #endregion

    #region D365_FN_OUHData

    public class D365_FN_OUHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_FN_OPLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_FN_OPLData
    {
        public ListD365_FN_OPLData()
        {
            // default constructor
        }

        public ListD365_FN_OPLData(decimal? _LINENO, decimal? _PROCQTY, string _OPRNO, string _OPRID, string _MACHINENO, DateTime? _STARTDATETIME, DateTime? _ENDDATETIME)
        {
            LINENO = _LINENO;
            PROCQTY = _PROCQTY;
            OPRNO = _OPRNO;
            OPRID = _OPRID;
            MACHINENO = _MACHINENO;
            STARTDATETIME = _STARTDATETIME;
            ENDDATETIME = _ENDDATETIME;
        }

        public decimal? LINENO { get; set; }
        public decimal? PROCQTY { get; set; }
        public string OPRNO { get; set; }
        public string OPRID { get; set; }
        public string MACHINENO { get; set; }
        public DateTime? STARTDATETIME { get; set; }
        public DateTime? ENDDATETIME { get; set; }
    }
    #endregion

    #region D365_FN_OPHData

    public class D365_FN_OPHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_FN_ISLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_FN_ISLData
    {
        public ListD365_FN_ISLData()
        {
            // default constructor
        }

        public ListD365_FN_ISLData(decimal? _LINENO, DateTime? _ISSUEDATE, string _ITEMID, string _STYLEID, decimal? _QTY, string _UNIT, string _SERIALID)
        {
            LINENO = _LINENO;
            ISSUEDATE = _ISSUEDATE;
            ITEMID = _ITEMID;
            STYLEID = _STYLEID;
            QTY = _QTY;
            UNIT = _UNIT;
            SERIALID = _SERIALID;
        }

        public decimal? LINENO { get; set; }
        public DateTime? ISSUEDATE { get; set; }
        public string ITEMID { get; set; }
        public string STYLEID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string SERIALID { get; set; }
    }
    #endregion

    #region  D365_FN_ISHData

    public class D365_FN_ISHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_FN_BPOData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_FN_BPOData
    {
        public ListD365_FN_BPOData()
        {
            // default constructor
        }

        public ListD365_FN_BPOData(decimal? _PRODID, string _LOTNO, string _ITEMID, string _LOADINGTYPE, decimal? _QTY, string _UNIT, string _OPERATION)
        {
            PRODID = _PRODID;
            LOTNO = _LOTNO;
            ITEMID = _ITEMID;
            LOADINGTYPE = _LOADINGTYPE;
            QTY = _QTY;
            UNIT = _UNIT;
            OPERATION = _OPERATION;
        }

        public decimal? PRODID { get; set; }
        public string LOTNO { get; set; }
        public string ITEMID { get; set; }
        public string LOADINGTYPE { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string OPERATION { get; set; }
    }
    #endregion

    #endregion

    #region DT

    #region ListD365_DT_OULData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_DT_OULData
    {
        public ListD365_DT_OULData()
        {
            // default constructor
        }

        public ListD365_DT_OULData(decimal? _LINENO, DateTime? _OUTPUTDATE, string _ITEMID, decimal? _QTY, string _UNIT, decimal? _GROSSLENGTH, decimal? _NETLENGTH, decimal? _GROSSWEIGHT,
                                    decimal? _NETWEIGHT, string _PALLETNO, string _GRADE, string _LOADINGTYPE, string _SERIALID, decimal? _FINISH, string _MOVEMENTTRANS, string _WAREHOUSE, string _LOCATION)
        {
            LINENO = _LINENO;
            OUTPUTDATE = _OUTPUTDATE;
            ITEMID = _ITEMID;
            QTY = _QTY;
            UNIT = _UNIT;
            GROSSLENGTH = _GROSSLENGTH;
            NETLENGTH = _NETLENGTH;
            GROSSWEIGHT = _GROSSWEIGHT;
            NETWEIGHT = _NETWEIGHT;
            PALLETNO = _PALLETNO;
            GRADE = _GRADE;
            LOADINGTYPE = _LOADINGTYPE;
            SERIALID = _SERIALID;
            FINISH = _FINISH;
            MOVEMENTTRANS = _MOVEMENTTRANS;

            WAREHOUSE = _WAREHOUSE;
            LOCATION = _LOCATION;
        }

        public decimal? LINENO { get; set; }
        public DateTime? OUTPUTDATE { get; set; }
        public string ITEMID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public decimal? GROSSLENGTH { get; set; }
        public decimal? NETLENGTH { get; set; }
        public decimal? GROSSWEIGHT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string PALLETNO { get; set; }
        public string GRADE { get; set; }
        public string LOADINGTYPE { get; set; }
        public string SERIALID { get; set; }
        public decimal? FINISH { get; set; }
        public string MOVEMENTTRANS { get; set; }

        public string WAREHOUSE { get; set; }
        public string LOCATION { get; set; }
    }
    #endregion

    #region D365_DT_OUHData

    public class D365_DT_OUHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_DT_OPLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_DT_OPLData
    {
        public ListD365_DT_OPLData()
        {
            // default constructor
        }

        public ListD365_DT_OPLData(decimal? _LINENO, decimal? _PROCQTY, string _OPRNO, string _OPRID, string _MACHINENO, DateTime? _STARTDATETIME, DateTime? _ENDDATETIME)
        {
            LINENO = _LINENO;
            PROCQTY = _PROCQTY;
            OPRNO = _OPRNO;
            OPRID = _OPRID;
            MACHINENO = _MACHINENO;
            STARTDATETIME = _STARTDATETIME;
            ENDDATETIME = _ENDDATETIME;
        }

        public decimal? LINENO { get; set; }
        public decimal? PROCQTY { get; set; }
        public string OPRNO { get; set; }
        public string OPRID { get; set; }
        public string MACHINENO { get; set; }
        public DateTime? STARTDATETIME { get; set; }
        public DateTime? ENDDATETIME { get; set; }
    }
    #endregion

    #region D365_DT_OPHData

    public class D365_DT_OPHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_DT_ISLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_DT_ISLData
    {
        public ListD365_DT_ISLData()
        {
            // default constructor
        }

        public ListD365_DT_ISLData(decimal? _LINENO, DateTime? _ISSUEDATE, string _ITEMID, string _STYLEID, decimal? _QTY, string _UNIT, string _SERIALID)
        {
            LINENO = _LINENO;
            ISSUEDATE = _ISSUEDATE;
            ITEMID = _ITEMID;
            STYLEID = _STYLEID;
            QTY = _QTY;
            UNIT = _UNIT;
            SERIALID = _SERIALID;
        }

        public decimal? LINENO { get; set; }
        public DateTime? ISSUEDATE { get; set; }
        public string ITEMID { get; set; }
        public string STYLEID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string SERIALID { get; set; }
    }
    #endregion

    #region  D365_DT_ISHData

    public class D365_DT_ISHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_DT_BPOData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_DT_BPOData
    {
        public ListD365_DT_BPOData()
        {
            // default constructor
        }

        public ListD365_DT_BPOData(decimal? _PRODID, string _LOTNO, string _ITEMID, string _LOADINGTYPE, decimal? _QTY, string _UNIT, string _OPERATION)
        {
            PRODID = _PRODID;
            LOTNO = _LOTNO;
            ITEMID = _ITEMID;
            LOADINGTYPE = _LOADINGTYPE;
            QTY = _QTY;
            UNIT = _UNIT;
            OPERATION = _OPERATION;
        }

        public decimal? PRODID { get; set; }
        public string LOTNO { get; set; }
        public string ITEMID { get; set; }
        public string LOADINGTYPE { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string OPERATION { get; set; }
    }
    #endregion

    #endregion

    #region CP

    #region ListD365_CP_OULData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_CP_OULData
    {
        public ListD365_CP_OULData()
        {
            // default constructor
        }

        public ListD365_CP_OULData(decimal? _LINENO, DateTime? _OUTPUTDATE, string _ITEMID, decimal? _QTY, string _UNIT, decimal? _GROSSLENGTH, decimal? _NETLENGTH, decimal? _GROSSWEIGHT,
                                    decimal? _NETWEIGHT, string _PALLETNO, string _GRADE, string _LOADINGTYPE, string _SERIALID, decimal? _FINISH, string _MOVEMENTTRANS, string _WAREHOUSE, string _LOCATION)
        {
            LINENO = _LINENO;
            OUTPUTDATE = _OUTPUTDATE;
            ITEMID = _ITEMID;
            QTY = _QTY;
            UNIT = _UNIT;
            GROSSLENGTH = _GROSSLENGTH;
            NETLENGTH = _NETLENGTH;
            GROSSWEIGHT = _GROSSWEIGHT;
            NETWEIGHT = _NETWEIGHT;
            PALLETNO = _PALLETNO;
            GRADE = _GRADE;
            LOADINGTYPE = _LOADINGTYPE;
            SERIALID = _SERIALID;
            FINISH = _FINISH;
            MOVEMENTTRANS = _MOVEMENTTRANS;

            WAREHOUSE = _WAREHOUSE;
            LOCATION = _LOCATION;
        }

        public decimal? LINENO { get; set; }
        public DateTime? OUTPUTDATE { get; set; }
        public string ITEMID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public decimal? GROSSLENGTH { get; set; }
        public decimal? NETLENGTH { get; set; }
        public decimal? GROSSWEIGHT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string PALLETNO { get; set; }
        public string GRADE { get; set; }
        public string LOADINGTYPE { get; set; }
        public string SERIALID { get; set; }
        public decimal? FINISH { get; set; }
        public string MOVEMENTTRANS { get; set; }

        public string WAREHOUSE { get; set; }
        public string LOCATION { get; set; }
    }
    #endregion

    #region D365_CP_OUHData

    public class D365_CP_OUHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_CP_OPLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_CP_OPLData
    {
        public ListD365_CP_OPLData()
        {
            // default constructor
        }

        public ListD365_CP_OPLData(decimal? _LINENO, decimal? _PROCQTY, string _OPRNO, string _OPRID, string _MACHINENO, DateTime? _STARTDATETIME, DateTime? _ENDDATETIME)
        {
            LINENO = _LINENO;
            PROCQTY = _PROCQTY;
            OPRNO = _OPRNO;
            OPRID = _OPRID;
            MACHINENO = _MACHINENO;
            STARTDATETIME = _STARTDATETIME;
            ENDDATETIME = _ENDDATETIME;
        }

        public decimal? LINENO { get; set; }
        public decimal? PROCQTY { get; set; }
        public string OPRNO { get; set; }
        public string OPRID { get; set; }
        public string MACHINENO { get; set; }
        public DateTime? STARTDATETIME { get; set; }
        public DateTime? ENDDATETIME { get; set; }
    }
    #endregion

    #region D365_CP_OPHData

    public class D365_CP_OPHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_CP_ISLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_CP_ISLData
    {
        public ListD365_CP_ISLData()
        {
            // default constructor
        }

        public ListD365_CP_ISLData(decimal? _LINENO, DateTime? _ISSUEDATE, string _ITEMID, string _STYLEID, decimal? _QTY, string _UNIT, string _SERIALID)
        {
            LINENO = _LINENO;
            ISSUEDATE = _ISSUEDATE;
            ITEMID = _ITEMID;
            STYLEID = _STYLEID;
            QTY = _QTY;
            UNIT = _UNIT;
            SERIALID = _SERIALID;
        }

        public decimal? LINENO { get; set; }
        public DateTime? ISSUEDATE { get; set; }
        public string ITEMID { get; set; }
        public string STYLEID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string SERIALID { get; set; }
    }
    #endregion

    #region  D365_CP_ISHData

    public class D365_CP_ISHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_CP_BPOData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_CP_BPOData
    {
        public ListD365_CP_BPOData()
        {
            // default constructor
        }

        public ListD365_CP_BPOData(decimal? _PRODID, string _LOTNO, string _ITEMID, string _LOADINGTYPE, decimal? _QTY, string _UNIT, string _OPERATION)
        {
            PRODID = _PRODID;
            LOTNO = _LOTNO;
            ITEMID = _ITEMID;
            LOADINGTYPE = _LOADINGTYPE;
            QTY = _QTY;
            UNIT = _UNIT;
            OPERATION = _OPERATION;
        }

        public decimal? PRODID { get; set; }
        public string LOTNO { get; set; }
        public string ITEMID { get; set; }
        public string LOADINGTYPE { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string OPERATION { get; set; }
    }
    #endregion

    #endregion

    #region BM

    #region ListD365_BM_OULData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_BM_OULData
    {
        public ListD365_BM_OULData()
        {
            // default constructor
        }

        public ListD365_BM_OULData(decimal? _LINENO, DateTime? _OUTPUTDATE, string _ITEMID, decimal? _QTY, string _UNIT, decimal? _GROSSLENGTH, decimal? _NETLENGTH, decimal? _GROSSWEIGHT,
                                    decimal? _NETWEIGHT, string _PALLETNO, string _GRADE, string _LOADINGTYPE, string _SERIALID, decimal? _FINISH, string _MOVEMENTTRANS, string _WAREHOUSE, string _LOCATION)
        {
            LINENO = _LINENO;
            OUTPUTDATE = _OUTPUTDATE;
            ITEMID = _ITEMID;
            QTY = _QTY;
            UNIT = _UNIT;
            GROSSLENGTH = _GROSSLENGTH;
            NETLENGTH = _NETLENGTH;
            GROSSWEIGHT = _GROSSWEIGHT;
            NETWEIGHT = _NETWEIGHT;
            PALLETNO = _PALLETNO;
            GRADE = _GRADE;
            LOADINGTYPE = _LOADINGTYPE;
            SERIALID = _SERIALID;
            FINISH = _FINISH;
            MOVEMENTTRANS = _MOVEMENTTRANS;

            WAREHOUSE = _WAREHOUSE;
            LOCATION = _LOCATION;
        }

        public decimal? LINENO { get; set; }
        public DateTime? OUTPUTDATE { get; set; }
        public string ITEMID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public decimal? GROSSLENGTH { get; set; }
        public decimal? NETLENGTH { get; set; }
        public decimal? GROSSWEIGHT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string PALLETNO { get; set; }
        public string GRADE { get; set; }
        public string LOADINGTYPE { get; set; }
        public string SERIALID { get; set; }
        public decimal? FINISH { get; set; }
        public string MOVEMENTTRANS { get; set; }

        public string WAREHOUSE { get; set; }
        public string LOCATION { get; set; }
    }
    #endregion

    #region D365_BM_OUHData

    public class D365_BM_OUHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_BM_OPLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_BM_OPLData
    {
        public ListD365_BM_OPLData()
        {
            // default constructor
        }

        public ListD365_BM_OPLData(decimal? _LINENO, decimal? _PROCQTY, string _OPRNO, string _OPRID, string _MACHINENO, DateTime? _STARTDATETIME, DateTime? _ENDDATETIME)
        {
            LINENO = _LINENO;
            PROCQTY = _PROCQTY;
            OPRNO = _OPRNO;
            OPRID = _OPRID;
            MACHINENO = _MACHINENO;
            STARTDATETIME = _STARTDATETIME;
            ENDDATETIME = _ENDDATETIME;
        }

        public decimal? LINENO { get; set; }
        public decimal? PROCQTY { get; set; }
        public string OPRNO { get; set; }
        public string OPRID { get; set; }
        public string MACHINENO { get; set; }
        public DateTime? STARTDATETIME { get; set; }
        public DateTime? ENDDATETIME { get; set; }
    }
    #endregion

    #region D365_BM_OPHData

    public class D365_BM_OPHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_BM_ISLData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_BM_ISLData
    {
        public ListD365_BM_ISLData()
        {
            // default constructor
        }

        public ListD365_BM_ISLData(decimal? _LINENO, DateTime? _ISSUEDATE, string _ITEMID, string _STYLEID, decimal? _QTY, string _UNIT, string _SERIALID)
        {
            LINENO = _LINENO;
            ISSUEDATE = _ISSUEDATE;
            ITEMID = _ITEMID;
            STYLEID = _STYLEID;
            QTY = _QTY;
            UNIT = _UNIT;
            SERIALID = _SERIALID;
        }

        public decimal? LINENO { get; set; }
        public DateTime? ISSUEDATE { get; set; }
        public string ITEMID { get; set; }
        public string STYLEID { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string SERIALID { get; set; }
    }
    #endregion

    #region  D365_BM_ISHData

    public class D365_BM_ISHData
    {
        #region Public Properties

        public decimal? HEADERID { get; set; }
        public decimal? TOTALRECORD { get; set; }

        #endregion
    }

    #endregion

    #region ListD365_BM_BPOData
    /// <summary>
    /// 
    /// </summary>
    public class ListD365_BM_BPOData
    {
        public ListD365_BM_BPOData()
        {
            // default constructor
        }

        public ListD365_BM_BPOData(decimal? _PRODID, string _LOTNO, string _ITEMID, string _LOADINGTYPE, decimal? _QTY, string _UNIT, string _OPERATION)
        {
            PRODID = _PRODID;
            LOTNO = _LOTNO;
            ITEMID = _ITEMID;
            LOADINGTYPE = _LOADINGTYPE;
            QTY = _QTY;
            UNIT = _UNIT;
            OPERATION = _OPERATION;
        }

        public decimal? PRODID { get; set; }
        public string LOTNO { get; set; }
        public string ITEMID { get; set; }
        public string LOADINGTYPE { get; set; }
        public decimal? QTY { get; set; }
        public string UNIT { get; set; }
        public string OPERATION { get; set; }
    }
    #endregion

    #endregion
}
