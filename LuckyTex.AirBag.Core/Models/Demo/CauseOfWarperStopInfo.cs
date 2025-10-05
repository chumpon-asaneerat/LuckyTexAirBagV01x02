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
    /// CauseOfWarperStop Info
    /// </summary>
    public class CauseOfWarperStopInfo
    {
        #region Public Properties

       
        public string SpecificationType  { get; set; }
       
        public string Length { get; set; }
        
        public string Other { get; set; }

        public string Operator { get; set; }

        public string DisplayText
        {
            get
            {
                string msg = "SpecificationType : {0}" + Environment.NewLine +
                    "Length : {1}" + Environment.NewLine +
                    "Other : {2}" + Environment.NewLine +
                    "Operator : {3}";
                return msg.args(this.SpecificationType, this.Length, this.Other, this.Operator);
            }
        }

        #endregion
    }
}
