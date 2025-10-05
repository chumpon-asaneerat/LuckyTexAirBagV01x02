#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Data;
using System.Collections.ObjectModel;

using NLib;
using LuckyTex.Services;

#endregion

namespace LuckyTex.Models
{
    #region PositonBase

    /// <summary>
    /// ProcessBase class.
    /// </summary>
    [Serializable]
    public abstract class PositonBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets M/C Id (PK).
        /// </summary>
        [XmlAttribute]
        public string POSITIONLEVEL { get; set; }
        /// <summary>
        /// Gets or sets M/C Disaply Name.
        /// </summary>
        [XmlAttribute]
        public string POSITIONNAME { get; set; }

        #endregion
    }

    #endregion

    #region PositonItem

    /// <summary>
    /// The Inspection Process Item.
    /// </summary>
    [Serializable]
    public class PositonItem : PositonBase
    {
    }

    #endregion

}
