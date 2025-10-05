#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using NLib;
using NLib.Data;
using NLib.Logs;
using NLib.Xml;

using LuckyTex.Configs;
using LuckyTex.Domains;
using LuckyTex.Models;

using System.Data;
using System.Data.OracleClient;

#endregion

namespace LuckyTex.Services
{
    #region Base Data Service class

    /// <summary>
    /// Base Data Service Abstract class.
    /// </summary>
    public abstract class BaseDataService
    {
        #region Protected Methods

        /// <summary>
        /// Checks is database connected.
        /// </summary>
        /// <returns>Returns true if database is connected</returns>
        protected bool HasConnection()
        {
            bool result = false;

            if (!DatabaseManager.Instance.IsConnected)
                DatabaseManager.Instance.Start();

            result = DatabaseManager.Instance.IsConnected;

            return result;
        }

        // เพิ่ม con SQL D365
        protected bool HasConnectionD365()
        {
            bool result = false;

            if (!DatabaseManager_D365.Instance.IsConnected)
                DatabaseManager_D365.Instance.Start();

            result = DatabaseManager_D365.Instance.IsConnected;

            return result;
        }


        /// <summary>
        /// Gets Machines by process id.
        /// </summary>
        /// <param name="iProcessId">The process id.</param>
        /// <returns>Returns list of mahines.</returns>
        protected List<GETMACHINELISTBYPROCESSIDResult> GetMachines(int iProcessId)
        {
            List<GETMACHINELISTBYPROCESSIDResult> results = null;
            if (!HasConnection())
                return results;

            GETMACHINELISTBYPROCESSIDParameter dbPara = new GETMACHINELISTBYPROCESSIDParameter();
            dbPara.P_PROCESSID = iProcessId.ToString();
            try
            {
                results = DatabaseManager.Instance.GETMACHINELISTBYPROCESSID(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
                results = null;
            }

            if (null != results)
            {
                // Sort by m/c name
                results.Sort(
                    (GETMACHINELISTBYPROCESSIDResult x, GETMACHINELISTBYPROCESSIDResult y) =>
                    {
                        if (x == null || string.IsNullOrWhiteSpace(x.MCNAME))
                        {
                            if (y == null || string.IsNullOrWhiteSpace(y.MCNAME)) 
                                return 0;
                            return -1;
                        }
                        if (y == null || string.IsNullOrWhiteSpace(y.MCNAME))
                            return 0;
                        if (x.MCNAME.Length < y.MCNAME.Length)
                            return -1;
                        else if (x.MCNAME.Length > y.MCNAME.Length)
                            return 1;
                        else return x.MCNAME.CompareTo(y.MCNAME);
                    });
            }

            return results;
        }

        /// <summary>
        /// Gets process 
        /// </summary>
        protected List<MASTER_AIRBAGPROCESSLISTResult> GetProcess()
        {
            List<MASTER_AIRBAGPROCESSLISTResult> results = null;
            if (!HasConnection())
                return results;

            MASTER_AIRBAGPROCESSLISTParameter dbPara = new MASTER_AIRBAGPROCESSLISTParameter();
            
            try
            {
                results = DatabaseManager.Instance.MASTER_AIRBAGPROCESSLIST(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
                results = null;
            }

            return results;
        }

        /// <summary>
        /// Gets Positon 
        /// </summary>
        protected List<MASTER_POSITIONLISTResult> GetPositon()
        {
            List<MASTER_POSITIONLISTResult> results = null;
            if (!HasConnection())
                return results;

            MASTER_POSITIONLISTParameter dbPara = new MASTER_POSITIONLISTParameter();

            try
            {
                results = DatabaseManager.Instance.MASTER_POSITIONLIST(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
                results = null;
            }

            return results;
        }

        /// <summary>
        /// Gets Positon 
        /// </summary>
        protected List<ITM_GETITEMYARNLISTResult> GetItemYarn()
        {
            List<ITM_GETITEMYARNLISTResult> results = null;
            if (!HasConnection())
                return results;

            ITM_GETITEMYARNLISTParameter dbPara = new ITM_GETITEMYARNLISTParameter();

            try
            {
                results = DatabaseManager.Instance.ITM_GETITEMYARNLIST(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
                results = null;
            }

            return results;
        }

        #endregion
    }

    #endregion
}

// Template class
namespace LuckyTex.Services
{
    #region XXX Data Service
    
    /// <summary>
    /// The data service for XXX process.
    /// </summary>
    public class DataService : BaseDataService
    {
        #region Singelton

        private static DataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static DataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(DataService))
                    {
                        _instance = new DataService();
                    }

                }
                return _instance;
            }
        }

        #endregion
    
        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private DataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~DataService()
        {
        }

        #endregion

        #region Public Methods

        #endregion
    }

    #endregion
}

namespace LuckyTex.Services
{
    #region InspectionDataService

    /// <summary>
    /// The data service for Inspection process.
    /// </summary>
    public class InspectionDataService : BaseDataService
    {
        #region Singelton

        private static InspectionDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static InspectionDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(InspectionDataService))
                    {
                        _instance = new InspectionDataService();
                    }

                }
                return _instance;
            }
        }

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private InspectionDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~InspectionDataService()
        {
        }

        #endregion

        #region Public Methods

        #region Get related informtion methods
        
        /// <summary>
        /// Gets all Inspection Machines.
        /// </summary>
        /// <returns>Returns inspection machine.</returns>
        public List<InspectionMCItem> GetMachines()
        {
            List<InspectionMCItem> results = new List<InspectionMCItem>();

            // Inspection Process ID = 8
            List<GETMACHINELISTBYPROCESSIDResult> dbResults = this.GetMachines(8);
            if (null == dbResults || dbResults.Count <= 0)
                return results;

            int mcNo = 1;
            InspectionMCItem inst = null;
            foreach (GETMACHINELISTBYPROCESSIDResult dbResult in dbResults)
            {
                inst = new InspectionMCItem();

                inst.MCNo = mcNo;
                inst.MCId = dbResult.MACHINEID;
                inst.DisplayName = dbResult.MCNAME.TrimEnd();

                results.Add(inst);

                ++mcNo;
            }

            // Add special 2 machine(s) for display on another page.
            inst = new InspectionMCItem();
            inst.MCNo = mcNo;
            inst.MCId = "A";
            inst.DisplayName = "Attach File";
            results.Add(inst);
            ++mcNo;

            inst = new InspectionMCItem();
            inst.MCNo = mcNo;
            inst.MCId = "W";
            inst.DisplayName = "Re-Print Inspection Report";
            results.Add(inst);
            ++mcNo;

            return results;
        }

        // เพิ่มเพื่อเรียก MC อย่างเดียว
        /// <summary>
        /// Gets all Inspection Machines.
        /// </summary>
        /// <returns>Returns inspection machine.</returns>
        public List<InspectionMCItem> GetMachinesData()
        {
            List<InspectionMCItem> results = new List<InspectionMCItem>();

            // Inspection Process ID = 8
            List<GETMACHINELISTBYPROCESSIDResult> dbResults = this.GetMachines(8);
            if (null == dbResults || dbResults.Count <= 0)
                return results;

            int mcNo = 1;
            InspectionMCItem inst = null;
            foreach (GETMACHINELISTBYPROCESSIDResult dbResult in dbResults)
            {
                inst = new InspectionMCItem();

                inst.MCNo = mcNo;
                inst.MCId = dbResult.MACHINEID;
                inst.DisplayName = dbResult.MCNAME.TrimEnd();

                results.Add(inst);

                ++mcNo;
            }

            return results;
        }

        /// <summary>
        /// Get Finishing Data.
        /// </summary>
        /// <param name="finLotNo">The Finishing Lot No.</param>
        /// <returns>Returns instance of FinishingInfo.</returns>
        public FinishingInfo GetFinishingData(string finLotNo)
        {
            #region Init Instance
            
            FinishingInfo result = new FinishingInfo();
            result.FinishingLotNo = (!string.IsNullOrWhiteSpace(finLotNo)) ?
                finLotNo.Trim() : string.Empty;

            // เพิ่ม CUSTOMERID
            result.CUSTOMERID = string.Empty;

            result.ItemCode = string.Empty;
            result.OverallLength = decimal.Zero;
            result.ActualLength = decimal.Zero;
            result.TotalIns = decimal.Zero;
            
            #endregion

            #region Check Parameter and Connection
            
            if (string.IsNullOrWhiteSpace(finLotNo))
            {
                return result;
            }

            if (!HasConnection())
                return result;

            #endregion

            #region Get Finishing Data

            try
            {
                GETFINISHINGDATAParameter dbPara = new GETFINISHINGDATAParameter();
                dbPara.P_FINISHINGLOT = finLotNo;
                List<GETFINISHINGDATAResult> dbResults = null;

                dbResults = DatabaseManager.Instance.GETFINISHINGDATA(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    foreach (GETFINISHINGDATAResult dbResult in dbResults)
                    {
                        bool match =
                            string.Compare(dbResult.ITEMLOT.Trim(), finLotNo.Trim(), true) == 0;
                        if (match)
                        {
                            result.FinishingLotNo = (!string.IsNullOrWhiteSpace(dbResult.ITEMLOT)) ?
                                dbResult.ITEMLOT.Trim() : string.Empty;
                            result.ItemCode = (!string.IsNullOrWhiteSpace(dbResult.ITEMCODE)) ?
                                dbResult.ITEMCODE.Trim() : string.Empty; 
                            result.OverallLength = (dbResult.FINISHLENGTH.HasValue) ?
                                dbResult.FINISHLENGTH.Value : decimal.Zero;

                            // เพิ่ม CUSTOMERID
                            result.CUSTOMERID = (!string.IsNullOrWhiteSpace(dbResult.CUSTOMERID)) ?
                                dbResult.CUSTOMERID.Trim() : string.Empty;

                            result.PARTNO = (!string.IsNullOrWhiteSpace(dbResult.PARTNO)) ?
                                dbResult.PARTNO.Trim() : string.Empty;

                            result.FINISHLOT = (!string.IsNullOrWhiteSpace(dbResult.FINISHLOT)) ?
                                dbResult.FINISHLOT.Trim() : string.Empty;

                            result.FINISHINGPROCESS = (!string.IsNullOrWhiteSpace(dbResult.FINISHINGPROCESS)) ?
                               dbResult.FINISHINGPROCESS.Trim() : string.Empty;

                            result.REPROCESS = (!string.IsNullOrWhiteSpace(dbResult.REPROCESS)) ?
                                dbResult.REPROCESS.Trim() : string.Empty;

                            result.SND_BARCODE = (!string.IsNullOrWhiteSpace(dbResult.SND_BARCODE)) ?
                              dbResult.SND_BARCODE.Trim() : string.Empty;

                            result.ITM_WEAVING = (!string.IsNullOrWhiteSpace(dbResult.ITM_WEAVING)) ?
                                dbResult.ITM_WEAVING.Trim() : string.Empty; 

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            #endregion

            #region Get Current Ins Data

            try
            {
                GETCURRENTINSDATAParameter dbPara = new GETCURRENTINSDATAParameter();
                dbPara.P_FINISHINGLOT = finLotNo;
                GETCURRENTINSDATAResult dbResult = null;
                dbResult = DatabaseManager.Instance.GETCURRENTINSDATA(dbPara);
                if (null != dbResult)
                {
                    result.ActualLength = (dbResult.ACTUALLENGTH.HasValue) ?
                        dbResult.ACTUALLENGTH.Value : decimal.Zero;
                    result.TotalIns = (dbResult.TOTALINS.HasValue) ? 
                        dbResult.TOTALINS.Value : decimal.Zero;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            #endregion

            #region Check Result

            if (null != result)
            {
                if (result.ActualLength <= 0)
                {
                    // If Actual length is zero set to overall
                    result.ActualLength = result.OverallLength;
                }
            }

            #endregion

            return result;
        }
        /// <summary>
        /// Get Inspection Test List By Item Code.
        /// </summary>
        /// <param name="itemCode">The Item Code. Required.</param>
        /// <returns>Returns InspectionTests instance.</returns>
        public InspectionTests GetInspectionTestListByItemCode(string itemCode)
        {
            InspectionTests result = null;

            if (string.IsNullOrWhiteSpace(itemCode))
                return null;
            if (!HasConnection())
                return null;

            result = new InspectionTests();

            GETINSPECTIONLISTTESTBYITMCODEParameter dbPara = new GETINSPECTIONLISTTESTBYITMCODEParameter();
            List<GETINSPECTIONLISTTESTBYITMCODEResult> dbResults = null;
            try
            {
                // ปรับส่งค่า Para ก่อนเรียกใช้
                dbPara.P_ITMCODE = itemCode.Trim();

                dbResults = DatabaseManager.Instance.GETINSPECTIONLISTTESTBYITMCODE(dbPara);
                
                if (null != dbResults && dbResults.Count > 0)
                {
                    GETINSPECTIONLISTTESTBYITMCODEResult dbResult = dbResults[0];
                    // DENSITY
                    result.Densities.F.Enabled =
                        (null != dbResult.DENSITY_F) ? true : false;
                    result.Densities.W.Enabled =
                        (null != dbResult.DENSITY_W) ? true : false;                        
                    // WIDTH
                    result.Widths.All.Enabled =
                        (null != dbResult.WIDTH_ALL) ? true : false;
                    result.Widths.Pin.Enabled =
                        (null != dbResult.WIDTH_PIN) ? true : false;
                    result.Widths.Coat.Enabled =
                        (null != dbResult.WIDTH_COAT) ? true : false;
                    // TRIM
                    result.Trims.L.Enabled =
                        (null != dbResult.TRIM_L) ? true : false;
                    result.Trims.R.Enabled =
                        (null != dbResult.TRIM_R) ? true : false;
                    // FLOPPY
                    result.Floppies.L.Enabled =
                        (null != dbResult.FLOPPY_L) ? true : false;
                    result.Floppies.R.Enabled =
                        (null != dbResult.FLOPPY_R) ? true : false;
                    // HARDNESS
                    result.Hardnesses.L.Enabled =
                        (null != dbResult.HARDNESS_L) ? true : false;
                    result.Hardnesses.C.Enabled =
                        (null != dbResult.HARDNESS_C) ? true : false;
                    result.Hardnesses.R.Enabled =
                        (null != dbResult.HARDNESS_R) ? true : false;
                    // UNWINDER
                    result.Unwinders.Set.Enabled =
                        (null != dbResult.UNWINDER) ? true : false;
                    result.Unwinders.Actual.Enabled =
                        (null != dbResult.UNWINDER) ? true : false;
                    // WINDER
                    result.Winders.Set.Enabled =
                        (null != dbResult.WINDER) ? true : false;
                    result.Winders.Actual.Enabled =
                        (null != dbResult.WINDER) ? true : false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        // เพิ่มเพื่อเรียก Process อย่างเดียว
        /// <summary>
        /// Gets all Process.
        /// </summary>
        /// <returns>Returns inspection machine.</returns>
        public List<ProcessItem> GetProcessData()
        {
            List<ProcessItem> results = new List<ProcessItem>();

            // Inspection Process ID = 8
            List<MASTER_AIRBAGPROCESSLISTResult> dbResults = this.GetProcess();
            if (null == dbResults || dbResults.Count <= 0)
                return results;

            ProcessItem inst = null;
            foreach (MASTER_AIRBAGPROCESSLISTResult dbResult in dbResults)
            {
                inst = new ProcessItem();

                inst.PROCESSID = dbResult.PROCESSID;
                inst.PROCESSDESCRIPTION = dbResult.PROCESSDESCRIPTION.TrimEnd();

                results.Add(inst);
            }

            return results;
        }

        // เพิ่มเพื่อเรียก Positon อย่างเดียว
        /// <summary>
        /// Gets all Positon.
        /// </summary>
        /// <returns>Returns inspection machine.</returns>
        public List<PositonItem> GetPositonData()
        {
            List<PositonItem> results = new List<PositonItem>();

            List<MASTER_POSITIONLISTResult> dbResults = this.GetPositon();
            if (null == dbResults || dbResults.Count <= 0)
                return results;

            PositonItem inst = null;
            foreach (MASTER_POSITIONLISTResult dbResult in dbResults)
            {
                inst = new PositonItem();

                inst.POSITIONLEVEL = dbResult.POSITIONLEVEL;
                inst.POSITIONNAME = dbResult.POSITIONNAME.TrimEnd();

                results.Add(inst);
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="INS_LOT"></param>
        /// <param name="STARTDATE"></param>
        /// <returns></returns>
        public List<INS_CUTSAMPLELIST> GetINS_GETCUTSAMPLELIST(string INS_LOT, DateTime? STARTDATE)
        {
            List<INS_CUTSAMPLELIST> result = null;

            if (string.IsNullOrWhiteSpace(INS_LOT))
                return null;

            if (!HasConnection())
                return null;

            result = new List<INS_CUTSAMPLELIST>();

            INS_GETCUTSAMPLELISTParameter dbPara = new INS_GETCUTSAMPLELISTParameter();
            List<INS_GETCUTSAMPLELISTResult> dbResults = null;
            try
            {
                dbPara.P_INS_LOT = INS_LOT.Trim();
                dbPara.P_STARTDATE = STARTDATE;

                dbResults = DatabaseManager.Instance.INS_GETCUTSAMPLELIST(dbPara);

                if (null != dbResults && dbResults.Count > 0)
                {

                    INS_CUTSAMPLELIST inst = null;
                    foreach (INS_GETCUTSAMPLELISTResult dbResult in dbResults)
                    {
                        inst = new INS_CUTSAMPLELIST();

                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ORDERNO = dbResult.ORDERNO;
                        inst.CUTLENGTH = dbResult.CUTLENGTH;
                        inst.REMARK = dbResult.REMARK;
                        inst.CUTDATE = dbResult.CUTDATE;
                        inst.CUTBY = dbResult.CUTBY;

                        result.Add(inst);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion

        #region Insert/Update/Clear Process
        
        /// <summary>
        /// Insert Inspection record when Start button press.
        /// </summary>
        /// <param name="finLot">The Finishing Lot No.</param>
        /// <param name="itemCode">The Item Code.</param>
        /// <param name="insLot">The Inspection Lot No.</param>
        /// <param name="mcNo">The machine No. This is machine PK.</param>
        /// <param name="productivityId">The test type (Mass or Test)</param>
        /// <param name="customerId">The customer Id. This is customer PK.</param>
        /// <param name="customerType">The customer type string.</param>
        /// <param name="peInsLot">The PE Inspection Lot No.</param>
        /// <param name="insDate">The Inspection start date.</param>
        /// <param name="insBy">The user that start Inspection.</param>
        /// <param name="reTest"></param>
        /// <returns>Returns string that represent primary key if insert success.</returns>
        public string InsertInspectionProcess(string finLot, string itemCode, string insLot,
            string mcNo, string producttypeId, string customerId, string customerType,
            string peInsLot,
            DateTime insDate, string insBy, string reTest, string LOADTYPE ,string GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(finLot))
                return null;
            if (string.IsNullOrWhiteSpace(itemCode))
                return null;
            if (string.IsNullOrWhiteSpace(insLot))
                return null;
            if (string.IsNullOrWhiteSpace(mcNo))
                return null;
            //if (string.IsNullOrWhiteSpace(producttypeId))  { return null; }
            if (string.IsNullOrWhiteSpace(insBy))
                return null;

            if (!HasConnection())
                return result;

            INSERTINSPECTIONPROCESSParameter dbPara = new INSERTINSPECTIONPROCESSParameter();
            dbPara.P_FINISHLOT = finLot;
            dbPara.P_ITMCODE = itemCode;
            dbPara.P_INSLOT = insLot;
            dbPara.P_CUSTOMERID = customerId;
            dbPara.P_CUSTOMERTYPE = customerType;
            dbPara.P_MCNO = mcNo;
            dbPara.P_PEINSPECTIONLOT = peInsLot;
            dbPara.P_PRODUCTTYPEID = producttypeId;
            dbPara.P_RETEST = reTest;
            dbPara.P_STARTDATE = insDate;
            dbPara.P_INSPECTEDBY = insBy;
            dbPara.P_LOADTYPE = LOADTYPE;
            dbPara.P_GROUP = GROUP;
            //dbPara.P_FLAG = "Y";


            INSERTINSPECTIONPROCESSResult dbResult = null;
            try
            { 
                dbResult = DatabaseManager.Instance.INSERTINSPECTIONPROCESS(dbPara);
                if (null != dbResult)
                {
                    if (!string.IsNullOrWhiteSpace(dbResult.R_INSID))
                    {
                        result = dbResult.R_INSID;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        // เพิ่ม ส่งค่า nlength
      /// <summary>
      /// 
      /// </summary>
      /// <param name="insLotNo"></param>
      /// <param name="startDate"></param>
      /// <param name="customerType"></param>
      /// <param name="length"></param>
      /// <param name="nlength"></param>
      /// <param name="grade"></param>
      /// <param name="LOADTYPE"></param>
        public void UpdateInspectionProcess(string insLotNo, DateTime startDate,
            string customerType,
            decimal length, decimal nlength, string grade, string LOADTYPE, string GROUP, string FLAG)
        {
            if (string.IsNullOrWhiteSpace(insLotNo))
                return;

            if (!HasConnection())
                return;

            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();
            dbPara.P_STARTDATE = startDate;
            dbPara.P_ENDDATE = DateTime.Now;
            dbPara.P_CUSTOMERTYPE = customerType;

            if (FLAG != string.Empty)
                dbPara.P_FLAG = FLAG;
            else
                dbPara.P_FLAG = "Y";

            dbPara.P_GLENGHT = length;
            dbPara.P_GRADE = grade;
            dbPara.P_INSLOT = insLotNo;
            dbPara.P_NLENGTH = nlength;
            dbPara.P_LOADTYPE = LOADTYPE;
            dbPara.P_GROUP = GROUP;

            //dbPara.P_PACK = "N";

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        public void FinishInspectionProcess(string insLotNo, DateTime startDate)
        {
            if (string.IsNullOrWhiteSpace(insLotNo))
                return;

            if (!HasConnection())
                return;

            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();
            dbPara.P_STARTDATE = startDate;
            dbPara.P_ENDDATE = DateTime.Now;

            dbPara.P_FLAG = "Y";

            dbPara.P_INSLOT = insLotNo;

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        public void FinishInspectionProcess(string insLotNo, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(insLotNo))
                return;

            if (!HasConnection())
                return;

            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();
            dbPara.P_STARTDATE = startDate;
            dbPara.P_ENDDATE = endDate;

            dbPara.P_FLAG = "Y";

            dbPara.P_INSLOT = insLotNo;

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="insLotNo"></param>
        /// <param name="startDate"></param>
        /// <param name="customerType"></param>
        /// <param name="length"></param>
        /// <param name="nlength"></param>
        /// <param name="gweight"></param>
        /// <param name="nweight"></param>
        /// <param name="grade"></param>
        /// <param name="LOADTYPE"></param>
        public void UpdateInspectionProcess(string insLotNo, DateTime startDate,
            string customerType,
            decimal length, decimal nlength, decimal gweight, decimal nweight, string grade, string LOADTYPE, string GROUP)
        {
            if (string.IsNullOrWhiteSpace(insLotNo))
                return;

            if (!HasConnection())
                return;

            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();
            dbPara.P_STARTDATE = startDate;
            dbPara.P_ENDDATE = DateTime.Now;
            dbPara.P_CUSTOMERTYPE = customerType;

            dbPara.P_FLAG = "Y";

            dbPara.P_GRADE = grade;
            dbPara.P_INSLOT = insLotNo;

            dbPara.P_GLENGHT = length;
            dbPara.P_NLENGTH = nlength;

            dbPara.P_GWEIGHT = gweight;
            dbPara.P_NWEIGHT = nweight;
            dbPara.P_LOADTYPE = LOADTYPE;
            dbPara.P_GROUP = GROUP;
            //dbPara.P_PACK = "N";

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        public void EditInspectionProcess(string insLotNo, DateTime startDate,
            string customerType,
            decimal length, decimal nlength, decimal gweight, decimal nweight, string grade, string LOADTYPE, string GROUP)
        {
            if (string.IsNullOrWhiteSpace(insLotNo))
                return;

            if (!HasConnection())
                return;

            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();
            dbPara.P_STARTDATE = startDate;
            
            dbPara.P_CUSTOMERTYPE = customerType;

            dbPara.P_FLAG = "Y";

            dbPara.P_GRADE = grade;
            dbPara.P_INSLOT = insLotNo;

            dbPara.P_GLENGHT = length;
            dbPara.P_NLENGTH = nlength;

            dbPara.P_GWEIGHT = gweight;
            dbPara.P_NWEIGHT = nweight;
            dbPara.P_LOADTYPE = LOADTYPE;
            dbPara.P_GROUP = GROUP;

            //dbPara.P_PACK = "N";

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        // เพิ่มขึ้นมาสำหรับ Update Defect FileName ใน Inspection
        /// <summary>
        /// Update Inspection Defect FileName
        /// </summary>
        /// <param name="insLotNo"></param>
        /// <param name="startDate"></param>
        /// <param name="defectFile"></param>
        /// <returns></returns>
        public bool UpdateInspectionDefectFileNameProcess(string insLotNo, DateTime startDate, string defectFile)
        {
            bool chkUpdate = false;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return chkUpdate;

            if (!HasConnection())
                return chkUpdate;

            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();
            dbPara.P_INSLOT = insLotNo;
            dbPara.P_STARTDATE = startDate;
            dbPara.P_DEFECTFILE = defectFile;

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);

                if (dbResult != null)
                {
                    chkUpdate = true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkUpdate;
        }

        /// <summary>
        /// Update Weights.
        /// </summary>
        /// <param name="insLotNo">The inspection lot number.</param>
        /// <param name="startDate">The inspection lot start date.</param>
        /// <param name="GW">The gross weight</param>
        /// <param name="NW">The net weight.</param>
        public bool UpdateWeights(string insLotNo, DateTime startDate,
            string producttypeID,
            string customerType,string loadType,
            decimal GW, decimal NW)
        {
            bool chkUpdate = true;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return false;

            if (!HasConnection())
                return false;

            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();

            // เพิ่ม P_INSLOT เป็น PKID คู่กับ P_STARTDATE
            dbPara.P_INSLOT = insLotNo;
            dbPara.P_STARTDATE = startDate;
            dbPara.P_PRODUCTTYPEID = producttypeID;
            dbPara.P_CUSTOMERTYPE = customerType;
            dbPara.P_LOADTYPE = loadType;
            dbPara.P_GWEIGHT = GW;
            dbPara.P_NWEIGHT = NW;

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);

                if (dbResult != null)
                    chkUpdate = true;
            }
            catch (Exception ex)
            {
                ex.Err();
                chkUpdate = false;
            }

            return chkUpdate;
        }

        /// <summary>
        /// Add Remark.
        /// </summary>
        /// <param name="insLotNo">The inspection lot number.</param>
        /// <param name="startDate">The inspection lot start date.</param>
        /// <param name="customerType">The customer type string.</param>
        /// <param name="remark">The grade value.</param>
        public void AddRemark(string insLotNo, DateTime startDate,
            string customerType, string remark)
        {
            if (string.IsNullOrWhiteSpace(insLotNo))
                return;

            if (!HasConnection())
                return;
            
            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();

            dbPara.P_STARTDATE = startDate;
            dbPara.P_INSLOT = insLotNo;

            dbPara.P_CUSTOMERTYPE = customerType;
            dbPara.P_REMARK = remark;

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Gets Inspection Lot Data.
        /// </summary>
        /// <param name="insLotNo">The inspection Lot.</param>
        /// <returns>Returns list of InspectionLotData.</returns>
        public List<InspectionLotData> GetInspectionData(string insLotNo, bool reAdjust)
        {
            List<InspectionLotData> results = null;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return results;

            if (!HasConnection())
                return results;

            GETINSPECTIONDATAParameter dbPara = new GETINSPECTIONDATAParameter();
            List<GETINSPECTIONDATAResult> dbResults = null;
            dbPara.P_INS_LOT = insLotNo;
            try
            {
                dbResults = DatabaseManager.Instance.GETINSPECTIONDATA(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results = new List<InspectionLotData>();
                    foreach (GETINSPECTIONDATAResult dbResult in dbResults)
                    {
                        InspectionLotData inst = new InspectionLotData();

                        inst.ATTACHID = dbResult.ATTACHID;
                        inst.CUSTOMERID = dbResult.CUSTOMERID;

                        if (reAdjust == true)
                        {
                            if (dbResult.DEFECTID != "")
                            {
                                inst.OldDefectID = dbResult.DEFECTID;
                                inst.DEFECTID = string.Empty;
                            }
                        }
                        else
                        {
                            inst.OldDefectID = string.Empty;
                            inst.DEFECTID = dbResult.DEFECTID;
                        }

                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.GRADE = dbResult.GRADE;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.INSPECTEDBY = dbResult.INSPECTEDBY;
                        inst.INSPECTIONID = dbResult.INSPECTIONID;
                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.MCNO = dbResult.MCNO;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PEINSPECTIONLOT = dbResult.PEINSPECTIONLOT;
                        inst.PREITEMCODE = dbResult.PREITEMCODE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.REMARK = dbResult.REMARK;
                        inst.RETEST = dbResult.RETEST;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.SUSPENDDATE = dbResult.SUSPENDDATE;
                        inst.TESTRECORDID = dbResult.TESTRECORDID;

                        // เพิ่ม Load CUSTOMERTYPE
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;
                        inst.DEFECTFILENAME = dbResult.DEFECTFILENAME;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }
        /// <summary>
        /// Gets Inspection Lot Data.
        /// </summary>
        /// <param name="insLotNo">The inspection Lot.</param>
        /// <returns>Returns list of InspectionLotData.</returns>
        public List<InspectionLotData> GetInspectionData(string insLotNo)
        {
            List<InspectionLotData> results = null;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return results;

            if (!HasConnection())
                return results;

            GETINSPECTIONDATAParameter dbPara = new GETINSPECTIONDATAParameter();
            List<GETINSPECTIONDATAResult> dbResults = null;
            dbPara.P_INS_LOT = insLotNo;
            try
            {
                dbResults = DatabaseManager.Instance.GETINSPECTIONDATA(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results = new List<InspectionLotData>();
                    foreach (GETINSPECTIONDATAResult dbResult in dbResults)
                    {
                        InspectionLotData inst = new InspectionLotData();

                        inst.ATTACHID = dbResult.ATTACHID;
                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.DEFECTID = dbResult.DEFECTID;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.GRADE = dbResult.GRADE;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.INSPECTEDBY = dbResult.INSPECTEDBY;
                        inst.INSPECTIONID = dbResult.INSPECTIONID;
                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.MCNO = dbResult.MCNO;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PEINSPECTIONLOT = dbResult.PEINSPECTIONLOT;
                        inst.PREITEMCODE = dbResult.PREITEMCODE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.REMARK = dbResult.REMARK;
                        inst.RETEST = dbResult.RETEST;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.SUSPENDDATE = dbResult.SUSPENDDATE;
                        inst.TESTRECORDID = dbResult.TESTRECORDID;

                        // เพิ่ม Load CUSTOMERTYPE
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;
                        inst.DEFECTFILENAME = dbResult.DEFECTFILENAME;

                        // เพิ่ม LOADINGTYPE
                        inst.LOADTYPE = dbResult.LOADINGTYPE;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        public string GetCUSTOMERID(string finLotNo)
        {
            string results = string.Empty;

            if (string.IsNullOrWhiteSpace(finLotNo))
                return results;

            if (!HasConnection())
                return results;

            GETFINISHINGDATAParameter dbPara = new GETFINISHINGDATAParameter();
            dbPara.P_FINISHINGLOT = finLotNo;
            List<GETFINISHINGDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.GETFINISHINGDATA(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    foreach (GETFINISHINGDATAResult dbResult in dbResults)
                    {
                        if (!string.IsNullOrEmpty(dbResult.CUSTOMERID))
                        {
                            results = dbResult.CUSTOMERID;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }
        /// <summary>
        /// เพิ่มสำหรับ Get Remark
        /// </summary>
        /// <param name="insLotNo"></param>
        /// <returns></returns>
        public List<InspectionLotData> GetInspectionRemark(string insLotNo)
        {
            List<InspectionLotData> results = null;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return results;

            if (!HasConnection())
                return results;

            GETINSPECTIONDATAParameter dbPara = new GETINSPECTIONDATAParameter();
            List<GETINSPECTIONDATAResult> dbResults = null;
            dbPara.P_INS_LOT = insLotNo;
            try
            {
                dbResults = DatabaseManager.Instance.GETINSPECTIONDATA(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results = new List<InspectionLotData>();
                    foreach (GETINSPECTIONDATAResult dbResult in dbResults)
                    {
                        InspectionLotData inst = new InspectionLotData();

                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;
                        inst.REMARK = dbResult.REMARK;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        /// <summary>
        /// Clear current inspection process.
        /// </summary>
        /// <param name="insLotNo">The inspection Lot.</param>
        /// <param name="startDate">The inspection lot start date.</param>
        /// <param name="customerType">The customer type string.</param>
        /// <param name="remark">The remark string.</param>
        /// <param name="operatorId">The clear operator.</param>
        public void ClearCurrentInspection(string insLotNo, DateTime startDate,
            string customerType, string remark, string operatorId)
        {
            if (!HasConnection())
                return;

            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();
            dbPara.P_STARTDATE = startDate;
            dbPara.P_CUSTOMERTYPE = customerType;

            dbPara.P_FLAG = "C"; // Clear flag

            dbPara.P_INSLOT = insLotNo;
            dbPara.P_CLEARREMARK = remark;
            dbPara.P_CLEARBY = operatorId;

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);
                if (null != dbResult)
                {

                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Suspend Inspection Process.
        /// </summary>
        /// <param name="insLotNo">The inspection lot number.</param>
        /// <param name="startDate">The inspection start date (first time).</param>
        /// <param name="operatorId">The suspend operator.</param>
        public void SuspendInspectionProcess(string insLotNo, DateTime startDate,
            string customerType, string operatorId)
        {
            if (string.IsNullOrWhiteSpace(insLotNo))
                return;

            if (!HasConnection())
                return;

            UPDATEINSPECTIONPROCESSParameter dbPara = new UPDATEINSPECTIONPROCESSParameter();
            dbPara.P_STARTDATE = startDate;
            dbPara.P_CUSTOMERTYPE = customerType;

            dbPara.P_FLAG = "S";

            dbPara.P_STARTDATE1 = startDate;
            dbPara.P_INSLOT = insLotNo;
            dbPara.P_SUSPENDDATE = DateTime.Now;
            dbPara.P_SUSPENDBY = operatorId;

            UPDATEINSPECTIONPROCESSResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.UPDATEINSPECTIONPROCESS(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Get Suspend Inspection Process.
        /// </summary>
        /// <param name="machineId">The machine id.</param>
        /// <returns>Returns list of inspection data that suspend.</returns>
        public List<INS_GETMCSUSPENDDATAResult> GetSuspendInspectionProcess(string machineId)
        {
            if (!HasConnection())
                return null;

            INS_GETMCSUSPENDDATAParameter dbPara = new INS_GETMCSUSPENDDATAParameter();
            dbPara.P_INSMC = machineId;
            List<INS_GETMCSUSPENDDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.INS_GETMCSUSPENDDATA(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
                dbResults = null;
            }

            return dbResults;
        }

        // เพิ่มขึ้นมาใหม่ เพื่อหาค่า COREWEIGHT เอาไปคำนวณหาค่า NW ต่อ
        /// <summary>
        /// Gets NW Data.
        /// </summary>
        /// <param name="insLotNo">The inspection Lot.</param>
        /// <returns>Returns list of InspectionLotData.</returns>
        public List<InspectionItemCodeData> GetNW(string itemCode)
        {
            List<InspectionItemCodeData> results = null;

            if (string.IsNullOrWhiteSpace(itemCode))
                return results;

            if (!HasConnection())
                return results;

            GETITEMCODEDATAParameter dbPara = new GETITEMCODEDATAParameter();
            List<GETITEMCODEDATAResult> dbResults = null;
            dbPara.P_ITMCODE = itemCode;
            
            try
            {
                dbResults = DatabaseManager.Instance.GETITEMCODEDATA(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results = new List<InspectionItemCodeData>();
                    foreach (GETITEMCODEDATAResult dbResult in dbResults)
                    {
                        InspectionItemCodeData inst = new InspectionItemCodeData();

                        inst.Coreweight = dbResult.COREWEIGHT;
                        
                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ Search inspection Data
        /// <summary>
        /// Gets Inspection Lot Data.
        /// </summary>
        /// <param name="insLotNo">The inspection Lot.</param>
        /// <returns>Returns list of InspectionLotData.</returns>
        public List<INS_SearchInspectionData> GetINS_SearchinspectionData(string _date, string _mc)
        {
            List<INS_SearchInspectionData> results = null;

            if (!HasConnection())
                return results;

            INS_SEARCHINSPECTIONDATAParameter dbPara = new INS_SEARCHINSPECTIONDATAParameter();
            List<INS_SEARCHINSPECTIONDATAResult> dbResults = null;
            dbPara.P_DATE = _date;
            dbPara.P_MC = _mc;

            try
            {
                dbResults = DatabaseManager.Instance.INS_SEARCHINSPECTIONDATA(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results = new List<INS_SearchInspectionData>();
                    foreach (INS_SEARCHINSPECTIONDATAResult dbResult in dbResults)
                    {
                        INS_SearchInspectionData inst = new INS_SearchInspectionData();

                        inst.ATTACHID = dbResult.ATTACHID;
                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.DEFECTID = dbResult.DEFECTID;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.GRADE = dbResult.GRADE;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.INSPECTEDBY = dbResult.INSPECTEDBY;
                        inst.INSPECTIONID = dbResult.INSPECTIONID;
                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.MCNO = dbResult.MCNO;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PEINSPECTIONLOT = dbResult.PEINSPECTIONLOT;
                        inst.PREITEMCODE = dbResult.PREITEMCODE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.REMARK = dbResult.REMARK;
                        inst.RETEST = dbResult.RETEST;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.SUSPENDDATE = dbResult.SUSPENDDATE;
                        inst.TESTRECORDID = dbResult.TESTRECORDID;
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;
                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.CLEARREMARK = dbResult.CLEARREMARK;
                        inst.STARTDATE1 = dbResult.STARTDATE1;
                        inst.DEFECTFILENAME = dbResult.DEFECTFILENAME;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DEFECTID"></param>
        /// <param name="LENGTH"></param>
        /// <param name="DELETEBY"></param>
        /// <returns></returns>
        public bool INS_DELETEDEFECTBYLENGTH(string DEFECTID, decimal? LENGTH, string DELETEBY)
        {
            bool chkDelete = false;

            if (string.IsNullOrWhiteSpace(DEFECTID))
                return chkDelete;

            if (!HasConnection())
                return chkDelete;

            INS_DELETEDEFECTBYLENGTHParameter dbPara = new INS_DELETEDEFECTBYLENGTHParameter();
            dbPara.P_DEFECTID = DEFECTID;
            dbPara.P_LENGTH = LENGTH;
            dbPara.P_DELETEBY = DELETEBY;

            INS_DELETEDEFECTBYLENGTHResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_DELETEDEFECTBYLENGTH(dbPara);

                if (dbResult != null)
                {
                    chkDelete = true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return chkDelete;
        }

        public void INS_CUTSAMPLE(string insLotNo, DateTime startDate,
            decimal CUTLENGTH, string REMARK, string CUTBY)
        {
            if (string.IsNullOrWhiteSpace(insLotNo))
                return;

            if (!HasConnection())
                return;

            INS_CUTSAMPLEParameter dbPara = new INS_CUTSAMPLEParameter();

            dbPara.P_INSLOT = insLotNo;
            dbPara.P_STARTDATE = startDate;
            dbPara.P_CUTLENGTH = CUTLENGTH;
            dbPara.P_REMARK = REMARK;
            dbPara.P_CUTBY = CUTBY;

            INS_CUTSAMPLEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_CUTSAMPLE(dbPara);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #region Defect methods

        /// <summary>
        /// Get Defect Code.
        /// </summary>
        /// <returns>Returns list of defect codes that can access via dictionary.</returns>
        public Dictionary<string, InspectionDefectCode> GetDefectCodes(string DEFECTID)
        {
            Dictionary<string, InspectionDefectCode> results = null;

            if (!HasConnection())
                return results;

            GETDEFECTCODEDETAILParameter dbPara = new GETDEFECTCODEDETAILParameter();
            dbPara.P_DEFECTID = DEFECTID;
            List<GETDEFECTCODEDETAILResult> dbResults = null;
            try
            {
                dbResults = DatabaseManager.Instance.GETDEFECTCODEDETAIL(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results = new Dictionary<string, InspectionDefectCode>();

                    foreach (GETDEFECTCODEDETAILResult dbResult in dbResults)
                    {
                        InspectionDefectCode inst = new InspectionDefectCode();
                        inst.DefectCode = dbResult.DEFECTCODE.ToUpper();
                        inst.ProcessCode = dbResult.DEFECTPROCESSCODE;
                        inst.DesciptionEN = dbResult.DESCRIPTION_EN;
                        inst.DesciptionTH = dbResult.DESCRIPTION_TH;
                        // add to dictionary.
                        results.Add(inst.DefectCode.Trim(), inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        /// <summary>
        /// Add or Insert Inspection Lot Defect.
        /// </summary>
        /// <param name="code">The defect code.</param>
        /// <param name="point">The number of point.</param>
        /// <param name="position">The position of defect.</param>
        /// <param name="len1">The length start.</param>
        /// <param name="len2">The length end.</param>
        /// <param name="insLot">The inspection lot number.</param>
        /// <returns>Returns defect id (PK) if success.</returns>
        public string AddInspectionLotDefect(string code, 
            decimal? point, decimal? position, decimal? len1, decimal? len2, string insLot)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(insLot))
                return result;

            if (!len2.HasValue)
            {
                // If has only one length. So it is not long defect mode. Required check 2 digits code.
                if (string.IsNullOrWhiteSpace(code) || code.Trim().Length < 2)
                    return result;
            }

            if (!HasConnection())
                return result;

            INSTINSPECTIONLOTDEFECTParameter dbPara = new INSTINSPECTIONLOTDEFECTParameter();
            dbPara.P_DEFECTCODE = code.Trim();
            dbPara.P_INSLOT = insLot;
            dbPara.P_LENGTH1 = len1;
            dbPara.P_LENGTH2 = len2;
            dbPara.P_POINT = point;
            dbPara.P_POSITION = position;

            INSTINSPECTIONLOTDEFECTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INSTINSPECTIONLOTDEFECT(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.R_DEFECTID;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }
        /// <summary>
        /// Gets Defect Count.
        /// </summary>
        /// <param name="insLotNo">The inspection lot number.</param>
        /// <returns>Returns Number of defect in the inspection lot.</returns>
        public int GetDefectCount(string insLotNo,string DEFECTID)
        {
            int result = 0;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return result;

            if (!HasConnection())
                return result;

            INS_GETTOTALDEFECTBYINSLOTParameter dbPara = new INS_GETTOTALDEFECTBYINSLOTParameter();
            dbPara.P_INSLOT = insLotNo;
            dbPara.P_DEFECTID = DEFECTID;

            INS_GETTOTALDEFECTBYINSLOTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_GETTOTALDEFECTBYINSLOT(dbPara);
                if (null != dbResult && dbResult.TOTAL.HasValue)
                {
                    result = Convert.ToInt32(dbResult.TOTAL.Value);
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }
        /// <summary>
        /// Get Defect List.
        /// </summary>
        /// <param name="insLotNo">The inspection lot number.</param>
        /// <param name="defectId">The defect id (PK).</param>
        /// <returns>Returns list of InspectionDefectItem.</returns>
        public List<InspectionDefectItem> GetDefectList(string insLotNo, string defectId)
        {
            List<InspectionDefectItem> results = null;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return results;

            if (!HasConnection())
                return results;

            GETINSDEFECTLISTParameter dbPara = new GETINSDEFECTLISTParameter();
            dbPara.P_DEFECTID = defectId;
            dbPara.P_INSLOT = insLotNo;
            List<GETINSDEFECTLISTResult> dbResults = null;
            try
            {
                dbResults = DatabaseManager.Instance.GETINSDEFECTLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<InspectionDefectItem>();
                    int iNo = 1;
                    foreach (GETINSDEFECTLISTResult dbResult in dbResults)
                    {
                        InspectionDefectItem inst = new InspectionDefectItem();
                        inst.DefectID = dbResult.DEFECTID;

                        inst.No = iNo.ToString();
                        inst.Length = (dbResult.LENGTH1.HasValue) ? 
                            dbResult.LENGTH1.Value.ToString("n2") : string.Empty;
                        inst.Length2 = (dbResult.LENGTH2.HasValue) ?
                            dbResult.LENGTH2.Value.ToString("n2") : string.Empty;

                        // New cal length
                        decimal length1 = decimal.Zero;
                        decimal length2 = decimal.Zero;

                        if (dbResult.LENGTH1 != null )
                        {
                            if (dbResult.LENGTH1 > 0)
                            {
                                length1 = dbResult.LENGTH1.Value;
                            }
                        }

                        if (dbResult.LENGTH2 != null)
                        {
                            if (dbResult.LENGTH2 > 0)
                            {
                                length2 = dbResult.LENGTH2.Value;
                            }
                        }

                        if (length2 != 0)
                        {
                            if (length2 > length1)
                            {
                                inst.DefectLength = (length2 - length1).ToString("n2");
                            }
                            else 
                            {
                                inst.DefectLength = "";
                            }
                        }
                        else
                        {
                            inst.DefectLength = "";
                        }
                        //--------------------------------------------------------//

                        inst.DefectCode = dbResult.DEFECTCODE;
                        inst.Description = dbResult.DESCRIPTION_EN;
                        inst.Position = (dbResult.POSITION.HasValue) ?
                            dbResult.POSITION.Value.ToString("#,##0.0") : string.Empty;

                        inst.deleteBY = dbResult.DELETEBY;
                        inst.deleteREMARK = dbResult.DELETEREMARK;

                        //New 24/8/22
                        inst.DEFECTPOINT100 = dbResult.DEFECTPOINT100;

                        results.Add(inst);
                        ++iNo;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                results = null;
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defectId"></param>
        /// <param name="itmCode"></param>
        /// <returns></returns>
        public List<INS_ReportSumDefectData> GetINS_ReportSumDefect(string defectId, string itmCode)
        {
            List<INS_ReportSumDefectData> results = null;

            if (string.IsNullOrWhiteSpace(defectId))
                return results;

            if (string.IsNullOrWhiteSpace(itmCode))
                return results;

            if (!HasConnection())
                return results;

            INS_REPORTSUMDEFECTParameter dbPara = new INS_REPORTSUMDEFECTParameter();
            dbPara.P_DEFECTID = defectId;
            dbPara.P_ITMCODE = itmCode;

            List<INS_REPORTSUMDEFECTResult> dbResults = null;
            try
            {
                dbResults = DatabaseManager.Instance.INS_REPORTSUMDEFECT(dbPara);
                if (null != dbResults)
                {
                    results = new List<INS_ReportSumDefectData>();
                    int iNo = 1;
                    foreach (INS_REPORTSUMDEFECTResult dbResult in dbResults)
                    {
                        INS_ReportSumDefectData inst = new INS_ReportSumDefectData();

                        #region TOTALPOINT

                        if (dbResult.TOTALPOINT != null)
                        {
                            inst.TOTALPOINT = dbResult.TOTALPOINT;
                        }
                        else
                        {
                            inst.TOTALPOINT = 0;
                        }

                        #endregion

                        #region SHORTDEFECT

                        if (dbResult.SHORTDEFECT != null)
                        {
                            inst.SHORTDEFECT = dbResult.SHORTDEFECT;
                        }
                        else
                        {
                            inst.SHORTDEFECT = 0;
                        }

                        #endregion

                        #region LONGDEFECT
                        if (dbResult.LONGDEFECT != null)
                        {
                            inst.LONGDEFECT = dbResult.LONGDEFECT;
                        }
                        else
                        {
                            inst.LONGDEFECT = 0;
                        }
                        #endregion

                        #region COMLONGDEFECT

                        if (dbResult.COMLONGDEFECT != null)
                        {
                            inst.COMLONGDEFECT = dbResult.COMLONGDEFECT;
                        }
                        else
                        {
                            inst.COMLONGDEFECT = 0;
                        }

                        #endregion

                        #region COMSHORTDEFECT

                        if (dbResult.COMSHORTDEFECT != null)
                        {
                            inst.COMSHORTDEFECT = dbResult.COMSHORTDEFECT;
                        }
                        else
                        {
                            inst.COMSHORTDEFECT = 0;
                        }

                        #endregion

                        results.Add(inst);
                        ++iNo;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                results = null;
            }

            return results;
        }

        // ปรับเพิ่มใหม่ใช้แทน  GETINSDEFECTLIST ใน report Inspection ( Add date 08/02/16 )
        public List<InspectionDefectItem> ins_GetDefectListReport(string insLotNo, string defectId)
        {
            List<InspectionDefectItem> results = null;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return results;

            if (!HasConnection())
                return results;

            INS_GETDEFECTLISTREPORTParameter dbPara = new INS_GETDEFECTLISTREPORTParameter();
            dbPara.P_DEFECTID = defectId;
            dbPara.P_INSLOT = insLotNo;
            List<INS_GETDEFECTLISTREPORTResult> dbResults = null;
            try
            {
                dbResults = DatabaseManager.Instance.INS_GETDEFECTLISTREPORT(dbPara);
                if (null != dbResults)
                {
                    results = new List<InspectionDefectItem>();
                    int iNo = 1;
                    foreach (INS_GETDEFECTLISTREPORTResult dbResult in dbResults)
                    {
                        InspectionDefectItem inst = new InspectionDefectItem();
                        inst.DefectID = dbResult.DEFECTID;

                        inst.No = iNo.ToString();
                        inst.Length = (dbResult.LENGTH1.HasValue) ?
                            dbResult.LENGTH1.Value.ToString("n2") : string.Empty;
                        inst.Length2 = (dbResult.LENGTH2.HasValue) ?
                            dbResult.LENGTH2.Value.ToString("n2") : string.Empty;

                        // New cal length
                        decimal length1 = decimal.Zero;
                        decimal length2 = decimal.Zero;

                        if (dbResult.LENGTH1 != null)
                        {
                            if (dbResult.LENGTH1 > 0)
                            {
                                length1 = dbResult.LENGTH1.Value;
                            }
                        }

                        if (dbResult.LENGTH2 != null)
                        {
                            if (dbResult.LENGTH2 > 0)
                            {
                                length2 = dbResult.LENGTH2.Value;
                            }
                        }

                        if (length2 != 0)
                        {
                            if (length2 > length1)
                            {
                                inst.DefectLength = (length2 - length1).ToString("n2");
                            }
                            else
                            {
                                inst.DefectLength = "";
                            }
                        }
                        else
                        {
                            inst.DefectLength = "";
                        }
                        //--------------------------------------------------------//

                        inst.DefectCode = dbResult.DEFECTCODE;
                        inst.Description = dbResult.DESCRIPTION_EN;
                        inst.Position = (dbResult.POSITION.HasValue) ?
                            dbResult.POSITION.Value.ToString("#,##0.0") : string.Empty;

                        inst.deleteBY = dbResult.DELETEBY;
                        inst.deleteREMARK = dbResult.DELETEREMARK;

                        //New 24/8/22
                        inst.DEFECTPOINT100 = dbResult.DEFECTPOINT100;

                        results.Add(inst);
                        ++iNo;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                results = null;
            }

            return results;
        }

        /// เพิ่มใหม่เพื่อใช้กับการ Delete Defect    
        public bool DeleteInspectionLotDefect(string defectID,
            string defectCode, decimal? len1, string operatorID, string deleteremark)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(defectID))
                return result;

            if (!HasConnection())
                return result;

            INS_DELETEDEFECTParameter dbPara = new INS_DELETEDEFECTParameter();
            dbPara.P_DEFECTID = defectID.Trim();
            dbPara.P_DEFECTCODE = defectCode;
            dbPara.P_LENGTH1 = len1;
            dbPara.P_DELETEBY = operatorID;
            dbPara.P_DELETEREMARK = deleteremark;

            INS_DELETEDEFECTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_DELETEDEFECT(dbPara);
                if (null != dbResult)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }

        /// เพิ่มใหม่เพื่อใช้กับการ Edit Defect    
        public bool EditInspectionLotDefect(string defectID, string insLot,
            string defectCode, decimal? len1, decimal? len2, decimal? position, string ndefectCode
            , decimal? nlen, decimal? nlen2, decimal? nposition)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(defectID))
                return result;

            if (!HasConnection())
                return result;

            INS_EDITDEFECTParameter dbPara = new INS_EDITDEFECTParameter();

            dbPara.P_DEFECTID = defectID.Trim();
            dbPara.P_INSLOT = insLot;
            dbPara.P_DEFECTCODE = defectCode;
            dbPara.P_LENGTH1 = len1;
            dbPara.P_LENGTH2 = len2;
            dbPara.P_POSITION = position;
            dbPara.P_NDEFECTCODE = ndefectCode;
            dbPara.P_NLENGTH1 = nlen;
            dbPara.P_NLENGTH2 = nlen2;
            dbPara.P_NPOSITION = nposition;


            INS_EDITDEFECTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_EDITDEFECT(dbPara);
                if (null != dbResult)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }

        // Ins_delete100Mrecord

        public bool Ins_Delete100MRecord(string testID, string stdLength, decimal? actLength, string deleteBy, string deleteRemark)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(testID))
                return result;

            if (!HasConnection())
                return result;

            INS_DELETE100MRECORDParameter dbPara = new INS_DELETE100MRECORDParameter();
            dbPara.P_TESTID = testID.Trim();
            dbPara.P_STDLENGTH = stdLength;
            dbPara.P_ACTLENGTH = actLength;
            dbPara.P_DELETEBY = deleteBy;
            dbPara.P_DELETEREMARK = deleteRemark;

            INS_DELETE100MRECORDResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_DELETE100MRECORD(dbPara);
                if (null != dbResult)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }

        /// เพิ่มใหม่เพื่อใช้กับการ Edit 100TESTRECORD    
        public bool INS_EDIT100TESTRECORD(string testID, string insLot,
            string stdLength, decimal? actLength, decimal? nactLength, decimal? ndenw, decimal? ndenf
            , decimal? nwidthAll, decimal? nwidthPin, decimal? nwidthCoat, decimal? ntrimL
            , decimal? ntrimR, string nfloppyL, string nfloppyR, decimal? nunwinderSet, decimal? nunwinderAct
            , decimal? nwinderSet, decimal? nwinderAct
            , decimal? nhardnessL, decimal? nhardnessC, decimal? nhardnessR)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(testID))
                return result;

            if (string.IsNullOrWhiteSpace(insLot))
                return result;

            if (!HasConnection())
                return result;

            INS_EDIT100TESTRECORDParameter dbPara = new INS_EDIT100TESTRECORDParameter();

            dbPara.P_TESTID = testID;
            dbPara.P_INSLOT = insLot;
            dbPara.P_STDLENGTH = stdLength;
            dbPara.P_ACTLENGTH = actLength;
            dbPara.P_NACTLENGTH = nactLength;
            dbPara.P_NDENW = ndenw;
            dbPara.P_NDENF = ndenf;
            dbPara.P_NWIDTHALL = nwidthAll;
            dbPara.P_NWIDTHPIN = nwidthPin;
            dbPara.P_NWIDTHCOAT = nwidthCoat;
            dbPara.P_NTRIML = ntrimL;
            dbPara.P_NTRIMR = ntrimR;
            dbPara.P_NFLOPPYL = nfloppyL;
            dbPara.P_NFLOPPYR = nfloppyR;
            dbPara.P_NUNWINDERSET = nunwinderSet;
            dbPara.P_NUNWINDERACT = nunwinderAct;
            dbPara.P_NWINDERSET = nwinderSet;
            dbPara.P_NWINDERACT = nwinderAct;

            dbPara.P_NHARDNESSL = nhardnessL;
            dbPara.P_NHARDNESSC = nhardnessC;
            dbPara.P_NHARDNESSR = nhardnessR;

            INS_EDIT100TESTRECORDResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_EDIT100TESTRECORD(dbPara);
                if (null != dbResult)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }
        #endregion

        #region Item's Tests methods

        /// <summary>
        /// Add Inspection Test Data.
        /// </summary>
        /// <param name="insLotNo">The inspection lot no.</param>
        /// <param name="popupDate">The Popup Date.</param>
        /// <param name="actualLen">The actual length from counter.</param>
        /// <param name="stdLen">The STD length.</param>
        /// <param name="test">The list of test data.</param>
        /// <returns>Returns test id (PK) if success.</returns>
        public string AddInspectionTestData(string insLotNo,
            DateTime? popupDate, 
            decimal actualLen, decimal stdLen, 
            InspectionTests test)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return result;

            if (!HasConnection())
                return result;

            INSTINSPECTIONTESTRECORDParameter dbPara = new INSTINSPECTIONTESTRECORDParameter();
            dbPara.P_ACTUALLENGTH = actualLen;
            dbPara.P_INSLOT = insLotNo;
            dbPara.P_POPUPDATE = popupDate;
            dbPara.P_SAVEDATE = DateTime.Now;
            dbPara.P_STDLENGTH = stdLen;

            dbPara.P_DENF = test.Densities.F.Value;
            dbPara.P_DENW = test.Densities.W.Value;

            dbPara.P_WIDTHALL = test.Widths.All.Value;
            dbPara.P_WIDTHPIN = test.Widths.Pin.Value;
            dbPara.P_WIDTHCOAT = test.Widths.Coat.Value;

            dbPara.P_TRIML = test.Trims.L.Value;
            dbPara.P_TRIMR = test.Trims.R.Value;

            dbPara.P_FLOPPYL = test.Floppies.L.strValue;
            dbPara.P_FLOPPYR = test.Floppies.R.strValue;

            dbPara.P_UNWINDERACT = test.Unwinders.Actual.Value;
            dbPara.P_UNWINDERSET = test.Unwinders.Set.Value;

            dbPara.P_WINDERACT = test.Winders.Actual.Value;
            dbPara.P_WINDERSET = test.Winders.Set.Value;

            dbPara.P_HARDNESSL = test.Hardnesses.L.Value;
            dbPara.P_HARDNESSC = test.Hardnesses.C.Value;
            dbPara.P_HARDNESSR = test.Hardnesses.R.Value;

            INSTINSPECTIONTESTRECORDResult dbResult = null;

            try
            {
                dbResult = DatabaseManager.Instance.INSTINSPECTIONTESTRECORD(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.R_TESTID;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }
        /// <summary>
        /// Gets Test History List.
        /// </summary>
        /// <param name="insLotNo">The inspection lot no.</param>
        /// <param name="testId">The Test Id (PK).</param>
        /// <returns>Returns Test History List.</returns>
        public List<InspectionTestHistoryItem> GetTestHistoryList(
            string insLotNo, string testId)
        {
            List<InspectionTestHistoryItem> results = null;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return results;

            if (!HasConnection())
                return results;

            GETINSTESTRECORDLISTParameter dbPara = new GETINSTESTRECORDLISTParameter();
            dbPara.P_INSLOT = insLotNo;
            dbPara.P_TESTID = testId;

            List<GETINSTESTRECORDLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.GETINSTESTRECORDLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<InspectionTestHistoryItem>();
                    foreach (GETINSTESTRECORDLISTResult dbResult in dbResults)
                    {
                        InspectionTestHistoryItem inst = new InspectionTestHistoryItem();

                        inst.TESTRECORDID = dbResult.TESTRECORDID;

                        inst.ActualLength = (dbResult.ACTUALLENGTH.HasValue) ? 
                            dbResult.ACTUALLENGTH.Value.ToString("n2") : string.Empty;
                        inst.STDLength = (dbResult.STDLENGTH.HasValue) ?
                            dbResult.STDLENGTH.Value.ToString("n2") : string.Empty;

                        inst.DensityF = (dbResult.DENSITYF.HasValue) ?
                            dbResult.DENSITYF.Value.ToString("#,##0.0") : string.Empty;
                        inst.DensityW = (dbResult.DENSITYW.HasValue) ?
                            dbResult.DENSITYW.Value.ToString("#,##0.0") : string.Empty;

                        inst.WidthAll = (dbResult.WIDTHALL.HasValue) ?
                            dbResult.WIDTHALL.Value.ToString("#,##0.0") : string.Empty;
                        inst.WidthPin = (dbResult.WIDTHPIN.HasValue) ?
                            dbResult.WIDTHPIN.Value.ToString("#,##0.0") : string.Empty;
                        inst.WidthCoat = (dbResult.WIDTHCOAT.HasValue) ?
                            dbResult.WIDTHCOAT.Value.ToString("#,##0.0") : string.Empty;

                        inst.TrimL = (dbResult.TRIML.HasValue) ?
                            dbResult.TRIML.Value.ToString("n2") : string.Empty;
                        inst.TrimR = (dbResult.TRIMR.HasValue) ?
                            dbResult.TRIMR.Value.ToString("n2") : string.Empty;

                        inst.FloppyL = dbResult.FLOPPYL;
                        inst.FloppyR = dbResult.FLOPPYR;

                        inst.UnwinderActual = (dbResult.UNWINDERACTUAL.HasValue) ?
                            dbResult.UNWINDERACTUAL.Value.ToString("n2") : string.Empty;
                        inst.UnwinderSet = (dbResult.UNWINDERSET.HasValue) ?
                            dbResult.UNWINDERSET.Value.ToString("n2") : string.Empty;

                        inst.WinderActual = (dbResult.WINDERACTUAL.HasValue) ?
                            dbResult.WINDERACTUAL.Value.ToString("n2") : string.Empty;
                        inst.WinderSet = (dbResult.WINDERSET.HasValue) ?
                            dbResult.WINDERSET.Value.ToString("n2") : string.Empty;

                        inst.HardnessL = (dbResult.HARDNESS_L.HasValue) ?
                           dbResult.HARDNESS_L.Value.ToString("n2") : string.Empty;

                        inst.HardnessC = (dbResult.HARDNESS_C.HasValue) ?
                           dbResult.HARDNESS_C.Value.ToString("n2") : string.Empty;

                        inst.HardnessR = (dbResult.HARDNESS_R.HasValue) ?
                           dbResult.HARDNESS_R.Value.ToString("n2") : string.Empty;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region Report

        // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ Inspection Report 
        /// <summary>
        /// Gets Inspection Report Data.
        /// </summary>
        /// <param name="insLotNo">The inspection Lot.</param>
        /// <returns>Returns list of InspectionLotData.</returns>
        public List<InspectionReportData> GetInspectionReportData(string insLotNo)
        {
            List<InspectionReportData> results = null;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return results;

            if (!HasConnection())
                return results;

            INS_GETINSPECTIONREPORTDATAParameter dbPara = new INS_GETINSPECTIONREPORTDATAParameter();
            List<INS_GETINSPECTIONREPORTDATAResult> dbResults = null;
            dbPara.P_INS_LOT = insLotNo;
            try
            {
                dbResults = DatabaseManager.Instance.INS_GETINSPECTIONREPORTDATA(dbPara);
                if (null != dbResults && dbResults.Count > 0)
                {
                    results = new List<InspectionReportData>();
                    foreach (INS_GETINSPECTIONREPORTDATAResult dbResult in dbResults)
                    {
                        InspectionReportData inst = new InspectionReportData();

                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.GROSSLENGTH = dbResult.GROSSLENGTH;
                        inst.NETLENGTH = dbResult.NETLENGTH;
                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.GRADE = dbResult.GRADE;
                        inst.GROSSWEIGHT = dbResult.GROSSWEIGHT;
                        inst.NETWEIGHT = dbResult.NETWEIGHT;
                        inst.PEINSPECTIONLOT = dbResult.PEINSPECTIONLOT;
                        inst.DEFECTID = dbResult.DEFECTID;
                        inst.REMARK = dbResult.REMARK;

                        // เพิ่มใหม่ 13/05/16
                        inst.SHIFT_ID = dbResult.SHIFT_ID;
                        inst.SHIFT_REMARK = dbResult.SHIFT_REMARK;

                        inst.ATTACHID = dbResult.ATTACHID;
                        inst.TESTRECORDID = dbResult.TESTRECORDID;
                        inst.INSPECTEDBY = dbResult.INSPECTEDBY;
                        inst.MCNO = dbResult.MCNO;
                        inst.FINISHFLAG = dbResult.FINISHFLAG;
                        inst.SUSPENDDATE = dbResult.SUSPENDDATE;
                        inst.INSPECTIONID = dbResult.INSPECTIONID;
                        inst.RETEST = dbResult.RETEST;
                        inst.PREITEMCODE = dbResult.PREITEMCODE;
                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.CLEARREMARK = dbResult.CLEARREMARK;
                        inst.SUSPENDBY = dbResult.SUSPENDBY;
                        inst.STARTDATE1 = dbResult.STARTDATE1;
                        inst.PRODUCTNAME = dbResult.PRODUCTNAME;
                        inst.MCNAME = dbResult.MCNAME;
                        inst.CUSTOMERNAME = dbResult.CUSTOMERNAME;
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;
                        inst.PARTNO = dbResult.PARTNO;
                        inst.DEFECTFILENAME = dbResult.DEFECTFILENAME;
                        inst.LOADINGTYPE = dbResult.LOADINGTYPE;

                        //เพิ่ม 13/05/16
                        inst.SHIFT_ID = dbResult.SHIFT_ID;
                        inst.SHIFT_REMARK = dbResult.SHIFT_REMARK;

                        //เพิ่ม 23/8/22
                        inst.CONFIRMSTARTLENGTH = dbResult.CONFIRMSTARTLENGTH;
                        inst.CONFIRMSTDLENGTH = dbResult.CONFIRMSTDLENGTH;

                        //เพิ่ม 17/10/22
                        //inst.RESETSTARTLENGTH = dbResult.RESETSTARTLENGTH;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ INS_GetFinishinslotData

        /// <summary>
        /// INS_GetFinishinslotData
        /// </summary>
        /// <param name="insFINLOT"></param>
        /// <returns></returns>
        public List<INS_GetFinishinslotData> GetINS_GetFinishinslotDataList(
            string insFINLOT)
        {
            List<INS_GetFinishinslotData> results = null;

            if (string.IsNullOrWhiteSpace(insFINLOT))
                return results;

            if (!HasConnection())
                return results;

            INS_GETFINISHINSLOTDATAParameter dbPara = new INS_GETFINISHINSLOTDATAParameter();
            dbPara.P_FINLOT = insFINLOT;

            List<INS_GETFINISHINSLOTDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.INS_GETFINISHINSLOTDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<INS_GetFinishinslotData>();
                    foreach (INS_GETFINISHINSLOTDATAResult dbResult in dbResults)
                    {
                        INS_GetFinishinslotData inst = new INS_GetFinishinslotData();

                        inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.GRADE = dbResult.GRADE;
                        inst.CUSTOMERTYPE = dbResult.CUSTOMERTYPE;

                        inst.GROSSWEIGHT = (dbResult.GROSSWEIGHT.HasValue) ?
                            dbResult.GROSSWEIGHT.Value.ToString("n2") : string.Empty;
                        inst.NETWEIGHT = (dbResult.NETWEIGHT.HasValue) ?
                            dbResult.NETWEIGHT.Value.ToString("n2") : string.Empty;

                        inst.SHIFT_ID = dbResult.SHIFT_ID;
                        inst.SHIFT_REMARK = dbResult.SHIFT_REMARK;

                        // ยังไม่ได้ใช้
                        //inst.DEFECTFILENAME = dbResult.DEFECTFILENAME;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ GetDefect100MCount

        /// <summary>
        /// GetDefect100MCount
        /// </summary>
        /// <param name="defectId"></param>
        /// <param name="insLotNo"></param>
        /// <param name="startLeng"></param>
        /// <param name="endLeng"></param>
        /// <returns></returns>
        public int GetDefect100MCount(string defectId, string insLotNo
            , decimal startLeng, decimal endLeng)
        {
            int result = 0;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return result;

            if (!HasConnection())
                return result;

            GETCOUNTDEFECT100MParameter dbPara = new GETCOUNTDEFECT100MParameter();
            dbPara.P_DEFECTID = defectId;
            dbPara.P_INSPECTIONLOT = insLotNo;
            dbPara.STARTLENGTH = startLeng;
            dbPara.ENDLENGTH = endLeng;

            GETCOUNTDEFECT100MResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.GETCOUNTDEFECT100M(dbPara);
                if (null != dbResult && dbResult.TOTAL.HasValue)
                {
                    result = Convert.ToInt32(dbResult.TOTAL.Value);
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion

        #region เพิ่มใหม่ Test Add InspectionTest
        /// <summary>
        /// 
        /// </summary>
        /// <param name="insLotNo"></param>
        /// <param name="chkNew"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        public List<InspectionTestTempItem> AddInspectionTestTemptData(string insLotNo, int chkCount,
            InspectionTests test)
        {
            List<InspectionTestTempItem> results = null;

            if (string.IsNullOrWhiteSpace(insLotNo))
                return results;

            if (!HasConnection())
                return results;

            try
            {

                results = new List<InspectionTestTempItem>();

                InspectionTestTempItem inst = new InspectionTestTempItem();

                inst.InspecionLotNo = insLotNo;

                #region Unwinders

                inst.UnwindersActual = test.Unwinders.Actual.Value;
                inst.UnwindersSet = test.Unwinders.Set.Value;

                #endregion

                inst.ChkCountNew = chkCount;

                if (chkCount == 0)
                {
                    inst.OnlyAddUnwinder = false;
                }
                else
                {
                    inst.OnlyAddUnwinder = true;
                }

                results.Add(inst);

            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }


        #endregion

        #region NetLength


        public decimal? GetNetLength(string cusid, string itemCode, decimal? length, string grade, string defectId)
        {
            decimal? result = null;

            if (string.IsNullOrWhiteSpace(grade))
                return result;
            //if (string.IsNullOrWhiteSpace(itemCode)) { return result; }                

            if (!HasConnection())
                return result;

            INS_GETNETLENGTHParameter dbPara = new INS_GETNETLENGTHParameter();
            dbPara.P_CUSID = cusid;
            dbPara.P_ITMCODE = itemCode;
            dbPara.P_LENGTH = length;
            dbPara.P_GRADE = grade;
            dbPara.P_DEFECTID = defectId;

            INS_GETNETLENGTHResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_GETNETLENGTH(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.NETLENGTH;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }
        #endregion

        #region Grade

        /// <summary>
        /// Gets Grade.
        /// </summary>
        /// <param name="insLot">The inspection lot.</param>
        /// <param name="itemCode">The item code.</param>
        /// <param name="length">The current counter length.</param>
        /// <param name="customerId">The customer id (PK).</param>
        /// <returns></returns>
        public string GetGrade(string insLot, string itemCode, decimal? length,
            string customerId)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(insLot))
                return result;
            //if (string.IsNullOrWhiteSpace(itemCode)) { return result; }                

            if (!HasConnection())
                return result;

            GETGRADEParameter dbPara = new GETGRADEParameter();
            dbPara.P_INSLOT = insLot;
            dbPara.P_ITEM_CODE = itemCode;
            dbPara.P_LENGTH = length;
            dbPara.P_CUSID = customerId;

            GETGRADEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.GETGRADE(dbPara);
                if (null != dbResult)
                {
                    result = dbResult.R_GRADE;
                    // Dump result.
                    string sVal = (string.IsNullOrWhiteSpace(dbResult.R_GRADE)) ?
                        "null or empty string" : dbResult.R_GRADE;
                    string msg = "GETGRADE returns " + sVal;
                    msg.Info();
                }
                else
                {
                    "GETGRADE returns null.".Err();
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }
        /// <summary>
        /// Change Grade.
        /// </summary>
        /// <param name="insLot">The Inspection lot.</param>
        /// <param name="oldGrade">The Old Grade.</param>
        /// <param name="newGrade">The New Grade.</param>
        /// <param name="remark">The Remark.</param>
        /// <param name="userName">The User Name.</param>
        /// <param name="password">The Password.</param>
        /// <returns>Returns true if change grade success.</returns>
        public bool ChangeGrade(string insLot, string oldGrade, string newGrade,
            string remark,
            string userName, string password)
        {
            bool result = false;

            #region Check(s)
            
            if (string.IsNullOrWhiteSpace(insLot))
                return result;

            if (string.IsNullOrWhiteSpace(oldGrade))
                return result;

            if (string.IsNullOrWhiteSpace(newGrade))
                return result;

            if (string.IsNullOrWhiteSpace(userName))
                return result;

            if (string.IsNullOrWhiteSpace(password))
                return result;

            if (!HasConnection())
                return result;

            #endregion

            INST_INSPECTIONGRADEHISTORYParameter dbPara = new INST_INSPECTIONGRADEHISTORYParameter();
            dbPara.P_INSLOT = insLot;
            dbPara.P_OLDGRADE = oldGrade;
            dbPara.P_NEWGRADE = newGrade;
            dbPara.P_REMARK = remark;
            dbPara.P_USER = userName;
            dbPara.P_PASS = password;
            dbPara.P_STARTDATE = DateTime.Now;

            INST_INSPECTIONGRADEHISTORYResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INST_INSPECTIONGRADEHISTORY(dbPara);
                if (null != dbResult)
                {
                    if (!string.IsNullOrWhiteSpace(dbResult.R_RESULT) &&
                        dbResult.R_RESULT.Trim().ToUpper() == "Y")
                    {
                        // Success
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion

        #region Create new Session

        /// <summary>
        /// Gets Inspection Session.
        /// </summary>
        /// <param name="mcItem">The machine information.</param>
        /// <param name="loginResult">The current user login information.</param>
        /// <returns>Returns Inspection session instance.</returns>
        public InspectionSession GetSession(InspectionMCItem mcItem, LogInResult loginResult)
        {
            InspectionSession result = new InspectionSession();
            result.Init(mcItem, loginResult);
            return result;
        }

        #endregion

        #region เพิ่มใหม่ FINISHING_SEARCHFINISHDATA ใช้ในการ Load FINISHING

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_SEARCHFINISHDATA> Finishing_SearchDataList(string strDATE, string PROCESS)
        {
            List<FINISHING_SEARCHFINISHDATA> results = null;

            if (!HasConnection())
                return results;

            FINISHING_SEARCHFINISHDATAParameter dbPara = new FINISHING_SEARCHFINISHDATAParameter();
            dbPara.P_DATE = strDATE;
            dbPara.P_PROCESS = PROCESS;

            List<FINISHING_SEARCHFINISHDATAResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_SEARCHFINISHDATA(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_SEARCHFINISHDATA>();
                    foreach (FINISHING_SEARCHFINISHDATAResult dbResult in dbResults)
                    {
                        FINISHING_SEARCHFINISHDATA inst = new FINISHING_SEARCHFINISHDATA();

                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.TOTALLENGTH = dbResult.TOTALLENGTH;
                        inst.PROCESS = dbResult.PROCESS;
                        inst.FINISHINGDATE = dbResult.FINISHINGDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;
                        inst.MC = dbResult.MC;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;

                        inst.SAMPLING_WIDTH = dbResult.SAMPLING_WIDTH;
                        inst.SAMPLING_LENGTH = dbResult.SAMPLING_LENGTH;
                        inst.REMARK = dbResult.REMARK;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region Insert Manual

        /// <summary>
        /// Insert Inspection record when Start button press.
        /// </summary>
        /// <param name="finLot">The Finishing Lot No.</param>
        /// <param name="itemCode">The Item Code.</param>
        /// <param name="insLot">The Inspection Lot No.</param>
        /// <param name="mcNo">The machine No. This is machine PK.</param>
        /// <param name="productivityId">The test type (Mass or Test)</param>
        /// <param name="customerId">The customer Id. This is customer PK.</param>
        /// <param name="customerType">The customer type string.</param>
        /// <param name="peInsLot">The PE Inspection Lot No.</param>
        /// <param name="insDate">The Inspection start date.</param>
        /// <param name="insBy">The user that start Inspection.</param>
        /// <param name="reTest"></param>
        /// <returns>Returns string that represent primary key if insert success.</returns>
        public string INS_INSERTMANUALINSPECTDATA(string P_INSLOT,string P_ITMCODE,string P_FINISHLOT,
        DateTime? P_STARTDATE,DateTime? P_ENDDATE,
        string P_CUSTOMERID,string P_PRODUCTTYPEID,string P_INSPECTEDBY,string P_MCNO,string P_CUSTOMERTYPE,string P_LOADTYPE,
        Decimal? P_GLENGHT, Decimal? P_NLENGTH, string P_GRADE, Decimal? P_GWEIGHT, Decimal? P_NWEIGHT, string P_REMARK, string P_OPERATOR, string GROUP)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(P_INSLOT))
                return null;
            if (string.IsNullOrWhiteSpace(P_ITMCODE))
                return null;
            if (string.IsNullOrWhiteSpace(P_FINISHLOT))
                return null;
            if (string.IsNullOrWhiteSpace(P_MCNO))
                return null;

            if (string.IsNullOrWhiteSpace(P_OPERATOR))
                return null;

            if (!HasConnection())
                return result;

            INS_INSERTMANUALINSPECTDATAParameter dbPara = new INS_INSERTMANUALINSPECTDATAParameter();

            dbPara.P_INSLOT = P_INSLOT;
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_FINISHLOT = P_FINISHLOT;
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_ENDDATE = P_ENDDATE;
            dbPara.P_CUSTOMERID = P_CUSTOMERID;
            dbPara.P_PRODUCTTYPEID = P_PRODUCTTYPEID;
            dbPara.P_INSPECTEDBY = P_INSPECTEDBY;
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_CUSTOMERTYPE = P_CUSTOMERTYPE;
            dbPara.P_LOADTYPE = P_LOADTYPE;
            dbPara.P_GLENGHT = P_GLENGHT;
            dbPara.P_NLENGTH = P_NLENGTH;
            dbPara.P_GRADE = P_GRADE;
            dbPara.P_GWEIGHT = P_GWEIGHT;
            dbPara.P_NWEIGHT = P_NWEIGHT;
            dbPara.P_REMARK = P_REMARK;
            dbPara.P_OPERATOR = P_OPERATOR;
            dbPara.P_GROUP = GROUP;

            INS_INSERTMANUALINSPECTDATAResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_INSERTMANUALINSPECTDATA(dbPara);
                if (null != dbResult)
                {
                    if (!string.IsNullOrWhiteSpace(dbResult.R_INSID))
                    {
                        result = dbResult.R_INSID;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region INS_REWAREHOUSE
        public bool INS_REWAREHOUSE(string P_INSOLD, string P_DEFECTID, string P_TESTID, string P_INSNEW)
        {
            bool result = false;

            #region Check(s)

            if (string.IsNullOrWhiteSpace(P_INSOLD))
                return result;

            if (string.IsNullOrWhiteSpace(P_INSNEW))
                return result;

            if (!HasConnection())
                return result;

            #endregion

            INS_REWAREHOUSEParameter dbPara = new INS_REWAREHOUSEParameter();
            dbPara.P_INSOLD = P_INSOLD;
            dbPara.P_DEFECTID = P_DEFECTID;
            dbPara.P_TESTID = P_TESTID;
            dbPara.P_INSNEW = P_INSNEW;

            INS_REWAREHOUSEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_REWAREHOUSE(dbPara);

                result = (null != dbResult);
            }
            catch (Exception ex)
            {
                ex.Err();
                result = false;
            }

            return result;
        }
        #endregion

        #region INS_SHIFTREMARK
        public string INS_SHIFTREMARK(string P_INSLOT,DateTime? P_STARTDATE, string P_SHIFTID, string P_SHIFTREMARK)
        {
            string result = string.Empty;

            #region Check(s)

            if (string.IsNullOrWhiteSpace(P_INSLOT))
                return result;

            if (!HasConnection())
                return result;

            #endregion

            INS_SHIFTREMARKParameter dbPara = new INS_SHIFTREMARKParameter();
            dbPara.P_INSLOT = P_INSLOT;
            dbPara.P_STARTDATE = P_STARTDATE;
            dbPara.P_SHIFTID = P_SHIFTID;
            dbPara.P_SHIFTDATE = DateTime.Now;
            dbPara.P_SHIFTREMARK = P_SHIFTREMARK;

            INS_SHIFTREMARKResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_SHIFTREMARK(dbPara);

                if (null != dbResult)
                {
                    if (!string.IsNullOrEmpty(dbResult.R_RESULT))
                    {
                        result = dbResult.R_RESULT;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }
        #endregion

        #region INS_INSERTCONFIRMSTARTING
        public string INS_INSERTCONFIRMSTARTING(string P_INSID, string P_INSLOT, decimal? P_CONFIRMSTART)
        {
            string result = string.Empty;

            #region Check(s)
            if (string.IsNullOrWhiteSpace(P_INSID))
                return result;

            if (string.IsNullOrWhiteSpace(P_INSLOT))
                return result;

            if (!HasConnection())
                return result;

            #endregion

            INS_INSERTCONFIRMSTARTINGParameter dbPara = new INS_INSERTCONFIRMSTARTINGParameter();
            dbPara.P_INSID = P_INSID;
            dbPara.P_INSLOT = P_INSLOT;
            dbPara.P_CONFIRMSTART = P_CONFIRMSTART;

            INS_INSERTCONFIRMSTARTINGResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_INSERTCONFIRMSTARTING(dbPara);

                if (null != dbResult)
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = "Can't save data";
            }

            return result;
        }
        #endregion

        #region INS_GET100MDEFECTPOINT
        public decimal? INS_GET100MDEFECTPOINT(string P_INSLOT, string P_DEFECTID, decimal? P_LENGTH1, decimal? P_LENGTH2)
        {
            decimal? result = null;

            #region Check(s)

            if (string.IsNullOrWhiteSpace(P_INSLOT))
                return result;

            if (string.IsNullOrWhiteSpace(P_DEFECTID))
                return result;

            if (!HasConnection())
                return result;

            #endregion

            INS_GET100MDEFECTPOINTParameter dbPara = new INS_GET100MDEFECTPOINTParameter();
            dbPara.P_INSLOT = P_INSLOT;
            dbPara.P_DEFECTID = P_DEFECTID;
            dbPara.P_LENGTH1 = P_LENGTH1;
            dbPara.P_LENGTH2 = P_LENGTH2;

            INS_GET100MDEFECTPOINTResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_GET100MDEFECTPOINT(dbPara);

                if (null != dbResult)
                {
                    if (dbResult.R_POINT != null)
                    {
                        result = dbResult.R_POINT;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = null;
            }

            return result;
        }
        #endregion

        #region INS_GETRESETSTARTLENGTH
        public decimal? INS_GETRESETSTARTLENGTH(string P_ITMCODE, string P_CUSTOMERID)
        {
            decimal? result = null;

            #region Check(s)

            if (string.IsNullOrWhiteSpace(P_ITMCODE))
                return result;

            if (string.IsNullOrWhiteSpace(P_CUSTOMERID))
                return result;

            if (!HasConnection())
                return result;

            #endregion

            INS_GETRESETSTARTLENGTHParameter dbPara = new INS_GETRESETSTARTLENGTHParameter();
            dbPara.P_ITMCODE = P_ITMCODE;
            dbPara.P_CUSTOMERID = P_CUSTOMERID;

           List<INS_GETRESETSTARTLENGTHResult> dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.INS_GETRESETSTARTLENGTH(dbPara);

                if (null != dbResult)
                {
                    if (dbResult.Count > 0)
                    {
                        if (dbResult[0].RESETSTARTLENGTH != null)
                        {
                            result = dbResult[0].RESETSTARTLENGTH;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = null;
            }

            return result;
        }
        #endregion

        #endregion
    }

    #endregion
}

namespace LuckyTex.Services
{
    #region Master Data Service

    /// <summary>
    /// The data service for Master data.
    /// </summary>
    public class MasterDataService : BaseDataService
    {
        #region Singelton

        private static MasterDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static MasterDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(MasterDataService))
                    {
                        _instance = new MasterDataService();
                    }

                }
                return _instance;
            }
        }

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private MasterDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~MasterDataService()
        {
        }

        #endregion

        #region Public Methods

        #region เพิ่มใหม่ CUS_GETLIST ใช้ในการ Load Customer

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CUS_GETLISTData> GetCUS_GETLISTDataList()
        {
            List<CUS_GETLISTData> results = null;

            if (!HasConnection())
                return results;

            CUS_GETLISTParameter dbPara = new CUS_GETLISTParameter();

            List<CUS_GETLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.CUS_GETLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<CUS_GETLISTData>();
                    foreach (CUS_GETLISTResult dbResult in dbResults)
                    {
                        CUS_GETLISTData inst = new CUS_GETLISTData();

                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.CUSTOMERNAME = dbResult.CUSTOMERNAME;
                        inst.METHODID = dbResult.METHODID;
                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ CUS_GETITEMGOODBYCUSTOMER ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CUS_GETITEMGOODBYCUSTOMERData> GetCUS_GETITEMGOODBYCUSTOMERDataList(string cusID)
        {
            List<CUS_GETITEMGOODBYCUSTOMERData> results = null;

            if (!HasConnection())
                return results;

            CUS_GETITEMGOODBYCUSTOMERParameter dbPara = new CUS_GETITEMGOODBYCUSTOMERParameter();
            dbPara.P_CUSTOMERID = cusID;
            List<CUS_GETITEMGOODBYCUSTOMERResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.CUS_GETITEMGOODBYCUSTOMER(dbPara);
                if (null != dbResults)
                {
                    results = new List<CUS_GETITEMGOODBYCUSTOMERData>();
                    foreach (CUS_GETITEMGOODBYCUSTOMERResult dbResult in dbResults)
                    {
                        CUS_GETITEMGOODBYCUSTOMERData inst = new CUS_GETITEMGOODBYCUSTOMERData();

                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.ITM_CODE = dbResult.ITM_CODE;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ FinishingCustomerData ใช้ในการ Load Customer

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FinishingCustomerData> GetFinishingCustomerDataList()
        {
            List<FinishingCustomerData> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETCUTOMERLISTParameter dbPara = new FINISHING_GETCUTOMERLISTParameter();

            List<FINISHING_GETCUTOMERLISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETCUTOMERLIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<FinishingCustomerData>();
                    foreach (FINISHING_GETCUTOMERLISTResult dbResult in dbResults)
                    {
                        FinishingCustomerData inst = new FinishingCustomerData();

                        inst.FINISHINGCUSTOMER = dbResult.FINISHINGCUSTOMER;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ GetFINISHING_GETITEMGOODDataList ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FINISHING_GETITEMGOODData> GetFINISHING_GETITEMGOODDataList(string cusID)
        {
            List<FINISHING_GETITEMGOODData> results = null;

            if (!HasConnection())
                return results;

            FINISHING_GETITEMGOODParameter dbPara = new FINISHING_GETITEMGOODParameter();
            dbPara.P_CUSTOMER = cusID;

            List<FINISHING_GETITEMGOODResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_GETITEMGOOD(dbPara);
                if (null != dbResults)
                {
                    results = new List<FINISHING_GETITEMGOODData>();
                    foreach (FINISHING_GETITEMGOODResult dbResult in dbResults)
                    {
                        FINISHING_GETITEMGOODData inst = new FINISHING_GETITEMGOODData();

                        inst.CUSTOMERID = dbResult.CUSTOMERID;
                        inst.ITM_CODE = dbResult.ITM_CODE;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่มใหม่ Operator_SearchDataList ใช้ในการ Load Operator

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Operator_SearchData> Operator_SearchDataList(string opID, string title, string fName, string lName, string processID, string position)
        {
            List<Operator_SearchData> results = null;

            if (!HasConnection())
                return results;

            OPERATOR_SEARCHParameter dbPara = new OPERATOR_SEARCHParameter();
            dbPara.P_OPID = opID;
            dbPara.P_TITLE = title;
            dbPara.P_FNAME = fName;
            dbPara.P_LNAME = lName;
            dbPara.P_PROCESSID = processID;
            dbPara.P_POSITION = position;

            List<OPERATOR_SEARCHResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.OPERATOR_SEARCH(dbPara);
                if (null != dbResults)
                {
                    results = new List<Operator_SearchData>();
                    foreach (OPERATOR_SEARCHResult dbResult in dbResults)
                    {
                        Operator_SearchData inst = new Operator_SearchData();

                        inst.OPERATORID = dbResult.OPERATORID;
                        inst.TITLE = dbResult.TITLE;
                        inst.FNAME = dbResult.FNAME;
                        inst.LNAME = dbResult.LNAME;
                        inst.USERNAME = dbResult.USERNAME;
                        inst.PASSWORD = dbResult.PASSWORD;
                        inst.DELETEFLAG = dbResult.DELETEFLAG;
                        inst.POSITIONLEVEL = dbResult.POSITIONLEVEL;
                        inst.PROCESSID = dbResult.PROCESSID;
                        inst.DELETEFLAG = dbResult.DELETEFLAG;

                        inst.CREATEDDATE = dbResult.CREATEDDATE;
                        inst.CREATEDBY = dbResult.CREATEDBY;
                        inst.WEB = dbResult.WEB;
                        inst.WIP = dbResult.STOCK;

                        results.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return results;
        }

        #endregion

        #region เพิ่ม Save Operator

        public string SaveOperator(string opID,string title ,string fName , string lName ,string userName ,string password ,
            string positionLevel  ,string processID ,string deleteFlag,  string createdBy ,string web ,string wip)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(opID))
                return null;
            if (string.IsNullOrWhiteSpace(fName))
                return null;
            if (string.IsNullOrWhiteSpace(password))
                return null;
            if (string.IsNullOrWhiteSpace(positionLevel))
                return null;
            if (string.IsNullOrWhiteSpace(processID))
                return null;

            if (!HasConnection())
                return result;

            OPERATOR_INSERTUPDATEParameter dbPara = new OPERATOR_INSERTUPDATEParameter();
            dbPara.P_OPID = opID;
            dbPara.P_FNAME = fName;
            dbPara.P_LNAME = lName;
            dbPara.P_PROCESSID = processID;
            dbPara.P_USER = userName;
            dbPara.P_PASSWORD = password;
            dbPara.P_TITLE = title;
            dbPara.P_FLAG = deleteFlag;
            dbPara.P_POSITION = positionLevel;
            dbPara.P_CREATEBY = createdBy;
            dbPara.P_WEB = web;

            //New 07/05/19
            dbPara.P_WIP = wip;

            OPERATOR_INSERTUPDATEResult dbResult = null;
            try
            {
                dbResult = DatabaseManager.Instance.OPERATOR_INSERTUPDATE(dbPara);
                if (null != dbResult)
                {
                    if (!string.IsNullOrWhiteSpace(dbResult.R_RESULT))
                    {
                        result = dbResult.R_RESULT;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #endregion
    }

    #endregion
}







