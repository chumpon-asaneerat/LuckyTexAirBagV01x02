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
    #region MachineBase
    
    /// <summary>
    /// MachineBase class.
    /// </summary>
    [Serializable]
    public abstract class MachineBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets M/C No.
        /// </summary>
        [XmlAttribute]
        public int MCNo { get; set; }
        /// <summary>
        /// Gets or sets M/C Id (PK).
        /// </summary>
        [XmlAttribute]
        public string MCId { get; set; }
        /// <summary>
        /// Gets or sets M/C Disaply Name.
        /// </summary>
        [XmlAttribute]
        public string DisplayName { get; set; }

        #endregion
    }

    #endregion

    #region InspectionMCItem

    /// <summary>
    /// The Inspection M/C Item.
    /// </summary>
    [Serializable]
    public class InspectionMCItem : MachineBase
    {
    }

    #endregion

    #region FinishingMCItem

    /// <summary>
    /// The Finishing M/C Item.
    /// </summary>
    [Serializable]
    public class FinishingMCItem : MachineBase
    {
    }

    #endregion

    #region CutPrintMCItem

    /// <summary>
    /// The CutPrint M/C Item.
    /// </summary>
    [Serializable]
    public class CutPrintMCItem : MachineBase
    {
    }

    #endregion

    #region PackingMCItem

    /// <summary>
    /// The Packing M/C Item.
    /// </summary>
    [Serializable]
    public class PackingMCItem : MachineBase
    {
    }

    #endregion

    #region WarpingMCItem

    /// <summary>
    /// The Warping M/C Item.
    /// </summary>
    [Serializable]
    public class WarpingMCItem : MachineBase
    {
    }

    #endregion

    #region BeamingMCItem

    /// <summary>
    /// The Warping M/C Item.
    /// </summary>
    [Serializable]
    public class BeamingMCItem : MachineBase
    {
    }

    #endregion
}
