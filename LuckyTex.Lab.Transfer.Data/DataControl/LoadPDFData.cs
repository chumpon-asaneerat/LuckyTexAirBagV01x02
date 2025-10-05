#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Reflection;
using iTextSharp.text;

//using System.Xml;
//using System.Xml.Serialization;
//using NLib;
//using LuckyTex.Services;
//using System.Windows.Media;

#endregion

namespace DataControl.ClassData
{
    public class PDFClassData
    {
        #region PDFClassData
        private static PDFClassData _instance = null;

        public static PDFClassData Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(PDFClassData))
                    {
                        _instance = new PDFClassData();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Test Load File PDF

        #region ListID_PTF

        public class ListID_PTF
        {

            public ListID_PTF()
            {
                // default constructor
            }

            #region ListID_PTF


            public ListID_PTF(string P_specimenLabel, string P_specimenRep, string P_operatorID, string P_weavingLot, string P_weavingType, string P_finishingLot,
                decimal? P_ave1, decimal? P_ave2, decimal? P_ave3, decimal? P_ave4, decimal? P_ave5, decimal? P_ave6, decimal? P_avg)
            {
                #region ListID_PTF

                specimenLabel = P_specimenLabel;
                specimenRep = P_specimenRep;
                operatorID = P_operatorID;
                weavingLot = P_weavingLot;
                weavingType = P_weavingType;
                finishingLot = P_finishingLot;

                ave1 = P_ave1;
                ave2 = P_ave2;
                ave3 = P_ave3;
                ave4 = P_ave4;
                ave5 = P_ave5;
                ave6 = P_ave6;
                avg = P_avg;

                #endregion
            }

            #endregion

            public string specimenLabel { get; set; }
            public string specimenRep { get; set; }
            public string operatorID { get; set; }
            public string weavingLot { get; set; }
            public string weavingType { get; set; }
            public string finishingLot { get; set; }
            public decimal? ave1 { get; set; }
            public decimal? ave2 { get; set; }
            public decimal? ave3 { get; set; }
            public decimal? ave4 { get; set; }
            public decimal? ave5 { get; set; }
            public decimal? ave6 { get; set; }
            public decimal? avg { get; set; }
        }
        #endregion

        #region ListID_TENS

        public class ListID_TENS
        {

            public ListID_TENS()
            {
                // default constructor
            }

            #region ListID_TENS


            public ListID_TENS(string P_specimenLabel, string P_specimenRep, string P_operatorID, string P_weavingLot, string P_weavingType, string P_scouringLot,
                string P_lastTestDate, string P_lastTestTime,
                decimal? P_max1, decimal? P_max2, decimal? P_max3, decimal? P_max4, decimal? P_max5, decimal? P_max6,
                decimal? P_maxStrain1, decimal? P_maxStrain2, decimal? P_maxStrain3, decimal? P_maxStrain4, decimal? P_maxStrain5, decimal? P_maxStrain6, decimal? P_avg, string P_methodType)
            {
                #region ListID_TENS

                specimenLabel = P_specimenLabel;
                specimenRep = P_specimenRep;
                operatorID = P_operatorID;
                weavingLot = P_weavingLot;
                weavingType = P_weavingType;
                scouringLot = P_scouringLot;
                lastTestDate = P_lastTestDate;
                lastTestTime = P_lastTestTime;

                max1 = P_max1;
                max2 = P_max2;
                max3 = P_max3;
                max4 = P_max4;
                max5 = P_max5;
                max6 = P_max6;

                maxStrain1 = P_maxStrain1;
                maxStrain2 = P_maxStrain2;
                maxStrain3 = P_maxStrain3;
                maxStrain4 = P_maxStrain4;
                maxStrain5 = P_maxStrain5;
                maxStrain6 = P_maxStrain6;
                avg = P_avg;

                methodType = P_methodType;

                #endregion
            }

            #endregion

            public string specimenLabel { get; set; }
            public string specimenRep { get; set; }
            public string operatorID { get; set; }
            public string weavingLot { get; set; }
            public string weavingType { get; set; }
            public string scouringLot { get; set; }
            public string lastTestDate { get; set; }
            public string lastTestTime { get; set; }

            public decimal? max1 { get; set; }
            public decimal? max2 { get; set; }
            public decimal? max3 { get; set; }
            public decimal? max4 { get; set; }
            public decimal? max5 { get; set; }
            public decimal? max6 { get; set; }

            public decimal? maxStrain1 { get; set; }
            public decimal? maxStrain2 { get; set; }
            public decimal? maxStrain3 { get; set; }
            public decimal? maxStrain4 { get; set; }
            public decimal? maxStrain5 { get; set; }
            public decimal? maxStrain6 { get; set; }
            public decimal? avg { get; set; }

            public string methodType { get; set; }
        }
        #endregion

        #region LoadID_PTF
        public DataControl.ClassData.PDFClassData.ListID_PTF LoadID_PTF(string fileName)
        {
            try
            {
                if (fileName.Contains("is_ptf"))
                {
                    string text = string.Empty;

                    if (File.Exists(fileName))
                    {
                        PdfReader pdfReader = new PdfReader(fileName);

                        for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                        {
                            ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                            string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                            currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                            text = text + currentText;

                        }
                        pdfReader.Close();

                        if (text != string.Empty)
                        {
                            DataControl.ClassData.PDFClassData.ListID_PTF inst = new DataControl.ClassData.PDFClassData.ListID_PTF();

                            string test = string.Empty;

                            string[] lines = text.Split(new[] { "\n" },
                                                    StringSplitOptions.RemoveEmptyEntries);
                            bool chk_aveStart = false;

                            foreach (string file in lines)
                            {
                                try
                                {

                                    test = file.Substring(file.LastIndexOf(@"\") + 1);

                                    if (test != string.Empty)
                                    {
                                        string id_ptf = test;
                                        string specimenLabel = "Specimen label";
                                        string operatorID = "Operator ID";
                                        string weavingLot = "Weaving Lot no.";
                                        string finishingLot = "Finishing Lot no.";

                                        string ave = "Ave Load@All Peaks";
                                        string mean = "Mean";

                                        bool chk_specimen = id_ptf.Contains(specimenLabel);
                                        bool chk_operator = id_ptf.Contains(operatorID);
                                        bool chk_weaving = id_ptf.Contains(weavingLot);
                                        bool chk_finishing = id_ptf.Contains(finishingLot);

                                        bool chk_ave = id_ptf.Contains(ave);
                                        bool chk_mean = id_ptf.Contains(mean);


                                        String strString = id_ptf.Substring(0, id_ptf.Length).Trim();
                                        strString = strString.Replace(" ", ",").TrimEnd();
                                        String[] myArr = strString.Split(',');

                                        if (myArr.Length > 1)
                                        {
                                            if (chk_specimen)
                                            {
                                                inst.specimenLabel = myArr[2].ToString().Trim();

                                                if (myArr.Length > 3)
                                                {
                                                    id_ptf = id_ptf.Replace(specimenLabel, "").TrimEnd();
                                                    inst.specimenRep = id_ptf.Replace(myArr[2].ToString().Trim(), "").TrimStart().TrimEnd();
                                                }

                                                //inst.specimenLabel = id_ptf.Replace(specimenLabel, "").TrimEnd();
                                            }

                                            if (chk_operator)
                                            {
                                                inst.operatorID = myArr[2].ToString().Trim();
                                                //inst.operatorID = id_ptf.Replace(operatorID, "").TrimEnd();
                                            }

                                            if (chk_weaving)
                                            {
                                                inst.weavingLot = myArr[3].ToString().Trim();

                                                if (myArr.Length > 4)
                                                {
                                                    id_ptf = id_ptf.Replace(weavingLot, "").TrimEnd();
                                                    inst.weavingType = id_ptf.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd();
                                                }

                                                //inst.weavingLot = id_ptf.Replace(weavingLot, "").TrimEnd();
                                            }

                                            if (chk_finishing)
                                            {
                                                inst.finishingLot = myArr[3].ToString().Trim();
                                                //inst.finishingLot = id_ptf.Replace(finishingLot, "").TrimEnd();
                                            }

                                            if (chk_ave)
                                            {
                                                chk_aveStart = true;
                                            }

                                            if (chk_aveStart)
                                            {
                                                if (myArr[0] == "1")
                                                {
                                                    if (myArr[1] != null)
                                                        inst.ave1 = decimal.Parse(myArr[1].ToString());
                                                }

                                                if (myArr[0] == "2")
                                                {
                                                    if (myArr[1] != null)
                                                        inst.ave2 = decimal.Parse(myArr[1].ToString());
                                                }

                                                if (myArr[0] == "3")
                                                {
                                                    if (myArr[1] != null)
                                                        inst.ave3 = decimal.Parse(myArr[1].ToString());
                                                }

                                                if (chk_mean)
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    if (myArr[0] == "4")
                                                    {
                                                        if (myArr[1] != null)
                                                            inst.ave4 = decimal.Parse(myArr[1].ToString());
                                                    }

                                                    if (myArr[0] == "5")
                                                    {
                                                        if (myArr[1] != null)
                                                            inst.ave5 = decimal.Parse(myArr[1].ToString());
                                                    }

                                                    if (myArr[0] == "6")
                                                    {
                                                        if (myArr[1] != null)
                                                            inst.ave6 = decimal.Parse(myArr[1].ToString());
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // Could not load the image - probably related to Windows file system permissions.
                                    System.Windows.Forms.MessageBox.Show("Cannot display the : " + file.Substring(file.LastIndexOf('\\'))
                                        + ". You may not have permission to read the file, or " +
                                        "it may be corrupt.\n\nReported error: " + ex.Message);

                                    break;
                                }
                            }

                            int? i = 0;

                            decimal? ave1 = inst.ave1;
                            decimal? ave2 = inst.ave2;
                            decimal? ave3 = inst.ave3;
                            decimal? ave4 = inst.ave4;
                            decimal? ave5 = inst.ave5;
                            decimal? ave6 = inst.ave6;

                            #region Old
                            //if (ave1 != null && ave1 != 0)
                            //    i++;
                            //else
                            //    ave1 = 0;

                            //if (ave2 != null && ave2 != 0)
                            //    i++;
                            //else
                            //    ave2 = 0;

                            //if (ave3 != null && ave3 != 0)
                            //    i++;
                            //else
                            //    ave3 = 0;

                            //if (ave4 != null && ave4 != 0)
                            //    i++;
                            //else
                            //    ave4 = 0;

                            //if (ave5 != null && ave5 != 0)
                            //    i++;
                            //else
                            //    ave5 = 0;

                            //if (ave6 != null && ave6 != 0)
                            //    i++;
                            //else
                            //    ave6 = 0;
                            #endregion

                            #region New

                            if (ave1 == null)
                                ave1 = 0;
                            else
                                i++;

                            if (ave2 == null)
                                ave2 = 0;
                            else
                                i++;

                            if (ave3 == null)
                                ave3 = 0;
                            else
                                i++;

                            if (ave4 == null)
                                ave4 = 0;
                            else
                                i++;

                            if (ave5 == null)
                                ave5 = 0;
                            else
                                i++;

                            if (ave6 == null)
                                ave6 = 0;
                            else
                                i++;

                            #endregion

                            if (ave1 != 0 || ave2 != 0 || ave3 != 0 || ave4 != 0 || ave5 != 0 || ave6 != 0)
                                inst.avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 2);
                            else
                                inst.avg = 0;

                            return inst;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region LoadID_TENS
        public DataControl.ClassData.PDFClassData.ListID_TENS LoadID_TENS(string fileName)
        {
            try
            {
                if (fileName.Contains("is_tens"))
                {
                    string text = string.Empty;

                    if (File.Exists(fileName))
                    {
                        PdfReader pdfReader = new PdfReader(fileName);

                        for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                        {
                            ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                            string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                            currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                            text = text + currentText;

                        }
                        pdfReader.Close();

                        if (text != string.Empty)
                        {
                            DataControl.ClassData.PDFClassData.ListID_TENS inst = new DataControl.ClassData.PDFClassData.ListID_TENS();

                            string test = string.Empty;

                            string[] lines = text.Split(new[] { "\n" },
                                                    StringSplitOptions.RemoveEmptyEntries);
                            bool chk_maxStart = false;
                            bool chk_method = false;

                            foreach (string file in lines)
                            {
                                try
                                {

                                    test = file.Substring(file.LastIndexOf(@"\") + 1);

                                    if (test != string.Empty)
                                    {
                                        string id_tens = test;
                                        string specimenLabel = "Specimen label";
                                        string operatorID = "Opertor ID";
                                        string weavingLot = "Weaving Lot no.";
                                        string scouringLot = "Scouring Lot no.";

                                        string testdate = "Last test date";
                                        string testtime = "Last test time";

                                        string method = "Edge comb.im_tens";
                                        string max = "Max Ld Max Ld Strain@Max Ld";
                                        string mean = "Mean";

                                        bool chk_specimen = id_tens.Contains(specimenLabel);
                                        bool chk_operator = id_tens.Contains(operatorID);
                                        bool chk_weaving = id_tens.Contains(weavingLot);
                                        bool chk_scouring = id_tens.Contains(scouringLot);

                                        bool chk_testdate = id_tens.Contains(testdate);
                                        bool chk_testtime = id_tens.Contains(testtime);

                                        bool chk_met = id_tens.Contains(method);
                                        bool chk_max = id_tens.Contains(max);
                                        bool chk_mean = id_tens.Contains(mean);

                                        String strString = id_tens.Substring(0, id_tens.Length).Trim();
                                        strString = strString.Replace(" ", ",").TrimEnd();
                                        String[] myArr = strString.Split(',');

                                        if (myArr.Length > 1)
                                        {
                                            if (chk_specimen)
                                            {
                                                inst.specimenLabel = myArr[2].ToString().Trim();

                                                if (myArr.Length > 3)
                                                {
                                                    id_tens = id_tens.Replace(specimenLabel, "").TrimEnd();
                                                    inst.specimenRep = id_tens.Replace(myArr[2].ToString().Trim(), "").TrimStart().TrimEnd();
                                                }
                                                //inst.specimenLabel = id_tens.Replace(specimenLabel, "").TrimEnd();
                                            }

                                            if (chk_operator)
                                            {
                                                inst.operatorID = myArr[2].ToString().Trim();
                                                //inst.operatorID = id_tens.Replace(operatorID, "").TrimEnd();
                                            }

                                            if (chk_weaving)
                                            {
                                                inst.weavingLot = myArr[3].ToString().Trim();

                                                if (myArr.Length > 4)
                                                {
                                                    id_tens = id_tens.Replace(weavingLot, "").TrimEnd();
                                                    inst.weavingType = id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd();
                                                }

                                                //inst.weavingLot = id_tens.Replace(weavingLot, "").TrimEnd();
                                            }

                                            if (chk_scouring)
                                            {
                                                inst.scouringLot = myArr[3].ToString().Trim();
                                                //inst.scouringLot = id_tens.Replace(scouringLot, "").TrimEnd();
                                            }

                                            if (chk_testdate)
                                            {
                                                //inst.lastTestDate = myArr[3].ToString().Trim();
                                                inst.lastTestDate = id_tens.Replace(testdate, "").TrimEnd();
                                            }

                                            if (chk_testtime)
                                            {
                                                //inst.lastTestTime = myArr[3].ToString().Trim();
                                                inst.lastTestTime = id_tens.Replace(testtime, "").TrimEnd();
                                            }

                                            if (chk_met == true)
                                            {
                                                chk_method = true;
                                            }

                                            if (chk_max)
                                            {
                                                chk_maxStart = true;
                                            }

                                            if (chk_maxStart)
                                            {
                                                if (chk_method == false)
                                                {
                                                    #region chk_method = false;

                                                    if (myArr[0] == "1")
                                                    {
                                                        if (myArr[2] != null)
                                                            inst.max1 = decimal.Parse(myArr[2].ToString());
                                                        if (myArr[3] != null)
                                                            inst.maxStrain1 = decimal.Parse(myArr[3].ToString());
                                                    }

                                                    if (myArr[0] == "2")
                                                    {
                                                        if (myArr[2] != null)
                                                            inst.max2 = decimal.Parse(myArr[2].ToString());

                                                        if (myArr[3] != null)
                                                            inst.maxStrain2 = decimal.Parse(myArr[3].ToString());
                                                    }

                                                    if (myArr[0] == "3")
                                                    {
                                                        if (myArr[2] != null)
                                                            inst.max3 = decimal.Parse(myArr[2].ToString());

                                                        if (myArr[3] != null)
                                                            inst.maxStrain3 = decimal.Parse(myArr[3].ToString());
                                                    }

                                                    if (chk_mean)
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        if (myArr[0] == "4")
                                                        {
                                                            if (myArr[2] != null)
                                                                inst.max4 = decimal.Parse(myArr[2].ToString());
                                                            if (myArr[3] != null)
                                                                inst.maxStrain4 = decimal.Parse(myArr[3].ToString());
                                                        }

                                                        if (myArr[0] == "5")
                                                        {
                                                            if (myArr[2] != null)
                                                                inst.max5 = decimal.Parse(myArr[2].ToString());

                                                            if (myArr[3] != null)
                                                                inst.maxStrain5 = decimal.Parse(myArr[3].ToString());
                                                        }

                                                        if (myArr[0] == "6")
                                                        {
                                                            if (myArr[2] != null)
                                                                inst.max6 = decimal.Parse(myArr[2].ToString());

                                                            if (myArr[3] != null)
                                                                inst.maxStrain6 = decimal.Parse(myArr[3].ToString());
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                else if (chk_method == true)
                                                {
                                                    inst.methodType = "Edge comb";

                                                    #region chk_method = true;

                                                    if (myArr[0] == "1")
                                                    {
                                                        if (myArr[2] != null)
                                                            inst.max1 = decimal.Parse(myArr[2].ToString());
                                                    }

                                                    if (myArr[0] == "2")
                                                    {
                                                        if (myArr[2] != null)
                                                            inst.max2 = decimal.Parse(myArr[2].ToString());
                                                    }

                                                    if (myArr[0] == "3")
                                                    {
                                                        if (myArr[2] != null)
                                                            inst.max3 = decimal.Parse(myArr[2].ToString());
                                                    }

                                                    if (chk_mean)
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        if (myArr[0] == "4")
                                                        {
                                                            if (myArr[2] != null)
                                                                inst.max4 = decimal.Parse(myArr[2].ToString());
                                                        }

                                                        if (myArr[0] == "5")
                                                        {
                                                            if (myArr[2] != null)
                                                                inst.max5 = decimal.Parse(myArr[2].ToString());
                                                        }

                                                        if (myArr[0] == "6")
                                                        {
                                                            if (myArr[2] != null)
                                                                inst.max6 = decimal.Parse(myArr[2].ToString());
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // Could not load the image - probably related to Windows file system permissions.
                                    System.Windows.Forms.MessageBox.Show("Cannot display the : " + file.Substring(file.LastIndexOf('\\'))
                                        + ". You may not have permission to read the file, or " +
                                        "it may be corrupt.\n\nReported error: " + ex.Message);

                                    break;
                                }
                            }

                            return inst;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #endregion

        #region TENSILE

        #region ListTENSILE

        public class ListTENSILE
        {

            public ListTENSILE()
            {
                // default constructor
            }

            #region ListTENSILE


            public ListTENSILE(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, string P_OPERATOR, string P_TESTDATE, string P_TESTTIME, string P_YARN,
                decimal? P_TENSILE1, decimal? P_TENSILE2, decimal? P_TENSILE3, decimal? P_TENSILE4, decimal? P_TENSILE5, decimal? P_TENSILE6, decimal? P_AVE_TENSILE,
                decimal? P_ELONG1, decimal? P_ELONG2, decimal? P_ELONG3, decimal? P_ELONG4, decimal? P_ELONG5, decimal? P_ELONG6, decimal? P_AVE_ELONG, string P_FILEPDF)
            {
                #region ListID_PTF

                ITMCODE = P_ITMCODE;
                WEAVINGLOG = P_WEAVINGLOG;
                FINISHINGLOT = P_FINISHINGLOT;
                OPERATOR = P_OPERATOR;
                TESTDATE = P_TESTDATE;
                TESTTIME = P_TESTTIME;
                YARN = P_YARN;

                TENSILE1 = P_TENSILE1;
                TENSILE2 = P_TENSILE2;
                TENSILE3 = P_TENSILE3;
                TENSILE4 = P_TENSILE4;
                TENSILE5 = P_TENSILE5;
                TENSILE6 = P_TENSILE6;
                AVE_TENSILE = P_AVE_TENSILE;

                ELONG1 = P_ELONG1;
                ELONG2 = P_ELONG2;
                ELONG3 = P_ELONG3;
                ELONG4 = P_ELONG4;
                ELONG5 = P_ELONG5;
                ELONG6 = P_ELONG6;
                AVE_ELONG = P_AVE_ELONG;

                FILEPDF = P_FILEPDF;

                #endregion
            }

            #endregion

            public string ITMCODE { get; set; }
            public string WEAVINGLOG { get; set; }
            public string FINISHINGLOT { get; set; }
            public string OPERATOR { get; set; }
            public string TESTDATE { get; set; }
            public string TESTTIME { get; set; }
            public string YARN { get; set; }

            public decimal? TENSILE1 { get; set; }
            public decimal? TENSILE2 { get; set; }
            public decimal? TENSILE3 { get; set; }
            public decimal? TENSILE4 { get; set; }
            public decimal? TENSILE5 { get; set; }
            public decimal? TENSILE6 { get; set; }
            public decimal? AVE_TENSILE { get; set; }

            public decimal? ELONG1 { get; set; }
            public decimal? ELONG2 { get; set; }
            public decimal? ELONG3 { get; set; }
            public decimal? ELONG4 { get; set; }
            public decimal? ELONG5 { get; set; }
            public decimal? ELONG6 { get; set; }
            public decimal? AVE_ELONG { get; set; }

            public string FILEPDF { get; set; }
        }
        #endregion

        #region LoadTENSILE
        public DataControl.ClassData.PDFClassData.ListTENSILE LoadTENSILE(string fileName)
        {
            try
            {
                if (fileName.Contains("is_tens"))
                {
                    string text = string.Empty;

                    if (File.Exists(fileName))
                    {
                        PdfReader pdfReader = new PdfReader(fileName);

                        for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                        {
                            ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                            string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                            currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                            text = text + currentText;

                        }
                        pdfReader.Close();

                        if (text != string.Empty)
                        {
                            DataControl.ClassData.PDFClassData.ListTENSILE inst = new DataControl.ClassData.PDFClassData.ListTENSILE();

                            string test = string.Empty;

                            string[] lines = text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            bool chk_maxStart = false;
                            bool chk_itemcode = false;

                            string method = "Edge comb.im_tens";
                            string method2 = "Edgecomb.im_tens";

                            if (lines != null)
                            {
                                if (lines[10].ToString().Contains(method) == false && lines[10].ToString().Contains(method2) == false)
                                {
                                    #region Select Data

                                    foreach (string file in lines)
                                    {
                                        try
                                        {

                                            test = file.Substring(file.LastIndexOf(@"\") + 1);

                                            if (test != string.Empty)
                                            {
                                                string id_tens = test;
                                                string specimenLabel = "Specimen label";
                                                string operatorID = "Opertor ID";
                                                string weavingLot = "Weaving Lot no.";
                                                string finishing = "Scouring Lot no.";

                                                string testdate = "Last test date";
                                                string testtime = "Last test time";

                                                string max = "Max Ld Max Ld Strain@Max Ld";
                                                //30/08/21
                                                string max2 = "Maximum Load Strain@Max Ld";

                                                string mean = "Mean";

                                                bool chk_specimen = id_tens.Contains(specimenLabel);
                                                bool chk_operator = id_tens.Contains(operatorID);
                                                bool chk_weaving = id_tens.Contains(weavingLot);
                                                bool chk_finishing = id_tens.Contains(finishing);

                                                bool chk_testdate = id_tens.Contains(testdate);
                                                bool chk_testtime = id_tens.Contains(testtime);

                                                bool chk_max = id_tens.Contains(max);
                                                bool chk_max2 = id_tens.Contains(max2);

                                                bool chk_mean = id_tens.Contains(mean);

                                               
                                                String strString = id_tens.Substring(0, id_tens.Length).Trim();
                                                strString = strString.Replace(" ", ",").TrimEnd();
                                                String[] myArr = strString.Split(',');

                                                if (myArr.Length > 1)
                                                {
                                                    if (chk_specimen)
                                                    {
                                                        inst.ITMCODE = myArr[2].ToString().Trim();

                                                        if (inst.ITMCODE.Contains("4L46C15R") || inst.ITMCODE.Contains("4746C00") || inst.ITMCODE.Contains("4755C00") ||
                                                            inst.ITMCODE.Contains("68147") || inst.ITMCODE.Contains("4755ATW") || inst.ITMCODE.Contains("4755AT") ||
                                                            inst.ITMCODE.Contains("4746W") || inst.ITMCODE.Contains("A50FFBL") || inst.ITMCODE.Contains("4746G05Z") ||
                                                            inst.ITMCODE.Contains("A5ZBBALZ") || inst.ITMCODE.Contains("4753G00S"))
                                                        {
                                                            chk_itemcode = true;
                                                        }
                                                    }

                                                    if (chk_operator)
                                                    {
                                                        inst.OPERATOR = myArr[2].ToString().Trim();
                                                    }

                                                    if (chk_weaving)
                                                    {
                                                        inst.WEAVINGLOG = myArr[3].ToString().Trim();

                                                        if (myArr.Length > 4)
                                                        {
                                                            id_tens = id_tens.Replace(weavingLot, "").TrimEnd();

                                                            if (id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd() == "W")
                                                            {
                                                                inst.YARN = "Warp";
                                                            }
                                                            else if (id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd() == "F")
                                                            {
                                                                inst.YARN = "Weft";
                                                            }
                                                            else if (id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd() == "WR")
                                                            {
                                                                inst.YARN = "Warp-R";
                                                            }
                                                            else if (id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd() == "FR")
                                                            {
                                                                inst.YARN = "Weft-R";
                                                            }
                                                            else if (id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd() == "WL")
                                                            {
                                                                inst.YARN = "Warp-L";
                                                            }
                                                            else if (id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd() == "FL")
                                                            {
                                                                inst.YARN = "Weft-L";
                                                            }
                                                            else
                                                            {
                                                                inst.YARN = id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd();
                                                            }
                                                        }
                                                    }

                                                    if (chk_finishing)
                                                    {
                                                        inst.FINISHINGLOT = myArr[3].ToString().Trim();
                                                    }

                                                    if (chk_testdate)
                                                    {
                                                        inst.TESTDATE = id_tens.Replace(testdate, "").TrimStart().TrimEnd();
                                                    }

                                                    if (chk_testtime)
                                                    {
                                                        inst.TESTTIME = id_tens.Replace(testtime, "").TrimStart().TrimEnd();
                                                    }

                                                    if (chk_max)
                                                    {
                                                        chk_maxStart = true;
                                                    }
                                                    else if (chk_max2)
                                                    {
                                                        chk_maxStart = true;
                                                    }

                                                    if (chk_maxStart)
                                                    {
                                                        if (chk_itemcode == false)
                                                        {
                                                            #region TENSILE  & ELONG;

                                                            if (myArr[0] == "1")
                                                            {
                                                                if (myArr[2] != null)
                                                                    inst.TENSILE1 = decimal.Parse(myArr[2].ToString());
                                                                if (myArr[3] != null)
                                                                    inst.ELONG1 = decimal.Parse(myArr[3].ToString());
                                                            }

                                                            if (myArr[0] == "2")
                                                            {
                                                                if (myArr[2] != null)
                                                                    inst.TENSILE2 = decimal.Parse(myArr[2].ToString());

                                                                if (myArr[3] != null)
                                                                    inst.ELONG2 = decimal.Parse(myArr[3].ToString());
                                                            }

                                                            if (myArr[0] == "3")
                                                            {
                                                                if (myArr[2] != null)
                                                                    inst.TENSILE3 = decimal.Parse(myArr[2].ToString());

                                                                if (myArr[3] != null)
                                                                    inst.ELONG3 = decimal.Parse(myArr[3].ToString());
                                                            }

                                                            if (chk_mean)
                                                            {
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                if (myArr[0] == "4")
                                                                {
                                                                    if (myArr[2] != null)
                                                                        inst.TENSILE4 = decimal.Parse(myArr[2].ToString());
                                                                    if (myArr[3] != null)
                                                                        inst.ELONG4 = decimal.Parse(myArr[3].ToString());
                                                                }

                                                                if (myArr[0] == "5")
                                                                {
                                                                    if (myArr[2] != null)
                                                                        inst.TENSILE5 = decimal.Parse(myArr[2].ToString());

                                                                    if (myArr[3] != null)
                                                                        inst.ELONG5 = decimal.Parse(myArr[3].ToString());
                                                                }

                                                                if (myArr[0] == "6")
                                                                {
                                                                    if (myArr[2] != null)
                                                                        inst.TENSILE6 = decimal.Parse(myArr[2].ToString());

                                                                    if (myArr[3] != null)
                                                                        inst.ELONG6 = decimal.Parse(myArr[3].ToString());
                                                                }
                                                            }
                                                            #endregion
                                                        }
                                                        else
                                                        {
                                                            #region TENSILE  & ELONG;

                                                            if (myArr[0] == "1")
                                                            {
                                                                if (myArr[2] != null)
                                                                    inst.TENSILE1 = MathEx.Round((decimal.Parse(myArr[2].ToString()) / 3),2);
                                                                if (myArr[3] != null)
                                                                    inst.ELONG1 = decimal.Parse(myArr[3].ToString());
                                                            }

                                                            if (myArr[0] == "2")
                                                            {
                                                                if (myArr[2] != null)
                                                                    inst.TENSILE2 = MathEx.Round((decimal.Parse(myArr[2].ToString()) / 3),2);

                                                                if (myArr[3] != null)
                                                                    inst.ELONG2 = decimal.Parse(myArr[3].ToString());
                                                            }

                                                            if (myArr[0] == "3")
                                                            {
                                                                if (myArr[2] != null)
                                                                    inst.TENSILE3 = MathEx.Round((decimal.Parse(myArr[2].ToString()) / 3),2);

                                                                if (myArr[3] != null)
                                                                    inst.ELONG3 = decimal.Parse(myArr[3].ToString());
                                                            }

                                                            if (chk_mean)
                                                            {
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                if (myArr[0] == "4")
                                                                {
                                                                    if (myArr[2] != null)
                                                                        inst.TENSILE4 = MathEx.Round((decimal.Parse(myArr[2].ToString()) / 3),2);
                                                                    if (myArr[3] != null)
                                                                        inst.ELONG4 = decimal.Parse(myArr[3].ToString());
                                                                }

                                                                if (myArr[0] == "5")
                                                                {
                                                                    if (myArr[2] != null)
                                                                        inst.TENSILE5 = MathEx.Round((decimal.Parse(myArr[2].ToString()) / 3),2);

                                                                    if (myArr[3] != null)
                                                                        inst.ELONG5 = decimal.Parse(myArr[3].ToString());
                                                                }

                                                                if (myArr[0] == "6")
                                                                {
                                                                    if (myArr[2] != null)
                                                                        inst.TENSILE6 = MathEx.Round((decimal.Parse(myArr[2].ToString()) / 3),2);

                                                                    if (myArr[3] != null)
                                                                        inst.ELONG6 = decimal.Parse(myArr[3].ToString());
                                                                }
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            // Could not load the image - probably related to Windows file system permissions.
                                            System.Windows.Forms.MessageBox.Show("Cannot display the : " + file.Substring(file.LastIndexOf('\\'))
                                                + ". You may not have permission to read the file, or " +
                                                "it may be corrupt.\n\nReported error: " + ex.Message);

                                            break;
                                        }
                                    }

                                    #endregion

                                    #region AVE_TENSILE

                                    int? i = 0;

                                    decimal? tensile1 = inst.TENSILE1;
                                    decimal? tensile2 = inst.TENSILE2;
                                    decimal? tensile3 = inst.TENSILE3;
                                    decimal? tensile4 = inst.TENSILE4;
                                    decimal? tensile5 = inst.TENSILE5;
                                    decimal? tensile6 = inst.TENSILE6;

                                    if (tensile1 != null && tensile1 != 0)
                                        i++;
                                    else
                                        tensile1 = 0;

                                    if (tensile2 != null && tensile2 != 0)
                                        i++;
                                    else
                                        tensile2 = 0;

                                    if (tensile3 != null && tensile3 != 0)
                                        i++;
                                    else
                                        tensile3 = 0;

                                    if (tensile4 != null && tensile4 != 0)
                                        i++;
                                    else
                                        tensile4 = 0;

                                    if (tensile5 != null && tensile5 != 0)
                                        i++;
                                    else
                                        tensile5 = 0;

                                    if (tensile6 != null && tensile6 != 0)
                                        i++;
                                    else
                                        tensile6 = 0;

                                    if (tensile1 != 0 || tensile2 != 0 || tensile3 != 0 || tensile4 != 0 || tensile5 != 0 || tensile6 != 0)
                                        inst.AVE_TENSILE = DataControl.ClassData.MathEx.Round(((tensile1 + tensile2 + tensile3 + tensile4 + tensile5 + tensile6) / i).Value, 2);
                                    else
                                        inst.AVE_TENSILE = 0;

                                    #endregion

                                    #region AVE_ELONG

                                    int? e = 0;

                                    decimal? elong1 = inst.ELONG1;
                                    decimal? elong2 = inst.ELONG2;
                                    decimal? elong3 = inst.ELONG3;
                                    decimal? elong4 = inst.ELONG4;
                                    decimal? elong5 = inst.ELONG5;
                                    decimal? elong6 = inst.ELONG6;

                                    if (elong1 != null && elong1 != 0)
                                        e++;
                                    else
                                        elong1 = 0;

                                    if (elong2 != null && elong2 != 0)
                                        e++;
                                    else
                                        elong2 = 0;

                                    if (elong3 != null && elong3 != 0)
                                        e++;
                                    else
                                        elong3 = 0;

                                    if (elong4 != null && elong4 != 0)
                                        e++;
                                    else
                                        elong4 = 0;

                                    if (elong5 != null && elong5 != 0)
                                        e++;
                                    else
                                        elong5 = 0;

                                    if (elong6 != null && elong6 != 0)
                                        e++;
                                    else
                                        elong6 = 0;

                                    if (elong1 != 0 || elong2 != 0 || elong3 != 0 || elong4 != 0 || elong5 != 0 || elong6 != 0)
                                        inst.AVE_ELONG = DataControl.ClassData.MathEx.Round(((elong1 + elong2 + elong3 + elong4 + elong5 + elong6) / e).Value, 2);
                                    else
                                        inst.AVE_ELONG = 0;

                                    #endregion

                                    inst.FILEPDF = fileName;

                                    return inst;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #endregion

        #region EDGECOMB

        #region ListEDGECOMB

        public class ListEDGECOMB
        {

            public ListEDGECOMB()
            {
                // default constructor
            }

            #region ListEDGECOMB


            public ListEDGECOMB(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, string P_OPERATOR, string P_TESTDATE, string P_TESTTIME, string P_YARN,
                decimal? P_EDGECOMB1, decimal? P_EDGECOMB2, decimal? P_EDGECOMB3, decimal? P_EDGECOMB4, decimal? P_EDGECOMB5, decimal? P_EDGECOMB6, decimal? P_AVE_EDGECOMB, string P_FILEPDF)
            {
                #region ListID_PTF

                ITMCODE = P_ITMCODE;
                WEAVINGLOG = P_WEAVINGLOG;
                FINISHINGLOT = P_FINISHINGLOT;
                OPERATOR = P_OPERATOR;
                TESTDATE = P_TESTDATE;
                TESTTIME = P_TESTTIME;
                YARN = P_YARN;

                EDGECOMB1 = P_EDGECOMB1;
                EDGECOMB2 = P_EDGECOMB2;
                EDGECOMB3 = P_EDGECOMB3;
                EDGECOMB4 = P_EDGECOMB4;
                EDGECOMB5 = P_EDGECOMB5;
                EDGECOMB6 = P_EDGECOMB6;
                AVE_EDGECOMB = P_AVE_EDGECOMB;

                FILEPDF = P_FILEPDF;

                #endregion
            }

            #endregion

            public string ITMCODE { get; set; }
            public string WEAVINGLOG { get; set; }
            public string FINISHINGLOT { get; set; }
            public string OPERATOR { get; set; }
            public string TESTDATE { get; set; }
            public string TESTTIME { get; set; }
            public string YARN { get; set; }

            public decimal? EDGECOMB1 { get; set; }
            public decimal? EDGECOMB2 { get; set; }
            public decimal? EDGECOMB3 { get; set; }
            public decimal? EDGECOMB4 { get; set; }
            public decimal? EDGECOMB5 { get; set; }
            public decimal? EDGECOMB6 { get; set; }
            public decimal? AVE_EDGECOMB { get; set; }

            public string FILEPDF { get; set; }
        }
        #endregion

        #region LoadEDGECOMB
        public DataControl.ClassData.PDFClassData.ListEDGECOMB LoadEDGECOMB(string fileName)
        {
            try
            {
                if (fileName.Contains("is_tens"))
                {
                    string text = string.Empty;

                    if (File.Exists(fileName))
                    {
                        PdfReader pdfReader = new PdfReader(fileName);

                        for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                        {
                            ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                            string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                            currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                            text = text + currentText;

                        }
                        pdfReader.Close();

                        if (text != string.Empty)
                        {
                            DataControl.ClassData.PDFClassData.ListEDGECOMB inst = new DataControl.ClassData.PDFClassData.ListEDGECOMB();

                            string test = string.Empty;

                            string[] lines = text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            bool chk_maxStart = false;

                            string method = "Edge comb.im_tens";
                            string method2 = "Edgecomb.im_tens";

                            if (lines != null)
                            {
                                if (lines[10].ToString().Contains(method) == true || lines[10].ToString().Contains(method2) == true)
                                {
                                    #region Select Data

                                    foreach (string file in lines)
                                    {
                                        try
                                        {

                                            test = file.Substring(file.LastIndexOf(@"\") + 1);

                                            if (test != string.Empty)
                                            {
                                                string id_tens = test;
                                                string specimenLabel = "Specimen label";
                                                string operatorID = "Opertor ID";
                                                string weavingLot = "Weaving Lot no.";
                                                string finishing = "Scouring Lot no.";

                                                string testdate = "Last test date";
                                                string testtime = "Last test time";

                                                string max = "Max Ld Max Ld Strain@Max Ld";
                                                string max2 = "x Ld";
                                                string mean = "Mean";

                                                bool chk_specimen = id_tens.Contains(specimenLabel);
                                                bool chk_operator = id_tens.Contains(operatorID);
                                                bool chk_weaving = id_tens.Contains(weavingLot);
                                                bool chk_finishing = id_tens.Contains(finishing);

                                                bool chk_testdate = id_tens.Contains(testdate);
                                                bool chk_testtime = id_tens.Contains(testtime);

                                                bool chk_max = false;

                                                if (id_tens.Contains(max) == true)
                                                    chk_max = true;
                                                else if(id_tens.Contains(max2) == true)
                                                    chk_max = true;
                                                else
                                                    chk_max = false;

                                                bool chk_mean = id_tens.Contains(mean);

                                                String strString = id_tens.Substring(0, id_tens.Length).Trim();
                                                strString = strString.Replace(" ", ",").TrimEnd();
                                                String[] myArr = strString.Split(',');

                                                if (myArr.Length > 1)
                                                {
                                                    if (chk_specimen)
                                                    {
                                                        inst.ITMCODE = myArr[2].ToString().Trim();
                                                    }

                                                    if (chk_operator)
                                                    {
                                                        inst.OPERATOR = myArr[2].ToString().Trim();
                                                    }

                                                    if (chk_weaving)
                                                    {
                                                        inst.WEAVINGLOG = myArr[3].ToString().Trim();

                                                        if (myArr.Length > 4)
                                                        {
                                                            id_tens = id_tens.Replace(weavingLot, "").TrimEnd();

                                                            if (id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd() == "W")
                                                            {
                                                                inst.YARN = "Warp";
                                                            }
                                                            else if (id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd() == "F")
                                                            {
                                                                inst.YARN = "Weft";
                                                            }
                                                            else
                                                            {
                                                                inst.YARN = id_tens.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd();
                                                            }
                                                        }
                                                    }

                                                    if (chk_finishing)
                                                    {
                                                        inst.FINISHINGLOT = myArr[3].ToString().Trim();
                                                    }

                                                    if (chk_testdate)
                                                    {
                                                        inst.TESTDATE = id_tens.Replace(testdate, "").TrimStart().TrimEnd();
                                                    }

                                                    if (chk_testtime)
                                                    {
                                                        inst.TESTTIME = id_tens.Replace(testtime, "").TrimStart().TrimEnd();
                                                    }

                                                    if (chk_max)
                                                    {
                                                        chk_maxStart = true;
                                                    }

                                                    if (chk_maxStart)
                                                    {
                                                        #region TENSILE  & ELONG;

                                                        if (myArr[0] == "1")
                                                        {
                                                            if (myArr[2] != null)
                                                                inst.EDGECOMB1 = decimal.Parse(myArr[2].ToString());
                                                        }

                                                        if (myArr[0] == "2")
                                                        {
                                                            if (myArr[2] != null)
                                                                inst.EDGECOMB2 = decimal.Parse(myArr[2].ToString());
                                                        }

                                                        if (myArr[0] == "3")
                                                        {
                                                            if (myArr[2] != null)
                                                                inst.EDGECOMB3 = decimal.Parse(myArr[2].ToString());
                                                        }

                                                        if (chk_mean)
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            if (myArr[0] == "4")
                                                            {
                                                                if (myArr[2] != null)
                                                                    inst.EDGECOMB4 = decimal.Parse(myArr[2].ToString());
                                                            }

                                                            if (myArr[0] == "5")
                                                            {
                                                                if (myArr[2] != null)
                                                                    inst.EDGECOMB5 = decimal.Parse(myArr[2].ToString());
                                                            }

                                                            if (myArr[0] == "6")
                                                            {
                                                                if (myArr[2] != null)
                                                                    inst.EDGECOMB6 = decimal.Parse(myArr[2].ToString());
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            // Could not load the image - probably related to Windows file system permissions.
                                            System.Windows.Forms.MessageBox.Show("Cannot display the : " + file.Substring(file.LastIndexOf('\\'))
                                                + ". You may not have permission to read the file, or " +
                                                "it may be corrupt.\n\nReported error: " + ex.Message);

                                            break;
                                        }
                                    }

                                    #endregion

                                    #region AVE_EDGECOMB

                                    int? i = 0;

                                    decimal? edgecomb1 = inst.EDGECOMB1;
                                    decimal? edgecomb2 = inst.EDGECOMB2;
                                    decimal? edgecomb3 = inst.EDGECOMB3;
                                    decimal? edgecomb4 = inst.EDGECOMB4;
                                    decimal? edgecomb5 = inst.EDGECOMB5;
                                    decimal? edgecomb6 = inst.EDGECOMB6;

                                    if (edgecomb1 != null && edgecomb1 != 0)
                                        i++;
                                    else
                                        edgecomb1 = 0;

                                    if (edgecomb2 != null && edgecomb2 != 0)
                                        i++;
                                    else
                                        edgecomb2 = 0;

                                    if (edgecomb3 != null && edgecomb3 != 0)
                                        i++;
                                    else
                                        edgecomb3 = 0;

                                    if (edgecomb4 != null && edgecomb4 != 0)
                                        i++;
                                    else
                                        edgecomb4 = 0;

                                    if (edgecomb5 != null && edgecomb5 != 0)
                                        i++;
                                    else
                                        edgecomb5 = 0;

                                    if (edgecomb6 != null && edgecomb6 != 0)
                                        i++;
                                    else
                                        edgecomb6 = 0;

                                    if (edgecomb1 != 0 || edgecomb2 != 0 || edgecomb3 != 0 || edgecomb4 != 0 || edgecomb5 != 0 || edgecomb6 != 0)
                                        inst.AVE_EDGECOMB = DataControl.ClassData.MathEx.Round(((edgecomb1 + edgecomb2 + edgecomb3 + edgecomb4 + edgecomb5 + edgecomb6) / i).Value, 2);
                                    else
                                        inst.AVE_EDGECOMB = 0;

                                    #endregion

                                    inst.FILEPDF = fileName;

                                    return inst;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #endregion

        #region TEAR

        #region ListTEAR

        public class ListTEAR
        {

            public ListTEAR()
            {
                // default constructor
            }

            #region ListTEAR


            public ListTEAR(string P_ITMCODE, string P_WEAVINGLOG, string P_FINISHINGLOT, string P_OPERATOR, string P_YARN,
                decimal? P_TEAR1, decimal? P_TEAR2, decimal? P_TEAR3, decimal? P_TEAR4, decimal? P_TEAR5, decimal? P_TEAR6, decimal? P_AVE_TEAR, string P_FILEPDF)
            {
                #region ListID_PTF

                ITMCODE = P_ITMCODE;
                WEAVINGLOG = P_WEAVINGLOG;
                FINISHINGLOT = P_FINISHINGLOT;
                OPERATOR = P_OPERATOR;
                YARN = P_YARN;

                TEAR1 = P_TEAR1;
                TEAR2 = P_TEAR2;
                TEAR3 = P_TEAR3;
                TEAR4 = P_TEAR4;
                TEAR5 = P_TEAR5;
                TEAR6 = P_TEAR6;
                AVE_TEAR = P_AVE_TEAR;

                FILEPDF = P_FILEPDF;

                #endregion
            }

            #endregion

            public string ITMCODE { get; set; }
            public string WEAVINGLOG { get; set; }
            public string FINISHINGLOT { get; set; }
            public string OPERATOR { get; set; }
            public string YARN { get; set; }

            public decimal? TEAR1 { get; set; }
            public decimal? TEAR2 { get; set; }
            public decimal? TEAR3 { get; set; }
            public decimal? TEAR4 { get; set; }
            public decimal? TEAR5 { get; set; }
            public decimal? TEAR6 { get; set; }
            public decimal? AVE_TEAR { get; set; }

            public string FILEPDF { get; set; }
        }
        #endregion

        #region LoadTEAR
        public DataControl.ClassData.PDFClassData.ListTEAR LoadTEAR(string fileName)
        {
            try
            {
                if (fileName.Contains("is_ptf"))
                {
                    string text = string.Empty;

                    if (File.Exists(fileName))
                    {
                        PdfReader pdfReader = new PdfReader(fileName);

                        for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                        {
                            ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                            string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                            currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                            text = text + currentText;

                        }
                        pdfReader.Close();

                        if (text != string.Empty)
                        {
                            DataControl.ClassData.PDFClassData.ListTEAR inst = new DataControl.ClassData.PDFClassData.ListTEAR();

                            string test = string.Empty;

                            string[] lines = text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            bool chk_aveStart = false;

                            if (lines != null)
                            {
                                #region Select Data

                                foreach (string file in lines)
                                {
                                    try
                                    {

                                        test = file.Substring(file.LastIndexOf(@"\") + 1);

                                        if (test != string.Empty)
                                        {
                                            string id_ptf = test;
                                            string specimenLabel = "Specimen label";
                                            string operatorID = "Operator ID";
                                            string weavingLot = "Weaving Lot no.";
                                            string finishingLot = "Finishing Lot no.";

                                            string ave = "Ave Load@All Peaks";
                                            string mean = "Mean";

                                            bool chk_specimen = id_ptf.Contains(specimenLabel);
                                            bool chk_operator = id_ptf.Contains(operatorID);
                                            bool chk_weaving = id_ptf.Contains(weavingLot);
                                            bool chk_finishing = id_ptf.Contains(finishingLot);

                                            bool chk_ave = id_ptf.Contains(ave);
                                            bool chk_mean = id_ptf.Contains(mean);


                                            String strString = id_ptf.Substring(0, id_ptf.Length).Trim();
                                            strString = strString.Replace(" ", ",").TrimEnd();
                                            String[] myArr = strString.Split(',');

                                            if (myArr.Length > 1)
                                            {
                                                if (chk_specimen)
                                                {
                                                    inst.ITMCODE = myArr[2].ToString().Trim();
                                                }

                                                if (chk_operator)
                                                {
                                                    inst.OPERATOR = myArr[2].ToString().Trim();
                                                }

                                                if (chk_weaving)
                                                {
                                                    inst.WEAVINGLOG = myArr[3].ToString().Trim();

                                                    if (myArr.Length > 4)
                                                    {
                                                        id_ptf = id_ptf.Replace(weavingLot, "").TrimEnd();

                                                        if (id_ptf.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd() == "W")
                                                        {
                                                            inst.YARN = "Warp";
                                                        }
                                                        else if (id_ptf.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd() == "F")
                                                        {
                                                            inst.YARN = "Weft";
                                                        }
                                                        else
                                                        {
                                                            inst.YARN = id_ptf.Replace(myArr[3].ToString().Trim(), "").TrimStart().TrimEnd();
                                                        }
                                                    }
                                                }

                                                if (chk_finishing)
                                                {
                                                    inst.FINISHINGLOT = myArr[3].ToString().Trim();
                                                }

                                                if (chk_ave)
                                                {
                                                    chk_aveStart = true;
                                                }

                                                if (chk_aveStart)
                                                {
                                                    if (myArr[0] == "1")
                                                    {
                                                        if (myArr[1] != null)
                                                            inst.TEAR1 = decimal.Parse(myArr[1].ToString());
                                                    }

                                                    if (myArr[0] == "2")
                                                    {
                                                        if (myArr[1] != null)
                                                            inst.TEAR2 = decimal.Parse(myArr[1].ToString());
                                                    }

                                                    if (myArr[0] == "3")
                                                    {
                                                        if (myArr[1] != null)
                                                            inst.TEAR3 = decimal.Parse(myArr[1].ToString());
                                                    }

                                                    if (chk_mean)
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        if (myArr[0] == "4")
                                                        {
                                                            if (myArr[1] != null)
                                                                inst.TEAR4 = decimal.Parse(myArr[1].ToString());
                                                        }

                                                        if (myArr[0] == "5")
                                                        {
                                                            if (myArr[1] != null)
                                                                inst.TEAR5 = decimal.Parse(myArr[1].ToString());
                                                        }

                                                        if (myArr[0] == "6")
                                                        {
                                                            if (myArr[1] != null)
                                                                inst.TEAR6 = decimal.Parse(myArr[1].ToString());
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        // Could not load the image - probably related to Windows file system permissions.
                                        System.Windows.Forms.MessageBox.Show("Cannot display the : " + file.Substring(file.LastIndexOf('\\'))
                                            + ". You may not have permission to read the file, or " +
                                            "it may be corrupt.\n\nReported error: " + ex.Message);

                                        break;
                                    }
                                }

                                #endregion

                                #region AVE_TEAR

                                int? i = 0;

                                decimal? ave1 = inst.TEAR1;
                                decimal? ave2 = inst.TEAR2;
                                decimal? ave3 = inst.TEAR3;
                                decimal? ave4 = inst.TEAR4;
                                decimal? ave5 = inst.TEAR5;
                                decimal? ave6 = inst.TEAR6;

                                #region Old
                                //if (ave1 != null && ave1 != 0)
                                //    i++;
                                //else
                                //    ave1 = 0;

                                //if (ave2 != null && ave2 != 0)
                                //    i++;
                                //else
                                //    ave2 = 0;

                                //if (ave3 != null && ave3 != 0)
                                //    i++;
                                //else
                                //    ave3 = 0;

                                //if (ave4 != null && ave4 != 0)
                                //    i++;
                                //else
                                //    ave4 = 0;

                                //if (ave5 != null && ave5 != 0)
                                //    i++;
                                //else
                                //    ave5 = 0;

                                //if (ave6 != null && ave6 != 0)
                                //    i++;
                                //else
                                //    ave6 = 0;
                                #endregion

                                #region New

                                if (ave1 == null)
                                    ave1 = 0;
                                else
                                    i++;

                                if (ave2 == null)
                                    ave2 = 0;
                                else
                                    i++;

                                if (ave3 == null)
                                    ave3 = 0;
                                else
                                    i++;

                                if (ave4 == null)
                                    ave4 = 0;
                                else
                                    i++;

                                if (ave5 == null)
                                    ave5 = 0;
                                else
                                    i++;

                                if (ave6 == null)
                                    ave6 = 0;
                                else
                                    i++;

                                #endregion

                                if (ave1 != 0 || ave2 != 0 || ave3 != 0 || ave4 != 0 || ave5 != 0 || ave6 != 0)
                                    inst.AVE_TEAR = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 2);
                                else
                                    inst.AVE_TEAR = 0;

                                #endregion

                                inst.FILEPDF = fileName;

                                return inst;

                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #endregion

        #region ReadPdfFile
        public string ReadPdfFile(string fileName)
        {
            string text = string.Empty;

            if (File.Exists(fileName))
            {
                PdfReader pdfReader = new PdfReader(fileName);

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text = text + currentText;

                }
                pdfReader.Close();
            }
            return text.ToString();
        }
        #endregion

    }
}
