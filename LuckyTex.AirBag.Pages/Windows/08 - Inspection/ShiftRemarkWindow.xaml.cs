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
using System.Collections;

using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using DataControl.ClassData;

#endregion

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for ShiftRemarkWindow.xaml
    /// </summary>
    public partial class ShiftRemarkWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public ShiftRemarkWindow()
        {
            InitializeComponent();
            ClearControl();

            txtInspectorRemark.IsEnabled = false;
        }

        #endregion

        #region Internal Variables

        private string insLotNo = string.Empty;
        private DateTime? startDate = null;
        private bool _useShift = false;
        private bool _useRemark = false;
        
        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            //if (rbShiftRemark.IsChecked == true)
            //{
                if (SaveShiftRemark(insLotNo) == true)
                {
                    "Shift Remark Save Commplete".ShowMessageBox();
                }
            //}
            //else
            //{
            //    "Please select Shift Remark".ShowMessageBox();
            //}
        }

        private void cmdYes_Click(object sender, RoutedEventArgs e)
        {
            if (rbShiftRemark.IsChecked == true)
            {
                _useShift = true;
            }
            else
            {
                _useShift = false;
            }
            _useRemark = true;

            this.DialogResult = true;
        }

        private void cmdNo_Click(object sender, RoutedEventArgs e)
        {
            _useRemark = false;
            this.DialogResult = false;
        }

        #endregion

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region txtInspectorRemark_KeyDown

        private void txtInspectorRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtShiftRemark.Focus();
                txtShiftRemark.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtShiftRemark_KeyDown

        private void txtShiftRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtShiftID.Focus();
                txtShiftID.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtShiftID_KeyDown

        private void txtShiftID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdYes.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #endregion

        #region ClearControl

        private void ClearControl()
        {
            rbInspectorRemark.IsChecked = true;
            rbShiftRemark.IsChecked = false;

            txtInspectorRemark.Text = string.Empty;
            txtShiftRemark.Text = string.Empty;
            txtShiftID.Text = string.Empty;

            rbInspectorRemark.Focus();
        }

        #endregion

        #region Private Properties

        #region SaveShiftRemark

        private bool SaveShiftRemark(string P_INSLOT)
        {
            try
            {
                string P_SHIFTID = string.Empty;
                string P_SHIFTREMARK = string.Empty;

                if (!string.IsNullOrEmpty(txtShiftID.Text))
                    P_SHIFTID = txtShiftID.Text;

                if (!string.IsNullOrEmpty(txtShiftRemark.Text))
                    P_SHIFTREMARK = txtShiftRemark.Text;

                if (!string.IsNullOrEmpty(P_SHIFTID) && !string.IsNullOrEmpty(P_SHIFTREMARK))
                {
                    string save = InspectionDataService.Instance.INS_SHIFTREMARK(P_INSLOT,startDate, P_SHIFTID, P_SHIFTREMARK);

                    if (string.IsNullOrEmpty(save))
                    {
                        return true;
                    }
                    else
                    {
                        save.ShowMessageBox(true);
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(P_SHIFTID))
                        "Shift ID isn't null".ShowMessageBox(true);
                    else if (string.IsNullOrEmpty(P_SHIFTREMARK))
                        "Shift Remark isn't null".ShowMessageBox(true);

                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);

                return false;
            }
        }

        #endregion

        #endregion

        #region Public Properties

        public void Setup(string InsLotNo, DateTime STARTDATE)
        {
            insLotNo = InsLotNo;
            startDate = STARTDATE;

            if (!string.IsNullOrEmpty(insLotNo))
            {
                 List<InspectionReportData> lots =
                       InspectionDataService.Instance.GetInspectionReportData(insLotNo);
                 if (null != lots && lots.Count > 0 && null != lots[0])
                 {
                     txtInspectorRemark.Text = lots[0].REMARK;

                     if (!string.IsNullOrEmpty(lots[0].SHIFT_ID))
                         txtShiftID.Text = lots[0].SHIFT_ID;

                     txtShiftRemark.Text = lots[0].SHIFT_REMARK;
                 }
            }
        }

        /// <summary>
        /// Gets or sets Message Text.
        /// </summary>
        public bool useShiftRemark
        {
            get { return _useShift; }
            set
            {
                if (_useShift != value)
                {
                    _useShift = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets Message Text.
        /// </summary>
        public bool useRemark
        {
            get { return _useRemark; }
            set
            {
                if (_useRemark != value)
                {
                    _useRemark = value;
                }
            }
        }
        #endregion
    }
}
