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

using System.Globalization;
using System.IO;
using System.Collections;
using System.Threading;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for InspectionMCMenu.xaml
    /// </summary>
    public partial class SearchInspectionDataPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public SearchInspectionDataPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            ClearControl();
        }

        #endregion

        #region Internal Variables

        private List<InspectionMCItem> instList = null;

        string _inspecionLotNo = string.Empty;
        DateTime? _startDate = null;
        string _defectFileName = string.Empty;

        string P_FINISHINGLOT = string.Empty;
        string P_INSPECTIONLOT = string.Empty;
        DateTime? P_StartDate = null;

        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Private Methods
        
        private void LoadMacines()
        {
            try
            {
                instList = InspectionDataService.Instance.GetMachinesData();

                this.cbInstMC.ItemsSource = instList;
                this.cbInstMC.DisplayMemberPath = "DisplayName";
                this.cbInstMC.SelectedValuePath = "MCId";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        private void LoadInspectionData()
        {
            string _date = string.Empty;
            string _mc = string.Empty;

            if (dteInspectionDate.SelectedDate != null)
            {
                _date = dteInspectionDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            }

            if (chkAll.IsChecked == false)
            {
                if (cbInstMC.SelectedValue != null)
                {
                    if (cbInstMC.SelectedValue != null)
                    {
                        _mc = cbInstMC.SelectedValue.ToString();
                    }
                }
            }

            ClearControl();

            List<INS_SearchInspectionData> lots = new List<INS_SearchInspectionData>();

            lots = InspectionDataService.Instance.GetINS_SearchinspectionData(_date, _mc);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridInsLots.ItemsSource = lots;
            }
            else
            {
                gridInsLots.ItemsSource = null;
            }
        }

        private void ClearControl()
        {
            if (cmdAttach.IsEnabled == true)
                cmdAttach.IsEnabled = false;

            if (cmdView.IsEnabled == true)
                cmdView.IsEnabled = false;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridInsLots.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridInsLots.SelectedItems.Clear();
            else
                this.gridInsLots.SelectedItem = null;

            _inspecionLotNo = "";
            _startDate = null;
            _defectFileName = "";

            P_FINISHINGLOT = string.Empty;
            P_INSPECTIONLOT = string.Empty;
            P_StartDate = null;

            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;
        }

        private void AttachFile()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif|Bitmap Files (.bmp)|*.bmp|Tiff Files (.tiff)|*.tiff|Wmf Files (.wmf)|*.wmf";
            dlg.Filter = "All Files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string sourceFile = dlg.FileName;

                try
                {
                    string destinationFile = dlg.SafeFileName;

                    if (destinationFile != "")
                    {
                        CopyFile(sourceFile, destinationFile);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(true);
                }
            }
        }

        private void CopyFile(string sourceFile, string destinationFile)
        {
            string pathDefectFile = ConfigManager.Instance.DefectFileConfig;
            string destinationInfo = pathDefectFile + destinationFile;

            try
            {
                try
                {
                    if (!System.IO.Directory.Exists(pathDefectFile))
                    {
                        System.IO.Directory.CreateDirectory(pathDefectFile);
                    }

                    File.Copy(sourceFile, destinationInfo, true);

                    if (InspectionDataService.Instance.UpdateInspectionDefectFileNameProcess(_inspecionLotNo, _startDate.Value, destinationInfo) == true)
                    {
                        LoadInspectionData();
                    }
                    else
                    {
                        "Can't Update Inspection Defect File Name".ShowMessageBox(false);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(true);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void ViewFile(string destinationInfo)
        {
            if (null != instList)
            {
                this.ShowViewDefectBox(destinationInfo);
            }
        }

        #endregion

        #region strRight

        private string strRight(string value, int length)
        {
            if (String.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }

        #endregion

        #region SaveFile

        private void SaveFile(string destinationInfo)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            int index = 0;
            string type = string.Empty;
            string pathDefectFile = string.Empty;

            try
            {
                index = destinationInfo.Trim().IndexOf(".");
                type = ((destinationInfo.Length) > index ? destinationInfo.Substring(index, (destinationInfo.Length - (index))) : "");
                dlg.Filter = "Files (*" + type + ")|*" + type;   
            }
            catch
            {
                dlg.Filter = "Files (*.*)|*.*";
            }

            try
            {
                pathDefectFile = ConfigManager.Instance.DefectFileConfig;
                dlg.FileName = destinationInfo.Remove(0, pathDefectFile.Trim().Length);
            }
            catch
            {
                if (type != "")
                    dlg.FileName = "Temp" + type;
            }

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string sourceFile = dlg.FileName;

                try
                {
                    File.Copy(destinationInfo, sourceFile, true);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(true);
                }
            }
        }

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMacines();

            // Load config
            ConfigManager.Instance.LoadDefectConfigs();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region gridInsLots_SelectedCellsChanged

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        private void gridInsLots_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridInsLots.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridInsLots);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            P_FINISHINGLOT = string.Empty;
                            P_INSPECTIONLOT = string.Empty;
                            P_StartDate = null;

                            PRODID = null;
                            HEADERID = null;

                            P_LOTNO = string.Empty;
                            P_ITEMID = string.Empty;
                            P_LOADINGTYPE = string.Empty;

                            if (((LuckyTex.Models.INS_SearchInspectionData)(gridInsLots.CurrentCell.Item)).FINISHINGLOT != null)
                            {
                                P_FINISHINGLOT = ((LuckyTex.Models.INS_SearchInspectionData)(gridInsLots.CurrentCell.Item)).FINISHINGLOT;
                            }

                            if (((LuckyTex.Models.INS_SearchInspectionData)(gridInsLots.CurrentCell.Item)).INSPECTIONLOT != null)
                            {
                                _inspecionLotNo = ((LuckyTex.Models.INS_SearchInspectionData)(gridInsLots.CurrentCell.Item)).INSPECTIONLOT;

                                if (((LuckyTex.Models.INS_SearchInspectionData)(gridInsLots.CurrentCell.Item)).STARTDATE != null)
                                {
                                    _startDate = ((LuckyTex.Models.INS_SearchInspectionData)(gridInsLots.CurrentCell.Item)).STARTDATE;
                                }

                                if (!string.IsNullOrEmpty(_inspecionLotNo))
                                    P_INSPECTIONLOT = _inspecionLotNo;

                                if (_startDate != null)
                                    P_StartDate = _startDate;

                                if (cmdAttach.IsEnabled == false)
                                    cmdAttach.IsEnabled = true;
                            }
                            else
                            {
                                if (cmdAttach.IsEnabled == true)
                                    cmdAttach.IsEnabled = false;

                                _inspecionLotNo = "";
                                _startDate = null;
                            }

                            if (((LuckyTex.Models.INS_SearchInspectionData)(gridInsLots.CurrentCell.Item)).DEFECTFILENAME != null)
                            {
                                _defectFileName = ((LuckyTex.Models.INS_SearchInspectionData)(gridInsLots.CurrentCell.Item)).DEFECTFILENAME;

                                if (cmdView.IsEnabled == false)
                                    cmdView.IsEnabled = true;
                            }
                            else
                            {
                                if (cmdView.IsEnabled == true)
                                    cmdView.IsEnabled = false;

                                _defectFileName = "";
                            }
                        }
                    }
                }
                else
                {
                    P_FINISHINGLOT = string.Empty;
                    P_INSPECTIONLOT = string.Empty;
                    P_StartDate = null;

                    PRODID = null;
                    HEADERID = null;

                    P_LOTNO = string.Empty;
                    P_ITEMID = string.Empty;
                    P_LOADINGTYPE = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Button

        #region cmdAttach_Click

        private void cmdAttach_Click(object sender, RoutedEventArgs e)
        {
            if (_inspecionLotNo != "")
            {
                AttachFile();
            }
        }

        #endregion

        #region cmdView_Click

        private void cmdView_Click(object sender, RoutedEventArgs e)
        {
            if (_defectFileName != "")
            {
                SaveFile(_defectFileName);
                //ViewFile(_defectFileName);
            }
            else
            {
                "No Defect File Attach for This Inspection Lot".ShowMessageBox(false);
            }
        }

        #endregion

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadInspectionData();
        }

        #endregion

        private void cmdSendD365_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
            {
                if (D365_IN_BPO() == true)
                {
                    if (PRODID != null)
                    {
                        if (PRODID != 0)
                        {
                            #region D365_IN_ISH
                            if (D365_IN_ISH(PRODID) == true)
                            {
                                if (HEADERID != null)
                                {
                                    if (D365_IN_ISL(HEADERID) == true)
                                    {
                                        if (D365_IN_OPH(PRODID) == true)
                                        {
                                            if (HEADERID != null)
                                            {
                                                if (D365_IN_OPL(HEADERID) == true)
                                                {
                                                    if (D365_IN_OUH(PRODID) == true)
                                                    {
                                                        if (HEADERID != null)
                                                        {
                                                            if (D365_IN_OUL(HEADERID) == true)
                                                            {
                                                                "Send D365 complete".ShowMessageBox();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            "HEADERID is null".Info();
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                "HEADERID is null".Info();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    "HEADERID is null".Info();
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region D365_IN_OPH
                            if (D365_IN_OPH(PRODID) == true)
                            {
                                if (HEADERID != null)
                                {
                                    if (D365_IN_OPL(HEADERID) == true)
                                    {
                                        if (D365_IN_OUH(PRODID) == true)
                                        {
                                            if (HEADERID != null)
                                            {
                                                if (D365_IN_OUL(HEADERID) == true)
                                                {
                                                    "Send D365 complete".ShowMessageBox();
                                                }
                                            }
                                            else
                                            {
                                                "HEADERID is null".Info();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    "HEADERID is null".Info();
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        "PRODID is null".Info();
                    }
                }
            }
            else
            {
                "Inspection Lot is null".ShowMessageBox();
            }
        }

        #endregion

        #region private Methods

        #region D365_IN_BPO
        private bool D365_IN_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_IN_BPOData> results = new List<ListD365_IN_BPOData>();

                results = D365DataService.Instance.D365_IN_BPO(P_FINISHINGLOT, P_INSPECTIONLOT,P_StartDate);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].PRODID != null)
                            PRODID = Convert.ToInt64(results[i].PRODID);
                        else
                            PRODID = null;

                        if (!string.IsNullOrEmpty(results[i].LOTNO))
                            P_LOTNO = results[i].LOTNO;
                        else
                            P_LOTNO = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].ITEMID))
                            P_ITEMID = results[i].ITEMID;
                        else
                            P_ITEMID = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].LOADINGTYPE))
                            P_LOADINGTYPE = results[i].LOADINGTYPE;
                        else
                            P_LOADINGTYPE = string.Empty;

                        if (PRODID != null)
                        {
                            if (PRODID != 0)
                            {
                                chkError = D365DataService.Instance.Insert_ABBPO(PRODID, results[i].LOTNO, results[i].ITEMID, results[i].LOADINGTYPE, 0, "N", results[i].QTY, results[i].UNIT, results[i].OPERATION);

                                if (!string.IsNullOrEmpty(chkError))
                                {
                                    chkError.Err();
                                    chkError.ShowMessageBox();
                                    chkD365 = false;
                                    break;
                                }
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_BPO Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_ISH
        private bool D365_IN_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_IN_ISHData> results = new List<D365_IN_ISHData>();

                results = D365DataService.Instance.D365_IN_ISH(P_FINISHINGLOT, P_INSPECTIONLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABISH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_ISH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_ISL
        private bool D365_IN_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_IN_ISLData> results = new List<ListD365_IN_ISLData>();

                results = D365DataService.Instance.D365_IN_ISL(P_FINISHINGLOT, P_INSPECTIONLOT, P_StartDate);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string issDate = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].ISSUEDATE != null)
                            issDate = results[i].ISSUEDATE.Value.ToString("yyyy-MM-dd");
                        else
                            issDate = string.Empty;

                        chkError = D365DataService.Instance.Insert_ABISL(HEADERID, results[i].LINENO, "N", 0, issDate, results[i].ITEMID, results[i].STYLEID, results[i].QTY, results[i].UNIT, results[i].SERIALID);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_ISL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_OPH
        private bool D365_IN_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_IN_OPHData> results = new List<D365_IN_OPHData>();

                results = D365DataService.Instance.D365_IN_OPH(P_INSPECTIONLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOPH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_OPH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_OPL
        private bool D365_IN_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_IN_OPLData> results = new List<ListD365_IN_OPLData>();

                results = D365DataService.Instance.D365_IN_OPL(P_INSPECTIONLOT, P_StartDate);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        chkError = D365DataService.Instance.Insert_ABOPL(HEADERID, results[i].LINENO, "N", 0, results[i].PROCQTY, results[i].OPRNO, results[i].OPRID, results[i].MACHINENO, results[i].STARTDATETIME, results[i].ENDDATETIME);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_OPL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_OUH
        private bool D365_IN_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_IN_OUHData> results = new List<D365_IN_OUHData>();

                results = D365DataService.Instance.D365_IN_OUH(P_INSPECTIONLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_OUH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_OUL
        private bool D365_IN_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_IN_OULData> results = new List<ListD365_IN_OULData>();

                results = D365DataService.Instance.D365_IN_OUL(P_FINISHINGLOT, P_INSPECTIONLOT, P_StartDate);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string outputDate = string.Empty;
                    int? finish = null;

                    foreach (var row in results)
                    {
                        if (results[i].OUTPUTDATE != null)
                            outputDate = results[i].OUTPUTDATE.Value.ToString("yyyy-MM-dd");
                        else
                            outputDate = string.Empty;

                        if (results[i].FINISH != null)
                            finish = Convert.ToInt32(results[i].FINISH);
                        else
                            finish = 0;

                        chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, "N", 0, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
                            , results[i].GROSSWEIGHT, results[i].NETWEIGHT, results[i].PALLETNO, results[i].GRADE, results[i].SERIALID, results[i].LOADINGTYPE, finish, results[i].MOVEMENTTRANS, results[i].WAREHOUSE, results[i].LOCATION);


                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_OUL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #endregion
    }
}
