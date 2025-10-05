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
    /// InstGrade Info
    /// </summary>
    public class InstGradeInfo
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
        /// <summary>
        /// Gets display text.
        /// </summary>
        public string DisplayText
        {
            get
            {
                string msg = "User : {0}" + Environment.NewLine +
                    "Password : {1}" + Environment.NewLine +
                    "Remark : {2}";
                return msg.args(this.UserName, this.Password, this.Remark);
            }
        }

        #endregion
    }
}
