#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLib;

#endregion

namespace LuckyTex.Models
{
    public class InspectionRecordInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets STDLength.
        /// </summary>
        public decimal STDLength { get; set; }
        /// <summary>
        /// Gets or sets ActualLength.
        /// </summary>
        public decimal ActualLength { get; set; }
        /// <summary>
        /// Gets or sets DensityW.
        /// </summary>
        public decimal DensityW { get; set; }
        /// <summary>
        /// Gets or sets DensityF.
        /// </summary>
        public decimal DensityF { get; set; }
        /// <summary>
        /// Gets or sets WidthAll.
        /// </summary>
        public decimal WidthAll { get; set; }
        /// <summary>
        /// Gets or sets WidthPin.
        /// </summary>
        public decimal WidthPin { get; set; }
        /// <summary>
        /// Gets or sets WidthCoat.
        /// </summary>
        public decimal WidthCoat { get; set; }
        /// <summary>
        /// Gets or sets TrimL.
        /// </summary>
        public decimal TrimL { get; set; }
        /// <summary>
        /// Gets or sets TrimR.
        /// </summary>
        public decimal TrimR { get; set; }
        /// <summary>
        /// Gets or sets FloppyL.
        /// </summary>
        public decimal FloppyL { get; set; }
        /// <summary>
        /// Gets or sets FloppyR.
        /// </summary>
        public decimal FloppyR { get; set; }

        public string DisplayText
        {
            get
            {
                string msg = "STDLength : {0}" + Environment.NewLine +
                    "ActualLength : {1}" + Environment.NewLine +
                    "DensityW : {2}" + Environment.NewLine +
                    "DensityF : {3}" + Environment.NewLine +
                    "WidthAll : {4}" + Environment.NewLine +
                    "WidthPin : {5}" + Environment.NewLine +
                    "WidthCoat : {6}" + Environment.NewLine +
                    "TrimL : {7}" + Environment.NewLine +
                    "TrimR : {8}" + Environment.NewLine +
                    "FloppyL : {9}" + Environment.NewLine +
                    "FloppyR : {10}";
                return msg.args(this.STDLength.ToString(), this.ActualLength.ToString(), this.DensityW.ToString()
                    , this.DensityF.ToString(), this.WidthAll.ToString(), this.WidthPin.ToString()
                    , this.WidthCoat.ToString(), this.TrimL.ToString(), this.TrimR, this.FloppyL.ToString(), this.FloppyR.ToString());
            }
        }

        #endregion
    }
}
