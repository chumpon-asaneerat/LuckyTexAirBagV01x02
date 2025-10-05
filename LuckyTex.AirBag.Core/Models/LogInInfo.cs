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
    /// Log In Info
    /// </summary>
    public class LogInInfo
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

        // เพิ่มใหม่เพื่อใช้กับ Finish
        /// <summary>
        /// Gets or sets Position.
        /// </summary>
        public string Position { get; set; }

        #endregion
    }
    /// <summary>
    /// Log In Result
    /// </summary>
    public class LogInResult
    {
        #region Public Properties

        public string OperatorId { get; set; }
        public string ProcessId { get; set; }
        public string PositionLevel { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                string title = (string.IsNullOrWhiteSpace(this.Title)) ? 
                    string.Empty :this.Title.Trim();
                string fName = (string.IsNullOrWhiteSpace(this.FirstName)) ? 
                    string.Empty :this.FirstName.Trim();
                string lName = (string.IsNullOrWhiteSpace(this.LastName)) ? 
                    string.Empty :this.LastName.Trim();

                return string.Format("{0} {1} {2}", title, fName, lName).Trim();
            }
        }

        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool Deleted { get; set; }

        // เพิ่มใหม่เพื่อใช้กับ Finish
        public string Position { get; set; }
        #endregion
    }
}
