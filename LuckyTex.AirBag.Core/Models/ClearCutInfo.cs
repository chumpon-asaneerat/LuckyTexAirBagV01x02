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
    /// ClearCut Info
    /// </summary>
    public class ClearCutInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets user name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets remark.
        /// </summary>
        public string Remark { get; set; }

        #endregion
    }
    /// <summary>
    /// ClearCut Result
    /// </summary>
    public class ClearCutResult
    {
        #region Public Properties

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Remark { get; set; }

        #endregion
    }
}
