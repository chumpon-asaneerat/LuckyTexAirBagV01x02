#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#endregion

using NLib;
using LuckyTex.Services;
using System.Windows.Media;

namespace LuckyTex.Models
{

    #region ListTableNameData
    /// <summary>
    /// 
    /// </summary>
    public class ListTableNameData
    {
        public ListTableNameData()
        {
            // default constructor
        }

        public ListTableNameData(int? sheetID, string sheetDesc)
        {
            SheetID = sheetID;
            SheetDesc = sheetDesc;

        }

        public int? SheetID { get; set; }
        public string SheetDesc { get; set; }

    }
    #endregion

    #region ListExcelLoadAs400Data
    /// <summary>
    /// 
    /// </summary>
    public class ListExcelLoadAs400Data
    {
        public ListExcelLoadAs400Data()
        {
            // default constructor
        }

        public ListExcelLoadAs400Data(string truckNo, string desc, string descItem, string descType, string traceNo
            , decimal? cone, decimal? qty, string lot, string item, DateTime? recDt, string um)
        {
            TruckNo = truckNo;
            Desc = desc;
            DescItem = descItem;
            DescType = descType;
            TraceNo = traceNo;
            Cone = cone;
            Qty = qty;
            Lot = lot;
            Item = item;
            RecDt = recDt;
            Um = um;
        }

        public string TruckNo { get; set; }
        public string Desc { get; set; }
        public string DescItem { get; set; }
        public string DescType { get; set; }
        public string TraceNo { get; set; }

        public decimal? Cone { get; set; }
        public decimal? Qty { get; set; }
        public string Lot { get; set; }
        public string Item { get; set; }
        public DateTime? RecDt { get; set; }
        public string Um { get; set; }
    }
    #endregion
}
