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

using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;

using System.Globalization;

using NLib;
using LuckyTex.Services;
using LuckyTex.Models;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for ImportExcelPage.xaml
    /// </summary>
    public partial class ImportExcelPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImportExcelPage()
        {
            InitializeComponent();

            cmdLoadExcel.IsEnabled = false;
            cmdSave.IsEnabled = false;
        }

        #endregion

        #region Internal Variables

        string strLogIn = string.Empty;
        string strFileName = string.Empty;
        private G3Session _session = new G3Session();

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        private void cmdImportExcel_Click(object sender, RoutedEventArgs e)
        {
            cmdLoadExcel.IsEnabled = false;
            cmdSave.IsEnabled = false;
            ConnDataExcel();
        }

        private void cmdLoadExcel_Click(object sender, RoutedEventArgs e)
        {
            BindingOperations.ClearAllBindings(gridImportExcel);
            this.gridImportExcel.ItemsSource = null;

            cmdSave.IsEnabled = false;

            if (cbSheetName.Text != "")
            {
                string table = cbSheetName.Text + "$";
                ReloadDataExcel(strFileName, table);
            }
            else
            {
                "Sheet Name not null".ShowMessageBox(false);
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (gridImportExcel.Items.Count > 0)
            {
                SaveData();
            }
        }

        #endregion

        #region private Methods

        #region ConnDataExcel
        private void ConnDataExcel()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Excel 97-2003 (*.xls)|*.xls|Excel 2007 (*.xlsx)|*.xlsx|Excel (*.xlsm)|*.xlsm";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                strFileName = dlg.FileName;
                try
                {
                    GetExcelSheetNames(strFileName);

                    if (this.cbSheetName.Items.Count > 0)
                    {
                        cbSheetName.SelectedIndex = 0;

                        cmdLoadExcel.IsEnabled = true;
                    }
                    else
                    {
                        cbSheetName.ItemsSource = null;
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(true);
                }

            }
        }
        #endregion

        #region GetExcelSheetNames
        private void GetExcelSheetNames(string strFileName)
        {
            System.Data.DataTable dt = null;
            System.Data.OleDb.OleDbConnection conn;
            FileInfo fileInfo = new FileInfo(strFileName);

            if (fileInfo.Extension == ".xlsx")
                conn = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1;MAXSCANROWS=0\"");
            else if (fileInfo.Extension == ".xlsm")
                conn = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties=\"Excel 12.0 Macro;HDR=YES\"");
            else
                conn = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName + ";Extended Properties=\"Excel 8.0;HDR=YES;\"");
            try
            {
                conn.Open();

                dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                int index = 0;
                string strSheetTableName = string.Empty;

                int i = 0;
                List<LuckyTex.Models.ListTableNameData> dataList = new List<LuckyTex.Models.ListTableNameData>();

                foreach (DataRow row in dt.Rows)
                {
                    LuckyTex.Models.ListTableNameData dataItem = new LuckyTex.Models.ListTableNameData();

                    strSheetTableName = row["TABLE_NAME"].ToString();
                    index = strSheetTableName.Trim().IndexOf("$");
                    
                    dataItem.SheetID = i;
                    dataItem.SheetDesc = (0 < (index) ? strSheetTableName.Substring(0, (index)) : "");

                    dataList.Add(dataItem);
                    i++;
                }

                this.cbSheetName.ItemsSource = dataList;
                this.cbSheetName.DisplayMemberPath = "SheetDesc";
                this.cbSheetName.SelectedValuePath = "SheetID";
            
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region GetDataTableExcel
        public static DataTable GetDataTableExcel(string strFileName, string strTable)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.OleDb.OleDbConnection conn;
            FileInfo fileInfo = new FileInfo(strFileName);

            if (fileInfo.Extension == ".xlsx")
                conn = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                    + strFileName + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1;MAXSCANROWS=0\"");
            else if (fileInfo.Extension == ".xlsm")
                conn = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                    + strFileName + ";Extended Properties=\"Excel 12.0 Macro;HDR=YES\"");
            else
                conn = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                    + strFileName + ";Extended Properties=\"Excel 8.0;HDR=YES;\"");

            try
            {
                conn.Open();

                string strQuery = "SELECT * FROM [" + strTable + "]";

                System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter(strQuery, conn);

                adapter.Fill(ds);

                conn.Close();
                conn.Dispose();

                return ds.Tables[0];
            }
            catch
            {
                conn.Close();
                conn.Dispose();
                return ds.Tables[0];
            }
        }
        #endregion

        #region ReloadDataExcel
        private void ReloadDataExcel(string filename,string table)
        {
            try
            {
                #region ExcelLoadAs400

                string temp = string.Empty;
                IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

                DataTable dataTable = GetDataTableExcel(filename, table);
                if (dataTable.Rows.Count > 0)
                {
                    List<LuckyTex.Models.ListExcelLoadAs400Data> listExcelLoadAs400 = new List<LuckyTex.Models.ListExcelLoadAs400Data>();
                    
                    string desc = string.Empty;
                    string itmyarn = string.Empty;

                    int index = 0;

                    foreach (var row in dataTable.Rows)
                    {
                        LuckyTex.Models.ListExcelLoadAs400Data dataItem = new ListExcelLoadAs400Data();

                        #region TruckNo

                        if (((System.Data.DataRow)(row))["TruckNo"] != DBNull.Value)
                        {
                            dataItem.TruckNo = ((System.Data.DataRow)(row))["TruckNo"].ToString();
                            
                        }
                        else
                        {
                            dataItem.TruckNo = "";
                        }

                        #endregion

                        #region Desc

                        if (((System.Data.DataRow)(row))["Desc"] != DBNull.Value)
                        {
                            desc = ((System.Data.DataRow)(row))["Desc"].ToString();
                            dataItem.Desc = desc;

                            try
                            {
                                itmyarn = GetDescItem(desc);

                                // ยังไม่ได้ใช้ CheckItemCodeData รอสรุป
                                //if (_session.CheckItemCodeData(itmyarn) == true)
                                //{
                                    dataItem.DescItem = itmyarn;
                                //}
                                //else
                                //{
                                //    dataItem.DescItem = "";
                                //}
                            }
                            catch
                            {
                                dataItem.DescItem = "";
                            }

                            try
                            {
                                index = desc.Trim().IndexOf("(");
                                dataItem.DescType = ((desc.Length) > index ? desc.Substring(index + 1, (desc.Length - 1 - (index + 1))) : "");
                            }
                            catch
                            {
                                dataItem.DescType = "";
                            }
                        }
                        else 
                        {
                            dataItem.Desc = "";
                        }

                        #endregion

                        #region TraceNo

                        if (((System.Data.DataRow)(row))["TraceNo"] != DBNull.Value)
                        {
                            dataItem.TraceNo = ((System.Data.DataRow)(row))["TraceNo"].ToString();
                        }
                        else
                        {
                            dataItem.TraceNo = string.Empty;
                        }

                        #endregion

                        #region Cone

                        if (((System.Data.DataRow)(row))["Cone"] != DBNull.Value)
                        {
                            dataItem.Cone = Convert.ToDecimal(((System.Data.DataRow)(row))["Cone"].ToString());
                        }
                        else
                        {
                            dataItem.Cone = 0;
                        }

                        #endregion

                        #region Qty

                        if (((System.Data.DataRow)(row))["Qty"] != DBNull.Value)
                        {
                            dataItem.Qty = Convert.ToDecimal(((System.Data.DataRow)(row))["Qty"].ToString());
                        }
                        else
                        {
                            dataItem.Qty = 0;
                        }

                        #endregion

                        #region Lot

                        if (((System.Data.DataRow)(row))["Lot"] != DBNull.Value)
                        {
                            dataItem.Lot = ((System.Data.DataRow)(row))["Lot"].ToString();
                        }
                        else
                        {
                            dataItem.Lot = string.Empty;
                        }

                        #endregion

                        #region Item

                        if (((System.Data.DataRow)(row))["Item"] != DBNull.Value)
                        {
                            dataItem.Item = ((System.Data.DataRow)(row))["Item"].ToString();
                        }
                        else
                        {
                            dataItem.Item = string.Empty;
                        }

                        #endregion

                        #region RecDt

                        if (((System.Data.DataRow)(row))["RecDt"] != DBNull.Value)
                        {
                            temp = ((System.Data.DataRow)(row))["RecDt"].ToString();

                            dataItem.RecDt = DateTime.ParseExact(temp, "dd/MM/yy", culture); ;
                        }
                        else
                        {
                            dataItem.RecDt = null;
                        }

                        #endregion

                        #region Um

                        if (((System.Data.DataRow)(row))["Um"] != DBNull.Value)
                        {
                            dataItem.Um = ((System.Data.DataRow)(row))["Um"].ToString();
                        }
                        else
                        {
                            dataItem.Um = string.Empty;
                        }

                        #endregion

                        listExcelLoadAs400.Add(dataItem);
                    }

                    dataTable.Dispose();

                    if (listExcelLoadAs400.Count > 0)
                    {
                        cmdSave.IsEnabled = true;
                        this.gridImportExcel.ItemsSource = listExcelLoadAs400;
                    }
                    else
                    {
                        cmdSave.IsEnabled = false;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region GetDescItem
        private string GetDescItem(string desc)
        {
            string descItem = string.Empty;
            try
            {
                int chkStr = 0;
                
                for (int i = 0; i < desc.Length; i++)
                {
                    if (desc[i].ToString() == "-")
                    {
                        chkStr++;
                        if (chkStr == 3)
                            break;
                    }
                    descItem += desc[i].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }

            return descItem;
        }
        #endregion

        #region SaveData

        private void SaveData()
        {
            #region ExcelLoadAs400

            string palletNo = string.Empty;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            int i = 0;

            foreach (var row in gridImportExcel.Items)
            {
                _session.New();

                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).TruckNo != null)
                {
                    _session.TruckNo = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).TruckNo;
                }
                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Desc != null)
                {
                    _session.Desc = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Desc;
                }
                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).DescItem != null)
                {
                    _session.ItmYarn = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).DescItem;
                }
                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).DescType != null)
                {
                    _session.Type = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).DescType;
                }
                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).TraceNo != null)
                {
                    _session.PalletNo = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).TraceNo;
                }
                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Cone != null)
                {
                    _session.CH = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Cone;
                }
                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Qty != null)
                {
                    _session.Weight = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Qty;
                }
                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Lot != null)
                {
                    _session.LotorderNo = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Lot;
                }
                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Item != null)
                {
                    _session.Itmorder = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Item;
                }
                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).RecDt != null)
                {
                    DateTime dtRec = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).RecDt.Value;

                    _session.ReceiveDate = dtRec.ToString("yyyy", culture) + "/" + dtRec.ToString("MM") + "/" + dtRec.ToString("dd") + " 00:00:00";

                    //_session.ReceiveDate = "TO_DATE('" + dtRec.ToString("yyyy", culture) + "/" + dtRec.ToString("MM") + "/" + dtRec.ToString("dd") + " 00:00:00', 'yyyy/mm/dd hh24:mi:ss')";
                }
                if (((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Um != null)
                {
                    _session.UM = ((LuckyTex.Models.ListExcelLoadAs400Data)((gridImportExcel.Items)[i])).Um;
                }

                if (_session.TruckNo != "" && _session.Desc != "")
                {
                    if (_session.SaveG3_yarn() == false)
                    {
                        palletNo += _session.PalletNo + "\r\n";
                    }
                }

                i++;
            }


            if (palletNo == "")
            {
                "Save Data Complete".ShowMessageBox(false);

                BindingOperations.ClearAllBindings(gridImportExcel);
                this.gridImportExcel.ItemsSource = null;
                cbSheetName.ItemsSource = null;

                cmdLoadExcel.IsEnabled = false;
                cmdSave.IsEnabled = false;

                _session.New();
            }
            else
            {
                string error = "Error on Save Data please try again: " + "\r\n Trace No Error \r\n" + palletNo;
                error.ShowMessageBox(true);
            }

            #endregion
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="logIn"></param>
        public void Setup(string logIn)
        {
            strLogIn = logIn;
        }

        #endregion
    }
}
