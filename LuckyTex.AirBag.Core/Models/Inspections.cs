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
    #region Enum(s)
    
    /// <summary>
    /// Inspection Session State
    /// </summary>
    public enum InspectionSessionState : byte
    {
        /// <summary>
        /// No finishing data and not in another state.
        /// </summary>
        None,
        /// <summary>
        /// Has finishing data and not in another state. Wait for start by user.
        /// </summary>
        Idle,
        /// <summary>
        /// In Started state. Wait for Stop or Next.
        /// </summary>
        Started,
        /// <summary>
        /// In Long Defect entry state. This occur when press start long defect entry and wait for user
        /// press or enter end long defect.
        /// </summary>
        LongDefect
    }
    /// <summary>
    /// Inspection Types.
    /// </summary>
    public enum InspectionTypes : byte
    {
        /// <summary>
        /// Mass Production.
        /// </summary>
        Mass = 1,
        /// <summary>
        /// Test.
        /// </summary>
        Test = 2
    }
    /// <summary>
    /// Re Adjust Mode
    /// </summary>
    public enum ReAdjustMode : byte
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Re-Adjust Same Item.
        /// </summary>
        Same = 1,
        /// <summary>
        /// Re-Adjust Diff Item.
        /// </summary>
        Diff = 2
    }
    /// <summary>
    /// Re Process Mode
    /// </summary>
    public enum ReProcessMode : byte
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Re Process Mode.
        /// </summary>
        ReProcess = 1
    }

    #endregion

    #region InspectionConsts
    
    /// <summary>
    /// Inspection Consts
    /// </summary>
    public static class InspectionConsts
    {
        public static SolidColorBrush ABackground = new SolidColorBrush(Colors.LimeGreen);
        public static SolidColorBrush BBackground = new SolidColorBrush(Colors.Yellow);
        public static SolidColorBrush CBackground = new SolidColorBrush(Colors.Red);
        public static SolidColorBrush NBackground = new SolidColorBrush(Colors.White);

        public static SolidColorBrush BlackBlush = new SolidColorBrush(Colors.Black);
        public static SolidColorBrush WhiteBlush = new SolidColorBrush(Colors.White);
    }

    #endregion

    #region Inspection Defects (classes)

    /// <summary>
    /// Inspection Defect Code.
    /// </summary>
    public class InspectionDefectCode
    {
        #region Public Properties

        public string DefectCode { get; set; }
        public string ProcessCode { get; set; }
        public string DesciptionEN { get; set; }
        public string DesciptionTH { get; set; }

        #endregion
    }
    /// <summary>
    /// Inspection Defect Item
    /// </summary>
    public class InspectionDefectItem
    {
        #region Public Properties

        public string DefectID { get; set; }
        /// <summary>
        /// Gets or sets running number (for record display).
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// Gets or sets defect length.
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// Gets or sets defect length2.
        /// </summary>
        public string Length2 { get; set; }
        /// <summary>
        /// Gets or sets DefectLength.
        /// </summary>
        public string DefectLength { get; set; }

        /// <summary>
        /// Gets or sets defect code.
        /// </summary>
        public string DefectCode { get; set; }
        /// <summary>
        /// Gets or sets defect code description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets defect position.
        /// </summary>
        public string Position { get; set; }

        public System.String deleteBY { get; set; }
        public System.String deleteREMARK { get; set; }

        public decimal? DEFECTPOINT100 { get; set; }

        #endregion
    }

    #endregion

    #region Inspection Tests (classes)

    /// <summary>
    /// The inspection test information class.
    /// </summary>
    [Serializable]
    public class TestInfo
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TestInfo() : base()
        {
            this.Enabled = false;
            this.Value = new decimal?();
        }

        #endregion

        #region Public Proeprties

        /// <summary>
        /// Gets or sets Is enabled test.
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public decimal? Value { get; set; }

        /// <summary>
        /// เพิ่มเพื่อใส่ค่าที่ไม่ใช้ตัวเลข
        /// </summary>
        public string strValue { get; set; }
        #endregion
    }
    /// <summary>
    /// Inspection Test Density Class.
    /// </summary>
    [Serializable]
    public class Densities
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Densities()
        {
            this.W = new TestInfo();
            this.F = new TestInfo();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if instance is null auto create default instance.
        /// </summary>
        public void CheckInstances()
        {
            if (null == this.W)
                this.W = new TestInfo();
            if (null == this.F)
                this.F = new TestInfo();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets is W Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo W { get; set; }
        /// <summary>
        /// Gets or sets is F Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo F { get; set; }

        #endregion
    }
    /// <summary>
    /// Inspection Test Width Class.
    /// </summary>
    [Serializable]
    public class Widths
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Widths()
        {
            this.All = new TestInfo();
            this.Pin = new TestInfo();
            this.Coat = new TestInfo();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if instance is null auto create default instance.
        /// </summary>
        public void CheckInstances()
        {
            if (null == this.All)
                this.All = new TestInfo();
            if (null == this.Pin)
                this.Pin = new TestInfo();
            if (null == this.Coat)
                this.Coat = new TestInfo();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets is All Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo All { get; set; }
        /// <summary>
        /// Gets or sets is Pin Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo Pin { get; set; }
        /// <summary>
        /// Gets or sets is Coat Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo Coat { get; set; }

        #endregion
    }
    /// <summary>
    /// Inspection Test Trim Class.
    /// </summary>
    [Serializable]
    public class Trims
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Trims()
        {
            this.L = new TestInfo();
            this.R = new TestInfo();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if instance is null auto create default instance.
        /// </summary>
        public void CheckInstances()
        {
            if (null == this.L)
                this.L = new TestInfo();
            if (null == this.R)
                this.R = new TestInfo();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets is L Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo L { get; set; }
        /// <summary>
        /// Gets or sets is R Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo R { get; set; }

        #endregion
    }
    /// <summary>
    /// Inspection Test Floppy Class.
    /// </summary>
    [Serializable]
    public class Floppies
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Floppies()
        {
            this.L = new TestInfo();
            this.R = new TestInfo();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if instance is null auto create default instance.
        /// </summary>
        public void CheckInstances()
        {
            if (null == this.L)
                this.L = new TestInfo();
            if (null == this.R)
                this.R = new TestInfo();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets is L Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo L { get; set; }
        /// <summary>
        /// Gets or sets is R Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo R { get; set; }

        #endregion
    }
    /// <summary>
    /// Inspection Test Hardness Class.
    /// </summary>
    [Serializable]
    public class Hardnesses
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Hardnesses()
        {
            this.L = new TestInfo();
            this.C = new TestInfo();
            this.R = new TestInfo();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if instance is null auto create default instance.
        /// </summary>
        public void CheckInstances()
        {
            if (null == this.L)
                this.L = new TestInfo();
            if (null == this.C)
                this.C = new TestInfo();
            if (null == this.R)
                this.R = new TestInfo();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets is L Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo L { get; set; }
        /// <summary>
        /// Gets or sets is C Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo C { get; set; }
        /// <summary>
        /// Gets or sets is R Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo R { get; set; }

        #endregion
    }
    /// <summary>
    /// Inspection Test Un Winder Class.
    /// </summary>
    [Serializable]
    public class Unwinders
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Unwinders()
        {
            this.Set = new TestInfo();
            this.Actual = new TestInfo();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if instance is null auto create default instance.
        /// </summary>
        public void CheckInstances()
        {
            if (null == this.Set)
                this.Set = new TestInfo();
            if (null == this.Actual)
                this.Actual = new TestInfo();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets is Set Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo Set { get; set; }
        /// <summary>
        /// Gets or sets is Actual Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo Actual { get; set; }

        #endregion
    }
    /// <summary>
    /// Inspection Test Winder Class.
    /// </summary>
    [Serializable]
    public class Winders
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Winders()
        {
            this.Set = new TestInfo();
            this.Actual = new TestInfo();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if instance is null auto create default instance.
        /// </summary>
        public void CheckInstances()
        {
            if (null == this.Set)
                this.Set = new TestInfo();
            if (null == this.Actual)
                this.Actual = new TestInfo();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets is Set Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo Set { get; set; }
        /// <summary>
        /// Gets or sets is Actual Test enabed.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TestInfo Actual { get; set; }

        #endregion
    }
    /// <summary>
    /// The Inspection Tests class.
    /// </summary>
    [Serializable]
    public class InspectionTests
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public InspectionTests() : base()
        {
            // General
            this.Densities = new Densities();
            this.Widths = new Widths();
            this.Trims = new Trims();
            this.Floppies = new Floppies();
            this.Hardnesses = new Hardnesses();
            // Tension
            this.Unwinders = new Unwinders();
            this.Winders = new Winders();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if instance is null auto create default instance.
        /// </summary>
        public void CheckInstances()
        {
            if (null == this.Densities)
                this.Densities = new Densities();
            this.Densities.CheckInstances();

            if (null == this.Widths)
                this.Widths = new Widths();
            this.Widths.CheckInstances();
            
            if (null == this.Trims)
                this.Trims = new Trims();
            this.Trims.CheckInstances();
            
            if (null == this.Floppies)
                this.Floppies = new Floppies();
            this.Floppies.CheckInstances();

            if (null == this.Hardnesses)
                this.Hardnesses = new Hardnesses();
            this.Hardnesses.CheckInstances();

            if (null == this.Unwinders)
                this.Unwinders = new Unwinders();
            this.Unwinders.CheckInstances();

            if (null == this.Winders)
                this.Winders = new Winders();
            this.Winders.CheckInstances();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Densities test.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Densities Densities { get; set; }
        /// <summary>
        /// Gets or sets Widths test.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Widths Widths { get; set; }
        /// <summary>
        /// Gets or sets Trims test.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Trims Trims { get; set; }
        /// <summary>
        /// Gets or sets Floppies test.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Floppies Floppies { get; set; }
        /// <summary>
        /// Gets or sets Hardnesses test.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Hardnesses Hardnesses { get; set; }
        /// <summary>
        /// Gets or sets Unwinders test.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Unwinders Unwinders { get; set; }
        /// <summary>
        /// Gets or sets Winders test.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Winders Winders { get; set; }

        #endregion
    }
    /// <summary>
    /// The Inspection Test History Item.
    /// </summary>
    [Serializable]
    public class InspectionTestHistoryItem
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets STD Length.
        /// </summary>
        public string TESTRECORDID { get; set; }
        /// <summary>
        /// Gets or sets STD Length.
        /// </summary>
        public string STDLength { get; set; }
        /// <summary>
        /// Gets or sets Actual Length.
        /// </summary>
        public string ActualLength { get; set; }
        /// <summary>
        /// Gets or sets Density W.
        /// </summary>
        public string DensityW { get; set; }
        /// <summary>
        /// Gets or sets Density F.
        /// </summary>
        public string DensityF { get; set; }
        /// <summary>
        /// Gets or sets Width All.
        /// </summary>
        public string WidthAll { get; set; }
        /// <summary>
        /// Gets or sets Width Pin.
        /// </summary>
        public string WidthPin { get; set; }
        /// <summary>
        /// Gets or sets Width Coat.
        /// </summary>
        public string WidthCoat { get; set; }
        /// <summary>
        /// Gets or sets Trim L.
        /// </summary>
        public string TrimL { get; set; }
        /// <summary>
        /// Gets or sets Trim R.
        /// </summary>
        public string TrimR { get; set; }
        /// <summary>
        /// Gets or sets Floppy L.
        /// </summary>
        public string FloppyL { get; set; }
        /// <summary>
        /// Gets or sets Floppy R.
        /// </summary>
        public string FloppyR { get; set; }
        /// <summary>
        /// Gets or sets Unwinder Actual.
        /// </summary>
        public string UnwinderActual { get; set; }
        /// <summary>
        /// Gets or sets Unwinder Set.
        /// </summary>
        public string UnwinderSet { get; set; }
        /// <summary>
        /// Gets or sets Winder Actual.
        /// </summary>
        public string WinderActual { get; set; }
        /// <summary>
        /// Gets or sets Winder Set.
        /// </summary>
        public string WinderSet { get; set; }

        public string HardnessL { get; set; }
        public string HardnessR { get; set; }
        public string HardnessC { get; set; }

        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class INS_CUTSAMPLELIST
    {
        #region Public Properties

        public string INSPECTIONLOT { get; set; }
        public DateTime? STARTDATE { get; set; }
        public decimal? ORDERNO { get; set; }
        public decimal? CUTLENGTH { get; set; }
        public string REMARK { get; set; }
        public DateTime? CUTDATE { get; set; }
        public string CUTBY { get; set; }

        #endregion
    }
    #endregion

    #region InspectionLotData

    /// <summary>
    /// The Inspection Lot Data class.
    /// </summary>
    [Serializable]
    public class InspectionLotData
    {
        #region Public Propeties
        
        public System.String INSPECTIONLOT { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? GROSSLENGTH { get; set; }
        public System.Decimal? NETLENGTH { get; set; }
        public System.String CUSTOMERID { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String GRADE { get; set; }
        public System.Decimal? GROSSWEIGHT { get; set; }
        public System.Decimal? NETWEIGHT { get; set; }
        public System.String PEINSPECTIONLOT { get; set; }
        public System.String DEFECTID { get; set; }
        public System.String REMARK { get; set; }
        public System.String ATTACHID { get; set; }
        public System.String TESTRECORDID { get; set; }
        public System.String INSPECTEDBY { get; set; }
        public System.String MCNO { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.DateTime? SUSPENDDATE { get; set; }
        public System.String INSPECTIONID { get; set; }
        public System.String RETEST { get; set; }
        public System.String PREITEMCODE { get; set; }

        // เพิ่มใหม่
        public System.String CUSTOMERTYPE { get; set; }

        public System.String CLEARBY { get; set; }
        public System.String CLEARREMARK { get; set; }
        public System.String SUSPENDBY { get; set; }
        public System.DateTime? STARTDATE1 { get; set; }
        public System.String DEFECTFILENAME { get; set; }

        // เก็บค่า DefectID เดิม
        public System.String OldDefectID { get; set; }

        public System.String LOADTYPE { get; set; }

        // เพิ่มใหม่
        public System.String OPERATOR_GROUP { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ Inspection Report
    #region InspectionReportData

    /// <summary>
    /// The Inspection Report Data class.
    /// </summary>
    [Serializable]
    public class InspectionReportData
    {
        #region Public Propeties

        public System.String INSPECTIONLOT { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? GROSSLENGTH { get; set; }
        public System.Decimal? NETLENGTH { get; set; }
        public System.String CUSTOMERID { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String GRADE { get; set; }
        public System.Decimal? GROSSWEIGHT { get; set; }
        public System.Decimal? NETWEIGHT { get; set; }
        public System.String PEINSPECTIONLOT { get; set; }
        public System.String DEFECTID { get; set; }
        public System.String REMARK { get; set; }
        public System.String SHIFT_ID { get; set; }
        public System.String SHIFT_REMARK { get; set; }
        public System.String ATTACHID { get; set; }
        public System.String TESTRECORDID { get; set; }
        public System.String INSPECTEDBY { get; set; }
        public System.String MCNO { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.DateTime? SUSPENDDATE { get; set; }
        public System.String INSPECTIONID { get; set; }
        public System.String RETEST { get; set; }
        public System.String PREITEMCODE { get; set; }
        public System.String CLEARBY { get; set; }
        public System.String CLEARREMARK { get; set; }
        public System.String SUSPENDBY { get; set; }
        public System.DateTime? STARTDATE1 { get; set; }
        public System.String CUSTOMERTYPE { get; set; }
        public System.String DEFECTFILENAME { get; set; }
        public System.String PRODUCTNAME { get; set; }
        public System.String MCNAME { get; set; }
        public System.String CUSTOMERNAME { get; set; }
        public System.String PARTNO { get; set; }
        public System.String LOADINGTYPE { get; set; }

        public System.Decimal? CONFIRMSTARTLENGTH { get; set; }
        public System.String CONFIRMSTDLENGTH { get; set; }

        //New 17/10/22
        public System.Decimal? RESETSTARTLENGTH { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ Inspection Sum Defect Report
    #region INS_ReportSumDefectData

    /// <summary>
    /// The Inspection Sum Defect Report Data class.
    /// </summary>
    [Serializable]
    public class INS_ReportSumDefectData
    {
        #region Public Propeties

        public System.Decimal? TOTALPOINT { get; set; }
        public System.Decimal? SHORTDEFECT { get; set; }
        public System.Decimal? LONGDEFECT { get; set; }
        public System.Decimal? COMLONGDEFECT { get; set; }
        public System.Decimal? COMSHORTDEFECT { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ สำหรับส่งค่า Coreweight ไปคำนวณ
    #region Inspection ItemCode Data (classes)

    /// <summary>
    /// Inspection Defect Code.
    /// </summary>
    public class InspectionItemCodeData
    {
        #region Public Properties

        public decimal? Coreweight { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ INS_GetFinishinslotData
    #region INS_GetFinishinslotData

    /// <summary>
    /// The INS_GetFinishinslotData class.
    /// </summary>
    public class INS_GetFinishinslotData
    {
        #region Public Propeties

        public System.String INSPECTIONLOT { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.String GRADE { get; set; }
        public System.String GROSSWEIGHT { get; set; }
        public System.String NETWEIGHT { get; set; }
        public System.String CUSTOMERTYPE { get; set; }

        // เพิ่มใหม่ Load ค่า DEFECTFILENAME
        public System.String DEFECTFILENAME { get; set; }

        // เพิ่มใหม่ 13/05/16
        public System.String SHIFT_REMARK { get; set; }
        public System.String SHIFT_ID { get; set; }
        public System.DateTime? SHIFT_REMARK_DATE { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ INS_SEARCHINSPECTIONDATA
    #region INS_SearchinspectionData

    /// <summary>
    /// The Inspection Lot Data class.
    /// </summary>
    [Serializable]
    public class INS_SearchInspectionData
    {
        #region Public Propeties

        public System.String INSPECTIONLOT { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? GROSSLENGTH { get; set; }
        public System.Decimal? NETLENGTH { get; set; }
        public System.String CUSTOMERID { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String GRADE { get; set; }
        public System.Decimal? GROSSWEIGHT { get; set; }
        public System.Decimal? NETWEIGHT { get; set; }
        public System.String PEINSPECTIONLOT { get; set; }
        public System.String DEFECTID { get; set; }
        public System.String REMARK { get; set; }
        public System.String ATTACHID { get; set; }
        public System.String TESTRECORDID { get; set; }
        public System.String INSPECTEDBY { get; set; }
        public System.String MCNO { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.DateTime? SUSPENDDATE { get; set; }
        public System.String INSPECTIONID { get; set; }
        public System.String RETEST { get; set; }
        public System.String PREITEMCODE { get; set; }
        public System.String CLEARBY { get; set; }
        public System.String CLEARREMARK { get; set; }
        public System.String SUSPENDBY { get; set; }
        public System.DateTime? STARTDATE1 { get; set; }
        public System.String CUSTOMERTYPE { get; set; }

        // เพิ่มใหม่ เก็บค่า DEFECTFILENAME
        public System.String DEFECTFILENAME { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ INS_DELETEDEFECTData
    #region INS_DELETEDEFECTData

    /// <summary>
    /// The INS_DELETEDEFECTData class.
    /// </summary>
    public class INS_DeleteDefectData
    {
        #region Public Propeties

        public System.String P_DEFECTID { get; set; }
        public System.String P_DEFECTCODE { get; set; }
        public System.Decimal? P_LENGTH1 { get; set; }
        public System.String P_DELETEBY { get; set; }
        public System.String P_DELETEREMARK { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ INS_EDITDEFECTData
    #region INS_EDITDEFECTData

    /// <summary>
    /// The INS_EDITDEFECTData class.
    /// </summary>
    public class INS_EDITDEFECTData
    {
        #region Public Propeties

        public System.String P_DEFECTID { get; set; }
        public System.String P_INSLOT { get; set; }
        public System.String P_DEFECTCODE { get; set; }
        public System.Decimal? P_LENGTH1 { get; set; }
        public System.Decimal? P_POSITION { get; set; }
        public System.String P_NDEFECTCODE { get; set; }
        public System.Decimal? P_NLENGTH1 { get; set; }
        public System.Decimal? P_NPOSITION { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ INS_INSERTMANUALINSPECTDATA
    #region INS_INSERTMANUALINSPECTDATA 

    public class INS_INSERTMANUALINSPECTDATA
    {
        public System.String P_INSLOT { get; set; }
        public System.String P_ITMCODE { get; set; }
        public System.String P_FINISHLOT { get; set; }
        public System.DateTime? P_STARTDATE { get; set; }
        public System.DateTime? P_ENDDATE { get; set; }
        public System.String P_CUSTOMERID { get; set; }
        public System.String P_PRODUCTTYPEID { get; set; }
        public System.String P_INSPECTEDBY { get; set; }
        public System.String P_MCNO { get; set; }
        public System.String P_CUSTOMERTYPE { get; set; }
        public System.String P_LOADTYPE { get; set; }
        public System.Decimal? P_GLENGHT { get; set; }
        public System.Decimal? P_NLENGTH { get; set; }
        public System.String P_GRADE { get; set; }
        public System.Decimal? P_GWEIGHT { get; set; }
        public System.Decimal? P_NWEIGHT { get; set; }
        public System.String P_REMARK { get; set; }
        public System.String P_OPERATOR { get; set; }
    }

    #endregion

    // เพิ่มขึ้นมาเพื่อเป็นการ Load ข้อมุลที่เคยมีใน InspectionTest
    #region InspectionTestTempItem

    /// <summary>
    /// The Inspection Test Temp Item.
    /// </summary>
    public class InspectionTestTempItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets InspecionLotNo.
        /// </summary>
        public string InspecionLotNo { get; set; }

        /// <summary>
        /// Gets or sets ChkNew.
        /// </summary>
        public int ChkCountNew { get; set; }

        /// <summary>
        /// Gets or sets OnlyAddUnwinder.
        /// </summary>
        public bool OnlyAddUnwinder { get; set; }

        #region Winder

        /// <summary>
        /// Gets or sets Unwinders Actual.
        /// </summary>
        public decimal? UnwindersActual { get; set; }
        /// <summary>
        /// Gets or sets Unwinders Set.
        /// </summary>
        public decimal? UnwindersSet { get; set; }

        #endregion

        #endregion
    }

    #endregion

    #region FINISHING_SEARCHFINISHDATA

    /// <summary>
    /// The FINISHING Data class.
    /// </summary>
    [Serializable]
    public class FINISHING_SEARCHFINISHDATA
    {
        #region Public Propeties

        public System.String WEAVINGLOT { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.Decimal? TOTALLENGTH { get; set; }
        public System.String PROCESS { get; set; }
        public System.DateTime? FINISHINGDATE { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String FINISHINGCUSTOMER { get; set; }
        public System.String MC { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? LENGTH1 { get; set; }
        public System.Decimal? LENGTH2 { get; set; }
        public System.Decimal? LENGTH3 { get; set; }
        public System.Decimal? LENGTH4 { get; set; }
        public System.Decimal? LENGTH5 { get; set; }
        public System.Decimal? LENGTH6 { get; set; }
        public System.Decimal? LENGTH7 { get; set; }
        public System.Decimal? SAMPLING_WIDTH { get; set; }
        public System.Decimal? SAMPLING_LENGTH { get; set; }
        public System.String REMARK { get; set; }

        #endregion
    }

    #endregion

    #region Inspection Session

    /// <summary>
    /// Inspection Session.
    /// </summary>
    [Serializable]
    public class InspectionSession
    {
        #region Internal Variables

        private InspectionMCItem _machine = null;
        private LogInResult _currUser = null;

        private InspectionSessionState _state = InspectionSessionState.None;

        private string _finishingLotNo = string.Empty;
        private string _itemCode = string.Empty;
        private string _customerID = string.Empty;

        private string _loadingType = string.Empty;
        private string _customerType = string.Empty;

        private decimal _overallLength = decimal.Zero; // finishing length.
        private decimal _actualLength = decimal.Zero; // actual length on inspection process.
        private decimal _totalIns = decimal.Zero; // total finished lot.

        private string _inspecionLotNo = string.Empty;
        private InspectionTypes _inspectionType = InspectionTypes.Mass;
        private ReAdjustMode _reAdjustMode = ReAdjustMode.None;
        private ReProcessMode _reProcessMode = ReProcessMode.None;
        private string _diffItemCode = string.Empty;

        private InspectionTests _inspectionTest = null;

        private DateTime _startDate = DateTime.MinValue;
        private DateTime _endDate = DateTime.MinValue;

        private string _inspectionId = string.Empty; // PK.

        private Dictionary<string, InspectionDefectCode> _defectCodes = null;

        private InspectionSessionState _lastState = InspectionSessionState.None;
        private string _firstDefId = string.Empty;
        private decimal? _longDefectStart = new decimal?();
        private decimal? _longDefectEnd = new decimal?();
        private string _peInsLot = null;
        // PKs.
        private string _defectId = null;
        private string _testId = null;

        private string _errorInspectionId = string.Empty;

        private string _group = null;


        //New 24/8/22
        private decimal? _longDefectStart100M = new decimal?();
        private decimal? _longDefectEnd100M = new decimal?();


        //New 18/10/22
        private decimal? _resetStartLength = null;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public InspectionSession() : base()
        {

        }

        #endregion

        #region Private Methods

        #region Event Raiser(s)
        
        private void RaiseStateChanged()
        {
            if (null != OnStateChanged)
            {
                OnStateChanged.Call(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Finishing and Inspection Lot Check and Generate
        
        private void LoadTestsByItemCode(string itemCode)
        {
            if (!string.IsNullOrWhiteSpace(itemCode))
            {
                _inspectionTest = InspectionDataService.Instance
                    .GetInspectionTestListByItemCode(itemCode);
            }
            else
            {
                _inspectionTest = null;
            }
        }

        #region GenerateInspectionLotNo

        private void GenerateInspectionLotNo(bool isNextLot)
        {
            if (string.IsNullOrWhiteSpace(_finishingLotNo))
            {
                _inspecionLotNo = string.Empty;
                return;
            }

            #region lotNo

            string lotNo = string.Empty;

            int chkLot = 0;
            string lastLotNo = string.Empty;
            _errorInspectionId = string.Empty;

            if (string.IsNullOrWhiteSpace(_inspecionLotNo) || !isNextLot)
            {
                if (isNextLot == false)
                {
                    lotNo = _finishingLotNo;
                }
                else
                {
                    if (_inspecionLotNo != "")
                        lotNo = _inspecionLotNo;
                    else
                        lotNo = _finishingLotNo;
                }

                #region Check 0 Or 1 Or 2

                if (_finishingLotNo != "")
                {
                    lastLotNo = ((_finishingLotNo.Length) > 0 ? _finishingLotNo.Substring((_finishingLotNo.Length - 1), 1) : "");

                    if (lastLotNo == "0")
                    {
                        chkLot = 0;
                    }
                    else if (lastLotNo == "1")
                    {
                        chkLot = 1;
                    }
                    else if (lastLotNo == "2")
                    {
                        chkLot = 2;
                    }
                    else
                        chkLot = -1;
                }
                else
                    chkLot = -1;

                #endregion
            }
            else
            {
                if (isNextLot == false)
                {
                    lotNo = _finishingLotNo;
                }
                else
                {
                    if (_inspecionLotNo != "")
                        lotNo = _inspecionLotNo;
                    else
                        lotNo = _finishingLotNo;
                }

                #region Check 0 Or 1  Or 2

                if (_finishingLotNo != "")
                {
                    lastLotNo = ((_finishingLotNo.Length) > 0 ? _finishingLotNo.Substring((_finishingLotNo.Length - 1), 1) : "");

                    if (lastLotNo == "0")
                    {
                        chkLot = 0;
                    }
                    else if (lastLotNo == "1")
                    {
                        chkLot = 1;
                    }
                    else if (lastLotNo == "2")
                    {
                        chkLot = 2;
                    }
                    else
                        chkLot = -1;
                }
                else
                    chkLot = -1;

                #endregion
            }

            #endregion

            if (chkLot == 0)
            {
                #region inspecionLotNo

                // Increase last digit lot number.
                if (!string.IsNullOrWhiteSpace(lotNo))
                {
                    // เพิ่มใหม่สำหรับ check ค่าตัวสุดท้ายเป็นตัวเลข
                    string test = ((lotNo.Length) > 0 ? lotNo.Substring((lotNo.Length - 1), 1) : "");

                    if (test != "0" && test != "1" && test != "2" && test != "3" && test != "4" && test != "5"
                        && test != "6" && test != "7" && test != "8" && test != "9")
                    {
                        if (string.IsNullOrWhiteSpace(_finishingLotNo))
                        {
                            _inspecionLotNo = string.Empty;
                            return;
                        }
                        else
                            lotNo = _finishingLotNo;
                    }

                    char ch = lotNo[lotNo.Length - 1];

                    if (char.IsDigit(ch))
                    {
                        int lastChar = Convert.ToInt32(ch.ToString());
                        if (_totalIns > 0)
                        {
                            // Increase lot number to last finished inspection lot.
                            while (lastChar <= _totalIns)
                            {
                                ++lastChar;
                            }
                        }
                        else ++lastChar; // No finished inspection lot.

                        if (lastChar < 10)
                        {
                            _inspecionLotNo =
                                lotNo.Remove(lotNo.Length - 1, 1) +
                                lastChar.ToString("D1");
                        }
                        else
                        {
                            // เพิ่มใหม่ใส่ตัวอักษรต่อท้าย a-z
                            string last = string.Empty;

                            #region last String

                            if (lastChar == 10)
                                last = "A";
                            else if (lastChar == 11)
                                last = "B";
                            else if (lastChar == 12)
                                last = "C";
                            else if (lastChar == 13)
                                last = "D";
                            else if (lastChar == 14)
                                last = "E";
                            else if (lastChar == 15)
                                last = "F";
                            else if (lastChar == 16)
                                last = "G";
                            else if (lastChar == 17)
                                last = "H";
                            else if (lastChar == 18)
                                last = "J";
                            else if (lastChar == 19)
                                last = "K";
                            else if (lastChar == 20)
                                last = "L";
                            else if (lastChar == 21)
                                last = "M";
                            else if (lastChar == 22)
                                last = "N";
                            else if (lastChar == 23)
                                last = "P";
                            else
                            {
                                _errorInspectionId = "Over Number";
                            }

                            #endregion

                            if (_errorInspectionId == "")
                            {
                                _inspecionLotNo =
                                    lotNo.Remove(lotNo.Length - 1, 1) + last;
                            }
                            else
                                _inspecionLotNo = string.Empty;
                        }
                    }
                    else _inspecionLotNo = string.Empty;
                }
                else _inspecionLotNo = string.Empty;

                #endregion
            }
            else if (chkLot == 1)
            {
                #region inspecionLotNo

                if (!string.IsNullOrWhiteSpace(lotNo))
                {
                    int lastChar = 0;
                    string last = string.Empty;

                    string test = ((lotNo.Length) > 0 ? lotNo.Substring((lotNo.Length - 1), 1) : "");
                    if (test != "0" && test != "1" && test != "2" && test != "3" && test != "4" && test != "5"
                        && test != "6" && test != "7" && test != "8" && test != "9")
                    {
                        if (test == "R")
                            last = "T";
                        else if (test == "T")
                            last = "U";
                        else if (test == "U")
                            last = "V";
                        else if (test == "V")
                            last = "W";
                        else
                        {
                            _errorInspectionId = "Over Number";
                        }

                        if (_errorInspectionId == "")
                        {
                            _inspecionLotNo =
                              lotNo.Remove(lotNo.Length - 1, 1) + last;
                        }
                        else
                            _inspecionLotNo = string.Empty;
                    }
                    else
                    {
                        char ch = lotNo[lotNo.Length - 1];

                        if (char.IsDigit(ch))
                        {
                            lastChar = (Convert.ToInt32(ch.ToString())-1);
                            if (_totalIns > 0)
                            {
                                // Increase lot number to last finished inspection lot.
                                while (lastChar <= _totalIns)
                                {
                                    ++lastChar;
                                }
                            }
                            else ++lastChar; // No finished inspection lot.

                            if (lastChar > 0)
                            {
                                #region last String

                                if (lastChar == 1)
                                    last = "R";
                                else if (lastChar == 2)
                                    last = "T";
                                else if (lastChar == 3)
                                    last = "U";
                                else if (lastChar == 4)
                                    last = "V";
                                else if (lastChar == 5)
                                    last = "W";
                                else
                                {
                                    _errorInspectionId = "Over Number";
                                }

                                #endregion

                                if (_errorInspectionId == "")
                                {
                                    _inspecionLotNo =
                                        lotNo.Remove(lotNo.Length - 1, 1) + last;
                                }
                                else
                                    _inspecionLotNo = string.Empty;
                            }

                        }
                        else _inspecionLotNo = string.Empty;
                    }
                }
                else _inspecionLotNo = string.Empty;

                #endregion
            }
            else
            {
                _errorInspectionId = "Item Lot No. isn't True";
            }

            #region Old
            //else if (chkLot == 2)
            //{
            //    #region inspecionLotNo

            //    if (!string.IsNullOrWhiteSpace(lotNo))
            //    {
            //        int lastChar = 0;
            //        string last = string.Empty;

            //        string test = ((lotNo.Length) > 0 ? lotNo.Substring((lotNo.Length - 1), 1) : "");
            //        if (test != "0" && test != "1" && test != "2" && test != "3" && test != "4" && test != "5"
            //            && test != "6" && test != "7" && test != "8" && test != "9")
            //        {

            //            if (test == "U")
            //                last = "V";
            //            else if (test == "V")
            //                last = "W";
            //            else if (test == "W")
            //                last = "X";
            //            else if (test == "X")
            //                last = "Y";
            //            else if (test == "Y")
            //                last = "Z";
            //            else
            //            {
            //                _errorInspectionId = "Over Number";
            //            }

            //            if (_errorInspectionId == "")
            //            {
            //                _inspecionLotNo =
            //                  lotNo.Remove(lotNo.Length - 1, 1) + last;
            //            }
            //            else
            //                _inspecionLotNo = string.Empty;
            //        }
            //        else
            //        {
            //            char ch = lotNo[lotNo.Length - 1];

            //            if (char.IsDigit(ch))
            //            {
            //                lastChar = (Convert.ToInt32(ch.ToString()) - 2);
            //                if (_totalIns > 0)
            //                {
            //                    // Increase lot number to last finished inspection lot.
            //                    while (lastChar <= _totalIns)
            //                    {
            //                        ++lastChar;
            //                    }
            //                }
            //                else ++lastChar; // No finished inspection lot.

            //                if (lastChar > 0)
            //                {
            //                    #region last String

            //                    if (lastChar == 0)
            //                        last = "U";
            //                    else if (lastChar == 1)
            //                        last = "V";
            //                    else if (lastChar == 2)
            //                        last = "W";
            //                    else if (lastChar == 3)
            //                        last = "X";
            //                    else if (lastChar == 4)
            //                        last = "Y";
            //                    else if (lastChar == 5)
            //                        last = "Z";
            //                    else
            //                    {
            //                        _errorInspectionId = "Over Number";
            //                    }

            //                    #endregion

            //                    if (_errorInspectionId == "")
            //                    {
            //                        _inspecionLotNo =
            //                            lotNo.Remove(lotNo.Length - 1, 1) + last;
            //                    }
            //                    else
            //                        _inspecionLotNo = string.Empty;
            //                }

            //            }
            //            else _inspecionLotNo = string.Empty;
            //        }
            //    }
            //    else _inspecionLotNo = string.Empty;

            //    #endregion
            //}
            #endregion
            
            if (_errorInspectionId == "")
            {
                LoadTestsByItemCode(_itemCode);
            }
        }

        #endregion

        private void VerifyFinishingData()
        {
            if (string.IsNullOrWhiteSpace(_finishingLotNo))
            {
                // Reset Inspection value
                New();
                return;
            }

            FinishingInfo result = 
                InspectionDataService.Instance.GetFinishingData(_finishingLotNo);
            if (null != result)
            {
                // Set item code.

                // เพิ่ม CUSTOMERID
                _customerID = result.CUSTOMERID;

                if (!string.IsNullOrEmpty(result.ItemCode))
                {
                    if (result.ItemCode == "4L46B25R-210" || result.ItemCode == "4L46C15R-210")
                    {
                        if (result.ItemCode == "4L46C15R-210")
                        {
                            _itemCode = "4L46C15R";
                        }
                        else if (result.ItemCode == "4L46B25R-210")
                        {
                            if (!string.IsNullOrEmpty(result.ITM_WEAVING))
                            {
                                if (result.ITM_WEAVING.Length >= 6)
                                {
                                    if (result.ITM_WEAVING.Substring((result.ITM_WEAVING.Length - 3), 3) == "APM")
                                    {
                                        _itemCode = "4L46B25R-AF01";
                                    }
                                    else if (result.ITM_WEAVING.Substring((result.ITM_WEAVING.Length - 3), 3) == "TTS")
                                    {
                                        _itemCode = "4L46B25R";
                                    }
                                }
                                else
                                {
                                    _itemCode = result.ItemCode;
                                }
                            }
                            else
                            {
                                _itemCode = result.ItemCode;
                            }
                        }
                    }
                    else
                    {
                        _itemCode = result.ItemCode;
                    }
                }

                _overallLength = result.OverallLength;
                _actualLength = result.ActualLength;
                _totalIns = result.TotalIns;

                if (!string.IsNullOrWhiteSpace(_finishingLotNo) &&
                    !string.IsNullOrWhiteSpace(_itemCode))
                {
                    if (_totalIns <= 0)
                    {
                        GenerateInspectionLotNo(false);
                    }
                    else
                    {
                        // เดิมถ้า _totalIns > 0 จะส่งเป็น true เพื่อเอาเลข _inspecionLotNo ไปใช้
                        //GenerateInspectionLotNo(true);

                        GenerateInspectionLotNo(false);
                    }

                    if (!string.IsNullOrWhiteSpace(_inspecionLotNo))
                    {
                        // can generate new inspection lot number.
                        // change state to Idle state.
                        _state = InspectionSessionState.Idle;
                        // Raise event.
                        RaiseStateChanged();
                    }
                }
            }
        }

        private void VerifyInspectionData()
        {
            if (string.IsNullOrWhiteSpace(_inspecionLotNo))
            {
                // Reset Inspection value
                New();
                return;
            }

            bool reAdjust = false;
            if (_reAdjustMode == ReAdjustMode.Same)
            {
                reAdjust = true;
            }
            else
                reAdjust = false;

            InspectionLotData result = this.GetInspectionLotData(reAdjust);
            if (null != result)
            {
                // setup session data
                _inspectionId = result.INSPECTIONID;
                _finishingLotNo = result.FINISHINGLOT;

                // เพิ่ม customerType
                _customerType = result.CUSTOMERTYPE;

                if (!string.IsNullOrWhiteSpace(_finishingLotNo))
                {
                    FinishingInfo result2 =
                        InspectionDataService.Instance.GetFinishingData(_finishingLotNo);
                    if (null != result2)
                    {
                        // เพิ่ม CUSTOMERID
                        _customerID = result2.CUSTOMERID;

                        //_actualLength = result2.ActualLength;
                        _totalIns = result2.TotalIns;

                        if (ReProcessMode == ReProcessMode.None)
                        {
                            // Re Adjust Mode
                            if (result2.ItemCode != result.ITEMCODE)
                            {
                                //_itemCode = result2.ItemCode;

                                if (!string.IsNullOrEmpty(result2.ItemCode))
                                {
                                    if (result2.ItemCode == "4L46B25R-210" || result2.ItemCode == "4L46C15R-210")
                                    {
                                        if (result2.ItemCode == "4L46C15R-210")
                                        {
                                            _itemCode = "4L46C15R";
                                        }
                                        else if (result2.ItemCode == "4L46B25R-210")
                                        {
                                            if (!string.IsNullOrEmpty(result2.ITM_WEAVING))
                                            {
                                                if (result2.ITM_WEAVING.Length >= 6)
                                                {
                                                    if (result2.ITM_WEAVING.Substring((result2.ITM_WEAVING.Length - 3), 3) == "APM")
                                                    {
                                                        _itemCode = "4L46B25R-AF01";
                                                    }
                                                    else if (result2.ITM_WEAVING.Substring((result2.ITM_WEAVING.Length - 3), 3) == "TTS")
                                                    {
                                                        _itemCode = "4L46B25R";
                                                    }
                                                    else
                                                    {
                                                        _itemCode = result2.ItemCode;
                                                    }
                                                }
                                                else
                                                {
                                                    _itemCode = result2.ItemCode;
                                                }
                                            }
                                            else
                                            {
                                                _itemCode = result2.ItemCode;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _itemCode = result2.ItemCode;
                                    }
                                }

                                _diffItemCode = result.ITEMCODE;
                            }
                            else
                            {
                                _itemCode = result.ITEMCODE;

                                _diffItemCode = string.Empty;
                            }
                            // Load Test List
                            if (!string.IsNullOrWhiteSpace(_diffItemCode))
                                LoadTestsByItemCode(_diffItemCode);
                            else LoadTestsByItemCode(_itemCode);
                        }
                        else
                        {
                            // Re Process Mode.
                            //_itemCode = result2.ItemCode;

                            _diffItemCode = string.Empty;

                            _itemCode = result.ITEMCODE;
                            _diffItemCode = string.Empty;
                            // Keep Inspection Lot before generate new one
                            _peInsLot = _inspecionLotNo;
                            // Generate next lot number
                            if (_totalIns <= 0)
                                GenerateInspectionLotNo(false);
                            else GenerateInspectionLotNo(true);
                        }

                        if (!string.IsNullOrEmpty(result.OPERATOR_GROUP))
                        {
                            _group = result.OPERATOR_GROUP;
                        }

                        //_session.StartDate = result.STARTDATE.Value;
                        if (!result.GROSSLENGTH.HasValue)
                        {
                            "VerifyInspectionData detect GROSSLENGTH is null.".Info();
                        }
                        _overallLength = (result.GROSSLENGTH.HasValue) ? 
                            result.GROSSLENGTH.Value : decimal.Zero; // Set Overall to GROSS LENGTH
                        _actualLength = _overallLength; // Set Actual Length to Overall

                        // change state to Idle state.
                        _state = InspectionSessionState.Idle;

                        // Raise event.
                        RaiseStateChanged();
                    }
                }
            }
        }

        #endregion

        #region Database Operation(s)

        private void LoadDefectCodes()
        {
            // load defect code.
            _defectCodes = InspectionDataService.Instance.GetDefectCodes(_defectId);
        }

        #endregion

        #endregion

        #region Public Methods

        #region Init
        
        /// <summary>
        /// Init current Session.
        /// </summary>
        /// <param name="machine">The selected machine.</param>
        /// <param name="currUser">The current user that operate machine.</param>
        public void Init(InspectionMCItem machine, LogInResult currUser)
        {
            _machine = machine;
            _currUser = currUser;
            this.New(); // Reset all data.
            // Load master datasource
            LoadDefectCodes();
        }

        #endregion

        #region Get Inspection Lot data

        /// <summary>
        /// Gets Inspection Data.
        /// </summary>
        /// <returns>Returns first occurance of inpseciont data.</returns>
        public InspectionLotData GetInspectionLotData(bool reAdjust)
        {
            InspectionLotData result = null;

            List<InspectionLotData> results =
                InspectionDataService.Instance.GetInspectionData(_inspecionLotNo, reAdjust);
            if (null != results && results.Count > 0)
            {
                result = results[0];
            }

            return result;
        }

        #endregion

        #region Main Operations

        /// <summary>
        /// Start new inspection lot.
        /// </summary>
        public void Start()
        {
            #region Check State
            
            if (this.State == InspectionSessionState.None)
            {
                "Cannot do start operation when in None state".Info();
                return;
            }
            if (this.State == InspectionSessionState.Started)
            {
                "Cannot do start operation when in Started state".Info();
                return;
            }
            if (this.State == InspectionSessionState.LongDefect)
            {
                "Cannot do start operation when in Long Defect state".Info();
                return;
            }

            #endregion

            #region Insert Inspection Process

            _startDate = DateTime.Now;
            string mcId = (null != _machine) ? _machine.MCId : string.Empty;
            string instBy = (null != _currUser) ? _currUser.OperatorId : string.Empty;
            string instType = (_inspectionType == InspectionTypes.Mass) ?
                "1" : "2";
            string retestFlag = "N";
            if (ReAdjustMode == ReAdjustMode.Same ||
                ReAdjustMode == ReAdjustMode.Diff ||
                ReProcessMode == ReProcessMode.ReProcess)
            {
                retestFlag = "Y";
            }
            string itmCode = (ReAdjustMode != ReAdjustMode.Diff) ? _itemCode : _diffItemCode;
            try
            {
                _inspectionId = InspectionDataService.Instance.InsertInspectionProcess(
                    _finishingLotNo, itmCode, _inspecionLotNo, mcId,
                    instType, _customerID, _customerType, _peInsLot, _startDate, instBy,
                    retestFlag, _loadingType,_group);
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            #endregion

            #region Check is inserted success
            
            if (string.IsNullOrWhiteSpace(_inspectionId))
            {
                // Failed.
                _startDate = DateTime.MinValue;
            }
            else
            {
                // Success.
                this.State = InspectionSessionState.Started; // changed state
            }

            #endregion

            // Raise event.
            RaiseStateChanged();
        }
        /// <summary>
        /// End current inspection lot. The end date is stamped.
        /// </summary>
        /// <param name="length">The gross length (counter).</param>
        /// <param name="grade">The grade.</param>
        public void End(decimal length, decimal nlength, string grade, string LOADTYPE, string FLAG)
        {
            #region Check State
            
            if (this.State == InspectionSessionState.None)
            {
                "Cannot do stop operation when in None state".Info();
                return;
            }
            if (this.State == InspectionSessionState.Idle)
            {
                "Cannot do stop operation when in Idle state".Info();
                return;
            }
            if (this.State == InspectionSessionState.LongDefect)
            {
                "Cannot do stop operation when in Long Defect state".Info();
                return;
            }

            #endregion

            //นำออกไปก่อนยังไม่ได้ใช้
            //if (_customerID == "09")
            //{
            //    if (grade != "C" || grade != "X")
            //    {
            //        INS_GETRESETSTARTLENGTH();

            //        if (_resetStartLength != null)
            //        {
            //            length = (length - _resetStartLength.Value);

            //            decimal? nleng = GetNetLength(length, grade);

            //            if (nleng != null)
            //                nlength = nleng.Value;
            //        }
            //    }
            //}

            #region Update To Database

            InspectionDataService.Instance.UpdateInspectionProcess(_inspecionLotNo,
                _startDate, _customerType, length, nlength, grade, LOADTYPE, _group, FLAG);

            #endregion

            //_startDate = DateTime.MinValue; // Do not clear start time used for next process.
            _endDate = DateTime.Now; // Set End Time

            this.State = InspectionSessionState.Idle; // changed state
            // Raise event.
            RaiseStateChanged();
        }
        /// <summary>
        /// Start next inspection lot. The exists lot will stop automatically.
        /// </summary>
        public void Next()
        {
            #region Check State
            
            if (this.State == InspectionSessionState.None)
            {
                "Cannot do next operation when in None state".Info();
                return;
            }
            if (this.State == InspectionSessionState.Started)
            {
                "Cannot do next operation when in Started state".Info();
                return;
            }
            if (this.State == InspectionSessionState.LongDefect)
            {
                "Cannot do next operation when in Long Defect state".Info();
                return;
            }

            #endregion

            if (this.ReAdjustMode == ReAdjustMode.None &&
                this.ReProcessMode == ReProcessMode.None)
            {
                #region Normal
                
                // Generate next lot.
                if (_startDate != DateTime.MinValue || _startDate != DateTime.MaxValue)
                {
                    FinishingInfo result =
                        InspectionDataService.Instance.GetFinishingData(_finishingLotNo);
                    if (null != result)
                    {
                        // Update New Length Information.

                        // เพิ่ม CUSTOMERID
                        _customerID = result.CUSTOMERID;

                        if (!string.IsNullOrEmpty(result.ItemCode))
                        {
                            if (result.ItemCode == "4L46B25R-210" || result.ItemCode == "4L46C15R-210")
                            {
                                if (result.ItemCode == "4L46C15R-210")
                                {
                                    _itemCode = "4L46C15R";
                                }
                                else if (result.ItemCode == "4L46B25R-210")
                                {
                                    if (!string.IsNullOrEmpty(result.ITM_WEAVING))
                                    {
                                        if (result.ITM_WEAVING.Length >= 6)
                                        {
                                            if (result.ITM_WEAVING.Substring((result.ITM_WEAVING.Length - 3), 3) == "APM")
                                            {
                                                _itemCode = "4L46B25R-AF01";
                                            }
                                            else if (result.ITM_WEAVING.Substring((result.ITM_WEAVING.Length - 3), 3) == "TTS")
                                            {
                                                _itemCode = "4L46B25R";
                                            }
                                            else
                                            {
                                                _itemCode = result.ItemCode;
                                            }
                                        }
                                        else
                                        {
                                            _itemCode = result.ItemCode;
                                        }
                                    }
                                    else
                                    {
                                        _itemCode = result.ItemCode;
                                    }
                                }
                            }
                            else
                            {
                                _itemCode = result.ItemCode;
                            }
                        }

                        _overallLength = result.OverallLength;
                        _actualLength = result.ActualLength;
                        _totalIns = result.TotalIns;
                    }
                    // in case already start and required next lot no.
                    if (!string.IsNullOrWhiteSpace(_finishingLotNo) &&
                        !string.IsNullOrWhiteSpace(_itemCode))
                    {
                        GenerateInspectionLotNo(true);
                    }
                }

                #endregion
            }
            else if (this.ReAdjustMode == ReAdjustMode.Same)
            {
                #region Same Item

                // Generate next lot.
                if (_startDate != DateTime.MinValue || _startDate != DateTime.MaxValue)
                {

                }

                #endregion
            }
            else if (this.ReAdjustMode == ReAdjustMode.Diff)
            {
                #region Diff Item

                // Generate next lot.
                if (_startDate != DateTime.MinValue || _startDate != DateTime.MaxValue)
                {

                }

                #endregion
            }
            else if (this.ReProcessMode == ReProcessMode.ReProcess)
            {
                #region Re Process

                // Generate next lot.
                if (_startDate != DateTime.MinValue || _startDate != DateTime.MaxValue)
                {

                }

                #endregion
            }

            // Reset start/end time
            _startDate = DateTime.MinValue;
            _endDate = DateTime.MinValue;
            // Reset IDs when change lot
            _defectId = null;
            _testId = null;
            // Raise event.
            RaiseStateChanged();
        }
        /// <summary>
        /// Imediately Clear inspection. Clear all data on screen but No end date stamped.
        /// </summary>
        /// <param name="userToClear">The use to clear.</param>
        public void Clear(LogInInfo userToClear = null)
        {
            #region Update To database

            int processId = 8;
            string operatorId = string.Empty;
            if (null != userToClear)
            {
                List<LogInResult> users = UserDataService.Instance.GetOperators(
                    userToClear.UserName.Trim(), userToClear.Password.Trim(), processId);
                if (null != users && users.Count > 0)
                {
                    operatorId = users[0].OperatorId;
                }
            }
            string remark = (null != userToClear) ? userToClear.Remark : null;

            InspectionDataService.Instance.ClearCurrentInspection(
                _inspecionLotNo, _startDate, _customerType, remark, operatorId);

            #endregion

            this.New(); // Clear all input data.
        }
        /// <summary>
        /// Suspend current operation.
        /// </summary>
        public void Suspend()
        {
            #region Check State

            if (this.State == InspectionSessionState.None)
            {
                "Cannot do suspend operation when in None state".Info();
                return;
            }
            if (this.State == InspectionSessionState.Idle)
            {
                "Cannot do suspend operation when in Idle state".Info();
                return;
            }
            if (this.State == InspectionSessionState.LongDefect)
            {
                "Cannot do suspend operation when in Long Defect state".Info();
                return;
            }

            #endregion

            #region Update To database

            string operatorId = (null != _currUser) ? _currUser.OperatorId : null;
            InspectionDataService.Instance.SuspendInspectionProcess(
                _inspecionLotNo, _startDate, _customerType, operatorId);

            #endregion

            this.State = InspectionSessionState.Idle; // changed state
            // Raise event.
            RaiseStateChanged();
        }
        /// <summary>
        /// Resume.
        /// </summary>
        /// <param name="finLotNo"></param>
        /// <param name="itemCode"></param>
        /// <param name="insLotNo"></param>
        /// <param name="customerType"></param>
        /// <param name="insId"></param>
        /// <param name="defectId"></param>
        /// <param name="testId"></param>
        /// <param name="netLen"></param>
        /// <param name="grossLen"></param>
        public void Resume(string finLotNo, string itemCode, string insLotNo, 
            string customerType,
            string insId, string defectId, string testId,
            decimal? netLen, decimal? grossLen)
        {
            _finishingLotNo = finLotNo;
            _itemCode = itemCode;
            _inspecionLotNo = insLotNo;
            _customerType = customerType;
            _inspectionId = insId;
            _defectId = defectId;
            _testId = testId;

            _overallLength = (grossLen.HasValue) ? grossLen.Value : decimal.Zero;
            _actualLength = (grossLen.HasValue) ? grossLen.Value : decimal.Zero;

            if (!string.IsNullOrWhiteSpace(_finishingLotNo) &&
                !string.IsNullOrWhiteSpace(_itemCode) &&
                !string.IsNullOrWhiteSpace(_inspecionLotNo))
            {
                FinishingInfo result =
                    InspectionDataService.Instance.GetFinishingData(_finishingLotNo);
                if (null != result)
                {
                    // Update New Length Information.

                    // เพิ่ม CUSTOMERID
                    _customerID = result.CUSTOMERID;

                    if (!string.IsNullOrEmpty(result.ItemCode))
                    {
                        if (result.ItemCode == "4L46B25R-210" || result.ItemCode == "4L46C15R-210")
                        {
                            if (result.ItemCode == "4L46C15R-210")
                            {
                                _itemCode = "4L46C15R";
                            }
                            else if (result.ItemCode == "4L46B25R-210")
                            {
                                if (!string.IsNullOrEmpty(result.ITM_WEAVING))
                                {
                                    if (result.ITM_WEAVING.Length >= 6)
                                    {
                                        if (result.ITM_WEAVING.Substring((result.ITM_WEAVING.Length - 3), 3) == "APM")
                                        {
                                            _itemCode = "4L46B25R-AF01";
                                        }
                                        else if (result.ITM_WEAVING.Substring((result.ITM_WEAVING.Length - 3), 3) == "TTS")
                                        {
                                            _itemCode = "4L46B25R";
                                        }
                                        else
                                        {
                                            _itemCode = result.ItemCode;
                                        }
                                    }
                                    else
                                    {
                                        _itemCode = result.ItemCode;
                                    }
                                }
                                else
                                {
                                    _itemCode = result.ItemCode;
                                }
                            }
                        }
                        else
                        {
                            _itemCode = result.ItemCode;
                        }
                    }

                    _overallLength = result.OverallLength;
                    _actualLength = result.ActualLength;
                    _totalIns = result.TotalIns;
                }

                // Load test lists.
                LoadTestsByItemCode(_itemCode);

                _state = InspectionSessionState.Idle;
                // Raise event.
                RaiseStateChanged();
            }
        }
        /// <summary>
        /// New. Imediately Clear inspection. No data updat to database.
        /// </summary>
        public void New()
        {
            _state = InspectionSessionState.None;
            _reAdjustMode = ReAdjustMode.None;
            _reProcessMode = ReProcessMode.None;

            _finishingLotNo = string.Empty;
            _itemCode = string.Empty;
            _customerType = string.Empty;
            _overallLength = decimal.Zero;
            _actualLength = decimal.Zero;
            _totalIns = decimal.Zero;

            _inspecionLotNo = string.Empty;
            _startDate = DateTime.MinValue;
            _endDate = DateTime.MinValue;
            _peInsLot = null;

            _firstDefId = string.Empty;
            _longDefectStart = new decimal?();
            _longDefectEnd = new decimal?();

            //New 24/8/22
            _longDefectStart100M = new decimal?();
            _longDefectEnd100M = new decimal?();

            // Reset IDs when reset lot
            _defectId = null;
            _testId = null;
            _group = "A";

            // Raise event.
            RaiseStateChanged();
        }

        public void Finish()
        {

            #region Update To Database

            InspectionDataService.Instance.FinishInspectionProcess(_inspecionLotNo,
                _startDate);

            #endregion

            //_startDate = DateTime.MinValue; // Do not clear start time used for next process.
            _endDate = DateTime.Now; // Set End Time

            this.State = InspectionSessionState.Idle; // changed state
            // Raise event.
            RaiseStateChanged();
        }

        public void FinishManual()
        {

            #region Update To Database

            InspectionDataService.Instance.FinishInspectionProcess(_inspecionLotNo,
                _startDate,_endDate);

            #endregion

            //_startDate = DateTime.MinValue; // Do not clear start time used for next process.
            //_endDate = DateTime.Now; // Set End Time

            this.State = InspectionSessionState.Idle; // changed state
            // Raise event.
            RaiseStateChanged();
        }
        #endregion

        #region Sub Operations

        /// <summary>
        /// Add Remark.
        /// </summary>
        /// <param name="remark">The remark message.</param>
        public void AddRemark(string remark)
        {
            if (!string.IsNullOrWhiteSpace(this._inspecionLotNo))
            {
                InspectionDataService.Instance.AddRemark(
                    _inspecionLotNo, _startDate, _customerType, remark);
            }
        }
        /// <summary>
        /// Gets Remark.
        /// </summary>
        /// <returns>Returns Remark on current inspection lot.</returns>
        public string GetRemark()
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(this._inspecionLotNo))
            {
                #region Old
                //List<InspectionLotData> results =
                //    InspectionDataService.Instance.GetInspectionData(this._inspecionLotNo);

                //if (null != results && results.Count > 0)
                //{
                //    result = results[0].REMARK;
                //}
                #endregion

                List<InspectionLotData> results = InspectionDataService.Instance.GetInspectionRemark(this._inspecionLotNo);

                if (null != results && results.Count > 0)
                    result = results[0].REMARK;

            }
            return result;
        }
        /// <summary>
        /// Add Defect Code.
        /// </summary>
        /// <param name="value">The full code text.</param>
        /// <param name="counterValue">The counter value.</param>
        public void AddDefectCode(string value, string counterValue)
        {
            if (string.IsNullOrWhiteSpace(this._inspecionLotNo))
                return;
            if (_state == InspectionSessionState.None ||
                _state == InspectionSessionState.Idle ||
                _state == InspectionSessionState.LongDefect)
                return;

            #region Update to database

            string text = value.Trim();
            if (text.Length > 2)
            {
                string code = text.Substring(0, 2).ToUpper();
                string pos = text.Substring(2, text.Length - 2).ToUpper();
                decimal len1 = Convert.ToDecimal(counterValue);
                decimal? len2 = new decimal?();
                decimal point = (decimal)1;
                decimal? position = Convert.ToDecimal(pos);

                string defId = InspectionDataService.Instance.AddInspectionLotDefect(
                    code, point, position, len1, len2, _inspecionLotNo);
                if (string.IsNullOrWhiteSpace(_defectId) && !string.IsNullOrWhiteSpace(defId))
                {
                    // Update defect id for first time of lot.
                    _defectId = defId;
                }
            }

            #endregion
        }
        /// <summary>
        /// Input long defect start position.
        /// </summary>
        /// <param name="len1">The position from counter.</param>
        public void StartLongDefect(decimal len1)
        {
            if (string.IsNullOrWhiteSpace(this._inspecionLotNo))
                return;
            if (_state == InspectionSessionState.None ||
                _state == InspectionSessionState.Idle ||
                _state == InspectionSessionState.LongDefect)
                return;

            // Keep last state
            _lastState = _state;

            _longDefectStart = len1;

            //New 24/8/22
            _longDefectStart100M = len1;

            #region Change state

            _state = InspectionSessionState.LongDefect;
            // Raise event.
            RaiseStateChanged();

            #endregion
        }
        /// <summary>
        /// Input long defect end position.
        /// </summary>
        /// <param name="len2">The position from counter.</param>
        /// <param name="value">The full code text.</param>
        public void EndLongDefect(decimal len2, string value)
        {
            if (string.IsNullOrWhiteSpace(this._inspecionLotNo))
                return;
            if (_state != InspectionSessionState.LongDefect)
                return;

            _longDefectEnd = len2;

            #region Update to database

            string text = value.Trim();
            if (text.Length > 2)
            {
                string code = text.Substring(0, 2).ToUpper();
                string pos = text.Substring(2, text.Length - 2).ToUpper();
                decimal point = (decimal)2;
                decimal? position = Convert.ToDecimal(pos);

                string defectId = InspectionDataService.Instance.AddInspectionLotDefect(
                    code, point, position,
                    _longDefectStart, _longDefectEnd,
                    _inspecionLotNo);

                if (!string.IsNullOrWhiteSpace(defectId))
                {
                    // update success
                    #region Reset value

                    _longDefectStart = new decimal?();
                    _longDefectEnd = new decimal?();

                    #endregion

                    // Update defect id for first time of lot.
                    _defectId = defectId;
                }
                else
                {
                    // update failed
                    #region Reset value

                    _longDefectStart = new decimal?();
                    _longDefectEnd = new decimal?();

                    #endregion

                    _defectId = null;
                }

                #region Change state

                // Restore state
                _state = _lastState;
                // Raise event.
                RaiseStateChanged();

                #endregion
            }

            #endregion
        }
        /// <summary>
        /// Cancel long defect input.
        /// </summary>
        public void CancelLongDefect()
        {
            if (_state != InspectionSessionState.LongDefect)
                return;

            #region Reset value

            _longDefectStart = new decimal?();
            _longDefectEnd = new decimal?();

            //New 24/8/22
            _longDefectStart100M = new decimal?();
            _longDefectEnd100M = new decimal?();

            #endregion

            #region Change state

            // Restore state
            _state = _lastState;
            // Raise event.
            RaiseStateChanged();

            #endregion
        }
        /// <summary>
        /// Check defect code. This method used when user enter defect code with position.
        /// </summary>
        /// <param name="value">
        /// The full text. This value contains 2 digit code and the rest is position.
        /// </param>
        /// <returns>Returns string that represents vender code name.</returns>
        public string CheckDefectCode(string value)
        {
            string result = "-";
            // Check defect.
            if (string.IsNullOrWhiteSpace(value))
            {
                return result;
            }
            string text = value.Trim();
            if (text.Length >= 2)
            {
                string code = text.Substring(0, 2).ToUpper();
                if (null != _defectCodes && _defectCodes.ContainsKey(code))
                {
                    result = _defectCodes[code].DesciptionEN;
                }
            }
            else if (text.Length < 2)
            {
                // Do nothing
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckPosition(string value)
        {
            bool result = true;
            // Check defect.
            if (string.IsNullOrWhiteSpace(value))
            {
                return result;
            }
            string text = value.Trim();
            if (text.Length > 2)
            {
                try
                {
                    string code = text.Substring(2, (text.Length - 2));
                    if (!string.IsNullOrEmpty(code))
                    {
                        decimal chkP = decimal.Parse(code);

                        if (chkP > 220)
                            result = false;
                        else
                            result = true;
                    }
                }
                catch
                {
                    result = false;
                }
            }
            else if (text.Length <= 2)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Gets defect count.
        /// </summary>
        /// <returns>Returns defect count of current inspection lot.</returns>
        public int GetDefectCount()
        {
            int total = InspectionDataService.Instance.GetDefectCount(
                        _inspecionLotNo,_defectId);
            return total;
        }
        /// <summary>
        /// Get Grade.
        /// </summary>
        /// <param name="length">The counter length.</param>
        /// <returns>Returns grade string.</returns>
        public string GetGrade(decimal length)
        {
            string grade = InspectionDataService.Instance.GetGrade(
                        _inspecionLotNo, _itemCode, length, null);
            return grade;
        }
        /// <summary>
        /// Gets Defect List.
        /// </summary>
        /// <returns>Returns defect list.</returns>
        public List<InspectionDefectItem> GetDefectList()
        {
            List<InspectionDefectItem> results = InspectionDataService.Instance
                .GetDefectList(_inspecionLotNo, _defectId);

            return results;
        }
        /// <summary>
        /// AddInspection Test Data. 
        /// Note the Session's InspectionTests should not be null and set test(s) value.
        /// </summary>
        /// <param name="popupDate">The Popup Date.</param>
        /// <param name="actualLen">The actual length from counter.</param>
        /// <returns>Returns test id.</returns>
        public string AddInspectionTestData(DateTime? popupDate, decimal actualLen)
        {
            string result = string.Empty;

            if (null == _inspectionTest)
            {
                return result;
            }

            // Calc STD Lenght from actual length.
            decimal stdLen = decimal.Floor((actualLen / (decimal)100)) * (decimal)100;

            result = InspectionDataService.Instance.AddInspectionTestData(
                _inspecionLotNo, popupDate, actualLen, stdLen, _inspectionTest);

            if (string.IsNullOrWhiteSpace(_testId) && !string.IsNullOrWhiteSpace(result))
            {
                // Update test id for first time of lot.
                _testId = result;
            }

            return result;
        }
        /// <summary>
        /// Get Test History List.
        /// </summary>
        /// <returns>Returns history list.</returns>
        public List<InspectionTestHistoryItem> GetTestHistoryList()
        {
            List<InspectionTestHistoryItem> results = InspectionDataService.Instance
                .GetTestHistoryList(_inspecionLotNo, _testId);

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetTestID()
        {
            return _testId;
        }

        /// <summary>
        /// Change Grade.
        /// </summary>
        /// <param name="oldGrade">The Old Grade.</param>
        /// <param name="newGrade">The New Grade.</param>
        /// <param name="remark">The Remark.</param>
        /// <param name="userName">The User Name.</param>
        /// <param name="password">The Password.</param>
        /// <returns>Returns true if change grade success.</returns>
        public bool ChangeGrade(string oldGrade, string newGrade,
            string remark,
            string userName, string password)
        {
            #region Check State

            if (this.State == InspectionSessionState.None)
            {
                "Cannot Change Grade when in None state".Info();
                return false;
            }
            if (this.State == InspectionSessionState.Idle)
            {
                "Cannot Change Grade when in Idle state".Info();
                return false;
            }
            if (this.State == InspectionSessionState.LongDefect)
            {
                "Cannot Change Grade when in Long Defect state".Info();
                return false;
            }

            #endregion

            return InspectionDataService.Instance
                .ChangeGrade(_inspecionLotNo, oldGrade, newGrade, remark, userName, password);
        }

        // เพิ่มขึ้นมาใหม่ Defect 100M
        /// <summary>
        /// GetDefect100MCount
        /// </summary>
        /// <param name="startLeng"></param>
        /// <param name="endLeng"></param>
        /// <returns></returns>
        public int GetDefect100MCount(decimal startLeng, decimal endLeng)
        {
            int total = InspectionDataService.Instance.GetDefect100MCount(
                        _defectId, _inspecionLotNo, startLeng, endLeng);
            return total;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusid"></param>
        /// <param name="length"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public decimal? GetNetLength(decimal? length, string grade)
        {
            decimal? netLength = null;

            netLength = InspectionDataService.Instance.GetNetLength(
                        _customerID, _itemCode, length, grade, _defectId);

            return netLength;
        }

        #endregion

        //เพิ่มใหม่ เพื่อ Gen เลข InspectionLot ใหม่ ใช้ในหน้า ProcessControlPage
        #region GenNewInspectionLotNo

        public string GenNewInspectionLotNo()
        {
            string newInsLotNo = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(_finishingLotNo))
                {
                    newInsLotNo = string.Empty;
                    return newInsLotNo;
                }

                #region lotNo

                string lotNo = string.Empty;

                int chkLot = 0;
                string lastLotNo = string.Empty;
                _errorInspectionId = string.Empty;

                lotNo = _finishingLotNo;

                #region Check 0 Or 1  Or 2

                if (_finishingLotNo != "")
                {
                    lastLotNo = ((_finishingLotNo.Length) > 0 ? _finishingLotNo.Substring((_finishingLotNo.Length - 1), 1) : "");

                    if (lastLotNo == "0")
                    {
                        chkLot = 0;
                    }
                    else if (lastLotNo == "1")
                    {
                        chkLot = 1;
                    }
                    else if (lastLotNo == "2")
                    {
                        chkLot = 2;
                    }
                    else
                        chkLot = -1;
                }
                else
                    chkLot = -1;

                #endregion

                #endregion

                decimal? totalIns = null;

                FinishingInfo result =
               InspectionDataService.Instance.GetFinishingData(_finishingLotNo);
                if (null != result)
                {
                    totalIns = result.TotalIns;
                }

                if (chkLot == 0)
                {
                    #region inspecionLotNo

                    // Increase last digit lot number.
                    if (!string.IsNullOrWhiteSpace(lotNo))
                    {
                        // เพิ่มใหม่สำหรับ check ค่าตัวสุดท้ายเป็นตัวเลข
                        string test = ((lotNo.Length) > 0 ? lotNo.Substring((lotNo.Length - 1), 1) : "");

                        if (test != "0" && test != "1" && test != "2" && test != "3" && test != "4" && test != "5"
                            && test != "6" && test != "7" && test != "8" && test != "9")
                        {
                            if (string.IsNullOrWhiteSpace(_finishingLotNo))
                            {
                                newInsLotNo = string.Empty;
                                return newInsLotNo;
                            }
                            else
                                lotNo = _finishingLotNo;
                        }

                        char ch = lotNo[lotNo.Length - 1];

                        if (char.IsDigit(ch))
                        {
                            int lastChar = Convert.ToInt32(ch.ToString());
                            if (totalIns > 0)
                            {
                                // Increase lot number to last finished inspection lot.
                                while (lastChar <= totalIns)
                                {
                                    ++lastChar;
                                }
                            }
                            else ++lastChar; // No finished inspection lot.

                            if (lastChar < 10)
                            {
                                newInsLotNo =
                                    lotNo.Remove(lotNo.Length - 1, 1) +
                                    lastChar.ToString("D1");
                            }
                            else
                            {
                                // เพิ่มใหม่ใส่ตัวอักษรต่อท้าย a-z
                                string last = string.Empty;

                                #region last String

                                if (lastChar == 10)
                                    last = "A";
                                else if (lastChar == 11)
                                    last = "B";
                                else if (lastChar == 12)
                                    last = "C";
                                else if (lastChar == 13)
                                    last = "D";
                                else if (lastChar == 14)
                                    last = "E";
                                else if (lastChar == 15)
                                    last = "F";
                                else if (lastChar == 16)
                                    last = "G";
                                else if (lastChar == 17)
                                    last = "H";
                                else if (lastChar == 18)
                                    last = "J";
                                else if (lastChar == 19)
                                    last = "K";
                                else if (lastChar == 20)
                                    last = "L";
                                else if (lastChar == 21)
                                    last = "M";
                                else if (lastChar == 22)
                                    last = "N";
                                else if (lastChar == 23)
                                    last = "P";
                                else
                                {
                                    _errorInspectionId = "Over Number";
                                }

                                #endregion

                                if (_errorInspectionId == "")
                                {
                                    newInsLotNo =
                                        lotNo.Remove(lotNo.Length - 1, 1) + last;
                                }
                                else
                                    newInsLotNo = string.Empty;
                            }
                        }
                        else newInsLotNo = string.Empty;
                    }
                    else newInsLotNo = string.Empty;

                    #endregion
                }
                else if (chkLot == 1)
                {
                    #region inspecionLotNo

                    if (!string.IsNullOrWhiteSpace(lotNo))
                    {
                        int lastChar = 0;
                        string last = string.Empty;

                        string test = ((lotNo.Length) > 0 ? lotNo.Substring((lotNo.Length - 1), 1) : "");
                        if (test != "0" && test != "1" && test != "2" && test != "3" && test != "4" && test != "5"
                            && test != "6" && test != "7" && test != "8" && test != "9")
                        {
                            if (test == "R")
                                last = "T";
                            else if (test == "T")
                                last = "U";
                            else if (test == "U")
                                last = "V";
                            else if (test == "V")
                                last = "W";
                            else
                            {
                                _errorInspectionId = "Over Number";
                            }

                            if (_errorInspectionId == "")
                            {
                                newInsLotNo =
                                  lotNo.Remove(lotNo.Length - 1, 1) + last;
                            }
                            else
                                newInsLotNo = string.Empty;
                        }
                        else
                        {
                            char ch = lotNo[lotNo.Length - 1];

                            if (char.IsDigit(ch))
                            {
                                lastChar = (Convert.ToInt32(ch.ToString()) - 1);
                                if (totalIns > 0)
                                {
                                    // Increase lot number to last finished inspection lot.
                                    while (lastChar <= totalIns)
                                    {
                                        ++lastChar;
                                    }
                                }
                                else ++lastChar; // No finished inspection lot.

                                if (lastChar > 0)
                                {
                                    #region last String

                                    if (lastChar == 1)
                                        last = "R";
                                    else if (lastChar == 2)
                                        last = "T";
                                    else if (lastChar == 3)
                                        last = "U";
                                    else if (lastChar == 4)
                                        last = "V";
                                    else if (lastChar == 5)
                                        last = "W";
                                    else
                                    {
                                        _errorInspectionId = "Over Number";
                                    }

                                    #endregion

                                    if (_errorInspectionId == "")
                                    {
                                        newInsLotNo =
                                            lotNo.Remove(lotNo.Length - 1, 1) + last;
                                    }
                                    else
                                        newInsLotNo = string.Empty;
                                }

                            }
                            else newInsLotNo = string.Empty;
                        }
                    }
                    else newInsLotNo = string.Empty;

                    #endregion
                }
                else
                {
                    _errorInspectionId = "Item Lot No. isn't True";
                }

                if (string.IsNullOrEmpty(_errorInspectionId))
                    return newInsLotNo;
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.Err();
                return string.Empty;
            }
        }

        #endregion

        //เพิ่มใหม่ INS_REWAREHOUSE ใช้ในหน้า ProcessControlPage
        public bool INS_REWAREHOUSE(string P_INSOLD, string P_DEFECTID, string P_TESTID, string P_INSNEW)
        {
            return InspectionDataService.Instance.INS_REWAREHOUSE(P_INSOLD, P_DEFECTID, P_TESTID, P_INSNEW);
        }

        #endregion

        public void INS_INSERTCONFIRMSTARTING(decimal? P_CONFIRMSTART)
        {
            try
            {
                if (!string.IsNullOrEmpty(_inspecionLotNo))
                    InspectionDataService.Instance.INS_INSERTCONFIRMSTARTING(_inspectionId, _inspecionLotNo, P_CONFIRMSTART);
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        }

        public decimal? INS_GET100MDEFECTPOINT(string counterValue)
        {
            try
            {
                decimal? get100 = 0;

                decimal len1 = Convert.ToDecimal(counterValue);
                decimal? len2 = new decimal?();

                if (!string.IsNullOrEmpty(_inspecionLotNo))
                {
                    "---INS_GET100MDEFECTPOINT---".Info();
                    _inspecionLotNo.Info();
                    _defectId.Info();
                    len1.ToString().Info();
                    len2.ToString().Info();

                    get100 = InspectionDataService.Instance.INS_GET100MDEFECTPOINT(_inspecionLotNo, _defectId, len1, len2);

                    ("R_POINT = " + get100).Info();
                    "--------------------------".Info();
                }

                return get100;
            }
            catch (Exception ex)
            {
                ex.Message.Err();
                return null;
            }
        }

        public decimal? INS_GET100MDEFECTPOINTLongDefect(decimal len2)
        {
            try
            {
                decimal? get100 = 0;

                //New 24/8/22
                _longDefectEnd100M = len2;

                if (!string.IsNullOrEmpty(_inspecionLotNo))
                {
                    "---INS_GET100MDEFECTPOINT---".Info();
                    _inspecionLotNo.Info();
                    _defectId.Info();
                    _longDefectStart100M.ToString().Info();
                    _longDefectEnd100M.ToString().Info();

                    get100 = InspectionDataService.Instance.INS_GET100MDEFECTPOINT(_inspecionLotNo, _defectId, _longDefectStart100M, _longDefectEnd100M);

                    ("R_POINT = " + get100).Info();
                    "--------------------------".Info();
                }

                #region Reset value

                _longDefectStart100M = new decimal?();
                _longDefectEnd100M = new decimal?();

                #endregion

                return get100;
            }
            catch (Exception ex)
            {
                ex.Message.Err();
                return null;
            }
        }

        //New 18/10/22
        public decimal? INS_GETRESETSTARTLENGTH()
        {
            decimal? resetStartLength = null;

            try
            {
                if (_customerID == "09")
                {
                    string itmCode = (ReAdjustMode != ReAdjustMode.Diff) ? _itemCode : _diffItemCode;

                    resetStartLength = InspectionDataService.Instance.INS_GETRESETSTARTLENGTH(itmCode, _customerID);

                    if (resetStartLength != null)
                    {
                        _resetStartLength = resetStartLength;
                    }
                    else
                    {
                        _resetStartLength = null;
                    }
                }

                return resetStartLength;
            }
            catch (Exception ex)
            {
                ex.Message.Err();
                return null;
            }
        }

        #region Public Proeprties

        /// <summary>
        /// Gets or sets machine.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public InspectionMCItem Machine
        {
            get { return _machine; }
            set
            {
                _machine = value;
            }
        }
        /// <summary>
        /// Gets or sets current user.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public LogInResult CurrentUser
        {
            get { return _currUser; }
            set
            {
                _currUser = value;
            }
        }
        /// <summary>
        /// Gets or sets curent session state.
        /// </summary>
        [XmlAttribute]
        public InspectionSessionState State 
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets Finishing Lot No.
        /// </summary>
        [XmlAttribute]
        public string FinishingLotNo 
        {
            get { return _finishingLotNo; }
            set
            {
                if (_finishingLotNo != value)
                {
                    _finishingLotNo = value;

                    if (this.ReAdjustMode == Models.ReAdjustMode.None &&
                        this.ReProcessMode == Models.ReProcessMode.None)
                    {
                        VerifyFinishingData();
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets Item Code (or Grey Code).
        /// </summary>
        [XmlAttribute]
        public string ItemCode 
        {
            get { return _itemCode; }
            set
            {
                if (_itemCode != value)
                {
                    _itemCode = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets diff item code.
        /// </summary>
        [XmlAttribute]
        public string DiffItemCode
        {
            get 
            {
                if (this.ReAdjustMode != ReAdjustMode.Diff)
                    return string.Empty;
                return _diffItemCode;
            }
            set
            {
                if (this.ReAdjustMode != ReAdjustMode.Diff)
                    return;
                if (_diffItemCode != value)
                {
                    _diffItemCode = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets Overall Length (From Finishing process).
        /// </summary>
        [XmlAttribute]
        public decimal OverallLength
        {
            get { return _overallLength; }
            set
            {
                if (_overallLength != value)
                {
                    _overallLength = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets Actual Length (The rest length on current inspection process).
        /// </summary>
        [XmlAttribute]
        public decimal ActualLength
        {
            get { return _actualLength; }
            set
            {
                if (_actualLength != value)
                {
                    _actualLength = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets Total Inspection Lot that finished.
        /// </summary>
        [XmlAttribute]
        public decimal TotalIns
        {
            get { return _totalIns; }
            set
            {
                if (_totalIns != value)
                {
                    _totalIns = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets Inspecion Lot No.
        /// </summary>
        [XmlAttribute]
        public string InspecionLotNo 
        {
            get { return _inspecionLotNo; }
            set
            {
                if (_inspecionLotNo != value)
                {
                    _inspecionLotNo = value;
                    if (this.ReAdjustMode != Models.ReAdjustMode.None ||
                        this.ReProcessMode != Models.ReProcessMode.None)
                    {
                        VerifyInspectionData();
                    }
                }
            }
        }
        [XmlAttribute]
        public string LOADTYPE
        {
            get { return _loadingType; }
            set
            {
                if (_loadingType != value)
                {
                    _loadingType = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets inspection type.
        /// </summary>
        [XmlAttribute]
        public InspectionTypes InspectionType
        {
            get { return _inspectionType; }
            set
            {
                if (_inspectionType != value)
                {
                    _inspectionType = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets re adjust mode.
        /// </summary>
        public ReAdjustMode ReAdjustMode
        {
            get { return _reAdjustMode; }
            set
            {
                if (_reAdjustMode != value)
                {
                    _reAdjustMode = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets Re process mode.
        /// </summary>
        [XmlAttribute]
        public ReProcessMode ReProcessMode
        {
            get { return _reProcessMode; }
            set
            {
                if (_reProcessMode != value)
                {
                    _reProcessMode = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets Inspection Id (PK).
        /// </summary>
        [XmlAttribute]
        public string InspectionId
        {
            get { return _inspectionId; }
            set
            {
                if (_inspectionId != value)
                {
                    _inspectionId = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        [XmlAttribute]
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        [XmlAttribute]
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                }
            }
        }
        /// <summary>
        /// Gets start date time int string.
        /// </summary>
        [XmlIgnore]
        public string StartDateString
        {
            get 
            {
                if (_startDate == DateTime.MinValue || _startDate == DateTime.MaxValue)
                    return string.Empty;
                return _startDate.ToString("dd/MM/yyyy HH:mm:ss",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
        }
        /// <summary>
        /// Gets end date time int string.
        /// </summary>
        [XmlIgnore]
        public string EndDateString
        {
            get
            {
                if (_endDate == DateTime.MinValue || _endDate == DateTime.MaxValue)
                    return string.Empty;
                return _endDate.ToString("dd/MM/yyyy HH:mm:ss",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
        }
        /// <summary>
        /// Gets Original Lot No.
        /// </summary>
        [XmlIgnore]
        public string PEInsLotNo { get { return _peInsLot; } }
        
        /// <summary>
        /// Gets or sets CustomerType.
        /// </summary>
        [XmlAttribute]
        public string CustomerType
        {
            get { return _customerType; }
            set
            {
                if (_customerType != value)
                {
                    _customerType = value;
                }
            }
        }

        /// <summary>
        /// Gets current inspection test by item code.
        /// </summary>
        [XmlIgnore]
        public InspectionTests InspectionTests
        {
            get { return _inspectionTest; }
            set { _inspectionTest = value; }
        }

        // เพิ่มตัว check InspectionId 
        [XmlAttribute]
        public string ErrorInspectionId
        {
            get { return _errorInspectionId; }
            set
            {
                if (_errorInspectionId != value)
                {
                    _errorInspectionId = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets Group.
        /// </summary>
        [XmlAttribute]
        public string OPERATOR_GROUP
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                }
            }
        }


        //เพิ่ม RESETSTARTLENGTH 18/10/22
        [XmlAttribute]
        public decimal? RESETSTARTLENGTH
        {
            get { return _resetStartLength; }
            set
            {
                if (_resetStartLength != value)
                {
                    _resetStartLength = value;
                }
            }
        }
        #endregion

        #region Public Events

        /// <summary>
        /// OnStateChanged event.
        /// </summary>
        public event EventHandler OnStateChanged;

        #endregion
    }

    #endregion

}
