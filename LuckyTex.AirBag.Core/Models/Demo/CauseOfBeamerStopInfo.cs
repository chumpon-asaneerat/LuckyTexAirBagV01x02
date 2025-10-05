#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLib;

#endregion

namespace LuckyTex.Models
{
    /// <summary>
    /// CauseOfBeamerStop Info
    /// </summary>
    public class CauseOfBeamerStopInfo
    {
        #region Public Properties

       
        public string SpecificationType  { get; set; }
       
        public string Length { get; set; }

        public string Operator { get; set; }

        public string DisplayText
        {
            get
            {
                string msg = "SpecificationType : {0}" + Environment.NewLine +
                    "Length : {1}" + Environment.NewLine +
                    "Operator : {2}";
                return msg.args(this.SpecificationType, this.Length, this.Operator);
            }
        }

        #endregion
    }
}
