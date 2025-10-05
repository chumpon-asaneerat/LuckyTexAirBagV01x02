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
    #region ProcessBase

    /// <summary>
    /// ProcessBase class.
    /// </summary>
    [Serializable]
    public abstract class ProcessBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets M/C Id (PK).
        /// </summary>
        [XmlAttribute]
        public string PROCESSID { get; set; }
        /// <summary>
        /// Gets or sets M/C Disaply Name.
        /// </summary>
        [XmlAttribute]
        public string PROCESSDESCRIPTION { get; set; }

        #endregion
    }

    #endregion

    #region ProcessItem

    /// <summary>
    /// The Inspection Process Item.
    /// </summary>
    [Serializable]
    public class ProcessItem : ProcessBase
    {
    }

    #endregion

}
