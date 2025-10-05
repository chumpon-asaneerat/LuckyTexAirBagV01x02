#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLib;

#endregion

namespace LuckyTex.Models
{
    public class DefectListInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets No.
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// Gets or sets Length.
        /// </summary>
        public decimal Length { get; set; }
        /// <summary>
        /// Gets or sets Defect.
        /// </summary>
        public decimal Defect { get; set; }
        /// <summary>
        /// Gets or sets Position.
        /// </summary>
        public decimal Position { get; set; }
       
        public string DisplayText
        {
            get
            {
                string msg = "No : {0}" + Environment.NewLine +
                    "Length : {1}" + Environment.NewLine +
                    "Defect : {2}" + Environment.NewLine +
                    "Position : {3}";
                return msg.args(this.No.ToString(), this.Length.ToString(), this.Defect.ToString()
                    , this.Position.ToString() );
            }
        }

        #endregion
    }
}
