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

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for EditBeamNoWindow.xaml
    /// </summary>
    public partial class EditBeamNoWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public EditBeamNoWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables

        private string beamroll = string.Empty;
        private string oldNo = string.Empty;
        private string newNo = string.Empty;
   
        private string OperatorText = string.Empty;

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            txtBeamRoll.Text = beamroll;
            txtBeamerNo.Text = oldNo;
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            newNo = txtBeamerNo.Text; 

            if (!string.IsNullOrEmpty(beamroll) && !string.IsNullOrEmpty(newNo))
            {
                if (SaveBEAM_EDITNOBEAM(beamroll, newNo) == true)
                {
                    this.DialogResult = true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(newNo))
                {
                    "Beamer No isn't Null".ShowMessageBox(true);
                }
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region TextBox

        private void txtBeamRoll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBeamerNo.Focus();
                txtBeamerNo.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBeamerNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region ClearControl

        private void ClearControl()
        {
            txtBeamRoll.Text = string.Empty;
            txtBeamerNo.Text = string.Empty;

            txtBeamerNo.SelectAll();
            txtBeamerNo.Focus();
        }

        #endregion

        #region Private Properties

        #region SaveBEAM_EDITNOBEAM

        private bool SaveBEAM_EDITNOBEAM(string P_BEAMROLL, string newNo)
        {
            try
            {
                string result = string.Empty;

                result = BeamingDataService.Instance.BEAM_EDITNOBEAM(P_BEAMROLL, oldNo, newNo, OperatorText);

                if (string.IsNullOrEmpty(result))
                {
                    return true;
                }
                else
                {
                    result.ShowMessageBox(true);

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

        public string GetBeamerNo()
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(txtBeamerNo.Text))
            {
                result = txtBeamerNo.Text;
            }
            return result;
        }

        public void Setup(string P_BEAMROLL, string BeamerNo, string P_OPERATORID)
        {
            beamroll = P_BEAMROLL;
            oldNo = BeamerNo;

            OperatorText = P_OPERATORID;
        }

        #endregion

    }
}
