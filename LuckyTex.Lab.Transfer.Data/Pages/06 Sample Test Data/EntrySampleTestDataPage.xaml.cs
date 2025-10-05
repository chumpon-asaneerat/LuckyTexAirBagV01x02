#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NLib;
using LuckyTex.Services;
using LuckyTex.Models;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

using System.Reflection;
using iTextSharp.text;

using System.Globalization;
using System.Collections;

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;
using System.Threading;

using System.Configuration;
using System.Data;
using System.Data.OleDb;
//using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

using NPOI.HSSF.UserModel;
using NPOI.HSSF.Model;
using NPOI.SS.UserModel;

using NPOI.XSSF.UserModel;
using NPOI.XSSF.Model; 
#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for EntrySampleTestDataPage.xaml
    /// </summary>
    public partial class EntrySampleTestDataPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntrySampleTestDataPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;

        string itemCode = string.Empty;
        string weavingLot = string.Empty;
        string finishingLot = string.Empty;
        string method = string.Empty;
        DateTime? entryDate = null;
        int? no = 0;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #region cmdClear_Click
        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }
        #endregion

        #region cmdWidth_Click
        private void cmdWidth_Click(object sender, RoutedEventArgs e)
        {
            method = "Width";

            if(!string.IsNullOrEmpty(txtWidth.Text))
            {
                if (txtWidth.Text != "0")
                {
                    no = int.Parse(txtWidth.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdUsableWidth_Click
        private void cmdUsableWidth_Click(object sender, RoutedEventArgs e)
        {
            method = "Usable Width";

            if (!string.IsNullOrEmpty(txtUsableWidth.Text))
            {
                if (txtUsableWidth.Text != "0")
                {
                    no = int.Parse(txtUsableWidth.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdWidthofSilicone_Click
        private void cmdWidthofSilicone_Click(object sender, RoutedEventArgs e)
        {
            method = "Width of Silicone";

            if (!string.IsNullOrEmpty(txtWidthofSilicone.Text))
            {
                if (txtWidthofSilicone.Text != "0")
                {
                    no = int.Parse(txtWidthofSilicone.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdNumberofthreads_Click
        private void cmdNumberofthreads_Click(object sender, RoutedEventArgs e)
        {
            method = "Number of threads";

            if (!string.IsNullOrEmpty(txtNumberofthreads.Text))
            {
                if (txtNumberofthreads.Text != "0")
                {
                    no = int.Parse(txtNumberofthreads.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdTotalWeight_Click
        private void cmdTotalWeight_Click(object sender, RoutedEventArgs e)
        {
            method = "Total Weight";

            if (!string.IsNullOrEmpty(txtTotalWeight.Text))
            {
                if (txtTotalWeight.Text != "0")
                {
                    no = int.Parse(txtTotalWeight.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdUncoatedweight_Click
        private void cmdUncoatedweight_Click(object sender, RoutedEventArgs e)
        {
            method = "Uncoated weight";

            if (!string.IsNullOrEmpty(txtUncoatedweight.Text))
            {
                if (txtUncoatedweight.Text != "0")
                {
                    no = int.Parse(txtUncoatedweight.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdTotalcoatingweight_Click
        private void cmdTotalcoatingweight_Click(object sender, RoutedEventArgs e)
        {
            method = "Total coating weight";

            if (!string.IsNullOrEmpty(txtTotalcoatingweight.Text))
            {
                if (txtTotalcoatingweight.Text != "0")
                {
                    no = int.Parse(txtTotalcoatingweight.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdThickness_Click
        private void cmdThickness_Click(object sender, RoutedEventArgs e)
        {
            method = "Thickness";

            if (!string.IsNullOrEmpty(txtThickness.Text))
            {
                if (txtThickness.Text != "0")
                {
                    no = int.Parse(txtThickness.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdMaximumForce_Click
        private void cmdMaximumForce_Click(object sender, RoutedEventArgs e)
        {
            method = "Maximum Force";

            if (!string.IsNullOrEmpty(txtMaximumForce.Text))
            {
                if (txtMaximumForce.Text != "0")
                {
                    no = int.Parse(txtMaximumForce.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdElongationatMaximumForce_Click
        private void cmdElongationatMaximumForce_Click(object sender, RoutedEventArgs e)
        {
            method = "Elongation at Maximum Force";

            if (!string.IsNullOrEmpty(txtElongationatMaximumForce.Text))
            {
                if (txtElongationatMaximumForce.Text != "0")
                {
                    no = int.Parse(txtElongationatMaximumForce.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdFlammability_Click
        private void cmdFlammability_Click(object sender, RoutedEventArgs e)
        {
            method = "Flammability";

            if (!string.IsNullOrEmpty(txtFlammability.Text))
            {
                if (txtFlammability.Text != "0")
                {
                    no = int.Parse(txtFlammability.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdEdgecombResistance_Click
        private void cmdEdgecombResistance_Click(object sender, RoutedEventArgs e)
        {
            method = "Edgecomb Resistance";

            if (!string.IsNullOrEmpty(txtEdgecombResistance.Text))
            {
                if (txtEdgecombResistance.Text != "0")
                {
                    no = int.Parse(txtEdgecombResistance.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdStiffness_Click
        private void cmdStiffness_Click(object sender, RoutedEventArgs e)
        {
            method = "Stiffness";

            if (!string.IsNullOrEmpty(txtStiffness.Text))
            {
                if (txtStiffness.Text != "0")
                {
                    no = int.Parse(txtStiffness.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdTearForce_Click
        private void cmdTearForce_Click(object sender, RoutedEventArgs e)
        {
            method = "Tear Force";

            if (!string.IsNullOrEmpty(txtTearForce.Text))
            {
                if (txtTearForce.Text != "0")
                {
                    no = int.Parse(txtTearForce.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdStaticAirPermeability_Click
        private void cmdStaticAirPermeability_Click(object sender, RoutedEventArgs e)
        {
            method = "Static Air Permeability";

            if (!string.IsNullOrEmpty(txtStaticAirPermeability.Text))
            {
                if (txtStaticAirPermeability.Text != "0")
                {
                    no = int.Parse(txtStaticAirPermeability.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdDynamicAirPermeability_Click
        private void cmdDynamicAirPermeability_Click(object sender, RoutedEventArgs e)
        {
            method = "Dynamic Air Permeability";

            if (!string.IsNullOrEmpty(txtDynamicAirPermeability.Text))
            {
                if (txtDynamicAirPermeability.Text != "0")
                {
                    no = int.Parse(txtDynamicAirPermeability.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdExponent_Click
        private void cmdExponent_Click(object sender, RoutedEventArgs e)
        {
            method = "Exponent";

            if (!string.IsNullOrEmpty(txtExponent.Text))
            {
                if (txtExponent.Text != "0")
                {
                    no = int.Parse(txtExponent.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdDimensionalChange_Click
        private void cmdDimensionalChange_Click(object sender, RoutedEventArgs e)
        {
            method = "Dimensional Change";

            if (!string.IsNullOrEmpty(txtDimensionalChange.Text))
            {
                if (txtDimensionalChange.Text != "0")
                {
                    no = int.Parse(txtDimensionalChange.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdFlexAbrasion_Click
        private void cmdFlexAbrasion_Click(object sender, RoutedEventArgs e)
        {
            method = "Flex Abrasion";

            if (!string.IsNullOrEmpty(txtFlexAbrasion.Text))
            {
                if (txtFlexAbrasion.Text != "0")
                {
                    no = int.Parse(txtFlexAbrasion.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdBow_Click
        private void cmdBow_Click(object sender, RoutedEventArgs e)
        {
            method = "Bow";

            if (!string.IsNullOrEmpty(txtBow.Text))
            {
                if (txtBow.Text != "0")
                {
                    no = int.Parse(txtBow.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdSkew_Click
        private void cmdSkew_Click(object sender, RoutedEventArgs e)
        {
            method = "Skew";

            if (!string.IsNullOrEmpty(txtSkew.Text))
            {
                if (txtSkew.Text != "0")
                {
                    no = int.Parse(txtSkew.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData1Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdBENDING_Click
        private void cmdBENDING_Click(object sender, RoutedEventArgs e)
        {
            method = "Bending resistance";

            if (!string.IsNullOrEmpty(txtBENDING.Text))
            {
                if (txtBENDING.Text != "0")
                {
                    no = int.Parse(txtBENDING.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region cmdFLEX_SCOTT_Click
        private void cmdFLEX_SCOTT_Click(object sender, RoutedEventArgs e)
        {
            method = "Flex Abrasion (Scott type)";

            if (!string.IsNullOrEmpty(txtFLEX_SCOTT.Text))
            {
                if (txtFLEX_SCOTT.Text != "0")
                {
                    no = int.Parse(txtFLEX_SCOTT.Text);

                    if (no <= 100)
                    {
                        if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(method))
                        {
                            EntrySampleTestDataInfo labData = LabDataPDFDataService.Instance.ShowEntrySampleTestData2Window(opera, itemCode, weavingLot, finishingLot, entryDate, method, no);
                            if (null != labData)
                            {
                                if (labData.ChkSave == true)
                                {
                                    method = string.Empty;

                                    SearchData();
                                }
                            }
                        }
                    }
                    else
                    {
                        "Can't No > 100".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #endregion

        #region TextBox Handlers

        #region NumberValidationTextBox

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            string onlyNumeric = @"^([0-9]+(.[0-9]+)?)$";
            Regex regex = new Regex(onlyNumeric);
            e.Handled = !regex.IsMatch(e.Text);
        }

        #endregion

        #region General

        #region KeyDown

        #region txtITMCODE_KeyDown
        private void txtITMCODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWEAVINGLOG.Focus();
                txtWEAVINGLOG.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWEAVINGLOG_KeyDown
        private void txtWEAVINGLOG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFINISHINGLOT.Focus();
                txtFINISHINGLOT.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtFINISHINGLOT_KeyDown
        private void txtFINISHINGLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtWidth.IsEnabled == true)
                {
                    txtWidth.Focus();
                    txtWidth.SelectAll();
                }

                e.Handled = true;
            }
        }
        #endregion

        #region txtWidth_KeyDown
        private void txtWidth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtUsableWidth.Focus();
                txtUsableWidth.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtUsableWidth_KeyDown
        private void txtUsableWidth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWidthofSilicone.Focus();
                txtWidthofSilicone.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWidthofSilicone_KeyDown
        private void txtWidthofSilicone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNumberofthreads.Focus();
                txtNumberofthreads.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtNumberofthreads_KeyDown
        private void txtNumberofthreads_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTotalWeight.Focus();
                txtTotalWeight.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTotalWeight_KeyDown
        private void txtTotalWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtUncoatedweight.Focus();
                txtUncoatedweight.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtUncoatedweight_KeyDown
        private void txtUncoatedweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTotalcoatingweight.Focus();
                txtTotalcoatingweight.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTotalcoatingweight_KeyDown
        private void txtTotalcoatingweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtThickness.Focus();
                txtThickness.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtThickness_KeyDown
        private void txtThickness_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMaximumForce.Focus();
                txtMaximumForce.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtMaximumForce_KeyDown
        private void txtMaximumForce_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtElongationatMaximumForce.Focus();
                txtElongationatMaximumForce.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtElongationatMaximumForce_KeyDown
        private void txtElongationatMaximumForce_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFlammability.Focus();
                txtFlammability.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtFlammability_KeyDown
        private void txtFlammability_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEdgecombResistance.Focus();
                txtEdgecombResistance.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtEdgecombResistance_KeyDown
        private void txtEdgecombResistance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStiffness.Focus();
                txtStiffness.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStiffness_KeyDown
        private void txtStiffness_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTearForce.Focus();
                txtTearForce.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTearForce_KeyDown
        private void txtTearForce_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStaticAirPermeability.Focus();
                txtStaticAirPermeability.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStaticAirPermeability_KeyDown
        private void txtStaticAirPermeability_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDynamicAirPermeability.Focus();
                txtDynamicAirPermeability.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtDynamicAirPermeability_KeyDown
        private void txtDynamicAirPermeability_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtExponent.Focus();
                txtExponent.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtExponent_KeyDown
        private void txtExponent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDimensionalChange.Focus();
                txtDimensionalChange.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtDimensionalChange_KeyDown
        private void txtDimensionalChange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFlexAbrasion.Focus();
                txtFlexAbrasion.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtFlexAbrasion_KeyDown
        private void txtFlexAbrasion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBow.Focus();
                txtBow.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtBow_KeyDown
        private void txtBow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSkew.Focus();
                txtSkew.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtSkew_KeyDown
        private void txtSkew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBENDING.Focus();
                txtBENDING.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtBENDING_KeyDown
        private void txtBENDING_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLEX_SCOTT.Focus();
                txtFLEX_SCOTT.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtFLEX_SCOTT_KeyDown
        private void txtFLEX_SCOTT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdBack.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #endregion

        #region LostFocus

        #region txtITMCODE_LostFocus
        private void txtITMCODE_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtITMCODE.Text))
                itemCode = txtITMCODE.Text;
        }
        #endregion

        #region txtWEAVINGLOG_LostFocus
        private void txtWEAVINGLOG_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWEAVINGLOG.Text))
                weavingLot = txtWEAVINGLOG.Text;
        }
        #endregion

        #region txtFINISHINGLOT_LostFocus
        private void txtFINISHINGLOT_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFINISHINGLOT.Text))
                finishingLot = txtFINISHINGLOT.Text;

            if (!string.IsNullOrEmpty(txtITMCODE.Text) && !string.IsNullOrEmpty(txtWEAVINGLOG.Text) && !string.IsNullOrEmpty(txtFINISHINGLOT.Text))
            {
                SearchData();
            }
        }
        #endregion

        #region txtWidth_LostFocus
        private void txtWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWidth.Text))
            {
                if (txtWidth.Text != "0")
                {
                    cmdWidth.IsEnabled = true;
                }
                else
                {
                    cmdWidth.IsEnabled = false;
                }
            }
            else
            {
                cmdWidth.IsEnabled = false;
            }
        }
        #endregion

        #region txtUsableWidth_LostFocus
        private void txtUsableWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsableWidth.Text))
            {
                if (txtUsableWidth.Text != "0")
                {

                    cmdUsableWidth.IsEnabled = true;
                }
                else
                {
                    cmdUsableWidth.IsEnabled = false;
                }
            }
            else
            {
                cmdUsableWidth.IsEnabled = false;
            }
        }
        #endregion

        #region txtWidthofSilicone_LostFocus
        private void txtWidthofSilicone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWidthofSilicone.Text))
            {
                if (txtWidthofSilicone.Text != "0")
                {
                    cmdWidthofSilicone.IsEnabled = true;
                }
                else
                {
                    cmdWidthofSilicone.IsEnabled = false;
                }
            }
            else
            {
                cmdWidthofSilicone.IsEnabled = false;
            }
        }
        #endregion

        #region txtNumberofthreads_LostFocus
        private void txtNumberofthreads_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumberofthreads.Text))
            {
                if (txtNumberofthreads.Text != "0")
                {
                    cmdNumberofthreads.IsEnabled = true;
                }
                else
                {
                    cmdNumberofthreads.IsEnabled = false;
                }
            }
            else
            {
                cmdNumberofthreads.IsEnabled = false;
            }
        }
        #endregion

        #region txtTotalWeight_LostFocus
        private void txtTotalWeight_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotalWeight.Text))
            {
                if (txtTotalWeight.Text != "0")
                {
                    cmdTotalWeight.IsEnabled = true;
                }
                else
                {
                    cmdTotalWeight.IsEnabled = false;
                }
            }
            else
            {
                cmdTotalWeight.IsEnabled = false;
            }
        }
        #endregion

        #region txtUncoatedweight_LostFocus
        private void txtUncoatedweight_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUncoatedweight.Text))
            {
                if (txtUncoatedweight.Text != "0")
                {
                    cmdUncoatedweight.IsEnabled = true;
                }
                else
                {
                    cmdUncoatedweight.IsEnabled = false;
                }
            }
            else
            {
                cmdUncoatedweight.IsEnabled = false;
            }
        }
        #endregion

        #region txtTotalcoatingweight_LostFocus
        private void txtTotalcoatingweight_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotalcoatingweight.Text))
            {
                if (txtTotalcoatingweight.Text != "0")
                {
                    cmdTotalcoatingweight.IsEnabled = true;
                }
                else
                {
                    cmdTotalcoatingweight.IsEnabled = false;
                }
            }
            else
            {
                cmdTotalcoatingweight.IsEnabled = false;
            }
        }
        #endregion

        #region txtThickness_LostFocus
        private void txtThickness_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtThickness.Text))
            {
                if (txtThickness.Text != "0")
                {
                    cmdThickness.IsEnabled = true;
                }
                else
                {
                    cmdThickness.IsEnabled = false;
                }
            }
            else
            {
                cmdThickness.IsEnabled = false;
            }
        }
        #endregion

        #region txtMaximumForce_LostFocus
        private void txtMaximumForce_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaximumForce.Text))
            {
                if (txtMaximumForce.Text != "0")
                {
                    cmdMaximumForce.IsEnabled = true;
                }
                else
                {
                    cmdMaximumForce.IsEnabled = false;
                }
            }
            else
            {
                cmdMaximumForce.IsEnabled = false;
            }
        }
        #endregion

        #region txtElongationatMaximumForce_LostFocus
        private void txtElongationatMaximumForce_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtElongationatMaximumForce.Text))
            {
                if (txtElongationatMaximumForce.Text != "0")
                {
                    cmdElongationatMaximumForce.IsEnabled = true;
                }
                else
                {
                    cmdElongationatMaximumForce.IsEnabled = false;
                }
            }
            else
            {
                cmdElongationatMaximumForce.IsEnabled = false;
            }
        }
        #endregion

        #region txtFlammability_LostFocus
        private void txtFlammability_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFlammability.Text))
            {
                if (txtFlammability.Text != "0")
                {
                    cmdFlammability.IsEnabled = true;
                }
                else
                {
                    cmdFlammability.IsEnabled = false;
                }
            }
            else
            {
                cmdFlammability.IsEnabled = false;
            }
        }
        #endregion

        #region txtEdgecombResistance_LostFocus
        private void txtEdgecombResistance_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEdgecombResistance.Text))
            {
                if (txtEdgecombResistance.Text != "0")
                {
                    cmdEdgecombResistance.IsEnabled = true;
                }
                else
                {
                    cmdEdgecombResistance.IsEnabled = false;
                }
            }
            else
            {
                cmdEdgecombResistance.IsEnabled = false;
            }
        }
        #endregion

        #region txtStiffness_LostFocus
        private void txtStiffness_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStiffness.Text))
            {
                if (txtStiffness.Text != "0")
                {
                    cmdStiffness.IsEnabled = true;
                }
                else
                {
                    cmdStiffness.IsEnabled = false;
                }
            }
            else
            {
                cmdStiffness.IsEnabled = false;
            }
        }
        #endregion

        #region txtTearForce_LostFocus
        private void txtTearForce_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTearForce.Text))
            {
                if (txtTearForce.Text != "0")
                {
                    cmdTearForce.IsEnabled = true;
                }
                else
                {
                    cmdTearForce.IsEnabled = false;
                }
            }
            else
            {
                cmdTearForce.IsEnabled = false;
            }
        }
        #endregion

        #region txtStaticAirPermeability_LostFocus
        private void txtStaticAirPermeability_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStaticAirPermeability.Text))
            {
                if (txtStaticAirPermeability.Text != "0")
                {
                    cmdStaticAirPermeability.IsEnabled = true;
                }
                else
                {
                    cmdStaticAirPermeability.IsEnabled = false;
                }
            }
            else
            {
                cmdStaticAirPermeability.IsEnabled = false;
            }
        }
        #endregion

        #region txtDynamicAirPermeability_LostFocus
        private void txtDynamicAirPermeability_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDynamicAirPermeability.Text))
            {
                if (txtDynamicAirPermeability.Text != "0")
                {
                    cmdDynamicAirPermeability.IsEnabled = true;
                }
                else
                {
                    cmdDynamicAirPermeability.IsEnabled = false;
                }
            }
            else
            {
                cmdDynamicAirPermeability.IsEnabled = false;
            }
        }
        #endregion

        #region txtExponent_LostFocus
        private void txtExponent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExponent.Text))
            {
                if (txtExponent.Text != "0")
                {
                    cmdExponent.IsEnabled = true;
                }
                else
                {
                    cmdExponent.IsEnabled = false;
                }
            }
            else
            {
                cmdExponent.IsEnabled = false;
            }
        }
        #endregion

        #region txtDimensionalChange_LostFocus
        private void txtDimensionalChange_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDimensionalChange.Text))
            {
                if (txtDimensionalChange.Text != "0")
                {
                    cmdDimensionalChange.IsEnabled = true;
                }
                else
                {
                    cmdDimensionalChange.IsEnabled = false;
                }
            }
            else
            {
                cmdDimensionalChange.IsEnabled = false;
            }
        }
        #endregion

        #region txtFlexAbrasion_LostFocus
        private void txtFlexAbrasion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFlexAbrasion.Text))
            {
                if (txtFlexAbrasion.Text != "0")
                {
                    cmdFlexAbrasion.IsEnabled = true;
                }
                else
                {
                    cmdFlexAbrasion.IsEnabled = false;
                }
            }
            else
            {
                cmdFlexAbrasion.IsEnabled = false;
            }
        }
        #endregion

        #region txtBow_LostFocus
        private void txtBow_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBow.Text))
            {
                if (txtBow.Text != "0")
                {
                    cmdBow.IsEnabled = true;
                }
                else
                {
                    cmdBow.IsEnabled = false;
                }
            }
            else
            {
                cmdBow.IsEnabled = false;
            }
        }
        #endregion

        #region txtSkew_LostFocus
        private void txtSkew_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSkew.Text))
            {
                if (txtSkew.Text != "0")
                {
                    cmdSkew.IsEnabled = true;
                }
                else
                {
                    cmdSkew.IsEnabled = false;
                }
            }
            else
            {
                cmdSkew.IsEnabled = false;
            }
        }
        #endregion

        #region txtBENDING_LostFocus
        private void txtBENDING_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBENDING.Text))
            {
                if (txtBENDING.Text != "0")
                {
                    cmdBENDING.IsEnabled = true;
                }
                else
                {
                    cmdBENDING.IsEnabled = false;
                }
            }
            else
            {
                cmdBENDING.IsEnabled = false;
            }
        }
        #endregion

        #region txtFLEX_SCOTT_LostFocus
        private void txtFLEX_SCOTT_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFLEX_SCOTT.Text))
            {
                if (txtFLEX_SCOTT.Text != "0")
                {
                    cmdFLEX_SCOTT.IsEnabled = true;
                }
                else
                {
                    cmdFLEX_SCOTT.IsEnabled = false;
                }
            }
            else
            {
                cmdFLEX_SCOTT.IsEnabled = false;
            }
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region dteEntryDate_SelectedDateChanged
        private void dteEntryDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dteEntryDate.SelectedDate != null)
            {
                entryDate = dteEntryDate.SelectedDate;
            }
        }
        #endregion

        #region private Methods

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            itemCode = string.Empty;
            weavingLot = string.Empty;
            finishingLot = string.Empty;
            method = string.Empty;

            txtITMCODE.Text = string.Empty;
            txtWEAVINGLOG.Text = string.Empty;
            txtFINISHINGLOT.Text = string.Empty;
            dteEntryDate.SelectedDate = DateTime.Now;
            dteEntryDate.IsEnabled = true;

            txtWidth.Text = "0";
            txtUsableWidth.Text = "0";
            txtWidthofSilicone.Text = "0";
            txtNumberofthreads.Text = "0";
            txtTotalWeight.Text = "0";

            txtUncoatedweight.Text = "0";
            txtTotalcoatingweight.Text = "0";
            txtThickness.Text = "0";
            txtMaximumForce.Text = "0";
            txtElongationatMaximumForce.Text = "0";

            txtFlammability.Text = "0";
            txtEdgecombResistance.Text = "0";
            txtStiffness.Text = "0";
            txtTearForce.Text = "0";
            txtStaticAirPermeability.Text = "0";

            txtDynamicAirPermeability.Text = "0";
            txtExponent.Text = "0";
            txtDimensionalChange.Text = "0";
            txtFlexAbrasion.Text = "0";
            txtBow.Text = "0";
            txtSkew.Text = "0";

            txtBENDING.Text = "0";
            txtFLEX_SCOTT.Text = "0";

            buttonEnabled(false);
        }

        #endregion

        #region Clear
        private void Clear()
        {
            dteEntryDate.SelectedDate = DateTime.Now;
            dteEntryDate.IsEnabled = true;

            if (dteEntryDate.SelectedDate != null)
                entryDate = dteEntryDate.SelectedDate;

            txtWidth.Text = "0";
            txtUsableWidth.Text = "0";
            txtWidthofSilicone.Text = "0";
            txtNumberofthreads.Text = "0";
            txtTotalWeight.Text = "0";

            txtUncoatedweight.Text = "0";
            txtTotalcoatingweight.Text = "0";
            txtThickness.Text = "0";
            txtMaximumForce.Text = "0";
            txtElongationatMaximumForce.Text = "0";

            txtFlammability.Text = "0";
            txtEdgecombResistance.Text = "0";
            txtStiffness.Text = "0";
            txtTearForce.Text = "0";
            txtStaticAirPermeability.Text = "0";

            txtDynamicAirPermeability.Text = "0";
            txtExponent.Text = "0";
            txtDimensionalChange.Text = "0";
            txtFlexAbrasion.Text = "0";
            txtBow.Text = "0";
            txtSkew.Text = "0";
            txtBENDING.Text = "0";
            txtFLEX_SCOTT.Text = "0";

            buttonEnabled(false);
        }
        #endregion

        #region SearchData
        private void SearchData()
        {
            if (!string.IsNullOrEmpty(txtITMCODE.Text))
                itemCode = txtITMCODE.Text;

            if (!string.IsNullOrEmpty(txtWEAVINGLOG.Text))
                weavingLot = txtWEAVINGLOG.Text;

            if (!string.IsNullOrEmpty(txtFINISHINGLOT.Text))
                finishingLot = txtFINISHINGLOT.Text;

            if (dteEntryDate.SelectedDate != null)
                entryDate = dteEntryDate.SelectedDate;

            LAB_GETNOSAMPLEBYMETHOD(itemCode, weavingLot, finishingLot);

        }
        #endregion

        #region LAB_GETNOSAMPLEBYMETHOD
        private bool LAB_GETNOSAMPLEBYMETHOD(string P_ITMCODE, string P_PRODUCTIONLOT, string P_FINISHINGLOT)
        {
            bool chkLoad = true;

            try
            {
                List<LAB_GETNOSAMPLEBYMETHOD> results = LabDataPDFDataService.Instance.LAB_GETNOSAMPLEBYMETHOD(P_ITMCODE, P_PRODUCTIONLOT, P_FINISHINGLOT);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(results[0].ITM_CODE))
                            itemCode = results[0].ITM_CODE;

                        if (!string.IsNullOrEmpty(results[0].PRODUCTIONLOT))
                            weavingLot = results[0].PRODUCTIONLOT;

                        if (!string.IsNullOrEmpty(results[0].FINISHINGLOT))
                            finishingLot = results[0].FINISHINGLOT;

                        if (results[0].ENTRYDATE != null)
                        {
                            dteEntryDate.SelectedDate = results[0].ENTRYDATE;
                            entryDate = results[0].ENTRYDATE;
                            dteEntryDate.IsEnabled = false;
                        }

                        string m = string.Empty;

                        for (int page = 0; page <= results.Count - 1; page++)
                        {
                            m = string.Empty;

                            if (!string.IsNullOrEmpty(results[page].METHOD))
                            {
                               m = results[page].METHOD;
                            }

                            #region N

                            if (!string.IsNullOrEmpty(m))
                            {
                                if (m == "Width")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtWidth.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdWidth.IsEnabled = true;
                                    }
                                }
                                else if (m == "Usable Width")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtUsableWidth.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdUsableWidth.IsEnabled = true;
                                    }
                                }
                                else if (m == "Width of Silicone")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtWidthofSilicone.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdWidthofSilicone.IsEnabled = true;
                                    }
                                }
                                else if (m == "Number of threads")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtNumberofthreads.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdNumberofthreads.IsEnabled = true;
                                        }
                                    }
                                }
                                else if (m == "Total Weight")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtTotalWeight.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdTotalWeight.IsEnabled = true;
                                    }
                                }
                                else if (m == "Uncoated weight")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtUncoatedweight.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdUncoatedweight.IsEnabled = true;
                                    }
                                }
                                else if (m == "Total coating weight")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtTotalcoatingweight.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdTotalcoatingweight.IsEnabled = true;
                                    }
                                }
                                else if (m == "Thickness")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtThickness.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdThickness.IsEnabled = true;
                                    }
                                }
                                else if (m == "Maximum Force")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtMaximumForce.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdMaximumForce.IsEnabled = true;
                                        }
                                    }
                                }
                                else if (m == "Elongation at Maximum Force")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtElongationatMaximumForce.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdElongationatMaximumForce.IsEnabled = true;
                                        }
                                    }
                                }
                                else if (m == "Flammability")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtFlammability.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdFlammability.IsEnabled = true;
                                        }
                                    }
                                }
                                else if (m == "Edgecomb Resistance")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtEdgecombResistance.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdEdgecombResistance.IsEnabled = true;
                                        }
                                    }
                                }
                                else if (m == "Stiffness")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtStiffness.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdStiffness.IsEnabled = true;
                                        }
                                    }
                                }
                                else if (m == "Tear Force")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtTearForce.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdTearForce.IsEnabled = true;
                                        }
                                    }
                                }
                                else if (m == "Static Air Permeability")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtStaticAirPermeability.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdStaticAirPermeability.IsEnabled = true;
                                    }
                                }
                                else if (m == "Dynamic Air Permeability")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtDynamicAirPermeability.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdDynamicAirPermeability.IsEnabled = true;
                                    }
                                }
                                else if (m == "Exponent")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtExponent.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdExponent.IsEnabled = true;
                                    }
                                }
                                else if (m == "Dimensional Change")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtDimensionalChange.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdDimensionalChange.IsEnabled = true;
                                        }
                                    }
                                }
                                else if (m == "Flex Abrasion")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtFlexAbrasion.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdFlexAbrasion.IsEnabled = true;
                                        }
                                    }
                                }
                                else if (m == "Bow")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtBow.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdBow.IsEnabled = true;
                                    }
                                }
                                else if (m == "Skew")
                                {
                                    if (results[page].N != null)
                                    {
                                        txtSkew.Text = results[page].N.Value.ToString("#,##0.##");

                                        if (results[page].N > 0)
                                            cmdSkew.IsEnabled = true;
                                    }
                                }

                                else if (m == "Bending resistance")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtBENDING.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdBENDING.IsEnabled = true;
                                        }
                                    }
                                }

                                else if (m == "Flex Abrasion (Scott type)")
                                {
                                    if (results[page].N != null)
                                    {
                                        if (results[page].N > 0)
                                        {
                                            txtFLEX_SCOTT.Text = Math.Round((results[page].N / 2).Value, 0, MidpointRounding.AwayFromZero).ToString("#,##0.##");
                                            cmdFLEX_SCOTT.IsEnabled = true;
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        Clear();
                        chkLoad = false;
                    }
                }
                else
                {
                    Clear();
                    chkLoad = false;
                }

                return chkLoad;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region buttonEnabled
        private void buttonEnabled(bool enabled)
        {
            cmdWidth.IsEnabled = enabled;
            cmdUsableWidth.IsEnabled = enabled;
            cmdWidthofSilicone.IsEnabled = enabled;
            cmdNumberofthreads.IsEnabled = enabled;
            cmdTotalWeight.IsEnabled = enabled;

            cmdUncoatedweight.IsEnabled = enabled;
            cmdTotalcoatingweight.IsEnabled = enabled;
            cmdThickness.IsEnabled = enabled;
            cmdMaximumForce.IsEnabled = enabled;
            cmdElongationatMaximumForce.IsEnabled = enabled;

            cmdFlammability.IsEnabled = enabled;
            cmdEdgecombResistance.IsEnabled = enabled;
            cmdStiffness.IsEnabled = enabled;
            cmdTearForce.IsEnabled = enabled;
            cmdStaticAirPermeability.IsEnabled = enabled;

            cmdDynamicAirPermeability.IsEnabled = enabled;
            cmdExponent.IsEnabled = enabled;
            cmdDimensionalChange.IsEnabled = enabled;
            cmdFlexAbrasion.IsEnabled = enabled;
            cmdBow.IsEnabled = enabled;
            cmdSkew.IsEnabled = enabled;

            cmdBENDING.IsEnabled = enabled;
            cmdFLEX_SCOTT.IsEnabled = enabled;
        }
        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user)
        {
            if (opera != null)
            {
                opera = user;
            }
        }

        #endregion

    }
}

