#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NLib;

#endregion

namespace LuckyTex.Models
{
    public class ReceiveDetailInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Packaging.
        /// </summary>
        public bool Packaging { get; set; }
        /// <summary>
        /// Gets or sets Clear.
        /// </summary>
        public bool Clear { get; set; }
        /// <summary>
        /// Gets or sets Tearing.
        /// </summary>
        public bool Tearing { get; set; }
        /// <summary>
        /// Gets or sets FallDown.
        /// </summary>
        public bool FallDown { get; set; }
        /// <summary>
        /// Gets or sets Certification.
        /// </summary>
        public bool Certification { get; set; }
        /// <summary>
        /// Gets or sets Invoice.
        /// </summary>
        public bool Invoice { get; set; }
        /// <summary>
        /// Gets or sets IdentifyArea.
        /// </summary>
        public bool IdentifyArea { get; set; }
        /// <summary>
        /// Gets or sets Certification.
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// Gets or sets Other.
        /// </summary>
        public string Other { get; set; }
        /// <summary>
        /// Gets or sets Action.
        /// </summary>
        public string Action { get; set; }

        public string DisplayText
        {
            get
            {
                string msg = "Packaging : {0}" + Environment.NewLine +
                    "Clear : {1}" + Environment.NewLine +
                    "Tearing : {2}"+ Environment.NewLine +
                    "FallDown : {3}" + Environment.NewLine +
                    "Certification : {4}"+ Environment.NewLine +
                    "Invoice : {5}"+ Environment.NewLine +
                    "IdentifyArea : {6}"+ Environment.NewLine +
                    "Amount : {7}" + Environment.NewLine +
                    "Other : {8}" + Environment.NewLine +
                    "Action : {9}";
                return msg.args(this.Packaging.ToString(), this.Clear.ToString(), this.Tearing.ToString()
                    , this.FallDown.ToString(), this.Certification.ToString(), this.Invoice.ToString()
                    , this.IdentifyArea.ToString(), this.Amount.ToString(), this.Other, this.Action.ToString() );
            }
        }

        #endregion
    }
}
