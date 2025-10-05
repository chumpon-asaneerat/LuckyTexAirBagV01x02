#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#endregion

using NLib;
using LuckyTex.Services;
using System.Windows.Media;

namespace LuckyTex.Models
{
    #region Coating Information

    /// <summary>
    /// Coating Information
    /// </summary>
    public class CoatingInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Coating Lot No.
        /// </summary>
        public string CoatingLotNo { get; set; }
        /// <summary>
        /// Gets or sets Item Code (or Grey Code).
        /// </summary>
        public string ItemCode { get; set; }


        #endregion
    }

    #endregion
}
