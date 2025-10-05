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
    /// ConfirmGradeInfo
    /// </summary>
    public class ConfirmGradeInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Grade.
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// Gets or sets is comfirm.
        /// </summary>
        public bool IsConfirm { get; set; }
        /// <summary>
        /// Gets or sets error message if not confirm.
        /// </summary>
        public string ErrMessage { get; set; }

        #endregion
    }
}
