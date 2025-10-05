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
    /// StartNewInsLotInfo
    /// </summary>
    public class StartNewInsLotInfo
    {
        #region Public Properties

        /// <summary>
        ///
        /// </summary>
        public bool Yes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool No { get; set; }
        
        /// <summary>
        /// Gets display text.
        /// </summary>
        public string DisplayText
        {
            get
            {
                string msg = "Yes : {0}" + Environment.NewLine +
                    "No : {1}" ;
                return msg.args(this.Yes, this.No);
            }
        }

        #endregion
    }
}
