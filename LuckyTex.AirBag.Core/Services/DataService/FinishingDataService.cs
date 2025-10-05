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
    #region Finishing Data Service

    /// <summary>
    /// The data service for Finishing process.
    /// </summary>
    public class FinishingDataService : BaseDataService
    {
        #region Singelton

        private static FinishingDataService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static FinishingDataService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(FinishingDataService))
                    {
                        _instance = new FinishingDataService();
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
        private FinishingDataService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~FinishingDataService()
        {
        }

        #endregion

        #region Public Methods

        #region Get related Finishing methods

        /// <summary>
        /// Gets all Finishing Machines.
        /// </summary>
        /// <returns>Returns inspection machine.</returns>
        public List<FinishingMCItem> GetMachines()
        {
            List<FinishingMCItem> results = new List<FinishingMCItem>();

            // Inspection Process ID = 8
            List<GETMACHINELISTBYPROCESSIDResult> dbResults = this.GetMachines(6);
            if (null == dbResults || dbResults.Count <= 0)
                return results;

            int mcNo = 1;
            FinishingMCItem inst = null;
            foreach (GETMACHINELISTBYPROCESSIDResult dbResult in dbResults)
            {
                inst = new FinishingMCItem();

                inst.MCNo = mcNo;
                inst.MCId = dbResult.MACHINEID;
                inst.DisplayName = dbResult.MCNAME;

                results.Add(inst);

                ++mcNo;
            }

            // Add special 2 machine(s) for display on another page.
            inst = new FinishingMCItem();
            inst.MCNo = mcNo;
            inst.MCId = "FL";
            inst.DisplayName = "Finishing List";
            results.Add(inst);
            ++mcNo;

            inst = new FinishingMCItem();
            inst.MCNo = mcNo;
            inst.MCId = "FR";
            inst.DisplayName = "Finish Record";
            results.Add(inst);
            ++mcNo;

            return results;
        }

        #endregion

        #region Create new Session

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mcItem"></param>
        /// <param name="loginResult"></param>
        /// <returns></returns>
        public FinishingSession GetSession(FinishingMCItem mcItem, LogInResult loginResult)
        {
            FinishingSession result = new FinishingSession();
            result.Init(mcItem, loginResult);
            return result;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="machineId"></param>
        /// <returns></returns>
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

        // ใช้สำหรับ Load ข้อมูล GETCURRENTINSDATA
        #region Load 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WEAVINGLOT"></param>
        /// <returns></returns>
        public GETCURRENTINSDATA GETCURRENTINSDATA(string FINISHINGLOT)
        {
            if (!HasConnection())
                return null;

            GETCURRENTINSDATAParameter dbPara = new GETCURRENTINSDATAParameter();
            dbPara.P_FINISHINGLOT = FINISHINGLOT;
            GETCURRENTINSDATAResult dbResults = null;

            GETCURRENTINSDATA result = new GETCURRENTINSDATA();

            try
            {
                dbResults = DatabaseManager.Instance.GETCURRENTINSDATA(dbPara);

                if (dbResults != null)
                {
                    result.ACTUALLENGTH = dbResults.ACTUALLENGTH;
                    result.TOTALINS = dbResults.TOTALINS;
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

        #region เพิ่มใหม่ ITM_GETITEMCODELIST ใช้ในการ Load ItemGood

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ITM_GETITEMCODELIST> ITM_GETITEMCODELIST()
        {
            List<ITM_GETITEMCODELIST> results = null;

            if (!HasConnection())
                return results;

            ITM_GETITEMCODELISTParameter dbPara = new ITM_GETITEMCODELISTParameter();

            List<ITM_GETITEMCODELISTResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.ITM_GETITEMCODELIST(dbPara);
                if (null != dbResults)
                {
                    results = new List<ITM_GETITEMCODELIST>();
                    foreach (ITM_GETITEMCODELISTResult dbResult in dbResults)
                    {
                        ITM_GETITEMCODELIST inst = new ITM_GETITEMCODELIST();

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

        #region Load GETMACHINELISTBYPROCESSID

        public List<GETMACHINELISTBYPROCESSID> GETMACHINELISTBYPROCESSID(string P_PROCESSID)
        {
            List<GETMACHINELISTBYPROCESSID> results = null;

            if (!HasConnection())
                return results;

            GETMACHINELISTBYPROCESSIDParameter dbPara = new GETMACHINELISTBYPROCESSIDParameter();
            dbPara.P_PROCESSID = P_PROCESSID;

            List<GETMACHINELISTBYPROCESSIDResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.GETMACHINELISTBYPROCESSID(dbPara);
                if (null != dbResults)
                {
                    results = new List<GETMACHINELISTBYPROCESSID>();
                    foreach (GETMACHINELISTBYPROCESSIDResult dbResult in dbResults)
                    {
                        GETMACHINELISTBYPROCESSID inst = new GETMACHINELISTBYPROCESSID();

                        inst.MACHINEID = dbResult.MACHINEID;
                        inst.MCNAME = dbResult.MCNAME;
                        inst.PROCESSDESCRIPTION = dbResult.PROCESSDESCRIPTION;

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

        #region Load FINISHING_SEARCHFINISHRECORD

        public List<FINISHING_SEARCHFINISHRECORD> FINISHING_SEARCHFINISHRECORD(string P_DATE, string P_MCNO)
        {
            List<FINISHING_SEARCHFINISHRECORD> results = null;

            if (!HasConnection())
                return results;

            FINISHING_SEARCHFINISHRECORDParameter dbPara = new FINISHING_SEARCHFINISHRECORDParameter();
            dbPara.P_DATE = P_DATE;
            dbPara.P_MCNO = P_MCNO;

            List<FINISHING_SEARCHFINISHRECORDResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_SEARCHFINISHRECORD(dbPara);
                if (null != dbResults)
                {
                    int RowNo = 1;

                    decimal? length1 = 0;
                    decimal? length2 = 0;
                    decimal? length3 = 0;
                    decimal? length4 = 0;
                    decimal? length5 = 0;
                    decimal? length6 = 0;
                    decimal? length7 = 0;

                    string strlength1 = string.Empty;
                    string strlength2 = string.Empty;
                    string strlength3 = string.Empty;
                    string strlength4 = string.Empty;
                    string strlength5 = string.Empty;
                    string strlength6 = string.Empty;
                    string strlength7 = string.Empty;

                    results = new List<FINISHING_SEARCHFINISHRECORD>();

                    foreach (FINISHING_SEARCHFINISHRECORDResult dbResult in dbResults)
                    {
                        FINISHING_SEARCHFINISHRECORD inst = new FINISHING_SEARCHFINISHRECORD();

                        inst.RowNo = RowNo;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.MCNO = dbResult.MCNO;
                        inst.MC = dbResult.MC;
                        inst.WEAVLENGTH = dbResult.WEAVLENGTH;
                        inst.WIDTH_BE = dbResult.WIDTH_BE;
                        inst.WIDTH_AF = dbResult.WIDTH_AF;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

                        //New 17/07/19
                        inst.PRODUCTIONTYPE = dbResult.PRODUCTIONTYPE;

                        length1 = 0;
                        length2 = 0;
                        length3 = 0;
                        length4 = 0;
                        length5 = 0;
                        length6 = 0;
                        length7 = 0;

                        strlength1 = string.Empty;
                        strlength2 = string.Empty;
                        strlength3 = string.Empty;
                        strlength4 = string.Empty;
                        strlength5 = string.Empty;
                        strlength6 = string.Empty;
                        strlength7 = string.Empty;

                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;

                        if (inst.LENGTH1 != null)
                        {
                            if (inst.LENGTH1 > 0)
                            {
                                length1 = inst.LENGTH1;
                                strlength1 = length1.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH2 != null)
                        {
                            if (inst.LENGTH2 > 0)
                            {
                                length2 = inst.LENGTH2;
                                strlength2 = " + " + length2.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH3 != null)
                        {
                            if (inst.LENGTH3 > 0)
                            {
                                length3 = inst.LENGTH3;
                                strlength3 = " + " + length3.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH4 != null)
                        {
                            if (inst.LENGTH4 > 0)
                            {
                                length4 = inst.LENGTH4;
                                strlength4 = " + " + length4.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH5 != null)
                        {
                            if (inst.LENGTH5 > 0)
                            {
                                length5 = inst.LENGTH5;
                                strlength5 = " + " + length5.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH6 != null)
                        {
                            if (inst.LENGTH6 > 0)
                            {
                                length6 = inst.LENGTH6;
                                strlength6 = " + " + length6.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH7 != null)
                        {
                            if (inst.LENGTH7 > 0)
                            {
                                length7 = inst.LENGTH7;
                                strlength7 = " + " + length7.Value.ToString("#,##0.##");
                            }
                        }

                        inst.TOTALLENGTH = (length1 + length2 + length3 + length4 + length5 + length6 + length7);

                        inst.FinishingLength = (strlength1 + strlength2 + strlength3 + strlength4 + strlength5 + strlength6 + strlength7);
                        results.Add(inst);

                        RowNo++;
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

        #region FINISHING_SEARCHFINISHRECORD  P_DATE,  P_MCNO,  P_ITMCODE

        public List<FINISHING_SEARCHFINISHRECORD> FINISHING_SEARCHFINISHRECORD(string P_DATE, string P_MCNO, string P_ITMCODE)
        {
            List<FINISHING_SEARCHFINISHRECORD> results = null;

            if (!HasConnection())
                return results;

            FINISHING_SEARCHFINISHRECORDParameter dbPara = new FINISHING_SEARCHFINISHRECORDParameter();
            dbPara.P_DATE = P_DATE;
            dbPara.P_MCNO = P_MCNO;
            dbPara.P_ITMCODE = P_ITMCODE;

            List<FINISHING_SEARCHFINISHRECORDResult> dbResults = null;

            try
            {
                dbResults = DatabaseManager.Instance.FINISHING_SEARCHFINISHRECORD(dbPara);
                if (null != dbResults)
                {
                    int RowNo = 1;

                    decimal? length1 = 0;
                    decimal? length2 = 0;
                    decimal? length3 = 0;
                    decimal? length4 = 0;
                    decimal? length5 = 0;
                    decimal? length6 = 0;
                    decimal? length7 = 0;

                    string strlength1 = string.Empty;
                    string strlength2 = string.Empty;
                    string strlength3 = string.Empty;
                    string strlength4 = string.Empty;
                    string strlength5 = string.Empty;
                    string strlength6 = string.Empty;
                    string strlength7 = string.Empty;

                    results = new List<FINISHING_SEARCHFINISHRECORD>();

                    foreach (FINISHING_SEARCHFINISHRECORDResult dbResult in dbResults)
                    {
                        FINISHING_SEARCHFINISHRECORD inst = new FINISHING_SEARCHFINISHRECORD();

                        inst.RowNo = RowNo;
                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.FINISHINGLOT = dbResult.FINISHINGLOT;
                        inst.ITM_CODE = dbResult.ITM_CODE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.CONDITIONBY = dbResult.CONDITIONBY;
                        inst.MCNO = dbResult.MCNO;
                        inst.MC = dbResult.MC;
                        inst.WEAVLENGTH = dbResult.WEAVLENGTH;
                        inst.WIDTH_BE = dbResult.WIDTH_BE;
                        inst.WIDTH_AF = dbResult.WIDTH_AF;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;

                        //New 17/07/19
                        inst.PRODUCTIONTYPE = dbResult.PRODUCTIONTYPE;

                        length1 = 0;
                        length2 = 0;
                        length3 = 0;
                        length4 = 0;
                        length5 = 0;
                        length6 = 0;
                        length7 = 0;

                        strlength1 = string.Empty;
                        strlength2 = string.Empty;
                        strlength3 = string.Empty;
                        strlength4 = string.Empty;
                        strlength5 = string.Empty;
                        strlength6 = string.Empty;
                        strlength7 = string.Empty;

                        inst.LENGTH1 = dbResult.LENGTH1;
                        inst.LENGTH2 = dbResult.LENGTH2;
                        inst.LENGTH3 = dbResult.LENGTH3;
                        inst.LENGTH4 = dbResult.LENGTH4;
                        inst.LENGTH5 = dbResult.LENGTH5;
                        inst.LENGTH6 = dbResult.LENGTH6;
                        inst.LENGTH7 = dbResult.LENGTH7;

                        if (inst.LENGTH1 != null)
                        {
                            if (inst.LENGTH1 > 0)
                            {
                                length1 = inst.LENGTH1;
                                strlength1 = length1.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH2 != null)
                        {
                            if (inst.LENGTH2 > 0)
                            {
                                length2 = inst.LENGTH2;
                                strlength2 = " + " + length2.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH3 != null)
                        {
                            if (inst.LENGTH3 > 0)
                            {
                                length3 = inst.LENGTH3;
                                strlength3 = " + " + length3.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH4 != null)
                        {
                            if (inst.LENGTH4 > 0)
                            {
                                length4 = inst.LENGTH4;
                                strlength4 = " + " + length4.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH5 != null)
                        {
                            if (inst.LENGTH5 > 0)
                            {
                                length5 = inst.LENGTH5;
                                strlength5 = " + " + length5.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH6 != null)
                        {
                            if (inst.LENGTH6 > 0)
                            {
                                length6 = inst.LENGTH6;
                                strlength6 = " + " + length6.Value.ToString("#,##0.##");
                            }
                        }

                        if (inst.LENGTH7 != null)
                        {
                            if (inst.LENGTH7 > 0)
                            {
                                length7 = inst.LENGTH7;
                                strlength7 = " + " + length7.Value.ToString("#,##0.##");
                            }
                        }

                        inst.TOTALLENGTH = (length1 + length2 + length3 + length4 + length5 + length6 + length7);

                        inst.FinishingLength = (strlength1 + strlength2 + strlength3 + strlength4 + strlength5 + strlength6 + strlength7);
                        results.Add(inst);

                        RowNo++;
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

        #region SaveFinishingData
        /// <summary>
        /// Insert or Update Finishing Data.
        /// </summary>
        /// <param name="itemCode">The Item Grey Code.</param>
        /// <param name="finishingLotNo">The Lot Number.</param>
        /// <param name="length">The length in meter.</param>
        /// <param name="partNo"></param>
        /// <returns>Retrusn false if some input is invalid.</returns>
        public bool SaveFinishingData(string itemCode, string finishingLotNo,
            decimal length, string partNo)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(itemCode))
                return result;
            if (string.IsNullOrWhiteSpace(finishingLotNo))
                return result;

            if (!HasConnection())
                return result;

            INSERTUPDATEFINISHINGDATAParameter dbPara = new INSERTUPDATEFINISHINGDATAParameter();
            dbPara.P_ITEMCODE = itemCode.Trim();
            dbPara.P_FINISHINGLOT = finishingLotNo.Trim();
            dbPara.P_LENGHT = length;
            dbPara.P_PARTNO = (string.IsNullOrWhiteSpace(partNo)) ? null : partNo.Trim();

            // ส่งค่า Null เข้าไปก่อนตอนนี้
            dbPara.P_CUSID = null;
        
            INSERTUPDATEFINISHINGDATAResult dbResult = null;
            try
            {
                dbResult =
                    DatabaseManager.Instance.INSERTUPDATEFINISHINGDATA(dbPara);

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

        #endregion
    }

    #endregion
}








